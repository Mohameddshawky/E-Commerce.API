using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DeliveryMethodNotFoundException(int Id):NotFoundException($"DeliveryMethod with Id {Id} was not found")
    {
    }
}
