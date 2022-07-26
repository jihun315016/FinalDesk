﻿using System;
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
    public partial class PopEquipmentRegister : Form
    {
        EquipmentService srv;
        UserVO user;
        int? newNum;
        public PopEquipmentRegister(int lastNum, UserVO userV)
        {
            newNum = lastNum;
            user = userV;
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopEquipmentRegister_Load(object sender, EventArgs e)
        {
            if (srv == null)
                srv = new EquipmentService();
            //
            ComboBoxUtil.ComboBinding<EquipmentVO>(comboBox1, srv.SelectOperationTypeList(), "Name", "Code");

            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

            txtCode.Enabled = false;
            if (newNum != null && newNum != 0)
            {
                txtCode.Text = newNum.ToString();
            }
            txtName.Text = "";
            txtUser.Enabled = false;
            if (user != null)
            {
                txtUser.Text= user.User_Name;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ccTextBox[] txt = new ccTextBox[] { txtName };
            string msg = TextBoxUtil.IsRequiredCheck(txt);
            StringBuilder sb = new StringBuilder();
            if (msg.Length > 0)
            {
                sb.Append(msg);

                if (txtName.Text.Length > 30)
                {
                    sb.Append($"\n[{txtName.Tag}]의 글자수는 30개 이하만 가능합니다");
                }
                MessageBox.Show(sb.ToString(), "설비 등록 오류", MessageBoxButtons.OK);
                return;
            }
            EquipmentVO eq = new EquipmentVO
            {
                Equipment_No = Convert.ToInt32(txtCode.Text),
                Equipment_Name = txtName.Text,
                Operation_Type_No = Convert.ToInt32(comboBox1.SelectedValue),
                Create_User_No = user.Create_User_No
            };

            if (srv.InsertEquipmentList(eq))
            {
                if (MessageBox.Show("설비 등록 완료", "설비 등록", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("등록 실패");
            }

        }
    }
}