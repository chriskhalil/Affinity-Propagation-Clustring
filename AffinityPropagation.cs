using System;
using System.Collections.Generic;
namespace  MachineLearning.Clustering
{
    public class AffinityPropagation: ICluster
    {
        public HashSet<int> Centers {get;}
        private readonly int MaxIteration,Convergence;
        private readonly double damping;
        PreferenceType Type;
        private Graph _graph;
        private readonly Random rand;
        public AffinityPropagation(int number_of_points,PreferenceType Type=PreferenceType.median,
                                             double damping = 0.9, int MaxIteration = 1000, int Convergence = 200)
        {
            Centers =new HashSet<int>();
            _graph = new Graph(number_of_points);
            rand = new Random();
            this.Type=Type;
            this.damping=damping;
            this.MaxIteration=MaxIteration;
            this.Convergence=Convergence;
        }
  
        private void BuildGraph(List<Edge> points)
        {
            for (int i = 0; i < points.Count; ++i)
                _graph.edges.Add(points[i]);
            
            double pref = Preference();
            for (int i = 0; i < _graph.VerticesCount; ++i)
            {
                _graph.edges.Add(new Edge(i, i, pref));
            }

               for (int i = 0; i < _graph.edges.Count; ++i)
              {
            
                      Edge p = _graph.edges[i];
                    //Add noise to avoid degeneracies
                    p.Similarity += (1e-16 * p.Similarity + 1e-300) * (rand.Next() / (Int32.MaxValue + 1.0));

                    //add out/in edges to vertices
                     _graph.outEdges[p.Source].Add(p);
                     _graph.inEdges[p.Destination].Add(p);
                  
               }

            Console.WriteLine("\nGraph Constructed");
        }
        private double Preference()
        {
            double pref;

            if (Type == PreferenceType.minimum)
            {
                throw new NotImplementedException();
            }
            else 
            if (Type == PreferenceType.median)
            {
                var t=DateTime.Now;
               _graph.edges.Sort();
                int m=_graph.edges.Count;
                pref = m % 2 == 0 ? _graph.edges[m / 2].Similarity : ((_graph.edges[(m / 2) - 1].Similarity + _graph.edges[m / 2].Similarity) / 2.0);
                Console.WriteLine($"Preference :{pref}");
                Console.WriteLine($"Sort time  :{DateTime.Now - t}");
            }
            else
            {
                throw new NotImplementedException();
            }

            return pref;
        }

        private void UpdateResponsabilities()
        {
            List<Edge> edges;
            double max1,max2,argmax1;
            for (int i = 0; i < _graph.VerticesCount; ++i)
            {
                edges = _graph.outEdges[i];
                max1 = -Single.PositiveInfinity; max2 = -Single.PositiveInfinity;argmax1=-1;
                for (int k = 0; k < edges.Count; k++)
                {
                    double Similarity = edges[k].Similarity + edges[k].Availability;
                    if (Similarity > max1)
                    {
                        Tools.Swap(ref max1, ref Similarity);
                        argmax1 = k;
                    }
                    if (Similarity > max2)
                    {
                        max2 = Similarity;
                    }

                }
                //Update the Responsability
                for (int k = 0; k < edges.Count; ++k)
                {
                    if (k != argmax1)
                    {
                        double temp = edges[k].Responsability;
                        Tools.Update(ref temp, edges[k].Similarity - max1, damping);
                        edges[k].Responsability = temp;
                    }
                    else
                    {
                        double temp = edges[k].Responsability;
                        Tools.Update(ref temp, edges[k].Similarity - max2, damping);
                        edges[k].Responsability = temp;
                    }
                }//end of for
            }//end of outer for
        }
        private void UpdateAvailabilities()
        {
            List<Edge> edges;
            double sum;
            for (int k = 0; k < _graph.VerticesCount; ++k)
            {
                edges = _graph.inEdges[k];
                //calculate sum of positive responsabilities
                 sum= 0.0;
                for (int i = 0; i < edges.Count - 1; ++i)
                    sum += Math.Max(0.0, edges[i].Responsability);
                
                //calculate the availabilities
                double last = edges[edges.Count - 1].Responsability;
                for (int i = 0; i < edges.Count - 1; ++i)
                {
                    double temp1 = edges[i].Availability;
                    Tools.Update(ref temp1, Math.Min(0.0, last + sum - Math.Max(0.0, edges[i].Responsability)), damping);
                    edges[i].Availability = temp1;
                }
                //calculate self-Availability
                double temp = edges[edges.Count - 1].Availability;
                Tools.Update(ref temp, sum, damping);
                edges[edges.Count - 1].Availability = temp;
            }
        }
        private bool UpdateExamplars(List<int> examplar)
        {
            bool changed = false;
            List<Edge> edges;
            double maxValue;
            int argmax;
            for (int i = 0; i < _graph.VerticesCount; ++i)
            {
                edges = _graph.outEdges[i];
                maxValue = -Single.PositiveInfinity;
                argmax = i;
                for (int k = 0; k < edges.Count; ++k)
                {
                    double Similarity = edges[k].Availability + edges[k].Responsability;

                    if (Similarity > maxValue)
                    {
                        maxValue = Similarity;
                        argmax = edges[k].Destination;
                    }
                }

                if (examplar[i] != argmax)
                {
                    examplar[i] = argmax;
                    changed = true;
                    Centers.Clear();
                }
                Centers.Add(argmax);
            }
            return changed;
        }


        public List<int> Cluster(List<Edge> input)
        {
            BuildGraph(input);
            List<int> examplar = new List<int>(_graph.VerticesCount);
            for (int i = 0; i < _graph.VerticesCount; ++i)
                examplar.Add(-1);

            for (int i = 0, nochange = 0; i < MaxIteration && nochange < Convergence; ++i, ++nochange)
            {
                UpdateResponsabilities();
                UpdateAvailabilities();
                if (UpdateExamplars(examplar))
                  nochange = 0;
                Console.Write($"\rForcasted %:{(i + 1) * 100 / MaxIteration}%  ");
            }
            _graph = null;
            return examplar;
        }
    }



}