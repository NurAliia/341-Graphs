using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    class helper : IEnumerator
    {
        //public Graph graph;

        int position = -1;
        private Dictionary<int, SortedList<int, bool>> graph1;


        public helper(Dictionary<int, SortedList<int, bool>> graph1)
        {
            this.graph1 = graph1;
        }

        public bool MoveNext()
        {
            position++;
            return (position < graph1.Count);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public KeyValuePair<int, SortedList<int, bool>> Current
        {
            get
            {
                try
                {
                    return graph1.ElementAt(position);
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
