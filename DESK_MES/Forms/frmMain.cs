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

        public UserVO userInfo { get; set; }

        public frmMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Author : 정우성
        /// menuTree, menuList 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin pop = new frmLogin();
            if (pop.ShowDialog() != DialogResult.OK)
            {
                this.Close();
                return;
            }
            else
            {
                this.Show();
                this.userInfo = pop.userVO;
                toolStripLabel1.Text = $"[{pop.userVO.User_Group_Name}] {pop.userVO.User_Name}";
                srv = new MenuService();
                menuList = srv.GetMenuList(Convert.ToInt32(pop.userVO.User_Group_No));
            }

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
                OpenCreateForm(((string[])e.Node.Tag)[0], ((string[])e.Node.Tag)[1]);
            }
            catch
            {
                MessageBox.Show("빈 페이지");
            }
        }
        private void OpenCreateForm(string formName, string formKorName)
        {
            string appName = Assembly.GetEntryAssembly().GetName().Name;
            Type frmType = Type.GetType($"{appName}.{formName}");

            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == frmType)
                {
                    form.Activate();
                    return;
                }
            }

            Form frm = (Form)Activator.CreateInstance(frmType);
            frm.MdiParent = this;
            frm.Show();

        }

            /// <summary>
            /// 폼 생성 감지, 생성에 따른 탭 생성, 삭제, 앞에 띄워주기
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        private void Form1_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
            {
                tabControl1.Visible = false;
            }
            else
            {
                this.ActiveMdiChild.WindowState = FormWindowState.Maximized;

                if (this.ActiveMdiChild.Tag == null)
                {
                    //텝페이지 생성
                    TabPage tp = new TabPage(this.ActiveMdiChild.Text + "       ");
                    tp.Parent = tabControl1;
                    tp.Tag = this.ActiveMdiChild; //?

                    tabControl1.SelectedTab = tp;

                    //자식폼 종료시 종료
                    this.ActiveMdiChild.FormClosed += ActiveMdiChild_FormClosed;
                    //테그에 이름 넣기
                    this.ActiveMdiChild.Tag = tp;
                }
                else //기존에 추가된
                {
                    tabControl1.SelectedTab = (TabPage)this.ActiveMdiChild.Tag;
                }

                if (!tabControl1.Visible)
                {
                    tabControl1.Visible = true;
                }
            }
        }

        /// <summary>
        /// 폼 종료
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveMdiChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            //종료
            Form frm = (Form)sender;
            ((TabPage)frm.Tag).Dispose();
        }

        /// <summary>
        /// 탭창 변경에 따른 화면 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != null)
            {
                Form frm = (Form)tabControl1.SelectedTab.Tag;
                frm.Select();
            }
        }

        /// <summary>
        /// tab 마우스 클릭에 따른 창 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                var r = tabControl1.GetTabRect(i);
                var closeImage = Properties.Resources.close_grey;
                var closeRect = new Rectangle((r.Right - closeImage.Width), r.Top + (r.Height - closeImage.Height) / 2,
                    closeImage.Width, closeImage.Height);

                if (closeRect.Contains(e.Location))
                {
                    this.ActiveMdiChild.Close();
                    break;
                }
            }
        }
    }
}
