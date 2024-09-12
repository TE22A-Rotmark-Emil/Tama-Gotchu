public class Tamagotchi{
    private int hunger;
    private int boredom;
    private List<string> words = new(){""};
    private bool isAlive = true;
    public string Name;

    public Tamagotchi(){
        hunger = Random.Shared.Next(0, 3);
        boredom = Random.Shared.Next(0, 3);
    }

    public void DecideName(){
        string tamagotchiName = "";
        Console.WriteLine("Name your Tamagotchi!");
        do{
            tamagotchiName = Console.ReadLine();
            if (tamagotchiName.Length < 2){
                Console.WriteLine("tamagotchi name must be 3 characters or longer");
            }
            else if (tamagotchiName.Length > 16){
                Console.WriteLine("tamagotchi name must not exceed 16 characters");
            }
            else{
                break;
            }
        } while (tamagotchiName.Length < 2 && tamagotchiName.Length > 16);
        Name = tamagotchiName;
    }

    public void Feed(){

    }

    public void Hi(){

    }

    public void Teach(string word){

    }
    
    public void Tick(){

    }

    public void PrintStats(){

    }

    public bool GetAlive(){
        return isAlive;
    }

    private void ReduceBoredom(){
        
    }
}