using Mir.Core.file;
using Mir.Models.DTO;
using Spire.Xls.Core;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Mir2.MonExpRate
{
    public partial class FrmCretGroup : UIForm2
    {
        string type = "", MirPath = "", oldGroupName = "";
        int index = 0;
        public string ZDYGroupPath = "";
        public string GroupName = "";
        public FrmCretGroup(string type, string MirPath, string groupName = "", int index = 0)
        {
            InitializeComponent();
            this.type = type;
            this.MirPath = MirPath;
            this.index = index;
            switch (this.type)
            {
                case "item": this.Text = "新建物品分组"; break;
                case "mon": this.Text = "新建怪物分组"; break;
                case "itemRename":
                    this.Text = "重命名物品分组";
                    uiTextBox1.Text = groupName;
                    oldGroupName = groupName;
                    break;
                case "monsRename":
                    this.Text = "重命名怪物分组";
                    uiTextBox1.Text = groupName;
                    oldGroupName = groupName;
                    break;
                case "CreateGroup":
                    this.Text = "新建分组";
                    break;
                case "CreateSubgroup":
                    this.Text = "新建(" + groupName + ")子分组";
                    this.uiLabel1.Text = "请输入子分组名称：";
                    this.uiLabel1.Size = new Size(this.uiLabel1.Width + 20, this.uiLabel1.Height);
                    break;
                case "RenameGroupName":
                    this.Text = "重命名分组";
                    uiTextBox1.Text = groupName;
                    oldGroupName = groupName;
                    break;
                case "MonMagnificat":
                    this.Text = "请输入倍率倍数";
                    this.uiLabel1.Text = "200即当前爆率乘以2：";
                    this.uiLabel1.Size = new Size(this.uiLabel1.Width + 20, this.uiLabel1.Height);
                    uiTextBox1.Text = "100";
                    uiTextBox1.Width -= 20;
                    uiTextBox1.Location = new Point(uiTextBox1.Location.X + 20, uiTextBox1.Location.Y);
                    oldGroupName = groupName;
                    break;
            }
        }

        private void uiButton6_Click(object sender, EventArgs e)
        {
            string text = uiTextBox1.Text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                this.ShowErrorTip("分组名称不能为空！");
                return;
            }
            try
            {
                switch (this.type)
                {
                    case "item": SaveGroupItems(text); break;
                    case "mon": SaveGroupMon(text); break;
                    case "itemRename": RenameGroupItemName(text); break;
                    case "monsRename": RenameGroupMonsName(text); break;
                    case "CreateGroup": CustCreateGroup(text); break;
                    case "CreateSubgroup": CreateSubgroup(text); break;
                    case "RenameGroupName": RenameGroupName(text); break;
                    case "MonMagnificat": MonMagnificat(text); break;

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorTip(ex.Message);
            }

        }

        private void MonMagnificat(string text)
        {
            int mag = Convert.ToInt32(text);
            string newFileName = Path.GetDirectoryName(MirPath) + "\\" + oldGroupName;
            List<string> list = GetFiles(newFileName, new string[] { "*.txt" });
            string pattern = @"1/[0-9]+";
            foreach (var item in list)
            {
                List<string> listCont = FileHelper.ReadTxtReturnList(item);
                for (int i = 0; i < listCont.Count; i++)
                {
                    MatchCollection matches = Regex.Matches(listCont[i], pattern);
                    foreach (Match match in matches)
                    {
                        Console.WriteLine("Found: " + match.Value);
                        string[] arr = match.Value.Split(new char[] { '/' });
                        arr[1] = Math.Round(Convert.ToInt32(arr[1]) * mag / 100.0, 0).ToString();
                        string res = string.Join("/", arr);
                        listCont[i] = listCont[i].Replace(match.Value, res);
                    }

                }

                FileHelper.WriteAllText(item, string.Join("\r\n", listCont));
            }

            this.DialogResult = DialogResult.OK;
        }
        static List<string> GetFiles(string path, string[] patterns)
        {
            List<string> files = new List<string>();
            try
            {
                foreach (var pattern in patterns)
                {
                    foreach (var file in Directory.GetFiles(path, pattern))
                    {
                        if (file.Contains("Items.txt"))
                            files.Add(file);
                    }
                }
                foreach (var directory in Directory.GetDirectories(path))
                {
                    files.AddRange(GetFiles(directory, patterns));
                }
            }
            catch
            {

            }
            return files;
        }

        private void RenameGroupName(string text)
        {
            List<string> list = FileHelper.ReadTxtReturnList(MirPath);
            if (list.Contains(text))
                throw new Exception("分组名称已经存在！");

            list[index] = text;
            FileHelper.WriteAllText(MirPath, string.Join("\r\n", list));

            string originalFileName = Path.GetDirectoryName(MirPath) + $"\\{oldGroupName}";
            string newFileName = Path.GetDirectoryName(MirPath) + $"\\{text}";

            if (Directory.Exists(originalFileName))
                Directory.Move(originalFileName, newFileName);
            GroupName = text;
            ZDYGroupPath = newFileName;
            this.ShowSuccessTip("操作成功！");
            this.DialogResult = DialogResult.OK;
        }

        private void CreateSubgroup(string text)
        {
            string path = MirPath + "\\Groups.txt";

            List<string> list = FileHelper.ReadTxtReturnList(path);
            if (list.Contains(text))
                throw new Exception("分组名称已经存在！");

            string pathdir = MirPath + "\\" + text;
            Directory.CreateDirectory(pathdir);

            list.Add(text);
            ZDYGroupPath = pathdir;
            GroupName = text;
            FileHelper.WriteAllText(path, string.Join("\r\n", list));
            this.ShowSuccessTip("创建分组成功！");
            this.DialogResult = DialogResult.OK;
        }

        private void CustCreateGroup(string text)
        {
            string path = MirPath + "\\Groups.txt";

            List<string> list = FileHelper.ReadTxtReturnList(path);
            if (list.Contains(text))
                throw new Exception("分组名称已经存在！");

            string pathdir = MirPath + "\\" + text;
            Directory.CreateDirectory(pathdir);

            list.Add(text);
            ZDYGroupPath = pathdir;
            GroupName = text;
            FileHelper.WriteAllText(path, string.Join("\r\n", list));
            this.ShowSuccessTip("创建分组成功！");
            this.DialogResult = DialogResult.OK;
        }

        private void RenameGroupMonsName(string text)
        {
            List<string> list = FileHelper.ReadTxtReturnList(MirPath);
            if (list.Contains(text))
                throw new Exception("分组名称已经存在！");

            list[index] = text;
            FileHelper.WriteAllText(MirPath, string.Join("\r\n", list));

            string originalFileName = Path.GetDirectoryName(MirPath) + $"\\Mons\\{oldGroupName}.txt";
            string newFileName = Path.GetDirectoryName(MirPath) + $"\\Mons\\{text}.txt";
            if (File.Exists(originalFileName))
                File.Move(originalFileName, newFileName);

            this.ShowSuccessTip("操作成功！");
            this.DialogResult = DialogResult.OK;
        }

        private void SaveGroupMon(string text)
        {
            string path = MirPath + "\\Mir200\\Envir\\咕咕鸡配置\\Group";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!Directory.Exists(path + "\\Mons"))
            {
                Directory.CreateDirectory(path + "\\Mons");
            }
            //创建物品文件
            string monsPath = path + "\\Mons.txt";
            if (!File.Exists(monsPath))
            {
                using (FileStream fileStream = new FileStream(monsPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(text + "\r\n");
                    fileStream.Write(buffer, 0, buffer.Length);
                }
            }
            else
            {
                List<string> list = FileHelper.ReadTxtReturnList(monsPath);
                foreach (string fileName in list)
                {
                    if (fileName == text)
                        throw new Exception("分组名称已经存在！");
                }
                list.Add(text);

                FileHelper.DeleteFile(monsPath);
                FileHelper.WriteAllText(monsPath, string.Join("\r\n", list));
            }
            this.ShowSuccessTip("保存成功！");
            this.DialogResult = DialogResult.OK;
        }

        private void RenameGroupItemName(string text)
        {
            List<string> list = FileHelper.ReadTxtReturnList(MirPath);
            if (list.Contains(text))
                throw new Exception("分组名称已经存在！");

            list[index] = text;
            FileHelper.WriteAllText(MirPath, string.Join("\r\n", list));

            string originalFileName = Path.GetDirectoryName(MirPath) + $"\\Items\\{oldGroupName}.txt";
            string newFileName = Path.GetDirectoryName(MirPath) + $"\\Items\\{text}.txt";
            if (File.Exists(originalFileName))
                File.Move(originalFileName, newFileName);

            this.ShowSuccessTip("操作成功！");
            this.DialogResult = DialogResult.OK;
        }

        private void SaveGroupItems(string text)
        {
            string path = MirPath + "\\Mir200\\Envir\\咕咕鸡配置\\Group";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!Directory.Exists(path + "\\Items"))
            {
                Directory.CreateDirectory(path + "\\Items");
            }
            //创建物品文件
            string itemPath = path + "\\Items.txt";
            if (!File.Exists(itemPath))
            {
                using (FileStream fileStream = new FileStream(itemPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(text + "\r\n");
                    fileStream.Write(buffer, 0, buffer.Length);
                }
            }
            else
            {
                List<string> list = FileHelper.ReadTxtReturnList(itemPath);
                foreach (string fileName in list)
                {
                    if (fileName == text)
                        throw new Exception("分组名称已经存在！");
                }
                list.Add(text);

                FileHelper.DeleteFile(itemPath);
                FileHelper.WriteAllText(itemPath, string.Join("\r\n", list));
            }
            this.ShowSuccessTip("保存成功！");
            this.DialogResult = DialogResult.OK;
        }
        private void uiButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
