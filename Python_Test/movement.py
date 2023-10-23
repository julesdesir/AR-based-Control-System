import math

class BlueBall():

    def __init__(self):
        self.x = 0
        self.y = 0
    
    def go_to_objective(self, x_objective, y_objective, delta_time):

        norm = math.sqrt((x_objective-self.x)**2+(y_objective-self.y)**2)

        x_speed = (x_objective-self.x)/norm
        y_speed = (y_objective-self.y)/norm

        self.x += x_speed*delta_time
        self.y += y_speed*delta_time

