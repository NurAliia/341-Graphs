using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;

namespace Graph_R
{
    class Graph
    {
        private Dictionary<int, List<Arc>> graph = new Dictionary<int, List<Arc>>();              

        public Graph()
        {
            using (StreamReader fileIn = new StreamReader("input.txt"))
            {
                string line = fileIn.ReadToEnd();
                string[] lines = line.Split('\n');
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] != "-1")
                    {
                        List<Arc> listArc = new List<Arc>();
                        string[] line2 = lines[i].Split(' ');
                        for (int j = 0; j < line2.Length - 1; j += 2)
                        {
                            listArc.Add(new Arc(int.Parse(line2[j]), int.Parse(line2[j+1])));
                        }
                        graph.Add(i, listArc);
                    }
                    else
                    {
                        List<Arc> listArc = new List<Arc>();
                        graph.Add(i, listArc);
                    }
                }
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
            List<Arc> list = new List<Arc>();
            Random rnd = new Random();
            int graphCount = graph.Count;
            for (int i = 0; i < graphCount; i++)
            {
                int capacity = rnd.Next(0, 100);
                list.Add(new Arc(i,capacity));
            }
            graph.Add(graphCount, list);            
        }
        public void AddEdge(int u, int v)
        {
            SortedList<int, int> list = new SortedList<int, int>();
            Random rnd = new Random();
            graph[u].Add(new Arc(v, rnd.Next(0, 100)));
            graph[v].Add(new Arc(u, rnd.Next(0, 100)));
        }
        public void RemoveNode(int element)
        {
            graph.Remove(element);
        }
        public void RemoveEdge(int u, Arc arc)
        {            
            graph[u].Remove(arc);
        }
        
        public int ValuesCount
        {
            get
            {
                int countValues = 0;
                for (int i = 0; i < Size; i++)
                {
                    countValues += graph[i].Count;
                }
                return countValues;
            }
        }
        public Dictionary<int, List<Arc>>.KeyCollection Keys
        {
            get
            {
                return graph.Keys;
            }

        }
        public void Show()
        {
            foreach (KeyValuePair<int, List<Arc>> keyValue in graph)
            {
                Console.Write(keyValue.Key + " - ");
                if (keyValue.Value != null)
                {
                    foreach (Arc element in keyValue.Value)
                    {
                        element.Show();
                    }
                }
                Console.WriteLine();
            }
        }
        //public List<Arc> Values(int i)
        //{
        //    return graph[i];
        //}
        public  bool ContainsArc(int from, int to)
        {
            if (graph[from] != null)
            {
                foreach (var arc in graph[from])
                    if (arc.To == to)
                        return true;
            }
            return false;
        }
        public void ChangeFlow(int from, int to, int delta)
        {
            foreach (var arc in graph[from])
                if (arc.To == to)
                    arc.Flow += delta;
        }
        public void MaxFlow(int source, int sink)
        {
            int[] excess = new int[Size];    // Избытки в вершинах
            int[] height = new int[Size];    // Высоты вершин
                                             // for (int i =0; i < Size; i++ )
            foreach (var nodes in graph)
            {
                foreach (Arc element in graph[nodes.Key])
                {
                    if (ContainsArc(element.To, nodes.Key) == false)
                    {
                        ExtArc arc = new ExtArc(nodes.Key);
                        graph[element.To].Add(arc);
                    }
                    if (nodes.Key == source)
                    {
                        // Пускаем максимальный поток по выходящим из истока дугам.
                        element.Flow = element.Capacity;
                        ChangeFlow(element.To, nodes.Key, -element.Capacity);
                        excess[element.To] = element.Capacity;
                    }
                    else if (element.To != source)
                    {
                        // Все остальные дуги имеют нулевой начальный поток.
                        element.Flow = 0;
                    }
                }
            }
            // Высоты всех вершин кроме истока равны нулю.
            height[source] = graph.Count;
            // 2. Цикл проверки вершин
            int h = 0;
            bool excessFound;	// Есть вершина с положительным избытком?
            do
            {
                excessFound = false;
                foreach (var nodes in graph)
                {
                    if (nodes.Key == source || nodes.Key == sink)
                    {
                        continue;
                    }
                    if (excess[nodes.Key] > 0)
                    {
                        // Нашли вершину с положительным избытком;
                        // Исследуем ее соседей.
                        excessFound = true;
                        int minHeight = 2 * graph.Count;    // Минимальная из высот соседей
                        int maxFlowArc = 0;            // Максимум потока по дугам
                        Arc workArc = null;			// Дуга, по которой можно протолкнуть дополнительный поток.
                        foreach (Arc arc in nodes.Value)
                        {
                            int to = arc.To;
                            if (arc.Capacity - arc.Flow > 0)
                            {
                                if (height[to] == height[nodes.Key] - 1)
                                {
                                    // Можно выполнить проталкивание
                                    if (arc.Capacity - arc.Flow > maxFlowArc)
                                    {
                                        maxFlowArc = arc.Capacity - arc.Flow;
                                        workArc = arc;
                                    }
                                }
                                // Оцениваем высоту вершины для подъема.
                                if (height[to] < minHeight)
                                {
                                    minHeight = height[to];
                                }
                            }
                        }
                        if (workArc != null)
                        {   
                           
                            // Выполняем проталкивание предпотока по дуге
                            int flow = Math.Min(excess[nodes.Key], maxFlowArc);
                            excess[nodes.Key] -= flow;
                            excess[workArc.To] += flow;  
                            workArc.Flow += flow;
                            ChangeFlow(workArc.To, nodes.Key, -flow);
                            Console.WriteLine("Push " + nodes.Key + "-> " + workArc.To + "(" + flow + ")");
                        }
                        else
                        {
                            // Выполняем подъём
                            height[nodes.Key] = minHeight + 1;
                            Console.WriteLine("Lift " + nodes.Key + " to " + height[nodes.Key]);
                        }
                    }
                }
            }
            while (excessFound);
           // Show();
            // 3. Убираем фиктивные дуги
            foreach (var nodes in graph)
            {
                for (int i = 0; i < nodes.Value.Count; i++) //Так как размер коллекции изменяется, нельзя перечисление делать
                {
                    if (nodes.Value[i].GetType() == typeof(ExtArc))
                    {
                        nodes.Value.Remove(nodes.Value[i]);
                        i--;
                    }
                }
            }
            //Show();
            // 4. Вычисление величины потока
            int maxFlow = 0;
            Console.WriteLine("Arc Flow: ");
            foreach (var arc in graph[source])
            {
                Console.Write("В вершину " + arc.To + " ");
                Console.WriteLine("Поток " + arc.Flow);
                maxFlow += arc.Flow;
            }
            Console.WriteLine("Максимальный поток = " + maxFlow);
        }   
    }   
}
