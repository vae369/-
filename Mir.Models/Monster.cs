using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mir.Models
{
    public class Monster
    {
        public string Name { get; set; }
        public int Race { get; set; }
        public int RaceImg { get; set; }
        public int Appr { get; set; }
        public int Lvl { get; set; }
        public int Undead { get; set; }
        public int CoolEye { get; set; }
        public int Exp { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int AC { get; set; }
        public int MAC { get; set; }
        public int DC { get; set; }
        public int DCMAX { get; set; }
        public int MC { get; set; }
        public int SC { get; set; }
        public int SPEED { get; set; }
        public int HIT { get; set; }
        public int WALK_SPD { get; set; }
        public int WalkStep { get; set; }
        public int WaLkWait { get; set; }
        public int ATTACK_SPD { get; set; }
        public int AttackState { get; set; }
        public int AttackSource { get; set; }
        public int ExploreItem { get; set; }
        public int DisableSimpleActor { get; set; }
    }
}

