using ETicaret.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data.Common
{
    public static class ProgressPercentage
    {      
        public static int GetProgressPercentage(OrderState status)
        {
            switch (status)
            {
                case OrderState.OrderReceiving:
                    return 25;
                case OrderState.OrderPreparing:
                    return 50;
                case OrderState.OrderOnWay:
                    return 75;
                case OrderState.OrderCompleted:
                    return 100;
                default:
                    return 0;
            }
        }
    }
}
