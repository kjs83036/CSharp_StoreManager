using KjsLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManage
{
    public partial class StoreManage : Form
    {
        Crud crud;
        SqlQuery sqlQuery;
        DuplicatedFunction duplicatedFunction;
        List<string> checkedStoreName;
        public StoreManage()
        {
            string connectionString = $"server={"192.168.0.44"};Database={"netdb"};Uid={"netuser"};Pwd={"2k1234"};";
            InitializeComponent();
            this.crud = new Crud(connectionString);
            this.sqlQuery = new SqlQuery();
            this.duplicatedFunction = new DuplicatedFunction(crud, sqlQuery);

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

            //4. Panel_Left_Second datatable 할당

            //5. panel_Main_First 매장명, 관리자명 할당 및 textbox border color 변환

            //6. Panel_Main_Second_1 textbox border color 변환

            //7. Panel_Main_Second_4 가능하다면 db로부터 날짜를 할당, LeftDate 변경

            //8. Panel_Main_Third datatable할당

            //9. Panel_Main_Forth datatable 할당

        }

        private void buttonDeleteStore_Click(object sender, EventArgs e)
        {
            Store store = new Store();

            if (duplicatedFunction.isBiggerThanZero(crud.Delete_MySql(sqlQuery.deleteFrom_Where_("st_table","st_code" ),store )))
            {

            }
        }

        private void buttonAddStore_Click(object sender, EventArgs e)
        {

        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {

        }

        private void buttonSelectAddress_Click(object sender, EventArgs e)
        {

        }

        private void buttonSelectExpenseDetail_Click(object sender, EventArgs e)
        {

        }

        private void buttonManagementCostDateCalendar_Click(object sender, EventArgs e)
        {
            // 날짜 클릭시 남은기간 계산하는 이벤트 필요
        }

        private void dataGridViewStore_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
