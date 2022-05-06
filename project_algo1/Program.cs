using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace project_algo1
{
    class nono
    {
        public string actor1;
        public string actor2;
        public nono previous;
        public int stre;
        public int dos;

        public bool Equals(nono another)
        {
            if (another.actor1 == actor1 && another.actor2 == actor2)
                return true;
            else if (another.actor2 == actor1 && another.actor1 == actor2)
                return true;
            else return false;
        }
    }

    internal class Program
    {
  
        static Dictionary<string, List<string>> Actor_Movies = new Dictionary<string, List<string>>();
        static Dictionary<string, List<string>> Movie_actors = new Dictionary<string, List<string>>();
        static Dictionary<string, List<string>> sehas_actors = new Dictionary<string, List<string>>();
        static List<string> list_of_actors_in_queue = new List<string>();
        static Queue<string> optimization2 = new Queue<string>();
        static Queue<nono> actors_neighbors = new Queue<nono>();
        static Queue<string> movies_neighbors = new Queue<string>();

        private static readonly string MOVIES_LIST = "Movies967.txt";
        private static readonly string QUERIES_LIST = "queries85.txt";

        public static string MOVIES_LIST1 => MOVIES_LIST;

        static void Main(string[] args)
        {
            StreamReader movieReader = new StreamReader(MOVIES_LIST1);
            Console.SetIn(movieReader);

            //sample test         queries1.txt  ,        movies1.txt
            //
            //small test 1  "queries110.txt"    ,     Movies193.txt
            //
            //small test 2  "queries50.txt"    ,     Movies187.txt
            //
            //medium test 1  "queries4000.txt"    ,     Movies976.txt   
            //medium test 1  "queries85.txt"    ,     Movies976.txt   

            //medium test 2  "queries110.txt"    ,     Movies4736.txt   
            //medium test 2  "queries2000.txt"    ,     Movies4736.txt   

            //queries22
            //Movies122806

            string reader;
            //movies read
            while (true)
            {

                reader = Console.ReadLine();
                if (string.IsNullOrEmpty(reader))
                    break;

                string[] lineItems = reader.Split('/');

                string[] lineItems2 = reader.Split('/');
                for (int j = 1; j < lineItems2.Length; j++)
                {
                    if (sehas_actors.ContainsKey(lineItems2[j]) == true)
                    {

                        sehas_actors.TryGetValue(lineItems2[j], out List<string> er);
                        for (int k = 1; k < lineItems2.Length; k++)
                        {
                            if (er.Contains(lineItems2[k]) || lineItems2[k] == lineItems2[j])
                                continue;
                            else
                                er.Add(lineItems2[k]);
                        }

                    }
                    else
                    {
                        optimization2.Enqueue(lineItems2[j]);
                        List<string> x = new List<string>();
                        for (int i = 1; i < lineItems2.Length; i++)
                        {
                            if (lineItems2[j] != lineItems2[i])
                                x.Add(lineItems2[i]);
                        }
                        sehas_actors.Add(lineItems2[j], x);
                    }

                }



                Movie_actors.Add(lineItems[0], lineItems.Skip(1).ToList());


                for (int j = 1; j < lineItems.Length; j++)
                {

                    if (Actor_Movies.ContainsKey(lineItems[j]))
                    {

                        List<string> values = Actor_Movies[lineItems[j]];
                        values.Add(lineItems[0]);
                    }
                    else
                        Actor_Movies.Add(lineItems[j], new List<string>() { lineItems[0] });
                }

            }//movies end 


            //sort the actors according to strength
            for (int i = 0; i < sehas_actors.Count; i++)
            {
                Dictionary<string, int> Actor_tarteb = new Dictionary<string, int>();
                string which_act = optimization2.Dequeue();
                sehas_actors.TryGetValue(which_act, out List<string> er);
                foreach (string actor in er)
                {
                    var y = Actor_Movies[which_act].Intersect(Actor_Movies[actor]);
                    Actor_tarteb.Add(actor, y.Count());
                }
                var mySortedList = Actor_tarteb.OrderBy(d => d.Value).ToList();
                mySortedList.Reverse();
                er.Clear();
                foreach (var actor in mySortedList)
                {

                    er.Add(actor.Key);
                }
            }
            StreamReader queryReader = new StreamReader(QUERIES_LIST);
            Console.SetIn(queryReader);
            sehas_actors=sehas_actors.OrderByDescending(x => x.Value.Count()).ToDictionary(x=>x.Key,x=>x.Value);


  /*          //Queries Read start
            StreamReader queryReader = new StreamReader(QUERIES_LIST);
            Console.SetIn(queryReader);

            while (true)
            {
                actors_neighbors.Clear();
                movies_neighbors.Clear();
                reader = Console.ReadLine();
                if (string.IsNullOrEmpty(reader))
                    break;
                string[] lineItems = reader.Split('/');
                nono nona = new nono();
                nona.actor1 = lineItems[0];
                nona.actor2 = lineItems[1];
                holder = lineItems[1];
                nona.dos = 1;
                actors_neighbors.Enqueue(nona);
                while (actors_neighbors.Count > 0)
                {
                    var result = Degree_of_sep(actors_neighbors.Dequeue());
                    if (result != null)
                    {
                        Console.WriteLine(lineItems[0] + " " + lineItems[1] + " " + result.dos + " ");
                        break;
                    }
                }
            }*/
            while (true)
            {
                actors_neighbors.Clear();
                movies_neighbors.Clear();
                list_of_actors_in_queue.Clear();
              //  optimization2.Clear();  
                reader = Console.ReadLine();
                if (string.IsNullOrEmpty(reader))
                    break;

                string[] lineItems = reader.Split('/');
                nono nona = new nono();
                int no_of_actors_of_first = sehas_actors[lineItems[0]].ToList().Count;
                int no_of_actors_of_second = sehas_actors[lineItems[1]].ToList().Count;
                //if (no_of_actors_of_first < no_of_actors_of_second)
              //  {
                nona.actor1 = lineItems[0];   //a/b
                nona.actor2 = lineItems[1];
                holder = lineItems[1];
                nona.dos = 1;
              //  }
             /*   else
                {
                    nona.actor1 = lineItems[1];   //a/b
                    nona.actor2 = lineItems[0];
                    holder = lineItems[0];
                    nona.dos = 1;
                }*/

                actors_neighbors.Enqueue(nona);
                list_of_actors_in_queue.Add(nona.actor1);
                while (actors_neighbors.Count > 0)
                {
                 //   list_of_actors_in_queue.
                    var result = seha(actors_neighbors.Dequeue());
                    if (result != null)
                    {
                        Console.WriteLine(lineItems[0] + " / " + lineItems[1] );
                        List<string >printer=new List<string>();
                        nono looper = new nono();
                        looper = result;
                        printer.Add(holder);
           
                        printer.Add(result.actor1);
         

                        if (result.dos == 1) {
                            int dee = Actor_Movies[lineItems[0]].Intersect(Actor_Movies[lineItems[1]]).ToList().Count();
                            Console.WriteLine("Dos = "+result.dos+",  Rs = " + dee);
                            Console.WriteLine("");
                            break;

                        }
                        for (int rr=0; rr<result.dos-1; rr++)
                        {
                            looper = looper.previous;  
                            printer.Add(looper.actor1);
                          //  printer.Add("=>");
                            //  Console.WriteLine();
                        }
                        
                        List<string>movies = new List<string>();    
                        int rs = 0;
                        for (int i = 0; i < printer.Count - 1; i++)
                        {
                            
                            rs += (Actor_Movies[printer[i]].Intersect(Actor_Movies[printer[i + 1]]).ToList().Count());
                        }
                        Console.WriteLine("Dos = " + result.dos + ",  Rs = " + rs);

                        string le = "";
                      for(int de=0; de<printer.Count; de++)
                        {
                           le += printer[de].ToString();
                            if (de == printer.Count-1)
                                break;
                            le += "=> ";
                           
                        }
                    
                      Console.WriteLine("CHAIN OF ACTORS: "+le);
                        Console.WriteLine("");

              
                        break;
                    }
                   
                }
            }
                //Queries Read End

            }
        static string holder = "";
     
        static nono seha(nono nonos)
        {
     

            if (sehas_actors[nonos.actor1].Contains(holder))
            {
                return nonos;
            }
            else
            {
               
                sehas_actors.TryGetValue(nonos.actor1, out List<string> er);

              var presentInBoth = list_of_actors_in_queue.Intersect(er);//tcheck en neigbhours el actor de mesh 3ande fel queue 3ashan may7slsh redudancy
                if (presentInBoth.ToList().Count == er.Count)
                    return null;


                Dictionary<string, List<string>> zead_neighbors = new Dictionary<string, List<string>>();
                for (int i = 0; i < er.Count; i++)
                {
                    zead_neighbors.Add(er[i], sehas_actors[er[i]]);
                }
                zead_neighbors = zead_neighbors.OrderByDescending(x => x.Value.Count()).ToDictionary(x => x.Key, x => x.Value);



                foreach (string actor in zead_neighbors.Keys)
                {
//sehas_actors = sehas_actors.OrderByDescending(x => x.Value.Count()).ToDictionary(x => x.Key, x => x.Value);


                    sehas_actors.TryGetValue(actor, out List<string> dr);
         /*           var presentInBoth2 = sehas_actors[nonos.actor1].ToList().Intersect(dr);//btceck en neibours el actor dol mesh 3nde abl kda fa m3odsh amshe loop 3al fady
                    if (presentInBoth2.ToList().Count == dr.Count-1)
                        continue;*/
                    if (list_of_actors_in_queue.Contains(actor))//btcheck en el actor mesh 3ande fel queue abl kda
                        continue;
                    
                    list_of_actors_in_queue.Add(actor);
                    nono momo = new nono();
                    momo.actor1 = actor;
                    momo.actor2 = holder;
                    momo.dos = nonos.dos + 1;
                    momo.previous = nonos;
               
                        actors_neighbors.Enqueue(momo);
                }
                return null;
            }

        }
        static nono Degree_of_sep(nono nonos)
        {

            string actor1_query = nonos.actor1;
            string actor2_query = holder;
            nonos.actor2 = holder;

            List<string> CommonList = Actor_Movies[actor1_query].Intersect(Actor_Movies[actor2_query]).ToList();
            nonos.stre = CommonList.Count;
            if (nonos.stre > 0)
                return nonos;
            else
            {
                HashSet<string> Actor1MoviesTeam = new HashSet<string>();
                Actor_Movies[actor1_query].ForEach(movie => Actor1MoviesTeam = Actor1MoviesTeam.Concat(Movie_actors[movie]).ToHashSet());
                Actor1MoviesTeam.Remove(actor1_query);
                /* HashSet<string> Actor2MoviesTeam = new HashSet<string>();
                 Actor_Movies[actor2_query].ForEach(movie => Actor2MoviesTeam = Actor2MoviesTeam.Concat(Movie_actors[movie]).ToHashSet());
                 Actor2MoviesTeam.Remove(actor2_query);*/
                foreach (string actor in Actor1MoviesTeam)
                {

                    nono momo = new nono();
                    momo.actor1 = actor;
                    momo.actor2 = holder;
                    momo.dos = nonos.dos + 1;
                    momo.previous = nonos;
                    if (actors_neighbors.Where(item => momo.Equals(item)).ToList().Count > 0)
                        continue;
                    else
                        actors_neighbors.Enqueue(momo);

                }
                return null;

            }

        }
    }
}
