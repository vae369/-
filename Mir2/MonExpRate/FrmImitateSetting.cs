using Mir.Models.DTO;
using Mir.Models;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Documents;
using static System.Windows.Forms.ListBox;
using Mir.Core.cache;

namespace Mir2.MonExpRate
{
    public partial class FrmImitateSetting : UIForm2
    {
        DBInfo dBInfo = null;
        List<Monster> listMons = null;
        List<StdItems> listItems = null;
        List<string> listMaps = null;
        int type = 0;

        HttpRuntimeCache cache = new HttpRuntimeCache();

        public List<Monster> newMonsList = new List<Monster>();
        public List<StdItems> newItemsList = new List<StdItems>();
        public List<string> newMapsList = new List<string>();
        public FrmImitateSetting(int type, List<Monster> listMons, List<StdItems> listItems, List<string> listMaps, DBInfo dBInfo)
        {
            InitializeComponent();
            this.listMons = listMons;
            this.listItems = listItems;
            this.listMaps = listMaps;
            this.dBInfo = dBInfo;
            this.type = type;
        }

        private void FrmImitateSetting_Load(object sender, EventArgs e)
        {
            switch (type)
            {
                case 1: BindlbMonGroup(); break;//绑定怪物
                case 2: BindlbItemsGroup(); break;//绑定物品
                case 3: BindlbMapsGroup(); break;//绑定地图
            }
        }

        private void BindlbMapsGroup()
        {
            this.Text = "地图选择";
            uiTitlePanel1.Text = "已选择地图列表";
            uiTitlePanel2.Text = "未选择地图列表";

            newMapsList = cache.Get<List<string>>("newMapsList");
            if (newMapsList == null) newMapsList = new List<string>();
            newMapsList.ForEach(s => uiListBox1.Items.Add(s));
            listMaps.Except(newMapsList).ForEach(s => uiListBox2.Items.Add(s));
        }

        private void BindlbItemsGroup()
        {
            this.Text = "物品选择";
            uiTitlePanel1.Text = "已选择物品列表";
            uiTitlePanel2.Text = "未选择物品列表";

            newItemsList = cache.Get<List<StdItems>>("newItemsList");
            if (newItemsList == null) newItemsList = new List<StdItems>();
            newItemsList.ForEach(s => uiListBox1.Items.Add(s.Name));
            listItems.Except(newItemsList).ForEach(s => uiListBox2.Items.Add(s.Name));
        }

        private void BindlbMonGroup()
        {
            this.Text = "怪物选择";
            uiTitlePanel1.Text = "已选择怪物列表";
            uiTitlePanel2.Text = "未选择怪物列表";

            newMonsList = cache.Get<List<Monster>>("newMonsList");
            if (newMonsList == null) newMonsList = new List<Monster>();
            newMonsList.ForEach(s => uiListBox1.Items.Add(s.Name));
            listMons.Except(newMonsList).ForEach(s => uiListBox2.Items.Add(s.Name));
        }

        private void uiSymbolButton4_Click(object sender, EventArgs e)
        {
            switch (type)
            {
                case 1:
                    {
                        listMons.ForEach(s =>
                        {
                            if (!newMonsList.Contains(s))
                            {
                                uiListBox1.Items.Add(s.Name);
                                newMonsList.Add(s);
                            }
                        });
                    }
                    break;
                case 2:
                    {
                        listItems.ForEach(s =>
                        {
                            if (!newItemsList.Contains(s))
                            {
                                uiListBox1.Items.Add(s.Name);
                                newItemsList.Add(s);
                            }
                        });
                    }
                    break;
                case 3:
                    {
                        listMaps.ForEach(s =>
                        {
                            if (!newMapsList.Contains(s))
                            {
                                uiListBox1.Items.Add(s);
                                newMapsList.Add(s);
                            }
                        });
                    }
                    break;

            }
        }

        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {

            switch (type)
            {
                case 1:
                    {
                        if (newMonsList.Count == 0) return;
                        newMonsList.ForEach(s => uiListBox2.Items.Add(s.Name));
                        newMonsList = new List<Monster>();
                    }
                    break;
                case 2:
                    {
                        if (newItemsList.Count == 0) return;
                        newItemsList.ForEach(s => uiListBox2.Items.Add(s.Name));
                        newItemsList = new List<StdItems>();
                    }
                    break;
                case 3:
                    {
                        if (newMapsList.Count == 0) return;
                        newMapsList.ForEach(s => uiListBox2.Items.Add(s));
                        newMapsList = new List<string>();
                    }
                    break;

            }

        }

