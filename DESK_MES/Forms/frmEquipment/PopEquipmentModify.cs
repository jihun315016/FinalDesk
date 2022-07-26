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
        public PopEquipmentModify()
        {
            InitializeComponent();
        }

        private void PopEquipmentModify_Load(object sender, EventArgs e)
        {
            dtpCreate.Format = DateTimePickerFormat.Custom;
            dtpCreate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpUpdate.Format = DateTimePickerFormat.Custom;
            dtpUpdate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}