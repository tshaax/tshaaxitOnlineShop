using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int DefaultLegnth = 5;
        public string Value { get; }

        private OrderName(string value) => Value = value;

        public static OrderName Of(string value)
        {
            ArgumentNullException.ThrowIfNull(value);
          //  ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length , DefaultLegnth);

            return new OrderName(value);
        }
    }
}
