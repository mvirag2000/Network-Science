using System;
using System.Collections.Generic;

namespace Networks
{
    class Program //Network Science 5.12 Homework  
    {
        static void Main(string[] args)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("F:\\Math\\Network Science\\barabasi.edgelist2.txt");
            Random rnd = new Random();
            int m = 4; //Links per new node (m)
            int nodes = 2000; //Nodes in network (N) 
            int old_node, rnd_node_idx, count;
            List<Edge> edgelist = new List<Edge> 
            {
                new Edge(0, 1), new Edge(1, 2), new Edge(2, 3), new Edge(3, 0), new Edge(0, 2), new Edge(1, 3)
            };
            for (int new_node = 4; new_node < nodes; new_node++)
            {
                count = edgelist.Count;
                for (int i = 1; i <= m; i++)
                {
                    do
                    {
                        rnd_node_idx = rnd.Next(1, count * 2 + 1);  //Treat edge list as single (one-based) node list  
                        if (rnd_node_idx % 2 == 0)
                            old_node = edgelist[rnd_node_idx / 2 - 1].b;
                        else
                            old_node = edgelist[(int)rnd_node_idx / 2].a;
                    }
                    while (edgelist.Exists(x => (x.a == new_node && x.b == old_node)));  //Don't link to same node twice 
                    edgelist.Add(new Edge(new_node, old_node));
                }
            }
            foreach (var item in edgelist)
            {
                //Console.WriteLine(item.a + "," + item.b);
                file.WriteLine(item.a + "\t" + item.b);
            }
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
    }
}
