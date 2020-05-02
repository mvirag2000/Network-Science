using System;
using System.Collections;
using System.Collections.Generic;

namespace Networks
{
    class Program //Network Science 5.12 Homework  
    {
        static void Main(string[] args)
        {
            int nodes = 10000;
            int degree = 3;
            int prob = 50;
            bool sociable = false;
            System.IO.StreamWriter file = new System.IO.StreamWriter("F:\\Math\\Network Science\\dissociative.edgelist.txt");
            Edgelist edgelist = new Edgelist(nodes, degree, true);
            edgelist.Associate(prob, sociable);
            edgelist.Write(file);
            file.Close();
        }
        public class Edge
        {
            public int a, b;
            public Edge(int AA, int BB)
            {
                a = AA;
                b = BB;
            }
        }
        public class Edgelist : IEnumerable<Edge>
        {
            private List<Edge> edgelist;
            public IEnumerator<Edge> GetEnumerator()
            {
                return edgelist.GetEnumerator(); 
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return edgelist.GetEnumerator();
            }
            public Edgelist(List<Edge> l)
            {   //Creates this edgelist from input
                this.edgelist = l;
            }
            public Edgelist(int nodes, int degree, bool multi)
            {   //Creates edgelist using Barabasi-Albert preferential attachment 
                Random rnd = new Random();
                int old_node, rnd_node_idx, count;
                edgelist = new List<Edge> {new Edge(0, 1), new Edge(1, 2), new Edge(2, 3), new Edge(3, 0)};
                for (int new_node = 4; new_node < nodes; new_node++)
                {
                    count = edgelist.Count;
                    for (int i = 1; i <= degree; i++) 
                    {
                        do
                        {
                            rnd_node_idx = rnd.Next(1, count * 2 + 1);  //Treat edge list as single (one-based) node list  
                            if (rnd_node_idx % 2 == 0)
                                old_node = edgelist[rnd_node_idx / 2 - 1].b;
                            else
                                old_node = edgelist[(int)rnd_node_idx / 2].a;
                        }
                        while (edgelist.Exists(x => (x.a == new_node && x.b == old_node)) && !multi);  //Don't link to same node twice 
                        edgelist.Add(new Edge(new_node, old_node));
                    }
                }
            }
            public void Associate(int p, bool positive)
            {
                Random rnd = new Random();
                int count = edgelist.Count;
                int stop = count * p / 100; 
                List<int> values = new List<int> { };
                for (int i = 0; i < stop; i++) //Do p% of links  
                {
                    Edge edge1 = edgelist[rnd.Next(0, count)];
                    Edge edge2 = edgelist[rnd.Next(0, count)];
                    values.Add(edge1.a); values.Add(edge1.b); values.Add(edge2.a); values.Add(edge2.b);
                    values.Sort();
                    if (values[0] != values[1] && values[2] != values[3] && positive) //Avoid creating self-links
                    {
                        edgelist.Remove(edge1);
                        edgelist.Remove(edge2);
                        edgelist.Add(new Edge(values[0], values[1])); 
                        edgelist.Add(new Edge(values[2], values[3]));
                    }
                    if (values[0] != values[3] && values[1] != values[2] && !positive) //Avoid creating self-links
                    {
                        edgelist.Remove(edge1);
                        edgelist.Remove(edge2);
                        edgelist.Add(new Edge(values[0], values[3]));
                        edgelist.Add(new Edge(values[1], values[2]));
                    }
                    values.Clear();
                }
            }
            public void Write(System.IO.StreamWriter file)
            {
                foreach (Edge edge in edgelist) file.WriteLine(edge.a + "\t" + edge.b);
            }
        }
    }
}
