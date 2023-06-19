# Mars-Exploration

## About the project

This is the final version of this project.

We drop a rover on a map populated by water, minerals, mountains and pits.

The rover explores the map in order to find certain conditions, to decide if it's colonizable or not.

Then constructs a command center that can create more rovers to collect resources.

The simulation stops when a second command center is built.

### Simulation parameters

The rover has to find two spots that are good for placing command centers, this means two spots that have three minerals in a given radius.

Minerals are used to create more rovers, so having the command center near minerals makes building rovers more easier.

If the explorer rover dosen't find these spots in first 250 steps, it's a timeout.

If he does, they keep track of found minerals near command center, and go back and forth with resources that are transformed into rovers once they are enough.

# Short Video Demo

https://youtu.be/qq__IdUFiCo
