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

namespace DESK_MES
{
    public partial class PopEquipmentModify : Form
    {
        EquipmentService srv;
        EquipmentVO selectEqui;
        int eqNo;
        UserVO userV;
        public PopEquipmentModify(int eq, UserVO user)
        {
            eqNo = eq;
            userV = user;
            InitializeComponent();
        }

        private void PopEquipmentModify_Load(object sender, EventArgs e)
        {
            //기본설정
            if (srv == null)
                srv = new EquipmentService();

            dtpInoper.Format = DateTimePickerFormat.Custom;
            dtpInoper.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpCreate.Format = DateTimePickerFormat.Custom;
            dtpCreate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpUpdate.Format = DateTimePickerFormat.Custom;
            dtpUpdate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

            txtNo.Enabled = false;
            txtCreate.Enabled = false;
            txtUpdate.Enabled = false;

            selectEqui= srv.SelectEquipmentNoList(eqNo);
            List<EquipmentVO> equ = srv.SelectOperationTypeList().FindAll((f) => f.Catagory.Equals("설비유형"));
            ComboBoxUtil.ComboBinding<EquipmentVO>(cbotype, equ, "Name", "Code");
            List<EquipmentVO> ino = new List<EquipmentVO>
            {
                new EquipmentVO{Is_Inoperative ="N"},
                new EquipmentVO{Is_Inoperative="Y"}
            };
            ComboBoxUtil.ComboBinding<EquipmentVO>(cboInoper, ino, "Is_Inoperative", "Is_Inoperative");
            //바인딩

            
            txtNo.Text = selectEqui.Equipment_No.ToString();
            txtName.Text = selectEqui.Equipment_Name;
            txtHistory.Text = selectEqui.Action_History;
            txtReason.Text = selectEqui.Inoperative_Reason;
            txtCreate.Text = selectEqui.Create_User_Name;
            txtUpdate.Text = userV.User_Name;


            cbotype.SelectedValue = selectEqui.Operation_Type_No;
            cboInoper.SelectedValue = selectEqui.Is_Inoperative;

            dtpInoper.Value = Convert.ToDateTime(selectEqui.Is_Inoperative_Date);
            dtpCreate.Value = Convert.ToDateTime(selectEqui.Create_Time);
            dtpUpdate.Value = DateTime.Now;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboInoper_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInoper.SelectedIndex == 0) //N
            {
                label5.Visible = false;
                txtReason.Visible = false;
                label7.Visible = false;
                txtHistory.Visible = false;

                this.Size = new Size(513, 533);
                panel5.Location = new Point(58, 206);
            }
            else //Y
            {
                label5.Visible = true;
                txtReason.Visible = true;
                label7.Visible = true;
                txtHistory.Visible = true;

                this.Size = new Size(513, 769);
                panel5.Location = new Point(58, 430);
            }
        }
    }
}