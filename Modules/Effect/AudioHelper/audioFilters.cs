﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Based off the algorithms and pseudo-code in dspguide.com Chapter 20.

namespace VixenModules.Effect.AudioHelp
{
    public class filterCoefs
    {
        public double[] a;
        public double[] b;
    }

    class audioFilters
    {
        public static double[] highPass(double freq, int sampleRate, double[] data)
        {
            double fc = (double)freq / sampleRate;
            int numOfPoles = 0;
            if (fc <= .02) numOfPoles = 4;
            else if (fc <= .05) numOfPoles = 6;
            else if (fc <= .10) numOfPoles = 10;
            else if (fc <= .25) numOfPoles = 18;
            else if (fc <= .40) numOfPoles = 10;
            else if (fc <= .45) numOfPoles = 6;
            else numOfPoles = 4;

            filterCoefs coefs = generateCoefficients(fc, true, 2, numOfPoles);

            double[] paddedData = new double[data.Length + 100];
            double[] newData = new double[data.Length + 100];
            for (int i = 0; i < 50; i++)
            {
                paddedData[i] = 0;
                paddedData[paddedData.Length - i - 1] = 0;
            }
            for (int i = 0; i < data.Length; i++)
            {
                paddedData[i + 50] = data[i];
            }

            double current;
            double future;
            double past;

            for (int i = 20; i < (paddedData.Length - numOfPoles); i++)
            {
                current = paddedData[i] * coefs.a[0];
                future = 0;
                past = 0;
                for (int x = 1; x < coefs.a.Length; x++)
                {
                    future += paddedData[i + x] * coefs.a[x];
                    past += newData[i - x] * coefs.b[x - 1];
                }
                newData[i] = current + future + past;
            }

            double[] ret = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                ret[i] = newData[50 + i];
                if (ret[i] > 1)
                    ret[i] = 1;
                if (ret[i] < -1)
                    ret[i] = -1;
            }

            return ret;
        }

        public static double[] lowPass(double freq, int sampleRate, double[] data)
        {
            double fc = (double)freq / sampleRate;
            int numOfPoles = 0;
            if (fc <= .02) numOfPoles = 4;
            else if (fc <= .05) numOfPoles = 6;
            else if (fc <= .10) numOfPoles = 10;
            else if (fc <= .25) numOfPoles = 18;
            else if (fc <= .40) numOfPoles = 10;
            else if (fc <= .45) numOfPoles = 6;
            else numOfPoles = 4;

            filterCoefs coefs = generateCoefficients(fc, false, 2, numOfPoles);

            double[] paddedData = new double[data.Length + 100];
            double[] newData = new double[data.Length + 100];
            for (int i = 0; i < 50; i++)
            {
                paddedData[i] = 0;
                paddedData[paddedData.Length - i - 1] = 0;
            }
            for (int i = 0; i < data.Length; i++)
            {
                paddedData[i + 50] = data[i];
            }

            double current;
            double future;
            double past;

            for (int i = 20; i < (paddedData.Length - numOfPoles); i++)
            {
                current = paddedData[i] * coefs.a[0];
                future = 0;
                past = 0;
                for (int x = 1; x < coefs.a.Length; x++)
                {
                    future += paddedData[i + x] * coefs.a[x];
                    past += newData[i - x] * coefs.b[x - 1];
                }
                newData[i] = current + future + past;
            }

            double[] ret = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                ret[i] = newData[50 + i];
                if (ret[i] > 1)
                    ret[i] = 1;
                if (ret[i] < -1)
                    ret[i] = -1;
            }

