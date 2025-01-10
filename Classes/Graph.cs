using QuikGraph;
using QuikGraph.Algorithms.Observers;
using QuikGraph.Algorithms.Search;
using QuikGraph.Algorithms.ShortestPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseCalculator.Classes
{
    class PersonsGraph
    {
        // надо пересмотреть доступ
        public AdjacencyGraph<Person, Edge<Person>> graph = new AdjacencyGraph<Person, Edge<Person>>();
        BreadthFirstSearchAlgorithm<Person, Edge<Person>> bfs;
        FloydWarshallAllShortestPathAlgorithm<Person, Edge<Person>> floydWarshall;
        Person target;
        string save_str;

        public PersonsGraph(Person p) 
        {
            bfs = new BreadthFirstSearchAlgorithm<Person, Edge<Person>>(graph);
            floydWarshall = new FloydWarshallAllShortestPathAlgorithm<Person, Edge<Person>>(graph, double (Edge<Person> t) => { return 1; });
            graph.AddVertex(p);
            target = p;
            bfs.SetRootVertex(p);
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
            double tmp, maxdist;
            tmp = maxdist = 0;
            Person hiParent = target;
            foreach (Person vertex in graph.Vertices) 
            {
                if (floydWarshall.TryGetDistance(target, vertex, out tmp))
                {
                    if (tmp > maxdist)
                    {
                        maxdist = tmp;
                        hiParent = vertex;
                    }
                }
            }

            bfs.SetRootVertex(hiParent);

            bfs.Compute();
        }

        //save func
        //load func
    }
}
