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
    public partial class Lot_Add : Form
    {
        string oCode;
        PopVO sTWork;
        public Lot_Add(PopVO work)
        {
            sTWork = work;
            InitializeComponent();
        }

        private void Lot_Add_Load(object sender, EventArgs e)
        {
            
        }
    }
}
