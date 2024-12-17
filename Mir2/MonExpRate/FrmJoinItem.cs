using Mir.Core.file;
using Mir.Models;
using Spire.Xls.Core;
using Sunny.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using static Mysqlx.Datatypes.Scalar.Types;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.ListBox;

namespace Mir2.MonExpRate
{
    public partial class FrmJoinItem : UIForm2
    {
        List<StdItems> listItems = null;
        List<StdItems> listLeftStdItems = new List<StdItems>();
        string MirPath = "";
        string GroupItemName = "";
        public FrmJoinItem(DataTable stdItems, string mirPath, string groupItemName)
        {
            InitializeComponent();
            listItems = stdItems
                .AsEnumerable()
                .Select(s =>
                            new Mir.Models.StdItems
                            {
                                Name = s.Field<string>("Name")
                            }
                        ).ToList();
            MirPath = mirPath + "\\Mir200\\Envir\\咕咕鸡配置\\Group\\Items";
            GroupItemName = groupItemName;
        }

        private void FrmJoinItem_Load(object sender, EventArgs e)
        {
            BindlbItemGroup();
        }
        private void BindlbItemGroup()
        {
            string path = $"{MirPath}\\{GroupItemName}.txt";
            List<string> list = FileHelper.ReadTxtReturnList(path);
            uiListBox1.Items.Clear();
            if (list != null)
            {
                list.ForEach(item =>
                {
                    uiListBox1.Items.Add(item);
                    listLeftStdItems.Add(new StdItems() { Name = item });

                    var its = listItems.Where(t => t.Name == item).FirstOrDefault();
                    listItems.Remove(its);
                });
            }

            listItems.ForEach(s =>
            {
                if (!string.IsNullOrEmpty(s.Name))
                    uiListBox2.Items.Add(s.Name);
            });
        }

        private void uiTextBox2_TextChanged(object sender, EventArgs e)
        {
            uiListBox2.Items.Clear();
            var newList = listItems.Where(s => s.Name.Contains(uiTextBox2.Text.Trim())).ToList();
            newList.ForEach(s => uiListBox2.Items.Add(s.Name));
        }

        private void uiSymbolButton4_Click(object sender, EventArgs e)
        {
            var newList = new List<StdItems>();
            foreach (var item in uiListBox2.Items)
            {
                newList.Add(new StdItems { Name = item.ToString() });
            }
            newList.ForEach(s =>
            {
                if (!listLeftStdItems.Contains(s))
                {
                    listLeftStdItems.Add(s);
                    uiListBox1.Items.Add(s.Name);
                    var item = listItems.Where(t => t.Name == s.Name).FirstOrDefault();
                    listItems.Remove(item);
                }
            });
            uiListBox2.Items.Clear();
        }

        private void uiTextBox1_TextChanged(object sender, EventArgs e)
        {
            uiListBox1.Items.Clear();
            var newList = listLeftStdItems.Where(s => s.Name.Contains(uiTextBox1.Text.Trim())).ToList();
            newList.ForEach(s => uiListBox1.Items.Add(s.Name));
        }

        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            var newList = new List<StdItems>();
            foreach (var item in uiListBox1.Items)
            {
                newList.Add(new StdItems { Name = item.ToString() });
            }
            newList.ForEach(s =>
            {
                if (!listItems.Contains(s))
                {
                    listItems.Add(s);
                    uiListBox2.Items.Add(s.Name);
                    var item = listLeftStdItems.Where(t => t.Name == s.Name).FirstOrDefault();
                    listLeftStdItems.Remove(item);
                }

            });
            uiListBox1.Items.Clear();
        }

        private void uiSymbolButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uiSymbolButton5_Click(object sender, EventArgs e)
        {
            SelectedObjectCollection listbox2 = uiListBox2.SelectedItems;
            if (listbox2.Count == 0) return;

            var newList = new List<StdItems>();
            StdItems item = null;
            foreach (string name in listbox2)
            {
                item = new StdItems()
                {
                    Name = name,
                };

                if (!listLeftStdItems.Contains(item))
                {
                    newList.Add(item);
                    listLeftStdItems.Add(item);
                    uiListBox1.Items.Add(name);
                    var items = listItems.Where(t => t.Name == name).FirstOrDefault();
                    listItems.Remove(items);
                }
            }

            foreach (StdItems std in newList)
            {
                uiListBox2.Items.Remove(std.Name);
            }
        }

        private void uiSymbolButton19_Click(object sender, EventArgs e)
        {
            SelectedObjectCollection listbox1 = uiListBox1.SelectedItems;
            if (listbox1.Count == 0) return;

            var newList = new List<StdItems>();
            StdItems item = null;
            foreach (string name in listbox1)
            {
                item = new StdItems()
                {
                    Name = name,
                };

                if (!listItems.Contains(item))
                {
                    newList.Add(item);
                    listItems.Add(item);
                    uiListBox2.Items.Add(name);
                    var items = listLeftStdItems.Where(t => t.Name == name).FirstOrDefault();
                    listLeftStdItems.Remove(items);
                }
            }

            foreach (StdItems std in newList)
            {
                uiListBox1.Items.Remove(std.Name);
            }
        }

        private void uiSymbolButton2_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(MirPath))
            {
                Directory.CreateDirectory(MirPath);
            }
            string path = $"{MirPath}\\{GroupItemName}.txt";
            if (File.Exists(path))
            {
                FileHelper.DeleteFile(MirPath);
            }
            //File.WriteAllText(path, string.Join("\r\n", listLeftStdItems), Encoding.UTF8);

            StreamWriter sw = new StreamWriter(path); //保存到指定路径
            sw.Write(string.Join("\r\n", listLeftStdItems.Select(s => s.Name)));
            sw.Flush();
            sw.Close();
            this.DialogResult = DialogResult.OK;
            this.ShowSuccessTip("关联成功！");
        }
    }
}
