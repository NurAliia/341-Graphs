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
            //Graph graph_standart1 = new Graph(1, 5); //Сильно связанный граф
            //Console.WriteLine("---Graph 1----");
            //graph_standart1.Show();
            //Graph graph_standart2 = new Graph(0, 5); //Сильно связанный взвешанный граф
            //Console.WriteLine("---Graph 2----");
            //graph_standart2.Show();
            Graph graph_standart3 = new Graph(); //Граф созданные из файла
            Console.WriteLine("---Graph ----");
            graph_standart3.Show();


            //Console.WriteLine("---Remove Node----");
            //graph_standart3.RemoveNode(3);
            //graph_standart3.Show();
            //Console.WriteLine("---Remove Edge----");
            //graph_standart3.RemoveEdge(1, 2);
            //graph_standart3.Show();
            //Graph graph_standart4 = new Graph(graph_standart3); //Скопированный граф
            //Console.WriteLine("---Graph 4----");
            //graph_standart4.Show();
            ////Проверка копирования
            //Console.WriteLine("---Add Edge----");
            //graph_standart3.AddEdge(1, 2);
            //graph_standart3.Show();
            //Console.WriteLine("---Add Node----");
            //graph_standart3.AddNode();
            //graph_standart3.Show();
            //Console.WriteLine("Parent");
            //graph_standart4.Show();


            //Задание №16
            //int u = 2;
            //int v = 1;
            //if (Task1_16(graph_standart3, u, v))
            //{
            //    Console.WriteLine("Возможно попасть из вершины " + u + " в вершину " + v + " через одну вершину");
            //}
            //else
            //{
            //    Console.WriteLine("Невозможно попасть из вершины " + u + " в вершину " + v + " через одну вершину");
            //}

            //Задание №2
            //Graph additionGraph = Task2_1(graph_standart3);
            //Console.WriteLine("New Graph Addirion");
            //additionGraph.Show();

            //Задание №3
            //Task21(graph_standart3);
            //Задание №4
            Task4(graph_standart3);

        }

        public static bool Task1_16(Graph graphOur, int u, int v)
        {
            SortedList<int, int> list1 = new SortedList<int, int>();
            graphOur.TryGetValue(out list1, u);
            foreach (KeyValuePair<int, int> i in list1)
            {
                SortedList<int, int> list2 = new SortedList<int, int>();
                graphOur.TryGetValue(out list2, i.Key);
                if (list2.IndexOfKey(v) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static Graph Task2_1(Graph graphOur)
        {
            int count = graphOur.Size;
            Graph additionGraph = new Graph(graphOur.Type, count);

            foreach (var node in graphOur.Keys) 
            {
                foreach (KeyValuePair<int, int> item in graphOur.Values(node))
                {
                    additionGraph.Values(node).Remove(item.Key);
                }
            }
            return additionGraph;
        }
        public static void Task21(Graph graphOur)
        {
            graphOur.IsTreeOrNot();           
        }
        public static void Task4(Graph graphOur)
        {
            KruskalAlgorithm k = new KruskalAlgorithm(graphOur);

            k.BuildSpanningTree();
            Console.WriteLine("Cost: " + k.Cost);
            k.DisplayInfo();
        }
    }
}
