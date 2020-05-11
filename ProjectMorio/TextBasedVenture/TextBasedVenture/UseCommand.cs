using System;

namespace TextBasedVentures
{
    public class UseCommand : Command
    {
        public UseCommand()
        {
            this.name = "use";
        }
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.open(this.secondWord);
            }
            else
            {
                player.warningMessage("\nUse What?");
            }
            return false;
        }
    }

}

