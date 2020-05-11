using System;

namespace TextBasedVentures
{
    public enum OCState { Open, Closed }
    public interface IOCState
    {
        OCState State { get; }
        IOCState open();
        IOCState close();
        IOCState keyclose();
    }

    public class KeycloseState : IOCState
    {
        public OCState State { get { return OCState.Open; } }
        public IOCState keyclose()
        {
            return this;
        }
        public IOCState open()
        {
            return new OpenState();
        }
        public IOCState close()
        {
            return this;
        }
    }

    public class OpenState : IOCState
    {
        public OCState State { get { return OCState.Open; } }
        public IOCState open()
        {
            return this;
        }
        public IOCState close()
        {
            return new ClosedState();
        }
        public IOCState keyclose()
        {
            return this;
        }
    }

    public class ClosedState : IOCState
    {
        public OCState State { get { return OCState.Closed;  } }
        public IOCState open()
        {
            return new OpenState();
        }
        public IOCState close()
        {
            return this;
        }
        public IOCState keyclose()
        {
            return this;
        }
    }
    public class Door : IOCState
    {
        private Room oneSide;
        private Room otherSide;
        private IOCState _state;
        public OCState State { get { return _state.State; } }

        public Door(Room one, Room two)
        {
            oneSide = one;
            otherSide = two;
            _state = new OpenState();
        }
        public Room getRoom(Room from)
        {
            if(from == oneSide)
            {
                return otherSide;
            }
            else
            {
                return oneSide;
            }
        }

        //these can close or open any specific door
        public IOCState open()
        {
            _state = _state.open();
            return _state;
        }
        public IOCState close()
        {
            _state = _state.close();
            return _state;
        }
        public IOCState keyclose()
        {
            _state = _state.keyclose();
            return _state;
        }

        //helper method
        public static Door MakeDoor(Room one, Room two, string oneLabel, string twoLabel) 
        {
            Door door = new Door(one, two);
            one.setExit(oneLabel, door);
            two.setExit(twoLabel, door);
            return door;
        }
    }
}