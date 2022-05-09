using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_algo1
{
    public class ConsoleSpinner
    {
        int counter;

        public void Turn()
        {
            counter++;
           Console.Write(counter.ToString());
           
            Console.SetCursorPosition(Console.CursorLeft - counter.ToString().Length, Console.CursorTop);
        }
    }
}
