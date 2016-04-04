using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMeX3
{
   public interface AccuracyCalculator
    {
        double calculateAccuracy(int txPower, double rssi);
    }
}
