﻿using System;

namespace lr4_Lagrange_Spline
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] xValues = new double[5] { 0, 3, 6, 8, 10 };
            double[] yValues = new double[5] { 5, 2, 0, 4, 6 };

            Console.WriteLine("Lagrange Polynomial Calculation:");
            LagrangeCalc(xValues, yValues);
            Console.WriteLine("Spline Calculation:");
            SplineCalc(xValues, yValues);
            Console.ReadKey();
        }

        static void SplineCalc(double[] xValues, double[] yValues)
        {
            const int size = 5;
            double[] mValues = new double[size] { 3.56606, -1.43213, -0.27028, 0.64508, -0.81004 };        // chto eto????
            double h = (xValues[size - 1] - xValues[0]) / 99;
            int step = 0;
            for (double i = xValues[0]; i <= xValues[size - 1] + h; i += h)
            {
                step++;
                double result = Spline(i, xValues, yValues, mValues, size);

                Console.WriteLine(step + " " + result);
            }
        }

        static void LagrangeCalc(double[] xValues, double[] yValues)
        {
            const int size = 5;
            double h = (xValues[size - 1] - xValues[0]) / 99;
            int step = 0;
            for (double i = xValues[0]; i <= xValues[size - 1] + h; i += h)
            {
                step++;
                Console.WriteLine(step + " " + InterpolateLagrangePolynomial(i, xValues, yValues, size));
            }
        }

        static double InterpolateLagrangePolynomial(double x, double[] xValues, double[] yValues, int size)
        {
            double lagrangePol = 0.0;
            for (int i = 0; i < size; i++)
            {
                double basicsPol = 1.0;
                for (int j = 0; j < size; j++)
                {
                    if (j != i)
                    {
                        basicsPol *= (x - xValues[j]) / (xValues[i] - xValues[j]);
                    }
                }
                lagrangePol += basicsPol * yValues[i];
            }

            return lagrangePol;
        }

        static double Spline(double x, double[] xValues, double[] yValues, double[] mValues, int size)
        {
            double[] s0 = new double[4] { 0.0, 0.0, 0.0, 0.0 };
            double[] s1 = new double[4] { 0.0, 0.0, 0.0, 0.0 };
            double[] s2 = new double[4] { 0.0, 0.0, 0.0, 0.0 };
            double[] s3 = new double[4] { 0.0, 0.0, 0.0, 0.0 };
            double[] h = new double[4] { 2.0, 3.0, 4.0, 4.0 };
            double[] d = new double[4] { 2.00000, 0.66667, -0.25000, 0.75000 };
            double[] S = new double[4] { 0.0, 0.0, 0.0, 0.0 };
            int i = 3;
            double currentSpline = 0.0;
            if (x < xValues[1])
            {
                i = 0;
            }
            if (x < xValues[2])
            {
                i = 1;
            }
            if (x < xValues[3])
            {
                i = 2;
            }
            if (x < xValues[4])
            {
                i = 3;
            }
            s0[i] = yValues[i];
            s1[i] = mValues[i] / 2;
            s2[i] = d[i] - (h[i] * (2 * mValues[i] + mValues[i + 1])) / 6;
            s3[i] = (mValues[i + 1] - mValues[i]) / (6 * h[i]);
            S[i] = s0[i] + s1[i] * x - s1[i] * xValues[i] + s2[i] * x * x - 2 * s2[i] * x * xValues[i] + s2[i] * xValues[i] * xValues[i] + s3[i] * x * x * x - 3 * s3[i] * x * x * xValues[i] + 3 * s3[i] * x * xValues[i] * xValues[i] - s3[i] * xValues[i] * xValues[i] * xValues[i];
            currentSpline = S[i];
            return currentSpline;
        }
    }
}
