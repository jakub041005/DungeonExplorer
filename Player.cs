using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    // The Player class represents a player character in the game
    public class Player : IDamageable, ICollectable
    {
        // Player's name, health, gold amount, and gameplay statistics
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Gold { get; private set; }
        public Statistics Stats { get; private set; }

        // Inventory of items the player carries
        private List<Item> inventory = new List<Item>();
        public List<Item> Inventory => inventory;

        // Constructor to initialize player with name, health, and gold
        public Player(string name, int health, int gold)
        {
            Name = name;
            Health = health;
            Gold = gold;
            Stats = new Statistics();
        }

        // Adds gold to the player's total and updates statistics
        public void AddGold(int amount)
        {
            Gold += amount;
            Stats.RecordGoldCollected(amount);
        }

        // Reduces player's health when taking damage and updates statistics
        public void TakeDamage(int damageTaken)
        {
            Health -= damageTaken;
            Stats.RecordDamageTaken(damageTaken);
        }

        // Increases player's health, capping it at a maximum of 200
        public void Heal(int healthHealed)
        {
            Health += healthHealed;
            if (Health > 200) Health = 200;
        }

        // Adds an item to inventory, removing existing weapon if a new weapon is picked up
        public void PickUpItem(Item item)
        {
            if (item != null)
            {
                if (item is Weapon)
                {
                    // Only one weapon can be held; replace existing weapon
                    inventory.RemoveAll(i => i is Weapon);
                }
                inventory.Add(item);
            }
        }

        // Returns a string listing the contents of the inventory
        public string InventoryContents()
        {
            if (inventory.Count == 0)
                return "Empty";

            return string.Join(", ", inventory.ConvertAll(i => i.ItemName));
        }

        // Allows the player to attack a monster if they have a weapon
        public void Attack(Monster monster)
        {
            if (monster == null)
            {
                Console.WriteLine("There's no monster to attack.");
                return;
            }

            // Find a weapon in the inventory
            Weapon weapon = inventory.Find(item => item is Weapon) as Weapon;

            if (weapon != null)
            {
                Console.WriteLine($"{Name} attacks {monster.Name} with {weapon.ItemName} for {weapon.Damage} damage!");
                monster.TakeDamage(weapon.Damage);
                Stats.RecordDamageDealt(weapon.Damage);

                if (monster.Health <= 0)
                {
                    Stats.RecordKill();
                }
            }
            else
            {
                Console.WriteLine("You need a weapon to attack!");
            }
        }

        // Returns the healing potion with the highest heal amount from the inventory
        public HealthPotion GetStrongestHealingPotion()
        {
            var healingPotions = inventory.OfType<HealthPotion>(); // Find all health potions in inventory

            if (!healingPotions.Any()) return null; // No healing potions found

            return healingPotions.OrderByDescending(potion => potion.HealAmount).First(); // Return the strongest one
        }

        // Nested class for tracking player statistics during gameplay
        public class Statistics
        {
            public int EnemiesDefeated { get; private set; }
            public int DamageDealt { get; private set; }
            public int DamageTaken { get; private set; }
            public int GoldCollected { get; private set; }

            // Record a defeated enemy
            public void RecordKill()
            {
                EnemiesDefeated++;
            }

            // Record amount of damage dealt by the player
            public void RecordDamageDealt(int amount)
            {
                DamageDealt += amount;
            }

            // Record amount of damage taken by the player
            public void RecordDamageTaken(int amount)
            {
                DamageTaken += amount;
            }

            // Record amount of gold collected
            public void RecordGoldCollected(int amount)
            {
                GoldCollected += amount;
            }

            // Display a summary of statistics
            public override string ToString()
            {
                return $"Enemies Defeated: {EnemiesDefeated}\n" +
                       $"Damage Dealt: {DamageDealt}\n" +
                       $"Damage Taken: {DamageTaken}\n" +
                       $"Gold Collected: {GoldCollected}";
            }
        }
    }
}
