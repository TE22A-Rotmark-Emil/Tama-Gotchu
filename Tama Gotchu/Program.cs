Tamagotchi pT = new(); // instantiates a Tamagotchi entity, named "pT." All explanations for this class are in the class.
pT.DecideName(); // calls the DecideName method of the Tamagotchi class.

while (pT.GetAlive() == true){ // runs until the entity dies
    pT.Tick(); // calls "tick" - the Method that makes the game run
}
pT.Tick(); // is called one last time since the entity dying ends the loop