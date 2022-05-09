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
        //zezo
        // private static readonly string MOVIES_LIST = "movies1.txt";         //sample
        // private static readonly string QUERIES_LIST = "queries1.txt";       // sample
        // private static readonly string MOVIES_LIST = "Movies193.txt";     // small case1
        // private static readonly string QUERIES_LIST = "queries110.txt";   // small case1
        // private static readonly string MOVIES_LIST = "Movies187.txt";     // small case2
        // private static readonly string QUERIES_LIST = "queries50.txt";    // small case2
        // private static readonly string MOVIES_LIST = "Movies967.txt";     // medium case 1  
        // private static readonly string QUERIES_LIST = "queries85.txt";    // medium case1
        // private static readonly string MOVIES_LIST = "Movies967.txt";     // medium case1
        // private static readonly string QUERIES_LIST = "queries4000.txt";  // medium case1
        // private static readonly string MOVIES_LIST = "Movies4736.txt";    //medium case 2
        // private static readonly string QUERIES_LIST = "queries110.txt";   // medium case 2
        // private static readonly string MOVIES_LIST = "Movies4736.txt";    //medium case 2
        // private static readonly string QUERIES_LIST = "queries2000.txt";  // medium case 2
        // private static readonly string MOVIES_LIST = "Movies14129.txt";   //large
        //  private static readonly string QUERIES_LIST = "queries26.txt";    // large
         private static readonly string MOVIES_LIST = "Movies122806.txt";  //Extreme
         private static readonly string QUERIES_LIST = "queries200.txt";   // Extreme

        public static string MOVIES_LIST1 => MOVIES_LIST;

        static void Main(string[] args)
        {
            Dictionary<string, Actor> Optimization = new Dictionary<string, Actor>();
            List<WeightedEdge> edges = new List<WeightedEdge>();
            List<Actor> actors = new List<Actor>();
            StreamReader movieReader = new StreamReader(MOVIES_LIST1);
            Console.SetIn(movieReader);
            List<Actor> nodes = new List<Actor>();
            var spin = new ConsoleSpinner();

            string reader;
            Actor start;
            Actor end;
            Console.WriteLine("Read Movies Start");
            while (true)
            {
                spin.Turn();
                nodes.Clear();
                reader = Console.ReadLine();
                if (string.IsNullOrEmpty(reader))
                    break;

                string[] lineItems = reader.Split('/');
                for (int j = 1; j < lineItems.Length; j++)
                {
                    if (Optimization.ContainsKey(lineItems[j]) == false)
                    {
                        Actor actor2 = new Actor(lineItems[j]);
                        Optimization.Add(lineItems[j], actor2);
                        actors.Add(actor2);
                    }
                    Actor zoz = Optimization[lineItems[j]];
                    zoz.Movies.Add(lineItems[0]);

                    for (int i = 0; i < nodes.Count; i++)
                    {
                        start = nodes[i];
                        end = zoz;
                        WeightedEdge edgeR = start.Edges.FirstOrDefault(ed => ed.End.Equals(end));
                        WeightedEdge edgeL = end.Edges.FirstOrDefault(ed => ed.End.Equals(start));
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
                    nodes.Add(zoz);
                }


            }//movies end 
            Console.WriteLine("Read Movies Done");
            Console.WriteLine("do you want to start algorithm ? ");
            Console.ReadKey();
            Console.Clear();
            edges = null;
            Optimization = null;
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
                List<List<string>> solution = weightedGraph.DijkstraSearch(start, end);
                Console.WriteLine("Solving: => " + start.Name +" <=> " + end.Name + "...");
                Console.Write(" DOS = " + end.DegreeOfSepration);
                Console.Write(" , Strength = " + end.Strength);
                Console.WriteLine();
                Console.WriteLine("ChainOfMovies: ");
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
                Console.WriteLine();

                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------");

            }
            //Queries Read End

        }
        static string holder = "";
        static int counter = 0;
    }
}
