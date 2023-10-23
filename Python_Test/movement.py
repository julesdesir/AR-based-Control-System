import math

class BlueBall():

    def __init__(self):
        self.x = 0
        self.y = 0
    
    def go_to_objective(self, x_objective, y_objective, delta_time):

        norm = math.sqrt(x_objective**2+y_objective**2)

        x_objective_normalized = x_objective/norm
        y_objective_normalized = y_objective/norm

        self.x += x_objective_normalized*delta_time
        self.y += y_objective_normalized*delta_time

