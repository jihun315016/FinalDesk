﻿using DESK_DTO;
using DESK_MES.Service;
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
    public partial class PopProcessRegister : Form
    {
        OperationService processSrv;
        UserVO user;

        public PopProcessRegister(UserVO user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void PopProcessRegister_Load(object sender, EventArgs e)
        {
            processSrv = new OperationService();

            string[] answer = new string[] { "예", "아니오" };
            cboDeffect.Items.AddRange(answer);
            cboInspect.Items.AddRange(answer);
            cboMaterial.Items.AddRange(answer);

            cboDeffect.SelectedIndex = cboInspect.SelectedIndex = cboMaterial.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string msg = TextBoxUtil.IsRequiredCheck(new ccTextBox[] { txtName });
            if (msg.Length > 0)
            {
                MessageBox.Show(msg.ToString());
                return;
            }

            OperationVO oper = new OperationVO()
            {
                Operation_Name = txtName.Text,
                Is_Check_Deffect = cboDeffect.Text == "예" ? "Y" : "N",
                Is_Check_Inspect = cboInspect.Text == "예" ? "Y" : "N",
                Is_Check_Marerial = cboMaterial.Text == "예" ? "Y" : "N"
            };

            //processSrv.SaveProcess
        }
    }
}
