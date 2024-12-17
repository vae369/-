using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mir.Models.DTO
{
    public class UpdateFile
    {
        /// <summary>
        /// 文件夹列表
        /// </summary>
        public List<string> DirectoryList { get; set; } = new List<string>();
        /// <summary>
        /// 文件列表
        /// </summary>
        public List<FileType> FilesinfoList { get; set; } = new List<FileType>();
        /// <summary>
        /// 服务器版本号
        /// </summary>
        public string Version { get; set; }

    }

    public class FileType
    {
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 哈希值
        /// </summary>
        public string Hashs { get; set; }
    }
}
