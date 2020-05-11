using System;

namespace TextBasedVentures
{
    public class PickupCommand : Command
    {
        public PickupCommand() : base()
        {
            this.name = "pickup";
        }
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.pickup(this.secondWord);
                //if (this.secondWord.Equals("tree"))
                //{
                //player.warningMessage("\nCannot pick up " +this.secondWord);
                //}
                //else
                //{
                //player.pickup(this.secondWord);
                //}
            }
            else
            {
                player.warningMessage("\nPick up what?");
            }
            return false;
        }
    }
}

