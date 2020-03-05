using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game_Improved
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please write your name: ");
            string userInput = Console.ReadLine();
            Character Player1 = new Character();
            Monster Monster1 = new Monster();
            Player1.Name = userInput;
            Console.WriteLine("Welcome to the game {0}.", Player1.Name);
            Console.WriteLine("Press any key to start.");
            Console.ReadKey();
            Console.Clear();
            for (; ; )
            {
                Console.Write("{0}: ", Player1.Name);
                userInput = Console.ReadLine();
                if (userInput[0] == '/')
                {
                    userInput = userInput.ToLower();
                }
                if (userInput == "/exit")
                {
                    Environment.Exit(0);
                }
                else if (userInput == "/encounter")
                {
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    Console.Clear();
                    Monster1 = startEncounter();
                    Encounter fight = new Encounter();
                    Console.WriteLine("Encounter started.");
                    Console.WriteLine("");
                    fight.actualEncounter(Player1, Monster1);
                    Player1.currentEncounter = true;
                }
                else if (userInput == "/stats")
                {
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    Player1.listStats();
                }
                else if (userInput == "/help")
                {
                    Console.WriteLine("List of available commands: ");
                    Console.WriteLine("/encounter - to start a fight.");
                    Console.WriteLine("/exit - to leave the game.");
                    Console.WriteLine("/stats - to list your character's statistics.");
                }
                else
                {
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    Console.WriteLine(Player1.Name + ": " + userInput);
                }
            }
        }
        public static Monster startEncounter()
        {
            Monster Monster2 = new Monster();
            Monster2.MonstStats();
            return Monster2;
        }
    }
}
