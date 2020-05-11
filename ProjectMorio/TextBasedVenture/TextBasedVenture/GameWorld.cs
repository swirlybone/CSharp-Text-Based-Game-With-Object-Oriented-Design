using System;

namespace TextBasedVentures
{
	public class GameWorld
	{
		static private GameWorld _instance = null;
		static public GameWorld Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new GameWorld();
				}
				return _instance;
			}
		}

		
		private Room _entrance;
		//trigger room, designated trigger room/randomizer room
		//private Room _trigger;
		//private Room _fromRoom;
		//private Room _toRoom;
		private Room _lobby;
		private Room _battletrigger;
		private Room _victorytrigger;
		public Room Victory
		{
			get
			{
				return _victorytrigger;
			}
		}
		public Room Entrance
		{
			get
			{
				return _entrance;
			}
		}
		public Room Lobby
		{
			get
			{
				return _lobby;
			}
		}
		private GameWorld()
		{
			createWorld();
			NotificationCenter.Instance.addObserver("EndGame", EndGame);

			//NotificationCenter.Instance.addObserver("PlayerDidEnterRoom", PlayerWillEnterRoom);
		}

		//ending notification
		public void EndGame(Notification notification)
		{
			Player player = (Player)notification.Object;
			Room room = player.currentRoom;
			if (room == _victorytrigger)
			{
				player.informationMessage("*** You Win! ***");
			}

		}

		/*
		public void PlayerWillEnterRoom(Notification notification)
		{
			Player player = (Player)notification.Object;
			Room room = player.currentRoom;
			if(room == _trigger){
				player.debugMessage("*** The world has changed. ***");
				//New room added, connects a room when trigger room is reached.
				_fromRoom.setExit("west", _toRoom);
				_toRoom.setExit("east", _fromRoom);
			}

			//Statement to print current room
			//Console.WriteLine("The player is now " + player.currentRoom.tag);


		}
		*/
		

		private void createWorld()
		{
			Room mainRoom = new Room("main door of the Morioh Mansion");
			Room mainHall = new Room("Morioh Mansion main hall");
			Room diningHall = new Room("Morioh Mansion dining hall");
			Room kitchen = new Room("Morioh Mansion kitchen");
			Room bathRoom = new Room("bathroom");
			Room masterRoom = new Room("the head of the Higashikata Family's Bed Room");
			Room library = new Room("Morioh Mansion Library");
			Room storage = new Room("Morioh Mansion storage room");
			Room attic = new Room("attic");
			Room cellar = new Room("Morioh Mansion wine cellar");
			Room trophyRoom = new Room("Higashikata-Joestar family Trophy Room");
			Room studyRoom = new Room("Morioh Mansion Study room");
			Room lobby = new Room("in the lobby. Use command name to set a name, then when complete use say 'done'.\n");

			//Exit room leading to victory
			Room outside = new Room("outside of the Morioh Mansion");
			

			//New Room routes
			//connects a room to a door
			Door door = Door.MakeDoor(mainHall,mainRoom, "south","north" );
			door.close();
			//Room routes
			door = Door.MakeDoor(outside, mainRoom, "", "out");
			door.keyclose();
			door = Door.MakeDoor(studyRoom, mainRoom, "west", "east");
			door = Door.MakeDoor(diningHall, mainHall, "south", "north");
			door = Door.MakeDoor(bathRoom, mainHall, "east", "west");
			door = Door.MakeDoor(masterRoom, mainHall, "west", "east");
			door = Door.MakeDoor(kitchen, diningHall, "south", "north");
			door = Door.MakeDoor(diningHall, cellar, "down", "up");
			door = Door.MakeDoor(diningHall, storage, "west", "east");
			door = Door.MakeDoor(library, diningHall, "west", "east");
			door = Door.MakeDoor(attic, library, "down", "up");
			door = Door.MakeDoor(trophyRoom, storage, "west", "east");


			

			
			
			

			//Special rooms, assignments to special rooms
			_entrance = mainHall;
			_lobby = lobby;
			//_victorytrigger = outside;

			TrapRoom tRoom = new TrapRoom();
			library.Delegate = tRoom;

			EndRoom eRoom = new EndRoom();
			outside.Delegate = eRoom;

			cellar.Delegate = new EchoRoom();
			//mainHall.Delegate = new Battle();

			

			//Place items
			Item item = new Item();
			item.Name = "sword";
			item.Weight = 100f;
			mainHall.drop(item);

			Item item2 = new Item();
			item2.Name = "swordtitan";
			item2.Weight = 20f;
			mainRoom.drop(item2);

			//ItemContainer chest = new ItemContainer();
			//hest.Name = "chest";
			//Item other = new Item();
			//other.Name = "gem";
			//other.Weight = 0.5f;
			//chest.put(other);
			//mainHall.drop(chest);

			//Item item2 = new Item();
			//item.Name = "tree";
			//mainHall.drop(item);

			//key needed to beat the game
			//Item key = new Item();
			//item.Name = "key";
			//attic.drop(item);

			//UItem tree = new UItem();
			//tree.Name = "tree";
			//mainHall.drop(tree);


			IItem mace = new Item("mace", 0.1f, 0f, 0f);
			IItem sword = new Item("supersword", 0.1f, 0f, 0f);

			//IItem gold = new Item("gold", 0.7f, 10f, 15f);
			//IItem emerald = new Item("emerald", 0.5f, 6f, 9f);
			//IItem diamond = new Item("diamond", 0.6f, 9f, 12f);

			//mace.AddDecorator(gold);
			//sword.AddDecorator(emerald);
			//mace.AddDecorator(diamond);

			trophyRoom.drop(mace);
			kitchen.drop(sword);
		}


	}
}
