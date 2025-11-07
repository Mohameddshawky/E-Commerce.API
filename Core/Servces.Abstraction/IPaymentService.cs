using Shared.DTos.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IPaymentService
    {
        Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string BasketId);

    }
}