            return ret;
        }

        /// <summary>
        /// dspguide.com Chapter 20
        /// </summary>
        /// <param name="frequencyCutoff">0 to .5 relative to sampling frequency</param>
        /// <param name="lowPass">false for lowpass, true for highpass</param>
        /// <param name="percentRipple">0 to 29 percent ripple, .5 recommended</param>
        /// <param name="numPoles">Number of poles, must be even</param>
        ///         
        public static filterCoefs generateCoefficients(double frequencyCutoff, bool highPass, double percentRipple, int numPoles)
        {
            double[] a = new double[22];
            double[] b = new double[22];
            double[] ta = new double[22];
            double[] tb = new double[22];

            double a0, a1, a2, b1, b2;

            for (int i = 0; i < 22; i++) { a[i] = 0; b[i] = 0; }
            a[2] = 1;
            b[2] = 1;

            double rp, ip, es, vx, kx, t, w, m, d, k = 0, x0, x1, x2, y1, y2;

            for (int p = 1; p <= (numPoles / 2); p++)
            {
                rp = -Math.Cos(Math.PI / (numPoles * 2) + (p - 1) * Math.PI / numPoles);
                ip = Math.Sin(Math.PI / (numPoles * 2) + (p - 1) * Math.PI / numPoles);

                if (percentRipple != 0)
                { //Warp from a circle to an ellipse
                    es = Math.Sqrt(Math.Pow(100 / (100 - percentRipple), 2) - 1);
                    vx = (1.0 / numPoles) * Math.Log((1.0 / es) + Math.Sqrt((1.0 / Math.Pow(es, 2)) + 1));
                    kx = (1.0 / numPoles) * Math.Log((1.0 / es) + Math.Sqrt((1.0 / Math.Pow(es, 2)) - 1));
                    kx = (Math.Exp(kx) + Math.Exp(-kx)) / 2;
                    rp = rp * ((Math.Exp(vx) - Math.Exp(-vx)) / 2) / kx;
                    ip = ip * ((Math.Exp(vx) + Math.Exp(-vx)) / 2) / kx;
                }

                //s-domain to z-domain conversion
                t = 2 * Math.Tan(.5);
                w = 2 * Math.PI * frequencyCutoff;
                m = Math.Pow(rp, 2) + Math.Pow(ip, 2);
                d = 4 - 4 * rp * t + m * Math.Pow(t, 2);
                x0 = Math.Pow(t, 2) / d;
                x1 = 2 * Math.Pow(t, 2) / d;
                x2 = Math.Pow(t, 2) / d;
                y1 = (8 - 2 * m * Math.Pow(t, 2)) / d;
                y2 = (-4 - 4 * rp * t - m * Math.Pow(t, 2)) / d;

                //lp to lp or lp to hp transform
                if (highPass) k = -Math.Cos(w / 2 + .5) / Math.Cos(w / 2 - .5);
                if (!highPass) k = Math.Sin(.5 - w / 2) / Math.Sin(.5 + w / 2);
                d = 1 + y1 * k - y2 * Math.Pow(k, 2);
                a0 = (x0 - x1 * k + x2 * Math.Pow(k, 2)) / d;
                a1 = (-2 * x0 * k + x1 + x1 * Math.Pow(k, 2) - 2 * x2 * k) / d;
                a2 = (x0 * Math.Pow(k, 2) - x1 * k + x2) / d;
                b1 = (2 * k + y1 + y1 * Math.Pow(k, 2) - 2 * y2 * k) / d;
                b2 = (-(Math.Pow(k, 2)) - y1 * k + y2) / d;
                if (highPass) a1 = -a1;
                if (highPass) b1 = -b1;

                for (int i = 0; i < 22; i++)
                {
                    ta[i] = a[i];
                    tb[i] = b[i];
                }

                for (int i = 2; i < 22; i++)
                {
                    a[i] = a0 * ta[i] + a1 * ta[i - 1] + a2 * ta[i - 2];
                    b[i] = tb[i] - b1 * tb[i - 1] - b2 * tb[i - 2];
                }

            }

            b[2] = 0;
            for (int i = 0; i < 20; i++)
            {
                a[i] = a[i + 2];
                b[i] = -b[i + 2];
            }

            double sa = 0, sb = 0;
            for (int i = 0; i < 20; i++)
            {
                if (!highPass) sa += a[i];
                if (!highPass) sb += b[i];
                if (highPass) sa += a[i] * Math.Pow(-1, i);
                if (highPass) sb += b[i] * Math.Pow(-1, i);
            }

            double gain = sa / (1 - sb);
            for (int i = 0; i < 20; i++)
                a[i] = a[i] / gain;

            filterCoefs ret = new filterCoefs();
            ret.a = new double[numPoles + 1];
            ret.b = new double[numPoles];
            for (int i = 0; i <= numPoles; i++)
                ret.a[i] = a[i];

            for (int i = 1; i <= numPoles; i++)
                ret.b[i - 1] = b[i];

            return ret;
        }


    }
}
