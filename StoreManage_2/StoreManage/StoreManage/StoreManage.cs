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
        string id;
        string st_code;
        SortedSet<string> setCheckedStoreName;
        TextBox[] textBoxArray;
        DateTimePicker[] dateTimePickerArray;
        bool updateFlag;

        public StoreManage(string id)
        {
            //string connectionString = $"server={"192.168.0.44"};Database={"netdb"};Uid={"netuser"};Pwd={"2k1234"};";
            //임시 db
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\2klab\OneDrive\2kd\Projects_c#_real\StoreManage\StoreManage\temp.mdf;Integrated Security=True;Connect Timeout=30";
            InitializeComponent();
            this.crud = new Crud(connectionString);
            this.sqlQuery = new SqlQuery();

            this.textBoxArray = new TextBox[] {textBoxStroeName, textBoxAdminName,textBoxAddressZip, textBoxAddress, textBoxBasicInformation, textBoxContractInformation };
            this.dateTimePickerArray = new DateTimePicker[] {dateTimePickerContractDate, dateTimePickerContractDate2, dateTimePickerManagementExpense, dateTimePickerElectricityExpense, dateTimePickerWaterExpense };
            this.id = id;
            this.updateFlag = false;

            this.setCheckedStoreName = new SortedSet<string>();

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
            string queryReadEmp = sqlQuery.SelectAllFrom_Where_("st_employee", "st_emp_code", new string[] { id});
            Employee emp = crud.Read_MS(queryReadEmp, new Employee());
            if (emp.st_emp_level == "admin")
            {
                labelAdminName.Text = emp.st_emp_member + "관리자";
            }
            else
            {
                labelAdminName.Text = emp.st_emp_member + "비관리자";
            }


            //4. Panel_Left_Second datatable 할당
            //string queryReadStore = sqlQuery.select_From_Where("st_table", new string[] { "st_name", "st_manager"},"st_manager", new string[] {id});
            string queryReadStore = sqlQuery.select_From_("store", new string[] { "st_name", "st_manager" });
            DataSet dataSetStore = crud.ReadToGrid_MS(queryReadStore);
            setDataGridView(dataSetStore, dataGridViewStore);
            DataGridViewCheckBoxColumn dgvCmb = new DataGridViewCheckBoxColumn();
            dgvCmb.ValueType = typeof(bool);
            dgvCmb.Name = "checkBox";
            dataGridViewStore.Columns.Insert(0, dgvCmb);

            
            dataGridViewStore.Columns[0].Width = 30;
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
            foreach ( var v in setCheckedStoreName)
            {
                checkedNames += v + " ";
            }

            if (MessageBox.Show(checkedNames + "선택", "주의", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string resultName = "";
                foreach (var v in setCheckedStoreName)
                {

                    string queryRead = sqlQuery.SelectAllFrom_Where_("store", "st_name", new string[] { v });
                    Store store = crud.Read_MS(queryRead, new Store());
                    string queryDelete = sqlQuery.deleteFrom_Where_("store", "st_code");
                    int queryResult = crud.Delete_MS(queryDelete, store);
                    if (queryResult > 0)
                    {
                        resultName += v + " ";
                    }
                    else
                    {
                        Console.WriteLine("실패");
                        MessageBox.Show($"({ resultName})삭제 성공, ({v})삭제 실패");
                        string queryReadStore = sqlQuery.select_From_("store", new string[] { "st_name", "st_manager" });
                        DataSet dataSetStore = crud.ReadToGrid_MS(queryReadStore);
                        setDataGridView(dataSetStore,dataGridViewStore);
                        return;
                    }

                }
                MessageBox.Show(resultName + "삭제 성공");
                string queryReadStore2 = sqlQuery.select_From_("store", new string[] { "st_name", "st_manager" });
                DataSet dataSetStore2 = crud.ReadToGrid_MS(queryReadStore2);
                setDataGridView(dataSetStore2, dataGridViewStore);
            }
            
            
        }

        //추가 처리 필요
        private void buttonAddStore_Click(object sender, EventArgs e)
        {
            string queryReadMaxStcode = sqlQuery.selectAllFrom_Where_Select__From_("store", "max", "st_code");
            Store store = crud.Read_MS(queryReadMaxStcode, new Store());
            store.St_code = (Convert.ToInt32(store.St_code) + 1).ToString();
            store.St_name = textBoxStroeName.Text;
            store.St_manager = textBoxAdminName.Text;
            store.St_zip = textBoxAddressZip.Text;
            store.St_addr1 = textBoxAddress.Text;
            store.St_info_contract = textBoxContractInformation.Text;
            store.St_info_basic = textBoxBasicInformation.Text;
            store.St_contract_date = dateTimePickerContractDate.Value;
            store.St_contract_date2 = dateTimePickerContractDate2.Value;
            store.St_electricity_expense_date = dateTimePickerElectricityExpense.Value;
            store.St_management_expense_date = dateTimePickerElectricityExpense.Value;
            store.St_water_expense_date = dateTimePickerElectricityExpense.Value;
            store.St_date = DateTime.Now;
            string queryCreate = sqlQuery.InsertInto__Values_(store);
            int result = crud.Create_MS(queryCreate, store);

            if (result > 0)
            {
                MessageBox.Show("성공");
                string queryReadStore = sqlQuery.select_From_("store", new string[] { "st_name", "st_manager" });
                DataSet dataSetStore = crud.ReadToGrid_MS(queryReadStore);
                setDataGridView(dataSetStore, dataGridViewStore);

                return;
            }
            MessageBox.Show("실패");
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
            //조회시에는 textbox, button 비활성화
            
            
        }

        private void buttonSelectExpenseDetail_Click(object sender, EventArgs e)
        {
            //자세히를 버튼으로 할 것인지?
            //셀 클릭시 row, column를 통해 month를 저장해두고 클릭시 select

            // 아니면 월 셀 왼쪽에 +를 두고 열었다 닫았다 할 수 있게 할 것인지?
            // 테이블 확장 추가 공부 필요
            
        }

        private void buttonManagementCostDateCalendar_Click(object sender, EventArgs e)
        {
            // 날짜 클릭시 남은기간 계산하는 이벤트 필요
        }

        private void dataGridViewStore_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 1)
            {
                buttonUpdate.Visible = true;
                // 검색시 항목 변경 불가 처리
                foreach (var v in textBoxArray)
                {
                    v.ReadOnly = true;
                    v.BackColor = Color.FromArgb(230, 230, 230);
                }
                buttonSelectAddress.Visible = false;
                buttonSelectEmployee.Visible = true;

                // 셀 클릭시 해당 row의 매장명 저장
                string selectedStoreName = ((sender as DataGridView).Rows[e.RowIndex].Cells["st_name"].Value.ToString());

                // 해당 매장명 row 클릭시 해당 매장명을 통해 데이터 조회 후 오른쪽에 데이터를 뿌림
                string queryReadStoreSelected = sqlQuery.SelectAllFrom_Where_("store", "st_name", new string[] { selectedStoreName });
                Store store = crud.Read_MS(queryReadStoreSelected, new Store());
                st_code = store.St_code;
                textBoxStroeName.Text = store.St_name;
                textBoxAdminName.Text = store.St_manager;
                textBoxAddressZip.Text = store.St_zip;
                textBoxAddress.Text = store.St_addr1;
                textBoxContractInformation.Text = store.St_info_contract;
                textBoxBasicInformation.Text = store.St_info_basic;
                dateTimePickerContractDate.Value = store.St_contract_date;
                dateTimePickerContractDate2.Value = store.St_contract_date2;
                dateTimePickerManagementExpense.Value = store.St_management_expense_date;
                dateTimePickerElectricityExpense.Value = store.St_management_expense_date;
                dateTimePickerWaterExpense.Value = store.St_management_expense_date;
                


                // Panel_Main_Third datatable할당
                string queryReadExpense = sqlQuery.select_From_Where("st_expense", new string[] { "st_exp_basic", "st_exp_coupon", "st_exp_ad", "st_exp_acce", "st_exp_quick", "st_exp_cons", "st_exp_arti" }, "st_code", new string[] { store.St_code });
                DataSet dataSetExpense = crud.ReadToGrid_MS(queryReadExpense);
                setDataGridView(dataSetExpense, dataGridViewExpense);

                // Panel_Main_Forth datatable 할당
                string queryReadEmployee = sqlQuery.select_From_Where("st_employee", new string[] { "st_emp_level", "st_emp_member", "st_emp_state", "st_emp_state", "st_emp_time", "st_emp_year", "st_emp_etc" }, "st_code", new string[] { store.St_code });
                DataSet dataSetEmployee = crud.ReadToGrid_MS(queryReadEmployee);
                dataGridViewEmployee.Columns.Clear();
                setDataGridView(dataSetEmployee, dataGridViewEmployee);

                DataGridViewColumn dataGridViewColumn = new DataGridViewColumn();
                dataGridViewColumn.Name = "RowCount";
                dataGridViewColumn.ReadOnly = true;
                dataGridViewColumn.CellTemplate = new DataGridViewTextBoxCell();
                dataGridViewEmployee.Columns.Insert(0, dataGridViewColumn);
                for (int i = 0; i < dataSetEmployee.Tables[0].Rows.Count; i++)
                {
                    dataGridViewEmployee.Rows[i].Cells[0].Value = i + 1;
                };


            }
            


        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            // 검색시 항목 변경 가능 처리
            foreach (var v in textBoxArray)
            {
                v.ReadOnly = false;
                v.BackColor = Color.FromArgb(255, 255, 255);
            }
            buttonSelectAddress.Visible = true;
            buttonSelectEmployee.Visible = true;
            
            if (updateFlag)
            {
                string queryUpdate = sqlQuery.Update_Set_("store", new Store(), "St_code");
                string queryRead = sqlQuery.SelectAllFrom_Where_("store", "st_code", new string[] { st_code});
                Store storeRead = crud.Read_MS(queryRead, new Store());

                Store store = new Store
                {
                    St_code = storeRead.St_code,
                    St_date = storeRead.St_date,
                    St_name = textBoxStroeName.Text,
                    St_manager = textBoxAdminName.Text,
                    St_zip = textBoxAddressZip.Text,
                    St_addr1 = textBoxAddress.Text,
                    St_info_contract = textBoxContractInformation.Text,
                    St_info_basic = textBoxBasicInformation.Text,
                    St_contract_date = dateTimePickerContractDate.Value,
                    St_contract_date2 = dateTimePickerContractDate2.Value,
                    St_electricity_expense_date = dateTimePickerElectricityExpense.Value,
                    St_management_expense_date = dateTimePickerElectricityExpense.Value,
                    St_water_expense_date = dateTimePickerElectricityExpense.Value
                };
                int result = crud.Update_MS(queryUpdate, store);

                if (result > 0)
                {
                    MessageBox.Show("성공");
                    string queryReadStore = sqlQuery.select_From_("store", new string[] { "st_name", "st_manager" });
                    DataSet dataSetStore = crud.ReadToGrid_MS(queryReadStore);
                    setDataGridView(dataSetStore,dataGridViewStore );
                    return;
                }
                MessageBox.Show("실패");
            }
            updateFlag = true;
            
        }

        private void dataGridViewStore_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //체크가 true인지
            if (e.RowIndex >= 0&& e.ColumnIndex >=0)

            {
                if ((bool)(dataGridViewStore.Rows[e.RowIndex].Cells[0].EditedFormattedValue ?? false) == true)
                {
                    setCheckedStoreName.Add((string)dataGridViewStore.Rows[e.RowIndex].Cells["st_name"].Value);
                }
                else if ((bool)(dataGridViewStore.Rows[e.RowIndex].Cells[0].EditedFormattedValue ?? false) == false)
                {
                    setCheckedStoreName.Remove((string)dataGridViewStore.Rows[e.RowIndex].Cells["st_name"].Value);
                }
            }
            
        }

        private void buttonSelectEmployee_Click(object sender, EventArgs e)
        {

        }

        private void setDataGridView(DataSet dataset, DataGridView datagridview)
        {
            datagridview.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(68, 114, 196);
            datagridview.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            datagridview.ColumnHeadersHeight = 30;
            datagridview.EnableHeadersVisualStyles = true;
            datagridview.RowHeadersVisible = false;
            datagridview.DefaultCellStyle.BackColor = Color.FromArgb(207, 213, 234);
            datagridview.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(233, 235, 245);
            datagridview.DataSource = dataset.Tables[0];

        }
    }
}
