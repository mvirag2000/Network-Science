using System;
using System.Collections;
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
using MathNet.Numerics.LinearRegression;

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
            Series values2 = new Series();
            double x_range = x_right - x_left;
            double x_inc = x_range / (limit);
            for (int i = 1; i < limit; i++)
            {
                double x = x_left + i * x_inc;
                double y = 3.9 - Math.Pow(x, 2); 
                values.Points.Add(new DataPoint(x, y));
                values2.Points.Add(new DataPoint(x, 4-y));
            }
            chart1.Series.Add(values);
            chart2.Series.Add(values2);
            int cutoff = Convert.ToInt32(txtCutoff.Text);
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
            public double clustering;
            public double neighbors_degree;
            public Node(int ID)
            {
                node_ID = ID;
                degree = 0;
                neighbors = new List<int> { }; //Neighbors' node_IDs
                neighbors_degree = 0; //Average of neighbors' degrees 
            }
        }
        public class NodeList : IEnumerable<Node>
        {
            private List<Node> nodelist;
            public int selfies;
            public int dupes;
            public int max_degree;
            public int min_degree;
            public IEnumerator<Node> GetEnumerator()
            {
                return nodelist.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return nodelist.GetEnumerator();
            }
            public NodeList(List<Edge> edgelist, Control msg)
            {
                nodelist = new List<Node> { };
                selfies = 0;
                dupes = 0;
                foreach (Edge edge in edgelist) //Build nodelist  
                {
                    msg.Text = "Building Node List: " + edge.a; Application.DoEvents();
                    if (!nodelist.Exists(x => x.node_ID == edge.a)) nodelist.Add(new Node(edge.a));
                    if (!nodelist.Exists(x => x.node_ID == edge.b)) nodelist.Add(new Node(edge.b));
                    if (edge.a == edge.b) selfies++;
                    if (edgelist.Exists(x => x.a == edge.a && x.b == edge.b && !(x == edge)) ||
                        edgelist.Exists(x => x.b == edge.a && x.a == edge.b && !(x == edge))) dupes++;
                }
                min_degree = nodelist.Count();
                max_degree = 0;
                foreach (Node node in nodelist) //Traverse nodelist (1) degrees and neighbor list 
                {
                    msg.Text = "Building Neighbor List: " + node.node_ID; Application.DoEvents();
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
                    max_degree = Math.Max(max_degree, node.degree);
                    min_degree = Math.Min(min_degree, node.degree);
                }
                foreach (Node node in nodelist)
                {
                    msg.Text = "Calculating degree correlation: " + node.node_ID.ToString(); Application.DoEvents();
                    foreach (int neighbor in node.neighbors)
                    {
                        node.neighbors_degree += nodelist.Find(x => neighbor == x.node_ID).degree;
                    }
                    node.neighbors_degree = node.neighbors_degree / node.degree; //Average of neighbors' degrees
                }
            }
            public Node Find(Predicate<Node> match)
            {
                return nodelist.Find(match);
            }
            public double Clustering(List<Edge> e, Control msg)
            {
                double total_cluster = 0;
                int nodes = nodelist.Count();
                foreach (Node node in nodelist)  
                {
                    msg.Text = "Calculating clustering coefficient " + node.node_ID.ToString(); Application.DoEvents();
                    int cluster = 0;
                    foreach (int left in node.neighbors)
                    {
                        foreach (int right in node.neighbors)
                        {
                            if (e.Exists(x => x.a == left && x.b == right)) cluster++;
                        }
                    }
                    if (node.degree > 1) node.clustering = (double) 2 * cluster / (node.degree * (node.degree - 1)); 
                    total_cluster += node.clustering;
                }
                return total_cluster / nodes;  //Average clustering 
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
                            string[] token = line.Split('\t'); //Remember files may use tab or comma                            
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
                NodeList nodelist = new NodeList(edgelist, textBox1);
                int links = edgelist.Count();
                int nodes = nodelist.Count();
                int max = nodelist.max_degree;
                int min = nodelist.min_degree;
                string[] filename = openFileDialog1.FileName.Split('\\');
                lblDataset.Text = "Dataset: " + filename[filename.Length - 1];
                lblLinks.Text = "Links: " + links;
                lblNodes.Text = "Nodes: " + nodes;
                lblMean.Text = "Average Degree: " + String.Format("{0:n2}", (float)2 * links / nodes);
                lblSelf.Text = "Self links: " + nodelist.selfies;
                lblDupe.Text = "Duplicate links: " + nodelist.dupes;
                lblMax.Text = "Max Degree: " + max;
                lblMin.Text = "Min Degree: " + min;
                if (chkClustering.Checked) lblCluster.Text = "Average clustering coefficient: " + String.Format("{0:n3}", nodelist.Clustering(edgelist, textBox1));  
                Series neighbors = new Series();
                for (int deg = 1; deg <= max; deg++)
                {
                    double count = 0;
                    double accum = 0;
                    textBox1.Text = "Building Correlation Series " + deg.ToString();
                    Application.DoEvents();
                    foreach (Node node in nodelist)
                    {
                        if (node.degree == deg)
                        {
                            accum += node.neighbors_degree;
                            count++;
                        }
                    }
                    if (count > 0) neighbors.Points.Add(new DataPoint(deg, accum/count));
                }
                double[] XValues, YValues;
                int points = neighbors.Points.Count();
                XValues = new double[points];
                YValues = new double[points];
                for (int i = 0; i < points; i++)
                {
                    XValues[i] = neighbors.Points[i].XValue;
                    YValues[i] = neighbors.Points[i].YValues[0];
                }
                Tuple<double, double> r = MathNet.Numerics.Fit.Power(XValues, YValues);
                double c = r.Item1;
                double exp = r.Item2;
                lblCorrelation.Text = "Degree correlation exponent: " + String.Format("{0:n3}", exp);
                Series regression = new Series();
                double temp_x, temp_y;
                for (int i = 0; i < points; i++)
                {
                    textBox1.Text = "Building Regression Line " + i.ToString();
                    Application.DoEvents();
                    temp_x = XValues[i];
                    temp_y = c * Math.Pow(temp_x, exp);
                    regression.Points.Add(new DataPoint(temp_x, temp_y)); 
                }
                chart1.Series.Clear();
                chart1.ChartAreas[0].AxisX.IsLogarithmic = true;
                chart1.ChartAreas[0].AxisY.IsLogarithmic = true;
                neighbors.ChartType = SeriesChartType.Point;
                chart1.Series.Add(neighbors);
                regression.ChartType = SeriesChartType.Line;
                regression.Color = Color.Red;
                chart1.Series.Add(regression);
                /*
                 * Wrote this groovy log-binning routine and ended up not using it
                 * 
                int bins = max - min + 1; //Which would give one each, iff no gaps 
                double logsize = Math.Log10(max / min) / bins;
                double left, right;
                left = Math.Log10(min);
                Series distro = new Series();
                for (int deg = 1; deg <= max; deg++)
                {
                    right = left + Math.Pow(deg * logsize, 10); 
                    double count = 0;
                    foreach (Node node in nodelist) if (node.degree >= left && node.degree < right) count++;
                    if (count > 0) distro.Points.Add(new DataPoint(right, count/nodes));
                    left = right;
                } */
                Series distro = new Series();
                int cutoff = Convert.ToInt32(txtCutoff.Text);
                Series shadow = new Series();
                double prev = 1;
                for (int deg = 1; deg <= max; deg++)
                {
                    double count = 0;
                    textBox1.Text = "Building Degree Distribution " + deg.ToString();
                    Application.DoEvents();
                    foreach (Node node in nodelist) if (node.degree == deg) count++;
                    distro.Points.Add(new DataPoint(deg, prev));
                    if (deg > cutoff) shadow.Points.Add(new DataPoint(deg, prev));
                    prev = prev - count / nodes; 
                }
                double[] X2Values, Y2Values;
                points = shadow.Points.Count();
                X2Values = new double[points];
                Y2Values = new double[points];
                for (int i = 0; i < points; i++)
                {
                    X2Values[i] = shadow.Points[i].XValue;
                    Y2Values[i] = shadow.Points[i].YValues[0];
                }
                Tuple<double, double> r2 = MathNet.Numerics.Fit.Power(X2Values, Y2Values);
                double c2 = r2.Item1;
                double exp2 = r2.Item2;
                lblGamma.Text = "Degree exponent (gamma): " + String.Format("{0:n3}", 1-exp2);  //For cumulative distro gamma = 1 - exp 
                Series regression2 = new Series();
                double temp_x2, temp_y2;
                for (int i = 0; i < points; i++)
                {
                    textBox1.Text = "Building Regression Line " + i.ToString();
                    Application.DoEvents();
                    temp_x2 = X2Values[i];
                    temp_y2 = c2 * Math.Pow(temp_x2, exp2);
                    regression2.Points.Add(new DataPoint(temp_x2, temp_y2));
                }
                chart2.Series.Clear();
                chart2.ChartAreas[0].AxisX.IsLogarithmic = true;
                chart2.ChartAreas[0].AxisY.IsLogarithmic = true;
                distro.ChartType = SeriesChartType.Point;
                chart2.Series.Add(distro);
                regression2.ChartType = SeriesChartType.Line;
                regression2.Color = Color.Red;
                chart2.Series.Add(regression2);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void label1_Click_1(object sender, EventArgs e)
        {
        }

        private void txtCutoff_TextChanged(object sender, EventArgs e)
        {
            int cutoff = Convert.ToInt32(txtCutoff.Text);
        }
    }
}
