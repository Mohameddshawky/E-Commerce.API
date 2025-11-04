using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTos.OrderModule
{
    public record AddressDto
    {
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string Street { get; init; } = null!;
        public string City { get; init; } = null!;
        public string Country { get; init; } = null!;

    }
}
