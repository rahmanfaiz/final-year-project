# -*- coding: utf-8 -*-
"""
Created on Tue Nov  1 11:33:15 2022

@author: Faiz
"""
import matplotlib.pyplot as plt
import pandas as pd
import numpy as np

data = pd.read_csv('speedData1.csv')
boid_num = 250;
iteration = data.size/3; 

#print(data['Boid'] == 'Boid0') 
#print(data[data['Boid'] == 'Boid0']) 
#print(iteration)

data_boid0 = data[data['Boid'] == 'Boid0']
boid0_speed_data = data_boid0['Speed'].to_numpy()
time_data = data_boid0['Time'].to_numpy()

#data_boid0.to_csv('DataBoid0.csv')
print(time_data)

plt.scatter(time_data, boid0_speed_data)
plt.show()