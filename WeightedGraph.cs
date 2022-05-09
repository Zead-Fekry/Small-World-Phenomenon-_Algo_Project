using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_algo1
{
    internal class WeightedGraph
    {
        List<WeightedEdge> edges;
        List<Actor> vertices;
        public List<WeightedEdge> Edges { get { return edges; } }

        public WeightedGraph(List<Actor> vertices)
        {
            this.vertices = vertices;
        }
        public void AddEdge(WeightedEdge newEdge)
        {
            edges.Add(newEdge);
        }
        public void RemoveEdge(WeightedEdge edge)
        {
            edges.Remove(edge);
        }

        /// <summary>
        /// Pathfinding algorithms available: Dijkstra and AStar
        /// </summary>
        

        public List<List<string>> DijkstraSearch(Actor start, Actor end)
        {
            Queue<Actor> Queue = new Queue<Actor>();

            Initialize();
            start.DegreeOfSepration = 0;
            Queue.Enqueue(start);

            Actor current;
            int DOS = int.MaxValue;
            while (Queue.Count > 0)
            {
                current = Queue.Dequeue();

                if (!current.IsVisited && current.DegreeOfSepration <= DOS)
                {
                    current.IsVisited = true;

                    if (current.Equals(end))
                    {
                        DOS = current.DegreeOfSepration;
                        //break;
                    }

                    foreach (WeightedEdge edge in current.Edges)
                    {
                        Actor neighbor = edge.End;

                        if (neighbor.DegreeOfSepration <= current.DegreeOfSepration)
                            continue;

                        int newStrength = current.Strength + edge.Weight;
                        int neighborStrength = neighbor.Strength;

                        if(newStrength == neighborStrength)
                        {
                            neighbor.Previous.Add(current);
                        }
                        else if (newStrength > neighborStrength)
                        {
                            neighbor.Strength = newStrength;
                            neighbor.DegreeOfSepration = current.DegreeOfSepration+1;
                            neighbor.ReplacePrev(current);
                            Queue.Enqueue(neighbor);
                        }
                    }
                }
            }
            List<List<Actor>> path = ReconstructPath( start, end);
            List<List<string>> movies = ReconstructPathMovies(start, end);
            return movies;
        }

        
        public void Initialize()
        {
            foreach (Actor vertex in vertices)
            {
                vertex.Strength = 0;
                vertex.IsVisited = false;
                vertex.Previous.Clear();
                vertex.DegreeOfSepration = int.MaxValue;
            }

        }

        public List<List<Actor>> ReconstructPath(Actor start, Actor end)
        {
            Queue<List<Actor>> NotReadychains = new Queue<List<Actor>>();
            List<List<Actor>> Readychains = new List<List<Actor>>();
            Actor current = end;
            List<Actor> myChain = new List<Actor>();
            myChain.Add(end);
            NotReadychains.Enqueue(myChain);
            while(NotReadychains.Count > 0)
            {
                List<Actor> chain = NotReadychains.Dequeue();
                foreach (Actor child in chain.Last().Previous)
                {
                    List<Actor> chainTemp = new List<Actor>(chain);
                    chainTemp.Add(child);
                    if (child.Equals(start))
                        Readychains.Add(chainTemp);
                    else
                        NotReadychains.Enqueue(chainTemp);
                }
                chain = null;

            }
            foreach(List<Actor> readychain in Readychains)
                readychain.Reverse();
            return Readychains;
        }

        public List<List<String>> ReconstructPathMovies(Actor start, Actor end)
        {
            List<List<String>> moviesList = new List<List<String>>(end.DegreeOfSepration);
            Queue<Actor> chain = new Queue<Actor>();
            chain.Enqueue(end);
            Actor current = null;
            while (current != start)
            {
                current = chain.Dequeue();
                if (current.DegreeOfSepration == 0)
                    continue;
                int index = end.DegreeOfSepration - current.DegreeOfSepration;
                List<String> Movies = (moviesList.Count > index) ? moviesList[index] : null;
                if (Movies == null)
                {
                    Movies = new List<string>();
                    moviesList.Add(Movies);
                }
                foreach (Actor actor in current.Previous)
                {
                    List<String> CommonMovies = current.Movies.Intersect(actor.Movies).ToList();
                    Movies.AddRange(CommonMovies);
                    chain.Enqueue(actor);
                }
                Movies = Movies.Distinct().ToList();
                moviesList[index] = Movies;
            }
                moviesList.Reverse();
            return moviesList;
        }

    }
}
