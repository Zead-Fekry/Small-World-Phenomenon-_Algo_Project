using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_algo1
{
    internal class Actor
    {
        List<string> movies;
        List<WeightedEdge> edges;
        List<Actor> prev;
        string name;

        public List<WeightedEdge> Edges { get { return edges; } }
        public List<Actor> Previous { get { return prev; } }
        public List<String> Movies { get { return movies; } }
        public string Name { get { return name; } set { this.name = value; } }
        public bool IsVisited { get; set; }
        public int Strength { get; set; }
        public int DegreeOfSepration { get; set; }
        public Actor(string value)
        {
            this.name = value;
            IsVisited = false;
            edges = new List<WeightedEdge>();
            prev = new List<Actor>();
            movies = new List<string>();
        }

        public Actor(string value, List<Actor> neighbors)
        {
            this.name = value;
            IsVisited = false;
        }

        public void AddEdge(WeightedEdge edge)
        {
            edges.Add(edge);
        }
        public void ReplacePrev(Actor actor)
        {
            prev.Clear();
            prev.Add(actor);
        }

    }
}
