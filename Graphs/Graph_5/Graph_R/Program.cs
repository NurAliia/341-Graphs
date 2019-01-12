using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;

namespace Graph_R
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph_standart3 = new Graph(); //Граф созданные из файла
            graph_standart3.Show();
            int source = 0; //Исток
            int sink = 3;  // Сток
            graph_standart3.MaxFlow(source, sink);
        }        
    }
}
