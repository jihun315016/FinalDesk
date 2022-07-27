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
    public partial class frmOperationInspectItemRelation : FormStyle_1
    {
        PurchaseService srv;
        int purchaseNo = 0;
        List<PurchaseVO> purchaseList;
        List<PurchaseDetailVO> purchaseDetailList;


        public frmOperationInspectItemRelation()
        {
            InitializeComponent();
            label1.Text = "공정 - 검사 데이터 항목 관리";
        }
    }
}
