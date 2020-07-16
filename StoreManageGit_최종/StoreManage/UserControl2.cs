using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace StoreManage
{
    public partial class UserControl2 : UserControl
    {
        public readonly SqlQuery sqlQuery;
        public readonly Crud crud;
        public readonly TempFuncForWinform tempFuncForWinform;

        //전역변수 시작
        Employee employee;
        string selectedUser_phone_number;
        //전역변수 끝

        SortedSet<string> setCheckedResult;

        List<ComboBox> listComboBox;
        List<TextBox> listTextBox;
        string[] cellLabelArray;

        public UserControl2(Employee employee, SqlQuery sqlQuery, Crud crud, TempFuncForWinform tempFuncForWinform)
        {
            InitializeComponent();


            this.sqlQuery = sqlQuery;
            this.crud = crud;
            this.tempFuncForWinform = tempFuncForWinform;

            this.employee = employee;
            this.setCheckedResult = new SortedSet<string>();

            this.listComboBox = new List<ComboBox>();
            this.listTextBox = new List<TextBox>();

            //라벨 배열
            this.cellLabelArray = new string[]
            {
                "개통일", "가입자명", "은행", "모델명", "이전통신사",
                "공시지원금", "리베이트/NET", "보험가입유무", "송금금액", "현금판매금액",
                "모바일 개통시간", "가입자생년", "계좌번호", "일련번호", "현/카/할",
                "할부원금", "요금제", "유심후납", "예정일", "카드판매금액",
                "대리점", "요금제변경일", "예금주", "고객명", "출고가",
                "선납금", "", "정산금", "현금", "기타차감", "매장", "",
                "CIA", "개통번호", "", "", "정책추가", "유심선납", "카드",
                "세후금액", "판매자", "", "비고", "약정유형", "", "MNP수수료",
                "구두추가", "부가서비스추가/차감", "", "마진"
            };
            makeDetailTable(false, new DataGridViewCellEventArgs(0, 0));

        }

        /// <summary>
        /// <para>기능 : 실적 입력용 테이블 초기화</para>
        /// <para>진입 : </para>
        /// <para>분기 : </para>
        /// <para>로직 : 1. 10X10 테이블 생성</para>
        /// <para>로직 : 2. 특정 셀 병합</para>
        /// <para>로직 : 3. 라벨추가</para>
        /// </summary>
        public void dataGridViewInit()
        {
            //10X10 테이블 생성
            dataGridViewSelectionDateDetail.ColumnCount = 10;
            dataGridViewSelectionDateDetail.RowCount = 10;
            dataGridViewSelectionDateDetail.ReadOnly = false;

            //긁어온 함수 셀병합
            mergeCells(dataGridViewSelectionDateDetail[3, 6], true, false);
            mergeCells(dataGridViewSelectionDateDetail[4, 6], true, false);


            //라벨추가
            for (int i = 0; i < cellLabelArray.Length; i++)
            {
                dataGridViewSelectionDateDetail[2 * Convert.ToInt32(i / 10), i - 10 * Convert.ToInt32(i / 10)].Value = cellLabelArray[i];
                dataGridViewSelectionDateDetail[2 * Convert.ToInt32(i / 10), i - 10 * Convert.ToInt32(i / 10)].ReadOnly = true;
            }

        }
        /// <summary>
        /// <para>기능 : 셀 병합용 셀페인트 이벤트</para>
        /// <para>진입 : </para>
        /// <para>분기 : 1. e.Columnindex &lt; 0 || e.RowIndex &lt; 0</para>
        /// <para>분기 : 2. dgv.Cell.Tag == null</para>
        /// <para>분기 : 3. dgvCell.Tag.Tostring().Corntains("R")</para>
        /// <para>분기 : 4. dgvCell.Tag.Tostring().Corntains("B")</para>
        /// <para>로직 : 1. if 로우 혹은 컬럼이 음수</para>
        /// <para>로직 : ----2. return</para>
        /// <para>로직 : 3.  if 셀 태그가 null</para>
        /// <para>로직 : ----4. return</para>
        /// <para>로직 : 5. if 셀태그가 &quot;R&quot;을 품는 경우</para>
        /// <para>로직 : ----6. 오른쪽 셀 외각선 없음</para>
        /// <para>로직 : 7. if 셀태그가 &quot;B&quot;을 품는 경우</para>
        /// <para>로직 : ----9. 아래쪽 셀 외각선 없음</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //셀병합용 셀페인트 이벤트
        public void dataGridViewSelectionDateDetail_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

            if (hide.Contains("B"))
            {
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            }
        }

        /// <summary>
        /// <para>기능 : 셀병합</para>
        /// <para>진입 : </para>
        /// <para>분기 : 1. mergeH == true</para>
        /// <para>분기 : 2. mergeV == true</para>
        /// <para>로직 : 1. if mergeH == true</para>
        /// <para>로직 : ----2. 태그에 &quot;R&quot;추가</para>
        /// <para>로직 : 3. if mergeV == true</para>
        /// <para>로직 : ----4. 태그에 &quot;B&quot;추가</para>
        /// </summary>
        /// <param name="cell">datagridviewcell</param>
        /// <param name="mergeH">수평병합일 경우 true</param>
        /// <param name="mergeV">수직병합일 경우 true</param>
        //셀병합용
        public void mergeCells(DataGridViewCell cell, bool mergeH, bool mergeV)
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
        /// <summary>
        /// <para>기능 : 텍스트 박스 외각선 그리기</para>
        /// <para>진입 : </para>
        /// <para>분기 : </para>
        /// <para>로직 : 1.storeManage.drawTextBoxBorder </para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void panelStoreName_Paint(object sender, PaintEventArgs e)
        {
            StoreManage storeManage = new StoreManage(employee);
            storeManage.drawTextBoxBorder(sender, e, textBoxDate);
        }
        /// <summary>
        /// <para>기능 : 입력을 포함한 검색결과 테이블로 출력</para>
        /// <para>진입 : </para>
        /// <para>분기 : </para>
        /// <para>로직 : 1. readResultTablePattern</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonSelectDate_Click(object sender, EventArgs e)
        {
            readResultTablePattern();
        }
        /// <summary>
        /// <para>기능 : 실적 상세 테이블 생성</para>
        /// <para>진입 : </para>
        /// <para>분기 : 행은 0이상, 열은 1이상</para>
        /// <para>로직 : 1. resetToDefault</para>
        /// <para>로직 : 2. e.RowIndex &gt;= 0 &amp;&amp; e.ColumnIndex &gt;= 1</para>
        /// <para>로직 : ----3. makeDetailTable</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dataGridViewSelectDate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //아래 버튼 초기화
            resetToDefault();

            if (e.RowIndex >= 0 && e.ColumnIndex >= 1)
            {
                //할당
                makeDetailTable(true, e);

            }

        }
        /// <summary>
        /// <para>기능 :  실적테이블 check박스 체크 상태 저장</para>
        /// <para>진입 :  e.RowIndex >= 0 &amp;&amp; e.ColumnIndex >=0 &amp;&amp; (sender as DataGridView).CurrentCell is DataGridViewCheckBoxCell)</para>
        /// <para>분기 :  1. 테이블 에서 선택된 체크박스 값이 변했고 그값 == ture</para>
        /// <para>로직 :  1. if 테이블 에서 선택된 체크박스 값이 변했고 그값 == ture</para>
        /// <para>        ----2. 선택된 행의 &quot;개통번호&quot; setCheckedResult에 추가</para>
        /// <para>        ----return</para>
        /// <para>        3. 선택된 행의 &quot;개통번호&quot; setCheckedResult에 삭제</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dataGridViewSelectDate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0 && (sender as DataGridView).CurrentCell is DataGridViewCheckBoxCell)

            {
                if ((bool)(dataGridViewSelectDate.Rows[e.RowIndex].Cells[0].EditedFormattedValue ?? false) == true)
                {
                    setCheckedResult.Add((string)dataGridViewSelectDate.Rows[e.RowIndex].Cells["개통번호"].Value);
                    return;
                }
                setCheckedResult.Remove((string)dataGridViewSelectDate.Rows[e.RowIndex].Cells["개통번호"].Value);
            }
        }
        /// <summary>
        /// <para>기능 : 선택된 실적 제거 </para>
        /// <para>진입 : 1. setCheckedResult.Count &gt; 0</para>
        /// <para>분기 : 1. 메시지 박스 Yes 선택</para>
        /// <para>분기 : 2. queryResult > 0</para>
        /// <para>로직 : 1. if 체크된 이름 모두 메시지 박스를 통해 보여주고 Yes선택시 </para>
        /// <para>로직 : ----2. 반복문을 통해 체크된 실적들을 제거</para>
        /// <para>로직 : ----3. sqlQuery.SelectAllFrom_Where_와 Crud.Read사용하여 st_emp_result를 얻고 그 st_emp_result를 삭제</para>
        /// <para>로직 : ----4. 삭제 결과 가 0초과일 경우 삭제 된 결과 메시지로 출력</para>
        /// <para>로직 : --------5. 삭제 결과 가 0이하일 경우 삭제 된 결과 메시지로 출력 및 삭제 실패 출력</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonDeleteResult_Click(object sender, EventArgs e)
        {
            string checkedNumbers = "";
            if (setCheckedResult.Count > 0)
            {
                foreach (var v in setCheckedResult)
                {
                    checkedNumbers += v + " ";
                }

                if (MessageBox.Show(checkedNumbers + "선택", "주의", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string resultName = "";
                    foreach (var v in setCheckedResult)
                    {

                        string queryRead = sqlQuery.SelectAllFrom_Where_("st_emp_result", new string[] { "user_open_number" }, new string[] { v });
                        St_emp_result store_emp_result = crud.Read_MySql(queryRead, new St_emp_result());
                        //delete시 0번째 필드가 아닌 특정 필드를 지우고 싶을 때 사용 현재 datetime만 예외처리가 되어있기 때문에 datetime 외에 null이 불가능한 필드가 있을경우 버그 생길 예정
                        St_emp_result store_emp_resultForDelete = new St_emp_result
                        {
                            user_open_number = store_emp_result.user_open_number
                        };

                        string queryDelete = sqlQuery.deleteFrom_Where_("st_emp_result", "user_open_number");
                        int queryResult = crud.Delete_MySql(queryDelete, store_emp_resultForDelete);

                        if (queryResult > 0)
                        {
                            resultName += v + " ";
                        }
                        else
                        {
                            MessageBox.Show($"({ resultName})삭제 성공, ({v})삭제 실패");

                            clearSelectedDetail(true);

                            textBoxDate.Text = "";

                            readResultTablePattern();

                            return;
                        }

                    }
                    MessageBox.Show(resultName + "삭제 성공");

                    clearSelectedDetail(true);

                    textBoxDate.Text = "";

                    readResultTablePattern();
                }
            }
        }
        /// <summary>
        /// <para>기능 : 실적 변경</para>
        /// <para>진입 : 메시지 박스 Yes 클릭</para>
        /// <para>분기 : 1. buttonUpdateResult.Text == &quot;변경&quot;</para>
        /// <para>분기 : ----2. 검색시 항목 경 가능 처리</para>
        /// <para>분기 : ----3. 변경할 수 없는 항목 처리</para>
        /// <para>분기 : ----4. 버튼 텍스트 &quot;적용&quot;로 변경</para>
        /// <para>분기 : ----return</para>
        /// <para>분기 : 5. sqlQuery.SelectAllFrom_Where_와 crud.Read를 통해 기존 데이터 불러옴</para>
        /// <para>분기 : 6. 변경사항 st_emp_result에 할당</para>
        /// <para>분기 : 6. sqlQuery.Update_Set_와 crud.update를 를 통해 변경 적용</para>
        /// <para>분기 : 7. if 결과가 0 보다 클경우</para>
        /// <para>분기 : ----8. 성공 메시지박스띄움</para>
        /// <para>분기 : ----9. readResultTablePattern</para>
        /// <para>분기 : ----10. 버튼 텍스트 &quot;변경&quot; 적용</para>
        /// <para>분기 : ----return</para>
        /// <para>분기 : 11. 실패 메시지</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonUpdateResult_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("변경하시겟습니까?", "변경", MessageBoxButtons.YesNo) == DialogResult.Yes && selectedUser_phone_number != null)
            {
                if (buttonUpdateResult.Text == "변경")
                {
                    // 검색시 항목 변경 가능 처리
                    changeDatagridViewDetailWritable();
                    //변경할수 없는 항목 처리
                    cellNotResultDisable();

                    buttonUpdateResult.Text = "적용";
                    return;

                }
                string queryUpdate = sqlQuery.Update_Set_("st_emp_result", new St_emp_result(), "user_open_number");

                string queryRead = sqlQuery.SelectAllFrom_Where_("st_emp_result", new string[] { "user_open_number" }, new string[] { selectedUser_phone_number });
                St_emp_result storeRead = crud.Read_MySql(queryRead, new St_emp_result());

                //변경 할당
                storeRead.result_open_date = Convert.ToDateTime(dataGridViewSelectionDateDetail[1, 0].Value.ToString());
                storeRead.result_open_m_date = Convert.ToDateTime(dataGridViewSelectionDateDetail[3, 0].Value.ToString());
                storeRead.result_ph_m = dataGridViewSelectionDateDetail[1, 3].Value.ToString();
                storeRead.result_serial_number = dataGridViewSelectionDateDetail[3, 3].Value.ToString();
                storeRead.user_open_number = dataGridViewSelectionDateDetail[7, 3].Value.ToString();
                storeRead.user_agree_type = dataGridViewSelectionDateDetail[9, 3].Value.ToString();
                storeRead.user_previous_com = dataGridViewSelectionDateDetail[1, 4].Value.ToString();
                storeRead.user_pur_type = dataGridViewSelectionDateDetail[3, 4].Value.ToString();
                storeRead.release_price = dataGridViewSelectionDateDetail[5, 4].Value.ToString();
                storeRead.subsidy_price = dataGridViewSelectionDateDetail[1, 5].Value.ToString();
                storeRead.installment_price = dataGridViewSelectionDateDetail[3, 5].Value.ToString();
                storeRead.prepayments = dataGridViewSelectionDateDetail[5, 5].Value.ToString();
                storeRead.mnp_commission = dataGridViewSelectionDateDetail[9, 5].Value.ToString();
                storeRead.price_plan = dataGridViewSelectionDateDetail[3, 6].Value.ToString();
                storeRead.net_rebate = dataGridViewSelectionDateDetail[1, 6].Value.ToString();
                storeRead.add_policy = dataGridViewSelectionDateDetail[7, 6].Value.ToString();
                storeRead.sound_policy = dataGridViewSelectionDateDetail[9, 6].Value.ToString();
                storeRead.addition_service = dataGridViewSelectionDateDetail[9, 7].Value.ToString();
                storeRead.insurance_YN = dataGridViewSelectionDateDetail[1, 7].Value.ToString();
                storeRead.late_payment_usim = dataGridViewSelectionDateDetail[3, 7].Value.ToString();
                storeRead.prepayments_usim = dataGridViewSelectionDateDetail[7, 7].Value.ToString();
                storeRead.user_calculate = dataGridViewSelectionDateDetail[5, 7].Value.ToString();
                storeRead.send_price = dataGridViewSelectionDateDetail[1, 8].Value.ToString();
                storeRead.due_date = Convert.ToDateTime(dataGridViewSelectionDateDetail[3, 8].Value.ToString());
                storeRead.cash = dataGridViewSelectionDateDetail[5, 8].Value.ToString();
                storeRead.card = dataGridViewSelectionDateDetail[7, 8].Value.ToString();
                storeRead.agent = dataGridViewSelectionDateDetail[5, 0].Value.ToString();
                storeRead.card_sale_price = dataGridViewSelectionDateDetail[3, 9].Value.ToString();
                storeRead.cash_sale_price = dataGridViewSelectionDateDetail[1, 9].Value.ToString();
                storeRead.etc_offset = dataGridViewSelectionDateDetail[5, 9].Value.ToString();
                storeRead.after_tax_price = dataGridViewSelectionDateDetail[7, 9].Value.ToString();
                storeRead.profit_margin = dataGridViewSelectionDateDetail[9, 9].Value.ToString();
                storeRead.note = dataGridViewSelectionDateDetail[9, 2].Value.ToString();

                int result = crud.Update_MySql(queryUpdate, storeRead);

                if (result > 0)
                {
                    MessageBox.Show("성공");

                    readResultTablePattern();

                    buttonUpdateResult.Text = "변경";
                    return;
                }
                MessageBox.Show("실패");
            }
        }
        /// <summary>
        /// <para>기능 : 실적 등록</para>
        /// <para>진입 : 등록 메시지 박스 Yes클릭</para>
        /// <para>분기 : 1. buttonCreateResult.Text == &quot;등록&quot;</para>
        /// <para>로직 : 1. 버튼 텍스트 == 등록</para>
        /// <para>로직 : ----2. clearSelectedDetail</para>
        /// <para>로직 : ----3. cellNotResultDisable</para>
        /// <para>로직 : ----4. 버튼텍스트 &quot;적용&quot; 으로 변경</para>
        /// <para>로직 : ----return</para>
        /// <para>로직 : 5. st_emp_result에 등록사항 할당</para>
        /// <para>로직 : 6. sqlQuery.InsertInto__Values_ 와 Crud.Create를 사용하여 등록</para>
        /// <para>로직 : 7. 변경사항이 0 초과 일 경우</para>
        /// <para>로직 : ----8. 성공 메시지</para>
        /// <para>로직 : ----9. readResultTablePattern </para>
        /// <para>로직 : ----10. 버튼 텍스드 &quot;등록&quot;으로 변경 </para>
        /// <para>로직 : ----11. clearSelectedDetail </para>
        /// <para>로직 : 12. 실패 메시지 </para>
        /// <para>로직 : 13. clearSelectedDetail </para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonCreateResult_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("등록하시겟습니까?", "등록", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                selectedUser_phone_number = null;

                if (buttonCreateResult.Text == "등록")
                {
                    //datagrid클리어 및 쓰기가능
                    clearSelectedDetail(false);
                    //변경할수 없는 항목 처리
                    cellNotResultDisable();

                    buttonCreateResult.Text = "적용";

                    return;

                }

                St_emp_result st_emp_result = new St_emp_result
                {
                    // datagridviewSelectionDateDetail의 각 항목은 makeDetailTable() 참고

                    //직원코드 필요 임시로 id = st_emp_code
                    st_emp_code = employee.st_emp_code,
                    //매장코드 필요
                    st_code = employee.st_code,
                    result_date = DateTime.Now,
                    result_open_date = Convert.ToDateTime(dataGridViewSelectionDateDetail[1, 0].Value.ToString()),
                    result_open_m_date = Convert.ToDateTime(dataGridViewSelectionDateDetail[3, 0].Value.ToString()),
                    //정책 없음
                    result_policy = "",
                    //대리점코드 없음
                    agent_code = "",
                    result_ph_m = dataGridViewSelectionDateDetail[1, 3].Value.ToString(),
                    result_serial_number = dataGridViewSelectionDateDetail[3, 3].Value.ToString(),
                    //유저코드 필요 임시로 1 현재 유저가 1뿐임
                    user_code = "1",
                    user_open_number = dataGridViewSelectionDateDetail[7, 3].Value.ToString(),
                    user_agree_type = dataGridViewSelectionDateDetail[9, 3].Value.ToString(),
                    //개통유형 없음
                    user_open_type = "",
                    user_previous_com = dataGridViewSelectionDateDetail[1, 4].Value.ToString(),
                    user_pur_type = dataGridViewSelectionDateDetail[3, 4].Value.ToString(),
                    release_price = dataGridViewSelectionDateDetail[5, 4].Value.ToString(),
                    subsidy_price = dataGridViewSelectionDateDetail[1, 5].Value.ToString(),
                    installment_price = dataGridViewSelectionDateDetail[3, 5].Value.ToString(),
                    prepayments = dataGridViewSelectionDateDetail[5, 5].Value.ToString(),
                    mnp_commission = dataGridViewSelectionDateDetail[9, 5].Value.ToString(),
                    price_plan = dataGridViewSelectionDateDetail[3, 6].Value.ToString(),
                    net_rebate = dataGridViewSelectionDateDetail[1, 6].Value.ToString(),
                    add_policy = dataGridViewSelectionDateDetail[7, 6].Value.ToString(),
                    sound_policy = dataGridViewSelectionDateDetail[9, 6].Value.ToString(),
                    addition_service = dataGridViewSelectionDateDetail[9, 7].Value.ToString(),
                    insurance_YN = dataGridViewSelectionDateDetail[1, 7].Value.ToString(),
                    late_payment_usim = dataGridViewSelectionDateDetail[3, 7].Value.ToString(),
                    prepayments_usim = dataGridViewSelectionDateDetail[7, 7].Value.ToString(),
                    user_calculate = dataGridViewSelectionDateDetail[5, 7].Value.ToString(),
                    send_price = dataGridViewSelectionDateDetail[1, 8].Value.ToString(),
                    due_date = Convert.ToDateTime(dataGridViewSelectionDateDetail[3, 8].Value.ToString()),
                    cash = dataGridViewSelectionDateDetail[5, 8].Value.ToString(),
                    card = dataGridViewSelectionDateDetail[7, 8].Value.ToString(),
                    agent = dataGridViewSelectionDateDetail[5, 0].Value.ToString(),
                    card_sale_price = dataGridViewSelectionDateDetail[3, 9].Value.ToString(),
                    cash_sale_price = dataGridViewSelectionDateDetail[1, 9].Value.ToString(),
                    etc_offset = dataGridViewSelectionDateDetail[5, 9].Value.ToString(),
                    after_tax_price = dataGridViewSelectionDateDetail[7, 9].Value.ToString(),
                    profit_margin = dataGridViewSelectionDateDetail[9, 9].Value.ToString(),
                    note = dataGridViewSelectionDateDetail[9, 2].Value.ToString()

                };

                string queryCreate = sqlQuery.InsertInto__Values_(st_emp_result);
                int result = crud.Create_MySql(queryCreate, st_emp_result);

                if (result > 0)
                {
                    MessageBox.Show("성공");
                    textBoxDate.Text = "";
                    readResultTablePattern();
                    buttonCreateResult.Text = "등록";
                    clearSelectedDetail(true);


                    return;
                }
                MessageBox.Show("실패");
                clearSelectedDetail(false);

            }
        }
        /// <summary>
        /// <para>기능 : 실적 상세 테이블 클리어 </para>
        /// <para>진입 : </para>
        /// <para>분기 : </para>
        /// <para>로직 : 1. sqlquery.SelectAllFrom_Where_과 Crud.Read를 통해 st_table 생성</para>
        /// <para>로직 : 2. st_table을 통해 combobox보완</para>
        /// <para>로직 : 3. &quot;&quot;를 통해 입력컨트롤 클리어</para>
        /// <para>로직 : 5. 테이블 Readonly = false 설정</para>
        /// <para>로직 : 4. label Readonly 설정</para>
        /// <para>로직 : 6. if needReadOnly == true</para>
        /// <para>로직 : ----7. 테이블 readonly = true 설정</para>
        /// </summary>
        /// <param name="needReadOnly"></param>
        //false시 readonly 추가
        public void clearSelectedDetail(bool needReadOnly)
        {
            string querySelectStore = sqlQuery.SelectAllFrom_Where_("st_table", new string[] { "st_code" }, new string[] { employee.st_code });
            St_table st_table = crud.Read_MySql(querySelectStore, new St_table());

            //개통일
            dataGridViewSelectionDateDetail[1, 0].Value = "";

            //가입자명
            dataGridViewSelectionDateDetail[1, 1].Value = "";
            //은행 user_bank user_t
            dataGridViewSelectionDateDetail[1, 2].Value = "";
            //모델명
            dataGridViewSelectionDateDetail[1, 3].Value = "";

            DataGridViewComboBoxCell comboPreviousCom = new DataGridViewComboBoxCell();
            comboPreviousCom.Items.Add("1");
            comboPreviousCom.Items.Add("SKT");
            comboPreviousCom.Items.Add("KT");
            comboPreviousCom.Items.Add("LG");
            dataGridViewSelectionDateDetail[1, 4] = comboPreviousCom;

            //이전통신사 combo
            comboPreviousCom.Value = "SKT";
            //공시지원금
            dataGridViewSelectionDateDetail[1, 5].Value = "";
            //리베이트/NET
            dataGridViewSelectionDateDetail[1, 6].Value = "";
            //보험가입유무
            dataGridViewSelectionDateDetail[1, 7].Value = "";
            //송금금액
            dataGridViewSelectionDateDetail[1, 8].Value = "";
            //현금파냄 금액
            dataGridViewSelectionDateDetail[1, 9].Value = "";
            //모바일 개통시간
            dataGridViewSelectionDateDetail[3, 0].Value = "";
            //가입자 생년 user_birth user_t
            dataGridViewSelectionDateDetail[3, 1].Value = "";
            //계좌번호 user_bank_number user_t
            dataGridViewSelectionDateDetail[3, 2].Value = "";
            //일련번호
            dataGridViewSelectionDateDetail[3, 3].Value = "";
            //현/카/할
            dataGridViewSelectionDateDetail[3, 4].Value = "";
            //할부원금
            dataGridViewSelectionDateDetail[3, 5].Value = "";

            DataGridViewComboBoxCell comboPricePlan = new DataGridViewComboBoxCell();
            comboPricePlan.Items.Add("1");
            comboPricePlan.Items.Add("1만원");
            comboPricePlan.Items.Add("2만원");
            comboPricePlan.Items.Add("3만원");
            dataGridViewSelectionDateDetail[3, 6] = comboPricePlan;

            //요금제 combo
            comboPricePlan.Value = "1만원";
            //유심후납
            dataGridViewSelectionDateDetail[3, 7].Value = "";
            //예정일
            dataGridViewSelectionDateDetail[3, 8].Value = "";
            //카드판매금액
            dataGridViewSelectionDateDetail[3, 9].Value = "";

            DataGridViewComboBoxCell comboAgent = new DataGridViewComboBoxCell();
            comboAgent.Items.Add("1");
            comboAgent.Items.Add("스포");
            comboAgent.Items.Add("모다");
            comboAgent.Items.Add("아이원");
            dataGridViewSelectionDateDetail[5, 0] = comboAgent;

            //대리점 combo
            comboAgent.Value = "스포";
            //요금제변경일 user_plan_date user_t
            dataGridViewSelectionDateDetail[5, 1].Value = "";
            //예금주 bank_userName user_t
            dataGridViewSelectionDateDetail[5, 2].Value = "";
            //고객명? 없음
            dataGridViewSelectionDateDetail[5, 3].Value = null;
            //출고가
            dataGridViewSelectionDateDetail[5, 4].Value = "";
            //선납금
            dataGridViewSelectionDateDetail[5, 5].Value = "";
            //빈칸
            //정산금
            dataGridViewSelectionDateDetail[5, 7].Value = "";
            //현금
            dataGridViewSelectionDateDetail[5, 8].Value = "";
            //기타차감
            dataGridViewSelectionDateDetail[5, 9].Value = "";

            DataGridViewComboBoxCell comboStoreName = new DataGridViewComboBoxCell();
            comboStoreName.Items.Add("서울 강서");
            comboStoreName.Items.Add("원구 행주");
            comboStoreName.Items.Add("서울 마곡");
            comboStoreName.Items.Add("김포 공항");
            comboStoreName.Items.Add(st_table.St_name);

            dataGridViewSelectionDateDetail[7, 0] = comboStoreName;

            //매장 combo
            comboStoreName.Value = st_table.St_name;
            //빈칸
            //CIA? 없음
            dataGridViewSelectionDateDetail[7, 2].Value = null;
            //개통번호
            dataGridViewSelectionDateDetail[7, 3].Value = "";
            //빈칸
            //빈칸
            //정책추가
            dataGridViewSelectionDateDetail[7, 6].Value = "";
            //유심선납
            dataGridViewSelectionDateDetail[7, 7].Value = "";
            //카드
            dataGridViewSelectionDateDetail[7, 8].Value = "";
            //세후금액
            dataGridViewSelectionDateDetail[7, 9].Value = "";
            //판매자
            dataGridViewSelectionDateDetail[9, 0].Value = "";
            //빈칸
            //비고
            dataGridViewSelectionDateDetail[9, 2].Value = "";
            //약정유형
            dataGridViewSelectionDateDetail[9, 3].Value = "";
            //빈칸
            //MNP수수료
            dataGridViewSelectionDateDetail[9, 5].Value = "";
            //구두추가
            dataGridViewSelectionDateDetail[9, 6].Value = "";
            //부가서비스추가/차감
            dataGridViewSelectionDateDetail[9, 7].Value = "";
            //마진
            dataGridViewSelectionDateDetail[9, 9].Value = "";

            dataGridViewSelectionDateDetail.ReadOnly = false;

            //readonly 설정
            for (int i = 0; i < cellLabelArray.Length; i++)
            {
                dataGridViewSelectionDateDetail[2 * Convert.ToInt32(i / 10), i - 10 * Convert.ToInt32(i / 10)].ReadOnly = true;
            }

            if (needReadOnly)
            {
                dataGridViewSelectionDateDetail.ReadOnly = true;
            }

        }
        /// <summary>
        /// <para>기능 : 등록, 수정, 삭제시 테이블 리셋하기 위함</para>
        /// <para>진입 : </para>
        /// <para>분기 : </para>
        /// <para>로직 : 1. queryString과 Crud.ReadToGrid를 사용하여 검색된 개통일DataSet을 얻음</para>
        /// <para>로직 : 2. 컬럼명 한글로 변경</para>
        /// <para>로직 : 3. 체크박스 컬럼 추가</para>
        /// <para>로직 : 4. 필요한 컬럼 Readonly</para>
        /// <para>로직 : 5. 개통일 시간 제외 날짜까지만 출력</para>
        /// </summary>
        //등록, 수정, 삭제시 테이블 리셋하기 위함
        public void readResultTablePattern()
        {
            //추가 query 필요 : 매장, 판매자 ,가입자명, 전화번호
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

            dataGridViewSelectDate.Columns.Clear();

            tempFuncForWinform.setDataGridView(dataSetSelectDate, dataGridViewSelectDate);

            DataGridViewCheckBoxColumn datagridviewcheckBoxColumm = new DataGridViewCheckBoxColumn();
            datagridviewcheckBoxColumm.ValueType = typeof(bool);
            dataGridViewSelectDate.Columns.Insert(0, datagridviewcheckBoxColumm);
            dataGridViewSelectDate.ReadOnly = false;

            dataGridViewSelectDate.Columns[0].Width = 30;
            //check박스 제외 리드온리 설정, 전체를 리드온리하고 check박스만 풀경우 가 적용이 안됨
            for (int i = 1; i < 9; i++)
            {
                dataGridViewSelectDate.Columns[i].ReadOnly = true;
            }

            dataGridViewSelectDate.DefaultCellStyle.Format = "yyyy-MM-dd";

            setCheckedResult.Clear();

            dataGridViewSelectDate.ClearSelection();

        }
        /// <summary>
        /// <para>기능 : 버튼 텍스트 초기화</para>
        /// <para>진입 : </para>
        /// <para>분기 : </para>
        /// <para>로직 : 1. 등록 버튼 텍스트 &quot;등록&quot;으로 적용</para>
        /// <para>로직 : 2. 변경 버튼 텍스트 &quot;변경&quot;으로 적용</para>
        /// </summary>
        public void resetToDefault()
        {
            buttonCreateResult.Text = "등록";
            buttonUpdateResult.Text = "변경";
        }
        /// <summary>
        /// <para>기능 : 실적 상세 테이블 생성</para>
        /// <para>진입 : </para>
        /// <para>분기 : 1. needQuery == true</para>
        /// <para>로직 : 1. needQuery == true</para>
        /// <para>로직 : ----2. 선택된 개통번호 selectedUser_phone_number에 저장</para>
        /// <para>로직 : ----3. sqlQuery.SelectAllFrom_Where_와 Crud.Read를 사용하여 st_emp_result를 얻음</para>
        /// <para>로직 : ----4. sqlQuery.SelectAllFrom_Where_와 Crud.Read를 사용하여 user_t를 얻음</para>
        /// <para>로직 : ----5. sqlQuery.SelectAllFrom_Where_와 Crud.Read를 사용하여 st_table을 얻음</para>
        /// <para>로직 : ----6. 각 항목별 데이터 할당</para>
        /// <para>로직 : ----7. 각 항목 Readonly</para>
        /// <para>로직 : ----return</para>
        /// <para>로직 : 8. 빈 DataSet에 빈 DataTable 추가</para>
        /// <para>로직 : 9. tempFuncForWinform.setDataGridView</para>
        /// <para>로직 : 10. 기타 테이블 설정 적용</para>
        /// </summary>
        /// <param name="needQuery"></param>
        /// <param name="e"></param>
        public void makeDetailTable(bool needQuery, DataGridViewCellEventArgs e)
        {
            //true 시 만들어진 테이블에 데이터 할당, false시 빈 테이블 생성
            if (needQuery)
            {
                string userName = dataGridViewSelectDate.Rows[e.RowIndex].Cells["가입자명"].Value.ToString();
                string storeName = dataGridViewSelectDate.Rows[e.RowIndex].Cells["매장"].Value.ToString();
                string salerName = dataGridViewSelectDate.Rows[e.RowIndex].Cells["판매자"].Value.ToString();

                selectedUser_phone_number = dataGridViewSelectDate.Rows[e.RowIndex].Cells["개통번호"].Value.ToString();

                string querySelectResult = sqlQuery.SelectAllFrom_Where_("st_emp_result", new string[] { "user_open_number" }, new string[] { selectedUser_phone_number });
                St_emp_result st_emp_result = crud.Read_MySql(querySelectResult, new St_emp_result());

                string querySelectUser = sqlQuery.SelectAllFrom_Where_("user_t", new string[] { "user_code" }, new string[] { st_emp_result.user_code });
                User_t user_t = crud.Read_MySql(querySelectUser, new User_t());

                string querySelectStore = sqlQuery.SelectAllFrom_Where_("st_table", new string[] { "st_code" }, new string[] { employee.st_code });
                St_table st_table = crud.Read_MySql(querySelectStore, new St_table());

                //개통일
                dataGridViewSelectionDateDetail[1, 0].Value = st_emp_result.result_open_date.ToString("yyyy-MM-dd");

                //가입자명
                dataGridViewSelectionDateDetail[1, 1].Value = userName;
                //은행 user_bank user_t
                dataGridViewSelectionDateDetail[1, 2].Value = user_t.user_bank;
                //모델명
                dataGridViewSelectionDateDetail[1, 3].Value = st_emp_result.result_ph_m;

                DataGridViewComboBoxCell comboPreviousCom = new DataGridViewComboBoxCell();
                comboPreviousCom.Items.Add("1");
                comboPreviousCom.Items.Add("SKT");
                comboPreviousCom.Items.Add("KT");
                comboPreviousCom.Items.Add("LG");
                dataGridViewSelectionDateDetail[1, 4] = comboPreviousCom;

                //이전통신사 combo
                comboPreviousCom.Value = st_emp_result.user_previous_com;
                //공시지원금
                dataGridViewSelectionDateDetail[1, 5].Value = st_emp_result.subsidy_price;
                //리베이트/NET
                dataGridViewSelectionDateDetail[1, 6].Value = st_emp_result.net_rebate;
                //보험가입유무
                dataGridViewSelectionDateDetail[1, 7].Value = st_emp_result.insurance_YN;
                //송금금액
                dataGridViewSelectionDateDetail[1, 8].Value = st_emp_result.send_price;
                //현금파냄 금액
                dataGridViewSelectionDateDetail[1, 9].Value = st_emp_result.cash_sale_price;
                //모바일 개통시간
                dataGridViewSelectionDateDetail[3, 0].Value = st_emp_result.result_open_m_date.ToString("yyyy-MM-dd");
                //가입자 생년 user_birth user_t
                dataGridViewSelectionDateDetail[3, 1].Value = user_t.user_birth;
                //계좌번호 user_bank_number user_t
                dataGridViewSelectionDateDetail[3, 2].Value = user_t.user_bank_number;
                //일련번호
                dataGridViewSelectionDateDetail[3, 3].Value = st_emp_result.result_serial_number;
                //현/카/할
                dataGridViewSelectionDateDetail[3, 4].Value = st_emp_result.user_pur_type;
                //할부원금
                dataGridViewSelectionDateDetail[3, 5].Value = st_emp_result.installment_price;

                DataGridViewComboBoxCell comboPricePlan = new DataGridViewComboBoxCell();
                comboPricePlan.Items.Add("1");
                comboPricePlan.Items.Add("1만원");
                comboPricePlan.Items.Add("2만원");
                comboPricePlan.Items.Add("3만원");
                dataGridViewSelectionDateDetail[3, 6] = comboPricePlan;

                //요금제 combo
                comboPricePlan.Value = st_emp_result.price_plan;
                //유심후납
                dataGridViewSelectionDateDetail[3, 7].Value = st_emp_result.late_payment_usim;
                //예정일
                dataGridViewSelectionDateDetail[3, 8].Value = st_emp_result.due_date.ToString("yyyy-MM-dd");
                //카드판매금액
                dataGridViewSelectionDateDetail[3, 9].Value = st_emp_result.card_sale_price;

                DataGridViewComboBoxCell comboAgent = new DataGridViewComboBoxCell();
                comboAgent.Items.Add("1");
                comboAgent.Items.Add("스포");
                comboAgent.Items.Add("모다");
                comboAgent.Items.Add("아이원");
                dataGridViewSelectionDateDetail[5, 0] = comboAgent;

                //대리점 combo
                comboAgent.Value = st_emp_result.agent;
                //요금제변경일 user_plan_date user_t
                dataGridViewSelectionDateDetail[5, 1].Value = user_t.user_plan_date;
                //예금주 bank_userName user_t
                dataGridViewSelectionDateDetail[5, 2].Value = user_t.bank_userName;
                //고객명? 없음
                dataGridViewSelectionDateDetail[5, 3].Value = null;
                //출고가
                dataGridViewSelectionDateDetail[5, 4].Value = st_emp_result.release_price;
                //선납금
                dataGridViewSelectionDateDetail[5, 5].Value = st_emp_result.prepayments;
                //빈칸
                //정산금
                dataGridViewSelectionDateDetail[5, 7].Value = st_emp_result.user_calculate;
                //현금
                dataGridViewSelectionDateDetail[5, 8].Value = st_emp_result.cash;
                //기타차감
                dataGridViewSelectionDateDetail[5, 9].Value = st_emp_result.etc_offset;

                DataGridViewComboBoxCell comboStoreName = new DataGridViewComboBoxCell();
                comboStoreName.Items.Add("서울 강서");
                comboStoreName.Items.Add("원구 행주");
                comboStoreName.Items.Add("서울 마곡");
                comboStoreName.Items.Add("김포 공항");
                comboStoreName.Items.Add(st_table.St_name);
                dataGridViewSelectionDateDetail[7, 0] = comboStoreName;

                //매장 combo
                comboStoreName.Value = storeName;
                //빈칸
                //CIA? 없음
                dataGridViewSelectionDateDetail[7, 2].Value = null;
                //개통번호
                dataGridViewSelectionDateDetail[7, 3].Value = st_emp_result.user_open_number;
                //빈칸
                //빈칸
                //정책추가
                dataGridViewSelectionDateDetail[7, 6].Value = st_emp_result.add_policy;
                //유심선납
                dataGridViewSelectionDateDetail[7, 7].Value = st_emp_result.prepayments_usim;
                //카드
                dataGridViewSelectionDateDetail[7, 8].Value = st_emp_result.card;
                //세후금액
                dataGridViewSelectionDateDetail[7, 9].Value = st_emp_result.after_tax_price;
                //판매자
                dataGridViewSelectionDateDetail[9, 0].Value = salerName;
                //빈칸
                //비고
                dataGridViewSelectionDateDetail[9, 2].Value = st_emp_result.note;
                //약정유형
                dataGridViewSelectionDateDetail[9, 3].Value = st_emp_result.user_agree_type;
                //빈칸
                //MNP수수료
                dataGridViewSelectionDateDetail[9, 5].Value = st_emp_result.mnp_commission;
                //구두추가
                dataGridViewSelectionDateDetail[9, 6].Value = st_emp_result.sound_policy;
                //부가서비스추가/차감
                dataGridViewSelectionDateDetail[9, 7].Value = st_emp_result.addition_service;
                //마진
                dataGridViewSelectionDateDetail[9, 9].Value = st_emp_result.profit_margin;

                dataGridViewSelectionDateDetail.ReadOnly = true;
                return;
            }

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(new DataTable());
            tempFuncForWinform.setDataGridView(dataSet, dataGridViewSelectionDateDetail);
            dataGridViewSelectionDateDetail.DataSource = null;
            dataGridViewSelectionDateDetail.ColumnHeadersVisible = false;
            dataGridViewInit();
            dataGridViewSelectionDateDetail.ClearSelection();
            dataGridViewSelectionDateDetail.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridViewSelectionDateDetail.ReadOnly = true;

        }
        /// <summary>
        /// <para>기능 : 테이블 readonly 해제, label readonly 설정</para>
        /// <para>진입 : </para>
        /// <para>분기 : </para>
        /// <para>로직 : 1. 테이블 readonly = false</para>
        /// <para>로직 : 2. label readonly = true</para>
        /// </summary>
        public void changeDatagridViewDetailWritable()
        {
            dataGridViewSelectionDateDetail.ReadOnly = false;
            //라벨은 readonly
            for (int i = 0; i < cellLabelArray.Length; i++)
            {
                dataGridViewSelectionDateDetail[2 * Convert.ToInt32(i / 10), i - 10 * Convert.ToInt32(i / 10)].ReadOnly = true;
            }
        }
        /// <summary>
        /// <para>기능 : 유저 컨트롤이 안보이게 됐을 때 저장된 데이터 클리어</para>
        /// <para>진입 : </para>
        /// <para>분기 : </para>
        /// <para>로직 : 1. 개통번호 = null</para>
        /// <para>로직 : 2. 체크된 실적 클리어</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UserControl2_VisibleChanged(object sender, EventArgs e)
        {
            //저장된 전역변수 삭제
            selectedUser_phone_number = null;
            setCheckedResult.Clear();
        }
        /// <summary>
        /// <para>기능 : 실적이 아닌 셀 readonly</para>
        /// <para>진입 : </para>
        /// <para>분기 : </para>
        /// <para>로직 : 1. 실적이 아닌 셀.readonly = true</para>
        /// </summary>
        public void cellNotResultDisable()
        {
            //가입자명
            dataGridViewSelectionDateDetail[1, 1].ReadOnly = true;
            //은행
            dataGridViewSelectionDateDetail[1, 2].ReadOnly = true;
            //가입자생년
            dataGridViewSelectionDateDetail[3, 1].ReadOnly = true;
            //계좌번호
            dataGridViewSelectionDateDetail[3, 2].ReadOnly = true;
            //요금제 변경일
            dataGridViewSelectionDateDetail[5, 1].ReadOnly = true;
            //예금주
            dataGridViewSelectionDateDetail[5, 2].ReadOnly = true;
            //판매자
            dataGridViewSelectionDateDetail[9, 0].ReadOnly = true;
            //매장
            dataGridViewSelectionDateDetail[7, 0].ReadOnly = true;
            //CIA
            dataGridViewSelectionDateDetail[7, 2].ReadOnly = true;
        }
    }

}
