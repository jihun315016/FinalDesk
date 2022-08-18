
namespace DESK_MES
{
    partial class frmOperation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOperation));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboMaterial = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtOperNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOperName = new System.Windows.Forms.TextBox();
            this.cboIsInspect = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboIsDeffect = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtIsDeffectDetail = new System.Windows.Forms.TextBox();
            this.txtMaterialDetail = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtOperNoDetail = new System.Windows.Forms.TextBox();
            this.txtUpdateUserDetail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpUpdateTime = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpCreateTime = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCreateUserDetail = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtIsInspectDetail = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtOperNameDetail = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.btnExcel = new System.Windows.Forms.Button();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.tableLayoutPanel1);
            // 
            // panel6
            // 
            this.panel6.Location = new System.Drawing.Point(0, 228);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBox2);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExcel);
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.SetChildIndex(this.label2, 0);
            this.groupBox1.Controls.SetChildIndex(this.comboBox1, 0);
            this.groupBox1.Controls.SetChildIndex(this.textBox1, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnOpenDetail, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnReset, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnSearch, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnAdd, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnUpdate, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnExcel, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReset
            // 
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnOpenDetail
            // 
            this.btnOpenDetail.Click += new System.EventHandler(this.btnOpenDetail_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Size = new System.Drawing.Size(100, 25);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Images.SetKeyName(0, "새로고침.png");
            this.imageList1.Images.SetKeyName(1, "검색_1.png");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboMaterial);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtOperNo);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtOperName);
            this.groupBox2.Controls.Add(this.cboIsInspect);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cboIsDeffect);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1184, 72);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // cboMaterial
            // 
            this.cboMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaterial.FormattingEnabled = true;
            this.cboMaterial.Location = new System.Drawing.Point(858, 24);
            this.cboMaterial.Name = "cboMaterial";
            this.cboMaterial.Size = new System.Drawing.Size(91, 25);
            this.cboMaterial.TabIndex = 34;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(771, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 17);
            this.label13.TabIndex = 33;
            this.label13.Text = "||  자재 사용";
            // 
            // txtOperNo
            // 
            this.txtOperNo.Location = new System.Drawing.Point(78, 24);
            this.txtOperNo.Name = "txtOperNo";
            this.txtOperNo.Size = new System.Drawing.Size(100, 25);
            this.txtOperNo.TabIndex = 32;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 17);
            this.label5.TabIndex = 31;
            this.label5.Text = "공정 번호";
            // 
            // txtOperName
            // 
            this.txtOperName.Location = new System.Drawing.Point(253, 24);
            this.txtOperName.Name = "txtOperName";
            this.txtOperName.Size = new System.Drawing.Size(100, 25);
            this.txtOperName.TabIndex = 30;
            // 
            // cboIsInspect
            // 
            this.cboIsInspect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIsInspect.FormattingEnabled = true;
            this.cboIsInspect.Location = new System.Drawing.Point(674, 24);
            this.cboIsInspect.Name = "cboIsInspect";
            this.cboIsInspect.Size = new System.Drawing.Size(91, 25);
            this.cboIsInspect.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(543, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 17);
            this.label8.TabIndex = 28;
            this.label8.Text = "||  검사 데이터 체크";
            // 
            // cboIsDeffect
            // 
            this.cboIsDeffect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIsDeffect.FormattingEnabled = true;
            this.cboIsDeffect.Location = new System.Drawing.Point(446, 24);
            this.cboIsDeffect.Name = "cboIsDeffect";
            this.cboIsDeffect.Size = new System.Drawing.Size(91, 25);
            this.cboIsDeffect.TabIndex = 27;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(359, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 17);
            this.label7.TabIndex = 26;
            this.label7.Text = "||  불량 체크";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(184, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 17);
            this.label6.TabIndex = 24;
            this.label6.Text = "||  공정명";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1184, 419);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtIsDeffectDetail);
            this.groupBox4.Controls.Add(this.txtMaterialDetail);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.txtOperNoDetail);
            this.groupBox4.Controls.Add(this.txtUpdateUserDetail);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.dtpUpdateTime);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.dtpCreateTime);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.txtCreateUserDetail);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.txtIsInspectDetail);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.txtOperNameDetail);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(834, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(347, 413);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "상세 정보";
            // 
            // txtIsDeffectDetail
            // 
            this.txtIsDeffectDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIsDeffectDetail.Enabled = false;
            this.txtIsDeffectDetail.Location = new System.Drawing.Point(140, 109);
            this.txtIsDeffectDetail.Name = "txtIsDeffectDetail";
            this.txtIsDeffectDetail.ReadOnly = true;
            this.txtIsDeffectDetail.Size = new System.Drawing.Size(176, 25);
            this.txtIsDeffectDetail.TabIndex = 30;
            // 
            // txtMaterialDetail
            // 
            this.txtMaterialDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaterialDetail.Enabled = false;
            this.txtMaterialDetail.Location = new System.Drawing.Point(140, 178);
            this.txtMaterialDetail.Name = "txtMaterialDetail";
            this.txtMaterialDetail.ReadOnly = true;
            this.txtMaterialDetail.Size = new System.Drawing.Size(176, 25);
            this.txtMaterialDetail.TabIndex = 29;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(25, 181);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(91, 17);
            this.label16.TabIndex = 28;
            this.label16.Text = "자재사용 체크";
            // 
            // txtOperNoDetail
            // 
            this.txtOperNoDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOperNoDetail.Enabled = false;
            this.txtOperNoDetail.Location = new System.Drawing.Point(140, 42);
            this.txtOperNoDetail.Name = "txtOperNoDetail";
            this.txtOperNoDetail.ReadOnly = true;
            this.txtOperNoDetail.Size = new System.Drawing.Size(176, 25);
            this.txtOperNoDetail.TabIndex = 26;
            // 
            // txtUpdateUserDetail
            // 
            this.txtUpdateUserDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUpdateUserDetail.Enabled = false;
            this.txtUpdateUserDetail.Location = new System.Drawing.Point(140, 311);
            this.txtUpdateUserDetail.Name = "txtUpdateUserDetail";
            this.txtUpdateUserDetail.ReadOnly = true;
            this.txtUpdateUserDetail.Size = new System.Drawing.Size(176, 25);
            this.txtUpdateUserDetail.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 314);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 24;
            this.label4.Text = "변경사용자";
            // 
            // dtpUpdateTime
            // 
            this.dtpUpdateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpUpdateTime.Enabled = false;
            this.dtpUpdateTime.Location = new System.Drawing.Point(140, 277);
            this.dtpUpdateTime.Name = "dtpUpdateTime";
            this.dtpUpdateTime.Size = new System.Drawing.Size(176, 25);
            this.dtpUpdateTime.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 280);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "변경시간";
            // 
            // dtpCreateTime
            // 
            this.dtpCreateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpCreateTime.Enabled = false;
            this.dtpCreateTime.Location = new System.Drawing.Point(140, 209);
            this.dtpCreateTime.Name = "dtpCreateTime";
            this.dtpCreateTime.Size = new System.Drawing.Size(176, 25);
            this.dtpCreateTime.TabIndex = 21;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(25, 212);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 17);
            this.label15.TabIndex = 20;
            this.label15.Text = "생성시간";
            // 
            // txtCreateUserDetail
            // 
            this.txtCreateUserDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCreateUserDetail.Enabled = false;
            this.txtCreateUserDetail.Location = new System.Drawing.Point(140, 243);
            this.txtCreateUserDetail.Name = "txtCreateUserDetail";
            this.txtCreateUserDetail.ReadOnly = true;
            this.txtCreateUserDetail.Size = new System.Drawing.Size(176, 25);
            this.txtCreateUserDetail.TabIndex = 19;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(25, 246);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 17);
            this.label14.TabIndex = 18;
            this.label14.Text = "생성사용자";
            // 
            // txtIsInspectDetail
            // 
            this.txtIsInspectDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIsInspectDetail.Enabled = false;
            this.txtIsInspectDetail.Location = new System.Drawing.Point(140, 144);
            this.txtIsInspectDetail.Name = "txtIsInspectDetail";
            this.txtIsInspectDetail.ReadOnly = true;
            this.txtIsInspectDetail.Size = new System.Drawing.Size(176, 25);
            this.txtIsInspectDetail.TabIndex = 16;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(25, 147);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(109, 17);
            this.label12.TabIndex = 12;
            this.label12.Text = "검사 데이터 체크";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 17);
            this.label10.TabIndex = 10;
            this.label10.Text = "불량 체크";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 17);
            this.label11.TabIndex = 8;
            this.label11.Text = "공정 번호";
            // 
            // txtOperNameDetail
            // 
            this.txtOperNameDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOperNameDetail.Enabled = false;
            this.txtOperNameDetail.Location = new System.Drawing.Point(140, 76);
            this.txtOperNameDetail.Name = "txtOperNameDetail";
            this.txtOperNameDetail.ReadOnly = true;
            this.txtOperNameDetail.Size = new System.Drawing.Size(176, 25);
            this.txtOperNameDetail.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 17);
            this.label9.TabIndex = 6;
            this.label9.Text = "공정명";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvList);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(815, 413);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "공정목록";
            // 
            // dgvList
            // 
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.Location = new System.Drawing.Point(3, 21);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.Size = new System.Drawing.Size(809, 389);
            this.dgvList.TabIndex = 0;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.ImageIndex = 3;
            this.btnExcel.ImageList = this.imageList2;
            this.btnExcel.Location = new System.Drawing.Point(1096, 30);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnExcel.Size = new System.Drawing.Size(76, 27);
            this.btnExcel.TabIndex = 35;
            this.btnExcel.Text = "   엑셀";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "생성.png");
            this.imageList2.Images.SetKeyName(1, "변경.png");
            this.imageList2.Images.SetKeyName(2, "삭제.png");
            this.imageList2.Images.SetKeyName(3, "엑셀.png");
            this.imageList2.Images.SetKeyName(4, "check.png");
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdate.ImageIndex = 1;
            this.btnUpdate.ImageList = this.imageList2;
            this.btnUpdate.Location = new System.Drawing.Point(1014, 30);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnUpdate.Size = new System.Drawing.Size(76, 27);
            this.btnUpdate.TabIndex = 34;
            this.btnUpdate.Text = "   변경";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.ImageIndex = 0;
            this.btnAdd.ImageList = this.imageList2;
            this.btnAdd.Location = new System.Drawing.Point(932, 30);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnAdd.Size = new System.Drawing.Size(76, 27);
            this.btnAdd.TabIndex = 33;
            this.btnAdd.Text = "   생성";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // frmOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Name = "frmOperation";
            this.Text = "공정 관리";
            this.Load += new System.EventHandler(this.frmProcess_Load);
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtOperNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOperName;
        private System.Windows.Forms.ComboBox cboIsInspect;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboIsDeffect;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtOperNoDetail;
        private System.Windows.Forms.TextBox txtUpdateUserDetail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpUpdateTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpCreateTime;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCreateUserDetail;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtIsInspectDetail;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtOperNameDetail;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cboMaterial;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMaterialDetail;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtIsDeffectDetail;
    }
}