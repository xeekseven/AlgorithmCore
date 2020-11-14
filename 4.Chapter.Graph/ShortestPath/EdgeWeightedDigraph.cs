using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShortestPath
{
    public class EdgeWeightedDigraph
    {
        private int _V; //顶点数
        private int _E; //边数

        private HashSet<DirectedEdge>[] _adj;

        public EdgeWeightedDigraph(int v)
        {
            _V = v;
            _E = 0;
            _adj = new HashSet<DirectedEdge>[v];
            for (int s = 0; s < _V; s++)
            {
                _adj[s] = new HashSet<DirectedEdge>();
            }
        }

        public int V => _V;
        public int E => _E;

        public void AddEdge(DirectedEdge e)
        {
            _adj[e.From].Add(e);
            _E++;
        }

        public HashSet<DirectedEdge> Adj(int v)
        {
            return _adj[v];
        }

        public HashSet<DirectedEdge> Edges()
        {
            HashSet<DirectedEdge> edges = new HashSet<DirectedEdge>();
            for (int v = 0; v < _V; v++)
            {
                foreach (DirectedEdge e in _adj[v])
                {
                    edges.Add(e);
                }
            }
            return edges;
        }

        public string Visualize()
        {
            DigraphVisualizer vis = new DigraphVisualizer();
            for (int i = 0; i < _adj.Length; i++)
            {
                var node = new DigraphVisualizer.Node();
                node.label = i.ToString();
                node.id = i.ToString();
                vis.nodes.Add(node);
                foreach (var ei in _adj[i])
                {
                    var e = new DigraphVisualizer.Edge()
                    {
                        from = i.ToString(),
                        to = ei.To.ToString()
                    };
                    vis.edges.Add(e);
                }
            }
            return JsonConvert.SerializeObject(vis);
        }
    }
}