# Unity Medical Simulation Game  

Welcome to the Unity Medical Simulation Game, a comprehensive project designed to simulate medical scenarios and provide players with an interactive and educational experience. This project includes various features tailored to different aspects of medical training, such as patient care, emergency response, and surgical skills.  

## Key Features  

### Game Startup Manager  
- Initializes the game, ensuring that multiple instances are not created and the game starts smoothly.  

### Data Management  
- **DataSender**: Handles network requests to send player data, game records, and level details to a server.  
- **Player Data**: Manages player information, such as name and IP address.  
- **Game Record Data**: Tracks game statistics, including level completion and timing data.  

### Gameplay Mechanics  
- **GameTimer**: Manages in-game timers for levels, tracks elapsed time, and determines success or failure based on time limits.  
- **BabySimulation**: Simulates a baby's vital signs, requiring players to maintain a stable heart rate and respiratory rate.  
- **Level4Result**: Manages outcomes for level 4, including tracking incorrect actions.  

### User Interface  
- **GameResultDisplay**: Shows the results of the game, including level-specific outcomes and overall evaluations.  
- **LongPressButton**: Simulates long-press mechanics, often used in medical procedures like chest compressions.  
- **SliderGame**: Implements a slider-based mechanic, possibly for adjusting medical equipment or monitoring vital signs.  

### Scene Management  
- **SceneLoader**: Loads scenes after a specified delay, allowing smooth transitions between different parts of the game.  
- **SceneSwitcher**: Quickly switches between scenes based on in-game events.  

### Audio and Visual Feedback  
- **PlayerMovement**: Manages player movement and provides audio feedback for walking.  
- **CameraFollow**: Ensures the camera follows the player, maintaining a consistent gameplay perspective.  

### Interactive Dialogues  
- **Dialog**: Manages in-game dialogues, queuing and displaying text for a narrative experience.  
- **DialogueManager**: Triggers dialogues during specific game events.  

### Animation and Graphics  
- **ScaleAnimation**: Applies scaling animations to objects for enhanced visual effects.  
- **TimeDisplay**: Updates and shows real-time clock information on the screen.  

### Utility Scripts  
- **NumberController**: Controls numerical values over time, useful for scenarios involving increasing or decreasing metrics.  
- **ObjectDisplayController**: Manages game object visibility, showing or hiding objects based on game progress.  
- **TimerController**: Handles timers and provides a visual representation of time elapsed.  

