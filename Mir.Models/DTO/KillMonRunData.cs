using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mir.Models.DTO
{
    public class KillItemsData
    {
        public string ItemsName { get; set; }
        public int ItemsCount { get; set; }
        /// <summary>
        /// 物品爆率
        /// </summary>
        public int ItemsBL { get; set; }

        public string MonName { get; set; }
        public int LoopCount { get; set; }
        public string MapCode { get; set; }
    }

    public class KillMonsData
    {
        public int ms_id { get; set; }
        public string MonName { get; set; }
        public string MapCode { get; set; }
        public int MonCount { get; set; }
        public int LoopCount { get; set; }
    }
    public class KillMapsData
    {
        public int md_id { get; set; }
        public string MapName { get; set; }
        public string MapCode { get; set; }
        public int MapCount { get; set; }
        public int LoopCount { get; set; }
    }
}
