using System.Collections;
using System.Collections.Generic;

namespace TextBasedVentures
{
    public class GoCommand : Command
    {

        public GoCommand() : base()
        {
            this.name = "go";
        }

        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.waltTo(this.secondWord);
            }
            else
            {
                player.warningMessage("\nGo Where?");
            }
            return false;
        }
    }
}
