using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Auth.Login.Dtos;
using PrimeShop.Application.features.Auth.Register.Dtos;
using PrimeShop.Application.features.Basket.Dtos;
using PrimeShop.Application.features.BasketItem.Dtos;
using PrimeShop.Application.features.Category.Dtos;
using PrimeShop.Application.features.Order.Dtos;
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


        [Get("/api/Product/GetAll")]
        Task<List<ProductDto>> GetProductsAsync([FromQuery] Guid? categoryId ,  decimal? minPrice ,  decimal? maxPrice );



        [Get("/api/Category/GetAll")]
        Task<List<CategoryDto>> GetCategoriesAsync();


        [Post("/api/Product/GetByIds")]
        Task<List<ProductDto>> GetProductsByIdsAsync([FromBody] List<Guid> ids);


        [Get("/api/Product/GetById")]
        Task<ProductDto> GetProductByIdAsync([FromQuery] Guid id);


        [Get("/api/Product/GetRandomProduct")]
        Task<List<ProductDto>> GetRandomProductBAsync([FromQuery] int count);

        [Get("/api/Order/GetByIdAndCustomerId")]
        Task<List<OrderDto>> GetOrderByIdAndCustomerIdAsync([FromQuery] Guid? orderId,  Guid? customerId);





        [Get("/api/Basket/GetByCustomerId")]
        Task<BasketDto> GetBasketByCustomerIdAsync([Query] Guid customerId);

        [Post("/api/Basket/Create")]
        Task<Guid> CreateBasketAsync([Body] CreateBasketCommand command);


        [Post("/api/BasketItem/Create")]
        Task<Guid> CreateBasketItemAsync([Body] CreateBasketItemCommand command);


        [Put("/api/BasketItem/Update")]
        Task<Guid> UpdateBasketItemAsync([Body] UpdateBasketItemCommand command);


        [Get("/api/BasketItem/GetByBasketIdBasketItems")]
        Task<List<BasketItemDto>> GetBasketItemsByBasketIdAsync([Query] Guid basketId);

    }
}
