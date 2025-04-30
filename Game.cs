using System;
using System.Collections.Generic;
<<<<<<< Updated upstream
=======
using System.Diagnostics;
using System.Linq;
using System.Threading;
using DungeonExplorer;
using static DungeonExplorer.Weapon;
>>>>>>> Stashed changes

namespace DungeonExplorer
{
    internal class Game
    {
<<<<<<< Updated upstream
        private Player player; // Player Character
        private Room currentRoom; // The current room that the player is in
        private Random random; // Random number generator
        private List<Room> rooms; // A list with all the rooms that are available
        private List<string> items; // A list of all the items that are available
        private List<string> charNames; // A list of names
=======
        private const bool DEBUG_MODE = false; // set to true to debug player stats
        private Player player; // Player character
        private Room currentRoom; // The current room the player is in
        private Random random; // A random number generator
        private List<Room> rooms; // A list with all the rooms that are available
        private List<Item> items; // A list of all the items that are available
        private List<string> charNames; // A list of pre-determined names
        private List<Monster> monsters; // A list of all the monsters
        private GameMap gameMap; // Game Map logic
>>>>>>> Stashed changes

        public Game()
        {
            random = new Random();
            // A list of names the game can choose from if the player does not want to input their own character name
            charNames = new List<string>
<<<<<<< Updated upstream
            {
                "John", "Jake", "Mathew", "Dan", "Mike", "Blake", "Morgan", "Sebastian"
            };

            string playerName;
            while (true)
            {
                try
                {   // Displays the game title
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine("           DUNGEON EXPLORER ");
                    Console.WriteLine("========================================\n");
                    Console.ResetColor();

                    // Asks the player if they want to name their character

                    Console.WriteLine("Do you wish to name your character? (Yes|No)");
                    string input = Console.ReadLine()?.Trim().ToLower();

                    if (input == "yes")
                    {
                        Console.WriteLine("Please name your character:");
                        playerName = Console.ReadLine(); // Sets player name to the user input
                        break;
                    }
                    else if (input == "no")
                    {   

                        // Choose from the list of basic names, if player chooses no to naming their character

                        playerName = charNames[random.Next(charNames.Count)];
                        Console.WriteLine($"Your character's name is {playerName}.");
                        break;
                    }
                    else
                    {
                        throw new Exception("Invalid input! Please enter 'Yes' or 'No'.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            // Player starts off with 100 health

            player = new Player(playerName, 100);
            
            // List of all the possible items

            items = new List<string> { "Health Potion", "Gas Mask", "Rusty Sword", "Broken Shield", "A pile of bones", "Gold Bar" };

            // List of all rooms, with description with a random item each time player enters room

            rooms = new List<Room>
=======
>>>>>>> Stashed changes
            {
                "John", "Jake", "Matthew", "Dan", "Mike", "Blake", "Morgan", "Sebastian"
            };
<<<<<<< Updated upstream

            // Set the current room to a random room from the list

            currentRoom = rooms[random.Next(rooms.Count)];
        }
=======
            // List of monsters, with their health, damage and gold that the player can earn from killing them
            monsters = new List<Monster>
            {
                new Alive("Orc", 60, 20, 10),
                new Alive("Giant", 100, 40, 20),
                new Alive("Rat", 40, 10, 5),
                new Undead("Ghoul", 60, 15, 10),
                new Undead("Vampire", 80, 30, 15),
                new Undead("Zombie", 50, 25, 10),
                new Undead("Wandering Soul", 60, 30, 20),
                new Boss("Hydra", 200, 50, 50),
                new Alive("Bandit", 80, 30, 15),
            };
            // Order the list of monsters in descending order by their attack power
            monsters = monsters.OrderByDescending(m => m.AttackPower).ToList();
>>>>>>> Stashed changes

            string playerName;
            while (true)
            {
                try
                {   // Displays the game title and rules and monster list by attack power
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("========================================");
                    Console.WriteLine("           DUNGEON EXPLORER ");
                    Console.WriteLine("========================================\n");
                    Console.WriteLine("                  RULES ");
                    Console.WriteLine("Your goal is to kill the boss in the final room.");
                    Console.WriteLine("Everytime you pick up a new weapon it swaps the previous one in your inventory.");
                    Console.WriteLine("You may pick up as many potions as you want");
                    var strongestMonsters = monsters.OrderByDescending(m => m.AttackPower);

                    Console.WriteLine("Monsters are sorted by strength:");
                    foreach (var monster in strongestMonsters)
                    {
                        Console.WriteLine($"{monster.Name} (Attack Power: {monster.AttackPower})");
                    }
                    Console.WriteLine("\n\n========================================");
                    Console.ResetColor();
                    // Asks if the user wants to name their character
                    Console.WriteLine("\n\nDo you wish to name your character? (Yes|No)");
                    string input = Console.ReadLine()?.Trim().ToLower();

                    if (input == "yes")
                    {
                        Console.WriteLine("Please name your character:");
                        playerName = Console.ReadLine();
                        break;
                    }
                    else if (input == "no")
                    {   
                        // If user chooses no it picks from the list of pre-determined character names
                        playerName = charNames[random.Next(charNames.Count)];
                        Console.WriteLine($"Your character's name is {playerName}.");
                        break;
                    }
                    else
                    {
                        throw new Exception("Invalid input! Please enter 'Yes' or 'No'.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            // Base player stats
            player = new Player(playerName, 200, 0);

            // List of all the weapons and potions

            items = new List<Item>
            {
                new HealthPotion("Weak Health Potion", 10),
                new HealthPotion("Health Potion", 25),
                new HealthPotion("Greater Health Potion", 50),
                new Weapon("Rusty Sword", 20),
                new Weapon("Knights Sword", 40),
                new Weapon("Legendary Hero Sword", 80),
                new Weapon("Steel Mace",30),
                new Weapon("Elven Bow",25)
            };

            // Generates keys

            Key goldenKey = new Key("Golden Key");
            Key silverKey = new Key("Silver Key");

            // Rooms with description, whether it has a key,  a random item, a random monster and whether the room requires a key or not

            Room startingRoom = new Room("\nYou stand in the entrance hall filled with courage.", goldenKey, null);
            Room treasureRoom = new Room("\nGlittering gold covers the floor... but it seems this room is a dead end", items[random.Next(items.Count)], monsters[random.Next(monsters.Count)], true, "Golden Key");
            Room library = new Room("\nCenturies of knowledge tower over you as you enter the library.", items[random.Next(items.Count)], monsters[random.Next(monsters.Count)]);
            Room prisonCell = new Room("\nA prison cell with broken bars... what could have escaped?", silverKey, monsters[random.Next(monsters.Count)]);
            Room secretChamber = new Room("\nA hidden room with ancient relics.", items[random.Next(items.Count)], monsters[random.Next(monsters.Count)], true, "Silver Key");
            Room lair = new Room("\nAn evil room with dim lighting, bones lay in piles.", items[random.Next(items.Count)], monsters[random.Next(monsters.Count)]);
            Monster hydra = monsters.Find(m => m.Name == "Hydra");
            Room bigRoom = new Room("\nA massive room stands before you... You feel a ominous presence", items[random.Next(items.Count)], hydra);

            // Logic to connect rooms with a direction

            startingRoom.SetConnection("north", library);
            library.SetConnection("east", treasureRoom);
            treasureRoom.SetConnection("west", library);
            library.SetConnection("west", prisonCell);
            prisonCell.SetConnection("north", secretChamber);
            prisonCell.SetConnection("west", lair);
            secretChamber.SetConnection("north", bigRoom);


            rooms = new List<Room> { startingRoom, treasureRoom, library, prisonCell, secretChamber, lair, bigRoom };

            gameMap = new GameMap(rooms, random);
            currentRoom = startingRoom;
        }
        public void Start()
        {
            bool playing = true;
<<<<<<< Updated upstream
            while (playing)
            {   

                // Displays the players stats, inventory and what room they are in

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Player: {player.Name}, Health: {player.Health}");
                Console.ResetColor();
                Console.WriteLine("\nInventory: " + player.InventoryContents());
                Console.WriteLine($"\nCurrent Room: {currentRoom.GetDescription()}");
                Console.WriteLine($"\nThere is a {currentRoom.Item} in this room.");
                
                // Loop that asks the player if they want to pick up the item in the current room

                while (true)
                {
                    try
                    {
                        Console.WriteLine("Do you want to pick this item up? (Yes|No)");
                        string input = Console.ReadLine()?.Trim().ToLower();

                        if (input == "yes")
                        {
                            player.PickUpItem(currentRoom.Item);
                            Console.WriteLine($"\nYou picked up {currentRoom.Item}.");
                            break;
                        }
                        else if (input == "no")
                        {
                            break;
                        }
                        else
                        {
                            throw new Exception("Invalid input! Please enter 'Yes' or 'No'.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                // Loop that asks the player if they want to continue playing the game and move onto the next room

                while (true)
                {
                    try
                    {
                        Console.WriteLine("\nDo you want to continue? (Yes|No)");
                        string continueInput = Console.ReadLine()?.Trim().ToLower();

                        if (continueInput == "no")
                        {
                            playing = false;
                            Console.WriteLine("Game ended.");
                            return;
                        }
                        else if (continueInput == "yes")
                        {
                            Room newRoom;
                            // If user wants to continue it chooses a random room from the list and moves them
                            do
                            {
                                newRoom = rooms[random.Next(rooms.Count)];
                            } while (newRoom == currentRoom);

                            currentRoom = newRoom;
                            Console.WriteLine("\n========================================\nYou have moved to the next room...");
                            break;
                        }
                        else
                        {
                            throw new Exception("Invalid input! Please enter 'Yes' or 'No'.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
=======

            //Runs debug mode if it is set to true

            if (DEBUG_MODE == true)
            {
                PlayerTests.RunAllTests();
            }
            
            // Handles all the events and displays them within the loop

            DisplayRoomInfoAndHandleEvents();

            while (playing)
            {  

                while (currentRoom.Monster != null && currentRoom.Monster.Health > 0 && player.Health > 0)
                {   
                    // Gives user option to attack current monster or flee and move to the next room
                    Console.WriteLine($"\nDo you want to attack the {currentRoom.Monster.Name} or flee? [ATTACK | FLEE]");
                    string userOption = Console.ReadLine()?.Trim().ToLower();

                    if (userOption == "attack")
                    {
                        player.Attack(currentRoom.Monster);

                        if (currentRoom.Monster.Health <= 0)
                        {   // If monster is undead and is killed it will be resurrected
                            if (currentRoom.Monster is Undead undead && !undead.hasResurrected)
                            {
                                undead.Resurrect();
                                continue;
                            }
                            
                            // If monster is killed then displays how much gold player has earned from killing monster

                            int goldGained = currentRoom.Monster.GoldGained;
                            player.AddGold(goldGained);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{player.Name} defeated the {currentRoom.Monster.Name} and earned {goldGained} gold");
                            Console.ResetColor();

                        }


                        // If monnster attacks player it displays how much damage it did and what the player health is now,
                        // In addition to asking if the player wants to use a potion to heal if they take damage

                        if (currentRoom.Monster != null && currentRoom.Monster.Health > 0 && player.Health > 0)
                        {
                            if (random.Next(100) < 50)
                            {
                                Console.WriteLine($"\nThe {currentRoom.Monster.Name} attacks you!");
                                currentRoom.Monster.Attack(player);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"{player.Name}'s health is now {player.Health}");

                                Console.WriteLine("Do you wish to use a health potion? [Yes | No]");
                                string healPlayer = Console.ReadLine()?.Trim().ToLower();

                                if (healPlayer == "yes")
                                {
                                    // Use the GetStrongestHealingPotion method to find the strongest potion
                                    var strongestPotion = player.GetStrongestHealingPotion();

                                    if (strongestPotion != null)
                                    {
                                        // Display the strongest healing potion to the player
                                        Console.WriteLine($"The strongest potion in your inventory is: {strongestPotion.ItemName} (Heals {strongestPotion.HealAmount} HP).");

                                        // Ask the player if they want to use this potion
                                        Console.WriteLine("Do you want to use this potion? [Yes | No]");
                                        string usePotion = Console.ReadLine()?.Trim().ToLower();

                                        if (usePotion == "yes")
                                        {
                                            player.Heal(strongestPotion.HealAmount); // Heal the player
                                            Console.WriteLine($"{player.Name} healed for {strongestPotion.HealAmount} HP!");
                                            player.Inventory.Remove(strongestPotion); // Remove the used potion from the inventory
                                        }
                                        else
                                        {
                                            Console.WriteLine("You decided not to use the potion.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("You have no healing potions!");
                                    }
                                }

                                Console.ResetColor();
                                
                                // If the player health reaches 0 then it ends the game and displays the players statistics

                                if (player.Health <= 0)
                                {
                                    Console.WriteLine("\nYou have been killed... Game over.");
                                    Console.WriteLine("Final Statistics:");
                                    Console.WriteLine(player.Stats.ToString());
                                    return;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"{currentRoom.Monster.Name} did not attack.");
                            }
                        }
                    }
                    else if (userOption == "flee")
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("You decided to flee.");
                        Console.ResetColor();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please try again.");
                    }
                }

                // Loop to ask if the player wants to continue playing

                if (playing)
                {
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("\nDo you want to continue playing? (Yes|No)");
                            string continueInput = Console.ReadLine()?.Trim().ToLower();

                            // Ends game and displays statistics is player chooses no

                            if (continueInput == "no")
                            {
                                Console.WriteLine("Game ended.");
                                Console.WriteLine("Final Statistics:");
                                Console.WriteLine(player.Stats.ToString());
                                return;
                            }
                            else if (continueInput == "yes") // If player picks yes then asks what direction they want to move in
                            {
                                Console.WriteLine("\nWhich direction do you want to move? (north, east, west, south)");
                                string userDirection = Console.ReadLine()?.Trim().ToLower();
                                Room nextRoom = currentRoom.GetConnectedRoom(userDirection);

                                if (nextRoom == null)
                                {
                                    Console.WriteLine("\nNo room in that direction."); // Displays if there is no room in the direction the player picked to move
                                }
                                else if (nextRoom.IsLocked)
                                {
                                    Console.WriteLine($"The door is locked! You need the {nextRoom.KeyName}."); // Displays if the player does not have the required key to enter the room

                                    bool hasKey = player.Inventory.Exists(item => item.ItemName == nextRoom.KeyName); 

                                    if (hasKey)
                                    {
                                        Console.WriteLine($"You used the {nextRoom.KeyName} to unlock the door!"); // Displays if the player HAS required key and then unlocks the door and tells them that they have moved
                                        nextRoom.IsLocked = false;
                                        currentRoom = nextRoom;
                                        Console.WriteLine("\n========================================\nYou have moved to the next room...");
                                        DisplayRoomInfoAndHandleEvents();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("You don't have the required key!");
                                    }
                                }
                                else
                                {
                                    currentRoom = nextRoom;
                                    Console.WriteLine("\n========================================\nYou have moved to the next room...");
                                    DisplayRoomInfoAndHandleEvents();
                                    break;
                                }
                            }
                            else
                            {
                                throw new Exception("Invalid input! Please enter 'Yes' or 'No'.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

        // Dsiplay and handle all events that can happen in a room
        private void DisplayRoomInfoAndHandleEvents()
        {
            // Display all player details, name, health, inventory, room they are room and the item that is in the room

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Player: {player.Name}, Health: {player.Health}");
            Console.ResetColor();
            Console.WriteLine("\nInventory: " + player.InventoryContents());
            Console.WriteLine($"\nCurrent Room: {currentRoom.GetDescription()}");
            Console.WriteLine($"\nThere is a {currentRoom.RoomItem} in this room.");
            
            // Displays the current room monster

            if (currentRoom.Monster != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"There is a {currentRoom.Monster.Name} in this room.");
                Console.ResetColor();
            }

            // Asks if the player wants to pick the item up that is in the current room

            while (currentRoom.RoomItem != null)
            {
                try
                {
                    Console.WriteLine("\nDo you want to pick the item up? (Yes|No)");
                    string input = Console.ReadLine()?.Trim().ToLower();

                    if (input == "yes")
                    {
                        player.PickUpItem(currentRoom.RoomItem);
                        Console.WriteLine($"\nYou picked up {currentRoom.RoomItem}.");
                        currentRoom.RoomItem = null;
                    }
                    else if (input == "no")
                    {
                        break;
                    }
                    else
                    {
                        throw new Exception("Invalid input! Please enter 'Yes' or 'No'.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
>>>>>>> Stashed changes
                }
            }
        }
    }
}

// Class to display and test player health, pick up item and heal logic when the game is in debug mode
public static class PlayerTests
{
    public static void RunAllTests()
    {
        TestTakeDamage();
        TestHeal();
        TestPickUpItem();
        TestUseHealthPotion();

        // Displays when nothing is wrong

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nAll Player tests passed!");
        Console.ResetColor();
    }

    private static void TestTakeDamage() // Tests player taking damage
    {
        var player = new Player("TestHero", 100, 0);
        player.TakeDamage(30);
        Debug.Assert(player.Health == 70, "TestTakeDamage failed: Expected health to be 70.");
    }

    private static void TestHeal() // Tests player healing, if it is the correct amount
    {
        var player = new Player("TestHero", 100, 0);
        player.TakeDamage(50);
        player.Heal(25);
        Debug.Assert(player.Health == 75, "TestHeal failed: Expected health to be 75.");
    }

    private static void TestPickUpItem() // Test player picking up item correctly and storing it
    {
        var player = new Player("TestHero", 100, 0);
        var potion = new HealthPotion("Test Potion", 20);
        player.PickUpItem(potion);
        Debug.Assert(player.Inventory.Contains(potion), "TestPickUpItem failed: Potion not found in inventory.");
    }

    private static void TestUseHealthPotion() // Tests player using a healing potion
    {
        var player = new Player("TestHero", 100, 0);
        player.TakeDamage(50);
        var potion = new HealthPotion("Strong Potion", 40);
        player.PickUpItem(potion);
        var bestPotion = player.GetStrongestHealingPotion();
        Debug.Assert(bestPotion == potion, "TestUseHealthPotion failed: Strongest potion mismatch.");
    }
}

