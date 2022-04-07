using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Common.Services.SetPrecision
{
    public static class SetPrecisionDoubleService
    {
        public static double SetPrecision(this double self,int precision)
        {
            self = (double)Math.Round(self * Math.Pow(10,precision))/Math.Pow(10,precision);

            return self;
        }
    }
}
