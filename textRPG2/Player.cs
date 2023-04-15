using System;
namespace textRPG2
{
	public enum PlayerType
	{
		None=0,
		Knight=1,
		Mage=3,
		Archer=2

	}
	class Player : Creature
	{
		protected PlayerType type;
		
		protected Player(PlayerType type) : base(CreatureType.Player)
		{
			this.type = type;
			
		}
		
		
				
	}
	class Knight : Player
	{
		
		public Knight() : base(PlayerType.Knight)
		{
			SetInFor(100, 10,100,0);
            ReturnClass("전사");
        }
	}

	class Archer : Player
	{
        
        public Archer() : base(PlayerType.Archer)
		{
			SetInFor(75, 15,75,0);
            ReturnClass("궁수");
        }
	}

	class Mage : Player
	{
        
        public Mage() : base(PlayerType.Mage)
		{
			SetInFor(50, 20, 50,0);
			ReturnClass("마법사");
		}
	}
}

