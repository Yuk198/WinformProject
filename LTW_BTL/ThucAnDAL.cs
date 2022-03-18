using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTW_BTL
{
    class ThucAnDAL
    {
        string ketnoi = "Data Source=SLEEPWALKER0416\\MSSQLSERVER01; " +
                        " Initial Catalog=QLCUAHANG; " +
                        " Integrated Security=True";
        SqlConnection con = null;
        SqlDataAdapter da;
        SqlCommand cmd;
        public ThucAnDAL()
        {
            con = new SqlConnection(ketnoi);
            con.Open();
        }
        public DataTable getALLThucAn()
        {
            //B1: Tạo câu lệnh sql để lấy toàn bộ thức ăn
            string sql = "SELECT *FROM tblThucAn";
            //B2: Tạo 1 kết nối đến SQL
            con = new SqlConnection(ketnoi);
            //B3: Khởi tạo 1 đối tượng của lớp DataAdapter
            da = new SqlDataAdapter(sql, con);
            //B4: Mở kết nối 
            con.Open();
            //B5: Đổ giữ liệu từ DataAdapter vào DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            //B6: Đóng kết nối
            con.Close();
            return dt;
        }
        //Phương thức thêm thức ăn
        public bool InsertThucAn(tblThucAn thucan)
        {
            try
            {
                string sql = "INSERT INTO tblThucAn(MaMon, TenMon, SoLuong, DonGia) VALUES (@MaMon, @TenMon, @SoLuong, @DonGia)"; //Thêm kiểu tham số @
                con = new SqlConnection(ketnoi);
                cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.Parameters.Add("@MaMon", SqlDbType.NVarChar).Value = thucan.MaMon;
                cmd.Parameters.Add("@TenMon", SqlDbType.NVarChar).Value = thucan.TenMon;
                cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = thucan.SoLuong;
                cmd.Parameters.Add("@DonGia", SqlDbType.Int).Value = thucan.DonGia;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
        public bool UpdateThucAn(tblThucAn thucan)
        {
            try
            {
                string sql = "UPDATE tblThucAn SET  MaMon = @MaMon, TenMon=@TenMon, SoLuong=@Soluong, DonGia= @DonGia WHERE MaMon=@MaMon";
                con = new SqlConnection(ketnoi);
                cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.Parameters.Add("@MaMon", SqlDbType.NVarChar).Value = thucan.MaMon;
                cmd.Parameters.Add("@TenMon", SqlDbType.NVarChar).Value = thucan.TenMon;
                cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = thucan.SoLuong;
                cmd.Parameters.Add("@DonGia", SqlDbType.Int).Value = thucan.DonGia;
                cmd.ExecuteNonQuery();
                con.Close();
                
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
        public bool DeleteThucAn(tblThucAn thucan)
        {
            try
            {
                string sql = "DELETE tblThucAn WHERE MaMon=@MaMon";
                con = new SqlConnection(ketnoi);
                cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.Parameters.Add("@MaMon", SqlDbType.NVarChar).Value = thucan.MaMon;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception e)
            {
                    return false;
             }
            return true;
        }

        
    }
}
