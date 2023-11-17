import pygame

class Controller():
    def __init__(self):
        # Initialization
        pygame.init()
        pygame.joystick.init()
        
        # Collect controls from the controller
        self.joystick = pygame.joystick.Joystick(0)
        self.number_axis = self.joystick.get_numaxes()
        self.number_buttons = self.joystick.get_numbuttons()
        self.number_hats = self.joystick.get_numhats()        
      
        # Dictionaries to collect the controller's information
        self.dict_axis_values = dict()
        self.dict_buttons_values = dict()        
        self.dict_hats_values = dict()        

        for i in range(self.number_axis):
            axis_value = self.joystick.get_axis(i)
            self.dict_axis_values[f"Axe {i}"] = axis_value
            
        for i in range(self.number_buttons):
            button_value = self.joystick.get_button(i)
            self.dict_buttons_values[f"Button {i}"] = button_value

        for i in range(self.number_hats):
            hat_value = self.joystick.get_hat(i)
            self.dict_buttons_values[f"Hat {i}"] = hat_value           

    # Functions to collect controller's information
    def get_axis(self, i): # Axis
        return self.joystick.get_axis(i)
    
    def get_button(self, i): # Button 
        return self.joystick.get_button(i)
    
    def get_hat(self, i): # Hat
        return self.joystick.get_hat(i)
    
    def get_all_axis(self): # All axis
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()

        for i in range(self.number_axis):
            axis_value = self.get_axis(i)
            self.dict_axis_values[f"Axe {i}"] = axis_value
        return self.dict_axis_values
    
    def get_all_buttons(self): # All buttons
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()

        for i in range(self.number_buttons):
            button_value = self.get_button(i)
            self.dict_buttons_values[f"Button {i}"] = button_value
        return self.dict_buttons_values
     
    def get_all_hats(self): # All hats
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()

        for i in range(self.number_hats):
            hat_value = self.get_hat(i)
            self.dict_hats_values[f"Hat {i}"] = hat_value
        return self.dict_hats_values
