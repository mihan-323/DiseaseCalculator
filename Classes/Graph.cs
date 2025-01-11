using QuikGraph;
using QuikGraph.Algorithms.Observers;
using QuikGraph.Algorithms.Search;
using QuikGraph.Algorithms.ShortestPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace DiseaseCalculator.Classes
{
    class PersonsGraph
    {
        // надо пересмотреть доступ
        public AdjacencyGraph<PersonControl, Edge<PersonControl>> graph = new AdjacencyGraph<PersonControl, Edge<PersonControl>>();
        public BreadthFirstSearchAlgorithm<PersonControl, Edge<PersonControl>> bfs;
        public FloydWarshallAllShortestPathAlgorithm<PersonControl, Edge<PersonControl>> floydWarshall;
        public PersonControl target;
        string save_str;

        public PersonsGraph(PersonControl p) 
        {
            bfs = new BreadthFirstSearchAlgorithm<PersonControl, Edge<PersonControl>>(graph);
            floydWarshall = new FloydWarshallAllShortestPathAlgorithm<PersonControl, Edge<PersonControl>>(graph, double (Edge<PersonControl> t) => { return 1; });
            graph.AddVertex(p);
            target = p;
            bfs.SetRootVertex(p);
            //bfs.DiscoverVertex += new VertexAction<PersonControl>((PersonControl discovered) => { discovered.person.Calculate(); });
            bfs.DiscoverVertex += new VertexAction<PersonControl>((PersonControl discovered) => { discovered.CalculateDisease(); });
        }

        public void AddVertex(PersonControl parent, PersonControl child) 
        {
            if (child.person.father != null && parent.person.gender)
                throw new Exception("Максимальное число родителей M: 1");
            if (child.person.mother != null && !parent.person.gender)
                throw new Exception("Максимальное число родителей F: 1");

            if (!graph.Vertices.Contains(child))
            {
                graph.AddVertex(child);
            }
            if (!graph.Vertices.Contains(parent))
            {
                graph.AddVertex(parent);
                bfs.SetRootVertex(parent);
            }

            graph.AddEdge(new Edge<PersonControl>(parent, child));
            child.AddLine(parent);

            if (parent.person.gender)
            {
                child.person.father = parent.person;
            }
            else
            {
                child.person.mother = parent.person;
            }
        }

        public void Recalculate() 
        {
            double tmp, maxdist;
            tmp = maxdist = 0;
            PersonControl hiParent = target;
            foreach (PersonControl vertex in graph.Vertices) 
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
