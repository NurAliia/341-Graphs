using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    class Graph_R
    {
        public Dictionary<int, SortedList<int, int>> graph = new Dictionary<int, SortedList<int, int>>();
        private int type;
        private HashSet<int> nodes = new HashSet<int>();

        public Graph_R(int type, int v)
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

        public Graph_R()
        {
            // FileStream file1 = new FileStream("Input.txt", FileMode.Open); //создаем файловый поток            
            using (StreamReader fileIn = new StreamReader("Input.txt"))
            {
                int type = int.Parse(fileIn.ReadLine());
                this.type = type;
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
        public Graph_R(Graph_R a)
        {
            for (int i = 0; i < a.Size; i++)
            {
                SortedList<int, int> list1 = new SortedList<int, int>(a.graph[i]);
                graph.Add(i, list1);
            }
        }
        public int Size //свойство для получения размерности списка
        {
            get
            {
                return graph.Count;
            }
        }
        public void AddNode()
        {
            SortedList<int, int> list = new SortedList<int, int>();
            if (type == 0) //взвешанный граф
            {
                Random rnd = new Random();
                int graphCount = graph.Count;
                for (int i = 0; i < graphCount; i++)
                {
                    int weigth = rnd.Next(0, 100);
                    graph[i].Add(graphCount, weigth);
                    list.Add(i, weigth);
                }
                graph.Add(graphCount, list);
            }
            else
            {
                int graphCount = graph.Count;
                for (int i = 0; i < graphCount; i++)
                {
                    graph[i].Add(graphCount, 1);
                    list.Add(i, 1);
                }
                graph.Add(graphCount, list);
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
        public void TryGetValue(SortedList<int, int> list1, int u)
        {
            graph.TryGetValue(u, out list1);
        }
        public SortedList<int, int> Values(int i)
        {
            return graph[i];
        }
        public int Type { get { return type; } }
        public int ValuesCount { get { return graph.Values.Count; } }
        public Dictionary<int, SortedList<int, int>>.KeyCollection Keys
        {
            get
            {
                return graph.Keys;
            }

        }
        public void Floyd()
        {
            int[][] d = new int[Size][];
            for (int i = 0; i < Size; i++)
            {
                d[i] = new int[Size];
                for (int j = 0; j < Size; j++)
                {
                    if (i == j)
                    {
                        d[i][j] = 0;
                    }
                    else
                    {
                        if (graph[i] != null)
                        {
                            if (graph[i].ContainsKey(j))
                            {
                                int value;
                                graph[i].TryGetValue(j, out value);
                                d[i][j] = value;
                            }
                            else
                            {
                                d[i][j] = int.MaxValue;
                            }
                        }
                        else
                        {
                            d[i][j] = int.MaxValue;
                        }
                    }
                }
            }
            ShowMassive(d);
            Console.WriteLine();
            for (int pos = 0; pos < 2; pos++)
            {
                for (int j = 0; j < Size; j++)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        for (int k = 0; k < Size; k++)
                        {
                            if (d[i][k] < int.MaxValue && d[k][j] < int.MaxValue)
                            {
                                d[i][j] = Math.Min(d[i][j], d[i][k] + d[k][j]);
                            }
                        }
                    }
                }
            }
            ShowMassive(d);
            Console.WriteLine();
            for (int i = 0; i < Size; ++i)
                for (int j = 0; j < Size; ++j)
                    for (int t = 0; t < Size; ++t)
                        if (d[i][t] < int.MaxValue && d[t][j] < int.MaxValue && (d[t][t]) < 0)
                        {
                            d[i][j] = -int.MaxValue;
                        }
            for (int i = 0; i < Size; ++i)
                for (int j = 0; j < Size; ++j)
                {
                    if (d[i][j] == -int.MaxValue)
                    {
                        Console.WriteLine("(" + i + "," + j + ")");
                    }
                }
        }
        public void ShowMassive(int[][] d)
        {
            for (int k = 0; k < Size; k++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Console.Write(d[k][j] + " ");
                }
                Console.WriteLine();
            }
        }
        public void Show()
        {
            foreach (KeyValuePair<int, SortedList<int, int>> keyValue in graph)
            {
                Console.Write(keyValue.Key + " - ");
                if (keyValue.Value != null)
                {
                    foreach (KeyValuePair<int, int> element in keyValue.Value)
                    {
                        Console.Write(element);
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(" ");
                }
            }
        }

    }
}

