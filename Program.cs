using System;

namespace TextAdventure
{
    class Program
    {
        static void Main(string[] args)
        {

            /*  GAME COMMANDS
             *  
             *  INVENTORY
             *  REST
             *  STATUS
             *  
             */

            // Player Stats
            int Player_HP_Current = 20;
            int Player_HP_Full = 100;
            double Player_EXP_Current = 20;
            double Player_EXP_Full = 100;
            int Player_Level_Current = 1;
            int Resting_Time;

            // Move Options
            int Player_Move_Left_Right = 10;
            int Player_Move_Up_Down = 10;

            // Game Options
            int Game_Over = 0;
            int error = 0;

            // Player Attributes
            int Armour_Bonus = 0;
            //int Speech_Bonus; higher dice roll chance
            int Attack_Bonus = 0;
            int Range_Bonus = 0;
            int Casting_Bonus = 0;
            string Player_Weapon = "Fist";
            int Player_Attack = 1;

            // Player / Group
            string Player_Name = "Player";
            string Player_Class = "";
            int Player_Group = 1;

            string Current_Command;
            string Player_Status = "";

            // Enemy
            string Enemy_Name = "";
            int Enemy_HP_Current = 0;
            int Enemy_HP_Full = 0;
            int Enemy_Attack = 0;


            // Dice Roll
            int Dice_Rolled;
            Random Dice_Roll = new Random();


            // Inventory
            string[,] Inventory = new string[10, 20];
            string[,] Spells = new string[10,20];


            // Player Creation
            Console.Clear();
            Console.SetCursorPosition(3, 3);
            Console.WriteLine("GAME COMMANDS: ");
            Console.SetCursorPosition(3, 5);
            Console.WriteLine("INVENTORY");
            Console.SetCursorPosition(3, 6);
            Console.WriteLine("REST");
            Console.SetCursorPosition(3, 7);
            Console.WriteLine("STATUS");
            Console.SetCursorPosition(3, 12);
            Console.WriteLine("GAME CONTROLS: ");
            Console.SetCursorPosition(3, 14);
            Console.WriteLine("LEFT: NumPad4");
            Console.SetCursorPosition(3, 15);
            Console.WriteLine("RIGHT: NumPad6");
            Console.SetCursorPosition(3, 16);
            Console.WriteLine("UP: NumPad8");
            Console.SetCursorPosition(3, 17);
            Console.WriteLine("DOWN: NumPad5");
            Console.SetCursorPosition(3, 18);
            Console.WriteLine("ATTACK: SpaceBar");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 15, Console.WindowHeight / 2 - 3);
            Console.Write("What is your name?: ");
            Player_Name = Console.ReadLine();
            do
            {
                Console.Clear();
                Console.SetCursorPosition(3, 3);
                Console.WriteLine("GAME COMMANDS: ");
                Console.SetCursorPosition(3, 5);
                Console.WriteLine("INVENTORY");
                Console.SetCursorPosition(3, 6);
                Console.WriteLine("REST");
                Console.SetCursorPosition(3, 7);
                Console.WriteLine("STATUS");
                Console.SetCursorPosition(3, 12);
                Console.WriteLine("GAME CONTROLS: ");
                Console.SetCursorPosition(3, 14);
                Console.WriteLine("LEFT: NumPad4");
                Console.SetCursorPosition(3, 15);
                Console.WriteLine("RIGHT: NumPad6");
                Console.SetCursorPosition(3, 16);
                Console.WriteLine("UP: NumPad8");
                Console.SetCursorPosition(3, 17);
                Console.WriteLine("DOWN: NumPad5");
                Console.SetCursorPosition(3, 18);
                Console.WriteLine("ATTACK: SpaceBar");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 - 3);
                Console.Write("Choose a class");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 - 1);
                Console.Write("Melee");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2);
                Console.Write("Mage");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 + 1);
                Console.Write("Range");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 + 3);
                Player_Class = Console.ReadLine().ToUpper();
                if (Player_Class != "MELEE" || Player_Class != "MAGE" || Player_Class != "RANGE")
                {
                    error = 0;
                    if (Player_Class == "MELEE")
                    {
                        Attack_Bonus = Attack_Bonus + 8;
                        Armour_Bonus = Armour_Bonus + 10;
                        Casting_Bonus = Casting_Bonus + 6;
                        Range_Bonus = Range_Bonus + 6;
                        Spells[0, 0] = "Smash";
                        Spells[1, 0] = "8";
                        Spells[0, 1] = "Boost_Attack";
                        Spells[1, 1] = "3";
                        Spells[2, 0] = "DAMAGE";
                        Spells[2, 1] = "BOOST";
                    }
                    if (Player_Class == "MAGE")
                    {
                        Armour_Bonus = Armour_Bonus + 6;
                        Casting_Bonus = Casting_Bonus + 10;
                        Attack_Bonus = Attack_Bonus + 6;
                        Range_Bonus = Range_Bonus + 6;
                        Spells[0, 0] = "Flames";
                        Spells[1, 0] = "6";
                        Spells[0, 1] = "Destruction";
                        Spells[1, 1] = "6";
                        Spells[2, 0] = "DAMAGE";
                        Spells[2, 1] = "DAMAGE";
                    }
                    if (Player_Class == "RANGE")
                    {
                        Armour_Bonus = Armour_Bonus + 8;
                        Range_Bonus = Range_Bonus + 10;
                        Casting_Bonus = Casting_Bonus + 6;
                        Attack_Bonus = Attack_Bonus + 6;
                        Spells[0, 0] = "Laser";
                        Spells[1, 0] = "8";
                        Spells[0, 1] = "Enchant_Range";
                        Spells[1, 1] = "3";
                        Spells[2, 0] = "DAMAGE";
                        Spells[2, 1] = "BOOST";
                    }
                }
                else
                {
                    error = 1;
                }

            } while (error == 1);

            // Start of the Game

            do
            {
                // Count Exp
                if (Player_EXP_Current >= Player_EXP_Full)
                {
                    Player_EXP_Current = Player_EXP_Current - Player_EXP_Full;
                    Player_Level_Current++;
                    Player_EXP_Full = Player_EXP_Full * 1.20;
                }
                if (Player_HP_Current > Player_HP_Full)
                {
                    Player_HP_Current = Player_HP_Full;
                }
                if (Player_HP_Current <= 0)
                {
                    Game_Over = 1;
                }

                // User Interface
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                for (int a = 0; a <= 119; a++)
                {
                    Console.SetCursorPosition(a, 0);
                    Console.WriteLine("_");
                }
                Console.SetCursorPosition(5, 1);
                Console.WriteLine("Name: {0}", Player_Name);
                Console.SetCursorPosition(30, 1);
                Console.WriteLine("HP: {0}/{1}", Player_HP_Current, Player_HP_Full);
                Console.SetCursorPosition(60, 1);
                Console.WriteLine("EXP: {0}/{1}", Player_EXP_Current, Player_EXP_Full);
                Console.SetCursorPosition(90, 1);
                Console.WriteLine("Level: {0}", Player_Level_Current);
                for (int a = 0; a <= 119; a++)
                {
                    Console.SetCursorPosition(a, 2);
                    Console.WriteLine("_");
                }
                for (int a = 3; a <= 28; a++)
                {
                    Console.SetCursorPosition(0, a);
                    Console.WriteLine("|");
                    Console.SetCursorPosition(119, a);
                    Console.WriteLine("|");
                }
                Console.SetCursorPosition(0, 28);
                for (int a = 0; a <= 119; a++)
                {
                    Console.SetCursorPosition(a, 28);
                    Console.WriteLine("_");
                }

                // Player Controls
                ConsoleKeyInfo KeyInfo;
                KeyInfo = Console.ReadKey(true);
                switch (KeyInfo.Key)
                {
                    case ConsoleKey.NumPad6:
                        if (Player_Move_Left_Right < 118)
                        {
                            Player_Move_Left_Right++;
                            Console.SetCursorPosition(Player_Move_Left_Right, Player_Move_Up_Down);
                            Console.Write("X");
                            Console.SetCursorPosition(Player_Move_Left_Right - 1, Player_Move_Up_Down);
                            Console.Write(" ");
                        }
                        break;
                    case ConsoleKey.NumPad4:
                        if (Player_Move_Left_Right > 1)
                        {
                            Player_Move_Left_Right--;
                            Console.SetCursorPosition(Player_Move_Left_Right, Player_Move_Up_Down);
                            Console.Write("X");
                            Console.SetCursorPosition(Player_Move_Left_Right + 1, Player_Move_Up_Down);
                            Console.Write(" ");
                        }
                        break;
                    case ConsoleKey.NumPad8:
                        if (Player_Move_Up_Down > 3)
                        {
                            Player_Move_Up_Down--;
                            Console.SetCursorPosition(Player_Move_Left_Right, Player_Move_Up_Down);
                            Console.Write("X");
                            Console.SetCursorPosition(Player_Move_Left_Right, Player_Move_Up_Down + 1);
                            Console.Write(" ");
                        }
                        break;
                    case ConsoleKey.NumPad5:
                        if (Player_Move_Up_Down < 27)
                        {
                            Player_Move_Up_Down++;
                            Console.SetCursorPosition(Player_Move_Left_Right, Player_Move_Up_Down);
                            Console.Write("X");
                            Console.SetCursorPosition(Player_Move_Left_Right, Player_Move_Up_Down - 1);
                            Console.Write(" ");
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        Console.SetCursorPosition(Player_Move_Left_Right, Player_Move_Up_Down);
                        Console.Write("X");

                        Console.SetCursorPosition(Player_Move_Left_Right+1, Player_Move_Up_Down);
                        Console.Write("/");
                        System.Threading.Thread.Sleep(50);
                        Console.SetCursorPosition(Player_Move_Left_Right+1, Player_Move_Up_Down);
                        Console.Write(" ");
                        
                        Console.SetCursorPosition(Player_Move_Left_Right, Player_Move_Up_Down-1);
                        Console.Write("|");
                        System.Threading.Thread.Sleep(50);
                        Console.SetCursorPosition(Player_Move_Left_Right, Player_Move_Up_Down-1);
                        Console.Write(" ");

                        Console.SetCursorPosition(Player_Move_Left_Right-1, Player_Move_Up_Down);
                        Console.Write("/");
                        System.Threading.Thread.Sleep(50);
                        Console.SetCursorPosition(Player_Move_Left_Right-1, Player_Move_Up_Down);
                        Console.Write(" ");
                        break;
                }



                // Add to Inventory
                for (int a = 0; a <= 19; a++)
                {
                    for (int b = 0; b <= 9; b++)
                    {
                        Inventory[b, a] = "...";
                    }
                }


                // Inventory Options
                Current_Command = Console.ReadLine().ToUpper();
                if (Current_Command == "INVENTORY")
                {
                    for (int a = 0; a <= 19; a++)
                    {
                        for (int b = 0; b <= 9; b++)
                        {
                            if (b <= 9)
                            {
                                Console.SetCursorPosition(b * 7 + 3, 5 + a);
                                Console.Write("{0}", Inventory[b, a]);
                            }
                            else { }
                        }
                    }
                    for(int c = 0; c < 10; c++)
                    {
                        Console.SetCursorPosition(c, 29);
                        Console.WriteLine(" ");
                    }
                    Console.SetCursorPosition(2, 29);
                    Console.Write("< [Back] to return");
                    Current_Command = Console.ReadLine().ToUpper();
                    if (Current_Command == "BACK")
                    {
                        Console.Clear();
                    }
                }
                
                if (Current_Command == "REST")
                {
                    Console.Clear();
                    Console.SetCursorPosition(Console.WindowWidth / 2-12, Console.WindowHeight / 2);
                    Console.Write("Rest for how long? ");
                    Resting_Time = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();

                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 - 5);
                    Console.Write("Resting ");
                    Resting_Time = (Resting_Time) * 1000;
                    System.Threading.Thread.Sleep(Resting_Time);
                    Console.Clear();

                    Player_HP_Current = Player_HP_Current + ((Resting_Time/1000)/2)*15;
                }
                if (Current_Command == "STATUS")
                {
                    if (Player_HP_Current < Player_EXP_Full*.60)
                    {
                        Player_Status = "You feel better. ";
                    }
                    if (Player_HP_Current < Player_EXP_Full*.50)
                    {
                        Player_Status = "You feel bad, but are able to go on. ";
                    }
                    if (Player_HP_Current < Player_EXP_Full*.10)
                    {
                        Player_Status = "You are not feeling good. ";
                    }
                    Console.Clear();
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 - 5);
                    Console.Write("{0}", Player_Status);
                    for (int c = 0; c < 10; c++)
                    {
                        Console.SetCursorPosition(c, 29);
                        Console.WriteLine(" ");
                    }
                    Console.SetCursorPosition(2, 29);
                    Console.Write("< [Back] to return");
                    Console.SetCursorPosition(0, 20);
                    Current_Command = Console.ReadLine().ToUpper();
                    if (Current_Command == "BACK")
                    {
                        Console.Clear();
                    }
                    else { }
                }



                //Intro Mission
                Console.SetCursorPosition(3, 5);
                Console.Write("You awaken in a darkened dungeon cellar. Feeling around, you do not notice any particular items on your person. ");
                Console.Write("Your weapon is bloody from the fight you put up. ");
                Console.Write("It appears the foe did not enjoy the rebelion as much as you did. ");
                Console.Write("You must find your way out of this fight. The footsteps become louder in the distance. ");
                Console.Write("You are unaware of who is coming... ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 - 3);
                Console.Write("< [WAIT] to (Continue moving forward) ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 - 2);
                Console.Write("< [RUN] to (Escape potential enemies) ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 - 1);
                Current_Command = "";
                Current_Command = Console.ReadLine().ToUpper();
                if (Current_Command == "WAIT")
                {
                    Dice_Rolled = Dice_Roll.Next(1, 3);
                    if (Dice_Rolled == 1)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(3, 5);
                        Console.Write("The footsteps come to a stop, and faint sound comes from the corner of the room. ");
                        Console.Write("You pick up a light from the barrel next to you. ");
                        Console.Write("The light shines forward in the dark dungeon ahead, but the darkness consumes the light. ");
                        Console.Write("You hear another sound. ");
                        Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 - 3);
                        Console.Write("< [WAIT] to (Stay and put up a fight) ");
                        Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 - 2);
                        Console.Write("< [RUN] to (Escape danger) ");
                        Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 - 1);
                        Current_Command = "";
                        Current_Command = Console.ReadLine().ToUpper();
                        if (Current_Command == "WAIT")
                        {
                            Console.Clear();
                            Player_Group++;
                            Console.SetCursorPosition(3, 5);
                            Console.Write("Something starts to approach the room you are in. You see a dim face in the darkness of the dungeon. ");
                            Console.Write("You hold the light in front of you and see a strange man coming toward you. He says he is here to help you escape, but he thinks he was followed down to the dungeon. ");
                            Console.Write("The man tells you to follow him, and you begin to follow. You follow the man down the dungeon and the light gets brighter. ");
                            Console.Write("As you get further down the dungeon, you hear a loud screech. ");
                            // the light gets brighter. You can see outside the dungeon. You and the man have made it outside. "

                            Console.SetCursorPosition(2, 27);
                            Console.Write("[ENTER] to Enter Combat ");


                            Console.ReadLine();
                            Console.Clear();


                            // Enemy Create
                            Enemy_Name = "Shadow";
                            Enemy_HP_Full = 40;
                            Enemy_HP_Current = 40;
                            Enemy_Attack = 5;
                            Current_Command = "COMBAT";


                            // Combat Phase 
                            do
                            {
                                Console.Clear();
                                Console.SetCursorPosition(5, 1);
                                Console.WriteLine("Name: {0}", Player_Name);
                                Console.SetCursorPosition(30, 1);
                                Console.WriteLine("HP: {0}/{1}", Player_HP_Current, Player_HP_Full);
                                Console.SetCursorPosition(60, 1);
                                Console.WriteLine("EXP: {0}/{1}", Player_EXP_Current, Player_EXP_Full);
                                Console.SetCursorPosition(90, 1);
                                Console.WriteLine("Level: {0}", Player_Level_Current);

                                // Enemy Name & Hp Status
                                Console.SetCursorPosition(5, 3);
                                Console.WriteLine("Enemy: {0}", Enemy_Name);
                                Console.SetCursorPosition(30, 3);
                                Console.WriteLine("Enemy HP: {0}/{1}", Enemy_HP_Current, Enemy_HP_Full);

                                Console.SetCursorPosition(5, 10);
                                Console.Write("[1] to Attack");
                                Console.SetCursorPosition(30, 10);
                                Console.Write("[2] to Cast Magic");
                                Console.SetCursorPosition(5, 11);
                                Console.Write("[3] to Consume Potion");
                                Console.SetCursorPosition(30, 11);
                                Console.Write("[4] to Run from Combat");
                                Current_Command = Console.ReadLine().ToUpper();
                                if(Current_Command == "1")
                                {
                                    Dice_Rolled = Dice_Roll.Next(0, Player_Attack + Attack_Bonus+1);
                                    if(Dice_Rolled >= 1)
                                    {
                                        Console.SetCursorPosition(3, 15);
                                        Console.WriteLine("Your hit the {0}. You deal {1} damage. ", Enemy_Name, Dice_Rolled);
                                        Enemy_HP_Current = Enemy_HP_Current - Dice_Rolled;
                                    } else
                                    {
                                        Console.SetCursorPosition(3, 15);
                                        Console.WriteLine("You missed your attack. ");
                                    }
                                }
                                if (Current_Command == "2")
                                {
                                    int Line_Counter = 15;
                                    Console.SetCursorPosition(3, 15);
                                    Console.WriteLine("Choose a Spell. ");
                                    Console.SetCursorPosition(15, 5);
                                    Console.WriteLine("Name: ");
                                    Console.SetCursorPosition(35, 5);
                                    Console.WriteLine("Points: ");
                                    for (int a = 0; a <= 19; a++)
                                    {
                                        for(int b = 0; b <= 9; b++)
                                        {
                                            if(Spells[b,a] != "" || Spells[b,a] != null)
                                            {
                                                Console.SetCursorPosition(b*5+10, Line_Counter++);
                                                Console.Write("{0}", Spells[b, a]);
                                            }
                                        }
                                    }
                                }
                                if (Current_Command == "3")
                                {

                                }
                                if (Current_Command == "4")
                                {

                                }


                            } while (Enemy_HP_Current >= 0 || Player_HP_Current >= 0);

                        }
                    }
                    if (Dice_Rolled == 2)
                    {

                    }
                }
                if (Current_Command == "RUN")
                {

                }
            } while (Game_Over == 0);
        }
    }
}
