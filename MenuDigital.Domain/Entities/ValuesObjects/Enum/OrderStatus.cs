using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuDigital.Domain.Entities.ValuesObjects.Enum
{
    public enum OrderStatus
    {
        WaintingPayment = 0,
        Acepted = 1,
        Preparing = 2,
        OnTheWay = 3,
        Delivered = 4,
        Canceled = 5,

    }
}
