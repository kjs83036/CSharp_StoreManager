using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManage
{

    #region st_table--------------------------------------------------
    public class St_table
    {
        //매장코드
        public string St_code { set; get; }
        //매장이름
        public string St_name { set; get; }
        //매장관리자
        public string St_manager { set; get; }
        //매장주소1
        public string St_addr1 { set; get; }
        public string St_addr2 { set; get; }
        //매장주소2
        //매장 우편번호
        public string St_zip { set; get; }
        //계약일자
        public DateTime St_contractdate { set; get; }
        public DateTime St_contractdate2 { set; get; }
        //매장 계약정보1
        public string St_info1 { set; get; }
        //매장 일반정보
        public string St_info2 { set; get; }

        public DateTime St_date { set; get; }
        public DateTime St_maint_date { get; set; }
        public DateTime St_ele_date { get; set; }
        public DateTime St_sudo_date { get; set; }
        //등록일
        
    }
    #endregion
    # region st_expense-----------------------------------------------
    public class Expense
    {
        //매장코드
        public string st_code { set; get; }
        //등록일
        public DateTime st_exp_date { set; get; }
        //등록자, 직원코드
        public string st_member_code { set; get; }
        //지출명
        public string st_exp_name { set; get; }
        //일반지출
        public string st_exp_basic { set; get; }
        //쿠폰지출
        public string st_exp_coupon { set; get; }
        //광고
        public string st_exp_ad { set; get; }
        //액세서리
        public string st_exp_acce { set; get; }
        //퀵
        public string st_exp_quick { set; get; }
        //탁송
        public string st_exp_cons { set; get; }
        //용품(아티클)
        public string st_exp_arti { set; get; }

    }
    #endregion

    #region st_employee---------------------------------------------
    public class Employee
    {
        //직원코드
        public string st_emp_code { set; get; }
        //매장코드, 소속매장
        public string st_code { set; get; }
        //직원 레벨
        public int st_emp_level { set; get; }
        //이름, 직원명
        public string st_emp_member { set; get; }
        // 성별 1남 2여
        public SByte st_emp_sex { set; get; }
        //생년월일
        public string st_emp_birth { set; get; }
        //주소1
        public string st_emp_addr1 { set; get; }
        //주소2
        public string st_emp_addr2 { set; get; }
        //우편번호
        public string st_emp_zip { set; get; }
        //직원년차, 수기작성
        public string st_emp_Year { set; get; }
        //근무시간, 수기작성
        public string st_emp_time { set; get; }
        //등록일
        public DateTime st_emp_date { set; get; }
        //상태, 출근상태
        public string st_emp_state { set; get; }
        //비고
        public string st_emp_etc { set; get; }


    }
    #endregion
    #region user_t---------------------------------------------
    public class User_t
    {
        //직원코드
        public string st_emp_code { set; get; }
        //매장코드
        public string st_code { set; get; }
        //고객이름
        public string user_name { set; get; }
        //생년월일
        public string user_birth { set; get; }
        //성별 1남 2여
        public SByte user_sex { set; get; }
        //주소1
        public string user_addr1 { set; get; }
        //주소2
        public string user_addr2 { set; get; }
        //우편번호
        public string user_zip { set; get; }
        //휴대폰번호
        public string user_phon_number { set; get; }
        //요금제
        public DateTime user_price_plan { set; get; }
        //요금제변경일
        public string user_plan_date { set; get; }
        //은행정보
        public string user_bank { set; get; }
        //계좌번호
        public string user_bank_number { set; get; }
        //예금주
        public string bank_userName { set; get; }
    }
    #endregion
    #region user_h-------------------------------------
    public class User_h
    {
        //고객이름
        public string h_user_name { set; get; }
        //고객전화번호
        public string h_user_phone_number { set; get; }
        //등록일
        public DateTime h_date { set; get; }
        //메모사항
        public string h_memo { set; get; }
        //등록자
        public string st_emp_code { set; get; }

    }
    #endregion

    #region st_emp_result---------------------------------------------

    public class St_emp_result
    {
        //직원코드
        public string st_emp_code { set; get; }
        //매장코드
        public string st_code { set; get; }
        //등록일
        public DateTime result_date { set; get; }
        //개통일
        public DateTime result_open_date { set; get; }
        //모바일 개통시간
        public DateTime result_open_m_date { set; get; }
        //적용정책
        public string result_policy { set; get; }
        //대리점 코드
        public string agent_code { set; get; }
        //모델명
        public string result_ph_m { set; get; }
        //일련번호
        public string result_serial_number { set; get; }
        //고객코드
        public string user_code { set; get; }
        //개통번호
        public string user_open_number { set; get; }
        //약정유형
        public string user_agree_type { set; get; }
        //개통유형
        public string user_open_type { set; get; }
        //이전통신사
        public string user_previous_com { set; get; }
        //구매형태 현, 카 , 할
        public string user_pur_type { set; get; }
        // 출고가
        public string release_price { set; get; }
        //공시지원금
        public string subsidy_price { set; get; }
        //할부원금
        public string installment_price { set; get; }
        //선납금
        public string prepayments { set; get; }
        //mnp수수료
        public string mnp_commission { set; get; }
        //요금제
        public string price_plan { set; get; }
        //net 리베이트
        public string net_rebate { set; get; }
        //정책 추가
        public string add_policy { set; get; }
        //구두추가
        public string sound_policy { set; get; }
        //부가서비스/차감
        public string addition_service { set; get; }
        //보험가입유무
        public string insurance_YN { set; get; }
        //유심후납YN
        public string late_payment_usim { set; get; }
        //유심선납
        public string prepayments_usim { set; get; }
        //정산금
        public string user_calculate { set; get; }
        //송금금액
        public string send_price { set; get; }
        //예정일
        public DateTime due_date { set; get; }
        //현금
        public string cash { set; get; }
        //카드
        public string card { set; get; }
        //대리점
        public string agent { set; get; }
        //카드판매금액
        public string card_sale_price { set; get; }
        //기타차감
        public string etc_offset { set; get; }
        //세후금액
        public string after_tex_price { set; get; }
        //마진
        public string profit_margin { set; get; }
        //비고
        public string note { set; get; }

    }
    #endregion

    public class Crud
    {
        SqlConnection sqlConn;
        MySqlConnection mySqlConn;
        TempFunc tempFunc;

        public Crud(string connectionString)
        {
            //this.sqlConn = new SqlConnection(connectionString);
            this.mySqlConn = new MySqlConnection(connectionString);
            this.tempFunc = new TempFunc();
        }

        //전체 COLUMN과 VALUE를 받아서 INSERT 
        //T가 프로퍼티를 갖고 있어야함
        public int Create_MS<T>(string query, T t)
        {

            using (SqlCommand sqlCmd = new SqlCommand(query, sqlConn))
            {


                string[] columns = query.Substring(query.IndexOf("@")).Replace(" ", "").Split(',');
                //쿼리에서 마지막)제거
                columns[columns.Count() - 1] = columns[columns.Count() - 1].Substring(0, columns[columns.Count() - 1].Length - 1);

                PropertyInfo[] propertyInfos = tempFunc.GetPropertyInfos(t);

                //get value 시 정보를 가져올 obj와 인덱싱번호 (없으면null)을 파라미터로 지정
                object[] values = tempFunc.setValues(propertyInfos, t);
                tempFunc.setSqlCommad(sqlCmd, columns, values);
                //sqltransition관련 함수, 오류시 transition rollback
                int result = sqlTran(sqlConn, sqlCmd);
                //비정상 리턴 -1, 그외 적용 행수 리턴
                return result;
            }

        }

        public int Create_MySql<T>(string query, T t)
        {

            using (MySqlCommand mySqlCmd = new MySqlCommand(query, mySqlConn))
            {


                string[] columns = query.Substring(query.IndexOf("@")).Replace(" ", "").Split(',');
                //쿼리에서 마지막)제거
                columns[columns.Count() - 1] = columns[columns.Count() - 1].Substring(0, columns[columns.Count() - 1].Length - 1);

                PropertyInfo[] propertyInfos = tempFunc.GetPropertyInfos(t);

                //get value 시 정보를 가져올 obj와 인덱싱번호 (없으면null)을 파라미터로 지정
                object[] values = tempFunc.setValues(propertyInfos, t);
                tempFunc.setSqlCommad(mySqlCmd, columns, values);
                //sqltransition관련 함수, 오류시 transition rollback
                int result = sqlTran(mySqlConn, mySqlCmd);
                //비정상 리턴 -1, 그외 적용 행수 리턴
                return result;
            }

        }

        //T안에 데이터는 필요없음 형태만알면 됨
        //현재는데이터를 *로 얻어오고있음 후에 특정 칼럼만 얻어올 경우 수정 필요
        public T Read_MS<T>(string query, T t)
        {
            Type type = typeof(T);
            PropertyInfo[] propertyInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            T tFromDB = Activator.CreateInstance<T>();
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    while (reader.Read())

                        for (int i = 0; i < propertyInfo.Count(); i++)
                        {
                            //프로퍼티를 활용하여 T객체에 필드 값 할당
                            if (propertyInfo[i].PropertyType.Equals(typeof(int)))
                            {
                                propertyInfo[i].SetValue(tFromDB, Convert.ToInt32(reader.GetValue(i)), null);
                            }
                            else
                            {
                                propertyInfo[i].SetValue(tFromDB, reader.GetValue(i), null);
                            }
                        }
                    reader.Close();
                    sqlConn.Close();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                sqlConn.Close();
            }

            return tFromDB;


        }
        public T Read_MySql<T>(string query, T t)
        {
            Type type = typeof(T);
            PropertyInfo[] propertyInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            T tFromDB = Activator.CreateInstance<T>();
            try
            {
                using (MySqlCommand mySqlCmd = new MySqlCommand(query, mySqlConn))
                {
                    mySqlConn.Open();
                    MySqlDataReader reader = mySqlCmd.ExecuteReader();

                    while (reader.Read())

                        for (int i = 0; i < propertyInfo.Count(); i++)
                        {
                            //프로퍼티를 활용하여 T객체에 필드 값 할당
                            if (propertyInfo[i].PropertyType.Equals(typeof(int)))
                            {
                                propertyInfo[i].SetValue(tFromDB, Convert.ToInt32(reader.GetValue(i)), null);
                            }
                            else
                            {
                                propertyInfo[i].SetValue(tFromDB, reader.GetValue(i), null);
                            }
                        }
                    reader.Close();
                    mySqlConn.Close();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                mySqlConn.Close();

            }

            return tFromDB;


        }

        // 칼럼을 전체가아닌 부분으로 할경우 칼럼들을 파라미터로 받아야함
        public T Read_MS<T>(string query, T t, int[] columnNum)
        {
            Type type = typeof(T);
            PropertyInfo[] propertyInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            T tFromDB = Activator.CreateInstance<T>();
            try
            {

                using (SqlCommand sqlCmd = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();


                    while (reader.Read())

                        for (int i = 0; i < columnNum.Count(); i++)
                        {
                            //프로퍼티를 활용하여 T객체에 필드 값 할당
                            propertyInfo[columnNum[i]].SetValue(tFromDB, reader.GetValue(i), null);
                        }
                    reader.Close();
                    sqlConn.Close();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                sqlConn.Close();
            }

            return tFromDB;


        }

        public T Read_MySql<T>(string query, T t, int[] columnNum)
        {
            Type type = typeof(T);
            PropertyInfo[] propertyInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            T tFromDB = Activator.CreateInstance<T>();
            try
            {
                using (MySqlCommand mySqlCmd = new MySqlCommand(query, mySqlConn))
                {
                    mySqlConn.Open();
                    MySqlDataReader reader = mySqlCmd.ExecuteReader();


                    while (reader.Read())

                        for (int i = 0; i < columnNum.Count(); i++)
                        {
                            //프로퍼티를 활용하여 T객체에 필드 값 할당
                            propertyInfo[columnNum[i]].SetValue(tFromDB, reader.GetValue(i), null);
                        }
                    reader.Close();
                    mySqlConn.Close();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                mySqlConn.Close();
            }

            return tFromDB;


        }


        public DataSet ReadToGrid_MS(string query)
        {

            DataSet dataSet = new DataSet();

            try
            {
                //dataset을 얻기위해 adapter 사용
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConn))
                {
                    sqlConn.Open();


                    adapter.Fill(dataSet);
                    sqlConn.Close();
                    return dataSet;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                sqlConn.Close();
            }

            return dataSet;


        }

        public DataSet ReadToGrid_MySql(string query)
        {

            DataSet dataSet = new DataSet();

            try
            {
                //dataset을 얻기위해 adapter 사용
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, mySqlConn))
                {

                    mySqlConn.Open();
                    adapter.Fill(dataSet);
                    mySqlConn.Close();
                    return dataSet;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                mySqlConn.Close();
            }

            return dataSet;


        }


        public int Update_MS<T>(string query, T t)
        {
            int result = -2;
            using (SqlCommand sqlCmd = new SqlCommand(query, sqlConn))
            {

                PropertyInfo[] propertyInfos = tempFunc.GetPropertyInfos(t);
                string[] columns = tempFunc.setColumns(propertyInfos);
                object[] values = tempFunc.setValues(propertyInfos, t);
                tempFunc.setSqlCommad(sqlCmd, columns, values);
                result = sqlTran(sqlConn, sqlCmd);

            }

            return result;
        }
        public int Update_MySql<T>(string query, T t)
        {
            int result = -2;
            using (MySqlCommand mySqlCmd = new MySqlCommand(query, mySqlConn))
            {

                PropertyInfo[] propertyInfos = tempFunc.GetPropertyInfos(t);
                string[] columns = tempFunc.setColumns(propertyInfos);
                object[] values = tempFunc.setValues(propertyInfos, t);
                tempFunc.setSqlCommad(mySqlCmd, columns, values);
                result = sqlTran(mySqlConn, mySqlCmd);

            }

            return result;
        }

        public int Delete_MS<T>(string query, T t)
        {
            int result = -2;

            using (SqlCommand sqlCmd = new SqlCommand(query, sqlConn))
            {
                PropertyInfo[] propertyInfos = tempFunc.GetPropertyInfos(t);
                string[] columns = tempFunc.setColumns(propertyInfos);
                object[] values = tempFunc.setValues(propertyInfos, t);

                //@컬럼에 변수 할당, delete는 한개의 변수만 할당하므로 setSqlCommand를 쓰지않음
                sqlCmd.Parameters.AddWithValue(columns[0], values[0]);
                result = sqlTran(sqlConn, sqlCmd);

            }

            return result;
        }

        public int Delete_MySql<T>(string query, T t)
        {
            int result = -2;

            using (MySqlCommand mySqlCmd = new MySqlCommand(query, mySqlConn))
            {
                PropertyInfo[] propertyInfos = tempFunc.GetPropertyInfos(t);
                string[] columns = tempFunc.setColumns(propertyInfos);
                object[] values = tempFunc.setValues(propertyInfos, t);

                //@컬럼에 변수 할당, delete는 한개의 변수만 할당하므로 setSqlCommand를 쓰지않음
                mySqlCmd.Parameters.AddWithValue(columns[0], values[0]);
                result = sqlTran(mySqlConn, mySqlCmd);

            }

            return result;
        }

        //Traisition과 sql 실행관련 함수
        public int sqlTran(SqlConnection conn, SqlCommand sqlCmd)
        {
            int result = -2;
            SqlTransaction sqlTran = null;
            try
            {

                conn.Open();
                sqlTran = conn.BeginTransaction();
                sqlCmd.Transaction = sqlTran;

                // 결과는 행수, 기타결과 -1
                result = sqlCmd.ExecuteNonQuery();
                sqlTran.Commit();

                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                sqlTran?.Rollback();
                conn.Close();

            }
            return result;
        }
        public int sqlTran(MySqlConnection conn, MySqlCommand mySqlCmd)
        {
            int result = -2;
            MySqlTransaction mySqlTran = null;
            try
            {

                conn.Open();
                mySqlTran = conn.BeginTransaction();
                mySqlCmd.Transaction = mySqlTran;

                // 결과는 행수, 기타결과 -1
                result = mySqlCmd.ExecuteNonQuery();
                mySqlTran.Commit();

                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                mySqlTran?.Rollback();
                conn.Close();

            }
            return result;
        }





    }

    public class SqlQuery

    {
        TempFunc tempFunc;

        public string[] columnsMember;
        public string[] columnsMemberInformation;
        public string[] table;

        public const int MEMBER = 0;
        public const int MemberInformation = 1;

        public const string MAX = "MAX";
        public const string MIN = "MIN";

        public SqlQuery()
        {
            this.columnsMember = new string[] { "ROWNUM", "ID", "PASSWORD", "NAME", "PHONE", "EMAIL", "POSITION", "RegistrationDate" };
            this.columnsMemberInformation = new string[] { "ROWNUM", "ID", "RegistrationDate", "POINT", "WRITING", "ACTIVITY", "MEMBERID", };
            this.table = new string[] { "MEMBER", "MEMBERInformation" };

            this.tempFunc = new TempFunc();
        }

        //table을 받고 create query return
        public string InsertInto__Values_<T>(T t)
        {
            Type type = typeof(T);

            PropertyInfo[] propertyInfos = tempFunc.GetPropertyInfos(t);
            string[] columns = tempFunc.setColumns(propertyInfos);

            string query = "INSERT INTO " + type.Name + " (";


            for (int i = 0; i < columns.Count(); i++)

            {
                query += columns[i] + ", ";
            }
            query = query.Substring(0, query.Length - 2);

            query += ") VALUES (";

            for (int i = 0; i < columns.Count(); i++)
            {
                query += "@" + columns[i] + ", ";
            }

            query = query.Substring(0, query.Length - 2);
            query += ")";
            Console.WriteLine(query);
            return query;

        }

        //table과 value를 받아서 read query를 리턴
        public string SelectAllFrom_Where_(string table, string[] columns, object[] values)
        {
            string query = "SELECT * FROM " + table + " WHERE ";
            for (int i = 0; i < values.Count(); i++)
            {
                if (values.Count() == 1)
                {
                    query += columns[0] + "=N'" + values[0] + "'";
                    return query;
                }

                else
                {
                    query += columns[i] + "='" + values[i] + "' AND ";
                }
            }
            query.Substring(0, query.Length - 5);
            Console.WriteLine(query);
            return query;
        }

        public string SelectAllFrom_(string table)
        {
            string query = "SELECT * FROM " + table;
            Console.WriteLine(query);
            return query;
        }

        // table, column, whereList받고 query리턴
        public string selectAllFrom_Where_In_<T>(string table, string column, List<T> where_list)
        {
            if (where_list.Count != 0)
            {
                string query = "SELECT * FROM " + table + " WHERE " + column + " in (";

                foreach (var w in where_list)
                {
                    query += "N'" + w + "'" + ", ";
                }
                query = query.Substring(0, query.Length - 2);
                query += ")";
                Console.WriteLine(query);
                return query;
            }
            Console.WriteLine("null");
            return null;
        }

        //table 받고 MaxRownum 을 구하는 query 리턴
        public string selectAllFrom_Where_Select_MINMAX_From_(string table, string minMax, string columnWhere)

        {
            string query = $"SELECT * FROM {table} WHERE {columnWhere}=(SELECT {minMax}({columnWhere}) FROM {table})";
            Console.WriteLine(query);
            return query;
        }

        // 20개씩 읽어오기
        public string selectAllFrom_Where_Between_And_(string table, string column, int before, int after)
        {
            string query = "SELECT * FROM " + table + " WHERE " + column + " BETWEEN " + Convert.ToString(before) + " AND " + Convert.ToString(after);

            Console.WriteLine(query);
            return query;
        }

        public string select_From_Where(string table, string[] columnsResult, string columnWhere, object[] values)
        {
            string query = "SELECT ";

            foreach (var v in columnsResult)
            {
                query += v + ", ";
            }

            query = query.Substring(0, query.Length - 2);

            query += " FROM " + table + " WHERE " + columnWhere + "=N'" + values[0] + "'";
            Console.WriteLine(query);
            return query;
        }

        public string select_From_Where_In(string[] columnsResult, string table, string columnWhere, object[] values)
        {
            string query = "SELECT ";

            foreach (var v in columnsResult)
            {
                query += v + ", ";
            }

            query = query.Substring(0, query.Length - 2);

            query += " FROM " + table + " WHERE " + columnWhere + " IN (";

            foreach (var v in values)
            {
                query += "N'" + v + "'" + ", ";
            }

            query = query.Substring(0, query.Length - 2);

            query += ")";

            Console.WriteLine(query);
            return query;

        }

        public string select_Count_From_Where_GroupBy_(string table, string whereColumn, string value, string column)
        {
            //string query = "SELECT COUNT(*) FROM BARCODE WHERE MEMBERID=1";
            string query = "SELECT " + column + ", COUNT(" + column + ") as 'Count' FROM " + table + " WHERE " + whereColumn + "=N'" + value + "' GROUP BY " + column;

            Console.WriteLine(query);
            return query;

        }

        public string select_Sum__From_Where_GroupBy_(string columnResult, string table, string whereColumn, string value, string columnGroupBy)
        {
            
            string query = $"SELECT {columnResult} FROM {table} WHERE {whereColumn} =N'{value}' GROUP BY {columnGroupBy} ORDER BY {columnGroupBy}";
            Console.WriteLine(query);
            return query;
        }

        public string select_From_(string table, string[] columnResult)
        {
            string query = "SELECT ";
            foreach (var v in columnResult)
            {
                query += $"{v}" + ",";
            }
            query = query.Substring(0, query.Length - 1);
            query += $" FROM {table}";
            Console.WriteLine(query);
            return query;
        }



        //table 받아서 update query를 리턴
        public string Update_Set_<T>(string table, T t, string columnWhere)
        {
            Type type = typeof(T);

            PropertyInfo[] propertyInfos = tempFunc.GetPropertyInfos(t);
            string[] columns = tempFunc.setColumns(propertyInfos);
            //List<string> listTemp = columns.ToList<string>();
            //listTemp.Remove(columnWhere);
            //string[] columnsAfter = listTemp.ToArray();
            //string query = "UPDATE " + table + " SET ";
            //for (int i = 0; i < columnsAfter.Count(); i++)
            //{
            //    query += columnsAfter[i] + "=@" + columnsAfter[i] + ", ";
            //}
            string query = "UPDATE " + table + " SET ";
            for (int i = 0; i < columns.Count(); i++)
            {
                query += columns[i] + "=@" + columns[i] + ", ";
            }
            query = query.Substring(0, query.Length - 2);
            query += $" WHERE {columnWhere}=@{columnWhere}";
            Console.WriteLine(query);
            return query;
        }

        //table 받아서 delete query를 리턴
        public string deleteFrom_Where_(string table, string column)
        {
            string query = $"DELETE FROM {table} WHERE {column}=@{column}";
            Console.WriteLine(query);
            return query;


        }




    }

    public class TempFunc
    {
        public PropertyInfo[] GetPropertyInfos<T>(T t)
        {
            Type type = typeof(T);


            PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            return propertyInfos;
        }

        public object[] setValues<T>(PropertyInfo[] propertyInfos, T t)
        {
            object[] values = new object[propertyInfos.Length];
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                values[i] = propertyInfos[i].GetValue(t, null);
            }
            return values;
        }

        public string[] setColumns(PropertyInfo[] propertyInfos)
        {
            string[] columns = new string[propertyInfos.Length];
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                columns[i] = propertyInfos[i].Name;
            }
            return columns;
        }
        //조건 object.length = values.length
        public void setSqlCommad(SqlCommand sqlCmd, string[] columns, object[] values)
        {
            for (int i = 0; i < columns.Count(); i++)
            {
                //@컬럼에 변수 할당
                sqlCmd.Parameters.AddWithValue(columns[i], values[i]);
            }
        }

        public void setSqlCommad(MySqlCommand sqlCmd, string[] columns, object[] values)
        {
            for (int i = 0; i < columns.Count(); i++)
            {
                //@컬럼에 변수 할당
                sqlCmd.Parameters.AddWithValue(columns[i], values[i]);
            }
        }


    }

    public class DbFactory
    {
        public const string MSDB = "Ms";
        public const string OracleDB = "Oracle";
        public const string MysqlDB = "Mysql";


        Dictionary<string, Tuple<object, object>> dictDB;

        public DbFactory(string connectionString)
        {
            dictDB = new Dictionary<string, Tuple<object, object>>();
            addMssql(connectionString);
            addMysql(connectionString);
            addOracle(connectionString);
        }

        public Tuple<object, object> this[string s]
        {
            get { return dictDB[s]; }
        }

        private void addMysql(string connectionString)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.Connection = conn;
            dictDB.Add(MysqlDB, new Tuple<object, object>(conn, mySqlCommand));
        }
        private void addMssql(string connectionString)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.Connection = conn;
            dictDB.Add(MysqlDB, new Tuple<object, object>(conn, SqlCommand));
        }

        private void addOracle(string connectionString)
        {
            OracleConnection conn = new OracleConnection(connectionString);
            OracleCommand mySqlCommand = new OracleCommand();
            mySqlCommand.Connection = conn;
            dictDB.Add(MysqlDB, new Tuple<object, object>(conn, mySqlCommand));
        }
    }

    public class TempFuncForWinform
    {
        public void setDataGridView(DataSet dataset, DataGridView datagridview)
        {
            datagridview.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(68, 114, 196);
            datagridview.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            datagridview.ColumnHeadersHeight = 30;
            datagridview.EnableHeadersVisualStyles = true;
            datagridview.RowHeadersVisible = false;
            datagridview.DefaultCellStyle.BackColor = Color.FromArgb(207, 213, 234);
            datagridview.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            datagridview.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(233, 235, 245);
            datagridview.DataSource = dataset.Tables[0];
            datagridview.AllowUserToAddRows = false;
            datagridview.AllowUserToDeleteRows = false;
            datagridview.ReadOnly = true;
            datagridview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            datagridview.DefaultCellStyle.SelectionBackColor = Color.FromArgb(160, 170, 190);
            datagridview.MultiSelect = false;
            datagridview.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            datagridview.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            datagridview.Font = new Font("나눔고딕", 10, FontStyle.Regular);
            datagridview.BackgroundColor = Color.GhostWhite;
            datagridview.GridColor = Color.White;
            //datagridview.AutoSize = true;

            //첫줄 선택 없애기
            datagridview.ClearSelection();


        }
    }
}
