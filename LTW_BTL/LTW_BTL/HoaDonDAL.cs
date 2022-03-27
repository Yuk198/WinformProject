using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTW_BTL
{
    class HoaDonDAL
    {
        string ketnoi = "Data Source=SLEEPWALKER0416\\MSSQLSERVER01; " +
                        " Initial Catalog=QLCUAHANG; " +
                        " Integrated Security=True";
        SqlConnection con = null;
        SqlDataAdapter da;
        SqlCommand cmd;
        public HoaDonDAL()
        {
            con = new SqlConnection(ketnoi);
            con.Open();
        }
        public DataTable getALLHoaDon()
        {
            string sql = "SELECT *FROM tblHoaDon";
            con = new SqlConnection(ketnoi);
            da = new SqlDataAdapter(sql, con);
            con.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public bool InsertHoaDon(tblHoaDon hoadon)
        {
            try
            {
                string sql = "INSERT INTO tblHoaDon(MaHD, TenKH, GioiTinh, TongTien, DiaChi, NgayBan) VALUES (@MaHD, @TenKH, @GioiTinh, @TongTien, @DiaChi, @NgayBan)"; //Thêm kiểu tham số @
                con = new SqlConnection(ketnoi);
                cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.Parameters.Add("@MaHD", SqlDbType.NVarChar).Value = hoadon.MaHD;
                cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar).Value = hoadon.TenKH;
                cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar).Value = hoadon.GioiTinh;
                cmd.Parameters.Add("@TongTien", SqlDbType.Int).Value = hoadon.TongTien;
                cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = hoadon.DiaChi;
                cmd.Parameters.Add("@NgayBan", SqlDbType.NVarChar).Value = hoadon.NgayBan;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public bool UpdateHoaDon(tblHoaDon hoadon)
        {
            try
            {
                string sql = "UPDATE tblHoaDon SET  MaHD= @MaHD, TenKH=@TenKH, GioiTinh=@GioiTinh, TongTien=@TongTien, DiaChi= @DiaChi, NgayBan=@NgayBan WHERE MaHD=@MaHD";
                con = new SqlConnection(ketnoi);
                cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.Parameters.Add("@MaHD", SqlDbType.NVarChar).Value = hoadon.MaHD;
                cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar).Value = hoadon.TenKH;
                cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar).Value = hoadon.GioiTinh;
                cmd.Parameters.Add("@TongTien", SqlDbType.Int).Value = hoadon.TongTien;
                cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = hoadon.DiaChi;
                cmd.Parameters.Add("@NgayBan", SqlDbType.NVarChar).Value = hoadon.NgayBan;
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public bool DeleteHoaDon(tblHoaDon hoadon)
        {
            try
            {
                string sql = "DELETE tblHoaDon WHERE MaHD=@MaHD";
                con = new SqlConnection(ketnoi);
                cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.Parameters.Add("@MaHD", SqlDbType.NVarChar).Value = hoadon.MaHD;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }


    }
}