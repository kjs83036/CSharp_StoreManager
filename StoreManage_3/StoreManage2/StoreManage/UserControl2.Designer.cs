namespace StoreManage
{
    partial class UserControl2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelStoreName = new System.Windows.Forms.Panel();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.buttonSelectDate = new System.Windows.Forms.Button();
            this.dataGridViewSelectDate = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panelStoreName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectDate)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelStoreName);
            this.panel1.Controls.Add(this.buttonSelectDate);
            this.panel1.Controls.Add(this.dataGridViewSelectDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1208, 277);
            this.panel1.TabIndex = 0;
            // 
            // panelStoreName
            // 
            this.panelStoreName.Controls.Add(this.textBoxDate);
            this.panelStoreName.Location = new System.Drawing.Point(3, 3);
            this.panelStoreName.Name = "panelStoreName";
            this.panelStoreName.Size = new System.Drawing.Size(169, 37);
            this.panelStoreName.TabIndex = 10;
            this.panelStoreName.Paint += new System.Windows.Forms.PaintEventHandler(this.panelStoreName_Paint);
            // 
            // textBoxDate
            // 
            this.textBoxDate.BackColor = System.Drawing.Color.White;
            this.textBoxDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDate.Font = new System.Drawing.Font("굴림", 10F);
            this.textBoxDate.Location = new System.Drawing.Point(5, 5);
            this.textBoxDate.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.Size = new System.Drawing.Size(157, 23);
            this.textBoxDate.TabIndex = 2;
            // 
            // buttonSelectDate
            // 
            this.buttonSelectDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));
            this.buttonSelectDate.FlatAppearance.BorderSize = 0;
            this.buttonSelectDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSelectDate.ForeColor = System.Drawing.Color.White;
            this.buttonSelectDate.Location = new System.Drawing.Point(180, 8);
            this.buttonSelectDate.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
            this.buttonSelectDate.Name = "buttonSelectDate";
            this.buttonSelectDate.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectDate.TabIndex = 9;
            this.buttonSelectDate.Text = "검색";
            this.buttonSelectDate.UseVisualStyleBackColor = false;
            this.buttonSelectDate.Click += new System.EventHandler(this.buttonSelectDate_Click);
            // 
            // dataGridViewSelectDate
            // 
            this.dataGridViewSelectDate.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewSelectDate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSelectDate.Location = new System.Drawing.Point(0, 46);
            this.dataGridViewSelectDate.Name = "dataGridViewSelectDate";
            this.dataGridViewSelectDate.RowTemplate.Height = 23;
            this.dataGridViewSelectDate.Size = new System.Drawing.Size(804, 208);
            this.dataGridViewSelectDate.TabIndex = 2;
            this.dataGridViewSelectDate.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSelectDate_CellClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.dataGridView2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 277);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1208, 413);
            this.panel2.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(848, 377);
            this.button4.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "삭제";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(1118, 377);
            this.button3.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "확인";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(938, 377);
            this.button1.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "수정";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(1028, 377);
            this.button5.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 8;
            this.button5.Text = "등록";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // dataGridView2
            // 
            this.dataGridView2.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(1208, 369);
            this.dataGridView2.TabIndex = 0;
            // 
            // UserControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "UserControl2";
            this.Size = new System.Drawing.Size(1208, 690);
            this.panel1.ResumeLayout(false);
            this.panelStoreName.ResumeLayout(false);
            this.panelStoreName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectDate)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridViewSelectDate;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonSelectDate;
        private System.Windows.Forms.Panel panelStoreName;
        private System.Windows.Forms.TextBox textBoxDate;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
    }
}
