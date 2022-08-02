using DESK_DTO;
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
    public partial class frmEquipment : FormStyle_2
    {
        EquipmentService srv;
        List<EquipmentVO> allList;
        List<EquipmentVO> saveList;
        UserVO userVV;
        EquipmentVO selectEq;
        int userNO;
        string userName;
        string saveFileName;
        int selectEqui;
        bool flag = false;
        public frmEquipment()
        {
            InitializeComponent();
            label1.Text = "설비 관리";
        }
        /// <summary>
        /// 그리드뷰 초기화, 바인딩(DB)
        /// </summary>
        public void BindingGdv()
        {
            List<EquipmentVO> list = srv.SelectEquipmentAllList();
            saveList = allList = list;
            dgvMain.DataSource = null;
            dgvMain.DataSource = allList;
            ResetDetail();
        }
       /// <summary>
       /// 상세 정보단 초기화
       /// </summary>
        private void ResetDetail()
        {
            txtEquipNo.Text = "";
            txtName.Text = "";
            txtOPQty.Text = "";
            txtInoper.Text = "";
            dtpInoper.Value = DateTime.Now; //여기 정보 기입
            dtpCreate.Value = DateTime.Now;
            txtCreateUser.Text = "";
            dtpUpdate.Value = DateTime.Now;
            txtUpdateUser.Text = "";
        }
        /// <summary>
        /// 상단 검색, 상세검색등 초기화
        /// </summary>
        private void UpbarReset()
        {
            comboBox1.SelectedIndex = 0;
            cboUpInoper.SelectedIndex = 0;
            cboDate.SelectedIndex = 0;
            textBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now.AddDays(-1);
            dateTimePicker2.Value = DateTime.Now;
        }

        private void frmEquipment_Load(object sender, EventArgs e)
        {
            //초기 설정
            if (srv == null)
                srv = new EquipmentService();
            userVV = ((frmMain)this.MdiParent).userInfo;
            dtpInoper.Format = DateTimePickerFormat.Custom;
            dtpInoper.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpCreate.Format = DateTimePickerFormat.Custom;
            dtpCreate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";
            dtpUpdate.Format = DateTimePickerFormat.Custom;
            dtpUpdate.CustomFormat = "yyyy년 MM월 dd일 hh:mm:ss";

            comboBox1.Items.Add("설비번호");
            comboBox1.Items.Add("설비명");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            cboUpInoper.Items.Add("전체");
            cboUpInoper.Items.Add("N");
            cboUpInoper.Items.Add("Y");
            cboUpInoper.DropDownStyle = ComboBoxStyle.DropDownList;

            cboDate.Items.Add("전체");
            cboDate.Items.Add("생성시간");
            cboDate.Items.Add("변경시간");
            cboDate.Items.Add("최근다운시간");
            cboDate.DropDownStyle = ComboBoxStyle.DropDownList;

            

            DataGridUtil.SetInitGridView(dgvMain);
            dgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "설비번호", "Equipment_No"); //0
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "설비명", "Equipment_Name"); //1
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "초당 생산량", "Output_Qty"); //2           //초당 생산량으로 변경
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "상태", "Is_Inoperative"); //3
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "최근다운시간", "Is_Inoperative_Date"); //4

            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "생성시간", "Create_Time"); //5
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "생성자", "Create_User_Name"); //6
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "변경시간", "Update_Time"); //7
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "변경자", "Update_User_Name"); //8

            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "삭제 여부", "Is_Delete", isVisible: false); //9
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "생성자ID", "Create_User_No", isVisible: false); //11
            DataGridUtil.SetDataGridViewColumn_TextBox(dgvMain, "변경자ID", "Update_User_No", isVisible: false); //12

            //바인딩
            ResetDetail();
            UpbarReset();
            BindingGdv();

            flag = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int eqLastNum = allList.Last().Equipment_No + 1;//
            userVV = ((frmMain)this.MdiParent).userInfo;
            PopEquipmentRegister pop = new PopEquipmentRegister(eqLastNum, userVV);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                BindingGdv();
            }
        }

        //수정
        private void button1_Click(object sender, EventArgs e)
        {
            if (! string.IsNullOrEmpty(txtEquipNo.Text))
            {
                PopEquipmentModify pop = new PopEquipmentModify(selectEqui, userVV);
                if (pop.ShowDialog() == DialogResult.OK)
                {
                    BindingGdv();
                }
            }
        }

        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            ResetDetail();

            selectEqui = Convert.ToInt32(dgvMain[0, e.RowIndex].Value);
            txtEquipNo.Text = dgvMain[0, e.RowIndex].Value.ToString();
            txtName.Text = dgvMain[1, e.RowIndex].Value.ToString();
            if (dgvMain[2, e.RowIndex].Value != null)
            {
                txtOPQty.Text = dgvMain[2, e.RowIndex].Value.ToString();
            }
            txtInoper.Text = dgvMain[3, e.RowIndex].Value.ToString();
            if (dgvMain[4, e.RowIndex].Value != null)
            {
                dtpInoper.Value = Convert.ToDateTime(dgvMain[4, e.RowIndex].Value);
                dtpInoper.Visible = true;
                panel8.Location = new Point(9, 213); //9, 178
            }
            else
            {
                dtpInoper.Visible = false ;
                panel8.Location = new Point(9, 178);
            }
            dtpCreate.Value = Convert.ToDateTime(dgvMain[5, e.RowIndex].Value);
            txtCreateUser.Text = dgvMain[6, e.RowIndex].Value.ToString();
            if (dgvMain[7, e.RowIndex].Value != null)
            {
                dtpUpdate.Value = Convert.ToDateTime(dgvMain[7, e.RowIndex].Value);
                txtUpdateUser.Text = dgvMain[8, e.RowIndex].Value.ToString();
                dtpUpdate.Visible = true;
                txtUpdateUser.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
            }
            else
            {
                dtpUpdate.Value = Convert.ToDateTime("9997-01-01");
                dtpUpdate.Visible = false;
                txtUpdateUser.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
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
            else
            {
                return;
            }

            List<EquipmentVO> list = dgvMain.DataSource as List<EquipmentVO>;

            ExcelUtil excel = new ExcelUtil();
            List<EquipmentVO> orders = list; //type 부분 초당으로 변경

            string[] columnImport = { "Equipment_No", "Equipment_Name",  "Is_Inoperative", "Is_Inoperative_Date", "Create_Time", "Create_User_Name", "Update_Time", "Update_User_Name", "Is_Delete" };
            string[] columnName = { "설비번호", "설비명", "상태", "최근다운시간", "생성시간", "생성자", "변경시간", "변경자", "삭제여부" };

            if (excel.ExportList(orders, saveFileName, columnImport, columnName))
            {
                MessageBox.Show("엑셀 다운로드 완료");
            }
            else
            {
                MessageBox.Show("엑셀 다운 실패");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            UpbarReset();
            ResetDetail();
            BindingGdv();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (flag)//상세검색
            {                
                StringBuilder sb = new StringBuilder();
                saveList = allList;
                ComboBox[] cbo = new ComboBox[] { cboUpInoper, cboDate }; //type 업생기
                foreach (ComboBox item in cbo)
                {
                    if (item.SelectedIndex != 0)
                    {
                        if (item.Name == "cboUpInoper")
                        {
                            saveList = saveList.FindAll((f) => f.Is_Inoperative == item.Text);
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
                            else if(item.SelectedIndex ==2)
                            {
                                saveList = saveList.FindAll((f) => Convert.ToDateTime(f.Update_Time) >= dateTimePicker1.Value).FindAll((f) => Convert.ToDateTime(f.Update_Time) <= dateTimePicker2.Value);
                            }
                            else
                            {
                                saveList = saveList.FindAll((f) => Convert.ToDateTime(f.Is_Inoperative_Date) >= dateTimePicker1.Value).FindAll((f) => Convert.ToDateTime(f.Is_Inoperative_Date) <= dateTimePicker2.Value);
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
                        dgvMain.DataSource = allList.FindAll((f) => f.Equipment_No == num);
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
                        dgvMain.DataSource = allList.FindAll((f) => f.Equipment_Name.Contains(textBox1.Text));
                    }
                }
            }
        }

        private void btnOpenDetail_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                //일반검색
                textBox1.Enabled = true;
                comboBox1.Enabled = true;
                cboUpInoper.SelectedIndex = 0;
                cboDate.SelectedIndex = 0;
            }
            else
            {
                flag = true;
                //상세검색
                textBox1.Enabled = false;
                comboBox1.Enabled = false;
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
