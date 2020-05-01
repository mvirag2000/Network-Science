import pandas as pd
import math
import numpy as np 
from bokeh.plotting import figure, output_file, show
from bokeh.palettes import brewer   
from bokeh.models import Span, LinearColorMapper, ColumnDataSource, NumeralTickFormatter
from bokeh.models.widgets import DataTable, TableColumn, StringFormatter, Paragraph, Div
from bokeh.layouts import column, row, grid 
from scipy import stats as statlib
dataset = "www.edgelist.txt"
directed = True
want_in = True
edgelist = pd.read_csv('F:\\Math\\Network Science\\' + dataset, sep='\t', lineterminator='\n', names=['A', 'B']) 
filename = "F:\\Math\\Network Science\\" + dataset.split(".")[0] + ".histo.csv"
print(filename)
nodes = edgelist.values.max() + 1 # Assuming no skipped values, this is the (zero-based) number of nodes 
links = edgelist.shape[0]
print("Nodes:", nodes, " Links:", links)
out = edgelist.groupby('A').count()
degree = pd.DataFrame.from_dict(out)
degree['In'] = edgelist.groupby('B').count()
degree.fillna(0, inplace=True)
degree.rename(columns={'B': 'Out'}, inplace=True)
degree['Both'] = degree['In'] + degree['Out']
if not directed:
    direction = 'Both'
else:
    if want_in:
        direction = 'In'
    else:
        direction = 'Out'
max_deg = degree[direction].max()
min_deg = degree[direction].min()
bins_count = int((max_deg - min_deg) / 5)
degree_list = degree[direction]
bins = np.logspace(np.log10(min_deg), np.log10(max_deg), num=bins_count, base=10) # Logarithmic binning 
groups = degree_list.groupby(pd.cut(degree_list, bins)).count()
histo = pd.DataFrame.from_dict(groups)
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
string1 += "Average Degree: %1.2f <br>" % degree[direction].mean()
string1 += "Directed graph: " + direction + "<br>"
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