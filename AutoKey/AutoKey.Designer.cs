namespace AutoKey
{
    partial class AutoKey
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbStartKey = new System.Windows.Forms.ComboBox();
            this.cbWorkKey = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "시작키";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "작동키";
            // 
            // cbStartKey
            // 
            this.cbStartKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStartKey.FormattingEnabled = true;
            this.cbStartKey.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F"});
            this.cbStartKey.Location = new System.Drawing.Point(119, 44);
            this.cbStartKey.Name = "cbStartKey";
            this.cbStartKey.Size = new System.Drawing.Size(96, 23);
            this.cbStartKey.TabIndex = 1;
            this.cbStartKey.SelectedIndexChanged += new System.EventHandler(this.cbStartKey_SelectedIndexChanged);
            // 
            // cbWorkKey
            // 
            this.cbWorkKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWorkKey.FormattingEnabled = true;
            this.cbWorkKey.Items.AddRange(new object[] {
            "Z",
            "X",
            "C",
            "V",
            "B"});
            this.cbWorkKey.Location = new System.Drawing.Point(119, 79);
            this.cbWorkKey.Name = "cbWorkKey";
            this.cbWorkKey.Size = new System.Drawing.Size(96, 23);
            this.cbWorkKey.TabIndex = 1;
            this.cbWorkKey.SelectedIndexChanged += new System.EventHandler(this.cbWorkKey_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Rate";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(216, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "BPM";
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(119, 114);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(96, 25);
            this.txtRate.TabIndex = 4;
            this.txtRate.Text = "600";
            this.txtRate.TextChanged += new System.EventHandler(this.txtRate_TextChanged);
            // 
            // AutoKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 211);
            this.Controls.Add(this.txtRate);
            this.Controls.Add(this.cbWorkKey);
            this.Controls.Add(this.cbStartKey);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AutoKey";
            this.Text = "AutoKey";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbStartKey;
        private System.Windows.Forms.ComboBox cbWorkKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRate;
    }
}

