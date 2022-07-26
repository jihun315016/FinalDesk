using DESK_DTO;
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
    public partial class PopOperationModify : Form
    {
        OperationService operationSrv;
        int operationNo;

        public PopOperationModify(UserVO user, OperationVO oper)
        {
            InitializeComponent();
        }

        private void PopOperationModify_Load(object sender, EventArgs e)
        {
            operationSrv = new OperationService();

            string[] isChackArr = new string[] { "검사 여부", "예", "아니오" };
            cboIsDeffect.Items.AddRange(isChackArr);
            cboIsInspect.Items.AddRange(isChackArr);
            cboMaterial.Items.AddRange(isChackArr);
            
            txtOperationNo.Text = operationNo.ToString();
            
            
        }
    }
}
