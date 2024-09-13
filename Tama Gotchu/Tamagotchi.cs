using System.Drawing;
using System.Linq;

public class Tamagotchi
{
    #region Definitions
    private int hunger; // variable that, if it reaches "death," will kill the entity. increments by 1 every turn (except where otherwise specified)
    private int death; // variable that decides when an entity dies. is set at random below
    private bool satiated = false; // explained in "Feed" method
    private int consecutiveFeed = 0;
    private int boredom; // variable that, if it reaches "death," will kill the entity. increments by 1 every turn (except where otherwise specified)
    private int neglectedBoredom = 0; // variable that punishes the player for not doing fun activities with their entity (talking.) if this variable is above 3, boredom will increment twice (except where otherwise specified)
    private string lastAction = "nothing";
    private List<string> newWords = new();
    private List<string> oldWords = new();
    private bool isAlive = true; // check for if the entity is alive. if it is not, you lose the game
    public string Name; // simple name for the entity. has no use other tha sentimental value (you will cry when your entity dies, etc.)
    private string timeBorn; // checks when the entity "was born" (created) - allows for the gravestone mechanic further down
    private string timeDied; // ditto ^
    #endregion

    #region Construct
    public Tamagotchi()
    {
        hunger = Random.Shared.Next(0, 3); // following variables are randomised to add flavour to the game
        death = Random.Shared.Next(10, 15);
        boredom = Random.Shared.Next(0, 3);
        timeBorn = DateTime.Now.ToString(); // used to make Death a bit more realistic by showing a gravestone (initially created here since this is when the character is created/"born")
    }
    #endregion

    #region Methods
    private string TrimWord(string word, bool capitalLetters, bool isInt)
    { // method used to remove junk from a player's responses
        char[] integers = "0123456789".ToArray();
        char[] junkies = "§½!\"#¤%&/()=?`´' \t,.-;:_<>|¨^*\\@£$€{}[]".ToArray();
        if (!isInt)
        {
            foreach (char integer in integers)
            {
                word = word.Replace(integer.ToString(), "");
            }
        }
        foreach (char junk in junkies)
        {
            word = word.Replace(junk.ToString(), "");
        }
        if (capitalLetters == false)
        { // makes sure that the answer isn't case sensitive in instances where that is important. In other places this is not important (such as when creating the name of your character)
            word = word.ToLower();
        }
        return word;
    }

    public void DecideName()
    { // method used to create your character's name
        string tamagotchiName;
        Console.WriteLine("Name your Tamagotchi!"); // gets called once since it is outside of the loop
        do
        {
            tamagotchiName = Console.ReadLine();
            tamagotchiName = TrimWord(tamagotchiName, true, false); // trims, read above
            if (tamagotchiName.Length < 3 && tamagotchiName.ToLower() != "wu")
            { // explained below. some interactions could look excessively silly with tiny names
                Console.WriteLine("tamagotchi name must be 3 characters or longer");
            }
            else if (tamagotchiName.Length > 16)
            { // explained below. some interactions could look excessively silly with long names
                Console.WriteLine("tamagotchi name must not exceed 16 characters");
            }
        } while (tamagotchiName.ToLower() != "wu" && tamagotchiName.Length < 3 || tamagotchiName.Length > 16); // forces the loop until a name is between 3-16 characters
        Name = tamagotchiName; // sets the character's name to the chosen name of the player
    }

