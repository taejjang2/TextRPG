using System;
using static System.Net.Mime.MediaTypeNames;

namespace textRPG2
{
   
    enum GameMode
	{
		None,
		Robby,
		Town,
		Field,
		House
	}
	public class Game 
	{
		private Random rand = new Random();	

        private GameMode mode = GameMode.Robby;
		private Player player = null;
		private Monster monster = null;
        bool playerTurn = true;

        public void Process()
		{
			switch(mode)
			{
				case GameMode.Robby:
					ProcessRobby();
					break;

				case GameMode.Town:
					ProcessTown();
                    break;
				case GameMode.Field:
					ProcessField();
					break;
				case GameMode.House:
					ProcessHouse();
					break;
            }
		}
		
		public void ProcessRobby() 
		{
			Console.WriteLine("------직업 선택창------");
			Console.WriteLine("|   직업을 선택하세요 |");
			Console.WriteLine("|       1.Knight      |");
			Console.WriteLine("|       2.Archer      |");
			Console.WriteLine("|        3.Mage       |");
			Console.WriteLine("|_____________________|");
				string input = Console.ReadLine();

				switch (input)
				{
					case "1":
						player = new Knight();
						mode = GameMode.Town;
						Console.WriteLine("Knight를 선택했습니다.");
						break;

					case "2":
						player = new Archer();
						mode = GameMode.Town;
                        Console.WriteLine("Archer를 선택했습니다.");
                        break;

					case "3":
						player = new Mage();
						mode = GameMode.Town;
                        Console.WriteLine("Mage를 선택했습니다.");
                        break;
				}               
            
        }
		public void ProcessTown()
		{
			Console.WriteLine("-----------   마 을   -----------");
			Console.WriteLine("|       마을에 입장했습니다.    |");
            Console.WriteLine($"|      {player.Name()}  /  현재체력:{player.GetHp()}    |");
            Console.WriteLine("|1. 필드로 진입한다.            |");
			Console.WriteLine("|2. 캐릭터 선택창으로 돌아간다. |");
			Console.WriteLine("|3. 집으로 돌아가 회복한다.     |");
            Console.WriteLine("|_______________________________|");
            string input = Console.ReadLine();

				switch (input)
				{
					case "1":
						mode = GameMode.Field;
						break;

					case "2":
						mode = GameMode.Robby;
						break;
					case "3":
						mode = GameMode.House;
						break;
					
				}
			
		}	
		public void ProcessField() 
		{
            Console.WriteLine("---------   필 드   ---------");
            Console.WriteLine("|필드에 진입했습니다.       |");
			CreateRandomMonster();
            Console.WriteLine("|1. 전투 시작.              |");
            Console.WriteLine("|2. 일정 확률로 도망치기.   |");
            Console.WriteLine("|___________________________|");
            string input = Console.ReadLine();

			switch (input)
			{
				case "1":
					ProcessFight2();
					break;

				case "2":
					ProcessRun();
					break;
			}
        }

		private void CreateRandomMonster()
		{
           

            int randValue = rand.Next(1,4);

            switch (randValue)
            {
                case 1:
                    Console.WriteLine("|슬라임이 등장했습니다.     |");
                    monster = new Slime();
                    break;

                case 2:
                    Console.WriteLine("|오크가 등장했습니다.       |");
                    monster = new Orc();
                    break;

                case 3:
                    Console.WriteLine("|골렘이 등장했습니다.       |");
                    monster = new Golem();
                    break;

            }
        }

		public void ProcessRun()
		{
			
					int randValue = rand.Next(1, 101);

					if (randValue <= 33)
					{
						Console.WriteLine("도망에 성공했습니다.");
						mode = GameMode.Town;
						
					}

					else
					{
						Console.WriteLine("도망에 실패했습니다.");
						playerTurn = true;
						int damage = monster.GetAttack();
						player.onDamaged(damage);
						Console.WriteLine($"{monster.Name()}의 공격!");
						ProcessFight2();
					}
            
			
		}

