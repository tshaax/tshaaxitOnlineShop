
using Ordering.Application.Dtos;

namespace Ordering.Application.Extensions
{
    public static class OrderExtensions
    {
        public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
        {
            return orders.Select ( order => new OrderDto(
                    Id: order.Id.Value,
                    CustomerId: order.CustomerId.Value,
                    OrderName: order.OrderName.Value,
                    ShippingAddress: new AddressDto(
                        order.ShippingAddress.FirstName,
                        order.ShippingAddress.LastName,
                        order.ShippingAddress.Country,
                        order.ShippingAddress.State,
                        order.ShippingAddress.EmailAddress,
                        order.ShippingAddress.AddressLine,
                        order.ShippingAddress.ZipCode),
                    BillingAddress: new AddressDto(
                        order.BillingAddress.FirstName,
                        order.BillingAddress.LastName,
                        order.BillingAddress.Country,
                        order.BillingAddress.State,
                        order.BillingAddress.EmailAddress,
                        order.BillingAddress.AddressLine,
                        order.BillingAddress.ZipCode),
                    Payment: new PaymentDto(
                         order.Payment.CardName,
                         order.Payment.CardNumber,
                         order.Payment.Expiration,
                         order.Payment.CVV,
                         order.Payment.PaymentMethod),
                    Status: order.Status,
                    OrderItems: order.OrderItems.Select(s => new OrderItemDto(s.OrderId.Value, s.ProductId.Value, s.Quantity, s.Price)).ToList()
                    ));
        }

        public static OrderDto ToOrderDto(this Order order)
        {
            return DtoFromOrder(order);
        }

        private static OrderDto DtoFromOrder(this Order order)
        {
            return new OrderDto(
                    Id: order.Id.Value,
                    CustomerId: order.CustomerId.Value,
                    OrderName: order.OrderName.Value,
                    ShippingAddress: new AddressDto(
                        order.ShippingAddress.FirstName,
                        order.ShippingAddress.LastName,
                        order.ShippingAddress.Country,
                        order.ShippingAddress.State,
                        order.ShippingAddress.EmailAddress,
                        order.ShippingAddress.AddressLine,
                        order.ShippingAddress.ZipCode),
                    BillingAddress: new AddressDto(
                        order.BillingAddress.FirstName,
                        order.BillingAddress.LastName,
                        order.BillingAddress.Country,
                        order.BillingAddress.State,
                        order.BillingAddress.EmailAddress,
                        order.BillingAddress.AddressLine,
                        order.BillingAddress.ZipCode),
                    Payment: new PaymentDto(
                         order.Payment.CardName,
                         order.Payment.CardNumber,
                         order.Payment.Expiration,
                         order.Payment.CVV,
                         order.Payment.PaymentMethod),
                    Status: order.Status,
                    OrderItems: order.OrderItems.Select(s => new OrderItemDto(s.OrderId.Value, s.ProductId.Value, s.Quantity, s.Price)).ToList()
                    );
        }
    }
}
