using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mir.Models
{
    public class Magic
    {
        public int MagID { get; set; }
        public string MagName { get; set; }
        public int EffectType { get; set; }
        public int Effect { get; set; }
        public int Spell { get; set; }
        public int Power { get; set; }
        public int MaxPower { get; set; }
        public int DefSpell { get; set; }
        public int DefPower { get; set; }
        public int DefMaxPower { get; set; }
        public int Job { get; set; }
        public dynamic NeedL1 { get; set; }
        public dynamic L1Train { get; set; }
        public dynamic NeedL2 { get; set; }
        public dynamic L2Train { get; set; }
        public dynamic NeedL3 { get; set; }
        public dynamic L3Train { get; set; }
        public dynamic Delay { get; set; }
        public string Descr { get; set; }
        public dynamic NeedL4 { get; set; }
        public dynamic L4Train { get; set; }
        public dynamic NeedL5 { get; set; }
        public dynamic L5Train { get; set; }
        public dynamic NeedL6 { get; set; }
        public dynamic L6Train { get; set; }
        public dynamic NeedL7 { get; set; }
        public dynamic L7Train { get; set; }
        public dynamic NeedL8 { get; set; }
        public dynamic L8Train { get; set; }
        public dynamic NeedL9 { get; set; }
        public dynamic L9Train { get; set; }
        public dynamic NeedL10 { get; set; }
        public dynamic L10Train { get; set; }
        public dynamic NeedL11 { get; set; }
        public dynamic L11Train { get; set; }
        public dynamic NeedL12 { get; set; }
        public dynamic L12Train { get; set; }
        public dynamic NeedL13 { get; set; }
        public dynamic L13Train { get; set; }
        public dynamic NeedL14 { get; set; }
        public dynamic L14Train { get; set; }
        public dynamic NeedL15 { get; set; }
        public dynamic L15Train { get; set; }
        public dynamic MaxTrainLv { get; set; }
        public dynamic CanUpgrade { get; set; }
        public dynamic MaxUpgradeLv { get; set; }
    }
}
