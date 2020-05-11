using System;

namespace TextBasedVentures
{
	public class InventoryCommand : Command
	{
		public InventoryCommand() : base()
		{
            this.name = "inventory";
		}
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.warningMessage("\nI cannot inventory " + secondWord);
                
            }
            else
            {
                player.showInventory();
            }
            return false;
        }
    }


}