        private void uiSymbolButton5_Click(object sender, EventArgs e)
        {
            SelectedObjectCollection listbox2 = uiListBox2.SelectedItems;
            if (listbox2.Count == 0) return;
            switch (type)
            {
                case 1:
                    {
                        Monster item = null;
                        foreach (string name in listbox2)
                        {
                            item = listMons.Where(s => s.Name == name).FirstOrDefault();
                            newMonsList.Add(item);
                            uiListBox1.Items.Add(name);
                        }
                        newMonsList.ForEach(s => uiListBox2.Items.Remove(s.Name));
                    }
                    break;
                case 2:
                    {
                        StdItems item = null;
                        foreach (string name in listbox2)
                        {
                            item = listItems.Where(s => s.Name == name).FirstOrDefault();
                            newItemsList.Add(item);
                            uiListBox1.Items.Add(name);
                        }
                        newItemsList.ForEach(s => uiListBox2.Items.Remove(s.Name));
                    }
                    break;
                case 3:
                    {
                        string item = null;
                        foreach (string name in listbox2)
                        {
                            item = listMaps.Where(s => s == name).FirstOrDefault();
                            newMapsList.Add(item);
                            uiListBox1.Items.Add(name);
                        }
                        newMapsList.ForEach(s => uiListBox2.Items.Remove(s));
                    }
                    break;
            }


        }

        private void uiSymbolButton19_Click(object sender, EventArgs e)
        {
            SelectedObjectCollection listbox1 = uiListBox1.SelectedItems;
            if (listbox1.Count == 0) return;

            switch (type)
            {
                case 1:
                    {
                        Monster item = null;
                        foreach (string name in listbox1)
                        {
                            item = listMons.Where(s => s.Name == name).FirstOrDefault();
                            newMonsList.Remove(item);
                            uiListBox2.Items.Add(name);
                        }

                        uiListBox1.Items.Clear();
                        newMonsList.ForEach(s => uiListBox1.Items.Add(s.Name));
                    }
                    break;
                case 2:
                    {
                        StdItems item = null;
                        foreach (string name in listbox1)
                        {
                            item = listItems.Where(s => s.Name == name).FirstOrDefault();
                            newItemsList.Remove(item);
                            uiListBox2.Items.Add(name);
                        }

                        uiListBox1.Items.Clear();
                        newItemsList.ForEach(s => uiListBox1.Items.Add(s.Name));
                    }
                    break;
                case 3:
                    {
                        string item = null;
                        foreach (string name in listbox1)
                        {
                            item = listMaps.Where(s => s == name).FirstOrDefault();
                            newMapsList.Remove(item);
                            uiListBox2.Items.Add(name);
                        }

                        uiListBox1.Items.Clear();
                        newMapsList.ForEach(s => uiListBox1.Items.Add(s));
                    }
                    break;
            }

        }

        private void uiTextBox1_TextChanged(object sender, EventArgs e)
        {
            uiListBox1.Items.Clear();
            switch (type)
            {
                case 1:
                    {
                        var list = newMonsList.Where(s => s.Name.Contains(uiTextBox1.Text)).ToList();
                        list.ForEach(s => uiListBox1.Items.Add(s.Name));
                    }
                    break;
                case 2:
                    {
                        var list = newItemsList.Where(s => s.Name.Contains(uiTextBox1.Text)).ToList();
                        list.ForEach(s => uiListBox1.Items.Add(s.Name));
                    }
                    break;
                case 3:
                    {
                        var list = newMapsList.Where(s => s.Contains(uiTextBox1.Text)).ToList();
                        list.ForEach(s => uiListBox1.Items.Add(s));
                    }
                    break;
            }

        }

        private void uiTextBox2_TextChanged(object sender, EventArgs e)
        {
            uiListBox2.Items.Clear();
            switch (type)
            {
                case 1:
                    {
                        var list = listMons.Except(newMonsList).Where(s => s.Name.Contains(uiTextBox2.Text)).ToList();
                        list.ForEach(s => uiListBox2.Items.Add(s.Name));
                    }
                    break;
                case 2:
                    {
                        var list = listItems.Except(newItemsList).Where(s => s.Name.Contains(uiTextBox2.Text)).ToList();
                        list.ForEach(s => uiListBox2.Items.Add(s.Name));
                    }
                    break;
                case 3:
                    {
                        var list = listMaps.Except(newMapsList).Where(s => s.Contains(uiTextBox2.Text)).ToList();
                        list.ForEach(s => uiListBox2.Items.Add(s));
                    }
                    break;
            }

        }

        private void uiSymbolButton2_Click(object sender, EventArgs e)
        {
            switch (type)
            {
                case 1:
                    {
                        if (cache.ContainsKey<List<Monster>>("newMonsList")) cache.Remove<List<Monster>>("newMonsList");
                        cache.Add<List<Monster>>("newMonsList", newMonsList);
                    }
                    break;
                case 2:
                    {
                        if (cache.ContainsKey<List<StdItems>>("newItemsList")) cache.Remove<List<StdItems>>("newItemsList");
                        cache.Add<List<StdItems>>("newItemsList", newItemsList);
                    }
                    break;
                case 3:
                    {
                        if (cache.ContainsKey<List<string>>("newMapsList")) cache.Remove<List<string>>("newMapsList");
                        cache.Add<List<string>>("newMapsList", newMapsList);
                    }
                    break;
            }


            this.DialogResult = DialogResult.OK;
        }

        private void uiSymbolButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
