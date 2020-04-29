import pandas as pd
import math
import numpy as np 
from bokeh.plotting import figure, output_file, show
from bokeh.palettes import brewer   
from bokeh.models import Span, LinearColorMapper, ColumnDataSource, NumeralTickFormatter
from bokeh.models.widgets import DataTable, TableColumn, StringFormatter, Paragraph, Div
from bokeh.layouts import column, row, grid 
from scipy import stats as statlib
dataset = "barabasi.edgelist.txt"
directed = False
want_in = False
edgelist = pd.read_csv('F:\\Math\\Network Science\\' + dataset, sep='\t', lineterminator='\n', names=['A', 'B']) 
filename = "F:\\Math\\Network Science\\" + dataset.split(".")[0] + ".histo.csv"
print(filename)
nodes = edgelist.values.max() + 1 # Assuming no skipped values, this is the (zero-based) number of nodes 
links = edgelist.shape[0]
print("Nodes:", nodes, " Links:", links)
matrix = np.zeros(shape=[nodes, nodes]).astype(np.int) 
matrix[edgelist['A'],edgelist['B']] = 1
if (directed == False):
   matrix[edgelist['B'],edgelist['A']] = 1
if (directed == True and want_in == True):
    stats = pd.DataFrame(matrix.sum(axis=0))
else:
    stats = pd.DataFrame(matrix.sum(axis=1))
stats.columns = ['Degree']
max_deg = stats['Degree'].max()
min_deg = stats['Degree'].min()
bins = np.logspace(np.log10(min_deg), np.log10(max_deg), num=50, base=10) # Logarithmic binning 
groups = stats.groupby(pd.cut(stats['Degree'], bins))
histo = groups.count()
histo.columns = ['Count']
histo['Left'] = histo.index.categories.left
histo['Right'] = histo.index.categories.right
histo.drop(histo[histo['Count'] == 0].index, inplace=True)
histo['Log_Right'] = np.log10(histo['Right'])
histo['Log_Count'] = np.log10(histo['Count']) 
x = histo['Log_Right']
y = histo['Log_Count'] 
slope, intercept, r_value, p_value, std_err = statlib.linregress(x, y) 
histo['Pred_Log_Count'] = histo['Log_Right'] * slope + intercept  
histo['Pred_Count'] = 10**(histo['Pred_Log_Count'])
histo.to_csv(filename, index=False)
string1 = "Dataset: " + dataset +"<br>" 
string1 += "Nodes: %1.0f <br>" % nodes
string1 += "Links: %1.0f <br>" % links 
string1 += "Average Degree: %1.2f <br>" % stats['Degree'].mean()
string1 += "Max Degree: %1.0f <br>" % max_deg 
string1 += "Gamma: %1.2f <br>" % -slope
para1 = Div(text=string1, width=450) 
p = figure(plot_width=1000, plot_height=700, title='Degree Distribution', y_axis_type = "log", x_axis_type="log")  
p.yaxis.axis_label = 'Count p(k)' 
p.xaxis.axis_label = 'Local Degree (k)'
p.x_range.start = min_deg
p.circle(histo['Right'], histo['Count'], fill_color="blue")
p.line(histo['Right'], histo['Pred_Count'], line_color = 'red')
output_file('chart.htm')
layout = grid([
    [para1],
    [p]
])
show(layout)