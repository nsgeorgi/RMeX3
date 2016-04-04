using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMeX3
{
  public  class Trilateration
    {
        public double[] res1 = new double[5];
        public double[] res2 = new double[5];
        public  int[] res3 = new int[2];
        public double[] equation = new double[4];
        public int index = 0;
        public String y, a;

       /* public void runIt()
        {
            Trilateration tt = new Trilateration();
            Debug.WriteLine("Step 1: " + tt.config(100, 100, 50, tt.res1));
            Debug.WriteLine("Step 2: " + tt.config(160, 120, 36.06, tt.res2));
            Debug.WriteLine("Step 3: " + tt.subtractCircle(tt.res1, tt.res2));
            Debug.WriteLine("Step 4: " + tt.y);
            Debug.WriteLine("Step 5: " + tt.config(160, 120, 36.06, tt.res1));
            Debug.WriteLine("Step 6: " + tt.config(70, 150, 60.83, tt.res2));
            Debug.WriteLine("Step 7: " + tt.subtractCircle(tt.res1, tt.res2));
            Debug.WriteLine("Step 8: " + tt.y);
            //tt.solveForXAndY();
            //Debug.WriteLine("Step 9: " + tt.a);       
            //Debug.ReadLine();

        } */
        public String config(double x, double y, double r, double[] st)
        {
            String z = (-2 * x) + "x + " + (x * x) + " " + (-2 * y) + "y + " + (y * y) + " = " + (r * r);
            st[0] = (-2 * x); st[1] = (x * x); st[2] = (-2 * y); st[3] = (y * y); st[4] = (r * r);
            return z;
        }
       public  String subtractCircle(double[] st, double[] st2)
        {
            String z = (st[0] - st2[0]) + "x " + (st[1] - st2[1]) + " + " + (st[2] - st2[2]) + "y " + (st[3] - st2[3]) + " = " + (st[4] - st2[4]);
            try
            {
                y = "y = " + -1 * (st[0] - st2[0]) / (st[2] - st2[2]) + "x + " + ((st[4] - st2[4]) - ((st[1] - st2[1]) + (st[3] - st2[3]))) / (st[2] - st2[2]);
                equation[index++] = -1 * (st[0] - st2[0]) / (st[2] - st2[2]);
                equation[index++] = ((st[4] - st2[4]) - ((st[1] - st2[1]) + (st[3] - st2[3]))) / (st[2] - st2[2]);
            }
            catch (IndexOutOfRangeException a) { Debug.WriteLine("Out of bound"); }
            catch (ArithmeticException a) { Debug.WriteLine("Divide by zero"); }
            return z;
        }
        public int[] solveForXAndY()
        {
            a = (equation[0] + -1 * (equation[2])) + "x = " + (-1 * (equation[1]) + equation[3]);
            try
            {
                a += "\nStep 10: x = " + (-1 * (equation[1]) + equation[3]) / (equation[0] + -1 * (equation[2]));
                a += "\nStep 11: y = " + (equation[2] * (-1 * (equation[1]) + equation[3]) / (equation[0] + -1 * (equation[2])) + (equation[3]));
                res3[0] = (int)((-1 * (equation[1]) + equation[3]) / (equation[0] + -1 * (equation[2])));
                res3[1] = (int)((equation[2] * (-1 * (equation[1]) + equation[3]) / (equation[0] + -1 * (equation[2])) + (equation[3])));
            }
            catch (ArithmeticException a) { Debug.WriteLine("caugh this Zero exception: " + (equation[2] * (-1 * (equation[1]) + equation[3]))); Debug.WriteLine(a.ToString()); }
            index = 0;
            //Debug.WriteLine("\nx: " + res3[0] + " y: " + res3[1]);
            return res3;
        }
    }
}
