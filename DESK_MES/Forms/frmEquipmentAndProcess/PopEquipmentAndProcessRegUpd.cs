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
    public partial class PopEquipmentAndProcessRegUpd : Form
    {
        EquipmentService EquipmentSrv;
        OperationService operationSrv;
        List<InspectItemVO> inspectList;
        List<InspectItemVO> selectedInspect;
        UserVO user;

        public PopEquipmentAndProcessRegUpd(UserVO user, OperationVO oper, bool isReg)
        {
            InitializeComponent();
            this.user = user;
            lblOperationName.Text = oper.Operation_Name;
            lblOperationName.Tag = oper.Operation_No;

            lblTitle.Tag = isReg; // 등록 or 수정 유무
            if (isReg)
            {
                lblTitle.Text = "공정-검사 데이터 등록";
                btnSave.Text = "등록";
            }
            else
            {
                lblTitle.Text = "공정-검사 데이터 수정";
                btnSave.Text = "수정";
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
