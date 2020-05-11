using System;

namespace TextBasedVentures
{
	//class made to test combat
	class Encounters
	{
		static Random rand = new Random();
		//Encounter generic


		//Encounters
		public static void FirstEncounter()
		{
			Console.WriteLine("\nAs you walk into the main hall you find a weird creature moving at the back of the halls");
			Console.WriteLine("\nThe creature turns to you and begins running.");
			Console.WriteLine("\nTime for fight or flight.");
			Console.ReadKey();
			Combat(false, "Morioh Demon", 5, 10);
		}

		//Encounter tools
		public static void Combat(bool random, string name, int power, int health)
		{
			string n = "";
			int p = 0;
			int h = 0;
			if (random)
			{

			}
			else
			{
				n = name;
				p = power;
				h = health;
			}
			while (h > 0)
			{
				//Console.Clear();
				Console.WriteLine("Monster name: " + n);
				Console.WriteLine("Monster Attack: " + p + " Monster Health: " + h);
				Console.WriteLine("=====================");
				Console.WriteLine("| (A)ttack (D)efend |");
				Console.WriteLine("|    (H)eal          |");
				Console.WriteLine("=====================");
				Console.WriteLine("Potions: " + Program.currentPlayer.potion + " Health: " + Program.currentPlayer.health);
				string input = Console.ReadLine();
				if (input.ToLower() == "a" || input.ToLower() == "attack")
				{
					//Attack
					Console.WriteLine("With haste you whack the enemy. The " +n+ " strikes you back.");
					int damage = p - Program.currentPlayer.armorVal;
					if (damage < 0)
						damage = 0;
					int attack = rand.Next(1, Program.currentPlayer.weaponVal) +rand.Next(1,4);
					Console.WriteLine("You lose " + damage + "  health and you dealt " + attack + " damage");
					Program.currentPlayer.health -= damage;
					h -= attack;
				}
				else if (input.ToLower() == "d" || input.ToLower() == "defend")
				{
					//Defend
					Console.WriteLine("You prepare yourself to endure damage. The " + n + " strikes you at you with reduced damage.");
					int damage = (p/4) - Program.currentPlayer.armorVal;
					if (damage < 0)
					{
						damage = 0;
					}
					int attack = rand.Next(1, Program.currentPlayer.weaponVal) /2;
					Console.WriteLine("You lose " + damage + "  health and you dealt " + attack + " damage");
					Program.currentPlayer.health -= damage;
					h -= attack;
				}
				/*
				else if (input.ToLower() == "r" || input.ToLower() == "run")
				{
					//Run
					if (rand.Next(0, 2) == 0)
					{
						int damage = p - Program.currentPlayer.armorVal;
						if (damage < 0)
						{
							damage = 0;
						}
						Console.WriteLine("You lose " + damage + "health and are unable to escape.");
						Console.ReadKey();
					}
					else
					{
						Console.WriteLine("You successfully pulled off the secret Joestar technique, run.");
						Console.ReadKey();
						//flees
					}
				}
				*/
				else if (input.ToLower() == "h" || input.ToLower() == "heal")
				{
					//Heal
					if(Program.currentPlayer.potion == 0)
					{
						Console.WriteLine("You reach for an Estus Flash...however you are out.");
						int damage = p - Program.currentPlayer.armorVal;
						if(damage < 0)
						{
							damage = 0;
						}
						Console.WriteLine("You took " + damage + " damage from " + n);
					}
					else
					{
						int potVal = 5;
						Console.WriteLine("You pulled out an Estus Flask and took a drink. You gained " +  potVal + "health");
						Program.currentPlayer.health += potVal;
						Console.WriteLine("While drinking " + n + " attacks you, dealing.");
						int damage = (p / 2) - Program.currentPlayer.armorVal;
						if(damage < 0)
						{
							damage = 0;
						}
						Console.WriteLine("You took " + damage + " damage.");
					}
					Console.ReadKey();
				}
				Console.ReadKey();
			}
		}
	}
}


