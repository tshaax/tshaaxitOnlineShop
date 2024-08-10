using Shopping.Web.Models.Ordering;

namespace Shopping.Web.Services
{
    public interface IOrderingService
    {
        [Get("/odering-service/orders?pageIndex={pageIndex}&pageSize={pageSize}\"")]
        Task<GetOrdersResponse> GetOrders(int? pageIndex = 1,int? pageSize = 10);

        [Get("/odering-service/orders/{orderName}")]
        Task<GetOrdersByNameResponse> GetOrdersByName(string orderName);

        [Get("/odering-service/orders/customer/{customerId}")]
        Task<GetOrdersByCustomerResponse> GetOrdersByCustomer(Guid customerId);

    }
}
