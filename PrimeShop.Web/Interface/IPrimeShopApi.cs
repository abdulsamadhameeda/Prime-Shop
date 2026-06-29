using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Auth.Login.Dtos;
using PrimeShop.Application.features.Auth.Register.Dtos;
using PrimeShop.Application.features.Basket.Dtos;
using PrimeShop.Application.features.BasketItem.Dtos;
using PrimeShop.Application.features.Category.Dtos;
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


     

    }
}
