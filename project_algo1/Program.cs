using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace project_algo1
{
    internal class Program
    {
        private static readonly string MOVIES_LIST = "Movies4736.txt";
        private static readonly string QUERIES_LIST = "queries2000.txt";

        public static string MOVIES_LIST1 => MOVIES_LIST;

        static void Main(string[] args)
        {
            List<WeightedEdge> edges = new List<WeightedEdge>();
            List<Actor> actors = new List<Actor>();
            StreamReader movieReader = new StreamReader(MOVIES_LIST1);
            Console.SetIn(movieReader);
            List<Actor> nodes = new List<Actor>();
            //sample test         queries1.txt  ,        movies1.txt
            //
            //small test 1  "queries110.txt"    ,     Movies193.txt
            //
            //small test 2  "queries50.txt"    ,     Movies187.txt
            //
            //medium test 1  "queries4000.txt"    ,     Movies967.txt   
            //medium test 1  "queries85.txt"    ,     Movies967.txt   

            //medium test 2  "queries110.txt"    ,     Movies4736.txt   
            //medium test 2  "queries2000.txt"    ,     Movies4736.txt   

            //queries22
            //Movies122806

            string reader;
            Actor start;
            Actor end;
            Console.WriteLine("Read Movies Start");
            //movies read
            while (true)
            {
                nodes.Clear();
                reader = Console.ReadLine();
                if (string.IsNullOrEmpty(reader))
                    break;

                string[] lineItems = reader.Split('/');
                for (int j = 1; j < lineItems.Length; j++)
                {
                    Actor actor = actors.FirstOrDefault(ac => ac.Name.Equals(lineItems[j]));
                    if (actor == null)
                    {
                        actor = new Actor(lineItems[j]);
                        actors.Add(actor);
                    }
                    actor.Movies.Add(lineItems[0]);
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        start = nodes[i];
                        end = actor;
                        WeightedEdge edgeR = nodes[i].Edges.FirstOrDefault(ed => ed.End.Equals(end));
                        WeightedEdge edgeL = actor.Edges.FirstOrDefault(ed => ed.End.Equals(start));
                        if (edgeR != null)
                        {
                            edgeR.IncrementWeight();
                            edgeL.IncrementWeight();
                        }
                        else
                        {
                            edges.Add(new WeightedEdge(start, end, 1));
                            edges.Add(new WeightedEdge(end, start, 1));
                        }
                    }
                    nodes.Add(actor);
                }


            }//movies end 
            Console.WriteLine("Read Movies Done");
            edges = null;
            GC.Collect();


            StreamReader queryReader = new StreamReader(QUERIES_LIST);
            Console.SetIn(queryReader);
            WeightedGraph weightedGraph = new WeightedGraph(actors);
            while (true)
            {
                counter = 0;
                reader = Console.ReadLine();
                if (string.IsNullOrEmpty(reader))
                    break;

                string[] lineItems = reader.Split('/');
                start = actors.FirstOrDefault(ac => ac.Name.Equals(lineItems[0]));
                end = actors.FirstOrDefault(ac => ac.Name.Equals(lineItems[1]));
                Console.WriteLine("Solving "+ start.Name + end.Name+"...");
                List<List<string>> solution = weightedGraph.Pathfinder(start, end, "Dijkstra");
                Console.WriteLine(start.Name +" "+ end.Name + " Solution is =");
                for (int j = 0; j < solution.Count; j++)
                {
                    List<string> actor = solution[j];
                    for (int i = 0; i < actor.Count; i++)
                    {
                        if (i == actor.Count - 1)
                            Console.Write(actor[i]);
                        else Console.Write(actor[i] + " or ");
                    }
                    if (j != solution.Count - 1)
                        Console.Write(" => ");
                }
                Console.Write("\tDOS = " + end.DegreeOfSepration);
                Console.Write("\tStrength = " + end.Strength);
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------");

            }
            //Queries Read End

        }
        static string holder = "";
        static int counter = 0;
    }
}
