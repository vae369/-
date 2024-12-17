using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mir.Models.Forum
{
    public class Topic
    {
        public int Id { get; set; }
        /// <summary>
        /// 帖子ID
        /// </summary>
        public int t_Id { get; set; }
        /// <summary>
        /// 帖子名称
        /// </summary>
        public string t_Name { get; set; }
        /// <summary>
        /// 引擎类型
        /// </summary>
        public string t_Engine { get; set;}
        /// <summary>
        /// 售后
        /// </summary>
        public string t_AfterSales { get; set; }
        /// <summary>
        /// 补丁大小
        /// </summary>
        public string t_PatchSize { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string t_Price { get; set; }
        /// <summary>
        /// 版本类型
        /// </summary>
        public string t_VersionType { get; set; }
        /// <summary>
        /// 下载次数
        /// </summary>
        public string t_DownCount { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string t_PublishTime { get; set; }
        /// <summary>
        /// 发布链接
        /// </summary>
        public string t_Link { get; set; }
        /// <summary>
        /// 发布者
        /// </summary>
        public string t_Publisher { get; set; }
    }
}
