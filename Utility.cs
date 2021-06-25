using System;

namespace Utility
{
    public class Point
    {
        private float[] _coordinates;
        public Point(params float[] coordinates)
        {
            _coordinates = new float[coordinates.Length];
            for (int i = 0; i < coordinates.Length; ++i)
                _coordinates[i] = coordinates[i];
        }
        public int Dimension { get { return _coordinates.Length; } }
        public float Coordinates(int index) { return _coordinates[index]; }
    }
    public class Edge : IComparable<Edge>
    {
        public int   Source         { get; set; }
        public int   Destination    { get; set; }
        public float Similarity     { get; set; }
        public float Responsability { get; set; }
        public float Availability   { get; set; }

        public Edge()
        {
            Source     = Destination                   = 0;
            Similarity = Responsability = Availability = 0.0f;
        }
        public Edge(int Source, int Destination, float Similarity)
        {
            this.Source         = Source;
            this.Destination    = Destination;
            this.Similarity     = Similarity;
            this.Responsability = 0;
            this.Availability   = 0;
        }
        public int CompareTo(Edge obj)
        {
            return Similarity.CompareTo(obj.Similarity);
        }
    }

    public class Graph
    {
        public int VerticesCount { get; private set; }
        public int SimMatrixElementsCount;

        public Edge[][] outEdges;
        public Edge[][] inEdges;
        public Edge[] Edges;


        public Graph(int vertices)
        {
            VerticesCount = vertices < 0 ? 0 : vertices;
            SimMatrixElementsCount = ((VerticesCount - 1) * VerticesCount) + VerticesCount;

            outEdges = new Edge[VerticesCount][];
            inEdges = new Edge[VerticesCount][];
            Edges = new Edge[SimMatrixElementsCount];

            for (int i = 0; i < VerticesCount; ++i)
            {
                outEdges[i] = new Edge[VerticesCount];
                inEdges[i] = new Edge[VerticesCount];

            }

        }

    }

 
    public static class Distance{
        public static float NegEuclidienDistance(Point x, Point y)
        {   //checking for dim x == dim y will hurt performance this should be done at init
            float f = 0.0f;
            for (int i = 0; i < x.Dimension; ++i)
                f += ((y.Coordinates(i) - x.Coordinates(i)) * (y.Coordinates(i) - x.Coordinates(i)));

            return -1 * f;

        }
    }
    public static class SimilarityMatrix
    {
        public static Edge[] SparseSimilarityMatrix(Point[] ptr)
        {
            Edge[] items = new Edge[ptr.Length * ptr.Length];
            int p = 0;
            for (int i = 0; i < ptr.Length - 1; i++)
                for (int j = i + 1; j < ptr.Length; j++)
                {
                    items[p]     = new Edge(i, j, Distance.NegEuclidienDistance(ptr[i], ptr[j]));
                    items[p + 1] = new Edge(j, i, Distance.NegEuclidienDistance(ptr[i], ptr[j]));
                    p += 2;
                }
            return items;
        }
    }

}
