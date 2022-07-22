
namespace DESK_MES
{
    partial class frmProducts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProducts));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPrdNameDetailSearch = new System.Windows.Forms.TextBox();
            this.cboTypeDetailSearch = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPrdCodeDetailSearch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.txtCodeDetail = new System.Windows.Forms.TextBox();
            this.txtUpdateUserDetail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpUpdateTime = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpCreateTime = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCreateUserDetail = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtClientDetail = new System.Windows.Forms.TextBox();
            this.txtTypeDetail = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNameDetail = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ptbProductImage = new System.Windows.Forms.PictureBox();
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
            this.tableLayoutPanel2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbProductImage)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(1184, 861);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.tableLayoutPanel1);
            this.panel7.Size = new System.Drawing.Size(1184, 619);
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
            this.groupBox2.Controls.Add(this.txtPrdNameDetailSearch);
            this.groupBox2.Controls.Add(this.cboTypeDetailSearch);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtPrdCodeDetailSearch);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1184, 72);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // txtPrdNameDetailSearch
            // 
            this.txtPrdNameDetailSearch.Location = new System.Drawing.Point(218, 29);
            this.txtPrdNameDetailSearch.Name = "txtPrdNameDetailSearch";
            this.txtPrdNameDetailSearch.Size = new System.Drawing.Size(100, 25);
            this.txtPrdNameDetailSearch.TabIndex = 31;
            // 
            // cboTypeDetailSearch
            // 
            this.cboTypeDetailSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTypeDetailSearch.FormattingEnabled = true;
            this.cboTypeDetailSearch.Location = new System.Drawing.Point(380, 29);
            this.cboTypeDetailSearch.Name = "cboTypeDetailSearch";
            this.cboTypeDetailSearch.Size = new System.Drawing.Size(100, 25);
            this.cboTypeDetailSearch.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(324, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 17);
            this.label6.TabIndex = 27;
            this.label6.Text = "||  유형";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(162, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 17);
            this.label7.TabIndex = 25;
            this.label7.Text = "||  품명";
            // 
            // txtPrdCodeDetailSearch
            // 
            this.txtPrdCodeDetailSearch.Location = new System.Drawing.Point(56, 29);
            this.txtPrdCodeDetailSearch.Name = "txtPrdCodeDetailSearch";
            this.txtPrdCodeDetailSearch.Size = new System.Drawing.Size(100, 25);
            this.txtPrdCodeDetailSearch.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 17);
            this.label5.TabIndex = 23;
            this.label5.Text = "품번";
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1184, 619);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel2);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(834, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(347, 613);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "상세 정보";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.panel8, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel9, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(341, 589);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.txtCodeDetail);
            this.panel8.Controls.Add(this.txtUpdateUserDetail);
            this.panel8.Controls.Add(this.label4);
            this.panel8.Controls.Add(this.dtpUpdateTime);
            this.panel8.Controls.Add(this.label3);
            this.panel8.Controls.Add(this.dtpCreateTime);
            this.panel8.Controls.Add(this.label15);
            this.panel8.Controls.Add(this.txtCreateUserDetail);
            this.panel8.Controls.Add(this.label14);
            this.panel8.Controls.Add(this.txtClientDetail);
            this.panel8.Controls.Add(this.txtTypeDetail);
            this.panel8.Controls.Add(this.label13);
            this.panel8.Controls.Add(this.label10);
            this.panel8.Controls.Add(this.label11);
            this.panel8.Controls.Add(this.txtNameDetail);
            this.panel8.Controls.Add(this.label9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 238);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(335, 348);
            this.panel8.TabIndex = 0;
            // 
            // txtCodeDetail
            // 
            this.txtCodeDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodeDetail.Enabled = false;
            this.txtCodeDetail.Location = new System.Drawing.Point(123, 26);
            this.txtCodeDetail.Name = "txtCodeDetail";
            this.txtCodeDetail.ReadOnly = true;
            this.txtCodeDetail.Size = new System.Drawing.Size(190, 25);
            this.txtCodeDetail.TabIndex = 44;
            // 
            // txtUpdateUserDetail
            // 
            this.txtUpdateUserDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUpdateUserDetail.Enabled = false;
            this.txtUpdateUserDetail.Location = new System.Drawing.Point(123, 270);
            this.txtUpdateUserDetail.Name = "txtUpdateUserDetail";
            this.txtUpdateUserDetail.ReadOnly = true;
            this.txtUpdateUserDetail.Size = new System.Drawing.Size(190, 25);
            this.txtUpdateUserDetail.TabIndex = 43;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 273);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 42;
            this.label4.Text = "변경사용자";
            // 
            // dtpUpdateTime
            // 
            this.dtpUpdateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpUpdateTime.Enabled = false;
            this.dtpUpdateTime.Location = new System.Drawing.Point(123, 236);
            this.dtpUpdateTime.Name = "dtpUpdateTime";
            this.dtpUpdateTime.Size = new System.Drawing.Size(190, 25);
            this.dtpUpdateTime.TabIndex = 41;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 40;
            this.label3.Text = "변경시간";
            // 
            // dtpCreateTime
            // 
            this.dtpCreateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpCreateTime.Enabled = false;
            this.dtpCreateTime.Location = new System.Drawing.Point(123, 168);
            this.dtpCreateTime.Name = "dtpCreateTime";
            this.dtpCreateTime.Size = new System.Drawing.Size(190, 25);
            this.dtpCreateTime.TabIndex = 39;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(21, 171);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 17);
            this.label15.TabIndex = 38;
            this.label15.Text = "생성시간";
            // 
            // txtCreateUserDetail
            // 
            this.txtCreateUserDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCreateUserDetail.Enabled = false;
            this.txtCreateUserDetail.Location = new System.Drawing.Point(123, 202);
            this.txtCreateUserDetail.Name = "txtCreateUserDetail";
            this.txtCreateUserDetail.ReadOnly = true;
            this.txtCreateUserDetail.Size = new System.Drawing.Size(190, 25);
            this.txtCreateUserDetail.TabIndex = 37;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(21, 205);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 17);
            this.label14.TabIndex = 36;
            this.label14.Text = "생성사용자";
            // 
            // txtClientDetail
            // 
            this.txtClientDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientDetail.Enabled = false;
            this.txtClientDetail.Location = new System.Drawing.Point(123, 134);
            this.txtClientDetail.Name = "txtClientDetail";
            this.txtClientDetail.ReadOnly = true;
            this.txtClientDetail.Size = new System.Drawing.Size(190, 25);
            this.txtClientDetail.TabIndex = 35;
            // 
            // txtTypeDetail
            // 
            this.txtTypeDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTypeDetail.Enabled = false;
            this.txtTypeDetail.Location = new System.Drawing.Point(123, 94);
            this.txtTypeDetail.Name = "txtTypeDetail";
            this.txtTypeDetail.ReadOnly = true;
            this.txtTypeDetail.Size = new System.Drawing.Size(190, 25);
            this.txtTypeDetail.TabIndex = 33;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(21, 137);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 17);
            this.label13.TabIndex = 32;
            this.label13.Text = "입고처";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 17);
            this.label10.TabIndex = 30;
            this.label10.Text = "유형";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 17);
            this.label11.TabIndex = 29;
            this.label11.Text = "품번";
            // 
            // txtNameDetail
            // 
            this.txtNameDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNameDetail.Enabled = false;
            this.txtNameDetail.Location = new System.Drawing.Point(123, 60);
            this.txtNameDetail.Name = "txtNameDetail";
            this.txtNameDetail.ReadOnly = true;
            this.txtNameDetail.Size = new System.Drawing.Size(190, 25);
            this.txtNameDetail.TabIndex = 28;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 17);
            this.label9.TabIndex = 27;
            this.label9.Text = "품명";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.groupBox5);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(3, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(335, 229);
            this.panel9.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.ptbProductImage);
            this.groupBox5.Location = new System.Drawing.Point(24, 7);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(289, 209);
            this.groupBox5.TabIndex = 27;
            this.groupBox5.TabStop = false;
            // 
            // ptbProductImage
            // 
            this.ptbProductImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ptbProductImage.Location = new System.Drawing.Point(6, 15);
            this.ptbProductImage.Name = "ptbProductImage";
            this.ptbProductImage.Size = new System.Drawing.Size(277, 188);
            this.ptbProductImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbProductImage.TabIndex = 0;
            this.ptbProductImage.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvList);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(815, 613);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "품목 현황";
            // 
            // dgvList
            // 
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.Location = new System.Drawing.Point(3, 21);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.Size = new System.Drawing.Size(809, 589);
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
            this.btnExcel.TabIndex = 38;
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
            this.btnUpdate.TabIndex = 37;
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
            this.btnAdd.TabIndex = 36;
            this.btnAdd.Text = "   생성";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // frmProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 861);
            this.Name = "frmProducts";
            this.Text = "frmProducts";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmProducts_Load);
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
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptbProductImage)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPrdCodeDetailSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboTypeDetailSearch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.PictureBox ptbProductImage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox txtCodeDetail;
        private System.Windows.Forms.TextBox txtUpdateUserDetail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpUpdateTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpCreateTime;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCreateUserDetail;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtClientDetail;
        private System.Windows.Forms.TextBox txtTypeDetail;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNameDetail;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.TextBox txtPrdNameDetailSearch;
    }
}