using System;

namespace TextBasedVentures
{
	public class OpenCommand : Command
	{
		public OpenCommand()
		{
			this.name = "open";
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
                player.warningMessage("\nOpen What?");
            }
            return false;
        }
    }
}


