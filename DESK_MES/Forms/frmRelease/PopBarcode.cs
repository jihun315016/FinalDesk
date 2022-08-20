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
        ReleaseDAC db = new ReleaseDAC();

        public PopBarcode()
        {
            InitializeComponent();
        }

        private void PopBarcode_Load(object sender, EventArgs e)
        {
            this.Icon = Icon.FromHandle(Properties.Resources.free_icon_tree.GetHicon());
            // 체크박스 사용하여 선택된 항목 출력 

            DataGridUtil.SetInitGridView(dgvList);

            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.Name = "chk";
            chk.Width = 30;
            chk.HeaderText = "";
            dgvList.Columns.Add(chk);

            Point headerCell = dgvList.GetCellDisplayRectangle(0, -1, true).Location;
            headerChk.Location = new Point(headerCell.X + 8, headerCell.Y + 3);
            headerChk.Size = new Size(14, 14);
            headerChk.BackColor = Color.White;
            headerChk.Click += HeaderChk_Click;
            dgvList.Controls.Add(headerChk);

            DataGridUtil.AddGridTextBoxColumn(dgvList, "바코드ID", "BarcodeID", colwidth:150, align : DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "제품ID", "Product_Code", visibility : false);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "제품명", "Product_Name", colwidth: 300, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "단위", "Unit", colwidth: 150, visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "수량", "TotalQty", colwidth: 100, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "출고일자", "Release_OK_Date", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);

            dgvList.DataSource = db.GetBoxOutputList();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<string> selList = new List<string>();

            foreach (DataGridViewRow row in dgvList.Rows)
            {
                bool isChecked = (bool)row.Cells["chk"].EditedFormattedValue;
                if (isChecked)
                {
                    selList.Add(row.Cells[1].Value.ToString());
                }
            }

            if (selList.Count == 0)
            {
                MessageBox.Show("출력할 바코드를 선택해주세요.");
                return;
            }

            string selBarCodeIDs = string.Join(",", selList);

            DataTable dtData = db.GetPrintBoxOutputLabel(selBarCodeIDs);

            XtraReportBarcord rpt = new XtraReportBarcord();
            rpt.DataSource = dtData;

            ReportPreviewForm frm = new ReportPreviewForm(rpt);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopBarcode_Shown(object sender, EventArgs e)
        {
            dgvList.ClearSelection();
        }
    }
}
