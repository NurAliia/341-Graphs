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
            Graph graph = new Graph();
            graph.Show();
            //graph.RemoveNode(2);
            //graph.RemoveNode(4);
            //graph.Show();
            //TaskDejkstra(graph, 0); - РАботает
            TaskFordBellman(graph, 0);
            //TaskFloyd(graph); //oriented  - РАботает
            //TaskFord_Bellman(graph, 0);       

        }
        static void TaskDejkstra(Graph graph, int node)
        {
            int i = 0;
            foreach (var dictionary in graph.Dejkstra(node))
            {
                Console.WriteLine("The shortest way from the node: " + node + " to the node: " + dictionary.Key +
                       " is = " + dictionary.Value.Keys[0]);
                i++;
            }
        }
        static void TaskFordBellman(Graph graph, int node)
        {
            int i = 0;
            SortedList<int, int> d = new SortedList<int, int>(graph.Size);

            if (graph.FordBellman(node, d))
            {
                foreach (KeyValuePair<int,int> distance in d)
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
        static void TaskFloyd(Graph graph)
        {
            graph.Floyd();
        }
        static void TaskFord_Bellman(Graph graph, int node)
        {
            long[] d;
            int[] p;
            if (graph.Ford_Bellman(node,out p,out d))
            {
                for (int i=0; i<graph.Size; i ++)
                {
                    Console.WriteLine("The shortest way from the node: " + node + " to the node: " + i +
                            " is = " + d[i]);                    
                }
            }
        }
    }
}


