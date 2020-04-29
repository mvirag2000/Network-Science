using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security;

namespace Charts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(Object sender, EventArgs e)
        {
            int limit = 100;
            double x_left = -2.0;
            double x_right = 2.0;
            Series values = new Series();
            double x_range = x_right - x_left;
            double x_inc = x_range / (limit);
            for (int i = 1; i < limit; i++)
            {
                double x = x_left + i * x_inc;
                double y = 3.9 - Math.Pow(x, 2); 
                values.Points.Add(new DataPoint(x, y));
            }
            chart1.Series.Add(values);
        }
        private void chart1_Click(object sender, EventArgs e)
        {
        }
        public class Edge
        {
            public int a, b;
            public Edge(int AA, int BB)
            {
                a = AA;
                b = BB;
            }
            public Edge(string AA, string BB)
            {
                a = int.Parse(AA);
                b = int.Parse(BB);
            }
        }
        public class Node
        {
            public int node_ID;
            public int degree;
            public List<int> neighbors;
            public float clustering;
            public float neighbors_degree;
            public Node(int ID)
            {
                node_ID = ID;
                degree = 0;
                neighbors = new List<int> { }; //Neighbor's node_ID
                clustering = 0;
                neighbors_degree = 0;
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                List<Edge> edgelist = new List<Edge> { };
                try
                {
                    using (StreamReader file = new StreamReader(openFileDialog1.FileName))
                    {
                        string line;
                        while ((line = file.ReadLine()) != null)
                        {
                            string[] token = line.Split('\t');
                            edgelist.Add(new Edge(token[0], token[1]));
                            textBox1.Text = "Building Edge List " + token[0];
                            Application.DoEvents();
                        }
                    }
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
                List<Node> nodelist = new List<Node> { };
                int selfies = 0;
                int dupes = 0;
                foreach (Edge edge in edgelist) //Build nodelist  
                {
                    if (!nodelist.Exists(x => x.node_ID == edge.a)) nodelist.Add(new Node(edge.a));
                    if (!nodelist.Exists(x => x.node_ID == edge.b)) nodelist.Add(new Node(edge.b));
                    if (edge.a == edge.b) selfies++;
                    if (edgelist.Exists(x => x.a == edge.a && x.b == edge.b && !(x == edge)) ||
                        edgelist.Exists(x => x.b == edge.a && x.a == edge.b && !(x == edge))) dupes++;
                }
                int max = 0;
                int links = edgelist.Count();
                int nodes = nodelist.Count();
                string[] filename = openFileDialog1.FileName.Split('\\');
                lblDataset.Text = "Dataset: " + filename[filename.Length - 1];
                lblLinks.Text = "Links: " + links;
                lblNodes.Text = "Nodes: " + nodes;
                textBox1.Text += "Building Neighbor Lists\r\n";
                Application.DoEvents();
                foreach (Node node in nodelist) //Traverse nodelist (1) degrees and neighbor list 
                {
                    node.degree = 0;
                    foreach (Edge edge in edgelist)
                    {
                        if (edge.a == node.node_ID)
                        {
                            node.neighbors.Add(edge.b);
                            node.degree++;
                        }
                        if (edge.b == node.node_ID)
                        {
                            node.neighbors.Add(edge.a); 
                            node.degree++;
                        }
                    }
                    max = Math.Max(max, node.degree);
                }
                float total_cluster = 0;
                foreach (Node node in nodelist) //Traverse nodelist (2) clustering and degree correlation 
                {
                    int cluster = 0;
                    textBox1.Text = "Calculating clustering coefficient " + node.node_ID.ToString(); 
                    Application.DoEvents();
                    foreach (int left in node.neighbors)
                    {
                        foreach (int right in node.neighbors)
                        {
                            if (edgelist.Exists(x => x.a == left && x.b == right)) cluster++;
                        }
                        node.neighbors_degree += nodelist.Find(x => left == x.node_ID).degree;
                    }
                    node.neighbors_degree = node.neighbors_degree / node.degree;
                    if (node.degree < 2) node.clustering = 0;
                    else node.clustering = (float) 2 * cluster / (node.degree * (node.degree - 1));
                    total_cluster += node.clustering;
                }
                lblMean.Text = "Average Degree: " + String.Format("{0:n2}", (float)2 * links / nodes);
                lblMax.Text = "Max Degree: " + max;
                lblSelf.Text = "Self links: " + selfies;
                lblDupe.Text = "Duplicate links: " + dupes;
                lblCluster.Text = "Clustering coefficient: " + String.Format("{0:n3}", total_cluster / nodes);
                Series neighbors = new Series();
                foreach (Node node in nodelist)
                {
                    textBox1.Text = "Building Chart Series " + node.node_ID.ToString();
                    Application.DoEvents();
                    neighbors.Points.Add(new DataPoint(node.degree, node.neighbors_degree));
                }
                foreach (Node node in nodelist)
                {
                    textBox1.Text = node.node_ID + " Degree: " + node.degree;
                    textBox1.Text += " Degrees: " + String.Format("{0:n3}", node.neighbors_degree);
                    Application.DoEvents();
                }
                chart1.Series.Clear();
                chart1.ChartAreas[0].AxisX.IsLogarithmic = true;
                chart1.ChartAreas[0].AxisY.IsLogarithmic = true;
                neighbors.ChartType = SeriesChartType.Point;
                chart1.Series.Add(neighbors);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void label1_Click_1(object sender, EventArgs e)
        {
        }
    }
}
