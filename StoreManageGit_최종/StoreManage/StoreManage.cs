
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace StoreManage
{
    public partial class StoreManage : Form
    {
        //자체 라이브러리
        Crud crud;
        SqlQuery sqlQuery;
        TempFuncForWinform tempFuncForWinform;

        // 전역변수시작
        //로그인시 전달받음, 로그인한 직원의 정보를 담음, 로드시 권한 확인, 실적관리(ResultManage)클릭시 매장명을 얻기위해 사용
        Employee employee;
        //매장클릭(store_cellclick)시 선택된 매장코드를 저장하기 위함, 매장데이터 변경시 st_code를 활용하여 데이터 불러오기 및 변경 적용, 지출데이터 및 지출상세데이터 불러오기시 사용
        string st_code;
        //지출테이블에서 해당 로우 클릭시 저장 , 지출 상세테이블을 불러오는 것과 예외처리를 위하여 사용
        string selectedMonth;
        //전역변수 끝
        
        //매장 삭제를 위하여 check된 매장 이름 저장
        SortedSet<string> setCheckedStoreName;

        TextBox[] textBoxArray;
        DateTimePicker[] dateTimePickerArray;
        Button[] menuButtonArray;
        Button[] buttonForCreateAndUpdateArray;
        Label[] labelArray;

        
        public StoreManage(Employee employee)
        {
            string connectionString = $"server={"192.168.0.78"};Database={"2kdigital"};Uid={"root"};Pwd={"rladudwo"};";
            //임시 db
            //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\2klab\OneDrive\2kd\Projects_c#_real\StoreManage_4\StoreManage\StoreManage\temp.mdf;Integrated Security=True;Connect Timeout=30";
            InitializeComponent();
            this.crud = new Crud(connectionString);
            this.sqlQuery = new SqlQuery();
            this.tempFuncForWinform = new TempFuncForWinform();

            this.textBoxArray = new TextBox[] {textBoxStroeName, textBoxAdminName,textBoxAddressZip, textBoxAddress, textBoxBasicInformation, textBoxContractInformation, textBoxAddress2 };
            this.dateTimePickerArray = new DateTimePicker[] {dateTimePickerContractDate, dateTimePickerContractDate2, dateTimePickerManagementExpense, dateTimePickerElectricityExpense, dateTimePickerWaterExpense };
            this.employee = employee;

            this.setCheckedStoreName = new SortedSet<string>();
            this.menuButtonArray = new Button[] { buttonEmployeeManageTemp, buttonStoreManageTemp, buttonResultManegeTemp };
            this.buttonForCreateAndUpdateArray = new Button[] {  buttonSelectAddress, buttonSelectEmployee };
            this.labelArray = new Label[]
            {
                labelCompanyName, labelCurrentDate, labelAdminName, labelStoreName, labelAdminName2, labelAddress,
                labelBasicInformation, labelContractInformation, labelContractTerm, labelElectricCost, labelEmployeeCount,
                labelEmployeeCount, labelManagementCostDate, labelManagementCostDateLeft, labelManagementCostDateLeft2,
                labelWaterCost
            };

        }

        /// <summary>
        /// <para>종류 : Form Load 메서드</para>
        /// <para>기능 : 각종 초기세팅</para>
        /// <para>로직 : 1. 좌상단 텍스트에 현재시간 할당</para>
        /// <para>       2. Login으로 부터 받은 employee객체를 활용하여 우상단 텍스트에 이름 및 권한 할당</para>
        /// <para>       3. 매장테이블에서 매장 및 관리자 컬럼을 가져오는 query를 통해 Crud.ReadToGrid를 활용하여 dataset을 얻고 좌측 테이블에 tempFuncForWinform.setDataGridView 메서드를 통해 할당</para>
        /// <para>       4. 테이블에 checkBoxColumn추가, readonly 설정, cellSelection제거, </para>
        /// <para>       5. dateTimePicker 현재시간으로 초기화</para>
        /// <para>       6. textBox 가운데 정렬 및 폰트 설정</para>
        /// <para>       7. 주소검색, 직원검색 버튼 비활성화</para>
        /// <para>       8. 라벨 및 dateTimePicker폰트 설정</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void StoreManage_Load(object sender, EventArgs e)
        {
            //1. 왼쪽 메뉴 버튼 동적 생성 후에 필요
            //List<string> menuList = getMenuList();
            //foreach (var m in menuList)
            //{
            //    makebutton(this, coord, dictButtonProperty);
            //}

            //2. Panel_Top_Left 현재시간 할당
            labelCurrentDate.Text = DateTime.Now.ToString("yyyy - MM - dd : tt hh : mm");

            //3. Panel_Top_Right 관리자 이름 할당
            if (employee.st_emp_level != 1)
            {
                labelAdminName.Text = employee.st_emp_member + " (관리자)";
            }
            else
            {
                labelAdminName.Text = employee.st_emp_member + " (비관리자)";
            }


            //4. Panel_Left_Second datatable 할당
            string queryReadStore = sqlQuery.select_From_("st_table", new string[] { "st_name", "st_manager" });
            DataSet dataSetStore = crud.ReadToGrid_MySql(queryReadStore);

            //컬럼이름 할당
            dataSetStore.Tables[0].Columns["st_name"].ColumnName = "매장명";
            dataSetStore.Tables[0].Columns["st_manager"].ColumnName = "관리자";

            tempFuncForWinform.setDataGridView(dataSetStore, dataGridViewStore);
            //check박스 컬럼 추가
            DataGridViewCheckBoxColumn dgvCmb = new DataGridViewCheckBoxColumn();
            dgvCmb.ValueType = typeof(bool);
            dgvCmb.Name = "";
            dataGridViewStore.Columns.Insert(0, dgvCmb);

            dataGridViewStore.ClearSelection();
            dataGridViewStore.Columns[0].Width = 30;
            dataGridViewStore.ReadOnly = false;
            dataGridViewStore.Columns[1].ReadOnly = true;
            dataGridViewStore.Columns[2].ReadOnly = true;


            // datepicker init
            dateTimePickerContractDate.Value = DateTime.Now;
            dateTimePickerContractDate2.Value = dateTimePickerContractDate.Value.AddYears(1);
            dateTimePickerManagementExpense.Value = DateTime.Now;
            dateTimePickerElectricityExpense.Value = DateTime.Now;
            dateTimePickerWaterExpense.Value = DateTime.Now;

            // 텍스트 가운데 정렬
            textBoxSetting();

            //로드시 readonly
            readonlyTextBoxAndDatetimePicker(true);
            //로드시 buttontogglle false
            buttonEnableToggle(false, buttonForCreateAndUpdateArray);
            //폰트설정
            labelSetting();
            //폰트설정
            dateTimePickerSetting();

        }

        /// <summary>
        /// <para>종류 :  버튼클릭 이벤트</para>
        /// <para>기능 :  매장 삭제</para>
        /// <para>진입 :  1. 삭제 체크된 매장List.Count() > 0</para>
        /// <para>분기 :  1. 메시지 박스 Yes클릭</para>
        /// <para>        2. queryResult > 0</para>
        /// <para>로직 :  1. 체크된 매장수 0보다 클 경우 진입</para>
        /// <para>            2. 메시지 박스에 삭제 체크된 매장이름을 띄우며 재확인 요청</para>
        /// <para>                3. 승인 시 매장List를 돌며 반복 삭제 진행</para>
        /// <para>                4. 해당 매장 이름을 이용하여 queryRead와 queryDelete를 만들고 Crud.Read를 통해 조회, 그결과를 바탕으로 Crud.Delete를 진행</para>
        /// <para>                    5. Crud.Delete반환 결과에 따라 성공시 매장이름 기록, 실패시 이전에 성공한 매장이름을 보여주며 실패 메시지박스 출력. 그 후 readStoreTablePattern메서드를 활용하여 테이블 새로고침 후 return</para>
        /// <para>                6. 실패가 없을시 삭제에 성공한 모든 매장이름을 메시지 박스로 출력</para>
        /// <para>                7. readStoreTablePattern메서드를 활용하여 테이블 새로고침</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonDeleteStore_Click(object sender, EventArgs e)
        {

            //체크 박스 추가
            //체크 된 사용자 이름 list에 담기
            //list를 돌면서 패턴 실행
            //패턴
            //list[i]를 통해 st_code를 얻음
            //query를 생성하고 crud,delete를 통해 삭제처리
            //결과에 따라 성공/실패 분기
            //
            string checkedNames = "";
            if (setCheckedStoreName.Count > 0)
            {
                //삭제 체크된 이름들 출력
                foreach (var v in setCheckedStoreName)
                {
                    checkedNames += v + " ";
                }

                if (MessageBox.Show(checkedNames + "선택", "주의", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string resultName = "";
                    foreach (var v in setCheckedStoreName)
                    {

                        string queryRead = sqlQuery.SelectAllFrom_Where_("st_table", new string[] { "st_name" }, new string[] { v });
                        St_table store = crud.Read_MySql(queryRead, new St_table());

                        string queryDelete = sqlQuery.deleteFrom_Where_("st_table", "st_code");
                        int queryResult = crud.Delete_MySql(queryDelete, store);

                        if (queryResult > 0)
                        {
                            resultName += v + " ";
                            continue;
                        }
                        MessageBox.Show($"({ resultName})삭제 성공, ({v})삭제 실패");
                        //매장목록 새로고침
                        readStoreTablePattern();

                        return;

                    }
                    MessageBox.Show(resultName + "삭제 성공");
                    //매장목록 새로고침
                    readStoreTablePattern();
                }
            }
            
            
            
        }
        /// <summary>
        /// <para>종류 :  버튼클릭 이벤트</para>
        /// <para>기능 :  매장 등록</para>
        /// <para>진입 :  1. 메시지 박스 Yes클릭</para>
        /// <para>분기 :  1. 등록버튼 텍스트 ==&quot;등록&quot;</para>
        /// <para>        2. queryResult > 0</para>
        /// <para>로직 :  1. 메시지박스 재확인 요청 </para>
        /// <para>            2. 승인시 저장된 st_code 비움</para>
        /// <para>            3. 버튼 텍스트가 &quot;등록&quot;분기</para>
        /// <para>                4.직원검색, 주소검색버튼 buttonEnableToggle메서드로 토글</para>
        /// <para>                5. 지출 테이블과 직원 테이블 데이터 비움</para>
        /// <para>                6. 텍스트 박스와 타임피커 쓰기로 readonlyTextBoxAndDatetimePicker메서드를 활용하여 바꿈</para>
        /// <para>                7. clearDatetimePickerAndTextBox메서드를 활용하여 텍스트 박스와 타임 피커 초기화</para>
        /// <para>                8. 버튼 텍스트 &quot;적용&quot;으로 변경 후 return</para>
        /// <para>            9. Rownum이 필요할시 queryRead와 Crud.read를 통해 얻음</para>
        /// <para>            10. store객체에 입력받은 값 모두 할당</para>
        /// <para>            11. queryCreate와 crud.insert를 통해 저장</para>
        /// <para>            12. queryResult > 0 분기</para>
        /// <para>                13. buttonEnableToggle메서드를 활용하여 버튼 비활성</para>
        /// <para>                14. 성공 메시지박스 출력</para>
        /// <para>                15. readStoreTablePattern활용하여 테이블 초기화</para>
        /// <para>                16. 버튼 텍스트 &quot;등록&quot;으로 변경</para>
        /// <para>                17. readonlyTextBoxAndDatetimePicker를 통해 텍스트 박스 및 타임피커 readonly</para>
        /// <para>                18. clearDatetimePickerAndTextBox를 통해 초기화</para>
        /// <para>            19. 실패 메시지 박스 출력</para>
        /// <para>            20. clearDatetimePickerAndTextBox로 초기화</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonAddStore_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("등록하시겟습니까?", "등록", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                st_code = null;
                if (buttonAddStore.Text == "등록")
                {
                    //직원검색, 주소검색 enable로 토글
                    buttonEnableToggle(true, buttonForCreateAndUpdateArray);

                    dataGridViewEmployee.DataSource = null;
                    dataGridViewExpense.DataSource = null;

                    readonlyTextBoxAndDatetimePicker(false);
                    clearDatetimePickerAndTextBox();

                    buttonAddStore.Text = "적용";
                    return;

                }
                string queryReadMaxStcode = sqlQuery.selectAllFrom_Where_Select_MINMAX_From_("st_table", "max", "st_code");
                St_table store = crud.Read_MySql(queryReadMaxStcode, new St_table());

                //등록할 데이터 할당
                store.St_code = (Convert.ToInt32(store.St_code) + 1).ToString();
                store.St_name = textBoxStroeName.Text;
                store.St_manager = textBoxAdminName.Text;
                store.St_zip = textBoxAddressZip.Text;
                store.St_addr1 = textBoxAddress.Text;
                store.St_addr2 = textBoxAddress2.Text;
                store.St_info1 = textBoxContractInformation.Text;
                store.St_info2 = textBoxBasicInformation.Text;
                store.St_contractdate = dateTimePickerContractDate.Value;
                store.St_contractdate2 = dateTimePickerContractDate2.Value;
                store.St_ele_date = dateTimePickerElectricityExpense.Value;
                store.St_maint_date = dateTimePickerManagementExpense.Value;
                store.St_sudo_date = dateTimePickerWaterExpense.Value;
                store.St_date = DateTime.Now;

                string queryCreate = sqlQuery.InsertInto__Values_(store);
                int result = crud.Create_MySql(queryCreate, store);

                if (result > 0)
                {
                    buttonEnableToggle(false, buttonForCreateAndUpdateArray);

                    MessageBox.Show("성공");

                    readStoreTablePattern();

                    buttonAddStore.Text = "등록";

                    readonlyTextBoxAndDatetimePicker(true);

                    clearDatetimePickerAndTextBox();

                    return;
                }
                MessageBox.Show("실패");
                clearDatetimePickerAndTextBox();
            }
            
            
        }
        /// <summary>
        /// <para>종류 :  버튼 클릭 이벤트</para>
        /// <para>기능 :  로그아웃</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. StoreManage Form 안보이게</para>
        /// <para>        2. Login Form 보이게</para>
        /// <para>        3. 저장된 데이터 (employee, st_code, seletedMonth, setCheckedStoreName) 삭제</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Login login = new Login();
            login.Show();
            //저장한 데이터가 있다면 지울 것
            employee = null;
            st_code = null;
            selectedMonth = null;
            setCheckedStoreName = null;
        }
        /// <summary>
        /// <para>기능 :  주소 검색을 위한 패널을 띄움</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  1. 버튼 텍스트 ==&quot;검색&quot;</para>
        /// <para>로직 :  1. if 버튼 텍스트 ==&quot;검색&quot;</para>
        /// <para>            2. 주소검색 패널 보임</para>
        /// <para>            3. 버튼 텍스트 &quot;닫기&quot;로 변경</para>
        /// <para>        4. 주소 검색 패널 안보이게</para>
        /// <para>        5. 버튼 텍스트 &quot;검색&quot;으로 변경</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonSelectAddress_Click(object sender, EventArgs e)
        {
            if (buttonSelectAddress.Text == "검색")
            {
                panelSelectAddress.Visible = true;
                buttonSelectAddress.Text = "닫기";
                return;
            }
            panelSelectAddress.Visible = false;
            buttonSelectAddress.Text = "검색";
            clearPanelSelectDoro();


        }
        /// <summary>
        /// <para>기능 :  자세히 버튼 클릭시 지출상세 패널 보이도록</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  1. 버튼텍스트 ==&quot;자세히&quot;  &amp; &amp; 선택월이 !=null</para>
        /// <para>로직 :  1. if 버튼텍스트 ==&quot;자세히&quot;  &amp; &amp; 선택월이 !=null</para>
        /// <para>            1. 상세 지출 패널 보이게</para>
        /// <para>            2. queryRead와 crud.readToGrid를 지출상세 dataset을 얻음</para>
        /// <para>            3. 컬럼이름 한글로 할당</para>
        /// <para>            4. tempFuncForWinform.setDataGridView활용하여 데이터 바인딩</para>
        /// <para>            5. 날자 컬럼 포맷형식 변경</para>
        /// <para>            6. 버튼 텍스트 &quot;닫기&quot;로 변경</para>
        /// <para>        7. 버튼 텍스트 &quot;자세히&quot;로 변경</para>
        /// <para>        8. 상세 지출 패널 안보이게</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonSelectExpenseDetail_Click(object sender, EventArgs e)
        {
            if (buttonSelectExpenseDetail.Text == "자세히" && selectedMonth != null)
            {
                panelExpenseDetail.Visible = true;

                string queryReadDetail = $"SELECT st_exp_date, st_exp_basic, st_exp_coupon, st_exp_ad, st_exp_acce, st_exp_quick, st_exp_cons, st_exp_arti FROM ST_EXPENSE WHERE ST_CODE = N'{st_code}' AND MONTH(ST_EXP_DATE) = {selectedMonth}";
                DataSet dataSetExpenseDetail = crud.ReadToGrid_MySql(queryReadDetail);

                //컬럼이름할당
                dataSetExpenseDetail.Tables[0].Columns["st_exp_date"].ColumnName = "날짜";
                dataSetExpenseDetail.Tables[0].Columns["st_exp_basic"].ColumnName = "일반";
                dataSetExpenseDetail.Tables[0].Columns["st_exp_coupon"].ColumnName = "쿠폰";
                dataSetExpenseDetail.Tables[0].Columns["st_exp_ad"].ColumnName = "광고비";
                dataSetExpenseDetail.Tables[0].Columns["st_exp_acce"].ColumnName = "액세사리";
                dataSetExpenseDetail.Tables[0].Columns["st_exp_quick"].ColumnName = "퀵";
                dataSetExpenseDetail.Tables[0].Columns["st_exp_cons"].ColumnName = "택배";
                dataSetExpenseDetail.Tables[0].Columns["st_exp_arti"].ColumnName = "용품판매";

                tempFuncForWinform.setDataGridView(dataSetExpenseDetail, dataGridViewExpenseDetail);

                dataGridViewExpenseDetail.Columns["날짜"].DefaultCellStyle.Format = "yy-MM-dd";
                dataGridViewExpenseDetail.Columns["날짜"].Width = 80;

                buttonSelectExpenseDetail.Text = "닫기";
                return;
            }
            buttonSelectExpenseDetail.Text = "자세히";
            panelExpenseDetail.Visible = false;
            
        }
        /// <summary>
        /// <para>기능 :  매장 업데이터</para>
        /// <para>진입 :  메시지 박스 yes클릭</para>
        /// <para>분기 :  1. 버튼 텍스트 == &quot;변경&quot;</para>
        /// <para>        2. queryResult >0</para>
        /// <para>로직 :  1. if 버튼 텍스트 == &quot;변경&quot;</para>
        /// <para>            2. readonlyTextBoxAndDatetimePicker</para>
        /// <para>            3. buttonEnableToggle</para>
        /// <para>            4. 버튼 텍스트 &quot;적용&quot;으로 변경</para>
        /// <para>            5. return</para>
        /// <para>        6. 매장 코드를 기준으로 queryRead &amp; crud.Read를 통해 st_table 객체 생성</para>
        /// <para>        7. st_table객체에 변경된 값 할당</para>
        /// <para>        8. 매장 코드를 기준 queryUpdate &amp; crud.Update 를 진행</para>
        /// <para>        9. if queryResult >0</para>
        /// <para>            10. buttonEnableToggle</para>
        /// <para>            11. readonlyTextBoxAndDatetimePicker</para>
        /// <para>            12. 성공 메시지박스</para>
        /// <para>            13. readStoreTablePattern</para>
        /// <para>            14. 버튼 텍스트 &quot;변경&quot;으로 변경</para>
        /// <para>        15. 메시지 박스 &quot;실패&quot;</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("변경하시겟습니까?", "변경", MessageBoxButtons.YesNo) == DialogResult.Yes && st_code!=null)
            {
                if (buttonUpdate.Text == "변경")
                {
                    // 검색시 항목 변경 가능 처리
                    readonlyTextBoxAndDatetimePicker(false);

                    buttonEnableToggle(true, buttonForCreateAndUpdateArray);

                    buttonUpdate.Text = "적용";
                    return;

                }
                string queryUpdate = sqlQuery.Update_Set_("st_table", new St_table(), "St_code");
                string queryRead = sqlQuery.SelectAllFrom_Where_("st_table", new string[] { "st_code" }, new string[] { st_code });
                St_table storeRead = crud.Read_MySql(queryRead, new St_table());

                St_table store = new St_table
                {
                    St_code = storeRead.St_code,
                    St_date = storeRead.St_date,
                    St_name = textBoxStroeName.Text,
                    St_manager = textBoxAdminName.Text,
                    St_zip = textBoxAddressZip.Text,
                    St_addr1 = textBoxAddress.Text,
                    St_addr2 = textBoxAddress2.Text,
                    St_info1 = textBoxContractInformation.Text,
                    St_info2 = textBoxBasicInformation.Text,
                    St_contractdate = dateTimePickerContractDate.Value,
                    St_contractdate2 = dateTimePickerContractDate2.Value,
                    St_ele_date = dateTimePickerElectricityExpense.Value,
                    St_maint_date = dateTimePickerManagementExpense.Value,
                    St_sudo_date = dateTimePickerWaterExpense.Value
                };

                int result = crud.Update_MySql(queryUpdate, store);

                if (result > 0)
                {
                    buttonEnableToggle(false, buttonForCreateAndUpdateArray);

                    readonlyTextBoxAndDatetimePicker(true);

                    MessageBox.Show("성공");

                    readStoreTablePattern();

                    buttonUpdate.Text = "변경";

                    return;
                }
                MessageBox.Show("실패");
            }
            
            
        }
        /// <summary>
        /// <para>기능 :  매장 테이블 셀클릭시 상세정보 조회</para>
        /// <para>진입 :  e.rowindex >=0 &amp;&amp; e.columnindex >=1</para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. resetToDefault</para>
        /// <para>        2. readonlyTextBoxAndDatetimePicker</para>
        /// <para>        3. buttonEnableToggle</para>
        /// <para>        4. 셀클릭시 row의 매장명 selectedStoreName에저장</para>
        /// <para>        5. selectedStoreName를 기준으로 queryRead&amp;crud.Read를 통해 st_table객체 생성</para>
        /// <para>        6. st_table정보를 각 테이블박스 및 타임피커에 할당</para>
        /// <para>        7. st_code에 st_table.st_code 할당</para>
        /// <para>        7. st_code를 기준으로 queryGroupby&amp;crud.ReadToGrid로 지출dataset얻음</para>
        /// <para>        8. dataset의 컬럼명을 한글로 변경</para>
        /// <para>        9. No컬럼 삽입 및 증식 방지 처리</para>
        /// <para>        10. tempFuncForWinform.setDataGridView</para>
        /// <para>        11. dataset.table[0].row.count()만큼 NO 순차 할당 및 readonly</para>
        /// <para>        12. st_code를 기준으로 queryRead&amp;crud.ReadToGrid로 직원dataset얻음</para>
        /// <para>        13. 직원수 row.count()로 파악</para>
        /// <para>        14. tempFuncForWinform.setDataGridView</para>
        /// <para>        15. calculateLeftDate</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dataGridViewStore_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 1)
            {
                resetToDefault();
                // 검색시 항목 변경 불가 처리
                readonlyTextBoxAndDatetimePicker(true);
                buttonEnableToggle(false, buttonForCreateAndUpdateArray);

                // 셀 클릭시 해당 row의 매장명 저장
                string selectedStoreName = ((sender as DataGridView).Rows[e.RowIndex].Cells["매장명"].Value.ToString());

                // 해당 매장명 row 클릭시 해당 매장명을 통해 데이터 조회 후 오른쪽에 데이터를 뿌림
                string queryReadStoreSelected = sqlQuery.SelectAllFrom_Where_("st_table", new string[] { "st_name" }, new string[] { selectedStoreName });
                St_table store = crud.Read_MySql(queryReadStoreSelected, new St_table());
                //할당
                st_code = store.St_code;
                textBoxStroeName.Text = store.St_name;
                textBoxAdminName.Text = store.St_manager;
                textBoxAddressZip.Text = store.St_zip;
                textBoxAddress.Text = store.St_addr1;
                textBoxAddress2.Text = store.St_addr2;
                textBoxContractInformation.Text = store.St_info1;
                textBoxBasicInformation.Text = store.St_info2;
                dateTimePickerContractDate.Value = store.St_contractdate;
                dateTimePickerContractDate2.Value = store.St_contractdate2;
                dateTimePickerManagementExpense.Value = store.St_maint_date;
                dateTimePickerElectricityExpense.Value = store.St_ele_date;
                dateTimePickerWaterExpense.Value = store.St_sudo_date;

                // Panel_Main_Third datatable할당
                string columnResult = "MONTH(st_exp_date) AS MONTH, SUM(ST_EXP_BASIC) AS ST_EXP_BASIC, SUM(ST_EXP_COUPON) AS ST_EXP_COUPON, SUM(ST_EXP_AD) AS ST_EXP_AD, SUM(ST_EXP_ACCE) AS ST_EXP_ACCE, SUM(ST_EXP_QUICK) AS ST_EXP_QUICK, SUM(ST_EXP_CONS) AS ST_EXP_CONS, SUM(ST_EXP_ARTI) AS ST_EXP_ARTI";
                string queryGroupBy = sqlQuery.select_Sum__From_Where_GroupBy_(columnResult, "st_expense", "st_code", st_code, "Month(st_exp_date)");
                DataSet dataSetExpense = crud.ReadToGrid_MySql(queryGroupBy);

                dataSetExpense.Tables[0].Columns["MONTH"].ColumnName = "월";
                dataSetExpense.Tables[0].Columns["st_exp_basic"].ColumnName = "일반";
                dataSetExpense.Tables[0].Columns["st_exp_coupon"].ColumnName = "쿠폰";
                dataSetExpense.Tables[0].Columns["st_exp_ad"].ColumnName = "광고비";
                dataSetExpense.Tables[0].Columns["st_exp_acce"].ColumnName = "액세사리";
                dataSetExpense.Tables[0].Columns["st_exp_quick"].ColumnName = "퀵";
                dataSetExpense.Tables[0].Columns["st_exp_cons"].ColumnName = "택배";
                dataSetExpense.Tables[0].Columns["st_exp_arti"].ColumnName = "용품판매";

                tempFuncForWinform.setDataGridView(dataSetExpense, dataGridViewExpense);

                // Panel_Main_Forth datatable 할당
                string queryReadEmployee = sqlQuery.select_From_Where("st_employee", new string[] { "st_emp_level", "st_emp_member", "st_emp_state", "st_emp_time", "st_emp_year", "st_emp_etc" }, "st_code", new string[] { store.St_code });
                DataSet dataSetEmployee = crud.ReadToGrid_MySql(queryReadEmployee);

                dataSetEmployee.Tables[0].Columns["st_emp_level"].ColumnName = "직급";
                dataSetEmployee.Tables[0].Columns["st_emp_member"].ColumnName = "이름";
                dataSetEmployee.Tables[0].Columns["st_emp_state"].ColumnName = "상태";
                dataSetEmployee.Tables[0].Columns["st_emp_time"].ColumnName = "근무시간";
                dataSetEmployee.Tables[0].Columns["st_emp_year"].ColumnName = "년차";
                dataSetEmployee.Tables[0].Columns["st_emp_etc"].ColumnName = "비고";

                //No컬럼이 증식되는것 방지
                dataGridViewEmployee.Columns.Clear();

                DataColumn dataColumn = new DataColumn();
                dataColumn.ColumnName = "No";
                dataSetEmployee.Tables[0].Columns.Add(dataColumn);
                dataColumn.SetOrdinal(0);

                tempFuncForWinform.setDataGridView(dataSetEmployee, dataGridViewEmployee);

                //NO 순차 할당
                for (int i = 0; i < dataSetEmployee.Tables[0].Rows.Count; i++)
                {
                    dataGridViewEmployee.Rows[i].Cells[0].Value = i + 1;
                };

                dataColumn.ReadOnly = true;

                //Panel_Select_Employee 할당

                string queryReadEmployeePanel = sqlQuery.select_From_Where("st_employee", new string[] { "st_emp_level", "st_emp_member"}, "st_code", new string[] { store.St_code });
                DataSet dataSetEmployeePanel = crud.ReadToGrid_MySql(queryReadEmployeePanel);


                dataSetEmployeePanel.Tables[0].Columns["st_emp_level"].ColumnName = "직급";
                dataSetEmployeePanel.Tables[0].Columns["st_emp_member"].ColumnName = "이름";

                labelEmployeeCount.Text = $"매장직원 : {dataSetEmployee.Tables[0].Rows.Count} 명";

                //아마 불필요
                //dataGridViewSelectEmployee.Columns.Clear();

                tempFuncForWinform.setDataGridView(dataSetEmployeePanel, dataGridViewSelectEmployee);

                //관리비 남은 날짜 표시
                calculateLeftDate();
            }

        }
        /// <summary>
        /// <para>기능 :  직원조회 패널 보이게</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  1. 버튼 텍스트 ==&quot;직원열기&quot;</para>
        /// <para>로직 :  1. if 버튼텍스트 == &quot;직원열기&quot;</para>
        /// <para>            2. 직원 선택  패널 보이게</para>
        /// <para>            3. 버튼 텍스트 를 &quot;직원닫기&quot;로 변경</para>
        /// <para>        4. 직원 선택  패널 안보이게</para>
        /// <para>        5. 버튼 텍스트 를 &quot;직원열기&quot;로 변경</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonSelectEmployee_Click(object sender, EventArgs e)
        {
            if (buttonSelectEmployee.Text == "직원열기")
            {
                panelSelectEmployee.Visible = true;
                buttonSelectEmployee.Text = "직원닫기";
                return;
            }
            panelSelectEmployee.Visible = false;
            buttonSelectEmployee.Text = "직원열기";

        }
        /// <summary>
        /// <para>기능 :  직원관리 패널 보이게</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. panel탑의 중간 매장이름 가리기</para>
        /// <para>        2. 직원관리 패널 보이게</para>
        /// <para>        3. 다른 패널 안보이게</para>
        /// <para>        4. buttonColorChange메소드를 통해 직원관리 버튼은 회색 그외에는 흰색</para>
        /// <para>        5. 유저컨트롤을 직원관리 패널에 add</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonEmployeeManageTemp_Click(object sender, EventArgs e)
        {
            //panel탑의 중간 매장이름 가리기
            labelStroeNameVisibleAfterClickResult.Visible = false;

            panelEmployeeManage.Visible = true;
            panelEmployeeManage.BringToFront();
            panelResultManage.Visible = false;

            //클릭한 버튼만 회색으로
            buttonColorChange(sender);

            UserControl1 userControl1= new UserControl1();
            userControl1.Dock = DockStyle.Fill;

            panelEmployeeManage.Controls.Clear();

            panelEmployeeManage.Controls.Add(userControl1);
        }
        /// <summary>
        /// <para>기능 :  매장관리 화면으로 이동</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. panel탑의 중간 매장이름 가리기</para>
        /// <para>        2. 다른 패널 안보이게</para>
        /// <para>        3. buttonColorChange</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonStoreManageTemp_Click(object sender, EventArgs e)
        {
            //panel탑의 중간 매장이름 가리기
            labelStroeNameVisibleAfterClickResult.Visible = false;

            panelEmployeeManage.Visible = false;
            panelResultManage.Visible = false;

            buttonColorChange(sender);
        }
        /// <summary>
        /// <para>기능 :  직원이름 클릭시 텍스트박스에 적용</para>
        /// <para>진입 :  e.RowIndex >= 0 &amp;&amp; e.ColumnIndex >= 0</para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. 클릭한 테이블의 셀의 &quot;이름&quot;을 관리자 이름 텍스트 박스에 적용</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dataGridViewSelectEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                textBoxAdminName.Text = dataGridViewSelectEmployee.Rows[e.RowIndex].Cells["이름"].Value.ToString();
            }
        }
        /// <summary>
        /// <para>기능 :  지출 테이블에서 선택된 월 저장</para>
        /// <para>진입 :  e.RowIndex >= 0 &amp;&amp; e.ColumnIndex >= 0</para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. 선택된 테이블의 &quot;월&quot;을 selectedMonth에 저장</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dataGridViewExpense_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                selectedMonth = dataGridViewExpense.Rows[e.RowIndex].Cells["월"].Value.ToString();
            }
        }
        /// <summary>
        /// <para>기능 :  매장테이블의 check박스 체크 상태 저장</para>
        /// <para>진입 :  e.RowIndex >= 0 &amp;&amp; e.ColumnIndex >=0 &amp;&amp; (sender as DataGridView).CurrentCell is DataGridViewCheckBoxCell)</para>
        /// <para>분기 :  1. 테이블 에서 선택된 체크박스 값이 변했고 그값 == ture</para>
        /// <para>로직 :  1. if 테이블 에서 선택된 체크박스 값이 변했고 그값 == ture</para>
        /// <para>            2. 선택된 행의 &quot;매장명&quot; setCheckedStroeName에 추가</para>
        /// <para>        3. 선택된 행의 &quot;매장명&quot; setCheckedStroeName에 삭제</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dataGridViewStore_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //체크가 true인지
            if (e.RowIndex >= 0&& e.ColumnIndex >=0 && (sender as DataGridView).CurrentCell is DataGridViewCheckBoxCell)

            {
                if ((bool)(dataGridViewStore.Rows[e.RowIndex].Cells[0].EditedFormattedValue ?? false) == true)
                {
                    setCheckedStoreName.Add((string)dataGridViewStore.Rows[e.RowIndex].Cells["매장명"].Value);
                    return;
                }
                setCheckedStoreName.Remove((string)dataGridViewStore.Rows[e.RowIndex].Cells["매장명"].Value);
            }
            
        }
        /// <summary>
        /// <para>기능 :  텍스트 박스와 타임피커 색 및 readonly 변경</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  1. isReadonly == true</para>
        /// <para>로직 :  1. if isReadonly == true</para>
        /// <para>            2. 텍스트 박스 읽기전용, 회색배경</para>
        /// <para>            3. 타임피커 비활성화, 회색배경</para>
        /// <para>        4. 텍스트 박스 읽기전용 해제, 흰색배경</para>
        /// <para>        5. 타임피커 활성화, 흰색배경</para>
        /// </summary>
        /// <param name="isReadonly"></param>
        public void readonlyTextBoxAndDatetimePicker(bool isReadonly)
        {
            if (isReadonly)
            {
                foreach (var v in textBoxArray)
                {
                    
                    v.ReadOnly = true;
                    v.BackColor = Color.FromArgb(230, 230, 230);
                }
                foreach (var d in dateTimePickerArray)
                {
                    d.Enabled = false;
                    d.BackColor = Color.FromArgb(230, 230, 230);
                }
                return;
            }

            foreach (var v in textBoxArray)
            {
                v.ReadOnly = false;
                v.BackColor = Color.FromArgb(255, 255, 255);
            }
            foreach (var d in dateTimePickerArray)
            {
                d.Enabled = true;
                d.BackColor = Color.FromArgb(255, 255, 255);
            }
            
        }
        /// <summary>
        /// <para>기능 :  매장 테이블 불러오기 및 readonly 적용</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. queryRead&amp;Crud.Read 를 통해 매장 DataSet 얻음</para>
        /// <para>        2. dataset의 컬럼명 한글로 변경</para>
        /// <para>        3. tempFuncForWinform.setDataGridView</para>
        /// <para>        4. check박스 제외 하고 읽기 전용 설정</para>
        /// <para>        5. setCheckedStoreName 클리어</para>
        /// </summary>
        public void readStoreTablePattern()
        {
            string queryReadStore = sqlQuery.select_From_("st_table", new string[] { "st_name", "st_manager" });
            DataSet dataSetStore = crud.ReadToGrid_MySql(queryReadStore);

            dataSetStore.Tables[0].Columns["st_name"].ColumnName = "매장명";
            dataSetStore.Tables[0].Columns["st_manager"].ColumnName = "관리자";

            tempFuncForWinform.setDataGridView(dataSetStore, dataGridViewStore);

            dataGridViewStore.ReadOnly = false;
            dataGridViewStore.Columns[1].ReadOnly = true;
            dataGridViewStore.Columns[2].ReadOnly = true;

            setCheckedStoreName.Clear();

        }
        /// <summary>
        /// <para>기능 :  타임피커 및 텍스트 박스 초기화</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. 타임 피커배열을 돌며 현재 날짜 할당</para>
        /// <para>        2. 텍스트박스 배열을 돌며 &quot;&quot;할당</para>
        /// </summary>
        public void clearDatetimePickerAndTextBox()
        {
            foreach ( var v in dateTimePickerArray)
            {
                v.Value = DateTime.Now;
            }

            foreach ( var v in textBoxArray)
            {
                v.Text = "";
            }
            
            return;
        }
        /// <summary>
        /// <para>기능 :  텍스트 박스 가운데정렬 및 폰트설정</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. 텍스트 가운데 정렬</para> 
        /// <para>        2. 폰트 나눔고딕 설정</para>
        /// </summary>
        public void textBoxSetting()
        {
            foreach (var b in textBoxArray)
            {
                b.TextAlign = HorizontalAlignment.Center;
                b.Font = new Font("나눔고딕", 10, FontStyle.Regular);
            }
        }
        /// <summary>
        /// <para>기능 :  라벨 폰트 설정</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. 폰트 나눔고딕으로 설정</para>
        /// </summary>
        public void labelSetting()
        {
            foreach (var v in labelArray)
            {
                v.Font = new Font("나눔고딕", 10, FontStyle.Regular);
            }
        }
        /// <summary>
        /// <para>기능 :  타임피커 폰트 설정</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. 타임피커 폰트 나눔고딕</para>
        /// </summary>
        public void dateTimePickerSetting()
        {
            foreach (var v in dateTimePickerArray)
            {
                v.Font = new Font("나눔고딕", 10, FontStyle.Regular);
            }
        }
        /// <summary>
        /// <para>기능 :  버튼 배열 활성 비활성 토글</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  1. isSelectable == true</para>
        /// <para>로직 :  1. if isSelectable == true</para>
        /// <para>            2. 버튼 배열 돌며 버튼 활성화</para>
        /// <para>        3. 버튼배열 돌며 버튼 비활성화</para>
        /// </summary>
        /// <remarks>지금은 직원열기, 주소검색 버튼에만 쓰임</remarks>
        /// <param name="isSelectable"></param>
        /// <param name="buttonArray"></param>
        public void buttonEnableToggle(bool isSelectable, Button[] buttonArray)
        {
            if (isSelectable)
            {
                foreach (var b in buttonArray)
                {
                    b.Enabled = true;
                }
                return;
            }

            foreach (var b in buttonArray)
            {
                b.Enabled = false;
            }


        }
        /// <summary>
        /// <para>기능 :  실적관리 페이지로 이동</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. 매장 코드를 바탕으로 queryRead&amp;Crud.Read 로 st_table 객체 얻음</para>
        /// <para>        2. st_table.st_name을 상단 중앙 라벨에 할당하고 보이도록 설정</para>
        /// <para>        3. 실적패널 보이고 다른패널 안보이게</para>
        /// <para>        4. buttonColorChange</para>
        /// <para>        5. 유저컨트롤2(실적관리) 실적관리 콘트롤에 더함</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonResultManegeTemp_Click(object sender, EventArgs e)
        {
            string queryReadSt_table = sqlQuery.SelectAllFrom_Where_("st_table",new string[] {"st_code"}, new string[] {employee.st_code });
            St_table st_table = crud.Read_MySql(queryReadSt_table, new St_table());
            //매장명 세팅 및 보이기
            labelStroeNameVisibleAfterClickResult.Text = st_table.St_name;
            labelStroeNameVisibleAfterClickResult.Visible = true;
            

            panelResultManage.Visible = true;
            panelResultManage.BringToFront();
            panelEmployeeManage.Visible = false;

            buttonColorChange(sender);

            UserControl2 userControl2 = new UserControl2(employee, sqlQuery, crud, tempFuncForWinform);
            userControl2.Dock = DockStyle.Fill;

            panelResultManage.Controls.Clear();

            panelResultManage.Controls.Add(userControl2);

        }

        /// <summary>
        /// <para>기능 :  주소검색</para>
        /// <para>진입 :  textBoxSelectDoro.Text != &quot;&quot;</para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. 입력된 텍스트를 바탕으로 QueryRead&amp;Crud.readToGrid를 통해 시도, 시군구, 도로명, 우편번호의 DataSet 얻음</para>
        /// <para>        2. 컬럼을 한글로 변경</para>
        /// <para>        3. tempFuncForWinform.setDataGridView</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonSelectDoro_Click(object sender, EventArgs e)
        {
            if (textBoxSelectDoro.Text != "")
            {
                string querySelectDoro = $"SELECT SIDO, SIGUNGU, DORO, ZIP_NO FROM ZIPCODE WHERE DORO LIKE '%{textBoxSelectDoro.Text}%'";
                DataSet dataSetDoro = crud.ReadToGrid_MySql(querySelectDoro);

                dataSetDoro.Tables[0].Columns["SIDO"].ColumnName = "시도";
                dataSetDoro.Tables[0].Columns["SIGUNGU"].ColumnName = "시군구";
                dataSetDoro.Tables[0].Columns["DORO"].ColumnName = "도로명";
                dataSetDoro.Tables[0].Columns["ZIP_NO"].ColumnName = "우편번호";

                tempFuncForWinform.setDataGridView(dataSetDoro, dataGridViewSelectDoro);
            }
            
        }
        /// <summary>
        /// <para>기능 :  검색된 주소 클릭시 텍스트 박스에 할당</para>
        /// <para>진입 :  e.RowIndex >= 0 &amp;&amp; e.ColumnIndex >= 0</para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. 클릭된 행의 &quot;우편번호&quot;, &quot;시도&quot;, &quot;시군구&quot;, &quot;도로명&quot; 할당</para>
        /// <para>        2. clearPanelSelectDoro</para>
        /// <para>        3. 버튼 텍스트 &quot;검색&quot;으로 변경</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dataGridViewSelectDoro_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridView datagridview = (sender as DataGridView);

                textBoxAddressZip.Text = datagridview.Rows[e.RowIndex].Cells["우편번호"].Value.ToString();
                textBoxAddress.Text = datagridview.Rows[e.RowIndex].Cells["시도"].Value.ToString() +
                    datagridview.Rows[e.RowIndex].Cells["시군구"].Value.ToString() +
                    datagridview.Rows[e.RowIndex].Cells["도로명"].Value.ToString();

                clearPanelSelectDoro();
                buttonSelectAddress.Text = "검색";
            }
        }
        /// <summary>
        /// <para>기능 :  주소 검색 패널 비우고 안보이게 함</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. 검색된 주소 테이블 비우기</para>
        /// <para>        2. 주소 패널 안보이게</para>
        /// <para>        3. 도로 검색 텍스트 박스 초기화</para>
        /// </summary>
        public void clearPanelSelectDoro()
        {
            dataGridViewSelectDoro.DataSource = null;
            panelSelectAddress.Visible = false;
            textBoxSelectDoro.Text = "";
        }
        /// <summary>
        /// <para>기능 :  각종 버튼 및 패널 기본값으로 변경</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. 4종 버튼 값 초기화</para>
        /// <para>        2. 주소, 직원 패널 안보이게</para>
        /// <para>        3. clearPanelSelectDoro</para>
        /// </summary>
        public void resetToDefault()
        {
            //각종 컨트롤 기본값으로
            buttonAddStore.Text = "등록";
            buttonUpdate.Text = "변경";
            buttonSelectAddress.Text = "검색";
            buttonSelectEmployee.Text = "직원열기";
            panelSelectEmployee.Visible = false;
            panelSelectAddress.Visible = false;
            clearPanelSelectDoro();
        }
        /// <summary>
        /// <para>기능 :  매장 텍스트 박스에 외곽선색을 넣기위한 paint이벤트</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. drawTextBoxBorder</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void panelStoreName_Paint(object sender, PaintEventArgs e)
        {
            drawTextBoxBorder(sender, e, textBoxStroeName);
        }
        /// <summary>
        /// <para>기능 :  관리자 텍스트 박스에 외곽선색을 넣기위한 paint이벤트</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  drawTextBoxBorder</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void panelAdminName_Paint(object sender, PaintEventArgs e)
        {
            drawTextBoxBorder(sender, e, textBoxAdminName);
        }
        /// <summary>
        /// <para>기능 :  관리비 날짜와 현재날짜를 통해 남은 날짜 계산</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  1. 현재 달 == 관리바 달</para>
        /// <para>        2. 현재 일 == 관리비 일</para>
        /// <para>        3. 현재 일 &lt; 관리비 일</para>
        /// <para>        </para>
        /// <para>로직 :  1. if 현재 달 == 관리바 달</para>
        /// <para>            2. if 현재 일 == 관리비 일</para>
        /// <para>                3. 남은 날은 0</para>
        /// <para>            4. if 현재 일 &lt; 관리비 일</para>
        /// <para>                5. 남은날은 관리비 일 - 현재 일</para>
        /// <para>            6. 남은날은 현재달의 날짜 - 현재 날짜 + 지난 달의 날짜</para>
        /// <para>        7. 남은날은 (현재달의 날짜 - 현재 날짜 + 지난 달의 날짜)%30</para>
        /// </summary>
        public void calculateLeftDate()
        {
            if (DateTime.Now.Month == dateTimePickerManagementExpense.Value.Month)
            {
                if (DateTime.Now.Day == dateTimePickerManagementExpense.Value.Day)
                {
                    labelManagementCostDateLeft.Text = "0";
                    return;
                }
                if (DateTime.Now.Day < dateTimePickerManagementExpense.Value.Day)
                {
                    labelManagementCostDateLeft.Text = (dateTimePickerManagementExpense.Value.Day - DateTime.Now.Day).ToString();
                    return;
                }
                //현재달의 날짜 - 현재 날짜 + 지난 달의 날짜
                labelManagementCostDateLeft.Text = (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - DateTime.Now.Day + dateTimePickerManagementExpense.Value.Day).ToString();
                return;
            }

            labelManagementCostDateLeft.Text = ((DateTime.DaysInMonth(dateTimePickerManagementExpense.Value.Year, dateTimePickerManagementExpense.Value.Month) - DateTime.Now.Day + dateTimePickerManagementExpense.Value.Day) % 30).ToString();
        }
        /// <summary>
        /// <para>기능 :  관리비 날짜 변경시 남은 날짜 변경</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. calculateLeftDate</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dateTimePickerManagementExpense_ValueChanged(object sender, EventArgs e)
        {
            calculateLeftDate();
        }
        /// <summary>
        /// <para>기능 :  버튼 색 변경</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. 버튼 배열 돌며 배경 흰색</para>
        /// <para>로직 :  2. 선택된 버튼은 회색</para>
        /// </summary>
        /// <remarks>현재 왼쪽에 매뉴 버튼 에만 적용</remarks>
        /// <param name="sender"></param>
        public void buttonColorChange(object sender)
        {
            foreach (var v in menuButtonArray)
            {
                v.BackColor = Color.White;
            }
            (sender as Button).BackColor = Color.FromArgb(230, 230, 230);
        }
        /// <summary>
        /// <para>기능 :  텍스트 박스 외곽선 색주기</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. 매개변수sender를 패널로 캐스트</para>
        /// <para>로직 :  2. 텍스트 박스의 외곽선은 없음으로</para>
        /// <para>로직 :  3. 매개변수 e.Graphics의 DrawRectangle(pen, rectangle)를 통해 텍스트 박스 외곽선 그림(좌표는 텍스트박스 꼭지점 +_@)</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="textbox"></param>
        public void drawTextBoxBorder(object sender, PaintEventArgs e, TextBox textbox)
        {
            Panel panel = sender as Panel;
            textbox.BorderStyle = BorderStyle.None;
            var g = e.Graphics;
            g.DrawRectangle(Pens.Blue, new Rectangle(textbox.Location.X - 3, textbox.Location.Y - 3, textbox.Width + 3, textbox.Height + 3));
        }
    }
    
}
