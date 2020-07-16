using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace StoreManage
{
    public partial class UserControl2 : UserControl
    {
        private readonly SqlQuery sqlQuery;
        private readonly Crud crud;
        private readonly TempFuncForWinform tempFuncForWinform;
        private readonly string id;

        public UserControl2(string id, SqlQuery sqlQuery, Crud crud, TempFuncForWinform tempFuncForWinform)
        {
            InitializeComponent();
            

            this.sqlQuery = sqlQuery;
            this.crud = crud;
            this.tempFuncForWinform = tempFuncForWinform;

            this.id = id;
        }

        private void dataGridViewInit()
        {
            dataGridView2.ColumnCount = 10;
            dataGridView2.RowCount = 10;
            string[] cellLabelArray = new string[]
            {
                "개통일", "가입자명", "은행", "모델명", "이전통신사",
                "공시지원금", "리베이트/NET", "보험가입유무", "송금금액", "현금판매금액",
                "모바일 개통시간", "가입자생년", "계좌번호", "일련번호", "현/카/할",
                "할부원금", "요금제", "유심후납", "예정일", "카드판매금액",
                "대리점", "요금제변경일", "예금주", "고객명", "출고가",
                "선납금", "정산금", "현금", "기타차감", "매장",
                "CIA", "개통번호", "정책추가", "유심선납", "카드",
                "세후금액", "판매자", "비고", "약정유형", "MNP수수료",
                "구두추가", "부가서비스추가/차감", "대리점", "마진"
            };
            for(int i = 0; i < 44; i++)
            {
                if (i < 10)
                {
                    dataGridView2[0, i].Value = cellLabelArray[i];
                    continue;
                }
                if (i < 20)
                {
                    dataGridView2[2, i - 10].Value = cellLabelArray[i];
                    continue;
                }
                if (i < 26)
                {
                    dataGridView2[4, i - 20].Value = cellLabelArray[i];
                    continue;
                }
                if (i == 27)
                {
                    dataGridView2[4, i - 20].Value = cellLabelArray[i - 1];
                    continue;
                }
                if (i < 30)
                {
                    dataGridView2[4, i - 20].Value = cellLabelArray[i - 1];
                    continue;
                }
                if ( i == 30)
                {
                    dataGridView2[6, i - 30].Value = cellLabelArray[i - 1];
                    continue;
                }
                if (i == 32 && i ==33)
                {
                    dataGridView2[6, i - 30].Value = cellLabelArray[i - 2];
                    continue;
                }

            }
        
        
            dataGridView2[2, 6].Value = "요금제";

            mergeCells(dataGridView2[3, 6], true, false);
            mergeCells(dataGridView2[4, 6], true, false);


        }
        //셀병합용 셀페인트 이벤트
        private void dataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            DataGridViewCell dgvCell = ((DataGridView)sender)[e.ColumnIndex, e.RowIndex];
            if (dgvCell.Tag == null)
            {
                return;
            }

            string hide = dgvCell.Tag.ToString();

            if (hide.Contains("R"))
            {
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single;
            }

            if (hide.Contains("B"))
            {
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single;
            }
        }

        //셀병합용
        private void mergeCells(DataGridViewCell cell, bool mergeH, bool mergeV)
        {
            string m = "";
            if (mergeH)
            {
                m += "R";  // merge horizontally by hiding the right border line
            }

            if (mergeV)
            {
                m += "B"; // merge vertically by hiding the bottom border line
            }

            cell.Tag = m == "" ? null : m;
        }

        private void panelStoreName_Paint(object sender, PaintEventArgs e)
        {
            textBoxDate.BorderStyle = BorderStyle.None;
            Graphics g = e.Graphics;
            g.DrawRectangle(Pens.Blue, new Rectangle(textBoxDate.Location.X - 3, textBoxDate.Location.Y - 3, textBoxDate.Width + 3, textBoxDate.Height + 3));
        }

        private void buttonSelectDate_Click(object sender, EventArgs e)
        {//추가 query 필요 : 매장, 판매자 ,가입자명, 전화번호, 
            string querySelectDate = $"SELECT R.RESULT_OPEN_DATE, R.AGENT, T.ST_NAME, E.ST_EMP_MEMBER, U.USER_NAME, R.USER_OPEN_NUMBER, R.USER_PREVIOUS_COM, R.NOTE FROM ST_EMP_RESULT AS R INNER JOIN ST_TABLE AS T ON R.ST_CODE=T.ST_CODE AND R.RESULT_OPEN_DATE LIKE '{textBoxDate.Text}%' INNER JOIN USER_T AS U ON R.USER_CODE = U.USER_CODE INNER JOIN ST_EMPLOYEE AS E ON R.ST_EMP_CODE = E.ST_EMP_CODE";
            DataSet dataSetSelectDate = crud.ReadToGrid_MySql(querySelectDate);

            dataSetSelectDate.Tables[0].Columns["RESULT_OPEN_DATE"].ColumnName = "개통일";
            dataSetSelectDate.Tables[0].Columns["AGENT"].ColumnName = "대리점";
            dataSetSelectDate.Tables[0].Columns["ST_NAME"].ColumnName = "매장";
            dataSetSelectDate.Tables[0].Columns["ST_EMP_MEMBER"].ColumnName = "판매자";
            dataSetSelectDate.Tables[0].Columns["USER_NAME"].ColumnName = "가입자명";
            dataSetSelectDate.Tables[0].Columns["USER_OPEN_NUMBER"].ColumnName = "개통번호";
            dataSetSelectDate.Tables[0].Columns["USER_PREVIOUS_COM"].ColumnName = "통신사";
            dataSetSelectDate.Tables[0].Columns["NOTE"].ColumnName = "비고";

            tempFuncForWinform.setDataGridView(dataSetSelectDate, dataGridViewSelectDate);
            dataGridViewSelectDate.DefaultCellStyle.Format = "yyyy-MM-dd";
        }

        private void dataGridViewSelectDate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            string querySelectResult = sqlQuery.SelectAllFrom_Where_("st_emp_result", new string[] { "user_open_number" }, new string[] { (sender as DataGridView).Rows[e.RowIndex].Cells["개통번호"].Value.ToString() });
            St_emp_result st_emp_result = crud.Read_MySql(querySelectResult, new St_emp_result());
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(new DataTable());
            tempFuncForWinform.setDataGridView(dataSet, dataGridView2);
            dataGridView2.DataSource = null;
            dataGridView2.ColumnHeadersVisible = false;
            dataGridViewInit();


        }
    }


}
