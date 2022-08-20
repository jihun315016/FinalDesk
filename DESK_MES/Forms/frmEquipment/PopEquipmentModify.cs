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
            this.Icon = Icon.FromHandle(Properties.Resources.free_icon_tree.GetHicon());
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
            numericUpDown1.Value = selectEqui.Output_Qty;


            cboInoper.SelectedValue = selectEqui.Is_Inoperative;
            if (selectEqui.Is_Inoperative_Date != null)
            {
                dtpInoper.Value = Convert.ToDateTime(selectEqui.Is_Inoperative_Date);
            }
            else
            {
                dtpInoper.Value =Convert.ToDateTime( "9997-01-01");
            }
            dtpCreate.Value = Convert.ToDateTime(selectEqui.Create_Time);
            dtpUpdate.Value = DateTime.Now;
        }

        //닫기
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboInoper_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInoper.SelectedIndex == 0) //N
            {
                if (selectEqui.Is_Inoperative == "N")
                {
                    label5.Visible = false;
                    txtReason.Visible = false;
                    label7.Visible = false;
                    txtHistory.Visible = false;
                    this.Size = new Size(513, 533);
                    panel5.Location = new Point(58, 206);
                }
                else
                {
                    txtHistory.Enabled = true;
                }
            }
            else //Y
            {
                label5.Visible = true;
                txtReason.Visible = true;
                label7.Visible = true;
                txtHistory.Visible = true;
                txtHistory.Enabled = false;

                this.Size = new Size(513, 769);
                panel5.Location = new Point(58, 430);
                if (selectEqui.Is_Inoperative == "Y")
                {
                    txtHistory.Text = string.Empty;
                }
                else
                {
                    txtHistory.Text = "[설비 재가동시 기입]";
                }
            }
        }

        //수정
        private void button1_Click(object sender, EventArgs e)
        {
            ccTextBox[] txt = new ccTextBox[] { txtName };
            if (cboInoper.SelectedIndex == 1)
            {
                txt.Append(txtReason);
            }
            if (selectEqui.Is_Inoperative == "Y")
            {
                txt.Append(txtHistory);
            }
            string msg = TextBoxUtil.IsRequiredCheck(txt);
            StringBuilder sb = new StringBuilder();
            if (msg.Length > 0)
            {
                sb.Append(msg);

                if (txtName.Text.Length > 30)
                {
                    sb.Append($"\n[{txtName.Tag}]의 글자수는 30개 이하만 가능합니다");
                }
                MessageBox.Show(sb.ToString(), "설비 수정 오류", MessageBoxButtons.OK);
                return;
            }
            //수정
            EquipmentVO equi = new EquipmentVO
            {
                Equipment_No = Convert.ToInt32(txtNo.Text),
                Equipment_Name = txtName.Text,
                Is_Inoperative = cboInoper.SelectedValue.ToString(),
                Inoperative_Reason = txtReason.Text,
                Action_History = txtHistory.Text,
                Update_User_No = userV.User_No,
                Is_Inoperative_Date = selectEqui.Is_Inoperative_Date,
                Output_Qty = Convert.ToInt32( numericUpDown1.Value)
            };
            if (equi.Is_Inoperative_Date == null)
            {
                equi.Is_Inoperative_Date = DateTime.Now.ToString();
            }
            //db
            if (srv.UpdateEquipment(equi))
            {
                MessageBox.Show("설비 수정 성공", "수정", MessageBoxButtons.OK);
                
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                
            }
            else
            {
                MessageBox.Show("설비 수정 실패");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (srv.DeleteEquipment(selectEqui.Equipment_No))
            {
                MessageBox.Show("설비 삭제 성공", "삭제", MessageBoxButtons.OK);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
            }
            else
            {
                MessageBox.Show("설비 삭제 실패");
            }
        }
    }
}