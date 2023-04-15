using System;
using System.Numerics;

namespace textRPG2
{
    public enum CreatureType
    {
        none =0,
        Player = 1,
        Monster =2

    }
	public class Creature
	{
        CreatureType type;

        protected Creature(CreatureType type)
        {
            this.type = type;
        }

        protected int hp;
        protected string name;
        protected int maxhp;
        protected int attack;
        protected int MonsterExp;
        protected int exp;
        protected int HPportion;
        public int GetHp() { return hp; }
        public int GetAttack() { return attack; }
        public int Rest() { return maxhp; }
        public int GetExp() { return MonsterExp; }
        public bool isDead() { return hp <= 0; }
        public string Name() { return name; }

        public void Full_HP(int maxhp)
        {
            maxhp = Rest();
            this.hp = maxhp;
        }

        public void ReturnClass(string name)
        {
            this.name = name;
        }

        public void GetExp(int exp)
        {
            this.exp = exp;
        }
        
        public void onDamaged(int damage)
        {
            hp -= damage;
            if (hp < 0)
                hp = 0;
        }
        public void SetInFor(int hp, int attack,int maxhp,int exp)
        {
            this.hp = hp;
            this.attack = attack;
            this.maxhp = maxhp;
            this.MonsterExp = exp;
        }
    }
}

