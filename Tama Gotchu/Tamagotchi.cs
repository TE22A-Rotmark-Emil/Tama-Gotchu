public class Tamagotchi{
    private int hunger;
    private bool satiated = false;
    private int boredom;
    private List<string> words = new(){""};
    private bool isAlive = true;
    public string Name;

    public Tamagotchi(){
        hunger = Random.Shared.Next(0, 3);
        boredom = Random.Shared.Next(0, 3);
    }

    private string TrimName(string name, bool capitalLetters){
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
        if (capitalLetters == false){
            name = name.ToLower();
        }
        return name;
    }

    public void DecideName(){
        string tamagotchiName = "";
        Console.WriteLine("Name your Tamagotchi!");
        do{
            tamagotchiName = Console.ReadLine();
            tamagotchiName = TrimName(tamagotchiName, true);
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
        if (hunger - 3 < 0){
            hunger = 0;
            satiated = true;
            Console.WriteLine($"{Name} is no longer hungry!");
        }
        else{
            hunger -= 3;
            Console.WriteLine($"{Name} feels better! Their hunger is now at {hunger}");
        }
    }

    public void Hi(){
        if (words.Count == 0){
            string answer;
            Console.WriteLine($"{Name} cannot speak! Would you like to teach them some words instead?");
            do{
                answer = Console.ReadLine();
                answer = TrimName(answer, false);
                if (answer == "no"){
                    Console.WriteLine("lmao???");
                    Tick();
                }
                if (answer != "yes" && answer != "no"){
                    Console.WriteLine("Write yes or no");
                }
            }while(answer != "yes" && answer != "no");
            if (answer == "yes"){
                Teach();
            }
        }
        else{

        }
    }

    public void Teach(){
        string word;
        Console.WriteLine($"What word would you like to teach {Name}?");
        do{
            word = Console.ReadLine();
            word = TrimName(word, false);
            if (word.Length < 1 && word.Length > 45){
                Console.WriteLine("Invalid Word");
            }
        } while (word.Length < 1 && word.Length > 45);
        TeachWord(word);
    }

    private void TeachWord(string word) {words.Add(word); Console.WriteLine($"{Name} can now say {word}");}
    
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