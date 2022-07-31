
namespace DESK_POP
{
    partial class ucWorkGroup
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.gBox = new System.Windows.Forms.GroupBox();
            this.dtpWork = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEquipment = new System.Windows.Forms.TextBox();
            this.txtWStatus = new System.Windows.Forms.TextBox();
            this.txtOperation = new System.Windows.Forms.TextBox();
            this.txtProductYN = new System.Windows.Forms.TextBox();
            this.txtWkCode = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.gBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // gBox
            // 
            this.gBox.BackColor = System.Drawing.SystemColors.Control;
            this.gBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gBox.Controls.Add(this.dtpWork);
            this.gBox.Controls.Add(this.label14);
            this.gBox.Controls.Add(this.label4);
            this.gBox.Controls.Add(this.label3);
            this.gBox.Controls.Add(this.label8);
            this.gBox.Controls.Add(this.label15);
            this.gBox.Controls.Add(this.label1);
            this.gBox.Controls.Add(this.txtEquipment);
            this.gBox.Controls.Add(this.txtWStatus);
            this.gBox.Controls.Add(this.txtOperation);
            this.gBox.Controls.Add(this.txtProductYN);
            this.gBox.Controls.Add(this.txtWkCode);
            this.gBox.Controls.Add(this.button2);
            this.gBox.Location = new System.Drawing.Point(3, 3);
            this.gBox.Name = "gBox";
            this.gBox.Size = new System.Drawing.Size(336, 332);
            this.gBox.TabIndex = 1;
            this.gBox.TabStop = false;
            this.gBox.Text = "작업1";
            // 
            // dtpWork
            // 
            this.dtpWork.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtpWork.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpWork.Location = new System.Drawing.Point(144, 147);
            this.dtpWork.Name = "dtpWork";
            this.dtpWork.Size = new System.Drawing.Size(187, 27);
            this.dtpWork.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label14.Location = new System.Drawing.Point(19, 107);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 20);
            this.label14.TabIndex = 2;
            this.label14.Text = "설비명";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(19, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "작업진행단계";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(19, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "작업예정일";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.Location = new System.Drawing.Point(19, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "공정명";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label15.Location = new System.Drawing.Point(19, 236);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(99, 20);
            this.label15.TabIndex = 2;
            this.label15.Text = "자제불출여부";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(19, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "작업지시코드";
            // 
            // txtEquipment
            // 
            this.txtEquipment.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtEquipment.Location = new System.Drawing.Point(144, 104);
            this.txtEquipment.Name = "txtEquipment";
            this.txtEquipment.Size = new System.Drawing.Size(187, 27);
            this.txtEquipment.TabIndex = 1;
            // 
            // txtWStatus
            // 
            this.txtWStatus.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtWStatus.Location = new System.Drawing.Point(144, 190);
            this.txtWStatus.Name = "txtWStatus";
            this.txtWStatus.Size = new System.Drawing.Size(187, 27);
            this.txtWStatus.TabIndex = 1;
            // 
            // txtOperation
            // 
            this.txtOperation.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtOperation.Location = new System.Drawing.Point(144, 61);
            this.txtOperation.Name = "txtOperation";
            this.txtOperation.Size = new System.Drawing.Size(187, 27);
            this.txtOperation.TabIndex = 1;
            // 
            // txtProductYN
            // 
            this.txtProductYN.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtProductYN.Location = new System.Drawing.Point(144, 233);
            this.txtProductYN.Name = "txtProductYN";
            this.txtProductYN.Size = new System.Drawing.Size(187, 27);
            this.txtProductYN.TabIndex = 1;
            // 
            // txtWkCode
            // 
            this.txtWkCode.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtWkCode.Location = new System.Drawing.Point(144, 18);
            this.txtWkCode.Name = "txtWkCode";
            this.txtWkCode.Size = new System.Drawing.Size(187, 27);
            this.txtWkCode.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.Location = new System.Drawing.Point(84, 281);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 40);
            this.button2.TabIndex = 0;
            this.button2.Text = "진행하기";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ucWorkGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gBox);
            this.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "ucWorkGroup";
            this.Size = new System.Drawing.Size(342, 338);
            this.Load += new System.EventHandler(this.ucWorkGroup_Load);
            this.gBox.ResumeLayout(false);
            this.gBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gBox;
        private System.Windows.Forms.DateTimePicker dtpWork;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEquipment;
        private System.Windows.Forms.TextBox txtWStatus;
        private System.Windows.Forms.TextBox txtOperation;
        private System.Windows.Forms.TextBox txtProductYN;
        private System.Windows.Forms.TextBox txtWkCode;
        private System.Windows.Forms.Button button2;
    }
}
