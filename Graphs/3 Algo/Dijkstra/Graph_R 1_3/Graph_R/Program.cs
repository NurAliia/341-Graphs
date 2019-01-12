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
            graph.RemoveNode(2);
            graph.RemoveNode(4);
            //graph.Show();
            TaskDejkstra(graph, 3); //- РАботает    

        }
        static void TaskDejkstra(Graph_R graph, int node)
        {
            int i = 0;
            foreach (var dictionary in graph.Dejkstra(node))
            {
                Console.WriteLine("The shortest way from the node: " + node + " to the node: " + dictionary.Key +
                       " is = " + dictionary.Value.Keys[0]);
                i++;
            }
        }      
    }
}
