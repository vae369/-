using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mir.Models.Forum
{
    public class UserInfo
    {
        public int Uid { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        public string PassWord { get; set; }
        /// <summary>
        /// 回复数
        /// </summary>
        public int ReplyCount { get; set; }
        /// <summary>
        /// 帖子数
        /// </summary>
        public int ThreadCount { get; set; }
        /// <summary>
        /// 验证方式
        /// </summary>
        public string AuthMethod { get; set; }
        /// <summary>
        /// 用户组
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public string RegistTime { get; set; }
        /// <summary>
        /// 最后访问时间
        /// </summary>
        public string FinalyTime { get; set; }
        /// <summary>
        /// 上次活动时间
        /// </summary>
        public string LastActiveTime { get; set; }
        /// <summary>
        /// 上次发表时间
        /// </summary>
        public string LastPushTime { get; set; }
        /// <summary>
        /// 注册IP
        /// </summary>
        public string RegistIP { get; set; }
        /// <summary>
        /// 上次访问IP
        /// </summary>
        public string LastIP { get; set; }
        public string space { get; set; }
        public int jf { get; set; }
        public int ww { get; set; }
        public int gx { get; set; }
        public int yb { get; set; }
        public int jb { get; set; }
        public int hdb { get; set; }

    }
}
