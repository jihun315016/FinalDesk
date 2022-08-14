using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESK_MES
{
    public partial class PopBarcode : Form
    {
        CheckBox headerChk = new CheckBox();

        public PopBarcode()
        {
            InitializeComponent();
        }

        private void PopBarcode_Load(object sender, EventArgs e)
        {

            // 체크박스 사용하여 선택된 항목 출력 

            DataGridUtil.SetInitGridView(dgvList);

            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.Name = "chk";
            chk.Width = 30;
            chk.HeaderText = "";
            dgvList.Columns.Add(chk);

            Point headerCell = dgvList.GetCellDisplayRectangle(0, -1, true).Location;
            headerChk.Location = new Point(headerCell.X + 8, headerCell.Y + 3);
            headerChk.Size = new Size(18, 18);
            headerChk.BackColor = Color.White;
            headerChk.Click += HeaderChk_Click;
            dgvList.Controls.Add(headerChk);

            DataGridUtil.AddGridTextBoxColumn(dgvList, "바코드ID", "BarcodeID");
            DataGridUtil.AddGridTextBoxColumn(dgvList, "제품ID", "ProductID");
            DataGridUtil.AddGridTextBoxColumn(dgvList, "제품명", "ProductName");
            DataGridUtil.AddGridTextBoxColumn(dgvList, "수량", "Qty");


            //DataTable dt = db.GetProductList();

            //dgvList.DataSource = db.GetBoxOutputList();
        }

        private void HeaderChk_Click(object sender, EventArgs e)
        {
            dgvList.EndEdit();

            foreach (DataGridViewRow row in dgvList.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["chk"];
                chk.Value = headerChk.Checked;
            }
        }
    }
}
