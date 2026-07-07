using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Auth.Login.Dtos;
using PrimeShop.Application.features.Auth.Register.Dtos;
using PrimeShop.Application.features.Basket.Dtos;
using PrimeShop.Application.features.BasketItem.Dtos;
using PrimeShop.Application.features.Category.Dtos;
using PrimeShop.Application.features.Lookup.Dtos;
using PrimeShop.Application.features.Order.Dtos;
using PrimeShop.Application.features.OrderItem.Dtos;
using PrimeShop.Application.features.Product.Dtos;
using Refit;

namespace PrimeShop.Web.Interface
{
    public interface IPrimeShopApi
    {

        [Post("/api/auth/login")]
        Task<string> LoginAsync([FromBody] LoginCommand command);

        [Post("/api/auth/register")]
        Task RegisterAsync([FromBody] RegisterCommand command);


        [Get("/api/Category/GetAll")]
        Task<List<CategoryDto>> GetCategoriesAsync();


        [Get("/api/Product/GetAll")]
        Task<List<ProductDto>> GetProductsAsync([FromQuery] Guid? categoryId ,  decimal? minPrice ,  decimal? maxPrice );

        [Post("/api/Product/GetByIds")]
        Task<List<ProductDto>> GetProductsByIdsAsync([FromBody] List<Guid> ids);

        [Get("/api/Product/GetById")]
        Task<ProductDto> GetProductByIdAsync([FromQuery] Guid id);

        [Get("/api/Product/GetRandomProduct")]
        Task<List<ProductDto>> GetRandomProductBAsync([FromQuery] int count);

        [Put("/api/Product/Update")]
        Task<Guid> UpdateProductBAsync([FromBody] UpdateProductCommand command);




        [Get("/api/Order/GetByIdAndCustomerId")]
        Task<List<OrderDto>> GetOrderByIdAndCustomerIdAsync([FromQuery] Guid? orderId,  Guid? customerId);

        [Get("/api/Order/GetByIdOrder")]
        Task<OrderDto> GetOrderByIdAsync([FromQuery] Guid orderId);

        [Post("/api/Order/Create")]
        Task<Guid> CreateOrderAsync([FromBody] CreateOrderCommand command);


        [Post("/api/OrderItem/Create")]
        Task<Guid> CreateOrderItemAsync([FromBody] CreateOrderItemCommand command);

        [Get("/api/OrderItem/GetAll")]
        Task<List<OrderItemDto>> GetOrderItemAsync([FromQuery] Guid? orderId);



        [Get("/api/Basket/GetByCustomerId")]
        Task<BasketDto> GetBasketByCustomerIdAsync([FromQuery] Guid customerId);

        [Post("/api/Basket/Create")]
        Task<Guid> CreateBasketAsync([FromBody] CreateBasketCommand command);

        [Delete("/api/Basket/Delete")]
        Task DeleteBasketAsync([FromQuery] Guid id);


        [Post("/api/BasketItem/Create")]
        Task<Guid> CreateBasketItemAsync([FromBody] CreateBasketItemCommand command);

        [Put("/api/BasketItem/Update")]
        Task<Guid> UpdateBasketItemAsync([FromBody] UpdateBasketItemCommand command);

        [Delete("/api/BasketItem/Delete")]
        Task DeleteBasketItemAsync([Query] Guid id);

        [Get("/api/BasketItem/GetByBasketIdBasketItems")]
        Task<List<BasketItemDto>> GetBasketItemsByBasketIdAsync([FromQuery] Guid basketId);


        [Get("/api/Lookup/GetSpecificType")] 
        Task<List<LookupDto>> GetSpecificTypeAsync([FromQuery] string type);




    }
}
