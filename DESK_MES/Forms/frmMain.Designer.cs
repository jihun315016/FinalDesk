
namespace DESK_MES
{
    partial class frmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("주문 관리");
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("출고 관리");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("매입 현황");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("매출 현황");
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("영업 관리", new System.Windows.Forms.TreeNode[] {
            treeNode27,
            treeNode28,
            treeNode29,
            treeNode30});
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("사용자 관리");
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("사용자 그룹 설정");
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("거래처 관리");
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("품목 관리");
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("BOM 관리");
            System.Windows.Forms.TreeNode treeNode37 = new System.Windows.Forms.TreeNode("설비 관리");
            System.Windows.Forms.TreeNode treeNode38 = new System.Windows.Forms.TreeNode("공정 관리");
            System.Windows.Forms.TreeNode treeNode39 = new System.Windows.Forms.TreeNode("창고 관리");
            System.Windows.Forms.TreeNode treeNode40 = new System.Windows.Forms.TreeNode("품질검사항목 설정");
            System.Windows.Forms.TreeNode treeNode41 = new System.Windows.Forms.TreeNode("기준정보 관리", new System.Windows.Forms.TreeNode[] {
            treeNode32,
            treeNode33,
            treeNode34,
            treeNode35,
            treeNode36,
            treeNode37,
            treeNode38,
            treeNode39,
            treeNode40});
            System.Windows.Forms.TreeNode treeNode42 = new System.Windows.Forms.TreeNode("발주 관리");
            System.Windows.Forms.TreeNode treeNode43 = new System.Windows.Forms.TreeNode("생산 계획 관리");
            System.Windows.Forms.TreeNode treeNode44 = new System.Windows.Forms.TreeNode("작업지시 관리");
            System.Windows.Forms.TreeNode treeNode45 = new System.Windows.Forms.TreeNode("LOT 상태 및 이력 조회");
            System.Windows.Forms.TreeNode treeNode46 = new System.Windows.Forms.TreeNode("생산 관리", new System.Windows.Forms.TreeNode[] {
            treeNode42,
            treeNode43,
            treeNode44,
            treeNode45});
            System.Windows.Forms.TreeNode treeNode47 = new System.Windows.Forms.TreeNode("설비-공정관계 설정");
            System.Windows.Forms.TreeNode treeNode48 = new System.Windows.Forms.TreeNode("설비 비가동 관리");
            System.Windows.Forms.TreeNode treeNode49 = new System.Windows.Forms.TreeNode("설비 관리", new System.Windows.Forms.TreeNode[] {
            treeNode47,
            treeNode48});
            System.Windows.Forms.TreeNode treeNode50 = new System.Windows.Forms.TreeNode("검사데이터 관리");
            System.Windows.Forms.TreeNode treeNode51 = new System.Windows.Forms.TreeNode("불량 관리");
            System.Windows.Forms.TreeNode treeNode52 = new System.Windows.Forms.TreeNode("품질 관리", new System.Windows.Forms.TreeNode[] {
            treeNode50,
            treeNode51});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.treeView1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(250, 661);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(241, 150);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.ForeColor = System.Drawing.SystemColors.Menu;
            this.button1.Location = new System.Drawing.Point(3, 159);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(244, 50);
            this.button1.TabIndex = 10;
            this.button1.Text = "FORM TEST";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeView1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.treeView1.ForeColor = System.Drawing.SystemColors.Menu;
            this.treeView1.Location = new System.Drawing.Point(3, 215);
            this.treeView1.Name = "treeView1";
            treeNode27.Name = "노드1";
            treeNode27.Text = "주문 관리";
            treeNode28.Name = "노드2";
            treeNode28.Text = "출고 관리";
            treeNode29.Name = "노드3";
            treeNode29.Text = "매입 현황";
            treeNode30.Name = "노드6";
            treeNode30.Text = "매출 현황";
            treeNode31.Name = "노드0";
            treeNode31.Text = "영업 관리";
            treeNode32.Name = "노드1";
            treeNode32.Text = "사용자 관리";
            treeNode33.Name = "노드2";
            treeNode33.Text = "사용자 그룹 설정";
            treeNode34.Name = "노드8";
            treeNode34.Text = "거래처 관리";
            treeNode35.Name = "노드3";
            treeNode35.Text = "품목 관리";
            treeNode36.Name = "노드4";
            treeNode36.Text = "BOM 관리";
            treeNode37.Name = "노드5";
            treeNode37.Text = "설비 관리";
            treeNode38.Name = "노드6";
            treeNode38.Text = "공정 관리";
            treeNode39.Name = "노드7";
            treeNode39.Text = "창고 관리";
            treeNode40.Name = "노드9";
            treeNode40.Text = "품질검사항목 설정";
            treeNode41.Name = "노드0";
            treeNode41.Text = "기준정보 관리";
            treeNode42.Name = "노드4";
            treeNode42.Text = "발주 관리";
            treeNode43.Name = "노드7";
            treeNode43.Text = "생산 계획 관리";
            treeNode44.Name = "노드13";
            treeNode44.Text = "작업지시 관리";
            treeNode45.Name = "노드14";
            treeNode45.Text = "LOT 상태 및 이력 조회";
            treeNode46.Name = "노드5";
            treeNode46.Text = "생산 관리";
            treeNode47.Name = "노드16";
            treeNode47.Text = "설비-공정관계 설정";
            treeNode48.Name = "노드17";
            treeNode48.Text = "설비 비가동 관리";
            treeNode49.Name = "노드6";
            treeNode49.Text = "설비 관리";
            treeNode50.Name = "노드9";
            treeNode50.Text = "검사데이터 관리";
            treeNode51.Name = "노드19";
            treeNode51.Text = "불량 관리";
            treeNode52.Name = "노드7";
            treeNode52.Text = "품질 관리";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode31,
            treeNode41,
            treeNode46,
            treeNode49,
            treeNode52});
            this.treeView1.Size = new System.Drawing.Size(241, 650);
            this.treeView1.TabIndex = 11;
            // 
            // menuStrip2
            // 
            this.menuStrip2.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.menuStrip2.Location = new System.Drawing.Point(250, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(934, 24);
            this.menuStrip2.TabIndex = 3;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripLabel1,
            this.toolStripLabel2});
            this.toolStrip1.Location = new System.Drawing.Point(250, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(934, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.ForeColor = System.Drawing.SystemColors.Menu;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(59, 22);
            this.toolStripButton1.Text = "로그아웃";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.ForeColor = System.Drawing.SystemColors.Menu;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(43, 22);
            this.toolStripLabel1.Text = "김지원";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel2.ForeColor = System.Drawing.SystemColors.Menu;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(54, 22);
            this.toolStripLabel2.Text = "사용자 : ";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tabControl1.Location = new System.Drawing.Point(250, 49);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(934, 23);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(926, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(976, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.statusStrip1.Location = new System.Drawing.Point(250, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(934, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(219, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

