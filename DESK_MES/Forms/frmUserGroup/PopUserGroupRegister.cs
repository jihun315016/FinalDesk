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
    public partial class PopUserGroupRegister : Form
    {
        UserGroupService srv;
        public PopUserGroupRegister()
        {
            InitializeComponent();
        }

        private void PopUserGroupRegister_Load(object sender, EventArgs e)
        {
            //기본 설정
            srv = new UserGroupService();
            List<UserGroupVO>list = srv.SelectAuthList();
            ComboBinding(comboBox1, list);

            list.ForEach((f) => txtNO.Text = (f.LastUser_No+1).ToString());
            txtNO.Enabled = false;

            dtpCreate.Format = DateTimePickerFormat.Custom;
            dtpCreate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpCreate.Value = DateTime.Now;
            dtpCreate.Enabled = false;

            //여기서 부모폼에서 가져온거 쓰자
            txtCreateName.Enabled = false;
        }
        private void ComboBinding(ComboBox cbo,List<UserGroupVO>list)
        {
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.DisplayMember = "Auth_Name";
            cbo.ValueMember = "Auth_ID";

            cbo.DataSource = list; ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
