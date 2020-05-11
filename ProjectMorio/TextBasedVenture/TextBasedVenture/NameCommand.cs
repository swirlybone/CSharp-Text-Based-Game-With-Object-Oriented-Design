using System;

namespace TextBasedVentures
{
	public class NameCommand : Command
	{
		public NameCommand()
		{
            this.name = "name";
		}

        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.name(this.secondWord);
            }
            else
            {
                player.warningMessage("\nWhat name?");
            }
            return false;
        }
    }

}

