namespace RestaurantManager.Services.Services.OrderServices.Interfaces
{
    public interface IOrderNoGeneratorService
    {
        int GenerateOrderNo(System.Guid restaurantId);
    }
}
