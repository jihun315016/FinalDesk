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
        UserVO userV;
        public PopEquipmentModify(EquipmentVO eq, UserVO user)
        {
            selectEqui = eq;
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

            srv.SelectOperationTypeList();
            List<EquipmentVO> equ = srv.SelectOperationTypeList().FindAll((f) => f.Catagory.Equals("설비유형"));
            ComboBoxUtil.ComboBinding<EquipmentVO>(cbotype, equ, "Name", "Code");
            List<EquipmentVO> ino = new List<EquipmentVO>
            {
                new EquipmentVO{Is_Inoperative ="Y"},
                new EquipmentVO{Is_Inoperative="N"}
            };
            ComboBoxUtil.ComboBinding<EquipmentVO>(cboInoper, ino, "Is_Inoperative", "Is_Inoperative");
            //바인딩

            txtNo.Text = selectEqui.Equipment_No.ToString();
            txtName.Text = selectEqui.Equipment_Name;
            txtCreate.Text = selectEqui.Create_User_Name;
            txtUpdate.Text = userV.User_Name;

            cbotype.SelectedValue = selectEqui.Operation_Type_No;
            cboInoper.SelectedValue = selectEqui.Is_Inoperative;

            dtpInoper.Value = Convert.ToDateTime(selectEqui.Inoperative_Start_Time);
            dtpCreate.Value = Convert.ToDateTime(selectEqui.Create_Time);
            dtpUpdate.Value = DateTime.Now;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}