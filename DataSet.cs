using System;
using System.Collections.Generic;
using System.IO;
using Utility;

namespace DataSet
{
    public class RandomDataSet
    {
        Random rnd;
        private int max = 0, min = 0;
        public RandomDataSet(int seed, int max = 1000 * 1000, int min = -1000 * 1000)
        {
            if (seed > 0)
                rnd = new Random(seed);
            else
                rnd = new Random();

            this.max = max;
            this.min = min;
        }
        public Point[] GenerateDatasetFloat(int data_size)
        {
            if (data_size < 2)
                data_size = 2;

            var arnd = new Point[data_size];

            float temp = 0.0f, temp1 = 0.0f;
            for (int i = 0; i < data_size; ++i)
            {
                temp = (float)rnd.NextDouble() * (max - min) + min;
                temp1 = (float)rnd.NextDouble() * (max - min) + min;
                arnd[i] = new Point(temp, temp1);
            }
            return arnd;
        }
        public Point[] GenerateDataset(int data_size)
        {
            if (data_size < 2)
                data_size = 2;

            var arnd = new Point[data_size];

            int temp = 0, temp1 = 0;
            for (int i = 0; i < data_size; ++i)
            {
                temp = rnd.Next() * (max - min) + min;
                temp1 = rnd.Next() * (max - min) + min;
                arnd[i] = new Point(temp, temp1);
            }
            return arnd;
        }

    }


    interface ITestDataSet
    {
        public Point[] DataSet();
        public int[] Centers();
    }


    public class ToyDataset : ITestDataSet
    {
        public int[] Centers()
        {
            return new int[25] { 2, 2, 2, 2, 2, 2, 6, 6, 6, 6, 2, 6, 2, 6, 6, 19, 19, 19, 19, 19, 19, 2, 19, 19, 6 };
        }

        public Point[] DataSet()
        {
            Point[] data = new Point[25];
            data[0] = new Point(-2.3415f, 3.6968f);
            data[1] = new Point(-1.1092f, 3.1117f);
            data[2] = new Point(-1.5669f, 1.8351f);
            data[3] = new Point(-2.6585f, 0.6649f);
            data[4] = new Point(-4.0317f, 2.8457f);
            data[5] = new Point(-3.081f, 2.1011f);
            data[6] = new Point(2.588f, 1.7819f);
            data[7] = new Point(3.2923f, 3.0585f);
            data[8] = new Point(4.0317f, 1.6223f);
            data[9] = new Point(3.081f, -0.6117f);
            data[10] = new Point(0.2641f, 0.3989f);
            data[11] = new Point(1.3204f, 2.2074f);
            data[12] = new Point(0.1937f, 3.6436f);
            data[13] = new Point(1.9542f, -0.5053f);
            data[14] = new Point(1.6373f, 1.4096f);
            data[15] = new Point(-0.1232f, -1.516f);
            data[16] = new Point(-1.3556f, -3.0585f);
            data[17] = new Point(0.0176f, -4.016f);
            data[18] = new Point(1.0035f, -3.5904f);
            data[19] = new Point(0.0176f, -2.4202f);
            data[20] = new Point(-1.5317f, -0.9309f);
            data[21] = new Point(-1.1444f, 0.5053f);
            data[22] = new Point(0.6162f, -1.516f);
            data[23] = new Point(1.7077f, -2.2074f);
            data[24] = new Point(2.0951f, 3.4309f);

            return data;
        }
    }

    public static class DataIO
    {
        public static List<Point> ReadCSV(string path)
        {
            List<Point> data = new List<Point>(20);
            float x = 0, y = 0;
            using (var reader = new StreamReader(path))
            {

                try
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        var col = line.Split(',');
                        x = float.Parse(col[0]);
                        y = float.Parse(col[1]);
                        data.Add(new Point(x, y));
                    }
                }
                catch (FormatException)
                {
                    throw new Exception("0x02:Invalid input, make sure that your input is of the following format\n float,float,float");
                }
                catch (Exception)
                {
                    throw new Exception("0x01:Unkown exception in csv module");
                }
            }
            return data;
        }
        public static List<Edge> ReadSim(string path)
        {
            List<Edge> data = new List<Edge>(10000);
            int i, j;
            float sim = 0;
            using (var reader = new StreamReader(path))
            {

                try
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        var col = line.Split(',');
                        i = int.Parse(col[0]);
                        j = int.Parse(col[1]);
                        sim = float.Parse(col[2]);
                        data.Add(new Edge(i, j, sim));
                    }
                }
                catch (FormatException)
                {
                    throw new Exception("0x02:Invalid input, make sure that your input is of the following format\n int,int,float");
                }
                catch (Exception)
                {
                    throw new Exception("0x01:Unkown exception in Sim module");
                }
                return data;
            }
        }
    }
}
