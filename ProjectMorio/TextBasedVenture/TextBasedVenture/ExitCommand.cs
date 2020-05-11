using System;

namespace TextBasedVentures
{
	public class ExitCommand : Command
	{
		public ExitCommand()
		{
			name = "done";
		}
        override
        public bool execute(Player player)
        {
            if (!this.hasSecondWord())
            {
                player.exit();
            }
            else
            {
                player.warningMessage("\nExit?" +secondWord + "???");
            }
            return false;
        }
    }
}

