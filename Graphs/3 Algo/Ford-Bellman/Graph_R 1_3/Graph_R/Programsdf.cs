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
            //graph_standart1.Show();
            //Graph graph_standart2 = new Graph(0, 5); //Сильно связанный взвешанный граф
            //graph_standart2.Show();
            Graph graph_standart3 = new Graph(); //Граф созданные из файла
            graph_standart3.Show();

            //Console.WriteLine("---Remove Node----");
            //graph_standart3.RemoveNode(2);
            ////graph_standart3.Show();
            //Console.WriteLine("---Remove Edge----");
            //graph_standart3.RemoveEdge(1 ,2);
            //graph_standart3.Show();
            //Graph graph_standart4 = new Graph(graph_standart3); //Скопированный граф
            //graph_standart4.Show();
            ////Проверка копирования
            //Console.WriteLine("---Add Edge----");
            //graph_standart3.AddEdge(1, 2);
            //graph_standart3.Show();
            //Console.WriteLine("Parent");
            //graph_standart4.Show();


            //Задание №1
            //int u = 0;
            //int v = 4;
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
            //graph_standart3.Show();
            //Console.WriteLine("New Graph Addirion");
            //additionGraph.Show();

            //Задание №3
            Task21(graph_standart3);


        }
    
        public static bool Task1_16(Graph graphOur, int u, int v)
        {
            SortedList<int, int> list1 = new SortedList<int, int>();
            graphOur.graph.TryGetValue(u, out list1);
            foreach (KeyValuePair<int, int> i in list1)
            {
                SortedList<int, int> list2 = new SortedList<int, int>();
                graphOur.graph.TryGetValue(i.Key, out list2);
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
            Graph additionGraph = new Graph(graphOur.type, count);

            for (int i = 0; i < count; i++)
            {
                foreach (KeyValuePair<int, int> item in graphOur.graph[i])
                {
                    additionGraph.graph[i].Remove(item.Key);
                }
            }
            return additionGraph;
        }
        public static void Task21(Graph graphOur)
        {
            Graph graph2 = new Graph(graphOur);
            int counter = graphOur.Eulerian(0);
            Console.WriteLine(counter);
            SortedList<int, int> list = new SortedList<int, int>();
            int size = graphOur.Size, n=0;
            //for (int i = 0; i < size; i++)
            //{
            //    c = graphOur.graph[i].Keys.Count;
            //    //c = graphOur.graph[i].Keys.Count.Where(x => x > i);
            //    // sum +=
            //    list.Add(i, c);
            //}
            for (int i = 0; i < size; i++)
            {
                n = graphOur.graph[i].Keys.Count;
                Console.WriteLine(n);
                Console.WriteLine(size);
                Console.WriteLine((size - 1) - (counter - n));
                if (((size - 1) - (counter - n)) == 1)
                {
                    Console.WriteLine("При удалении вершины " + i + " граф становится деревом");
                }
            }
        }
    }
}
