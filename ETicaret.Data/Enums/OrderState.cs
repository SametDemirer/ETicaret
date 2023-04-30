using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data.Enums
{
    public enum OrderState
    {
        OrderReceiving,

        OrderPreparing,

        OrderOnWay,

        OrderCancelled,

        OrderCompleted
        
    }
}
