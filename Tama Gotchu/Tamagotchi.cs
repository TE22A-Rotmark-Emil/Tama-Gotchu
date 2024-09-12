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

    private string TrimName(string name){
        name = name.Replace("1", "");
        name = name.Replace("2", "");
        name = name.Replace("3", "");
        name = name.Replace("4", "");
        name = name.Replace("5", "");
        name = name.Replace("6", "");
        name = name.Replace("7", "");
        name = name.Replace("8", "");
        name = name.Replace("9", "");
        name = name.Replace("0", "");
        name = name.Replace("§", "");
        name = name.Replace("!", "");
        name = name.Replace("\"", "");
        name = name.Replace("#", "");
        name = name.Replace("¤", "");
        name = name.Replace("%", "");
        name = name.Replace("&", "");
        name = name.Replace("/", "");
        name = name.Replace("(", "");
        name = name.Replace(")", "");
        name = name.Replace("=", "");
        name = name.Replace("?", "");
        name = name.Replace("`", "");
        name = name.Replace("´", "");
        name = name.Replace("'", "");
        return name;
    }

    public void DecideName(){
        string tamagotchiName = "";
        Console.WriteLine("Name your Tamagotchi!");
        do{
            tamagotchiName = Console.ReadLine();
            tamagotchiName = TrimName(tamagotchiName);
            if (tamagotchiName.Length < 2){
                Console.WriteLine("tamagotchi name must be 3 characters or longer");
            }
            else if (tamagotchiName.Length > 16){
                Console.WriteLine("tamagotchi name must not exceed 16 characters");
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