namespace StoreManage
{
    partial class UserControl2
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

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
        public void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelStoreName = new System.Windows.Forms.Panel();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.buttonSelectDate = new System.Windows.Forms.Button();
            this.dataGridViewSelectDate = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonDeleteResult = new System.Windows.Forms.Button();
            this.buttonUpdateResult = new System.Windows.Forms.Button();
            this.buttonCreateResult = new System.Windows.Forms.Button();
            this.dataGridViewSelectionDateDetail = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panelStoreName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectDate)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectionDateDetail)).BeginInit();
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
            this.panel1.Size = new System.Drawing.Size(1100, 277);
            this.panel1.TabIndex = 0;
            // 
            // panelStoreName
            // 
            this.panelStoreName.Controls.Add(this.textBoxDate);
            this.panelStoreName.Location = new System.Drawing.Point(19, 3);
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
            this.textBoxDate.Location = new System.Drawing.Point(16, 7);
            this.textBoxDate.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.Size = new System.Drawing.Size(141, 23);
            this.textBoxDate.TabIndex = 2;
            // 
            // buttonSelectDate
            // 
            this.buttonSelectDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));
            this.buttonSelectDate.FlatAppearance.BorderSize = 0;
            this.buttonSelectDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSelectDate.ForeColor = System.Drawing.Color.White;
            this.buttonSelectDate.Location = new System.Drawing.Point(191, 8);
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
            this.dataGridViewSelectDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewSelectDate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSelectDate.Location = new System.Drawing.Point(19, 46);
            this.dataGridViewSelectDate.Margin = new System.Windows.Forms.Padding(30);
            this.dataGridViewSelectDate.Name = "dataGridViewSelectDate";
            this.dataGridViewSelectDate.RowTemplate.Height = 23;
            this.dataGridViewSelectDate.Size = new System.Drawing.Size(1069, 208);
            this.dataGridViewSelectDate.TabIndex = 2;
            this.dataGridViewSelectDate.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSelectDate_CellClick);
            this.dataGridViewSelectDate.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSelectDate_CellContentClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonDeleteResult);
            this.panel2.Controls.Add(this.buttonUpdateResult);
            this.panel2.Controls.Add(this.buttonCreateResult);
            this.panel2.Controls.Add(this.dataGridViewSelectionDateDetail);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 277);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1100, 413);
            this.panel2.TabIndex = 0;
            // 
            // buttonDeleteResult
            // 
            this.buttonDeleteResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));
            this.buttonDeleteResult.FlatAppearance.BorderSize = 0;
            this.buttonDeleteResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteResult.ForeColor = System.Drawing.Color.White;
            this.buttonDeleteResult.Location = new System.Drawing.Point(689, 377);
            this.buttonDeleteResult.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
            this.buttonDeleteResult.Name = "buttonDeleteResult";
            this.buttonDeleteResult.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteResult.TabIndex = 11;
            this.buttonDeleteResult.Text = "삭제";
            this.buttonDeleteResult.UseVisualStyleBackColor = false;
            this.buttonDeleteResult.Click += new System.EventHandler(this.buttonDeleteResult_Click);
            // 
            // buttonUpdateResult
            // 
            this.buttonUpdateResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));
            this.buttonUpdateResult.FlatAppearance.BorderSize = 0;
            this.buttonUpdateResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUpdateResult.ForeColor = System.Drawing.Color.White;
            this.buttonUpdateResult.Location = new System.Drawing.Point(779, 377);
            this.buttonUpdateResult.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
            this.buttonUpdateResult.Name = "buttonUpdateResult";
            this.buttonUpdateResult.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdateResult.TabIndex = 9;
            this.buttonUpdateResult.Text = "수정";
            this.buttonUpdateResult.UseVisualStyleBackColor = false;
            this.buttonUpdateResult.Click += new System.EventHandler(this.buttonUpdateResult_Click);
            // 
            // buttonCreateResult
            // 
            this.buttonCreateResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));
            this.buttonCreateResult.FlatAppearance.BorderSize = 0;
            this.buttonCreateResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreateResult.ForeColor = System.Drawing.Color.White;
            this.buttonCreateResult.Location = new System.Drawing.Point(869, 377);
            this.buttonCreateResult.Margin = new System.Windows.Forms.Padding(15, 15, 0, 0);
            this.buttonCreateResult.Name = "buttonCreateResult";
            this.buttonCreateResult.Size = new System.Drawing.Size(75, 23);
            this.buttonCreateResult.TabIndex = 8;
            this.buttonCreateResult.Text = "등록";
            this.buttonCreateResult.UseVisualStyleBackColor = false;
            this.buttonCreateResult.Click += new System.EventHandler(this.buttonCreateResult_Click);
            // 
            // dataGridViewSelectionDateDetail
            // 
            this.dataGridViewSelectionDateDetail.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewSelectionDateDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewSelectionDateDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSelectionDateDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewSelectionDateDetail.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewSelectionDateDetail.Name = "dataGridViewSelectionDateDetail";
            this.dataGridViewSelectionDateDetail.RowTemplate.Height = 23;
            this.dataGridViewSelectionDateDetail.Size = new System.Drawing.Size(1100, 369);
            this.dataGridViewSelectionDateDetail.TabIndex = 0;
            this.dataGridViewSelectionDateDetail.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewSelectionDateDetail_CellPainting);
            // 
            // UserControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "UserControl2";
            this.Size = new System.Drawing.Size(1100, 690);
            this.VisibleChanged += new System.EventHandler(this.UserControl2_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.panelStoreName.ResumeLayout(false);
            this.panelStoreName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectDate)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectionDateDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.DataGridView dataGridViewSelectDate;
        public System.Windows.Forms.DataGridView dataGridViewSelectionDateDetail;
        public System.Windows.Forms.Button buttonCreateResult;
        public System.Windows.Forms.Button buttonSelectDate;
        public System.Windows.Forms.Panel panelStoreName;
        public System.Windows.Forms.TextBox textBoxDate;
        public System.Windows.Forms.Button buttonDeleteResult;
        public System.Windows.Forms.Button buttonUpdateResult;
    }
}
