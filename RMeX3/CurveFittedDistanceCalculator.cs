using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMeX3
{
    public class CurveFittedDistanceCalculator : AccuracyCalculator
    {
        private double mCoefficient1;
        private double mCoefficient2;
        private double mCoefficient3;
        /**
     * Construct a calculator with coefficients specific for the device's signal vs. distance
     * (Ify) --> we use these coefficients for now until we can derive more specific values
     *		having said that, you can make the variable constants. 
     * @param coefficient1 0.42093
     * @param coefficient2 6.9476
     * @param coefficient3 0.54992
     */
        public CurveFittedDistanceCalculator(double coefficient1, double coefficient2, double coefficient3)
        {
            mCoefficient1 = coefficient1;
            mCoefficient2 = coefficient2;
            mCoefficient3 = coefficient3;
        }

        /**
         * Calculated the estimated distance in meters to the beacon based on a reference rssi at 1m
         * and the known actual rssi at the current location
         *
         * @param txPower (Ify) --> "-77 for Kontakt and -59 for the blue ones"
         * @param rssi "signal received at client"
         * @return estimated distance
         */
        public double calculateAccuracy(int txPower, double rssi)
        {
            if (rssi == 0) return -1.0; // if we cannot determine accuracy, return -1.

            double ratio = rssi * 1.0 / txPower;

            if (ratio < 1.0) return Math.Pow(ratio, 10);
            else return (mCoefficient1) * Math.Pow(ratio, mCoefficient2) + mCoefficient3;
        }
    }
}
