using GeoCoordinatePortable;
using RestaurantManager.Core.Helpers;
using RestaurantManager.Core.Integration;
using RestaurantManager.Entities.Orders;
using RestaurantManager.Entities.Restaurants;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.Services.OrderServices.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Services.Services.OrderServices
{
    public class AddressValidationService : IAddressValidationService
    {
        private readonly IGeocodingApiService _geocodingApiService;
        private readonly IGenericRepository<Restaurant> _restaurantAddressRepository;

        public AddressValidationService(IGeocodingApiService geocodingApiService,
                                        IUnitOfWork unitOfWork)
        {
            _geocodingApiService = geocodingApiService;
            _restaurantAddressRepository = unitOfWork.GetRepository<Restaurant>();
        }
        public async Task<AddressValidationResult> Validate(Guid restaurantId, ShippingAddress address)
        {

            var restaurant = _restaurantAddressRepository
                .FindMany(x => x.Id == restaurantId)
                .Select(x => new
                {
                    MaxShippingDistanceRadius =  5000, /*x.ShippingOptions.MaxShippingDistanceRadius,*/
                    Latitude = x.Address.Latitude,
                    Longitude = x.Address.Longitude
                }).First();


            var customerCoordinates = await _geocodingApiService.GetCordinatesFromAdressAsync(
                AddressHelper.GetAddressString(address.City, address.Address1, address.Address2));

            var distance = CalculateDistance(restaurant.Latitude, restaurant.Longitude, customerCoordinates.Latitude, customerCoordinates.Longitude);
            var isShippingAllowed = CheckShippingAvailability(distance, restaurant.MaxShippingDistanceRadius);

            return new AddressValidationResult(isShippingAllowed, distance, restaurant.MaxShippingDistanceRadius);

        }

        private bool CheckShippingAvailability(double distance, double maxShippingDistanceRadius)
        {
            return distance <= maxShippingDistanceRadius ? true : false;
        }

        private double CalculateDistance(double restaurantLatitude, double restaurantLongitude, double customerLatitude, double customerLongitude)
        {
            var restaurantCoord = new GeoCoordinate(restaurantLatitude, restaurantLongitude);
            var customerCoord = new GeoCoordinate(customerLatitude, customerLongitude);

            return restaurantCoord.GetDistanceTo(customerCoord);
        }
    }

    internal class RestaurantAddressResponse
    {
        public double MaxShippingDistanceRadius { get; internal set; }
        public double Latitude { get; internal set; }
        public double Longitude { get; internal set; }
    }

    public class AddressValidationResult
    {
        public AddressValidationResult(bool isShippingAllowed, double distance, double maxShippingDistanceRadius)
        {
            IsShippingAllowed = isShippingAllowed;
            Distance = distance;
            MaxShippingDistanceRadius = maxShippingDistanceRadius;
        }

        public bool IsShippingAllowed { get; }
        public double Distance { get; }
        public double MaxShippingDistanceRadius { get; }
    }
}
