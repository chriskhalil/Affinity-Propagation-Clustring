using System;
using System.Collections.Generic;

namespace MachineLearning{
  public enum PreferenceType { minimum = 1, median = 2 }
    public class Edge :IComparable<Edge>
    {
        public int Source { get; set; }
        public int Destination { get; set; }
        public double Similarity { get; set; }
        public double Responsability { get; set; }
        public double Availability { get; set; }

        public Edge(int Source, int Destination, double Similarity)
        {
            this.Source = Source;
            this.Destination = Destination;
            this.Similarity = Similarity;
        }
       public int CompareTo(Edge obj)
        {
            return Similarity.CompareTo(obj.Similarity);
        }
    }
    
    public class Graph
    {
        public int VerticesCount { get; set; }
        public int SimilarityMatrixElementsCount;
        public   List<List<Edge>> outEdges;
        public   List<List<Edge>> inEdges;
        public   List<Edge> edges;


        public Graph(int numberOfVertices)
        {

            VerticesCount = numberOfVertices;
            SimilarityMatrixElementsCount = ((numberOfVertices-1)*numberOfVertices) + VerticesCount;
            //Initialize the lists // vectors
            outEdges = new List<List<Edge>>();
            inEdges = new List<List<Edge>>();
            edges = new List<Edge>(SimilarityMatrixElementsCount);
            for (int i = 0; i < VerticesCount; i++)
            {
                
                outEdges.Add(new List<Edge>(VerticesCount) { });
                inEdges.Add(new List<Edge>(VerticesCount) { });
            }

        }
    }
      public static class Tools{
        public static void Update(ref double variable, double newValue, double damping)
        {
            variable = damping * variable + (1.0 - damping) * newValue;
        }
        public static void Swap(ref double x, ref double y)
        {
            double tempswap = x;
            x = y;
            y = tempswap;
        }
        }
     public interface ICluster
    {
        List<int> Cluster(List<Edge> input);
    }
    public static class Similarity{
      public static  List<Edge> SimilarityMatrix(List<Point> ptr)
        {
            List<Edge> items = new List<Edge>();
            for (int i = 0; i < ptr.Count - 1; i++)
                for (int j = i + 1; j < ptr.Count; j++)
                {
                    items.Add(new Edge(i, j, -((ptr[i].x - ptr[j].x) * (ptr[i].x - ptr[j].x) + (ptr[i].y - ptr[j].y) * (ptr[i].y - ptr[j].y))));
                    items.Add(new Edge(j, i, -((ptr[i].x - ptr[j].x) * (ptr[i].x - ptr[j].x) + (ptr[i].y - ptr[j].y) * (ptr[i].y - ptr[j].y))));

                }
            return items;

        }
    }
    public class Point
    {
        public float x;
        public float y;
        public float z;
        public Point(float x, float y,float z=0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}