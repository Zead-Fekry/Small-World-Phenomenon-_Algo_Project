using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_algo1
{
    internal class WeightedEdge
    {
        int weight;

        Actor start;
        Actor end;

        public int Weight { get { return weight; } }

        public Actor Start { get { return start; } }
        public Actor End { get { return end; } }

        public void IncrementWeight()
        {
            weight++;
        }

        public WeightedEdge(Actor  start, Actor  end, int weight)
        {
            this.start = start;
            this.end = end;
            this.weight = weight;
            start.AddEdge(this);
        }

        public override string ToString()
        {
            return string.Format("{0}--{1}-->{2}", start.Name, weight, end.Name);
        }
    }
}
