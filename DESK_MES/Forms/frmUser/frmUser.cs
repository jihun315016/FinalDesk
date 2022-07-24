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
    public partial class frmUser : FormStyle_2
    {
        UserService srv;
        List<UserVO> allList;
        List<UserVO> saveList;
        int userNO;
        string userName;
        string saveFileName;
        int selectUser;
        bool flag = false;
        public frmUser()
        {
            InitializeComponent();
            label1.Text = "사용자 관리";
        }
        /// <summary>
        /// 김준모/Dgv에 데이터 바인딩(DB에서 가져옴), 화면표시된 그리드 데이터를 전역List에 기록
        /// </summary>
        public void BindingGdv()
        {
            List<UserVO> list = srv.SelectUserList();
            saveList = allList = list;
            dgvMain.DataSource = null;
            dgvMain.DataSource = allList;
        }
        /// <summary>
        /// 김준모/콤보박스 바인딩 메서드(UserVO)
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
                { Auth_Name = "전체", User_Group_Name = "전체" }
                );
            }
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.DisplayMember = dis;
            cbo.ValueMember = val;

            cbo.DataSource = list;
        }
        /// <summary>
        /// 김준모/상세 정보란 초기화
        /// </summary>
        private void ResetDetail()
        {
            txtUserNo.Text = "";
            txtName.Text = "";
            txtUserG.Text = "";
            txtPwd.Text = "";
            txtAuth.Text = "";
            txtDelete.Text = "";
            dtpCreate.Value = DateTime.Now;
            txtCreateUser.Text = "";
            dtpUpdate.Value = DateTime.Now;
            txtUpdateUser.Text = "";


        }
        private void frmUser_Load(object sender, EventArgs e)
        {
            if (srv == null)
                srv = new UserService();
            UserGroupService srvG = new UserGroupService();

            dtpCreate.Format = DateTimePickerFormat.Custom;
            dtpCreate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpUpdate.Format = DateTimePickerFormat.Custom;
            dtpUpdate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

            List<UserGroupVO> cbo = srvG.SelectAuthList();
            ComboBinding(cboAuth, cbo, "Auth_Name", "Auth_ID", blank: true);
            comboBox1.Items.Add("사번");
            comboBox1.Items.Add("사용자명");
            comboBox1.SelectedIndex = 0;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            List<UserGroupVO> cbo2 = srvG.SelectGroupNameList();
            ComboBinding(cboGroupType, cbo2, "User_Group_Name", "User_Group_No", blank: true);

            cboDate.Items.Add("전체");
            cboDate.Items.Add("생성시간");
            cboDate.Items.Add("변경시간");
            cboDate.SelectedIndex = 0;
            cboDate.DropDownStyle = ComboBoxStyle.DropDownList;

            DataGridUtil.SetInitGridView(dgvMain);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "사번", "User_No"); //0
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "사용자명", "User_Name"); //1
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "사용자 그룹명", "User_Group_Name"); //2
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "비밀번호", "User_Pwd"); //3
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "권한", "Auth_Name"); //4

            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "생성시간", "Create_Time"); //5
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "생성사용자", "Create_User_Name"); //6
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "변경시간", "Update_Time"); //7
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "변경사용자", "Update_User_Name"); //8

            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "퇴사여부", "Is_Delete"); //9

            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "사용자 그룹유형코드", "User_Group_Type", isVisible: false); //10
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "생성사용자ID", "Create_User_No", isVisible: false);
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "변경사용자ID", "Update_User_No", isVisible: false);
            BindingGdv();

            flag = false;
        }
        private void dgvMain_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            ResetDetail();

            selectUser = Convert.ToInt32(dgvMain[0, e.RowIndex].Value);
            txtUserNo.Text = dgvMain[0, e.RowIndex].Value.ToString();
            txtName.Text = dgvMain[1, e.RowIndex].Value.ToString();

            if (dgvMain[2, e.RowIndex].Value != null) 
            {
                txtUserG.Text = dgvMain[2, e.RowIndex].Value.ToString();
                //txtUserG.Tag = dgvMain[10, e.RowIndex].Value.ToString();
            }

            if (dgvMain[4, e.RowIndex].Value != null) { txtAuth.Text = dgvMain[4, e.RowIndex].Value.ToString(); }

            txtPwd.Text = dgvMain[3, e.RowIndex].Value.ToString();

            dtpCreate.Value = Convert.ToDateTime(dgvMain[5, e.RowIndex].Value);
            txtCreateUser.Text = dgvMain[6, e.RowIndex].Value.ToString();
            if (dgvMain[7, e.RowIndex].Value != null)
            {
                dtpUpdate.Value = Convert.ToDateTime(dgvMain[7, e.RowIndex].Value);
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
            if (dgvMain[7, e.RowIndex].Value != null)
            {
                txtUpdateUser.Text = dgvMain[8, e.RowIndex].Value.ToString();
            }

            txtDelete.Text = dgvMain[9, e.RowIndex].Value.ToString();
        }

        //등록
        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopUserResister pop = new PopUserResister(userNO, userName);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                BindingGdv();
            }
        }

        //변경
        private void button5_Click(object sender, EventArgs e)
        {
            if (! string.IsNullOrWhiteSpace(txtUserNo.Text))
            {
                int gNo=0;
                allList.FindAll((f) => f.User_No.Equals(Convert.ToInt32( txtUserNo.Text))).ForEach((f) => gNo = f.User_Group_No);//미완성
                UserVO userV = new UserVO
                {
                    User_No = Convert.ToInt32(txtUserNo.Text),
                    User_Name = txtName.Text,
                    User_Group_Name = txtUserG.Text,
                    User_Group_No = gNo,
                    User_Pwd = txtPwd.Text,
                    Is_Delete = txtDelete.Text,
                    Create_Time = dtpCreate.Value.ToString(),
                    Create_User_Name = txtCreateUser.Text,
                    Update_Time = dtpUpdate.Value.ToString(),
                    Update_User_Name = txtUpdateUser.Text
                };

                PopUserModify pop = new PopUserModify(userNO, userName, userV);
                if (pop.ShowDialog() == DialogResult.OK)
                {
                    BindingGdv();
                }
            }
        }


        //엑셀
        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl Files(*.xls)|*.xls";
            dlg.Title = "엑셀파일로 내보내기";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                saveFileName = dlg.FileName;
            }

            List<UserGroupVO> list = dgvMain.DataSource as List<UserGroupVO>;

            ExcelUtil excel = new ExcelUtil();
            List<UserGroupVO> orders = list;

            string[] columnImport = { "User_No", "User_Name", "User_Group_Name", "User_Pwd", "Auth_Name", "Create_Time", "Create_User_Name", "Update_Time", "Update_User_Name", "Is_Delete" };
            string[] columnName = { "사번", "사용자명", "사용자 그룹유형", "사용자 그룹명", "비밀번호", "권한", "생성시간", "생성사용자", "변경시간", "변경사용자", "퇴사여부" };

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
                cboAuth.SelectedIndex = 0;
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            //상위
            BindingGdv();
            textBox1.Text = "";
            comboBox1.SelectedIndex = 0;
            selectUser = 0;
            cboGroupType.SelectedIndex = 0;
            cboAuth.SelectedIndex = 0;
            cboDate.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            // 상세정보
            txtUserNo.Text = "";
            txtName.Text = "";
            txtUserG.Text = "";
            txtPwd.Text = "";
            txtAuth.Text = "";
            txtDelete.Text = "";
            dtpCreate.Value = DateTime.Now;
            txtCreateUser.Text = "";
            dtpUpdate.Value = DateTime.Now;
            txtUpdateUser.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                #region
                //string groupType = null;
                //string dateStr = null;
                //string auth = null;
                //if (cboGroupType.SelectedIndex != 0)
                //{
                //    groupType = cboGroupType.Text;
                //}
                //if (cboAuth.SelectedIndex != 0)
                //{
                //    auth = cboAuth.Text;
                //}

                //if (cboDate.SelectedIndex == 1) //날짜 콤보 구분
                //{
                //    dateStr = "C";
                //}
                //else if (cboDate.SelectedIndex == 2)
                //{
                //    dateStr = "U";
                //}

                ////검색
                //if (groupType == null && dateStr == null&& auth==null)
                //{
                //    BindingGdv();
                //    return;
                //}
                //else if (groupType == null && dateStr!=null) //날짜만
                //{
                //    if (dateTimePicker1.Value > dateTimePicker2.Value)
                //    {
                //        MessageBox.Show("날짜 범위를 재설정 해주세요");
                //        return;
                //    }

                //    if (dateStr == "C")
                //    {
                //        dgvMain.DataSource = null;
                //        dgvMain.DataSource = saveList = allList.FindAll((f) => Convert.ToDateTime(f.Create_Time) >= dateTimePicker1.Value).FindAll((f) => Convert.ToDateTime(f.Create_Time) <= dateTimePicker2.Value);
                //    }
                //    else if (dateStr == "U")
                //    {
                //        dgvMain.DataSource = null;
                //        dgvMain.DataSource = saveList = allList.FindAll((f) => Convert.ToDateTime(f.Update_Time) >= dateTimePicker1.Value).FindAll((f) => Convert.ToDateTime(f.Update_Time) <= dateTimePicker2.Value);
                //    }
                //}
                //else if (dateStr == null&& groupType != null) //유형만
                //{
                //    if (cboGroupType.SelectedIndex != 0)
                //    {
                //        dgvMain.DataSource = null;
                //        dgvMain.DataSource = saveList = allList.FindAll((f) => f.User_Group_Name == groupType);
                //    }
                //}
                //else if (dateStr == null && groupType != null)
                //else //세부 전체 검색
                //{
                //    if (dateTimePicker1.Value > dateTimePicker2.Value)
                //    {
                //        MessageBox.Show("날짜 범위를 재설정 해주세요");
                //        return;
                //    }
                //    if (dateStr == "C")
                //    {
                //        saveList = allList.FindAll((f) => Convert.ToDateTime(f.Create_Time) >= dateTimePicker1.Value).FindAll((f) => Convert.ToDateTime(f.Create_Time) <= dateTimePicker2.Value);
                //    }
                //    else if (dateStr == "U")
                //    {
                //        saveList = allList.FindAll((f) => Convert.ToDateTime(f.Update_Time) >= dateTimePicker1.Value).FindAll((f) => Convert.ToDateTime(f.Update_Time) <= dateTimePicker2.Value);
                //    }
                //    dgvMain.DataSource = null;
                //    dgvMain.DataSource = saveList.FindAll((f) => f.User_Group_Name == groupType);
                //}
                #endregion
                StringBuilder sb = new StringBuilder();
                saveList = allList;
                ComboBox[] cbo = new ComboBox[] { cboGroupType, cboAuth, cboDate };
                foreach (ComboBox item in cbo)
                {
                    if (item.SelectedIndex != 0)
                    {
                        if (item.Name == "cboGroupType")
                        {                            
                            saveList = saveList.FindAll((f) => f.User_Group_Name == item.Text);
                        }
                        else if (item.Name == "cboAuth")
                        {
                            saveList = saveList.FindAll((f) => f.Auth_Name == item.Text);
                        }
                        else //데이타
                        {
                            if (dateTimePicker1.Value > dateTimePicker2.Value)
                            {
                                sb.Append("[날짜 범위를 재설정 해주세요]");
                            }

                            else if (item.SelectedIndex == 1)
                            {
                                 saveList = saveList.FindAll((f) => Convert.ToDateTime(f.Create_Time) >= dateTimePicker1.Value).FindAll((f) => Convert.ToDateTime(f.Create_Time) <= dateTimePicker2.Value);
                            }
                            else
                            {
                                saveList = saveList.FindAll((f) => Convert.ToDateTime(f.Update_Time) >= dateTimePicker1.Value).FindAll((f) => Convert.ToDateTime(f.Update_Time) <= dateTimePicker2.Value);
                            }
                        }
                    }
                }
                if (sb.Length > 0)
                {
                    MessageBox.Show(sb.ToString());
                    return;
                }
                dgvMain.DataSource = null;
                dgvMain.DataSource = saveList;
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
                        dgvMain.DataSource = allList.FindAll((f) => f.User_No == num);
                    }
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    //사용자 그룹명
                    if (string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        BindingGdv();
                    }
                    else
                    {
                        dgvMain.DataSource = allList.FindAll((f) => f.User_Name.Contains(textBox1.Text));
                    }
                }
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

