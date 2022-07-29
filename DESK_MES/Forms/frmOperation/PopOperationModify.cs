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
        UserVO user;

        public PopOperationModify(UserVO user, OperationVO oper)
        {
            InitializeComponent();
            this.user = user;
            InitControl(oper);
        }

        private void InitControl(OperationVO oper)
        {
            operationSrv = new OperationService();

            string[] isChackArr = new string[] { "검사 여부", "예", "아니오" };
            cboIsDeffect.Items.AddRange(isChackArr);
            cboIsInspect.Items.AddRange(isChackArr);
            cboMaterial.Items.AddRange(isChackArr);

            txtOperationNo.Text = oper.Operation_No.ToString();
            txtOperationName.Text = oper.Operation_Name;
            cboIsDeffect.SelectedIndex = oper.Is_Check_Deffect == "Y" ? 1 : 2;
            cboIsInspect.SelectedIndex = oper.Is_Check_Inspect == "Y" ? 1 : 2;
            cboMaterial.SelectedIndex = oper.Is_Check_Marerial == "Y" ? 1 : 2;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string msg = TextBoxUtil.IsRequiredCheck(new ccTextBox[] { txtOperationName });
            if (msg.Length > 0)
            {
                MessageBox.Show(msg);
                return;
            }

            if (cboIsDeffect.SelectedIndex == 0 || cboIsInspect.SelectedIndex == 0 || cboMaterial.SelectedIndex == 0)
            {
                MessageBox.Show("불량 체크 여부와 검사 데이터 입력, 자재 사용 여부를 모두 작성해주세요.");
                return;
            }

            OperationVO oper = new OperationVO()
            {
                Operation_No = Convert.ToInt32(txtOperationNo.Text),
                Operation_Name = txtOperationName.Text,
                Is_Check_Deffect = cboIsDeffect.SelectedIndex == 1 ? "Y" : "N",
                Is_Check_Inspect = cboIsInspect.SelectedIndex == 1 ? "Y" : "N",
                Is_Check_Marerial = cboMaterial.SelectedIndex == 1 ? "Y" : "N",
                Update_User_No = user.User_No
            };

            bool result = operationSrv.UpdateOperation(oper);
            if (result)
            {
                MessageBox.Show("수정이 완료되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("수정에 실패했습니다.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool result = operationSrv.DeleteOperation(Convert.ToInt32(txtOperationNo.Text));
                if (result)
                {
                    MessageBox.Show("삭제가 완료되었습니다.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("삭제에 실패했습니다.");
                }
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
