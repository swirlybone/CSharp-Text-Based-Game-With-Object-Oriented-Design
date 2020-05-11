using System;

namespace TextBasedVentures
{
	public class QuestCommand : Command
	{
		public QuestCommand() 
		{
			this.name = "quest";
		}
        override
       public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.warningMessage(this.secondWord + " isn't your quest");
            }
            else
            {
                player.quest();
            }
            return false;
        }
    }

}
