using System.Collections;
using System.Collections.Generic;
using System;

namespace TextBasedVentures
{
	public class Game
	{
		Player player;
		Parser parser;
		public bool playing;

		//command design
		Queue<Command> commandQueue;


		public Game()
		{
			playing = false;
			parser = new Parser(new CommandWords());
			player = new Player(GameWorld.Instance.Lobby);
			Dictionary<string, object> userInfo = new Dictionary<string, object>();
			userInfo["state"] = new ParserCharacterState();
			NotificationCenter.Instance.postNotification(new Notification("PlayerWillEnterState", player, userInfo));
			commandQueue = new Queue<Command>();
		}


		/**
	 *  Main play routine.  Loops until end of play.
	 */
		public void play()
		{

			// Enter the main command loop.  Here we repeatedly read commands and
			// execute them until the game is over.

			bool finished = false;
			while (!finished && playing)
			{
				Console.Write("\n" + player.Name + ">");
				Command command = parser.parseCommand(Console.ReadLine());
				if (command == null)
				{
					player.warningMessage("Command could not be understood.");
				}
				else
				{
					finished = command.execute(player);
				}
			}
		}

		public void start()
		{
			playing = true;
			player.informationMessage(welcome());
			processCommandQueue();
		}

		public void end()
		{
			playing = false;
			player.informationMessage(goodbye());
		}

		public string welcome()
		{
			return "You awake inside the Morioh Mansion that belongs to the esteemed Higashikata family.\n\nThe front door is locked and you feel a sense of dread, it's time to escape.\n\nType 'help' if you need help." + player.currentRoom.description();
			
		}

		public string goodbye()
		{
			return "\nThank you for playing. \n";
		}



		public void processCommandQueue()
		{
			while(commandQueue.Count > 0)
			{
				Command command = commandQueue.Dequeue();
				player.outputMessage(">" + command);
				command.execute(player);
			}
		}

		//Encounters
		public  void FirstEncounter()
		{
			player.informationMessage("As you proceed to the main ");
		}

		

	}
}
