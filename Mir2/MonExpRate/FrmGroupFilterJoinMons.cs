using Mir.Core.file;
using Mir.Models;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListBox;

namespace Mir2.MonExpRate
{
    public partial class FrmGroupFilterJoinMons : UIForm2
    {
        List<Monster> listMons = null;
        List<Monster> listLeftMonster = new List<Monster>();
        string DefMirPath = "";
        string MirPath = "";
        string GroupItemName = "";
        public FrmGroupFilterJoinMons(List<Monster> listMons, string defMirPath, string mirPath, string groupItemName)
        {
            InitializeComponent();

            this.listMons = listMons;
            DefMirPath = defMirPath;
            MirPath = mirPath;
            GroupItemName = groupItemName;
        }

        private void FrmGroupFilterJoinMons_Load(object sender, EventArgs e)
        {
            BindlbMonGroup();
        }
        private void BindlbMonGroup()
        {
            string path = $"{DefMirPath}\\Mons.txt";
            List<string> list = FileHelper.ReadTxtReturnList(path);
            uiListBox1.Items.Clear();
            if (list != null)
            {
                list.ForEach(item =>
                {
                    uiListBox1.Items.Add(item);
                    listLeftMonster.Add(new Monster() { Name = item });

                    var its = listMons.Where(t => t.Name == item).FirstOrDefault();
                    listMons.Remove(its);
                });
            }
            string monsPath = MirPath + "\\Mir200\\Envir\\咕咕鸡配置\\Group\\Mons.txt";
            List<string> monslist = FileHelper.ReadTxtReturnList(monsPath);
            if (monslist != null)
            {

                monslist.ForEach(s =>
                {
                    if (!listMons.Where(t => t.Name == "[分组]" + s).Any())
                        listMons.Insert(0, new Monster { Name = "[分组]" + s });
                });
            }


            listMons.ForEach(s => uiListBox2.Items.Add(s.Name));
        }

        private void uiTextBox1_TextChanged(object sender, EventArgs e)
        {
            uiListBox1.Items.Clear();
            var newList = listLeftMonster.Where(s => s.Name.Contains(uiTextBox1.Text.Trim())).ToList();
            newList.ForEach(s => uiListBox1.Items.Add(s.Name));
        }

        private void uiTextBox2_TextChanged(object sender, EventArgs e)
        {
            uiListBox2.Items.Clear();
            var newList = listMons.Where(s => s.Name.Contains(uiTextBox2.Text.Trim())).ToList();
            newList.ForEach(s => uiListBox2.Items.Add(s.Name));
        }

        private void uiSymbolButton4_Click(object sender, EventArgs e)
        {
            var newList = new List<Monster>();
            foreach (var item in uiListBox2.Items)
            {
                newList.Add(new Monster { Name = item.ToString() });
            }
            newList.ForEach(s =>
            {
                if (!listLeftMonster.Contains(s))
                {
                    listLeftMonster.Add(s);
                    uiListBox1.Items.Add(s.Name);
                    var item = listMons.Where(t => t.Name == s.Name).FirstOrDefault();
                    listMons.Remove(item);
                }
            });
            uiListBox2.Items.Clear();
        }

        private void uiSymbolButton5_Click(object sender, EventArgs e)
        {
            SelectedObjectCollection listbox2 = uiListBox2.SelectedItems;
            if (listbox2.Count == 0) return;

            var newList = new List<Monster>();
            Monster item = null;
            foreach (string name in listbox2)
            {
                item = new Monster()
                {
                    Name = name,
                };

                if (!listLeftMonster.Contains(item))
                {
                    newList.Add(item);
                    listLeftMonster.Add(item);
                    uiListBox1.Items.Add(name);
                    var items = listMons.Where(t => t.Name == name).FirstOrDefault();
                    listMons.Remove(items);
                }
            }

            foreach (Monster std in newList)
            {
                uiListBox2.Items.Remove(std.Name);
            }
        }

        private void uiSymbolButton19_Click(object sender, EventArgs e)
        {
            SelectedObjectCollection listbox1 = uiListBox1.SelectedItems;
            if (listbox1.Count == 0) return;

            var newList = new List<Monster>();
            Monster item = null;
            foreach (string name in listbox1)
            {
                item = new Monster()
                {
                    Name = name,
                };

                if (!listMons.Contains(item))
                {
                    newList.Add(item);
                    listMons.Add(item);
                    uiListBox2.Items.Add(name);
                    var items = listLeftMonster.Where(t => t.Name == name).FirstOrDefault();
                    listLeftMonster.Remove(items);
                }
            }

            foreach (Monster std in newList)
            {
                uiListBox1.Items.Remove(std.Name);
            }
        }

        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            var newList = new List<Monster>();
            foreach (var item in uiListBox1.Items)
            {
                newList.Add(new Monster { Name = item.ToString() });
            }
            newList.ForEach(s =>
            {
                if (!listMons.Contains(s))
                {
                    listMons.Add(s);
                    uiListBox2.Items.Add(s.Name);
                    var item = listLeftMonster.Where(t => t.Name == s.Name).FirstOrDefault();
                    listLeftMonster.Remove(item);
                }

            });
            uiListBox1.Items.Clear();
        }

        private void uiSymbolButton2_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(DefMirPath))
            {
                Directory.CreateDirectory(DefMirPath);
            }
            string path = $"{DefMirPath}\\Mons.txt";
            if (File.Exists(path))
            {
                FileHelper.DeleteFile(path);
            }
            this.Tag = listLeftMonster.Count;

            StreamWriter sw = new StreamWriter(path); //保存到指定路径

            foreach (Monster mon in listLeftMonster)
            {
                if (!mon.Name.StartsWith("[分组]"))
                {
                    sw.Write("\r\n" + mon.Name);
                }
                else
                {
                    string name = mon.Name.Replace("[分组]", "");
                    path = MirPath + $"\\Mir200\\Envir\\咕咕鸡配置\\Group\\Mons\\{name}.txt";
                    List<string> list = FileHelper.ReadTxtReturnList(path);
                    list.ForEach(s => sw.Write("\r\n" + s));
                }
            }

            sw.Flush();
            sw.Close();
            this.DialogResult = DialogResult.OK;
            this.ShowSuccessTip("关联成功！");
        }

        private void uiSymbolButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
