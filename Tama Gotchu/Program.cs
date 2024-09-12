Tamagotchi pT = new();
pT.DecideName();

while (pT.GetAlive() == true){
    pT.Tick();
    
}