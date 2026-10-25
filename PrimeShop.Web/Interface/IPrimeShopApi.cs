using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.Common.Models;
using PrimeShop.Application.features.Auth.Login.Dtos;
using PrimeShop.Application.features.Auth.Register.Dtos;
using PrimeShop.Application.features.Basket.Dtos;
using PrimeShop.Application.features.BasketItem.Dtos;
using PrimeShop.Application.features.Category.Dtos;
using PrimeShop.Application.features.Contact.Dtos;
using PrimeShop.Application.features.Customer.Dtos;
using PrimeShop.Application.features.Lookup.Dtos;
using PrimeShop.Application.features.Order.Dtos;
using PrimeShop.Application.features.OrderItem.Dtos;
using PrimeShop.Application.features.Product.Dtos;
using PrimeShop.Application.features.Wishlist.Dtos;
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


        [Get("/api/Customer/GetTotalCustomers")]
        Task<int> GetTotalCustomersAsync();

        [Get("/api/Customer/GetTop6NewCustomers")]
        Task<List<CustomerDto>> GetTop6NewCustomersAsync();



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

        [Get("/api/Product/GetTotalProducts")]
        Task<int> GetTotalProductsAsync();

        [Post("/api/Product/datatable")]
        Task<DataTableResponse<ProductDto>> GetProductsDataAsync([FromBody] GetDataTableProductsQuery query);

        [Get("/api/Product/GetShopProducts")]
        Task<DataTableResponse<ProductDto>> GetShopProductsAsync([Query] GetShopProductsQuery query);




        [Get("/api/Order/GetByIdAndUserId")]
        Task<List<OrderDto>> GetByIdAndUserIdAsync([FromQuery] Guid? orderId,  string? userId);

        [Get("/api/Order/GetByIdOrder")]
        Task<OrderDto> GetOrderByIdAsync([FromQuery] Guid orderId);

        [Post("/api/Order/Create")]
        Task<Guid> CreateOrderAsync([FromBody] CreateOrderCommand command);

        [Get("/api/Order/GetTotalProfit")]
        Task<decimal> GetTotalProfitAsync();

        [Get("/api/Order/GetTotalOrders")]
        Task<int> GetTotalOrdersAsync();

        [Get("/api/Order/GetOrdersPerMonth")]
        Task<List<OrdersPerMonthDto>> GetOrdersPerMonthAsync([FromQuery] int year);

        [Get("/api/Order/GetOrderStatus")]
        Task<List<OrderStatusDto>> GetOrderStatusAsync();

        [Get("/api/Order/GetTodaysSales")]
        Task<decimal> GetTodaysSalesAsync();

        [Post("/api/order/datatable")]  
        Task<DataTableResponse<OrderDto>> GetOrdersDataAsync([FromBody] GetDataTableOrdersQuery query);

        [Delete("/api/Order/Delete")]
        Task DeleteOrderAsync([FromQuery] Guid id);

        [Put("/api/Order/Update")]
        Task<Guid> UpdateOrderAsync([Body] UpdateOrderCommand command);

        [Get("/api/Order/GetMyOrders")]
        Task<DataTableResponse<OrderDto>> GetMyOrdersAsync([Query] GetMyOrdersQuery query);



        [Post("/api/OrderItem/Create")]
        Task<Guid> CreateOrderItemAsync([FromBody] CreateOrderItemCommand command);

        [Get("/api/OrderItem/GetAll")]
        Task<List<OrderItemDto>> GetOrderItemAsync([FromQuery] Guid? orderId);




        [Get("/api/Basket/GetByUserId")]
        Task<BasketDto> GetBasketByCustomerIdAsync([FromQuery] string userId);

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





        [Post("/api/Wishlist/Add")]
        Task<Guid> AddToWishlistAsync([Body] AddToWishlistCommand command);

        [Get("/api/Wishlist/GetProductIdsByUserId")]
        Task<List<WishlistItemDto>> GetWishlistProductIdsByUserIdAsync([Query] string userId);
        [Delete("/api/Wishlist/Delete")]
        Task<bool> DeleteWishlistAsync([FromQuery] DeleteWishlistCommand command);






        [Post("/api/Contact/Create")]
        Task<Guid> CreateContactMessageAsync([Body] CreateContactMessageCommand command);








    }
}
