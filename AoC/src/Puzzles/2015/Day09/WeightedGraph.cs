using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.src.Puzzles._2015.Day09
{
    internal class WeightedGraph
    {
        private Dictionary<string, List<Edge>> vertices = new Dictionary<string, List<Edge>>();

        public WeightedGraph(List<string> distances)
        {
            foreach (var distance in distances)
            {
                var subs = distance.Split(' ');
                AddEdge(subs[0], subs[2], int.Parse(subs[4]));
            }
        }

        public void AddEdge(string vertex1, string vertex2, int weight)
        {
            if (!vertices.ContainsKey(vertex1)) AddVertex(vertex1);
            if (!vertices.ContainsKey(vertex2)) AddVertex(vertex2);
            vertices[vertex1].Add(new Edge(vertex1, vertex2, weight));
            vertices[vertex2].Add(new Edge(vertex2, vertex1, weight));
        }

        private void AddVertex(string vertex) => vertices[vertex] = new List<Edge>();

        public void Print()
        {
            foreach (var vertex in vertices)
            {
                Console.WriteLine(vertex.Key);
                foreach(var edge in vertex.Value)
                {
                    Console.WriteLine($" -> {edge.To} ({edge.Weight})");
                }
            }
        }

        internal List<string> GetVertices() 
            => vertices.Keys.ToList();

        internal List<Edge> GetValidDestinationsFor(string vertex, List<string> visited)
            => vertices[vertex].Where(e => !visited.Contains(e.To)).ToList();  
    }

    internal class Edge
    {
        internal string From { get; init; }
        internal string To { get; init; }
        internal int Weight { get; init; }

        public Edge(string from, string to, int weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }
}
