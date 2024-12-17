using Mir.Core.file;
using Mir.Core.utils;
using Mir.Models;
using Mir.Models.DTO;
using Mir2.Helper;
using Sunny.UI;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Mir2.MonExpRate
{
    public partial class FrmExpRate : UIPage
    {
        DBInfo dBInfo = null;
        List<Monster> listMons = null;
        List<StdItems> listItems = null;

        List<string> listkillMon = new List<string>();
        List<string> listkillStd = new List<string>();
        List<string> listkillMap = new List<string>();

        //ConcurrentBag<KillItemsData> itemsResultList = new ConcurrentBag<KillItemsData>();
        List<KillItemsData> itemsResultList = new List<KillItemsData>();

        Dictionary<string, KillMapsData> listMaps = new Dictionary<string, KillMapsData>();
        Dictionary<string, List<KillMonsData>> dictMons = new Dictionary<string, List<KillMonsData>>();
        Dictionary<string, List<KillItemsData>> dictItems = new Dictionary<string, List<KillItemsData>>();


        public FrmExpRate(DataTable Monster, DataTable StdItems, DBInfo dBInfo)
        {
            InitializeComponent();

            listMons = Monster.AsEnumerable().Select(s => new Mir.Models.Monster { Name = s.Field<string>("Name") }).ToList();
            listItems = StdItems.AsEnumerable().Select(s => new Mir.Models.StdItems { Name = s.Field<string>("Name") }).ToList();
            this.dBInfo = dBInfo;
        }

        private void FrmExpRate_Load(object sender, EventArgs e)
        {
            rbAllMon.CheckedChanged += rb_ClickedChenged;
            rbAllItems.CheckedChanged += rb_ClickedChenged;
            rbAllMaps.CheckedChanged += rb_ClickedChenged;

            rbOtherMon.CheckedChanged += rb_ClickedChenged;
            rbOtherItems.CheckedChanged += rb_ClickedChenged;
            rbOtherMaps.CheckedChanged += rb_ClickedChenged;

            uiDataGridView1.SelectionChanged += uiDataGridView1_SelectionChanged;
            uiDataGridView2.SelectionChanged += uiDataGridView2_SelectionChanged;
            uiDataGridView3.SelectionChanged += uiDataGridView3_SelectionChanged;

            btnMon.Enabled = false;
            btnItems.Enabled = false;
            btnMaps.Enabled = false;

            txtPerson.Focus();
        }



        private void rb_ClickedChenged(object sender, EventArgs e)
        {
            UIRadioButton rbton = sender as UIRadioButton;
            switch (rbton.Name)
            {
                case "rbAllMon": btnMon.Enabled = false; break;
                case "rbAllItems": btnItems.Enabled = false; break;
                case "rbAllMaps": btnMaps.Enabled = false; break;
                case "rbOtherMon": btnMon.Enabled = true; break;
                case "rbOtherItems": btnItems.Enabled = true; break;
                case "rbOtherMaps": btnMaps.Enabled = true; break;
            }
        }

        private void btnMon_Click(object sender, EventArgs e)
        {
            FrmImitateSetting frmImitateSetting = new FrmImitateSetting(1, listMons, null, null, dBInfo);
            DialogResult dr = frmImitateSetting.ShowDialog();

            rbOtherMon.Text = $"指定怪物({frmImitateSetting.newMonsList.Count})";
            frmImitateSetting.newMonsList.ForEach(s => listkillMon.Add(s.Name));
        }


        private void btnItems_Click(object sender, EventArgs e)
        {
            FrmImitateSetting frmImitateSetting = new FrmImitateSetting(2, null, listItems, null, dBInfo);
            DialogResult dr = frmImitateSetting.ShowDialog();

            rbOtherItems.Text = $"指定物品({frmImitateSetting.newItemsList.Count})";
            frmImitateSetting.newItemsList.ForEach(s => listkillStd.Add(s.Name));
        }

        private void btnMaps_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> listMaps = GetAllMapsData();
                if (listMaps == null) return;
                FrmImitateSetting frmImitateSetting = new FrmImitateSetting(3, null, null, listMaps, dBInfo);
                DialogResult dr = frmImitateSetting.ShowDialog();

                rbOtherMaps.Text = $"指定地图({frmImitateSetting.newMapsList.Count})";
                frmImitateSetting.newMapsList.ForEach(s => listkillMap.Add(s));
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2("错误", ex.Message);
            }

        }

        private List<string> GetAllMapsData()
        {
            string path = dBInfo.MirPath + "\\Mir200\\Envir\\MapInfo.txt";
            if (!File.Exists(path))
            {
                throw new Exception("地图文件未找到，请查看Mir路径是否正确。");
            }

            List<string> list = FileHelper.ReadTxtReturnList(path).Where(s => s.StartsWith("[")).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                string map = list[i];
                map = map.Substring(0, map.IndexOf("]")).Replace("\t", " ").Replace("[", "");
                string[] mapArray = map.Split(' ');
                if (mapArray.Length > 1)
                {
                    if (mapArray[0].IndexOf("|") > 0)
                        mapArray[0] = mapArray[0].Substring(0, mapArray[0].IndexOf("|"));
                    map = $"{mapArray[mapArray.Length - 1]}[{mapArray[0]}]";
                    list[i] = map;
                }
            }
            return list;
        }
        UIProcessBar processBar = new UIProcessBar();
        UILabel iLabel = new UILabel();
        bool isAllMon = true, isAllItems = false, isAllMaps = false;
        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                Thread TMain = new Thread(new ThreadStart(KillMonster));
                TMain.IsBackground = true;
                TMain.Start();
            }
            catch (Exception ex)
            {
                uiPanel3.Visible = true;
                uiPanel4.Visible = true;
                uiPanel5.Visible = true;
                btnRun.Enabled = true;

                iLabel.Visible = false;
                processBar.Visible = false;
                this.ShowErrorDialog2("异常", ex.Message);
            }

        }
        bool plFlag = true;
        private void KillMonster()
        {
            this.Invoke((EventHandler)delegate
            {
                uiPanel3.Visible = false;
                uiPanel4.Visible = false;
                uiPanel5.Visible = false;
                btnRun.Enabled = false;

                processBar.Location = new Point(150, 200);
                processBar.Size = new Size(700, 30);
                processBar.Value = 0;
                processBar.DecimalPlaces = 0;
                processBar.Style = GameStyle.BindStyle();

                iLabel.Size = new Size(400, 30);
                iLabel.Location = new Point(440, 170);
                iLabel.Style = GameStyle.BindStyle();

                if (plFlag)
                {
                    this.uiPanel2.Controls.Add(processBar);
                    this.uiPanel2.Controls.Add(iLabel);
                }
                else
                {
                    processBar.Visible = true;
                    iLabel.Visible = true;
                }
            });

            int time = Convert.ToInt32(txtRun.Text) * 60;
            int monbl = Convert.ToInt32(txtMon.Text);
            int pbl = Convert.ToInt32(txtPerson.Text);
            int index = 0, totalCount = 0, iCount = 0;

            try
            {
                #region 加载所有地图
                if (rbAllMaps.Checked)
                {
                    try
                    {
                        var list = GetAllMapsData();
                        processBar.Value = 0;
                        index = 0;
                        totalCount = list.Count;
                        iCount = list.Count - 1;
                        KillMapsData kmd = null;
                        for (int i = iCount; i > 0; i--)
                        {
                            index++;
                            string mapcode = list[i].Substring(list[i].IndexOf('[') + 1).TrimEnd(']');
                            string mapname = list[i].Substring(0, list[i].IndexOf('['));
                            if (listMaps.TryGetValue(mapcode, out kmd))
                            {
                                kmd.MapCount += 1;
                                listMaps[mapcode] = kmd;
                            }
                            else
                            {
                                kmd = new KillMapsData()
                                {
                                    MapName = mapname,
                                    MapCode = mapcode,
                                    md_id = index,
                                    MapCount = 0,
                                };
                                listMaps.Add(mapcode, kmd);
                            }
                            if (index % 100 == 0) Thread.Sleep(10);

                            this.Invoke((EventHandler)delegate
                            {
                                iLabel.Text = $"正在加载地图数据{index}/{totalCount}";
                                processBar.Value = (int)(((double)index / totalCount) * 100);
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("地图异常!" + ex.Message);
                    }
                }
                #endregion

                #region 加载所有怪物
                if (rbAllMon.Checked)
                {
                    try
                    {
                        string path = dBInfo.MirPath + "\\Mir200\\Envir\\MonGen.txt";
                        var list = FileHelper.ReadTxtReturnList(path.ToLower());
                        totalCount = list.Count;
                        index = 0;
                        string mapCode = "";
                        double d;
                        int monCount;
                        List<string> arrMons;
                        List<KillMonsData> mList = new List<KillMonsData>();
                        KillMonsData kmd;
                        foreach (var item in list)
                        {
                            index++;
                            if (item.StartsWith(";")) continue;
                            arrMons = item.Replace("\t", " ").Split(' ').Where(s => !string.IsNullOrEmpty(s)).ToList();
                            if (arrMons.Count < 6) continue;

                            mapCode = arrMons[0];
                            d = time / Convert.ToInt32(arrMons[6]) * Convert.ToInt32(arrMons[5]) / 100.0;
                            monCount = (int)(d * monbl);

                            kmd = new KillMonsData()
                            {
                                MonName = arrMons[3],
                                MapCode = mapCode,
                                MonCount = monCount,
                                ms_id = index,
                            };

                            if (dictMons.TryGetValue(mapCode, out mList))
                            {
                                mList.Add(kmd);
                                mList = mList
                                           .GroupBy(s => new { s.MonName, s.MapCode })
                                           .Select(ds => new KillMonsData()
                                           {
                                               MonName = ds.Key.MonName,
                                               MapCode = ds.Key.MapCode,
                                               MonCount = ds.Sum(p => p.MonCount)
                                           }).ToList();
                                dictMons[mapCode] = mList;
                            }
                            else
                            {
                                if (mList == null)
                                    mList = new List<KillMonsData>();
                                mList.Add(kmd);
                                dictMons.Add(mapCode, mList);
                            }

                            if (index % 100 == 0) Thread.Sleep(10);

                            this.Invoke((EventHandler)delegate
                            {
                                iLabel.Text = $"正在加载怪物数据{index}/{totalCount}";
                                processBar.Value = (int)(((double)index / totalCount) * 100);
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("怪物异常!" + ex.Message);
                    }
                }
                #endregion

                #region 加载所有怪物关联的装备
                if (rbAllItems.Checked)
                {
                    try
                    {
                        string path = dBInfo.MirPath + "\\Mir200\\Envir\\MonItems";
                        var mons = Directory.GetFiles(path.ToLower());
                        totalCount = mons.Length;
                        index = 0;
                        string fix = "", npath = "", monName = "", itemName = "";
                        List<string> list = new List<string>();
                        List<string> nlist = new List<string>();
                        List<string> tlist = new List<string>();

                        string[] starr;
                        int bl = 0;
                        bool flag = false;
                        List<KillItemsData> kList = new List<KillItemsData>();
                        KillItemsData kid;
                        foreach (var file in mons)
                        {
                            fix = Path.GetExtension(file);
                            if (fix.ToLower() != ".txt") continue;
                            try
                            {
                                index++;
                                list = FileHelper
                                              .ReadTxtReturnList(file)
                                              .Where(s =>
                                                  !string.IsNullOrEmpty(s) &&
                                                  !s.ToUpper().StartsWith("#CHILD")
                                              ).ToList();
                                // 找文件中的爆率
                                list.ForEach(i =>
                                {
                                    if (i.ToUpper().StartsWith("#CALL"))
                                    {
                                        npath = i.Substring(i.IndexOf("[") + 1);
                                        npath = npath.Substring(0, npath.IndexOf("]")).Replace("\\\\", "\\");
                                        path = dBInfo.MirPath + "\\Mir200\\Envir\\QuestDiary" + npath;
                                        if (File.Exists(path))
                                        {
                                            nlist = FileHelper.ReadTxtReturnList(path);
                                            nlist = nlist.Where(n => !n.Replace(" ", "").StartsWith("[")
                                                                && !n.Replace(" ", "").StartsWith("{")
                                                                && !n.Replace(" ", "").StartsWith("}")
                                                                && !n.Replace(" ", "").StartsWith("#")
                                                                && !n.Replace(" ", "").StartsWith("(")
                                                                && !n.Replace(" ", "").StartsWith(")")
                                                                && !string.IsNullOrEmpty(n)
                                                                && !n.Replace(" ", "").StartsWith("1/1")
                                                                ).ToList();
                                            tlist.AddRange(nlist);
                                        }
                                    }
                                });
                                list = list.Where(i => !i.StartsWith("#CALL") && !i.StartsWith("1/1")).ToList();
                                list.AddRange(tlist);

                                monName = file.Substring(file.LastIndexOf("\\") + 1);
                                monName = monName.Substring(0, monName.IndexOf('.'));

                                // 定位哪个怪物报错
                                //if (monName == "暗之双头血魔[沙漠]")
                                //{

                                //}
                                if (list.Count > 0)
                                {
                                    foreach (var item in list)
                                    {
                                        starr = item.Split("/");
                                        if (starr.Length != 2) continue;
                                        if (string.IsNullOrEmpty(starr[1])) continue;

                                        do
                                        {
                                            starr[1] = starr[1].Split(' ')[0];

                                            flag = int.TryParse(starr[1], out bl);
                                            if (!flag)
                                            {
                                                starr[1] = starr[1].Substring(0, starr[1].Length - 1);
                                            }
                                        } while (!flag);

                                        itemName = Map(item);
                                        kid = new KillItemsData()
                                        {
                                            ItemsName = itemName,
                                            ItemsCount = 1,
                                            MonName = monName,
                                            ItemsBL = bl,
                                        };
                                        if (dictItems.TryGetValue(monName, out kList))
                                        {
                                            kList.Add(kid);
                                        }
                                        else
                                        {
                                            kList = new List<KillItemsData>();
                                            kList.Add(kid);
                                            dictItems.Add(monName, kList);
                                        }
                                    }

                                    kList = kList
                                                .GroupBy(s => new { s.MonName, s.ItemsName, s.ItemsBL })
                                                .Select(d => new KillItemsData()
                                                {
                                                    MonName = d.Key.MonName,
                                                    ItemsName = d.Key.ItemsName,
                                                    ItemsBL = d.Key.ItemsBL,
                                                    ItemsCount = d.Sum(p => p.ItemsCount)
                                                }).ToList();
                                    dictItems[monName] = kList;
                                }

                                if (index % 100 == 0) Thread.Sleep(10);

                                this.Invoke((EventHandler)delegate
                                {
                                    iLabel.Text = $"正在加载物品数据{index}/{totalCount}";
                                    processBar.Value = (int)(((double)index / totalCount) * 100);
                                });
                            }
                            catch (Exception ex)
                            {

                                throw;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("装备异常!" + ex.Message);
                    }
                }
                #endregion

                #region 生成数据
                index = 0;
                totalCount = dictMons.Count;
                Random rnd = new Random();

                foreach (var monsKV in dictMons)
                {
                    index++;
                    foreach (var mon in monsKV.Value)
                    {
                        if (dictItems.TryGetValue(mon.MonName, out List<KillItemsData> tempList))
                        {
                            int monCount = mon.MonCount;
                            while (monCount > 0)
                            {
                                tempList.ForEach(item =>
                                {
                                    item.MapCode = mon.MapCode;
                                    //ItemsBL为1 必爆，否则计算爆率
                                    if (item.ItemsBL == 1)
                                    {
                                        itemsResultList.Add(item);
                                    }
                                    else
                                    {
                                        #region 计算装备爆率 装备爆率 * 人物爆率
                                        int bl = rnd.Next(1, item.ItemsBL);
                                        double bll = 0;
                                        if (item.ItemsBL <= 10)
                                        {
                                            bll = item.ItemsBL / 10.0;
                                        }
                                        else if (item.ItemsBL <= 100)
                                        {
                                            bll = item.ItemsBL / 100.0;
                                        }
                                        else if (item.ItemsBL <= 1000)
                                        {
                                            bll = item.ItemsBL / 1000.0;
                                        }
                                        else if (item.ItemsBL <= 10000)
                                        {
                                            bll = item.ItemsBL / 10000.0;
                                        }
                                        else if (item.ItemsBL <= 100000)
                                        {
                                            bll = item.ItemsBL / 100000.0;
                                        }
                                        else if (item.ItemsBL <= 1000000)
                                        {
                                            bll = item.ItemsBL / 1000000.0;
                                        }
                                        else if (item.ItemsBL <= 10000000)
                                        {
                                            bll = item.ItemsBL / 10000000.0;
                                        }
                                        double b = 1.0 * item.ItemsBL * pbl / 100;
                                        if (b >= bl) itemsResultList.Add(item);
                                        #endregion
                                    }
                                });
                                monCount--;
                            }
                        }
                    }
                    this.Invoke((EventHandler)delegate
                    {
                        iLabel.Text = $"正在生成数据{index}/{totalCount}";
                        processBar.Value = (int)(((double)index / totalCount) * 100);
                    });
                }
                #endregion


                var irtList = itemsResultList
                                   .GroupBy(it => new { it.ItemsName })
                                   .Select(it => new KillItemsData
                                   {
                                       ItemsName = it.Key.ItemsName,
                                       LoopCount = it.Sum(p => p.ItemsCount)
                                   })
                                   .OrderByDescending(it => it.LoopCount)
                                   .ToList();
                if (irtList.Count > 0)
                {
                    this.Invoke((EventHandler)delegate
                    {
                        uiPanel3.Visible = true;
                        uiPanel4.Visible = true;
                        uiPanel5.Visible = true;
                        btnRun.Enabled = true;

                        processBar.Visible = false;
                        iLabel.Visible = false;
                        plFlag = false;

                        uiDataGridView1.DataSource = irtList;

                        this.ShowSuccessDialog2("提示", "生成出的数据不是真正的爆率数据，请知悉。");
                    });
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2(ex.Message);
            }
        }

        string Map(string name)
        {
            name = name.Replace("\t", "");
            string pattern = "1/[0-9]+";
            string replacement = " ";
            string result = Regex.Replace(name, pattern, replacement, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
            return result.Trim();
        }

        private void uiDataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (uiDataGridView1.SelectedRows.Count > 0)
            {
                // 获取选中的行
                DataGridViewRow selectedRow = uiDataGridView1.SelectedRows[0];
                DataGridViewColumn column = uiDataGridView1.Columns["ItemsCount"];
                column.Visible = false;
                column = uiDataGridView1.Columns["ItemsBL"];
                column.Visible = false;
                column = uiDataGridView1.Columns["MonName"];
                column.Visible = false;
                column = uiDataGridView1.Columns["MapCode"];
                column.Visible = false;

                string itemsName = selectedRow.Cells["ItemsName"].Value.ToString();

                try
                {
                    var irtList = itemsResultList
                               .Where(ir => ir.ItemsName == itemsName)
                               .GroupBy(it => new { it.MapCode })
                               .Select(it => new KillItemsData
                               {
                                   ItemsName = it.Key.MapCode,
                                   MapCode = it.Key.MapCode,
                                   LoopCount = it.Sum(p => p.ItemsCount)
                               })
                               .OrderByDescending(it => it.LoopCount)
                               .ToList();

                    irtList.ForEach(it =>
                    {
                        if (!string.IsNullOrEmpty(it.ItemsName))
                        {
                            if (listMaps.TryGetValue(it.ItemsName, out var map))
                            {
                                it.ItemsName = $"{map.MapName}[{map.MapCode}]";
                            }
                        }
                    });
                    uiDataGridView2.DataSource = irtList;
                    uiDataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                }
                catch (Exception ex)
                {
                    this.ShowErrorDialog2("异常:" + ex.Message);
                }
            }
        }

        private void uiDataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (uiDataGridView2.SelectedRows.Count > 0)
            {
                // 获取选中的行
                DataGridViewRow selectedRow = uiDataGridView2.SelectedRows[0];
                DataGridViewColumn column = uiDataGridView2.Columns["ItemsCount"];
                column.Visible = false;
                column = uiDataGridView2.Columns["ItemsBL"];
                column.Visible = false;
                column = uiDataGridView2.Columns["MonName"];
                column.Visible = false;
                column = uiDataGridView2.Columns["MapCode"];
                column.Visible = false;

                try
                {
                    string mapCode = selectedRow.Cells["MapCode"].Value.ToString();
                    DataGridViewRow selectedRow1 = uiDataGridView1.SelectedRows[0];
                    string itemsName = selectedRow1.Cells["ItemsName"].Value.ToString();

                    var irtList = itemsResultList
                                    .Where(ir => ir.MapCode == mapCode && ir.ItemsName == itemsName)
                                    .GroupBy(it => new { it.MonName, it.MapCode })
                                    .Select(it => new KillItemsData
                                    {
                                        MonName = it.Key.MonName,
                                        LoopCount = it.Sum(p => p.ItemsCount),
                                        ItemsCount = dictMons[it.Key.MapCode].Select(dm => dm.MonCount).Sum(),
                                    })
                                    .OrderByDescending(it => it.LoopCount)
                                    .ToList();
                    uiDataGridView3.DataSource = irtList;
                    uiDataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                }
                catch (Exception ex)
                {
                    this.ShowErrorDialog2("异常:" + ex.Message);
                }

            }
        }
        private void uiDataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (uiDataGridView3.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow selectedRow = uiDataGridView3.SelectedRows[0];
                    DataGridViewColumn column = uiDataGridView3.Columns["ItemsBL"];
                    column.Visible = false;
                    column = uiDataGridView3.Columns["MapCode"];
                    column.Visible = false;
                    column = uiDataGridView3.Columns["ItemsName"];
                    column.Visible = false;
                }
                catch (Exception ex)
                {
                    this.ShowErrorDialog2("异常:" + ex.Message);
                }
            }
        }
    }
}
