##
## Simulation of population change under selection pressure
##
import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
from matplotlib import cm
import seaborn as sns
sns.set_theme() 
sns.set_palette('RdBu', 2)

INIT_POP = 10000
INIT_MEAN = 100
INIT_SD = 16
PREDATION = 0.03
GROWTH = 0.03
GENERATIONS = 20

# Initialize population
equilibrium = 1 / (1 - PREDATION) # Equilibrium condition
trait_sd = INIT_SD
trait_mean = INIT_MEAN
population = INIT_POP

for gen in range(GENERATIONS + 1):

    pop = pd.DataFrame(index=np.arange(population))
    pop['Trait'] = np.random.default_rng().normal(trait_mean, trait_sd, pop.shape[0])
    pop['Survive'] = 'Yes'
    pop.sort_values(by='Trait', ascending=True, inplace=True, ignore_index=True)
    cutoff = int(pop.shape[0] * PREDATION)
    pop.loc[pop.index < cutoff, 'Survive'] = 'No'
    sns.histplot(pop, x='Trait', hue='Survive', element='step', multiple='stack')
    plt.xlim(0)
    title_str = 'Generation: ' + str(gen) + ', Trait mean: %1.1f' % trait_mean
    plt.title(title_str)
    plt.show()
    pop.drop(pop[pop['Survive'] == 'No'].index, inplace=True) 
    population = pop.shape[0]
    trait_mean = pop['Trait'].mean()
    reproduction = int(population * equilibrium * GROWTH)
    population += reproduction
    print(population)