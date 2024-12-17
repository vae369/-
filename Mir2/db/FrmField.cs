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

namespace Mir2.db
{
    public partial class FrmField : UIForm2
    {
        public List<string> fields = new List<string>();
        DataTable data = null;
        public FrmField(DataTable data, List<string> fields)
        {
            InitializeComponent();
            this.data = data;
            this.fields = fields;
        }

        private void FrmField_Load(object sender, EventArgs e)
        {
            foreach (DataColumn col in data.Columns)
            {
                if (!fields.Contains(col.ColumnName)) uiTransfer1.ItemsLeft.Add(col.ColumnName);
            }
            foreach (string f in fields)
            {
                uiTransfer1.ItemsRight.Add(f);
            }
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            fields.Clear();
            foreach (var col in uiTransfer1.ItemsRight)
            {
                fields.Add(col.ToString());
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
