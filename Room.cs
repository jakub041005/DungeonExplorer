using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using DungeonExplorer;

namespace DungeonExplorer
{
    // Represents a room in the dungeon
    public class Room
    {
        private string description;  // Room description
        public Item RoomItem { get; set; }  // Item present in the room
        public Monster Monster { get; set; }  // Monster present in the room
        public bool IsLocked { get; set; }  // Indicates if the room is locked
        public string KeyName { get; set; }  // Name of the key that unlocks the room
        public Dictionary<string, Room> Connections { get; set; }  // Connections to other rooms

        // Constructor to initialize the room
        public Room(string description, Item item, Monster monster, bool isLocked = false, string keyName = null)
        {
            this.description = description;
            this.RoomItem = item;
            this.Monster = monster;
            this.IsLocked = isLocked;
            this.KeyName = keyName;
            this.Connections = new Dictionary<string, Room>();
        }

        // Connects this room to another in a specified direction
        public void SetConnection(string direction, Room room)
        {
            Connections[direction] = room;
        }

        // Retrieves a connected room in the specified direction
        public Room GetConnectedRoom(string direction)
        {
            if (Connections.ContainsKey(direction))
                return Connections[direction];
            return null;
        }

        // Returns the description of the room
        public string GetDescription()
        {
            return description;
        }
    }

    // Represents a monster in the game
    public class Monster
    {
        public string Name { get; private set; }
        public int Health { get; set; }
        public int Damage { get; private set; }
        public int GoldGained { get; private set; }
        public int AttackPower => Damage;  // Read-only attack power

        // Constructor to initialize a monster
        public Monster(string monsterName, int health, int damage, int goldGained)
        {
            Name = monsterName;
            Health = health;
            Damage = damage;
            GoldGained = goldGained;
        }

        // Reduces monster's health by damage taken
        public void TakeDamage(int damageTaken)
        {
            Health -= damageTaken;
            if (Health < 0) Health = 0;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{Name} takes {damageTaken} damage and has {Health} health left\n");
            Console.ResetColor();
        }

        // Monster attacks a player
        public void Attack(Player player)
        {
            if (player == null) return;

            Console.WriteLine($"{Name} attacks you for {Damage} damage.");
            player.TakeDamage(Damage);
        }
    }

    // A monster that can resurrect after dying
    public class Undead : Monster
    {
        public bool hasResurrected { get; private set; } = false;

        public Undead(string monsterName, int health, int damage, int goldGained)
            : base(monsterName, health, damage, goldGained) { }

        // Resurrects the monster with preset health
        public void Resurrect()
        {
            if (Health <= 0)
            {
                Health = 20;
                hasResurrected = true;
                Console.WriteLine($"{Name} has resurrected with {Health} health!");
            }
        }
    }

    // A regular living monster
    public class Alive : Monster
    {
        public Alive(string monsterName, int health, int damage, int goldGained)
            : base(monsterName, health, damage, goldGained) { }
    }

    // A boss monster, inheriting from Alive
    public class Boss : Alive
    {
        public Boss(string monsterName, int health, int damage, int goldGained)
            : base(monsterName, health, damage, goldGained) { }
    }

    // Abstract base class for all items
    public abstract class Item
    {
        public string ItemName { get; private set; }

        protected Item(string itemName)
        {
            ItemName = itemName;
        }

        public override string ToString()
        {
            return ItemName;
        }
    }

    // Represents a health potion item
    public class HealthPotion : Item
    {
        public int HealAmount { get; private set; }

        public HealthPotion(string itemName, int healAmount)
            : base(itemName)
        {
            this.HealAmount = healAmount;
        }
    }

    // Represents a weapon item
    public class Weapon : Item
    {
        public int Damage { get; private set; }

        public Weapon(string itemName, int damage)
            : base(itemName)
        {
            Damage = damage;
        }

        // Represents a key used to unlock rooms
        public class Key : Item
        {
            public Key(string keyName) : base(keyName) { }
        }
    }

    // Represents the map of the game consisting of rooms
    public class GameMap
    {
        public List<Room> Rooms;
        private Random Random;

        // Constructor initializes room list and RNG
        public GameMap(List<Room> rooms, Random random)
        {
            Rooms = rooms;
            Random = random;
        }

        // Returns a random room (direction argument is unused here)
        public Room GetRandomRoom(string RoomDirection)
        {
            return Rooms[Random.Next(Rooms.Count)];
        }
    }

    // Interface for objects that can take damage
    public interface IDamageable
    {
        void TakeDamage(int damage);
        int Health { get; }
    }

    // Interface for objects that can collect items
    public interface ICollectable
    {
        void PickUpItem(Item item);
    }
}
