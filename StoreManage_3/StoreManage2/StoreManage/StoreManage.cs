
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace StoreManage
{
    public partial class StoreManage : Form
    {
        Crud crud;
        SqlQuery sqlQuery;
        TempFuncForWinform tempFuncForWinform;

        string id;
        string st_code;
        string selectedMonth;
        SortedSet<string> setCheckedStoreName;
        TextBox[] textBoxArray;
        DateTimePicker[] dateTimePickerArray;
        Button[] menuButtonArray;
        Button[] buttonForCreateAndUpdateArray;
        Label[] labelArray;

        //
        public StoreManage(string id)
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
            this.id = id;

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

        private void StoreManage_Load(object sender, EventArgs e)
        {
            ////1. 왼쪽 메뉴 버튼 동적 생성
            //List<string> menuList = getMenuList();
            //foreach (var m in menuList)
            //{
            //    makebutton(this, coord, dictButtonProperty);
            //}

            //2. Panel_Top_Left 현재시간 할당
            labelCurrentDate.Text = DateTime.Now.ToString("yyyy - MM - dd : tt hh : mm");

            //3. Panel_Top_Right 관리자 이름 할당
            string queryReadEmp = sqlQuery.SelectAllFrom_Where_("st_employee", new string[] { "st_emp_code" }, new string[] {id});
            Employee emp = crud.Read_MySql(queryReadEmp, new Employee());
            if (emp.st_emp_level == 1)
            {
                labelAdminName.Text = emp.st_emp_member + " (관리자)";
            }
            else
            {
                labelAdminName.Text = emp.st_emp_member + " (비관리자)";
            }


            //4. Panel_Left_Second datatable 할당
            //string queryReadStore = sqlQuery.select_From_Where("st_table", new string[] { "st_name", "st_manager"},"st_manager", new string[] {id});
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


            //5. panel_Main_First 매장명, 관리자명 할당 및 textbox border color 변환
            //border color 어디?

            //6. Panel_Main_Second_1 textbox border color 변환

            //7. Panel_Main_Second_4 가능하다면 db로부터 날짜를 할당, LeftDate 변경

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
            //로드시 enable
            buttonEnableToggle(false, buttonForCreateAndUpdateArray);
            //폰트설정
            labelSetting();
            dateTimePickerSetting();





        }

        private void buttonDeleteStore_Click(object sender, EventArgs e)
        {

            //체크 박스 추가
            //체크 된 사용자 이름 list에 담기
            //list를 돌면서 패턴 실행
            //패턴
            ////list[i]를 통해 st_code를 얻음
            //// query를 생성하고 crud,delete를 통해 삭제처리
            ///결과에 따라 성공/실패 분기
            ///
            string checkedNames = "";
            if (setCheckedStoreName.Count > 0)
            {
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
                        }
                        else
                        {
                            Console.WriteLine("실패");
                            MessageBox.Show($"({ resultName})삭제 성공, ({v})삭제 실패");
                            readStoreTablePattern();

                            return;
                        }

                    }
                    MessageBox.Show(resultName + "삭제 성공");
                    readStoreTablePattern();
                }
            }
            
            
            
        }

        //추가 처리 필요
        private void buttonAddStore_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("등록하시겟습니까?", "등록", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (buttonAddStore.Text == "등록")
                {
                    buttonEnableToggle(true, buttonForCreateAndUpdateArray);
                    dataGridViewEmployee.DataSource = null;
                    dataGridViewExpense.DataSource = null;
                    readonlyTextBoxAndDatetimePicker(false);
                    clearDatetimePickerAndTextBox();
                    buttonAddStore.Text = "적용";
                    return;

                }
                if (buttonAddStore.Text == "적용" )
                {
                    string queryReadMaxStcode = sqlQuery.selectAllFrom_Where_Select_MINMAX_From_("st_table", "max", "st_code");
                    St_table store = crud.Read_MySql(queryReadMaxStcode, new St_table());
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
            
            
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Login login = new Login();
            login.Show();
            //저장한 데이터가 있다면 지울 것
            id = "";
        }

        private void buttonSelectAddress_Click(object sender, EventArgs e)
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

        private void buttonSelectExpenseDetail_Click(object sender, EventArgs e)
        {
            if (buttonSelectExpenseDetail.Text == "자세히" && selectedMonth != null)
            {
                panelExpenseDetail.Visible = true;
                string queryReadDetail = $"SELECT st_exp_date, st_exp_basic, st_exp_coupon, st_exp_ad, st_exp_acce, st_exp_quick, st_exp_cons, st_exp_arti FROM ST_EXPENSE WHERE ST_CODE = N'{st_code}' AND MONTH(ST_EXP_DATE) = {selectedMonth}";
                DataSet dataSetEmployeeDetail = crud.ReadToGrid_MySql(queryReadDetail);

                dataSetEmployeeDetail.Tables[0].Columns["st_exp_date"].ColumnName = "날짜";
                dataSetEmployeeDetail.Tables[0].Columns["st_exp_basic"].ColumnName = "일반";
                dataSetEmployeeDetail.Tables[0].Columns["st_exp_coupon"].ColumnName = "쿠폰";
                dataSetEmployeeDetail.Tables[0].Columns["st_exp_ad"].ColumnName = "광고비";
                dataSetEmployeeDetail.Tables[0].Columns["st_exp_acce"].ColumnName = "액세사리";
                dataSetEmployeeDetail.Tables[0].Columns["st_exp_quick"].ColumnName = "퀵";
                dataSetEmployeeDetail.Tables[0].Columns["st_exp_cons"].ColumnName = "택배";
                dataSetEmployeeDetail.Tables[0].Columns["st_exp_arti"].ColumnName = "용품판매";

                tempFuncForWinform.setDataGridView(dataSetEmployeeDetail, dataGridViewExpenseDetail);
                dataGridViewExpenseDetail.Columns["날짜"].DefaultCellStyle.Format = "yy-MM-dd";
                dataGridViewExpenseDetail.Columns["날짜"].Width = 80;
                buttonSelectExpenseDetail.Text = "닫기";
                return;
            }
            if (buttonSelectExpenseDetail.Text == "닫기")
            {
                buttonSelectExpenseDetail.Text = "자세히";
                panelExpenseDetail.Visible = false;
            }
            //자세히를 버튼으로 할 것인지?
            //셀 클릭시 row, column를 통해 month를 저장해두고 클릭시 select

            // 아니면 월 셀 왼쪽에 +를 두고 열었다 닫았다 할 수 있게 할 것인지?
            // 테이블 확장 추가 공부 필요
            
            
        }

        private void buttonManagementCostDateCalendar_Click(object sender, EventArgs e)
        {
            // 날짜 클릭시 남은기간 계산하는 이벤트 필요
        }


        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("변경하시겟습니까?", "변경", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (buttonUpdate.Text == "변경")
                {
                    // 검색시 항목 변경 가능 처리
                    readonlyTextBoxAndDatetimePicker(false);
                    buttonEnableToggle(true, buttonForCreateAndUpdateArray);

                    buttonUpdate.Text = "적용";
                    return;

                }
                if (buttonUpdate.Text == "적용")
                {
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
            
            
        }

        private void dataGridViewStore_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            resetToDefault();
            
            if (e.RowIndex >= 0 && e.ColumnIndex >= 1)
            {
                // 검색시 항목 변경 불가 처리
                readonlyTextBoxAndDatetimePicker(true);
                buttonEnableToggle(false, buttonForCreateAndUpdateArray);

                // 셀 클릭시 해당 row의 매장명 저장
                string selectedStoreName = ((sender as DataGridView).Rows[e.RowIndex].Cells["매장명"].Value.ToString());

                // 해당 매장명 row 클릭시 해당 매장명을 통해 데이터 조회 후 오른쪽에 데이터를 뿌림
                string queryReadStoreSelected = sqlQuery.SelectAllFrom_Where_("st_table", new string[] { "st_name" }, new string[] { selectedStoreName });
                St_table store = crud.Read_MySql(queryReadStoreSelected, new St_table());
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
                string queryTemp = sqlQuery.select_Sum__From_Where_GroupBy_(columnResult, "st_expense", "st_code", st_code, "Month(st_exp_date)");
                //string queryReadExpense = sqlQuery.select_From_Where("st_expense", new string[] { "st_exp_basic", "st_exp_coupon", "st_exp_ad", "st_exp_acce", "st_exp_quick", "st_exp_cons", "st_exp_arti" }, "st_code", new string[] { store.St_code });
                DataSet dataSetExpense = crud.ReadToGrid_MySql(queryTemp);

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

                dataGridViewEmployee.Columns.Clear();

                DataColumn dataColumn = new DataColumn();
                dataColumn.ColumnName = "No";
                dataSetEmployee.Tables[0].Columns.Add(dataColumn);
                dataColumn.SetOrdinal(0);

                tempFuncForWinform.setDataGridView(dataSetEmployee, dataGridViewEmployee);
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

                dataGridViewSelectEmployee.Columns.Clear();

                tempFuncForWinform.setDataGridView(dataSetEmployeePanel, dataGridViewSelectEmployee);

                

                //관리비 남은 날짜 표시
                //과거
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
                    labelManagementCostDateLeft.Text = (DateTime.DaysInMonth(dateTimePickerManagementExpense.Value.Year, dateTimePickerManagementExpense.Value.Month) - DateTime.Now.Day  + dateTimePickerManagementExpense.Value.Day).ToString();
                    return;
                }
                labelManagementCostDateLeft.Text =(DateTime.DaysInMonth(dateTimePickerManagementExpense.Value.Year, dateTimePickerManagementExpense.Value.Month) - DateTime.Now.Day - 1 + dateTimePickerManagementExpense.Value.Day).ToString();
                
            }

        }
        private void buttonSelectEmployee_Click(object sender, EventArgs e)
        {
            if (buttonSelectEmployee.Text == "직원열기")
            {
                panelSelectEmployee.Visible = true;
                buttonSelectEmployee.Text = "직원닫기";
                return;
            }
            if(buttonSelectEmployee.Text == "직원닫기")
            {
                panelSelectEmployee.Visible = false;
                buttonSelectEmployee.Text = "직원열기";
            }

        }
        private void buttonEmployeeManageTemp_Click(object sender, EventArgs e)
        {
            panelEmployeeManage.Visible = true;
            panelEmployeeManage.BringToFront();
            panelResultManage.Visible = false;
            foreach ( var v in menuButtonArray)
            {
                v.BackColor = Color.White;
            }
            (sender as Button).BackColor = Color.FromArgb(230, 230, 230);

            UserControl1 userControl1= new UserControl1();
            userControl1.Dock = DockStyle.Fill;

            panelEmployeeManage.Controls.Clear();

            panelEmployeeManage.Controls.Add(userControl1);





        }

        private void buttonStoreManageTemp_Click(object sender, EventArgs e)
        {
            panelEmployeeManage.Visible = false;
            panelResultManage.Visible = false;
            foreach (var v in menuButtonArray)
            {
                v.BackColor = Color.White;
            }
            (sender as Button).BackColor = Color.FromArgb(230, 230, 230);
        }

        private void dataGridViewSelectEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0&& e.ColumnIndex >= 0)
            {
                textBoxAdminName.Text = dataGridViewSelectEmployee.Rows[e.RowIndex].Cells["이름"].Value.ToString();
            }
        }

        private void dataGridViewExpense_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                selectedMonth = dataGridViewExpense.Rows[e.RowIndex].Cells["월"].Value.ToString();
            }
        }
        private void dataGridViewStore_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
        
        private void readonlyTextBoxAndDatetimePicker(bool isReadonly)
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
            if (!isReadonly)
            {
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
                return;
            }
            
        }
        private void readStoreTablePattern()
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
        private void clearDatetimePickerAndTextBox()
        {
            //달력 초기화
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

        private void textBoxSetting()
        {
            foreach (var b in textBoxArray)
            {
                b.TextAlign = HorizontalAlignment.Center;
                b.Font = new Font("나눔고딕", 10, FontStyle.Regular);
            }
        }

        private void buttonEnableToggle(bool isSelectable, Button[] buttonArray)
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

        private void buttonResultManegeTemp_Click(object sender, EventArgs e)
        {
            panelResultManage.Visible = true;
            panelResultManage.BringToFront();
            panelEmployeeManage.Visible = false;
            foreach (var v in menuButtonArray)
            {
                v.BackColor = Color.White;
            }
            (sender as Button).BackColor = Color.FromArgb(230, 230, 230);

            UserControl2 userControl2 = new UserControl2(id, sqlQuery, crud, tempFuncForWinform);
            userControl2.Dock = DockStyle.Fill;

            panelResultManage.Controls.Clear();

            panelResultManage.Controls.Add(userControl2);

        }

        

        

        private void buttonSelectDoro_Click(object sender, EventArgs e)
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

        private void dataGridViewSelectDoro_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void clearPanelSelectDoro()
        {
            dataGridViewSelectDoro.DataSource = null;
            panelSelectAddress.Visible = false;
            textBoxSelectDoro.Text = "";
        }

        private void resetToDefault()
        {
            buttonAddStore.Text = "등록";
            buttonUpdate.Text = "변경";
            buttonSelectAddress.Text = "검색";
            buttonSelectEmployee.Text = "직원열기";
            panelSelectEmployee.Visible = false;
            panelSelectAddress.Visible = false;
            clearPanelSelectDoro();
        }

        private void labelSetting()
        {
            foreach ( var v in labelArray)
            {
                v.Font = new Font("나눔고딕", 10, FontStyle.Regular);
            }
        }

        private void dateTimePickerSetting()
        {
            foreach( var v in dateTimePickerArray)
            {
                v.Font= new Font("나눔고딕", 10, FontStyle.Regular);
            }
        }

        private void panelStoreName_Paint(object sender, PaintEventArgs e)
        {
            textBoxStroeName.BorderStyle = BorderStyle.None;
            var g = e.Graphics;
            g.DrawRectangle(Pens.Blue, new Rectangle(textBoxStroeName.Location.X - 3, textBoxStroeName.Location.Y - 3, textBoxStroeName.Width + 3, textBoxStroeName.Height + 3));
        }

        private void panelAdminName_Paint(object sender, PaintEventArgs e)
        {
            textBoxAdminName.BorderStyle = BorderStyle.None;
            var g = e.Graphics;
            g.DrawRectangle(Pens.Blue, new Rectangle(textBoxAdminName.Location.X - 3, textBoxAdminName.Location.Y - 3, textBoxAdminName.Width + 3, textBoxAdminName.Height + 3));
        }
    }

}
