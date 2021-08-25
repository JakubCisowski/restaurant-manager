using EasyCaching.Core.Configurations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestaurantManager.Api.Configs;
using RestaurantManager.Consts.Configs;
using RestaurantManager.Context;
using RestaurantManager.Core.Cache;
using RestaurantManager.Infrastructure.Repositories;
using RestaurantManager.Infrastructure.Repositories.Interfaces;
using RestaurantManager.Infrastructure.UnitOfWork;
using RestaurantManager.Services.RestaurantServices;
using RestaurantManager.Services.RestaurantServices.Interfaces;
using RestaurantManager.Services.Services.OrderServices;
using RestaurantManager.Services.Services.OrderServices.Interfaces;
using RestaurantManager.Services.Services.RestaurantServices;
using RestaurantManager.Services.Services.RestaurantServices.Interfaces;
using Serilog;

namespace RestaurantManager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddPooledDbContextFactory<RestaurantDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<RestaurantDbContext>();

            services.AddTransient<IRestaurantService, RestaurantService>();
            services.AddTransient<IDishService, DishService>();
            services.AddTransient<IIngredientService, IngredientService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IOrderNoGeneratorService, OrderNoGeneratorService>();
            services.AddTransient<IOrderCalculationService, OrderCalculationService>();
            services.AddTransient<IMenuService, MenuService>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IGeneratorLockService, GeneratorLockService>();

            services.AddMvc().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddEasyCaching(options =>
            {
                options.UseInMemory("restaurant_cache");

                //options.UseRedis(config =>
                //{
                //    config.DBConfig.Endpoints.Add(new ServerEndPoint("127.0.0.1", 6379));
                //}, "redis-cache");
            });

            services.AddScoped<ICacheService, EasyCacheService>();
            services.AddScoped<ICacheKeyService, CacheKeyService>();

            services.Configure<CacheConfig>(Configuration.GetSection(nameof(CacheConfig)));


            var keycloakConfig = new KeycloakConfig();
            Configuration.GetSection(nameof(KeycloakConfig)).Bind(keycloakConfig);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = keycloakConfig.Authority;
                    options.Audience = keycloakConfig.ClientId;
                    options.IncludeErrorDetails = true;
                    options.RequireHttpsMetadata = false; //for development
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        //ValidAudiences = new[] { "master-realm", "tetstst" },
                        ValidateIssuer = true,
                        ValidIssuer = keycloakConfig.Authority,
                        ValidateLifetime = false,
                    };
                });

            services.AddAuthorization();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestaurantManager.Api", Version = "v1" });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

                c.AddSecurityRequirement(securityRequirement);
            });

            services.AddSingleton(Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build())
                .CreateLogger());


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantManager.Api v1"));
            }

            app.UseHttpsRedirection();

            // Whenever request is made, serilog is going to log that.
            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
