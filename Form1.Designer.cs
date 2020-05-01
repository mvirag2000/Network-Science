using System;
using System.Windows.Forms.DataVisualization.Charting;
namespace Charts
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblLinks = new System.Windows.Forms.Label();
            this.lblNodes = new System.Windows.Forms.Label();
            this.lblMean = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.lblSelf = new System.Windows.Forms.Label();
            this.lblDupe = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblCluster = new System.Windows.Forms.Label();
            this.lblDataset = new System.Windows.Forms.Label();
            this.lblCorrelation = new System.Windows.Forms.Label();
            this.chkClustering = new System.Windows.Forms.CheckBox();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblGamma = new System.Windows.Forms.Label();
            this.lblMin = new System.Windows.Forms.Label();
            this.txtCutoff = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(15, 121);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(407, 300);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            title1.Name = "Title1";
            title1.Text = "Degree Correlation";
            this.chart1.Titles.Add(title1);
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(633, 34);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(235, 78);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblLinks
            // 
            this.lblLinks.AutoSize = true;
            this.lblLinks.Location = new System.Drawing.Point(12, 50);
            this.lblLinks.Name = "lblLinks";
            this.lblLinks.Size = new System.Drawing.Size(32, 13);
            this.lblLinks.TabIndex = 2;
            this.lblLinks.Text = "Links";
            this.lblLinks.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblNodes
            // 
            this.lblNodes.AutoSize = true;
            this.lblNodes.Location = new System.Drawing.Point(12, 37);
            this.lblNodes.Name = "lblNodes";
            this.lblNodes.Size = new System.Drawing.Size(38, 13);
            this.lblNodes.TabIndex = 3;
            this.lblNodes.Text = "Nodes";
            // 
            // lblMean
            // 
            this.lblMean.AutoSize = true;
            this.lblMean.Location = new System.Drawing.Point(12, 76);
            this.lblMean.Name = "lblMean";
            this.lblMean.Size = new System.Drawing.Size(47, 13);
            this.lblMean.TabIndex = 4;
            this.lblMean.Text = "Average";
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(198, 92);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(65, 13);
            this.lblMax.TabIndex = 5;
            this.lblMax.Text = "Max Degree";
            // 
            // lblSelf
            // 
            this.lblSelf.AutoSize = true;
            this.lblSelf.Location = new System.Drawing.Point(198, 37);
            this.lblSelf.Name = "lblSelf";
            this.lblSelf.Size = new System.Drawing.Size(53, 13);
            this.lblSelf.TabIndex = 6;
            this.lblSelf.Text = "Self Links";
            // 
            // lblDupe
            // 
            this.lblDupe.AutoSize = true;
            this.lblDupe.Location = new System.Drawing.Point(198, 50);
            this.lblDupe.Name = "lblDupe";
            this.lblDupe.Size = new System.Drawing.Size(57, 13);
            this.lblDupe.TabIndex = 7;
            this.lblDupe.Text = "Duplicates";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(883, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // lblCluster
            // 
            this.lblCluster.AutoSize = true;
            this.lblCluster.Location = new System.Drawing.Point(198, 63);
            this.lblCluster.Name = "lblCluster";
            this.lblCluster.Size = new System.Drawing.Size(53, 13);
            this.lblCluster.TabIndex = 9;
            this.lblCluster.Text = "Clustering";
            this.lblCluster.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // lblDataset
            // 
            this.lblDataset.AutoSize = true;
            this.lblDataset.Location = new System.Drawing.Point(12, 24);
            this.lblDataset.Name = "lblDataset";
            this.lblDataset.Size = new System.Drawing.Size(44, 13);
            this.lblDataset.TabIndex = 10;
            this.lblDataset.Text = "Dataset";
            // 
            // lblCorrelation
            // 
            this.lblCorrelation.AutoSize = true;
            this.lblCorrelation.Location = new System.Drawing.Point(12, 102);
            this.lblCorrelation.Name = "lblCorrelation";
            this.lblCorrelation.Size = new System.Drawing.Size(57, 13);
            this.lblCorrelation.TabIndex = 11;
            this.lblCorrelation.Text = "Correlation";
            // 
            // chkClustering
            // 
            this.chkClustering.AutoSize = true;
            this.chkClustering.Location = new System.Drawing.Point(184, 62);
            this.chkClustering.Name = "chkClustering";
            this.chkClustering.Size = new System.Drawing.Size(15, 14);
            this.chkClustering.TabIndex = 12;
            this.chkClustering.UseVisualStyleBackColor = true;
            // 
            // chart2
            // 
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            this.chart2.Location = new System.Drawing.Point(428, 121);
            this.chart2.Name = "chart2";
            series2.ChartArea = "ChartArea1";
            series2.Name = "Series1";
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(440, 300);
            this.chart2.TabIndex = 13;
            this.chart2.Text = "chart2";
            title2.Name = "Title2";
            title2.Text = "Cumulative Degree Distribution";
            this.chart2.Titles.Add(title2);
            // 
            // lblGamma
            // 
            this.lblGamma.AutoSize = true;
            this.lblGamma.Location = new System.Drawing.Point(12, 89);
            this.lblGamma.Name = "lblGamma";
            this.lblGamma.Size = new System.Drawing.Size(43, 13);
            this.lblGamma.TabIndex = 14;
            this.lblGamma.Text = "Gamma";
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Location = new System.Drawing.Point(198, 79);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(62, 13);
            this.lblMin.TabIndex = 15;
            this.lblMin.Text = "Min Degree";
            // 
            // txtCutoff
            // 
            this.txtCutoff.Location = new System.Drawing.Point(381, 33);
            this.txtCutoff.Name = "txtCutoff";
            this.txtCutoff.Size = new System.Drawing.Size(36, 20);
            this.txtCutoff.TabIndex = 16;
            this.txtCutoff.Text = "0";
            this.txtCutoff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCutoff.TextChanged += new System.EventHandler(this.txtCutoff_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(324, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Set Cutoff";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 439);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCutoff);
            this.Controls.Add(this.lblMin);
            this.Controls.Add(this.lblGamma);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.chkClustering);
            this.Controls.Add(this.lblCorrelation);
            this.Controls.Add(this.lblDataset);
            this.Controls.Add(this.lblCluster);
            this.Controls.Add(this.lblDupe);
            this.Controls.Add(this.lblSelf);
            this.Controls.Add(this.lblMax);
            this.Controls.Add(this.lblMean);
            this.Controls.Add(this.lblNodes);
            this.Controls.Add(this.lblLinks);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Network Analyzer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblLinks;
        private System.Windows.Forms.Label lblNodes;
        private System.Windows.Forms.Label lblMean;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label lblSelf;
        private System.Windows.Forms.Label lblDupe;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.Label lblCluster;
        private System.Windows.Forms.Label lblDataset;
        private System.Windows.Forms.Label lblCorrelation;
        private System.Windows.Forms.CheckBox chkClustering;
        public Chart chart2;
        private System.Windows.Forms.Label lblGamma;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.TextBox txtCutoff;
        private System.Windows.Forms.Label label1;
    }
}

