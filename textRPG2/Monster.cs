using System;
namespace textRPG2
{
    public enum MonsterType
    {
        None = 0,
        Slime = 1,
        Orc = 3,
        Golem = 2

    }
    class Monster : Creature
    {
        protected MonsterType type;
        protected Monster(MonsterType type) : base(CreatureType.Monster)
        {
            this.type = type;
        }



    }
    class Slime : Monster
    {
        public Slime() : base(MonsterType.Slime)
        {
            SetInFor(30, 5, 30,5);
            ReturnClass("슬라임");
        }
    }

    class Orc : Monster
    {
        public Orc() : base(MonsterType.Orc)
        {
            SetInFor(40, 7, 40,7);
            ReturnClass("오크");
        }
    }

    class Golem : Monster
    {
        public Golem() : base(MonsterType.Golem)
        {
            SetInFor(35, 13, 35,10);
            ReturnClass("골렘");
        }
    }
}

