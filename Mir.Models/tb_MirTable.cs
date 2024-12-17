using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mir.Models
{
    public class tb_MirTable
    {
        public string TableName { get; set; }
        public List<tb_MirColumn> Columns { get; set; }
        public List<string> MirData { get; set; }
        public DataTable MirDataTable { get; set; }
        public Dictionary<string, int> TableKv = new Dictionary<string, int>();

        public tb_MirTable()
        {
            TableKv.Add("magic_magid", 1);
            TableKv.Add("magic_magname", 2);
            TableKv.Add("magic_effecttype", 3);
            TableKv.Add("magic_effect", 4);
            TableKv.Add("magic_spell", 5);
            TableKv.Add("magic_power", 6);
            TableKv.Add("magic_maxpower", 7);
            TableKv.Add("magic_defspell", 8);
            TableKv.Add("magic_defpower", 9);
            TableKv.Add("magic_defmaxpower", 10);
            TableKv.Add("magic_job", 11);
            TableKv.Add("magic_needl1", 12);
            TableKv.Add("magic_l1train", 13);
            TableKv.Add("magic_needl2", 14);
            TableKv.Add("magic_l2train", 15);
            TableKv.Add("magic_needl3", 16);
            TableKv.Add("magic_l3train", 17);
            TableKv.Add("magic_needl4", 18);
            TableKv.Add("magic_l4train", 19);
            TableKv.Add("magic_needl5", 20);
            TableKv.Add("magic_l5train", 21);
            TableKv.Add("magic_needl6", 22);
            TableKv.Add("magic_l6train", 23);
            TableKv.Add("magic_needl7", 24);
            TableKv.Add("magic_l7train", 25);
            TableKv.Add("magic_needl8", 26);
            TableKv.Add("magic_l8train", 27);
            TableKv.Add("magic_needl9", 28);
            TableKv.Add("magic_l9train", 29);
            TableKv.Add("magic_needl10", 30);
            TableKv.Add("magic_l10train", 31);
            TableKv.Add("magic_needl11", 32);
            TableKv.Add("magic_l11train", 33);
            TableKv.Add("magic_needl12", 34);
            TableKv.Add("magic_l12train", 35);
            TableKv.Add("magic_needl13", 36);
            TableKv.Add("magic_l13train", 37);
            TableKv.Add("magic_needl14", 38);
            TableKv.Add("magic_l14train", 39);
            TableKv.Add("magic_needl15", 40);
            TableKv.Add("magic_l15train", 41);
            TableKv.Add("magic_delay", 42);
            TableKv.Add("magic_descr", 43);
            TableKv.Add("magic_maxtrainlv", 44);
            TableKv.Add("magic_canupgrade", 44);
            TableKv.Add("magic_maxupgradelv", 44);

            TableKv.Add("monster_name", 1);
            TableKv.Add("monster_race", 2);
            TableKv.Add("monster_raceimg", 3);
            TableKv.Add("monster_appr", 4);
            TableKv.Add("monster_lvl", 5);
            TableKv.Add("monster_undead", 6);
            TableKv.Add("monster_cooleye", 7);
            TableKv.Add("monster_exp", 8);
            TableKv.Add("monster_hp", 9);
            TableKv.Add("monster_mp", 10);
            TableKv.Add("monster_ac", 11);
            TableKv.Add("monster_mac", 12);
            TableKv.Add("monster_dc", 13);
            TableKv.Add("monster_dcmax", 14);
            TableKv.Add("monster_mc", 15);
            TableKv.Add("monster_sc", 16);
            TableKv.Add("monster_speed", 17);
            TableKv.Add("monster_hit", 18);
            TableKv.Add("monster_walk_spd", 19);
            TableKv.Add("monster_walkstep", 20);
            TableKv.Add("monster_walkwait", 21);
            TableKv.Add("monster_attack_spd", 22);


            TableKv.Add("stditems_idx", 1);
            TableKv.Add("stditems_name", 2);
            TableKv.Add("stditems_stdmode", 3);
            TableKv.Add("stditems_shape", 4);
            TableKv.Add("stditems_weight", 5);
            TableKv.Add("stditems_anicount", 6);
            TableKv.Add("stditems_source", 7);
            TableKv.Add("stditems_reserved", 8);
            TableKv.Add("stditems_looks", 9);
            TableKv.Add("stditems_duramax", 10);
            TableKv.Add("stditems_ac", 11);
            TableKv.Add("stditems_ac2", 12);
            TableKv.Add("stditems_mac", 13);
            TableKv.Add("stditems_mac2", 14);
            TableKv.Add("stditems_dc", 15);
            TableKv.Add("stditems_dc2", 16);
            TableKv.Add("stditems_mc", 17);
            TableKv.Add("stditems_mc2", 18);            
            TableKv.Add("stditems_sc", 19);
            TableKv.Add("stditems_sc2", 20);
            TableKv.Add("stditems_need", 21);
            TableKv.Add("stditems_needlevel", 22);
            TableKv.Add("stditems_price", 23);
            TableKv.Add("stditems_stock", 24);
            TableKv.Add("stditems_color", 25);
            TableKv.Add("stditems_overlap", 26);
            TableKv.Add("stditems_hp", 27);
            TableKv.Add("stditems_mp", 28);
            TableKv.Add("stditems_job", 29);
            TableKv.Add("stditems_value1", 30);
            TableKv.Add("stditems_value2", 31);
            TableKv.Add("stditems_value3", 32);
            TableKv.Add("stditems_value4", 33);
            TableKv.Add("stditems_value5", 34);
            TableKv.Add("stditems_value6", 35);
            TableKv.Add("stditems_value7", 36);
            TableKv.Add("stditems_value8", 37);
            TableKv.Add("stditems_value9", 38);
            TableKv.Add("stditems_value10", 39);
            TableKv.Add("stditems_value11", 40);
            TableKv.Add("stditems_value12", 41);
            TableKv.Add("stditems_value13", 42);
            TableKv.Add("stditems_value14", 43);
            TableKv.Add("stditems_value15", 44);
            TableKv.Add("stditems_value16", 45);
            TableKv.Add("stditems_value17", 46);
            TableKv.Add("stditems_value18", 47);
            TableKv.Add("stditems_value19", 48);
            TableKv.Add("stditems_value20", 49);
            TableKv.Add("stditems_value21", 50);
            TableKv.Add("stditems_value22", 51);
            TableKv.Add("stditems_value23", 52);
            TableKv.Add("stditems_value24", 53);
            TableKv.Add("stditems_value25", 54);
            TableKv.Add("stditems_value26", 55);
            TableKv.Add("stditems_value27", 56);
            TableKv.Add("stditems_value28", 57);
            TableKv.Add("stditems_value29", 58);
            TableKv.Add("stditems_value30", 59);
            TableKv.Add("stditems_insurancegold", 60);
            TableKv.Add("stditems_insurancecurrency", 61);            
        }
    }
    public class tb_MirColumn
    {
       
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public bool IsPrimarykey { get; set; }
        public int ColumnLength { get; set; }
        public string ColumnDescription { get; set; }
        public int SortCode { get; set; }
        public object ColumnData { get; set; }
    }
}
