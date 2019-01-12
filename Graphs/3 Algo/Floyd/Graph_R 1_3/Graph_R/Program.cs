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
           // graph.RemoveNode(2);
            //graph.RemoveNode(4);
            //graph.Show();
            TaskFloyd(graph); //oriented  - РАботает  
        }       
        static void TaskFloyd(Graph_R graph)
        {
            graph.Floyd();
        }      
    }
}
