using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class data
    {
        public string[] strs { get; set; }
    }

    public class GED
    {
        public double getStringDistance(string Str1, string Str2)
        {
            double[,] distance;
            int strlen1 = Str1.Length;
            int strlen2 = Str2.Length;
            if (strlen1 == 0)
                return strlen2;
            if (strlen2 == 0)
                return strlen1;
            distance = new double[strlen1 + 1 , strlen2 + 1];

            for (int i = 0; i < strlen1; i++)
            {
                distance[i,0] = i;
            }
            for (int i = 0; i < strlen2; i++)
            {
                distance[0,i] = i;
            }
            for (int i = 1; i <= strlen1; i++)
            {
                char s1_i = Str1[i - 1];
                for (int j = 1; j <= strlen2; j++)
                {
                    char s2_j = Str2[j - 1];
                    if (s1_i == s2_j)
                    {
                        distance[i, j] = distance[i - 1, j - 1];
                    }
                    else
                    {
                        distance[i, j] = getMin(distance[i - 1, j], distance[i, j - 1], distance[i - 1, j - 1]) + 1;
                    }
                }
            }
            return distance[strlen1, strlen2];
        }

        public double getMin(double num1, double num2, double num3)
        {
            double min = num1;
            if (num2 < min)
            {
                min = num2;
            }
            if (num3 < min)
            {
                min = num3;
            }
            return min;
        }
    }
}













