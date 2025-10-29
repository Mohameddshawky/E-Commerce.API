using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTos.BasketModule
{
    public record BasketDto
    {
        public string Id { get; init; }//to make it immutable
        public ICollection<BasketItemDto> basketItems { get; init; } = [];
    }
}
