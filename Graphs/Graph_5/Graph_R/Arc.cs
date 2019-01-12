using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_R
{
    public class ExtArc : Arc
    {
        public ExtArc(int to) : base(to,0)
        {           
        }
    }
    public class Arc
    {
        private int to;
        private int capacity;
        private int flow;
        public Arc() { }
        public Arc(int to)
        {
            this.to = to;
        }
        public Arc(int to, int capacity)
        {
            this.to = to;
            this.capacity = capacity;
        }
        public Arc(int to, int capacity, int flow)
        {
            this.to = to;
            this.capacity = capacity;
            this.flow = flow;
        }
        public Arc(Arc a)
        {
            to = a.To;
            capacity = a.Capacity;
            flow = a.Flow;
        }
        public void changeFlow(int delta)
        {
            flow += delta;
        }
        public int To
        {
            get
            {
                return to;
            }
            set
            {
                to = value;
            }
        }
        public int Flow
        {
            get
            {
                return flow;
            }
            set
            {
                flow = value;
            }
        }
        public int Capacity
        {
            get
            {
                return capacity;
            }
        }
        public void Show()
        {
            Console.Write("("+ to + ", " + capacity + ", " + flow + ")");
        }
    }
}
