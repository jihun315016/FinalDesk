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
    public partial class PopIncomingCreateLot : Form
    {
        public PopIncomingCreateLot()
        {
            InitializeComponent();
        }

        private void PopIncomingCreateLot_Load(object sender, EventArgs e)
        {
            //string id = "ROH_20220726_00001";
            //List<string> insertIDList = new List<string>();
            //string[] a = id.Split('_');
            //int num = Convert.ToInt32(a[2]);

            //for(int i=0; i<2; i++)
            //{
            //    insertIDList.Add("ROH" + DateTime.Now.ToString("yyyyMMdd") + num.ToString(n4));

            //    MessageBox.Show(insertIDList[i]);
            //}

            string nowid = "ROH_20220726_00001";

            string[] search = nowid.Split(new char[] { '_' });
            string prodcode = search[0];
            string date = DateTime.Now.ToString("yyyyMMdd");
            int addID = int.Parse(search[2]);

            List<string> list = new List<string>();
            for (int i = 0; i <dataGridView1.RowCount; i++)
            {
                addID++;
                string newid = addID.ToString().PadLeft(4, '0');
                list.Add(prodcode + '_' + date + '_' + newid);
            }

        }
    }
}
