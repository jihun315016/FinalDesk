using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESK_POP
{
    public partial class ccTextBox : TextBox
    {
        public bool isRequired { get; set; } // 필수 입력 여부
        public bool isNumeric { get; set; } // 숫자만 입력 가능한지 설정
        public string PlaceHolder { get; set; } // PlaceHolder 설정

        public ccTextBox()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        /// <summary>
        /// Author : 강지훈
        /// isNumeric이 적용되었다면 숫자가 입력되었는지 검사한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (isNumeric && !char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Author : 강지훈
        /// 사용자에 의해 입력된 것이 없는 ccTextBox에서 입력이 시작된다면
        /// ccTextBox.Text를 비워준다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccTextBox_Enter(object sender, EventArgs e)
        {
            if (this.Text.Equals(this.PlaceHolder))
            {
                this.Text = string.Empty;
                this.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Author : 강지훈
        /// Leave 이벤트 발생시 사용자에 의해 따로 입력된 것이 없다면 PlaceHolder를 표시한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccTextBox_Leave(object sender, EventArgs e)
        {
            if (this.Text.Equals(string.Empty) && !string.IsNullOrWhiteSpace(this.PlaceHolder))
            {
                this.Text = this.PlaceHolder;
                this.ForeColor = Color.Gray;
            }
        }


        /// <summary>
        /// Author : 강지훈
        /// PlaceHolder를 표시해준다.
        /// </summary>
        public void SetPlaceHolder()
        {
            ccTextBox_Leave(this, null);
        }
    }
}
