# Final Year Project: Boid Algorithm
### The Background
- Boid algorithm is an algorithm that developed by Craig Reynolds in 1986 to simulate flocking behavior of bird. Boid itself stands for _bird-oid_ which means bird-like object. 
- In 1999, Reynolds made the proceedings for Game Developers Conference that specifically discuss steering behaviors of autonomous agent (boid is an autonomous agent). 
- In that paper, basic building blocks of boid are classified into three main part cohesion, separation, alignment.
- Another basic building blocks such as seek, flee, arrival, wander, perch, evade etc can be compose to make another behavior (like leader following behavior is implemented using seek, arrival, and evade)

### The Rules
In the flock, each boid agent must obey three main building blocks that mentioned above:
1. Cohesion
Boid is attracted to each other and they attempt to stay close relative to each other. 
2. Separation
Boid will maintain a certain distance that separate each other so they didn't collide with each other
3. Alignment
Boid would to try align their velocity (speed and direction) with nearby boid 

### The Math
Position of each boid $x_i$ is updated by velocity $v_i$ that has been computed every frame/timescale iteratively. 
The $\Delta v_i$ is calculated by adding all of the building blocks that is, 
- $\Delta v_{i-c} = x_i - \dfrac{\Sigma_{j\in S_c} \cdot x_j}{n_c}$ for cohesion;

 
- $\Delta v_{i-s} = \Sigma_{j \in S_s} \dfrac{(x_i - x_j)} {|(x_i-x_j)|}$ for separation; and 


- $\Delta v_{i-c} = v_i - \dfrac{\Sigma_{j\in S_a} \cdot v_j}{n_a}$ for alignment,

with i is the boid we're working with; j is another boid; W is weight that determines the amplitudes of these interaction; S is interaction range around each agent; n is number of each boid that evaluated in the range. Adding them up, we get the equation below: 

$$ \Delta v_i = W_c \cdot \Delta v_{i-c} + W_s \cdot \Delta v_{i-s} + W_a \cdot \Delta v_{i-a}$$
