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
    }
}
