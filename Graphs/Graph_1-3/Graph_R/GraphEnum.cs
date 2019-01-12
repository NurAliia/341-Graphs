using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_R
{
    class GraphEnum : IEnumerator
    {

        int position = -1;
        private Dictionary<int, SortedList<int, int>> graph1;


        public GraphEnum(Dictionary<int, SortedList<int, int>> graph1)
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

        public KeyValuePair<int, SortedList<int, int>> Current
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
