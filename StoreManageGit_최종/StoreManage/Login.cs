using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManage
{
    public partial class Login : Form
    {
        SqlQuery sqlQuery;
        Crud crud;
        Temp _temp;
        public Login()
        {
            InitializeComponent();
            string connectionString = $"server={"192.168.0.78"};Database={"2kdigital"};Uid={"root"};Pwd={"rladudwo"};";

            sqlQuery = new SqlQuery();
            crud = new Crud(connectionString);
            _temp = new Temp();
        }

        /// <summary>
        /// <para>종류 : 버튼 클릭 이벤트 메소드</para>
        /// <para>기능 : id, pass일치시 로그인</para>
        /// <para>로직 : 입력받은 id를 기준으로 Query작성, Crud클래스의 Read_MySql을 통해 db로 부터  employee 객체를 얻음</para>
        /// <para>       db로 부터 얻은 employee 객체의 정보를 바탕으로 id와 pass 일치 확인</para>
        /// <para>       일치시 로그인, 불일치시 로그인 불가 메시지 박스 출력</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttonConfirm_Click(object sender, EventArgs e)
        {
            string queryRead = sqlQuery.SelectAllFrom_Where_("st_employee", new string[] { "st_emp_id" }, new string[] { textBoxId.Text });
            Employee employee = crud.Read_MySql(queryRead, new Employee());

            if ( employee.st_emp_id == textBoxId.Text && employee.st_emp_password == textBoxPassword.Text)
            {
                StoreManage storeManage = new StoreManage(employee);
                storeManage.Show();
                this.Visible = false;
                return;
            }
            MessageBox.Show("로그인 불가");
            TextBox[] ArrayTextBoxLogin = new TextBox[] {textBoxId, textBoxPassword };
            _temp.clearTextFromArray(ArrayTextBoxLogin);




        }
        /// <summary>
        /// id 텍스트 클릭시 text삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void textBox_Click(object sender, EventArgs e)
        {
            (sender as TextBox).Text = "";
        }
    }
}
