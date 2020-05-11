using System;

namespace TextBasedVentures
{
	public class DropCommand : Command
	{
		public DropCommand()
		{
			this.name = "drop";
		}
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {

                player.drop(this.secondWord);
            }
            else
            {
                player.showInventory();
                player.warningMessage("\nDrop what?");
            }
            return false;
        }
    }

}
