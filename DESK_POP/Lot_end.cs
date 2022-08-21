using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DESK_DTO;

namespace DESK_POP
{
    public partial class Lot_End : Form
    {
        string tMemo;
        PopVO workList;
        public Lot_End(string memo,PopVO workV)
        {
            tMemo = memo;
            workList = workV;
            InitializeComponent();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            POP_DAC dac = new POP_DAC();
            if (MessageBox.Show("작업종료 저장 완료", "저장", MessageBoxButtons.OK) == DialogResult.OK)
            {
                foreach (Form frm1 in Application.OpenForms)
                {
                    if (frm1.GetType() == typeof(POP_Main))
                    {
                        if(dac.Getupdate(workList.Work_Code));
                        {
                            frm1.Activate();
                            frm1.BringToFront();
                        }
                        return;
                    }
                }
            }
        }

        private void Lot_End_Load(object sender, EventArgs e)
        {
            txtMemo.Text = tMemo;
            textBox1.Text = workList.Planned_Qty.ToString();
            textBox3.Text = workList.Working_Qty.ToString();
            textBox4.Text = workList.User_Name;
            comboBox3.SelectedIndex = 0;
        }
    }
}
