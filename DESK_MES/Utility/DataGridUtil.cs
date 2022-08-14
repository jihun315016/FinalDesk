using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESK_MES
{
    public class DataGridUtil
    {
        public static void SetInitGridView(DataGridView dgv)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.RowHeadersWidth = 10;
        }

        public static void SetDataGridViewColumn_TextBox
            (
                DataGridView dgv,
                string headerText,
                string propertyName,
                int colWidth = 100,
                bool isVisible = true,
                bool isReadlonly = true,
                DataGridViewContentAlignment alignHeader = DataGridViewContentAlignment.MiddleCenter,
                DataGridViewContentAlignment alignContent = DataGridViewContentAlignment.MiddleLeft

            )
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.HeaderText = headerText;
            col.DataPropertyName = propertyName;
            col.Name = propertyName;
            col.ReadOnly = isReadlonly;
            col.Width = colWidth;
            col.HeaderCell.Style.Alignment = alignHeader;
            col.DefaultCellStyle.Alignment = alignContent;

            col.Visible = isVisible;

            dgv.Columns.Add(col);
        }

        void SetDataGridViewColumn_Button(DataGridView dgv, string header, string text, int width = 60, int paddingLeftRIght = 8, int paddingTopBottom = 1)
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();

            btn.Width = width;
            btn.DefaultCellStyle.Padding = new Padding(paddingLeftRIght, paddingTopBottom, paddingLeftRIght, paddingTopBottom);
            btn.Name = header;
            btn.Text = text;
            btn.UseColumnTextForButtonValue = true;

            dgv.Columns.Add(btn);
        }

        public static void AddGridTextBoxColumn(DataGridView dgv,
            string headerText,
            string propertyName,
            int colwidth = 100,
            DataGridViewContentAlignment align = DataGridViewContentAlignment.MiddleLeft,
            bool visibility = true,
            bool fixedCol = false)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.Name = propertyName;
            col.HeaderText = headerText;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            col.DataPropertyName = propertyName;
            col.DefaultCellStyle.Alignment = align;
            col.Width = colwidth;
            col.Visible = visibility;
            col.ReadOnly = true; //그리드뷰에서 데이터수정 불가
            col.Frozen = fixedCol;
            dgv.Columns.Add(col);
        }
    }
}