    public void Feed()
    { // "feed" method. reduces the entity's hunger by a flat 3 (effectively 2 since the game tick removes 1)
        if (hunger - (1+consecutiveFeed) <= 0)
        { // here to ensure that the hunger variable never reaches a negative number
            hunger = 0;
            consecutiveFeed++;
            satiated = true; // variable used to make feeding your pet less punishing. if you reach 0 hunger, you stay at 0 hunger for one turn.
            Console.WriteLine(""); // blank space to make it easier to read
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Name} is no longer hungry!");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("");
        }
        else
        { // runs if above statement returns false, such as if the entity's hunger is at 4
            Console.WriteLine($"{(hunger - 1+consecutiveFeed) <= 0}");
            hunger -= 1+consecutiveFeed;
            consecutiveFeed++;
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"{Name} feels less hungry!");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("");
        }
        lastAction = "feed";
        neglectedBoredom++; // this does not count as "doing something fun" with your entity, so a variable "neglectedboredom" is incremented. read definitions for explanation
    }

    public void Talk()
    { // only boredom reducing element (officially, since "teach" can randomly make the entity less bored)
        if (newWords.Count == 0 && oldWords.Count == 0)
        { // if you have never taught your entity a word, this will automatically run
            string answer;
            Console.WriteLine($"{Name} cannot speak! Would you like to teach them some words instead?");
            do
            {
                answer = Console.ReadLine();
                answer = TrimWord(answer, false, false);
                if (answer == "no")
                {
                    Console.WriteLine("lmao???"); // lmao
                }
                if (answer != "yes" && answer != "no")
                {
                    Console.WriteLine("Write yes or no");
                }
            } while (answer != "yes" && answer != "no");
            if (answer == "yes")
            {
                Teach(); // automatically runs the teach method since the talk method's purpose is not to teach the entity a word
            }
        }
        else
        { // if the entity knows any word at all, this will automatically run
            ReduceBoredom(Random.Shared.Next(2, 3)); // has a random number because I'm just evil like that
            neglectedBoredom = 0; // resets neglected boredom, read the definitions section for an explanation as to what this does
            consecutiveFeed = 0;
            lastAction = "talk";
            string selectedWord;
            if (newWords.Count > 0){
                selectedWord = newWords[Random.Shared.Next(0, newWords.Count())];
            }
            else{
                selectedWord = oldWords[Random.Shared.Next(0, oldWords.Count())];
            }
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Name} says {selectedWord}");
            if (oldWords.Contains(selectedWord)){
                Console.WriteLine($"{Name} has gotten tired of saying {selectedWord}");
                boredom += 2;
            }
            else{
                newWords.Remove(selectedWord);
                oldWords.Add(selectedWord);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("");
        }
    }

    public void Teach()
    { // teaches the entity a word of your chosing. literally anything. you can type anything. I have no safeguards
        string word;
        Console.WriteLine($"What word would you like to teach {Name}?");
        do
        {
            word = Console.ReadLine();
            word = TrimWord(word, false, false);
            if (word.Length < 1 || word.Length > 45)
            { // maximum word length is based on pneumonoultramicroscopicsilicovolcanoconiosis
                Console.WriteLine("Invalid Word");
            }
        } while (word.Length < 1 || word.Length > 45);
        ReduceBoredom(Random.Shared.Next(1)); // randomly makes you GAIN boredom because I'm kind of evil like that. randomly reduces boredom because I'm nice and kind like that
        consecutiveFeed = 0;
        lastAction = "teach";
        TeachWord(word); // read "teachword" method explanation
    }

    private void TeachWord(string word) {// adds the word you typed to the entity's word list
        newWords.Add(word);
        Console.WriteLine($"{Name} can now say {word}");
    }

    public void Tick()
    { // game loop
        PrintStats(); // automatically sends all information in print stats
        if (GetAlive() == false)
        { // if the method getalive returns false, you met one of the lose conditions
            Console.WriteLine($"{Name} has died. Paramedics arrived at the scene but it was too late.");
            Console.WriteLine("They groveled. Screaming. 'Someone, please! Help me!' But no one could hear them.");
            if (hunger >= death) { Console.Write("They were too hungry to move. "); }
            if (boredom >= death) { Console.Write("They were too bored to see. "); }
            Console.WriteLine();
            Console.WriteLine($"- | {Name.ToUpper()} | -"); // GRAVESTONE MECHANIC
            timeDied = DateTime.Now.ToString(); // explained in definitions
            Console.WriteLine($"{timeBorn} - {timeDied}");
            Console.ReadLine();
        }
        else
        { // returns true if you aren't dead
            Console.WriteLine("What would you like to do?");
            Console.WriteLine($"1. Feed {Name}");
            Console.WriteLine($"2. Talk to {Name}");
            Console.WriteLine($"3. Teach {Name} something");
            Console.WriteLine($"4. Do nothing");
            string answer;
            List<string> validAnswers = new(){ // list of words you can type that continue the game loop
                "1", "2", "3", "4",
                "one", "two", "three", "four",
                "feed", "talk", "teach", "nothing",
                "food", "speak", "teacher", "no"};
            do
            {
                answer = Console.ReadLine();
                answer = TrimWord(answer, false, true);
                if (!validAnswers.Contains(answer))
                {
                    answer = TrimWord(answer, false, false);
                    if (!validAnswers.Contains(answer))
                    {
                        Console.WriteLine("Please write a valid response");
                    }
                }
            } while (!validAnswers.Contains(answer));
            if (answer == "1" || answer == "one" || answer == "feed" || "answer" == "food")
            {
                Feed();
            }
            else if (answer == "2" || answer == "two" || answer == "talk" || answer == "speak")
            {
                Talk();
            }
            else if (answer == "3" || answer == "three" || answer == "teach" || answer == "teacher")
            {
                Teach();
            }
            else
            { // only returns true if you choose the 4th option, aka "nothing" - increments boredom neglection because you aren't talking
                lastAction = "nothing";
                neglectedBoredom++;
                consecutiveFeed = 0;
            }
        }

        if (neglectedBoredom != 0 && lastAction != "talk") { boredom++; } // increments unless you just did an anti-boredom task, e.g. talking
        if (neglectedBoredom > 2 && boredom + 1 != death) { boredom += neglectedBoredom-1; } // increments if it wouldn't kill you and if your neglect is 3 or higher

        if (!satiated && lastAction != "feed") { hunger++; } else { satiated = false; } // increments if you are not satiated (you reached 0 hunger last turn.) otherwise removes satiated
    }

    public void PrintStats()
    { // prints all relevant stats so that the player can see them and gauge a strategy
        Console.WriteLine($"--- {Name} ---");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("Hunger: ");
        Severity(hunger); // read severity below
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Boredom: ");
        Severity(boredom); // read severity below
        Console.ForegroundColor = ConsoleColor.Gray;

        void Severity(int value)
        { // "severity" is an inline method which only runs in PrintStats. it gauges how close you are to a death value in all relevant stats, and chooses an appropriate colour and message for them.
            if (value < death)
            {
                switch (value)
                {
                    case int n when n >= 0.75 * death: // if your entity has a value that is between 75% and 99% of the death value
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{value}/{death}");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(" (CRITICAL)");
                        break;
                    case int n when n < 0.75 * death && n >= 0.4 * death: // if your entity has a value between 74% and 40% of the death value
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{value}/{death}");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(" (NORMAL)");
                        break;
                    case int n when n < 0.4 * death && n > 0: // if your entity has a value between 1% and 39%
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{value}/{death}");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine(" (GOOD)");
                        break;
                    case 0: // if your entity's value is literally 0
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"{value}/{death}");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine(" (EXCELLENT)");
                        break;
                    default: // secure just in case it ouputs an invalid value
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine($"{value} (WHAT)");
                        break;
                }
            }
            else
            { // runs if you have a value equal to (or greater) than the death value
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(value);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" (DEATH)");
            }
        }
    }

    public bool GetAlive()
    { // checks if syour entity is alive with the relevant values
        if (boredom >= death || hunger >= death) { isAlive = false; }
        return isAlive;
    }

    private void ReduceBoredom(int amount)
    { // reduces boredom value based on the task completed
        if (boredom - (amount+neglectedBoredom-1) >= 0)
        {
            boredom -= amount+neglectedBoredom-1;
            if (hunger++ == death && neglectedBoredom > 1 && consecutiveFeed < 0){
                hunger++;
            }
            else if (hunger + (neglectedBoredom+1) >= death && neglectedBoredom > 2 && consecutiveFeed < 0){
                hunger = death-1;
            }
        }
        else
        { // failsafe just in case the sum of boredom - amount is a negative value
            boredom = 0;
        }
    }
    #endregion
}