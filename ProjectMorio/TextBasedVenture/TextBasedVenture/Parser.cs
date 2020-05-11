using System.Collections;
using System.Collections.Generic;
using System;

namespace TextBasedVentures
{
    //parser state
    public enum ParserState {Normal, Battle, Trade, Craft, Character, Key }
    public interface IParserState
    {
        //takes parser state
        ParserState State { get; }
        void Enter(Parser parser);
        void Exit(Parser parser);
    }
    public class ParserNormalState : IParserState
    {
        //normal state
        public ParserState State { get { return ParserState.Normal; } }
        public ParserNormalState()
        {

        }
        public void Enter(Parser parser)
        {
            //new commands for when you enter a different state
            //Command[] commandArray = { new QuitCommand(), new SayCommand() };
            //pushes new commands 
            //parser.Push(new CommandWords(commandArray));
        }
        public void Exit(Parser parser)
        {
            //removes the new set of commands pushed, back to normal state
            //parser.Pop();
        }
    }
    public class ParserBattleState : IParserState
    {
        //battle state
        public ParserState State { get { return ParserState.Battle; } }
        public ParserBattleState()
        {
            
        }
        public void Enter(Parser parser)
        {
            //new commands for when you enter a different state
            Command[] commandArray = {new QuitCommand(), new ExitCommand() };
            //pushes new commands 
            parser.Push(new CommandWords(commandArray));
    }
        public void Exit(Parser parser)
        {
            //removes the new set of commands pushed, back to normal state
            parser.Pop();
        }
    }
    public class ParserTradeState : IParserState
    {
        //trade state
        public ParserState State { get { return ParserState.Trade; } }
        public ParserTradeState()
        {

        }
        public void Enter(Parser parser)
        {
            //new commands for when you enter a different state
            Command[] commandArray = { new QuitCommand(), new SayCommand(), new ExitCommand() };
            //pushes new commands 
            parser.Push(new CommandWords(commandArray));
        }
        public void Exit(Parser parser)
        {
            //removes the new set of commands pushed, back to normal state
            parser.Pop();
        }
    }
    public class ParserCraftState : IParserState
    {
        //craft state
        public ParserState State { get { return ParserState.Craft; } }
        public ParserCraftState()
        {

        }
        public void Enter(Parser parser)
        {
            //new commands for when you enter a different state
            Command[] commandArray = { new QuitCommand(), new ExitCommand() };
            //pushes new commands 
            parser.Push(new CommandWords(commandArray));
        }
        public void Exit(Parser parser)
        {
            //removes the new set of commands pushed, back to normal state
            parser.Pop();
        }

    }
    public class ParserCharacterState : IParserState
    {
        //craft state
        public ParserState State { get { return ParserState.Character; } }
        public ParserCharacterState()
        {

        }
        public void Enter(Parser parser)
        {
            //new commands for when you enter a different state
            Command[] commandArray = { new QuitCommand(), new NameCommand(), new ExitCommand(), new GoCommand(), new HelpCommand(), new OpenCommand(), new PickupCommand(), new DropCommand(), new QuestCommand()};
            //pushes new commands 
            parser.Push(new CommandWords(commandArray));
        }
        public void Exit(Parser parser)
        {
            //removes the new set of commands pushed, back to normal state
            parser.Pop();
        }
    }

    //key state
    public class ParserKeyState : IParserState
    {
        public ParserState State { get { return ParserState.Key; } }

        public ParserKeyState()
        {

        }
        public void Enter(Parser parser)
        {
            //new commands for when you enter a different state
            Command[] commandArray = { new QuitCommand(), new NameCommand(), new ExitCommand(), new UseCommand() };
            //pushes new commands 
            parser.Push(new CommandWords(commandArray));
        }
        public void Exit(Parser parser)
        {
            //removes the new set of commands pushed, back to normal state
            parser.Pop();
        }
    }

    public class Parser
    {
        private Stack <CommandWords> commands;
        private IParserState _state;
        public IParserState State
        {
            get
            {
                return _state;
            }
            set
            {
                //exits old state
                _state.Exit(this);
                //enter value of new state
                _state = value;
                //enters new state, puts commands in the parser. new set of commands avaliable
                _state.Enter(this);
            }
        }

        public Parser() : this(new CommandWords())
        {

        }

        public Parser(CommandWords newCommands)
        {
            //commands moved to a stack
            commands = new Stack<CommandWords>();
            _state = new ParserNormalState();
            //commands = newCommands;
            Push(newCommands);
            //pushing the new commands
            NotificationCenter.Instance.addObserver("PlayerWillEnterState", PlayerWillEnterState);
        }
        public void PlayerWillEnterState(Notification notification)
        {
            Player player = (Player)notification.Object;
            Dictionary<string, Object> userInfo = notification.userInfo;
            IParserState state = (IParserState)userInfo["state"];
            if(State.State == ParserState.Character)
            {
                player.currentRoom = GameWorld.Instance.Entrance;
            }
            State = state;

        }
        //accessibility to the stack, pushes commands into stacks
        public void Push(CommandWords newCommands)
        {
            commands.Push(newCommands);
        }

        //pops command out of stack
        public void Pop()
        {
            commands.Pop();
        }

        public Command parseCommand(string commandString)
        {
            Command command = null;
            string[] words = commandString.Split(' ');
            if (words.Length > 0)
            {
                //use peek to take a look at the command words at the top of the stack
                //returns the object to the top of the stack, doesnt remove it
                command = commands.Peek().get(words[0]);
                if (command != null)
                {
                    if (words.Length > 1)
                    {
                        command.secondWord = words[1];
                    }
                    else
                    {
                        command.secondWord = null;
                    }
                }
                else
                {
                    Console.WriteLine(">>>Did not find the command " + words[0]);
                }
            }
            else
            {
                Console.WriteLine("No words parsed!");
            }
            return command;
        }

        public string description()
        {
            return commands.Peek().description();
        }
    }
}
