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
        //private HashSet<int> nodes = new HashSet<int>();
        //private PriorityQueue<int> queue = new PriorityQueue<int>();
        //private int[] p;

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
    
      
        public bool FordBellman(int node, SortedList<int, int> d)
        {
            SortedList<int, bool> nov = new SortedList<int, bool>();
            Initialize(node, d, nov);
            for (int i = 0; i < Size - 1; i++)
            {
                foreach (var str in graph)
                {
                    foreach (KeyValuePair<int, int> pair in str.Value)
                    {
                        if (nov[pair.Key] && (d[pair.Key] > d[str.Key] + pair.Value))
                        {
                            d[pair.Key] = d[str.Key] + pair.Value;
                        }
                    }
                }
            }
            foreach (KeyValuePair<int, SortedList<int, int>> p in graph)
            {
                if (p.Value != null)
                {
                    foreach (KeyValuePair<int, int> pair in graph[p.Key])
                    {
                        if ((pair.Key != node) && (d[pair.Key] > d[p.Key] + pair.Value))
                        {
                            Console.WriteLine(pair.Key + " " + p.Key + ' ' + pair.Value);
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public void Initialize(int node, SortedList<int, int> d, SortedList<int, bool> nov)
        {
            foreach (var str in graph)
            {
                if (str.Key != node)
                {
                    d.Add(str.Key, int.MaxValue - 10);
                    nov[str.Key] = true;
                }
                else
                {
                    d.Add(str.Key, 0);
                    nov[str.Key] = false;
                }
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

