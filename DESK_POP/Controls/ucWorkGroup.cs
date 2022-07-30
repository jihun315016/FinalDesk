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

namespace DESK_POP
{
    public partial class ucWorkGroup : UserControl
    {
        PopVO orderDetail;

        public int OrderCount { get; set; }
        public ucWorkGroup()
        {
            InitializeComponent();
        }
    }
}
