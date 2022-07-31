using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESK_POP
{
    public partial class POP_Detail : Form
    {
        string oCode;
        public POP_Detail(string orderCode)
        {
            oCode = orderCode;
            InitializeComponent();
        }
    }
}
