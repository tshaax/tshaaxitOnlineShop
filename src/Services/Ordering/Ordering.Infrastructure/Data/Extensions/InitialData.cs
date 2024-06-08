namespace Ordering.Infrastructure.Data.Extensions
{
    public class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            new List<Customer>()
            {
                Customer.Create(CustomerId.Of(new Guid("701DF4DE-B387-4DF8-821B-FC4914D029CA")), "mehmet", "mehemet@gmail.com"),
                Customer.Create(CustomerId.Of(new Guid("98C2E43D-2230-45C0-85A4-F1E93324C198")), "john", "john@gmail.com")
            };

        public static IEnumerable<Product> Products => new List<Product>()
        {
            Product.Create(ProductId.Of(new Guid("701DF4DE-B387-4DF8-821B-FC4914D029CA")), "produc_1",20.90m),
            Product.Create(ProductId.Of(new Guid("701DF4DE-B387-4DF8-821B-FC4914D029CB")), "produc_2",25.90m),
            Product.Create(ProductId.Of(new Guid("701DF4DE-B387-4DF8-821B-FC4914D029CC")), "produc_3",45.90m),
            Product.Create(ProductId.Of(new Guid("701DF4DE-B387-4DF8-821B-FC4914D029CD")), "produc_4",55.90m)
        };

        public static IEnumerable<Order> OrdersWithItems
        { 
            get
            {
                var address1 = Address.Of("memet", "ozkaya", "mehemt@gmail.com", "1st Street", "South Africa", "Gauteng", "2001");
                var address2 = Address.Of("memet", "ozkaya", "mehemt@gmail.com", "1st Street", "South Africa", "Gauteng", "2001");

                var payment1 = Payment.Of("mehmet", "55555555555544444", "12/04", "333", 1);
                var payment2 = Payment.Of("mehmet", "55222434555522244", "12/04", "232", 2);

                var order1 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("701DF4DE-B387-4DF8-821B-FC4914D029CA")),
                    OrderName.Of("ORD_1"),
                    shippingAddress: address1,
                    billingAddress: address2,
                    payment1
                    );

                order1.Add(ProductId.Of(new Guid("701DF4DE-B387-4DF8-821B-FC4914D029CA")), 2, 34.90m);
                order1.Add(ProductId.Of(new Guid("701DF4DE-B387-4DF8-821B-FC4914D029CB")), 1, 50);

                var order2 = Order.Create(
                      OrderId.Of(Guid.NewGuid()),
                      CustomerId.Of(new Guid("98C2E43D-2230-45C0-85A4-F1E93324C198")),
                      OrderName.Of("ORD_1"),
                      shippingAddress: address1,
                      billingAddress: address2,
                      payment1
                      );

                order2.Add(ProductId.Of(new Guid("701DF4DE-B387-4DF8-821B-FC4914D029CA")), 2, 34.90m);
                order2.Add(ProductId.Of(new Guid("701DF4DE-B387-4DF8-821B-FC4914D029CB")), 1, 50);

                return new List<Order> { order1, order2 };
            }
        }
    }
}
