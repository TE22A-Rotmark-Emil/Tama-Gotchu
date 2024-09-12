public class Tamagotchi{
    private int hunger;
    private int death;
    private bool satiated = false;
    private int boredom;
    private int neglectedBoredom = 0;
    private List<string> words = new();
    private bool isAlive = true;
    public string Name;
    private string timeBorn;
    private string timeDied;

    public Tamagotchi(){
        hunger = Random.Shared.Next(0, 3);
        death = Random.Shared.Next(10, 15);
        boredom = Random.Shared.Next(0, 3);
        timeBorn = DateTime.Now.ToString();
    }

    private string TrimName(string name, bool capitalLetters, bool isInt){
        if (!isInt){
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
        }
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
            tamagotchiName = TrimName(tamagotchiName, true, false);
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
            Console.WriteLine($"{Name} feels less hungry!");
        }
        ReduceBoredom(1);
        neglectedBoredom++;
    }

    public void Hi(){
        if (words.Count == 0){
            string answer;
            Console.WriteLine($"{Name} cannot speak! Would you like to teach them some words instead?");
            do{
                answer = Console.ReadLine();
                answer = TrimName(answer, false, false);
                if (answer == "no"){
                    Console.WriteLine("lmao???");
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
            ReduceBoredom(Random.Shared.Next(2, 3));
            neglectedBoredom = 0;
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Name} says {words[Random.Shared.Next(0, words.Count())]}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("");
        }
        Console.WriteLine(words.Count);
    }

    public void Teach(){
        string word;
        Console.WriteLine($"What word would you like to teach {Name}?");
        do{
            word = Console.ReadLine();
            word = TrimName(word, false, false);
            if (word.Length < 1 && word.Length > 45){
                Console.WriteLine("Invalid Word");
            }
        } while (word.Length < 1 && word.Length > 45);
        ReduceBoredom(Random.Shared.Next(-1, 2));
        neglectedBoredom++;
        TeachWord(word);
    }

    private void TeachWord(string word) {words.Add(word); Console.WriteLine($"{Name} can now say {word}");}
    
    public void Tick(){
        PrintStats();
        if (GetAlive() == false){
            Console.WriteLine($"{Name} has died. Paramedics arrived at the scene but it was too late.");
            Console.WriteLine("They groveled. Screaming. 'Someone, please! Help me!' But no one could hear them.");
            if (hunger > 9) {Console.Write("They were too hungry to move. ");}
            if (boredom > 9) {Console.Write("They were too bored to see. ");}
            Console.WriteLine();
            Console.WriteLine($"- | {Name.ToUpper()} | -");
            timeDied = DateTime.Now.ToString();
            Console.WriteLine($"{timeBorn} - {timeDied}");
            Console.ReadLine();
        }else{
            Console.WriteLine("What would you like to do?");
            Console.WriteLine($"1. Feed {Name}");
            Console.WriteLine($"2. Talk to {Name}");
            Console.WriteLine($"3. Teach {Name} something");
            Console.WriteLine($"4. Do nothing");
            string answer;
            List<string> validAnswers = new(){
                "1", "2", "3", "4",
                "one", "two", "three", "four",
                "feed", "talk", "teach", "nothing",
                "food", "speak", "teacher", "no"};
            do{
               answer = Console.ReadLine();
               answer = TrimName(answer, false, true);
               if (!validAnswers.Contains(answer)){
                    answer = TrimName(answer, false, false);
                    if (!validAnswers.Contains(answer)){
                        Console.WriteLine("Please write a valid response");
                    }
               }
            }while(!validAnswers.Contains(answer));
            if (answer == "1" || answer == "one" || answer == "feed" || "answer" == "food"){
                Feed();
            }
            else if (answer == "2" || answer == "two" || answer == "talk" || answer == "speak"){
                Hi();
            }
            else if (answer == "3" || answer == "three" || answer == "teach" || answer == "teacher"){
                Teach();
            }
            else{
                neglectedBoredom++;
            }
        }

        boredom++;
        if (neglectedBoredom > 2 && boredom + 1 != death) {boredom++;}

        if (!satiated) {hunger++;} else {satiated = false;}
    }

    public void PrintStats(){
        Console.WriteLine($"--- {Name} ---");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("Hunger: ");
        Severity(hunger);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Boredom: ");
        Severity(boredom);
        Console.ForegroundColor = ConsoleColor.Gray;

        void Severity(int value){
            if (value < death){
                switch(value){
                    case int n when n >= Convert.ToInt32(0.75*death):
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(value);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(" (CRITICAL)");
                    break;
                    case int n when n < 0.75*death && n >= 0.4*death:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(value);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(" (NORMAL)");
                    break;
                    case int n when n < 0.4*death && n > 0:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(value);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(" (GOOD)");
                    break;
                    case 0:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(value);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine(" (EXCELLENT)");
                    break;
                    default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"{value} (WHAT)");
                    Console.WriteLine($"{death*0.7} {death*0.4}");
                    break;
                }
            }
            else{
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(value);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(" (DEATH)");
            }
        }
    }

    public bool GetAlive() {
        if (boredom >= death || hunger >= death) {isAlive = false;}
        return isAlive;
    }

    private void ReduceBoredom(int amount){
        boredom -= amount;
    }
}