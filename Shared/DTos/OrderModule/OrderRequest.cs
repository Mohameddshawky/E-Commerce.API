using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTos.OrderModule
{
    public record OrderRequest
    {
        public AddressDto ShipToAddress { get; init; } = null!;
        public int DeliveryMethodID { get; init; }
        public string BasketId { get; init; } = string.Empty;
    }
}