        public void ProcessHouse() 
        {
            Console.WriteLine("---------   집   ---------");
            Console.WriteLine("집에 도착했습니다.");
            Console.WriteLine("체력을 회복합니다.");
			Console.WriteLine("-------------------------");
            int maxhp = player.Rest();
            player.Full_HP(maxhp);
			Console.WriteLine($"현재체력:{player.GetHp()}");
			mode = GameMode.Town;
        }

        public void ProcessFight()
		{
			while (true)
			{
				Console.WriteLine("플레이어의 턴");
				int damage = player.GetAttack();
				monster.onDamaged(damage);
				if (monster.isDead())
				{
					monster.GetExp();
					Console.WriteLine("전투에서 승리했습니다.");
					Console.WriteLine("1. 계속해서 싸운다.");
					Console.WriteLine("2. 마을로 돌아간다.");

					string input = Console.ReadLine();

					switch (input)
					{
						case "1":
							CreateRandomMonster();
							mode = GameMode.Field;
							break;
						case "2":
                            Console.WriteLine("마을로 돌아갑니다.");
							mode = GameMode.Town;
                            return;
                    }										
				}

                Console.WriteLine($"플레이어 HP:{player.GetHp()} 몬스터 HP:{monster.GetHp()}");

                Console.WriteLine("몬스터의 턴");
				damage = monster.GetAttack();
				player.onDamaged(damage);
                if (player.isDead())
				{
					Console.WriteLine("플레이어는 쓰러졌다!");
					Console.WriteLine("집으로 돌아가 체력을 회복합니다...");
					mode = GameMode.House;
					break;
				}
                Console.WriteLine($"플레이어 HP:{player.GetHp()} 몬스터 HP:{monster.GetHp()}");
            }
		}

        public void ProcessFight2()
        {
            Console.WriteLine($"{player.Name()} 체력: {player.GetHp()}/{player.Rest()} / {monster.Name()} 체력: {monster.GetHp()}/{monster.Rest()}");
            while (true)
			{
                
                Console.WriteLine("1. 일반공격");
                Console.WriteLine("2. 일정확률로 도망친다.");

                string input = Console.ReadLine();
				
				
                switch (input)
                {
                    case "1":

						if (playerTurn)
						{
							PlayerTurn();    
                        }
						if (playerTurn !=true)
						{
							MonsterTurn();
                        }


                        if (player.isDead())
                        {
                            Console.WriteLine("플레이어는 쓰러졌다!");
                            Console.WriteLine("집으로 돌아가 체력을 회복합니다...");
                            mode = GameMode.House;
                            return;
                        }

                        break;
                        
                        
                    case "2":
                        ProcessRun();
                        return;
                }
            }	
        }

		public void PlayerTurn()
		{
            int damage = player.GetAttack();
            monster.onDamaged(damage);
            Console.WriteLine($"{player.Name()}의 공격!");
            Console.WriteLine($"{player.Name()} 체력: {player.GetHp()}/{player.Rest()} | {monster.Name()} 체력: {monster.GetHp()}/{monster.Rest()}");
            playerTurn = false;
            if (monster.isDead())
            {
                Console.WriteLine($"{monster.Name()}을 쓰러뜨렸다!");
                Console.WriteLine("1. 계속해서 싸운다.");
                Console.WriteLine("2. 마을로 돌아간다.");
                string input2 = Console.ReadLine();
                switch (input2)
                {
                    case "1":
                        CreateRandomMonster();
                        playerTurn = true;
                        mode = GameMode.Field;
                        break;
                    case "2":
                        Console.WriteLine("마을로 돌아갑니다.");
                        mode = GameMode.Town;
                        return;
                }
            }
        }

		public void MonsterTurn()
		{
            int damage = monster.GetAttack();
            player.onDamaged(damage);
            Console.WriteLine($"{monster.Name()}의 공격!");
            Console.WriteLine($"{player.Name()} 체력: {player.GetHp()}/{player.Rest()} | {monster.Name()} 체력: {monster.GetHp()}/{monster.Rest()}");
            playerTurn = true;
        }
    }
}

