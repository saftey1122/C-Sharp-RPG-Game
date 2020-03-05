using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game_Improved
{
    class Player
    {
        private string name;
        public string Name
        {
            get
            {
                return name != null ? name : "Name not set";
            }
            set
            {
                name = value;
            }
        }
        private decimal lvl = 1;
        public decimal Lvl
        {
            get
            {
                return lvl;
            }
            set
            {
                lvl = value;
            }
        }
        private decimal xpthreshhold = 10;
        public decimal XPthreshhold
        {
            get
            {
                return xpthreshhold;
            }
            set
            {
                xpthreshhold = value;
            }
        }
        private decimal currentxp = 0;
        public decimal currentXP
        {
            get { return currentxp; }
            set { currentxp = value; }
        }
        private decimal defense = 1;
        public decimal Defense
        {
            get
            {
                return defense;
            }
            set
            {
                defense = value;
            }
        }
        private decimal attack = 1;
        public decimal Attack
        {
            get
            {
                return attack;
            }
            set
            {
                attack = value;
            }
        }
        private decimal basemaxhp = 10;
        public decimal maxhp
        {
            get { return basemaxhp; }
            set { basemaxhp = value; }
        }
        private decimal hp = 10;
        public decimal HP
        {
            get
            {
                return hp;
            }
            set
            {
                hp = value;
            }
        }
        public bool currentEncounter = false;

        public void listStats()
        {
            Console.WriteLine("{0} stats:", Name);
            Console.WriteLine("Attack: {0}", Attack);
            Console.WriteLine("Current HP: {0}", HP);
            Console.WriteLine("Current Lvl: {0}", Lvl);
            if (currentXP > 0) { Console.WriteLine("Current XP: {0}", currentXP); }
            if (XPthreshhold > 0) { Console.WriteLine("XP Needed to lvl up: {0}", XPthreshhold - currentXP); }
            Console.WriteLine("Max HP: {0}", maxhp);
            Console.WriteLine("Defense: {0}", Defense);
        }

        public decimal basicAttack(Player target)
        {
            decimal newDamage = Attack / target.Defense;
            target.HP -= newDamage;
            return newDamage;
        }
        public void levelUp(Player ply)
        {
            ply.Lvl = ply.Lvl + 1;
            ply.XPthreshhold = ply.XPthreshhold * Lvl;
            ply.maxhp = ply.maxhp * Lvl;
            ply.Attack = ply.Attack * Lvl;
            ply.Defense = ply.Defense * Lvl;
        }
    }
    class Monster : Player
    {
        public void MonstStats()
        {
            Random ranName = new Random();
            string[] ranNames =
            {
                "Doog",
                "Oorg",
                "Rugge",
                "Yoorf",
            };
            Name = ranNames[ranName.Next(0, ranNames.Length)];
            currentXP = -1;
            XPthreshhold = -1;
        }
        private int xpworth = 1;
        public int MAX_ATTACKS = 2;
        public int XPworth
        {
            get { return xpworth; }
        }
        public decimal monstSpecialAttack(Player target)
        {
            target.HP -= Attack;
            HP += maxhp / 10; //Attack that ignored defense and heals the monster for 10% max hp
            return Attack;
        }

        public void monstTurn(int choice, Player target)
        {
            switch (choice)
            {
                case 1:
                    decimal damageTaken = basicAttack(target);
                    Console.WriteLine("The monster attacked you for {0} damage.", damageTaken);
                    Console.WriteLine("");
                    break;
                case 2:
                    decimal damageTaken2 = monstSpecialAttack(target);
                    Console.WriteLine("The monster penetrates your armour dealing {0} damage and healing for {1} HP!", damageTaken2, maxhp / 10);
                    Console.WriteLine("");
                    break;
            }
        }
    }
    class Character : Player
    {
        public void charSpecialAttack(Player target)
        {
            decimal newDamage = Attack * 2;
            target.HP -= newDamage; // Critical strike (2x damage) that ignores defense
            Console.WriteLine("You critically hit the target for {0} damage!", newDamage);
            Console.WriteLine("");
        }

        public void charRiskAttack(Player target)
        {
            Random attackRan = new Random();
            int attackChance = attackRan.Next(1, 9);
            if (attackChance <= 6)
            {
                Console.WriteLine("You missed your attack!");
                Console.WriteLine("");
            }
            else
            {
                decimal newDamage = Attack + target.HP;
                target.HP -= newDamage;
                Console.WriteLine("You execute your target for a whopping {0} damage.", newDamage);
                Console.WriteLine("");
            }
        }

        public int Choice()
        {
            bool correctInput = true;
            int choice = 0;
            while (correctInput)
            {
                Console.WriteLine("Please type a number corrosponding to the action you'd like to take.");
                Console.WriteLine("1) Basic Attack");
                Console.WriteLine("2) Special Attack");
                Console.WriteLine("3) Risky Attack");
                bool test = int.TryParse(Console.ReadLine(), out choice);
                if (!test || choice > 3 || choice <= 0)
                {
                    Console.WriteLine("Please pick a valid choice.");
                    Console.WriteLine("");
                    continue;
                }
                if (choice == 1 || choice == 2 || choice == 3)
                {
                    break;
                }
            }
            Console.Clear();
            return choice;
        }

        public void charTurn(int choice, Monster target)
        {
            switch (choice)
            {
                case 1:
                    basicAttack(target);
                    decimal damageTaken = basicAttack(target);
                    Console.WriteLine("You attacked the monster for {0} damage.", damageTaken);
                    Console.WriteLine("");
                    break;
                case 2:
                    charSpecialAttack(target);
                    break;
                case 3:
                    charRiskAttack(target);
                    break;
            }
        }
    }
    class Encounter
    {
        private int xpGained;
        public void isCharDead(Character character)
        {
            if (character.HP <= 0)
            {
                Console.Clear();
                Console.WriteLine("You died.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
        public void printEncStats(Player P1, Player P2)
        {
            P1.listStats();
            Console.WriteLine("");
            P2.listStats();
            Console.WriteLine("");
        }
        public void actualEncounter(Character charc, Monster monst)    
        {
            while (charc.HP > 0 && monst.HP > 0)
            {
                printEncStats(charc, monst);
                charc.charTurn(charc.Choice(), monst);
                if (monst.HP > 0)
                {
                    Random monstAttackChance = new Random();
                    monst.monstTurn(monstAttackChance.Next(1, monst.MAX_ATTACKS + 1), charc);
                    isCharDead(charc);
                }
            }
            charc.currentEncounter = false;
            Random randomXPBoost = new Random();
            xpGained = monst.XPworth + randomXPBoost.Next(0, 4);
            Console.WriteLine("The monster was killed! You earnt {0} xp!", xpGained);
            Console.WriteLine("You were healed fully for killing the monster.");
            charc.HP = charc.maxhp;
            if (charc.currentXP + xpGained >= charc.XPthreshhold)
            {
                charc.levelUp(charc);
                charc.currentXP = 0;
                Console.WriteLine("You gained a level, you're now level {0}", charc.Lvl);
            }
            else
            {

                charc.currentXP = charc.currentXP + xpGained;
            }
            Console.ReadKey();
            Console.Clear();
        }
    }
}
