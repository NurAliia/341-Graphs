using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_R
{ 
  /// 
  /// Class represents graph edge.
  /// 
    public class Edge
    {
        public int U;
        public int V;
        public double Weight;
        public Edge()
        {

        }
        public Edge(int U, int V, double Weight)
        {
            this.U = U;
            this.V = V;
            this.Weight = Weight;
        }
    }

    /// 
    /// Implementation of Kruskal algorithm.
    /// 
    class KruskalAlgorithm
    {
        private int _edgesCount;
        private int _nodeCount;
        private List<Edge> _edges;
        private SortedList<int, int> tree;
        private int[] sets;

        public List<Edge> Edges { get { return _edges; } }
        public int NodeCount { get { return _nodeCount; } }
        public double Cost { get; private set; }

        public KruskalAlgorithm(Graph graph)
        {
            _nodeCount = graph.Size;
            tree = new SortedList<int, int>();
            sets = new int [_nodeCount];
            
            _edgesCount = graph.ValuesCount;            
            _edges = new List<Edge>(_edgesCount);

             
            foreach (var dictionary in graph)
            {
                foreach (var elem in dictionary.Value)
                    _edges.Add(new Edge
                    {
                        U = dictionary.Key,
                        V = elem.Key,
                        Weight = elem.Value
                    });
            }
           
            
            //foreach (Edge i in _edges)
            //{                
            //        Console.Write(i.U + "--");
            //        Console.Write(i.V + "--");
            //        Console.WriteLine(i.Weight);                
            //}
            for (int i = 0; i < _nodeCount; i++) sets[i] = i;
        }

        private void ArrangeEdges(int k) //сортировка пузырьком
        {
            Edge temp;
            for (int i = 0; i < k; i++)
            {
                for (int j = k - 1; j > i ; j--)
                {
                    if (_edges[j - 1].Weight >= _edges[j].Weight)
                    {
                        temp = _edges[j - 1];
                        _edges[j - 1] = _edges[j];
                        _edges[j] = temp;
                    }
                }
            }
            for ( int i=0; i<k;i++)
            {
                Console.WriteLine(_edges[i].U + " " + _edges[i].V + " " + _edges[i].Weight);
            }
        }

        private int Find(int vertex)
        {
            return (sets[vertex]);
        }

        private void Join(int v1, int v2)
        {
            if (v1 < v2)
                sets[v2] = v1;
            else
                sets[v1] = v2;
        }

        public void BuildSpanningTree()
        {            
            Console.WriteLine("Количество ребер = " + _edgesCount);
            int i;
            this.ArrangeEdges(_edgesCount);
            this.Cost = 0;
            tree = new SortedList<int, int>();
            for (i = 0; i < _edgesCount; i++)
            {
                if (this.Find(_edges[i].U) != this.Find(_edges[i].V))
                {
                    if (!tree.ContainsKey(_edges[i].U))
                    {
                        tree.Add(_edges[i].U, _edges[i].V);
                        _edges.Remove(new Edge(_edges[i].V, _edges[i].U, _edges[i].Weight));
                        this.Join(_edges[i].U, _edges[i].V);
                        this.Cost += _edges[i].Weight;                        
                        _edgesCount--;
                    }                  
                }
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine("The Edges of the Minimum Spanning Tree are:");

            foreach (var couple in tree)
            {
                Console.WriteLine(couple);
            }
        }
    }
}
