using System;

using Cluster;
using DataSet;
using Utility;

using System.Diagnostics;
namespace AffinityPropagationClusteringt
{
    class Program
    {
        static void Main(string[] args)
        {
            //This is a simple driver program 

            Console.WriteLine("Testing:Driver program for Affinity Propagation clustering algorithm.");
            ToyDataset rnd = new ToyDataset();
            Stopwatch s = new Stopwatch();

            var data1 = rnd.DataSet();
            var sim = SimilarityMatrix.SparseSimilarityMatrix(data1);


            Console.WriteLine($"Data size:{data1.Length} ; SimilarityMatrix size:{sim.Length}");
            Console.WriteLine($"Start at:{DateTime.Now}");
            s.Start(); 
            try
            {
                AffinityPropagation model = new AffinityPropagation(data1.Length);
                var centers = model.Fit(sim);
                Print(centers);
            }
            catch (Exception e)
            {
                Console.WriteLine($"\a{e.Message}");
            }
            s.Stop();
            Console.WriteLine($"\nEnding at:{DateTime.Now}");
            Console.WriteLine($"Ellapsed time: {s.ElapsedMilliseconds} ms  | {s.Elapsed.TotalSeconds} s | {s.Elapsed.TotalMinutes} m");
            

          

        }

        public static void Print(int[] clusteredData)
        {
            Console.WriteLine();
            foreach (var s in clusteredData)
                Console.Write($"{s} ");
            Console.WriteLine();
        }

    }
}
