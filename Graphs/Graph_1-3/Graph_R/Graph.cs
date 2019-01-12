using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;

namespace Graph_R
{
    class Graph : IEnumerable
    {
        private Dictionary<int, SortedList<int, int>> graph = new Dictionary<int, SortedList<int, int>>();
        private int type, type2;
        private Stack myStack = new Stack();
        private bool[] visit;
        List<int> result = new List<int>();

        public Graph(int type, int v)
        {
            this.type = type;
            if (type == 1) //Невзвешанный граф
            {
                for (int i = 0; i < v; i++)
                {
                    SortedList<int, int> list = new SortedList<int, int>();
                    for (int j = 0; j < v; j++)
                    {
                        if (i != j)
                        {
                            list.Add(j, 1);
                        }
                    }
                    graph.Add(i, list);
                }
            }
            else
            {
                Random rnd = new Random();
                for (int i = 0; i < v; i++)
                {
                    SortedList<int, int> list = new SortedList<int, int>();
                    for (int j = 0; j < v; j++)
                    {
                        if (i != j)
                        {
                            list.Add(j, rnd.Next(0, 100));
                        }
                    }
                    graph.Add(i, list);
                }
            }
        }

        public Graph()
        {
            using (StreamReader fileIn = new StreamReader("Input.txt"))
            {
                int type = int.Parse(fileIn.ReadLine());
                this.type = type;
                int type2 = int.Parse(fileIn.ReadLine());
                this.type2 = type2;
                string line = fileIn.ReadToEnd();
                string[] lines = line.Split('\n');
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] != "-1")
                    {
                        SortedList<int, int> list = new SortedList<int, int>();
                        string[] line2 = lines[i].Split(' ');
                        for (int j = 0; j < line2.Length - 1; j += 2)
                        {
                            list.Add(Convert.ToInt32(line2[j]), Convert.ToInt32(line2[j + 1]));
                        }
                        graph.Add(i, list);
                    }
                    else
                    {
                        graph.Add(i, null);
                    }
                }
            }
        }
        public Graph(Graph a)
        {
            foreach ( var node in a.Keys)
            {
                SortedList<int, int> list1 = new SortedList<int, int>(a.graph[node]);
                graph.Add(node, list1);
            }
        }
        public int Size //свойство для получения размерности списка
        {
            get
            {
                return graph.Count();
            }
        }
        public void AddNode()
        {
            SortedList<int, int> list = new SortedList<int, int>();
            if (type == 0) //взвешанный граф
            {
                Random rnd = new Random();
                int lastNode = graph.Keys.Last() + 1;
                foreach (var node in graph.Keys)
                {
                    int weigth = rnd.Next(0, 100);
                    graph[node].Add(lastNode, weigth);
                    list.Add(node, weigth);
                }
                graph.Add(lastNode, list);
            }
            else
            {
                int lastNode = graph.Keys.Last() + 1;
                foreach (var node in graph.Keys)
                {
                    graph[node].Add(lastNode, 1);
                    list.Add(node, 1);
                }
                graph.Add(lastNode, list);
            }
        }
        public void AddEdge(int u, int v)
        {
            SortedList<int, int> list = new SortedList<int, int>();
            if (type == 0) //взвешанный граф
            {
                Random rnd = new Random();
                graph[u].Add(v, rnd.Next(0, 100));
                graph[v].Add(u, rnd.Next(0, 100));
            }
            else
            {
                graph[u].Add(v, 1);
                graph[v].Add(u, 1);
            }
        }
        public void RemoveNode(int element)
        {
            foreach (KeyValuePair<int, int> item in graph[element])
            {
                graph[item.Key].Remove(element);
            }
            graph.Remove(element);
        }
        public void RemoveEdge(int u, int v)
        {
            graph[u].Remove(v);
            graph[v].Remove(u);
        }
        public void RemoveEdgeOriented(int u, int v)
        {
            graph[u].Remove(v);
        }

        public void TryGetValue(out SortedList<int, int> list1, int u)
        {
            graph.TryGetValue(u, out list1);
        }
        public SortedList<int, int> Values(int i)
        {
            return graph[i];
        }
        public int Type { get { return type; } }
        public int ValuesCount
        {
            get
            {
                int countValues = 0;
                foreach (var node in graph.Keys)
                {
                    countValues += graph[node].Values.Count;
                }
                return countValues;
            }
        }
        //public int ValuesCount
        //{
        //    get
        //    {
        //        int countValues = 0;
        //        foreach (var node in graph.Keys)
        //        {
        //            countValues += graph[node].Values.Count;
        //        }
        //        if (type2 == 0)
        //            return countValues /= 2;
        //        else
        //            return countValues;
        //    }
        //}
        public Dictionary<int, SortedList<int, int>>.KeyCollection Keys
        {
            get
            {
                return graph.Keys;
            }

        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public GraphEnum GetEnumerator()
        {
            return new GraphEnum(graph);
        }
        public void Show()
        {
            foreach (KeyValuePair<int, SortedList<int, int>> keyValue in graph)
            {
                Console.Write(keyValue.Key + " - ");
                foreach (KeyValuePair<int, int> element in keyValue.Value)
                {
                    Console.Write(element);
                }
                Console.WriteLine();
            }
        }

        public void Dfs(int v)
        {
            visit[v] = true;
            myStack.Push(v);
            int u, w;
            while (myStack.Count != 0)
            {
                u = (int)myStack.Peek();
                w = AdjD(u);
                if (w >= 0)
                {
                    visit[w] = true;
                    myStack.Push(w);
                }
                else
                {
                    myStack.Pop();
                }
            }
        }
        public int AdjD(int node)
        {
            foreach (KeyValuePair<int, int> item in graph[node])
            {
                if (visit[item.Key] == false)
                    return item.Key;
            }
            return -1;
        }
        public int Eulerian(int node)
        {
            int counter = 0;
            myStack.Push(node);
            while (myStack.Count != 0)
            {
                node = (int)myStack.Peek();
                int w = AdjE(node);
                if (w > 0)
                {
                    myStack.Push(w);
                    RemoveEdge(node, w);
                    counter++;
                }
                else
                {
                    result.Add(node);
                    myStack.Pop();
                }
            }
            return counter;
        }
        public int AdjE(int node)
        {
            foreach (KeyValuePair<int, int> item in graph[node])
            {
                if (item.Key != -1)
                    return item.Key;
            }
            return -1;
        }
        public void IsTreeOrNot()
        {
            Graph graph2 = new Graph();
            int counterEdge = ValuesCount; 
            Console.WriteLine("Количество ребер в исходном графе = " + counterEdge);
            SortedList<int, int> list = new SortedList<int, int>();
            int size = Size;
            int n;
            Graph graph3;
            foreach (var node in graph.Keys)
            {
                n = Values(node).Values.Count;
                graph3 = new Graph();
                graph3.RemoveNode(node);
                if ((((Size - 1) - (counterEdge - n)) == 1) && (graph3.IsTree(node)))
                {
                    Console.WriteLine("При удалении вершины " + node + " граф становится деревом");
                }
            }
            //graph3 = new Graph();
            //graph3.RemoveNode(Keys.Last());
            //n = Values(0).Values.Count;
            //if ((((size - 1) - (counterEdge - n)) == 1) && (graph3.IsTree(0)))
            //{
            //    Console.WriteLine("При удалении вершины " + Keys.Last() + " граф становится деревом");
            //}
        }
        public bool IsTree(int node)
        {;
            if (node > graph.Keys.Last())
            {
                node = 0;
            }
            else
            {
                node++;
            }
            Dfs(node);
            int n = 0;
            for (int i = 0; i < Size; i++)
            {
                if (visit[i])
                    n++;
            }
            if (n == graph.Count - 1)
            {
                Console.WriteLine("Количество вершин = " + n + " ребер = " + graph.Count);
                return true;
            }
            else
                return false;
        }
    }
}
