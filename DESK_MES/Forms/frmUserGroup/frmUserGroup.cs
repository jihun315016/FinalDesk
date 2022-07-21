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
    public partial class frmUserGroup : FormStyle_2
    {
        UserGroupService srv;
        List<UserGroupVO> allList;
        List<UserGroupVO> saveList;
        int selectUser=0;
        string user;
        string saveFileName;
        bool flag = false;

        public frmUserGroup()
        {
            InitializeComponent();
            label1.Text = "사용자 그룹 관리";
            flag = false;
        }
        /// <summary>
        /// 김준모/Dgv에 데이터 바인딩(DB에서 가져옴), 화면표시된 그리드 데이터를 전역List에 기록
        /// </summary>
        public void BindingGdv()
        {
            List<UserGroupVO> list = srv.SelectUserGroupList();
            saveList=allList = list;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = allList;
        }
        /// <summary>
        /// 김준모/콤보박스 바인딩 메서드(UserGfoup전용)
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="list"></param>
        /// <param name="dis"></param>
        /// <param name="val"></param>
        private void ComboBinding(ComboBox cbo, List<UserGroupVO> list, string dis, string val, bool blank = false)
        {
            if (blank)
            {
                list.Insert(0, new UserGroupVO
                { Auth_Name = "전체" }
                );
            }
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.DisplayMember = dis;
            cbo.ValueMember = val;

            cbo.DataSource = list; ;
        }
        private void frmUserGroup_Load(object sender, EventArgs e)
        {
            //기본 설정
            if (srv == null)
                srv = new UserGroupService();

            dtpCreate.Format = DateTimePickerFormat.Custom;
            dtpCreate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpUpdate.Format = DateTimePickerFormat.Custom;
            dtpUpdate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

            List<UserGroupVO> cbo= srv.SelectAuthList();
            ComboBinding(cboGroupType, cbo, "Auth_Name", "Auth_ID", blank: true) ;
            comboBox1.Items.Add("사용자 그룹ID");
            comboBox1.Items.Add("사용자 그룹명");
            comboBox1.SelectedIndex = 0;

            cboDate.Items.Add("전체");
            cboDate.Items.Add("생성시간");
            cboDate.Items.Add("변경시간");
            cboDate.SelectedIndex = 0;
            cboDate.DropDownStyle = ComboBoxStyle.DropDownList;


            DataGridUtil.SetInitGridView(dataGridView1);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "사용자 그룹번호", "User_Group_No");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "사용자 그룹명", "User_Group_Name");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "사용자 그룹유형", "User_Group_TypeName");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성시간", "Create_Time");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성사용자", "Create_User_Name");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "변경시간", "Update_Time");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "변경사용자", "Update_User_Name");
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "사용자 그룹유형코드", "User_Group_Type", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "생성사용자ID", "Create_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dataGridView1, "변경사용자ID", "Update_User_No", isVisible: false);

            //데이터
            BindingGdv();
            List<UserGroupVO> auth = srv.SelectAuthList();
            
        }

        //등록
        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopUserGroupRegister pop = new PopUserGroupRegister();
            if (pop.ShowDialog() == DialogResult.OK)
            {
                BindingGdv();
            }
        }

        //변경
        private void button5_Click(object sender, EventArgs e)
        {
            if (selectUser != 0)
            {
                PopUserGroupModify pop = new PopUserGroupModify(selectUser, user);
                if (pop.ShowDialog() == DialogResult.OK)
                {
                    BindingGdv();
                }
            }
        }

        //DGV셀클릭
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            selectUser = Convert.ToInt32(dataGridView1[0, e.RowIndex].Value);
            txtID.Text = dataGridView1[0, e.RowIndex].Value.ToString();
            txtName.Text = dataGridView1[1, e.RowIndex].Value.ToString();
            txtType.Text = dataGridView1[2, e.RowIndex].Value.ToString();
            dtpCreate.Value = Convert.ToDateTime( dataGridView1[3, e.RowIndex].Value);
            txtCreateUser.Text = dataGridView1[4, e.RowIndex].Value.ToString();
            if (dataGridView1[5, e.RowIndex].Value != null)
            {
                dtpUpdate.Value = Convert.ToDateTime(dataGridView1[5, e.RowIndex].Value);
                dtpUpdate.Visible = true;
                txtUpdateUser.Visible = true;
                lblTime.Visible = true;
                lblUser.Visible = true;
            }
            else
            {
                dtpUpdate.Visible = false;
                txtUpdateUser.Visible = false;
                lblTime.Visible = false;
                lblUser.Visible = false;
            }
            if (dataGridView1[6, e.RowIndex].Value != null)
            {
                txtUpdateUser.Text = dataGridView1[6, e.RowIndex].Value.ToString();
            }
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            BindingGdv();
            textBox1.Text = "";
            comboBox1.SelectedIndex = 0;
            selectUser = 0;
            cboGroupType.SelectedIndex = 0;            
            txtID.Text = "";
            txtName.Text = "";
            txtType.Text = "";
            dtpCreate.Value = DateTime.Now;
            txtCreateUser.Text = "";
            dtpUpdate.Value = DateTime.Now;
            txtUpdateUser.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //상세검색
            if (flag)
            {
                string groupType=null;
                string dateStr = null;
                if (cboGroupType.SelectedIndex != 0)
                {
                    groupType = cboGroupType.Text;
                }

                if (cboDate.SelectedIndex == 1) //날짜 콤보 구분
                {
                    dateStr = "C";
                }
                else if (cboDate.SelectedIndex == 2)
                {
                    dateStr = "U";
                }

                if (groupType == null && dateStr == null)
                {
                    BindingGdv();
                    return;
                }
                else if (groupType == null) //날짜만
                {                    
                    if (dateTimePicker1.Value > dateTimePicker2.Value)
                    {
                        MessageBox.Show("날짜 범위를 재설정 해주세요");
                        return;
                    }

                    if (dateStr == "C")
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = saveList = allList.FindAll((f) => Convert.ToDateTime(f.Create_Time) >= dateTimePicker1.Value).FindAll((f) => Convert.ToDateTime(f.Create_Time) <= dateTimePicker2.Value);
                    }
                    else if (dateStr == "U")
                    {

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = saveList = allList.FindAll((f) => Convert.ToDateTime(f.Update_Time) >= dateTimePicker1.Value).FindAll((f) => Convert.ToDateTime(f.Update_Time) <= dateTimePicker2.Value);
                    }
                }
                else if (dateStr == null) //유형만
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource= saveList= allList.FindAll((f) => f.User_Group_TypeName == groupType);
                }
                else //세부 전체 검색
                {
                    if (dateTimePicker1.Value > dateTimePicker2.Value)
                    {
                        MessageBox.Show("날짜 범위를 재설정 해주세요");
                        return;
                    }
                    if (dateStr == "C")
                    {
                        saveList = allList.FindAll((f) => Convert.ToDateTime(f.Create_Time) >= dateTimePicker1.Value).FindAll((f) => Convert.ToDateTime(f.Create_Time) <= dateTimePicker2.Value);
                    }
                    else if (dateStr == "U")
                    {
                        saveList = allList.FindAll((f) => Convert.ToDateTime(f.Update_Time) >= dateTimePicker1.Value).FindAll((f) => Convert.ToDateTime(f.Update_Time) <= dateTimePicker2.Value);
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = saveList.FindAll((f) => f.User_Group_TypeName == groupType);
                }

            }
            //기본검색
            else
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    //사용자 그룹ID
                    if (string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        BindingGdv();
                    }
                    else
                    {
                        int num;
                        if (!int.TryParse(textBox1.Text, out num))
                        {
                            textBox1.Text = "";
                            return;
                        }
                        dataGridView1.DataSource = allList.FindAll((f) => f.User_Group_No == num);
                    }                   
                }
                else if(comboBox1.SelectedIndex ==1)
                {
                    //사용자 그룹명
                    if (string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        BindingGdv();
                    }
                    else
                    {                        
                        dataGridView1.DataSource = allList.FindAll((f) => f.User_Group_Name.Contains(textBox1.Text));
                    }
                }
            }
        }

        //엑셀버튼
        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl Files(*.xls)|*.xls";
            dlg.Title = "엑셀파일로 내보내기";            

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                saveFileName = dlg.FileName;
            }

            List<UserGroupVO> list = dataGridView1.DataSource as List<UserGroupVO>;

            ExcelUtil excel = new ExcelUtil();
            List<UserGroupVO> orders = list;

            string[] columnImport = { "User_Group_No", "User_Group_Name", "User_Group_TypeName", "Create_Time", "Create_User_Name", "Update_Time", "Update_User_Name" };
            string[] columnName = { "사용자 그룹번호", "사용자 그룹명", "사용자 그룹유형", "생성시간", "생성사용자", "변경시간", "변경사용자" };

            if (excel.ExportList(orders, saveFileName, columnImport, columnName))
            {
                MessageBox.Show("엑셀 다운로드 완료");
            }
            else
            {
                MessageBox.Show("엑셀 다운 실패");
            }
        }

        private void btnOpenDetail_Click(object sender, EventArgs e)
        {

            if (flag)
            {
                flag = false;
                //일반검색
                textBox1.Enabled = true;
                cboGroupType.SelectedIndex = 0;
                cboDate.SelectedIndex = 0;
            }
            else
            {
                flag = true;
                //상세검색
                textBox1.Enabled = false;
                comboBox1.SelectedIndex = 0;

            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch_Click(this, null);
            }
        }
    }
}
