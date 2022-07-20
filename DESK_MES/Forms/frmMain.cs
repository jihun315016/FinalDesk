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
using DESK_MES.DAC;
using System.Reflection;

namespace DESK_MES
{
    public partial class frmMain : Form
    {
        MenuService srv = new MenuService();
        List<MenuVO> menuList;
        TreeView menuTree;
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //frmWorkOrder frm = new frmWorkOrder();
            frmProducts frm = new frmProducts();
            frm.MdiParent = this;
            frm.Show();
        }

        /// <summary>
        /// Author : 정우성
        /// menuTree, menuList 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            menuList = srv.GetMenuList(1001);

            //최상위 메뉴 버튼 초기화
            var list = menuList.FindAll(m => m.Parent_Function_No == 0);
            for(int i = 0; i < list.Count; i++)
            {
                Button btn = new Button();
                btn.Text = list[i].Function_Name;
                btn.Size = new Size(244, 50);
                btn.Tag = new int[2] { list[i].Function_No, i };
                btn.Click += MenuButtonClick;
                btn.BackColor = SystemColors.ControlDark;
                flowLayoutPanel1.Controls.Add(btn);
            }

            menuTree = new TreeView();
            menuTree.Size = new Size(244, 50);
            menuTree.NodeMouseDoubleClick += MenuTree_NodeMouseDoubleClick;
            menuTree.BackColor = SystemColors.WindowFrame;
            menuTree.BorderStyle = BorderStyle.None;
            flowLayoutPanel1.Controls.Add(menuTree);

           
        }

        private void MenuButtonClick(object sender, EventArgs e)
        {
            Button clickedBtn = (Button)sender;
            menuTree.Nodes.Clear();

            var list = menuList.FindAll(m => m.Parent_Function_No == ((int[])clickedBtn.Tag)[0]);
            foreach (MenuVO item in list)
            {
                TreeNode node = new TreeNode(item.Function_Name);
                node.Tag = new string[] { item.frmName, item.Function_Name };
                menuTree.Nodes.Add(node);
            }

            menuTree.ItemHeight = 30;
            menuTree.Height = menuTree.Nodes.Count * menuTree.ItemHeight + 4;

            flowLayoutPanel1.Controls.SetChildIndex(menuTree, ((int[])clickedBtn.Tag)[1] + 2 );
            flowLayoutPanel1.Invalidate();
        }

        private void MenuTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                
            }
            catch
            {
                MessageBox.Show("준비 중 입니다.");
        }


    }
}
