using System.Collections;
using System.Collections.Generic;
using System;

namespace TextBasedVentures
{
    public class Player
    {
        List<Room> haveBeen = new List<Room>();
        List<Room> forBack = new List<Room>();
        bool key = false;

        private Room _currentRoom = null;
        public Room currentRoom
        {
            get
            {
                return _currentRoom;
            }
            set
            {
                _currentRoom = value;
            }
        }
        //lobby room set up
        //private string _name = "";
        public string Name { set; get; }
        /*
        private bool InBattle { get; set; }
        private bool InTrading { get; set; }
        private bool InCrafting { get; set; }
        */

        private IItemContainer inventory;
        //private IUItemContainer uinventory;

        public Player(Room room)//, GameOutput output)
        {
            
            _currentRoom = room;
            Name = "";
            inventory = new ItemContainer();
            //uinventory = new UItemContainer();
            /*
            InBattle = false;
            InTrading = false;
            InCrafting = false;
            */
        }

        public void give(IItem item)
        {
            inventory.put(item);
        }
        public IItem take(string itemName)
        {
            return inventory.remove(itemName);
        }

        public void showInventory()
        {
            informationMessage("\nInventory.\n" + inventory.contents());
        }

        public void pickup(string itemName)
        {
            IItem item = currentRoom.pickup(itemName);
            if(item != null)
            {
                if (item.CarryCapacity < item.Weight)
                {
                    warningMessage("\nItem " + itemName + " is too big to carry.");
                    //give(item);
                    //informationMessage("\nThe item " + itemName + " is now in your inventory.");
                    currentRoom.drop(item);
                }
                
                else
                {
                    give(item);
                    informationMessage("\nThe item " + itemName + " is now in your inventory.");
                }
                
            }
            /*
            if (item != null)
            {
                give(item);
                informationMessage("\nThe item " + itemName + " is now in your inventory.");
            }
            */
            else
            {
                warningMessage("\nItem " + itemName + " does not exist in this room.");
            }
        }

        public void drop(string itemName)
        {
            IItem item = take(itemName);
            if (item != null)
            {
                currentRoom.drop(item);
                informationMessage("\nThe item " + itemName + " is now in the room.");
            }
            else
            {
                warningMessage("\nThe item " + itemName + " is not in your inventory");
            }
        }


        public void waltTo(string direction)
        {
            Door door = this._currentRoom.getExit(direction);
            if (door != null)
            {
                haveBeen.Add(currentRoom);
                forBack.Add(currentRoom);
                if (door.State == OCState.Open)
                {
                    Room nextRoom = door.getRoom(_currentRoom);
                    //Notification used here not in use
                    //Player will move to room
                    NotificationCenter.Instance.postNotification(new Notification("PlayerWillEnterRoom", this));
                    this._currentRoom = nextRoom;
                    //Player did move to room
                    NotificationCenter.Instance.postNotification(new Notification("PlayerDidEnterRoom", this));
                    this.informationMessage("\n" + this._currentRoom.description());
                }
                else
                {
                    this.warningMessage("\nThe door on  " + direction + " is not open.");
                }
                
            }
            else
            {
                this.warningMessage("\nThe is no door on " + direction);
            }
        }


        public void say(string word)
        {
            Dictionary<string, Object> userInfo = new Dictionary<string, object>();
            userInfo["word"] = word;
            NotificationCenter.Instance.postNotification(new Notification("PlayerWillSayWord", this, userInfo));
            informationMessage("\n>>>" + word + "\n");
            NotificationCenter.Instance.postNotification(new Notification("PlayerDidSayWord", this, userInfo));
        }

        public void name(string newName)
        {
            Name = newName;
        }

        public void open(string direction)
        {
            Door door = this._currentRoom.getExit(direction);
            if (door != null)
            {
                if (door.State == OCState.Open)
                {
                    this.warningMessage("\nThe door on " + direction + " is already open.");
                }
                else
                {
                    if (door.open().State == OCState.Open)
                    {
                        this.informationMessage("\nThere door on  " + direction + " is now open.");
                    }
                    else
                    {
                        this.warningMessage("\nThe door on " + direction + " did NOT open.");
                    }

                }

            }
            else
            {
                this.warningMessage("\nThere is no door on " + direction);
            }
        }
        public void exit()
        {
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            //returns it back to normal state
            userInfo["state"] = new ParserNormalState();
            NotificationCenter.Instance.postNotification(new Notification("PlayerWillEnterState", this, userInfo));
            //shows where you're at currently
            this.informationMessage("\n" + this._currentRoom.description());
        }
        public void start(string state)
        {
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            if (state.Equals("battle"))
            {
                userInfo["state"] = new ParserBattleState();
            }
            else if (state.Equals("craft"))
            {
                userInfo["state"] = new ParserCraftState();
            }
            else if (state.Equals("trade"))
            {
                userInfo["state"] = new ParserTradeState();
            }
            NotificationCenter.Instance.postNotification(new Notification("PlayerWillEnterState", this, userInfo));
        }

        public void outputMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void coloredMessage(string message, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            outputMessage(message);
            Console.ForegroundColor = oldColor;
        }

        public void debugMessage(string message)
        {
            coloredMessage(message, ConsoleColor.DarkRed);
        }

        public void warningMessage(string message)
        {
            coloredMessage(message, ConsoleColor.Red);
        }

        public void informationMessage(string message)
        {
            coloredMessage(message, ConsoleColor.Green);
        }

        //back command
        public void back()
        {
            if (forBack.Count > 0)
            {
                currentRoom = forBack[forBack.Count - 1];
                forBack.Remove(forBack[forBack.Count - 1]);
                this.informationMessage("\n" + this._currentRoom.description());
            }
            else
            {
                this.outputMessage("\n\nThere is no where to return to");
            }
        }

        //Quest objective command
        public void quest()
        {
            informationMessage("Find the key to escape the Morio Mansion");
            //add an if statement for if player has key
        }
        /*
        public void use(string direction)
        {
            Door door = this._currentRoom.getExit(direction);
            if (door != null)
            {
                if(key == true)
                {

                }
                if (door.State == OCState.Open)
                {
                    this.warningMessage("\nThe door on " + direction + " is already open.");
                }
                else
                {
                    if (door.open().State == OCState.Open)
                    {
                        this.informationMessage("\nThere door on  " + direction + " is now open.");
                    }
                    else
                    {
                        this.warningMessage("\nThe door on " + direction + " did NOT open.");
                    }

                }

            }
            else
            {
                this.warningMessage("\nThere is no door on " + direction);
            }
        }

        */

    }

}
