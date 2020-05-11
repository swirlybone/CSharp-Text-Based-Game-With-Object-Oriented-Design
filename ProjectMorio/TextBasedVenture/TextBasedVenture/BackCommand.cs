using System;

namespace TextBasedVentures
{
	public class BackCommand : Command
	{

		public BackCommand() : base()
		{
			this.name = "back";
		}

		override
		public bool execute(Player player)
		{
			if (this.hasSecondWord())
			{
				player.outputMessage("\nI cannot back" + this.secondWord + "");
			}
			else
			{
				player.back();
			}
			return false;
		}
	}
}

