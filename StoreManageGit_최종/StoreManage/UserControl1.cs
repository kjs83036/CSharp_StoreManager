using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
//using MySql.Data.MySqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Security.Policy;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Markup;
using System.IO;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace StoreManage
{

    public partial class UserControl1 : UserControl
    {
        //PictureBox pictureBox1;         //픽쳐박스
        //ComboBox combo_store;           //매장 콤보박스
        //TextBox txt_name;               //이름 텍스트박스
        //Button btn_research;            //우편번호,주소 검색버튼
        //TextBox txt_birth;              //생년월일 텍스트 박스
        //TextBox txt_zipCode;            //우편번호 텍스트 박스

        //TextBox txt_address;            //주소 텍스트 박스
        //RadioButton btn_man;            //성별 남 라디오버튼
        //RadioButton btn_wommon;         //성별 여 라디오버튼
        //Button btn_picture_Add;         //사진 추가 버튼
        //Button btn_employee_serarch;   //직원 검색 버튼
        //Button btn_employee_ADD;       //직원 추가 버튼
        //DataGridView dataGridView1;     //직원 데이터 그리드뷰
        //TabControl tabControl;          // 월 ,일별 탭
        //TabPage tabPage1;
        //TabPage tabPage2;

        //버튼이 눌렸는지 확인 하기 위함
        static decimal show = 0;

        //한번만 실행되게 하기 위해 (text박스 초기값설정후 클릭이벤트로 초기화 시킬것)
        static int show2 = 0; 
      //--------------------------------------//
        make_qury qury;
        DBClass DB;

        Crud crud;
        SqlQuery sqlQuery;
        static public string strCon = "Server=192.168.0.78;Database=2kdigital ;Uid=root;Pwd=rladudwo;";
       
        string combo_vlaue = "";                     //콤보박스 값
        static public string Top_combo_vlaue;       //맨위 콤보박스 값
        static public int Radio;
        public string filepath;                     //사진 저장 경로

        //상단 그리드뷰 클릭된 직원 정보
        public string click_member;                //클릭된 직원 이름
        public string click_addr;                    //클릭된 직원 주소
        public string click_birth;                  //클릭된 직원 생년월일




        public UserControl1()
        {
            InitializeComponent();
            crud = new Crud(strCon);

        }



        public void UserControl1_Load(object sender, EventArgs e)
        {
           
            panel_Center_Top.Visible = true;





            //-------- 콤보박스에 매장 명 넣기-----------//
            string[] aa = insert_combox();

            combo_store2.Items.AddRange(aa);
            combo_storeName.Items.AddRange(aa);
            combo_storeName.SelectedIndex = 0;
            //------------------------------------------//

            txt_Top_Name.Text = "이름";       //상단 이름 text박스


            //-----------------우편번호 text박스------------//
            txt_zipCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            txt_zipCode.Text = "우편번호";


            //----------------상단 검색 버튼----------------//
            btn_research.UseVisualStyleBackColor = true;
            btn_research.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));
            btn_research.FlatStyle = FlatStyle.Flat;
            btn_research.ForeColor = Color.White;

            //----------------------------------------------------------


            label_sex.TextAlign = ContentAlignment.MiddleRight;     //성별 라벨

            btn_man.Checked = false;                                //남자 오디오 버튼


            btn_woman.Checked = false;                              //여자 오디오 버튼



            //----------------------------사진 추가 버튼---------------------------------------//
            btn_picture_Add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));
            btn_picture_Add.FlatStyle = FlatStyle.Flat;
            btn_picture_Add.ForeColor = Color.White;


            //----------------------------등록 버튼-------------------------------------------//
            btn_enrollment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));
            btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));
            btn_Ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));



            //-----------------------------상단 데이터그리드 뷰 ----------------//
            dataGridView1.Size = new System.Drawing.Size(1104, 171);
            dataGridView1.BackgroundColor = Color.LightGray;
            dataGridView1.TabIndex = 0;

            dataGridView1.AutoGenerateColumns = false;

            //그리드뷰 초기화
            DataGrid_Initial();


            //----------------첫번째 그리드뷰------------
            
            //기본 셀 색상
            dataGridView1.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(213)))), ((int)(((byte)(234)))));
            //홀수 행 셀 색상 
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(235)))), ((int)(((byte)(245)))));


            //-----------------월 탭의 그리드뷰------------
            //기본 셀 색상
            dataGridView2.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(213)))), ((int)(((byte)(234)))));
            //홀수 행 셀 색상 
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(235)))), ((int)(((byte)(245)))));

            //----------------일별 탭의 그리드뷰-----------
            //기본 셀 색상
            dataGridView3.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(213)))), ((int)(((byte)(234)))));
            //홀수 행 셀 색상 
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(235)))), ((int)(((byte)(245)))));


            Controls.Add(this.dataGridView1);
            //Controls.Add(tabControl);



            // ----------우편번호 text박스 클릭 이벤트(한번만 실행됨)----------//
            txt_zipCode.Click += (q, w) =>
            {
                txt_zipCode.Text = null;
            };

            //-----------상단 이름 text박스 클릭 이벤트(한번만 실행됨)----------//
            txt_Top_Name.Click += (q, w) =>
            {
                txt_Top_Name.Text = null;

            };
        
            //------------등록 버튼 클릭 이벤트(한번만 실행됨)------------------//
            btn_Ok.Click += (q, w) =>
             {
                 show2 = 1;
             };

            //우편번호 검색창의 데이터그리드 뷰 설정
            dataGridView_Zip_addr.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(68, 114, 196);
            dataGridView_Zip_addr.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView_Zip_addr.ColumnHeadersHeight = 30;
            dataGridView_Zip_addr.EnableHeadersVisualStyles = true;
            dataGridView_Zip_addr.RowHeadersVisible = false;
            dataGridView_Zip_addr.DefaultCellStyle.BackColor = Color.FromArgb(207, 213, 234);
            dataGridView_Zip_addr.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            dataGridView_Zip_addr.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(233, 235, 245);

            dataGridView_Zip_addr.AllowUserToAddRows = false;
            dataGridView_Zip_addr.AllowUserToDeleteRows = false;
            dataGridView_Zip_addr.ReadOnly = true;
            dataGridView_Zip_addr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_Zip_addr.DefaultCellStyle.SelectionBackColor = Color.FromArgb(160, 170, 190);
            dataGridView_Zip_addr.MultiSelect = false;
            dataGridView_Zip_addr.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_Zip_addr.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_Zip_addr.Font = new Font("나눔고딕", 10, FontStyle.Regular);
            dataGridView_Zip_addr.BackgroundColor = Color.GhostWhite;
            dataGridView_Zip_addr.GridColor = Color.White;



            panel_addrResearch.Visible = false;
          

        }




        //------------------------------------콤보 박스에 db 바인딩-------------------------------//
        public string[] insert_combox()
        {
            
            employee emp0 = new employee();         //employee 클래스
            make_qury qury = new make_qury();       //쿼리만들어주는 클래스
            DBClass db = new DBClass();             //db 만들어주는 클래스



            //string researchCode = qury.distinct__ALL_select_qury("st_table","st_name");
            
            //매장이름 가져오기
            string researchCode = qury.All_select_qury("st_table", emp0);
            DataSet ds = db.DataSetGrid(researchCode);

            DataTable dt = ds.Tables[0];
            string store_name = dt.Columns[1].ToString();




            object[] arr_st_name = dt.Select().Select(x => x["st_name"]).Distinct().ToArray();
            string[] str_st_name = arr_st_name.Cast<string>().ToArray();


            return str_st_name;

        }




        /// <summary>
        /// <para>기능 :  직원 조회</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  1. 입력이 &quot;&quot;</para>
        /// <para>분기 :  2. dr["st_emp_member"].ToString() != txt_Top_Name.Text</para>
        /// <para>로직 :  1. sqlQuery.SelectAllFrom_Where_와 DB.DataSetGrid를 통해 DataSet얻음</para>
        /// <para>로직 :  2. sqlQuery.SelectAllFrom_Where_와 DB.DataSetGrid를 통해 직원 DataSet얻음</para>
        /// <para>로직 :  3. 매장, 성별, 이름, 생년월일, 주소 외 컬럼 삭제</para>
        /// <para>로직 :  4. if txt_Top_Name.Text == &quot;&quot;</para>
        /// <para>로직 :  ----5. 컬럼이름 한글로</para>
        /// <para>로직 :  ----6. 테이블에 Dataset할당</para>
        /// <para>로직 :  ----7. 매장 및 NO컬럼 추가</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //--------------------------------상단 검색 버튼(직원 조회)-----------------------------------//
        public void btn_top_search_Click(object sender, EventArgs e)
        {   
            if(show==0)
            {
                txt_Top_Name.Text = null;
            }
            //버튼이 눌렸는지 확인 하기 위함
            show += 1;

            //-----------------------------//

            sqlQuery = new SqlQuery();
            DB = new DBClass();

            string Top_name = txt_Top_Name.Text;

            string[] values = { Top_combo_vlaue };
            //매장명을 이용하여 매장코드 검출
            string researchCode = sqlQuery.SelectAllFrom_Where_("st_table", new string[] { "st_name" }, values);
            DataSet dataset = DB.DataSetGrid(researchCode);
            DataTable dt = dataset.Tables[0];
            
            //매장코드
            string store_number = dt.Rows[0][0].ToString();


            string[] values2 = { store_number };

            string dbRead = sqlQuery.SelectAllFrom_Where_("st_employee", new string[] { "st_code" }, values2);
            DataSet ds2 = DB.DataSetGrid(dbRead);


            DataTable dt2 = ds2.Tables[0];
            DataTable dt3 = ds2.Tables[0];

            // 매장, 성별, 이름, 생년월일, 주소 빼고 다 지우기
            string[] col = { "st_emp_code", "st_code", "st_emp_level", "st_emp_addr2", "st_emp_zip","st_emp_Year", "st_emp_time", "st_emp_date", "st_emp_state", "st_emp_etc" ,"st_emp_picture_Path"};
            foreach (string i in col)
            {
                dt2.Columns.Remove(i);
            }

            //string[] col2 = { "st_emp_code", "st_code", "st_emp_level", "st_emp_Year", "st_emp_time", "st_emp_date", "st_emp_state", "st_emp_etc", "st_emp_picture_Path" };
            //foreach (string i in col2)
            //{
            //    dt3.Columns.Remove(i);
            //}


            //매장만 검색했을때
            if (txt_Top_Name.Text == "")
            {

                dataGridView1.Columns["성별"].DataPropertyName = "st_emp_sex";
                dataGridView1.Columns["이름"].DataPropertyName = "st_emp_member";
                dataGridView1.Columns["생년월일"].DataPropertyName = "st_emp_birth";
                dataGridView1.Columns["주소"].DataPropertyName = "st_emp_addr1";
       


                dataGridView1.DataSource = dt2;

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    dataGridView1.Rows[i].Cells["매장"].Value = Top_combo_vlaue;


                    dataGridView1.Rows[i].Cells["No"].Value = i + 1;
                }
            }

            //매장이름과 직원이름으로 검색했을 때
            else
            {

                foreach (DataRow dr in dt2.Rows)
                {
                    if (dr["st_emp_member"].ToString() != txt_Top_Name.Text)

                    {
                        dr.Delete();
                    }
                }
                dt2.AcceptChanges();

                dataGridView1.Columns["성별"].DataPropertyName = "st_emp_sex";
                dataGridView1.Columns["이름"].DataPropertyName = "st_emp_member";
                dataGridView1.Columns["생년월일"].DataPropertyName = "st_emp_birth";
                dataGridView1.Columns["주소"].DataPropertyName = "st_emp_addr1";
                dataGridView1.DataSource = dt2;

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    dataGridView1.Rows[i].Cells["매장"].Value = Top_combo_vlaue;


                    dataGridView1.Rows[i].Cells["No"].Value = i + 1;
                }

            }


            
        }




        /// <summary>
        /// <para>기능 :  직원 등록</para>
        /// <para>진입 :  combo_vlaue != &quot;&quot;</para>
        /// <para>분기 :  1. query결과 DataSet크기가 1이상</para>
        /// <para>분기 :  2. 마지막에 있는 직원코드의 길이가 1이거나 코드가 없을때</para>
        /// <para>분기 :  3. 디렉토리가 존재하지 않음</para>
        /// <para>분기 :  4. 사진경로가 존재함</para>
        /// <para>분기 :  5. 직원코드가 중복됨</para>
        /// <para>분기 :  6. query결과 값이 -1이 아님</para>
        /// <para>로직 :  1. 매장명을 이용하여 매장코드 검출</para>
        /// <para>로직 :  2. 매장 직원수 검출</para>
        /// <para>로직 :  3. if 매장직원수 1 > 1</para>
        /// <para>로직 :  ----4. 마지막직원코드 저장</para>
        /// <para>로직 :  5. if 마지막에 있는 직원코드의 길이가 1이거나 코드가 없을때</para>
        /// <para>로직 :  ----6. num + 1 직원코드</para>
        /// <para>로직 :  7. 직원코드 중 store_code를 잘라낸 후 int화 하여 +1</para>
        /// <para>로직 :  8. 직원 중복 확인</para>
        /// <para>로직 :  9. if 디렉토리 존재 x</para>
        /// <para>로직 :  ----10. 디렉토리 생성</para>
        /// <para>로직 :  11. 사진 경로 설정</para>
        /// <para>로직 :  12. if 사진 경로 존재</para>
        /// <para>로직 :  ----13. 사진 저장</para>
        /// <para>로직 :  14. if 직원코드 중복 확인</para>
        /// <para>로직 :  ----15. 직원코드 확인 메시지</para>
        /// <para>로직 :  16. employee 할당 및 등록</para>
        /// <para>로직 :  16. DB.insert 결과 != -1</para>
        /// <para>로직 :  ----16. 성공 메시지</para>
        /// <para>로직 :  ----17. 확인 버튼 활성화</para>
        /// <para>로직 :  18. 실패 메시지</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //-----------------------------------------등록 버튼(직원 등록)---------------------------------------//
        public void btn_enrollment_Click(object sender, EventArgs e)
        {

            if (combo_vlaue != "")
            {

                Employee emp0 = new Employee();
                sqlQuery = new SqlQuery();
                DB = new DBClass();
                qury = new make_qury();

                string[] values = { combo_vlaue };



                //---------매장명을 이용하여 매장코드 검출---------------//
                string researchCode = sqlQuery.SelectAllFrom_Where_("st_table", new string[] { "st_name" }, values);
                DataSet dataset = DB.DataSetGrid(researchCode);
                DataTable dt = dataset.Tables[0];

                //---------------------매장코드-----------------//
                string store_number = dt.Rows[0][0].ToString();

                //---------------해당 매장의 직원수 검출하기--------------//
                string[] values1 = { store_number };
                //string dbRead1 = sqlQuery.SelectAllFrom_Where_("st_employee", "st_code", values1);
                string dbRead1 = qury.SelectAllFrom_Where_DESC_("st_employee", "st_code", values1);
                DataSet ds1 = DB.DataSetGrid(dbRead1);
                DataTable dt1 = ds1.Tables[0];
                int Num = dt1.Rows.Count;



                string str_emp_code = "";
                if (Num >= 1)
                {
                    // 마지막 직원 코드
                    str_emp_code = dt1.Rows[Num - 1][0].ToString();


                }


                int sub = (store_number.Length);



                string new_emp_code = "";
                // 직원코드에서 매장코드 뺀 숫자

                //마지막에 있는 직원코드의 길이가 1이거나 코드가 없을때
                if (str_emp_code.Length == 1 || Num == 0)

                {
                    new_emp_code = store_number + Convert.ToString(Num + 1).PadLeft(4, '0');
                }




                else
                {
                    string str_max = str_emp_code.Substring(sub);

                    int int_max = Convert.ToInt32(str_max);

                    int int_max1 = int_max + 1;           //마지막 지원코드 +1

                    // 추가할때 새로운 직원코드
                    new_emp_code = store_number + Convert.ToString(int_max1).PadLeft(4, '0');
                }




                //해당 매장의 정보
                string[] values2 = { new_emp_code };
                string dbCreate = qury.insert_qury("st_employee", emp0);


                string dbRead = sqlQuery.SelectAllFrom_Where_("st_employee", new string[] { "st_emp_code" }, values2);

                //employee result = crud.Read_MS(dbRead, emp0);
                DataSet result = DB.DataSetGrid(dbRead);

                DataTable dt100 = result.Tables[0];

                //result..st_emp_code


                //사진 저장
                string path = System.IO.Directory.GetCurrentDirectory() + @"\emp";
                DirectoryInfo di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                    di.Create();
                }

                //사진 저장 경로
                string savefolder = path;
                string savePath = savefolder + $"\\{new_emp_code}.Jpeg";

                savePath = savePath.Replace("\\", "/");

                //사진 경로가  존재 할때
                if (filepath != null)
                {
                    pictureBox.Image?.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                //--------------------------------------직원 코드 중복 확인----------------------------------------//

                if ((dt100.Rows.Count != 0))        //직원코드가 중복됨
                {
                    MessageBox.Show("직원 코드 확인");
                }

                else
                {


                    emp0 = new Employee()
                    {
                        st_code = store_number,
                        st_emp_id = textBoxId.Text,
                        st_emp_password = textBoxPass.Text,
                        st_emp_code = new_emp_code, //store_number+(Num+1),
                        st_emp_member = txt_name.Text,
                        st_emp_birth = txt_birth.Text,
                        st_emp_zip = txt_zipCode.Text,
                        st_emp_addr1 = txt_DORO.Text,
                        st_emp_addr2 = txt_address.Text,
                        st_emp_sex = (sbyte)Radio,
                        st_emp_picture_path = savePath
                    };

                    dbCreate = qury.insert_qury("st_employee", emp0);

                    if (DB.DataInsert(dbCreate) != -1)
                    {
                        MessageBox.Show("등록 확인");
                        //Initial();
                        btn_Ok.Enabled = true;

                    }

                    else
                    {
                        MessageBox.Show("등록 실패");
                    }
                }
            }
            
        }

        // ---------------쓸려다 안썻음---혹시 몰라 놔둠//
        public void dataTableGroupBy(DataTable data, ref DataTable copydata)
        {
            DataRow[] drSelect = null;
            DataRow[] comSelect = null;

            string filter = string.Empty;
            string order = string.Empty;

            try
            {
                int dataCnt = data.Rows.Count;
                DataView dv = data.DefaultView;
                dv.Sort = order;

                DataTable dt = dv.ToTable();
                for (int i = 0; i < dataCnt; i++)
                {
                    filter = string.Format("매장별인원='{0}'", dt.Rows[i]["st_code"]);

                    drSelect = dt.Select(filter);

                    if (drSelect.Length > 0)
                    {
                        comSelect = copydata.Select(filter);
                        if (comSelect.Length <= 0)
                        {
                            copydata.ImportRow(drSelect[0]);
                        }
                    }
                }
            }
            catch (Exception err)
            {

            }
        }



        //------------------------------남자 오디오 버튼----------------------------------------//
        public void btn_man_CheckedChanged(object sender, EventArgs e)
        {
            Radio = 1;

        }

        //--------------------------------여자 오디오 버튼--------------------------------------------//
        public void btn_woman_CheckedChanged(object sender, EventArgs e)
        {

            Radio = 2;
        }


        //---------------------------------------초기화---------------------------------------------------//
        public void Initial()
        {

            btn_woman.Checked = false;              //성별 여 오디오버튼
            btn_man.Checked = false;                //성별 남 오디오버튼
            txt_name.Text = "";                     //중앙 이름 text박스
            txt_birth.Text = "";                   //중앙 생년월일 text박스
            txt_zipCode.Text = "";                  //중앙 우편번호 text박스                
            txt_DORO.Text = "";
            txt_address.Text = "";                  //중앙 주소text박스
            pictureBox.Image = null;                //picturebox
            pictureBox.Update();
            filepath = null;                          //사진경로
        }

        /// <summary>
        /// <para>기능 :  취소 버튼</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. Initial</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //------------------------------------------취소 버튼-------------------------------------------//
        public void btn_Cancel_Click(object sender, EventArgs e)
        {
            Initial();
        }
        /// <summary>
        /// <para>기능 :  데이터 테이블 새로고침</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  </para>
        /// <para>로직 :  1. 매장명을 이용하여 매장코드 검출</para>
        /// <para>로직 :  2. sqlQuery.SelectAllFrom_Where_와 DB.DataSetGrid를 통해 매장 DataSet 얻음</para>
        /// <para>로직 :  3. 매장코드 추출</para>
        /// <para>로직 :  4. 매장코드와 query 를 통해 직원 DataSet얻음</para>
        /// <para>로직 :  5. 컬럼 추가 및 한글화</para>
        /// <para>로직 :  6. 등록 버튼 비활성화</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //-----------------------------------------확인 버튼-------------------------------------------//
        public void btn_Ok_Click(object sender, EventArgs e)
        {
            //사진경로 저장변수
            filepath = null;
            pictureBox.Image = null;
            pictureBox.Update();

            employee emp0 = new employee();
            string[] values = { combo_vlaue };

            combo_storeName.Text = combo_vlaue;
            //매장명을 이용하여 매장코드 검출

            string researchCode = sqlQuery.SelectAllFrom_Where_("st_table", new string[] { "st_name" }, values);
            DataSet dataset = DB.DataSetGrid(researchCode);
            DataTable dt = dataset.Tables[0];
            
            //매장코드
            string store_number = dt.Rows[0][0].ToString();




            string[] values2 = { store_number };

            string dbRead = sqlQuery.SelectAllFrom_Where_("st_employee", new string[] { "st_code" }, values2);




            DataSet ds2 = DB.DataSetGrid(dbRead);
            DataTable dt2 = ds2.Tables[0];

            //직원이름, 성별, 생년월일, 주소1,우편번호 를 제외하고 다 날림
            string[] col = { "st_emp_code", "st_code", "st_emp_level", "st_emp_addr2", "st_emp_zip", "st_emp_Year", "st_emp_time", "st_emp_date", "st_emp_state", "st_emp_etc" };
            foreach (string i in col)
            {
                dt2.Columns.Remove(i);
            }


            DataRow row = dt2.NewRow();


            //------------------ 첫번째 데이터그리드 뷰에 정보기입-------------------//
            dataGridView1.Columns["성별"].DataPropertyName = "st_emp_sex";
            dataGridView1.Columns["이름"].DataPropertyName = "st_emp_member";
            dataGridView1.Columns["생년월일"].DataPropertyName = "st_emp_birth";
            dataGridView1.Columns["주소"].DataPropertyName = "st_emp_addr1";
            dataGridView1.DataSource = dt2;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                dataGridView1.Rows[i].Cells["매장"].Value = combo_vlaue;


                dataGridView1.Rows[i].Cells["No"].Value = i + 1;
            }

            //등록버튼 비 활성화
            btn_Ok.Enabled = false;
        }


        //----------------------------------------중앙 매장 콤보박스------------------------------------//
        public void combo_store2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            combo_vlaue = (string)comboBox.SelectedItem.ToString();


        }

        //---------------------------------------중앙 이름 text박스-----------------------------------//
        public void txt_name_TextChanged(object sender, EventArgs e)
        {
            TextBox text = (TextBox)sender;
            txt_name.Text = text.Text;

        }








        //--------------------------------------------직원 테이블 클레스--------------------------------
        public class employee
        {
            public string st_code { get; set; }
            public string st_emp_member { get; set; }

            public string st_emp_birth { get; set; }

            public string st_emp_zip { get; set; }

            public string st_emp_addr1 { get; set; }

            public int st_emp_sex { get; set; }

            //-------------------------위에는 입력 받는값
            public string st_emp_code { get; set; }

            public char st_emp_level { get; set; }

            public string st_emp_addr2 { get; set; }

            public string st_emp_Year { get; set; }

            public string st_emp_time { get; set; }

            public DateTime st_emp_date { get; set; }

            public string st_emp_state { get; set; }

            public string st_emp_dtc { get; set; }

            public string st_emp_picture_Path { get; set; }

        }

        //-----------------------------------------매장 테이블 크레스--------------------------------------//
        public class Store
        {
            public string st_code { get; set; }

            public string st_name { get; set; }

            public string st_manager { get; set; }

            public string st_addr1 { get; set; }

            public string st_addr2 { get; set; }

            public string st_zip { get; set; }

            public DateTime st_contractdate { get; set; }

            public string st_info1 { get; set; }

            public string st_info2 { get; set; }

            public DateTime st_date { get; set; }
        }


        //------------------------------------sql쿼리만들어주는 클래스-------------------------------//
        public class make_qury
        {

            public string[] columnsMember;


            public System.Data.SqlClient.SqlConnection myConnection = null;
            public bool Dbopened = false;
            public string DBConnectionString = "";
            //WebClass kyj = null;


            //--------------------------insert 쿼리문-------------------------------//
            public string insert_qury(string table, Employee t)
            {
                Employee emp = new Employee();
                emp.st_code = t.st_code;
                emp.st_emp_id = t.st_emp_id;
                emp.st_emp_password = t.st_emp_password;
                emp.st_emp_member = t.st_emp_member;
                emp.st_emp_birth = t.st_emp_birth;
                emp.st_emp_zip = t.st_emp_zip;
                emp.st_emp_addr2 = t.st_emp_addr2;
                emp.st_emp_sex = t.st_emp_sex;
                emp.st_emp_code = t.st_emp_code;
                emp.st_emp_picture_path = t.st_emp_picture_path;
                emp.st_emp_addr1 = t.st_emp_addr1;

                string sql = "insert into " + table + "(st_code,st_emp_id,st_emp_password,st_emp_member,st_emp_birth,st_emp_zip,st_emp_addr2,st_emp_sex,st_emp_code,st_emp_picture_Path,st_emp_addr1)" + "values (N'" + emp.st_code + "', N'" + emp.st_emp_id + "', N'" + emp.st_emp_password + "', N'" + emp.st_emp_member + "', N'" + emp.st_emp_birth + "', N'" + emp.st_emp_zip + "', N'" +
                    emp.st_emp_addr2 + "', N'" + emp.st_emp_sex + "', N'" + emp.st_emp_code + "' ,N'" + emp.st_emp_picture_path+ "',N'" + emp.st_emp_addr1+ "')";
                //string sql = "insert into " + table + "(st_code,st_emp_member,st_emp_birth,st_emp_zip,st_emp_addr2,st_emp_sex,st_emp_code)" + "values ('" + emp.st_code + "', '" + emp.st_emp_member + "', '" + emp.st_emp_birth + "', '" + emp.st_emp_zip + "', '" +
                //   emp.st_emp_addr2 + "', '" + emp.st_emp_sex + "', '" + emp.st_emp_code + "')";


                return sql;
            }

            //-----------------------테이블에서 모든 것을 가져옴--------------------------//
            public string All_select_qury(string table, employee t)
            {

                employee emp = new employee();
                emp.st_code = t.st_code;
                emp.st_emp_member = t.st_emp_member;
                emp.st_emp_birth = t.st_emp_birth;
                emp.st_emp_zip = t.st_emp_zip;
                emp.st_emp_addr2 = t.st_emp_addr2;
                emp.st_emp_sex = t.st_emp_sex;
                emp.st_emp_code = t.st_emp_code;
                emp.st_emp_picture_Path = t.st_emp_picture_Path;
                emp.st_emp_addr1 = t.st_emp_addr1;
                string sql = $"select * from " + table;

                return sql;
            }

            //----------------------------테이블에서 선택하여 가져옴(안 썼음)----------------------//
            public string select_qury(string table, employee t)
            {
                employee emp = new employee();
                emp.st_code = t.st_code;
                emp.st_emp_member = t.st_emp_member;
                emp.st_emp_birth = t.st_emp_birth;
                emp.st_emp_zip = t.st_emp_zip;
                emp.st_emp_addr2 = t.st_emp_addr1;
                emp.st_emp_sex = t.st_emp_sex;
                emp.st_emp_code = t.st_emp_code;
                emp.st_emp_picture_Path = t.st_emp_picture_Path;
                emp.st_emp_addr1 = t.st_emp_addr1;
                string sql = "select N'" + emp.st_code + "', N'" + emp.st_emp_member + "', N'" + emp.st_emp_birth + "', N'" + emp.st_emp_zip + "', N'" + emp.st_emp_member + "', N'"+
                    emp.st_emp_addr2 + "', N'" + emp.st_emp_zip + " from " + table;


                return sql;
            }
            //-----------------------테이블에서 조건에 맞는 테이블 가져옴(안 썻음)----------------------//
            public string target_select_qury(string table, employee t, string where)
            {
                employee emp = new employee();
                emp.st_code = t.st_code;
                emp.st_emp_member = t.st_emp_member;
                emp.st_emp_birth = t.st_emp_birth;
                emp.st_emp_zip = t.st_emp_zip;
                emp.st_emp_addr2 = t.st_emp_addr2;
                emp.st_emp_sex = t.st_emp_sex;
                emp.st_emp_code = t.st_emp_code;
                emp.st_emp_picture_Path = t.st_emp_picture_Path;
                emp.st_emp_addr1 = t.st_emp_addr1;
                string sql = $"select * from {table} where {where}";

                return sql;


            }


            //-----------------------------업데이트(안씀)-------------------------------// 
            public string update_qury(string table, employee t)
            {
                employee emp = new employee();
                emp.st_code = t.st_code;
                emp.st_emp_member = t.st_emp_member;
                emp.st_emp_birth = t.st_emp_birth;
                emp.st_emp_zip = t.st_emp_zip;
                emp.st_emp_addr2 = t.st_emp_addr2;
                emp.st_emp_sex = t.st_emp_sex;
                emp.st_emp_code = t.st_emp_code;
                emp.st_emp_picture_Path = t.st_emp_picture_Path;
                emp.st_emp_addr1 = t.st_emp_addr1;
                string sql = $"UPDATE {table} set  st_code= {emp.st_code},st_emp_member= {emp.st_emp_member},st_emp_birth= {emp.st_emp_birth},st_emp_zip= {emp.st_emp_zip},st_emp_addr1= {emp.st_emp_addr2},st_emp_sex= {emp.st_emp_sex}, st_emp_addr1={emp.st_emp_addr1}";

                return sql;


            }

            //-------------------------------------------삭제-------------------------------------//
            public string delete_qury(string table, employee t)
            {
                employee emp = new employee();
                emp.st_code = t.st_code;
                emp.st_emp_member = t.st_emp_member;
                emp.st_emp_birth = t.st_emp_birth;
                emp.st_emp_zip = t.st_emp_zip;
                emp.st_emp_addr2 = t.st_emp_addr2;
                emp.st_emp_sex = t.st_emp_sex;
                emp.st_emp_code = t.st_emp_code;
                emp.st_emp_picture_Path = t.st_emp_picture_Path;
                emp.st_emp_addr1 = t.st_emp_addr1;
                string sql = $"delete from {table} where  st_code= {emp.st_code},st_emp_member= {emp.st_emp_member},st_emp_birth= {emp.st_emp_birth},st_emp_zip= {emp.st_emp_zip},st_emp_addr1= {emp.st_emp_addr2},st_emp_sex= {emp.st_emp_sex} ,st_emp_addr1={emp.st_emp_addr1}";

                return sql;

            }

            //---------------------------테이블의 특정컬럼 값에서 중복 제거하고 가져옴------------------//
            public string distinct__ALL_select_qury(string table, string column)
            {
                string query = "SELECT DISTINCT  N'" + column + " FROM  " + table;

                return query;


            }

            //----------------테이블의 특정 컬럼과 값으 비교하여 오름차순으로 정렬하여 가져옴----------//
            public string SelectAllFrom_Where_DESC_(string table, string column, object[] values)
            {
                string query = "SELECT * FROM " + table + " WHERE ";
                for (int i = 0; i < values.Count(); i++)
                {
                    if (values.Count() == 1)
                    {
                        query += column + "=N'" + values[0] + "'" + "ORDER BY " + column + " DESC";
                        return query;
                    }

                    else
                    {
                        query += columnsMember[i] + "='" + values[i] + "' AND ";
                    }
                }
                query.Substring(0, query.Length - 5);
                Console.WriteLine(query);
                return query;
            }

            

        }







        //-----------------------------------DB 연결-----------------------------------------//
        public class DBClass
        {
            public MySqlConnection myConnection = null;
            public bool Dbopened = false;
            public string DBConnectionString = "";
            //WebClass kyj = null;

            public DBClass()
            {
                //
                // TODO: 여기에 생성자 논리를 추가합니다.
                //
            }
            /*
            public DBClass(WebClass ClsInst)
            {
                //
                // TODO: 여기에 생성자 논리를 추가합니다.
                //
                kyj = ClsInst;
            }
            */

            //------------------------연결---------------------------//
            #region Connection Open                                                                    
            public void ConnectionOpen()
            {
                //string strConn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\2klab\Desktop\employeeManege\temp1.mdf;Integrated Security=True;Connect Timeout=30";                       //추가하기
                string strConn = UserControl1.strCon;
                if (Dbopened) return;
                /* DBConnectionString = "server=kr.biareports.com; Database=bia ; PWD=Dkrjqkdn*; UID=sa;"; */// 이곳에 등록이 되어있는 서버들은 필히 서울대 분당서버에 등록이 되어있어야 한다.
                DBConnectionString = strConn;
                myConnection = new MySqlConnection(DBConnectionString);
                myConnection.Open();
                Dbopened = true;
            }
            #endregion

            //---------------------------끊음---------------------------//
            #region Connection Close                                                                   
            public void ConnectionClose()
            {

                if (!Dbopened) return;
                if (myConnection.State == System.Data.ConnectionState.Open) myConnection.Close();
                Dbopened = false;

            }
            #endregion


            #region DataFill                                                                           
            public System.Data.DataTable DataFill(string sQuery)
            {
                if (!Dbopened) ConnectionOpen();

                //string ssQuery = sQuery.Replace("\t", " ");
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(sQuery, myConnection);
                System.Data.DataSet ds = new System.Data.DataSet();
                myAdapter.Fill(ds);
                return ds.Tables[0];
            }
            #endregion

            public System.Data.DataSet DataSetGrid(string sQuery)
            {
                if (!Dbopened) ConnectionOpen();
                //string ssQuery = sQuery.Replace("\t", " ");
               MySqlDataAdapter myAdapter = new MySqlDataAdapter(sQuery, myConnection);
                System.Data.DataSet ds = new System.Data.DataSet();
                myAdapter.Fill(ds, "TablePoint");
                return ds;
            }

            #region int DataNon Query                                                                  
            public int DataNonQuery(string sQuery)
            {
                if (!Dbopened) ConnectionOpen();
                //string ssQuery = sQuery.Replace("\t", " ");
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(sQuery, myConnection);
                int ret = myAdapter.SelectCommand.ExecuteNonQuery();

                return ret;
            }
            #endregion



            public object DataScalar(string sQuery)
            {
                if (!Dbopened) ConnectionOpen();
                if (myConnection.State == System.Data.ConnectionState.Closed) ConnectionOpen();
                string ssQuery = sQuery.Replace("\t", " ");
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = new MySqlCommand(ssQuery, myConnection);

                object ret = null;
                try
                {
                    ret = myAdapter.SelectCommand.ExecuteScalar();
                }
                catch (Exception e1)
                {
                    //kyj.page.Response.Write("myConnection.State=" + myConnection.State.ToString() + "<br><br>");
                    //kyj.page.Response.Write(e1.ToString() );
                    //kyj.page.Response.End();
                }

                return ret;
            }

            //------------------------입력할 때 사용함------------------------//
            public int DataInsert(string sQury)
            {
                if (!Dbopened) ConnectionOpen();

                MySqlCommand cmd = new MySqlCommand(sQury, myConnection);
                int result = cmd.ExecuteNonQuery();

                return result;


            }



        }


        //--------------------------------상단 매장명 콤보박스 값 선택 이벤트--------------------//
        public void combo_storeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Top_combo_vlaue = (string)comboBox.SelectedItem.ToString();
        }

        //-----------------------폼 로드시 그리드뷰 초기화----------------------//
        public void DataGrid_Initial()
        {
            string[] row0 = { "", "", "", "", "", "", "" };
            string[] row1 = { "", "", "", "", "", "", "" };
            string[] row2 = { "", "", "", "", "", "", "" };
            string[] row3 = { "", "", "", "", "", "", "" };

            dataGridView1.Rows.Add(row0);
            dataGridView1.Rows.Add(row1);
            dataGridView1.Rows.Add(row2);
            dataGridView1.Rows.Add(row3);
        }

        /// <summary>
        /// <para>기능 :  직원 선택시 상세내역 조회</para>
        /// <para>진입 :  클릭된 셀위치가 0번째 부터 데이터가 존재하는 위치까지 이면서 확인버튼이나 검색버튼이 눌렀을때 실행</para>
        /// <para>분기 :  1. 주소와 생년월일이 일치하지 않는 경우</para>
        /// <para>분기 :  2. 성별 값이 1일 경우</para>
        /// <para>분기 :  3. 사진 경로가 &quot;&quot;가 아닌 경우</para>
        /// <para>분기 :  4. 월별 스위치문</para>
        /// <para>로직 :  1. DB에서 이름이 같은 데이터만 가져옴</para>
        /// <para>로직 :  2. if 주소와 생년월일이 일치하지 않는 경우</para>
        /// <para>로직 :  ----3. 해당 행 삭제</para>
        /// <para>로직 :  4. 불러온 데이터 컨트롤에 할당</para>
        /// <para>로직 :  5. if 성별이 남자</para>
        /// <para>로직 :  ----6. 남자 radio 체크</para>
        /// <para>로직 :  7. if 사진경로 존재</para>
        /// <para>로직 :  ----8. 예외처리</para>
        /// <para>로직 :  ----9. 픽쳐박스에 이미지  할당</para>
        /// <para>로직 :  10. 월별, 일별 실적테이블 초기화</para>
        /// <para>로직 :  11. query와 DB.DatasetGrid를 통해 실적테이블 얻음</para>
        /// <para>로직 :  12. 길이 12의 월 배열 생성</para>
        /// <para>로직 :  13. switch 월</para>
        /// <para>로직 :  ----14. 각 월에 해당할 경우 +1</para>
        /// <para>로직 :  15. 배열을 테이블에 할당</para>
        /// <para>로직 :  16. 31길이 일 배열 생성</para>
        /// <para>로직 :  16. 배열에 각 일 데이터 할당</para>
        /// <para>로직 :  17. 배열을 테이블에 할당</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //--------------------------------첫번째 데이터 그리드뷰 셀 클릭 이벤트------------------------//
        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 클릭된 셀위치가 0번째 부터 데이터가 존재하는 위치까지 이면서 확인버튼이나 검색버튼이 눌렀을때 실행
            if (((e.RowIndex>=0 && e.ColumnIndex>=0)&&(e.RowIndex < dataGridView1.Rows.Count - 1)) && (show2 == 1 || show >= 1))
            {
               
                pictureBox.Image = null;
                pictureBox.Update();

                sqlQuery = new SqlQuery();
                DB = new DBClass();
                //string[] member = { dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() };
                //string addr = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                //string birth = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

                click_member = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                click_addr = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                click_birth = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();


                string[] Member = { click_member };
                //DB 에서 이름이 같은 데이터만 가져옴
                string DBstr = sqlQuery.SelectAllFrom_Where_("st_employee", new string[] { "st_emp_member" }, Member);
                DataSet ds = DB.DataSetGrid(DBstr);
                DataTable dt = ds.Tables[0];
                DataTable dt2 = ds.Tables[0];

                //주소 같은거만 가져옴
                foreach (DataRow dr in dt.Rows)
                {


                    if (dr["st_emp_addr2"].ToString() != click_addr && dr["st_emp_birth"].ToString() != click_birth)

                    {
                        dr.Delete();
                    }
                    else
                    {

                    }
                }
                dt.AcceptChanges();


                //DataRow dw = dt.Rows[0];

                //ComboBox combo_store2 = new ComboBox();

                combo_store2.Text = Top_combo_vlaue;
                txt_name.Text = dt.Rows[0]["st_emp_member"].ToString();
                txt_birth.Text = dt.Rows[0]["st_emp_birth"].ToString();
                txt_address.Text = dt.Rows[0]["st_emp_addr2"].ToString();
                txt_DORO.Text = dt.Rows[0]["st_emp_addr1"].ToString();
                txt_zipCode.Text = dt.Rows[0]["st_emp_zip"].ToString();

                if (Convert.ToInt32(dt.Rows[0]["st_emp_sex"]) == 1)
                {
                    btn_man.Checked = true;
                }
                else
                {
                    btn_woman.Checked = true;
                }
                if (dt.Rows[0]["st_emp_picture_Path"].ToString() != "")
                {
                    try
                    {
                        pictureBox.Image = Bitmap.FromFile(dt.Rows[0]["st_emp_picture_Path"].ToString());
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    
                }

                //-------------------위에 까지는 셀클릭시 행동-----------------------------//




                //---------------------------------------------아래부터는 탭에 실적 표시----------------------//
                dataGridView2.Rows.Clear();          //월별 실적 테이블 초기화
                dataGridView3.Rows.Clear();         //일별 실적 테이블 초기화


                string[] memberCode = { dt2.Rows[0]["st_emp_code"].ToString() };

                string DBstr2 = sqlQuery.SelectAllFrom_Where_("st_emp_result", new string[] { "st_emp_code" }, memberCode);
                //string DBstr2 = qury.Select_month("st_empresult", "st_emp_code", memberCode, "result_open_date");

                DataSet ds3 = DB.DataSetGrid(DBstr2);
                DataTable dt3 = ds3.Tables[0];



                //월 실적수 표시
                int[] monthCount = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                foreach (DataRow dr in dt3.Rows)
                {
                    DateTime date = DateTime.Parse(dr["result_open_date"].ToString());
                    int month = date.Month;

                    switch (month)
                    {
                        case 1:

                            monthCount[0] += 1;
                            break;

                        case 2:
                            monthCount[1] += 1;
                            break;


                        case 3:
                            monthCount[2] += 1;
                            break;


                        case 4:
                            monthCount[3] += 1;
                            break;

                        case 5:
                            monthCount[4] += 1;
                            break;


                        case 6:
                            monthCount[5] += 1;
                            break;


                        case 7:
                            monthCount[6] += 1;
                            break;


                        case 8:
                            monthCount[7] += 1;
                            break;


                        case 9:
                            monthCount[8] += 1;
                            break;


                        case 10:
                            monthCount[9] += 1;
                            break;


                        case 11:
                            monthCount[10] += 1;
                            break;


                        case 12:
                            monthCount[11] += 1;
                            break;


                    }


                }
                string[] strmonth = new string[] { "", "", "", "", "", "", "", "", "", "", "", "" };

                for (int i = 0; i < monthCount.Length; i++)
                {
                    strmonth[i] = monthCount[i].ToString();
                }

                object[] row = new object[] { strmonth };
                foreach (string[] arr in row)
                {
                    dataGridView2.Rows.Add(arr);
                }

                //---------------------일별 실적수 표시--------------------------
                int[] dayCount = new int[31];

                foreach (DataRow dr in dt3.Rows)
                {
                    DateTime date = DateTime.Parse(dr["result_open_date"].ToString());
                    int day = date.Day;

                    dayCount[(day - 1)] += 1;


                }

                string[] strday = new string[31];

                for (int i = 0; i < dayCount.Length; i++)
                {
                    strday[i] = dayCount[i].ToString();
                }

                object[] col = new object[] { strday };
                foreach (string[] arr in col)
                {
                    dataGridView3.Rows.Add(arr);
                }
            }
        }

        /// <summary>
        /// <para>기능 :  사진 추가</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  1. 디렉토리 존재하지 않음</para>
        /// <para>분기 :  2. Dialog OK클릭</para>
        /// <para>로직 :  1. 패스 설정 및 디렉토리 생성</para>
        /// <para>로직 :  2. if di.Exists == false</para>
        /// <para>로직 :  ----3. 디렉토리 생성</para>
        /// <para>로직 :  4. 디렉토리 생성</para>
        /// <para>로직 :  5. 파일오픈 Dialog</para>
        /// <para>로직 :  6.if  Dialog OK</para>
        /// <para>로직 :  ----7.filepath저장</para>
        /// <para>로직 :  ----8.picture박스 이미지 할당</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //--------------------------사진 추가 버튼 클릭----------------------//
        internal void btn_picture_Add_Click_1(object sender, EventArgs e)
        {

            //사진 저장

            //프로그램 실행위치 emp폴더 만들기
            string path = System.IO.Directory.GetCurrentDirectory() + @"\emp";
            DirectoryInfo di = new DirectoryInfo(path);
            if (!di.Exists)
            {
                di.Create();
            }

            filepath = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog();
            
            //파일 열기창 초기 위치
            dialog.InitialDirectory = @"C:\";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filepath = dialog.FileName;
                pictureBox.Image = Bitmap.FromFile(filepath);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }

           



        }

        /// <summary>
        /// <para>기능 :  우편번호 검색 버튼</para>
        /// <para>진입 :  </para>
        /// <para>분기 :  1. 버튼 텍스트 == &quot;검색&quot;</para>
        /// <para>로직 :  1. if 버튼 텍스트 == &quot;검색&quot;</para>
        /// <para>로직 :  ----2. 주소검색 패널 보이게</para>
        /// <para>로직 :  ----3. 버튼 텍스트 &quot;닫기&quot;</para>
        /// <para>로직 :  ----return</para>
        /// <para>로직 :  4. 주소검색 패널 안보이게</para>
        /// <para>로직 :  5. 버튼 텍스트 &quot;검색&quot;</para>
        /// <para>로직 :  6. clearPanelSelectDoro</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //우편번호 검색 버튼
        public void btn_research_Click(object sender, EventArgs e)
        {

            if (btn_research.Text == "검색")
            {
                panel_addrResearch.Visible = true;
                btn_research.Text = "닫기";
                return;
            }
            else
            {
                panel_addrResearch.Visible = false;
                btn_research.Text = "검색";
                clearPanelSelectDoro();
            }
        }

        //-------------우편번호 검색 패널 초기화--------------//
        public void clearPanelSelectDoro()
        {
           dataGridView_Zip_addr.DataSource = null;
            panel_addrResearch.Visible = false;
            txt_ZIP_DORO.Text = "";
        }

        //----------------------월 실적 탭 그리드뷰-----------------------// 
        public void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView3.Rows.Clear();

            sqlQuery = new SqlQuery();
            DB = new DBClass();
            //클릭된 달
            int text_month = (e.ColumnIndex +1);


            string[] Member = { click_member };

            //DB 에서 이름이 같은 데이터만 가져옴
            string DBstr = sqlQuery.SelectAllFrom_Where_("st_employee", new string[] { "st_emp_member" },Member);
            DataSet ds = DB.DataSetGrid(DBstr);
            DataTable dt = ds.Tables[0];
            DataTable dt2 = ds.Tables[0];



            //선택된 직원의 직원코드로 실적 조회
            string[] memberCode = { dt2.Rows[0]["st_emp_code"].ToString() };

            string DBstr2 = sqlQuery.SelectAllFrom_Where_("st_emp_result", new string[] { "st_emp_code" }, memberCode);
            //string DBstr2 = qury.Select_month("st_empresult", "st_emp_code", memberCode, "result_open_date");

            DataSet ds3 = DB.DataSetGrid(DBstr2);
            DataTable dt3 = ds3.Tables[0];

            //---------------------일별 실적수 표시--------------------------
            int[] dayCount = new int[31];

            foreach (DataRow dr in dt3.Rows)
            {
                DateTime date = DateTime.Parse(dr["result_open_date"].ToString());
                int month = date.Month;

                if (text_month == month)
                {
                    int day = date.Day;

                    dayCount[(day - 1)] += 1;
                }

            }

            string[] strday = new string[31];

            for (int i = 0; i < dayCount.Length; i++)
            {
                strday[i] = dayCount[i].ToString();
            }

            object[] col = new object[] { strday };
            foreach (string[] arr in col)
            {
                dataGridView3.Rows.Add(arr);
            }

        
        }


        //----------------------우편번호 검색 버튼---------------------//
        public void btn_ZIP_DORO_Click(object sender, EventArgs e)
        {
            DB = new DBClass();
            crud = new Crud(strCon);
         
            if (txt_ZIP_DORO.Text != "")
            {
                string SelectDro = $"SELECT SIDO, SIGUNGU, DORO, ZIP_NO FROM ZIPCODE WHERE DORO LIKE '%{txt_ZIP_DORO.Text}%'";
                DataSet dtDoro = DB.DataSetGrid(SelectDro);

                dtDoro.Tables[0].Columns["SIDO"].ColumnName = "시도";
                dtDoro.Tables[0].Columns["SIGUNGU"].ColumnName = "시군구";
                dtDoro.Tables[0].Columns["DORO"].ColumnName = "도로명";
                dtDoro.Tables[0].Columns["ZIP_NO"].ColumnName = "우편번호";


             
                dataGridView_Zip_addr.DataSource = dtDoro.Tables[0];

                

            }
        }

        //-----------------------우편번호,도로명주소 그리드뷰------------------//
        public void dataGridView_Zip_addr_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //DataGridView datagridview = (sender as DataGridView);
                txt_zipCode.Text = dataGridView_Zip_addr.Rows[e.RowIndex].Cells["우편번호"].Value.ToString();
                txt_DORO.Text = dataGridView_Zip_addr.Rows[e.RowIndex].Cells["시도"].Value.ToString() +
                    dataGridView_Zip_addr.Rows[e.RowIndex].Cells["시군구"].Value.ToString() +
                    dataGridView_Zip_addr.Rows[e.RowIndex].Cells["도로명"].Value.ToString();
                clearPanelSelectDoro();
                btn_research.Text = "검색";
            }
        }

        public void button4_Click(object sender, EventArgs e)
        {
            clearPanelSelectDoro();
            btn_research.Text = "검색";
        }
    }
}