using QuikGraph;
using QuikGraph.Algorithms.Observers;
using QuikGraph.Algorithms.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseCalculator.Classes
{
    class PersonsGraph
    {
        AdjacencyGraph<Person, Edge<Person>> graph = new AdjacencyGraph<Person, Edge<Person>>();
        BreadthFirstSearchAlgorithm<Person, Edge<Person>> bfs;
        DepthFirstSearchAlgorithm<Person, Edge<Person>> dfs;

        public PersonsGraph(Person p) 
        {
            bfs = new BreadthFirstSearchAlgorithm<Person, Edge<Person>>(graph);
            dfs = new DepthFirstSearchAlgorithm<Person, Edge<Person>>(graph);
            graph.AddVertex(p);
            bfs.DiscoverVertex += new VertexAction<Person>((Person discovered) => { discovered.Calculate(); });
        }

        public void AddVertex(Person parent, Person child) 
        {
            if (!graph.Vertices.Contains(child))
            {
                graph.AddVertex(child);
            }
            if (!graph.Vertices.Contains(parent))
            {
                graph.AddVertex(parent);
                bfs.SetRootVertex(parent);
            }

            graph.AddEdge(new Edge<Person>(parent, child));

            if (parent.gender)
            {
                child.father = parent;
            }
            else
            {
                child.mother = parent;
            }
        }

        public void Recalculate() 
        {
            bfs.Compute();
        }
    }
}
