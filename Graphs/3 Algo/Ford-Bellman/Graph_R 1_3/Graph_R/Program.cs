using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph_R graph = new Graph_R();
            graph.Show();
            //graph.RemoveNode(2);
            //graph.RemoveNode(4);
            //graph.Show();
            TaskFordBellman(graph, 0);   
        }
        static void TaskFordBellman(Graph_R graph, int node)
        {
            int i = 0;
            SortedList<int, int> d = new SortedList<int, int>(graph.Size);

            if (graph.FordBellman(node, d))
            {
                foreach (KeyValuePair<int, int> distance in d)
                {
                    Console.WriteLine("The shortest way from the node: " + node + " to the node: " + distance.Key +
                            " is = " + distance.Value);
                    i++;
                }
            }
            else
            {
                Console.WriteLine("There is a cycle of negative weight");
            }
        }    
    }
}
