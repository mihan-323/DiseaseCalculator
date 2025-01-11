using QuikGraph;
using QuikGraph.Algorithms.Observers;
using QuikGraph.Algorithms.RankedShortestPath;
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

        public DijkstraShortestPathAlgorithm<PersonControl, Edge<PersonControl>> dijkstraSP;

        public PersonControl? target;
        public bool isOpen = false;
        //string save_str;

        public PersonsGraph(/*PersonControl p*/) 
        {
            bfs = new BreadthFirstSearchAlgorithm<PersonControl, Edge<PersonControl>>(graph);

            dijkstraSP = new DijkstraShortestPathAlgorithm<PersonControl, Edge<PersonControl>>(graph, double (Edge<PersonControl> t) => { return 1; });
            //dijkstraSP.SetRootVertex(p);

            //graph.AddVertex(p);
            //target = p;
            //bfs.SetRootVertex(p);
            //bfs.DiscoverVertex += new VertexAction<PersonControl>((PersonControl discovered) => { discovered.person.Calculate(); });
            bfs.DiscoverVertex += new VertexAction<PersonControl>((PersonControl discovered) => { discovered.CalculateDisease(); });
        }

        public void SetTarget(PersonControl p)
        {
            dijkstraSP.SetRootVertex(p);
            graph.AddVertex(p);
            target = p;
            bfs.SetRootVertex(p);
            isOpen = true;
        }

        public void Close()
        {
            graph.Clear();
            bfs.ClearRootVertex();
            dijkstraSP.ClearRootVertex();
            target = null;
            isOpen = false;
        }

        public void AddVertex(PersonControl parent, PersonControl child) 
        {
            if (child.Person.father != null && parent.Person.gender)
                throw new Exception("Максимальное число родителей M: 1");
            if (child.Person.mother != null && !parent.Person.gender)
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

            if (parent.Person.gender)
            {
                child.Person.father = parent.Person;
            }
            else
            {
                child.Person.mother = parent.Person;
            }
        }

        public void Recalculate() 
        {
            double tmp, maxdist;
            tmp = maxdist = 0;
            PersonControl hiParent = target;

            dijkstraSP.SetRootVertex(target);
            dijkstraSP.Compute();

            foreach (PersonControl vertex in graph.Vertices) 
            {
                tmp = dijkstraSP.GetDistance(vertex);
                if (tmp >= maxdist)
                {
                    maxdist = tmp;
                    hiParent = vertex;
                }
            }

            bfs.SetRootVertex(hiParent);

            bfs.Compute();
        }

        //save func
        //load func
    }
}
