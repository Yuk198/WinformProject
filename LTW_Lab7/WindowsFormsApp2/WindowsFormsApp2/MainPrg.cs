using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp2
{
    public partial class MainPrg : Form
    {
        SqlConnection conn = null;
        SqlDataAdapter HangXeadapter = null;
        System.Data.DataTable HangXedt = null;
        SqlDataAdapter KhachHangadapter = null;
        System.Data.DataTable KhachHangdt = null;
        SqlDataAdapter HoaDonadapter = null;
        System.Data.DataTable HoaDondt = null;
        SqlDataAdapter BaoCaoadapter = null;
        System.Data.DataTable BaoCaodt = null;

        public MainPrg()
        {
            InitializeComponent();
        }
        ~MainPrg()
        {
            if (conn != null && conn.State == ConnectionState.Open) { conn.Close(); }
        }
        private void MainPrg_Load(object sender, EventArgs e)
        {
            try
            {
                label1.TabStop = label2.TabStop = label3.TabStop = label4.TabStop = label5.TabStop = label6.TabStop = label7.TabStop = label8.TabStop = label9.TabStop = false;
                #region add data to Hang Xe datagridview
                dataGridView1.TabStop = false;
                string cnt = "Data Source = DESKTOP-KNN7K79; Initial Catalog = QLOTO; Integrated Security = True";
                conn = new SqlConnection(cnt);
                conn.Open();
                string cmd = "Select Id as 'ID', TenHang as 'Ten Hang', DatNuoc as 'Xuat Xu' from HangXe";
                HangXeadapter = new SqlDataAdapter(cmd, conn);
                HangXedt = new System.Data.DataTable();
                HangXeadapter.Fill(HangXedt);
                dataGridView1.DataSource = HangXedt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                #endregion
                #region add data to Khach Hang datagridview
                dataGridView2.TabStop = false;
                string cmd1 = "Select Id as 'ID', TenKH as 'Ten KH', CMT as 'Chung Minh Thu' from KhachHang";
                KhachHangadapter = new SqlDataAdapter(cmd1, conn);
                KhachHangdt = new System.Data.DataTable();
                KhachHangadapter.Fill(KhachHangdt);
                dataGridView2.DataSource = KhachHangdt;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                #endregion
                #region add data to Hoa Don datagridview
                dataGridView3.TabStop = false;
                string cmd2 = "Select hd.Id as 'ID', k.TenKH as 'Ten KH', o.TenXe as 'Ten Xe', h.TenHang as 'Ten Hang', o.GiaXe as 'Gia Xe' from Oto o join KhachHangXe hd on o.MaXe = hd.IdXe join KhachHang k on k.Id = hd.IdKhachHang join HangXe h on h.Id = o.IdHangXe";
                HoaDonadapter = new SqlDataAdapter(cmd2, conn);
                HoaDondt = new System.Data.DataTable();
                HoaDonadapter.Fill(HoaDondt);
                dataGridView3.DataSource = HoaDondt;
                dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                #endregion
                #region add data to Bao Cao datagridview
                string cmd3 = "Select h.TenHang as 'Ten Hang', sum(o.GiaXe) as 'Doanh Thu' from Oto o join KhachHangXe hd on o.MaXe = hd.IdXe join KhachHang k on k.Id = hd.IdKhachHang join HangXe h on h.Id = o.IdHangXe group by h.TenHang";
                BaoCaoadapter = new SqlDataAdapter(cmd3, conn);
                BaoCaodt = new System.Data.DataTable();
                BaoCaoadapter.Fill(BaoCaodt);
                dataGridView4.DataSource = BaoCaodt;
                dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                #endregion

            }
            catch (Exception)
            {
                MessageBox.Show("Cannot connect to the database");
                System.Windows.Forms.Application.Exit();
            }
            #region add Form1 to Oto tab 
            Form1 f1 = new Form1();
            f1.setCbb(HangXedt);
            f1.TopLevel = false;
            f1.Visible = true;
            f1.FormBorderStyle = FormBorderStyle.None;
            f1.Dock = DockStyle.Fill;
            tabPage2.Controls.Add(f1);
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = string.Format("Insert HangXe (Id, TenHang, DatNuoc) values ('{0}','{1}','{2}')", "H"+(dataGridView1.Rows.Count+1), textBox2.Text, textBox3.Text);
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            HangXedt.Clear();
            HangXeadapter.Fill(HangXedt);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedCells[0].OwningRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedCells[0].OwningRow.Cells[2].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Ban hay chon hang xe can xoa");
            }
            else
            {
                try
                {
                    string query = "Delete HangXe where Id = '" + textBox1.Text +"'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    HangXedt.Clear();
                    HangXeadapter.Fill(HangXedt);
                }
                catch (Exception)
                {
                    MessageBox.Show("Xoa khong thanh cong");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Ban chua chon hang xe can  sua");
            }
            else
            {
                try
                {
                    string query = "Update HangXe set TenHang = '" + textBox2.Text +"', " + "DatNuoc = '" + textBox3.Text +"' where Id = '" + textBox1.Text +"'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    HangXedt.Clear();
                    HangXeadapter.Fill(HangXedt);
                    textBox1.Text = textBox2.Text = textBox3.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Sua khong thanh cong");
                }
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string query = string.Format("Insert KhachHang (Id, TenKH, CMT) values ('{0}','{1}','{2}')", "K"+(dataGridView2.Rows.Count+1), textBox5.Text, textBox4.Text);
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                KhachHangdt.Clear();
                KhachHangadapter.Fill(KhachHangdt);
            }
            catch (Exception)
            {
                MessageBox.Show("Them khong thanh cong");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                MessageBox.Show("Ban hay chon khach hang can xoa");
            }
            else
            {
                try
                {
                    string query = "Delete KhachHang where Id = '" + textBox6.Text +"'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    KhachHangdt.Clear();
                    KhachHangadapter.Fill(KhachHangdt);
                    textBox6.Text = textBox5.Text = textBox4.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Xoa khong thanh cong");
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox6.Text = dataGridView2.SelectedCells[0].OwningRow.Cells[0].Value.ToString();
            textBox5.Text = dataGridView2.SelectedCells[0].OwningRow.Cells[1].Value.ToString();
            textBox4.Text = dataGridView2.SelectedCells[0].OwningRow.Cells[2].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                MessageBox.Show("Ban chua chon khach hang can sua");
            }
            else
            {
                try
                {
                    string query = "Update KhachHang set TenKH = '" + textBox5.Text +"', " + "CMT = '" + textBox4.Text +"' where Id = '" + textBox6.Text +"'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    KhachHangdt.Clear();
                    KhachHangadapter.Fill(KhachHangdt);
                    textBox6.Text = textBox5.Text = textBox4.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Sua khong thanh cong");
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                string query = string.Format("Insert KhachHangXe (Id, IdKhachHang, IdXe) values ('{0}','{1}','{2}')", "HD"+(dataGridView3.Rows.Count+1), textBox8.Text, textBox7.Text);
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                HoaDondt.Clear();
                HoaDonadapter.Fill(HoaDondt);
            }
            catch (Exception)
            {
                MessageBox.Show("Them khong thanh cong");
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string query = "Select k.Id, o.MaXe from Oto o join KhachHangXe hd on o.MaXe = hd.IdXe join KhachHang k on k.Id = hd.IdKhachHang join HangXe h on h.Id = o.IdHangXe where o.TenXe = '" + dataGridView3.SelectedCells[0].OwningRow.Cells[2].Value.ToString() + "' and k.TenKh = '" + dataGridView3.SelectedCells[0].OwningRow.Cells[1].Value.ToString() + "'";            using (SqlCommand cmd = new SqlCommand(query, conn)) {
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                textBox8.Text = reader[0].ToString();
                textBox7.Text = reader[1].ToString();
                reader.Close();
            }
            textBox9.Text = dataGridView3.SelectedCells[0].OwningRow.Cells[0].Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox9.Text == "")
            {
                MessageBox.Show("Ban hay chon hoa don can xoa");
            }
            else
            {
                try
                {
                    string query = "Delete KhachHangXe where Id = '" + textBox9.Text +"'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    HoaDondt.Clear();
                    HoaDonadapter.Fill(HoaDondt);
                    textBox9.Text = textBox8.Text = textBox7.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Xoa khong thanh cong");
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox9.Text == "")
            {
                MessageBox.Show("Ban chua chon hoa don can sua");
            }
            else
            {
                try
                {
                    string query = "Update KhachHangXe set IdKhachHang = '" + textBox8.Text +"', " + "IdXe = '" + textBox7.Text +"' where Id = '" + textBox9.Text +"'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    HoaDondt.Clear();
                    HoaDonadapter.Fill(HoaDondt);
                    textBox9.Text = textBox8.Text = textBox7.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Sua khong thanh cong");
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            _Application app = new Microsoft.Office.Interop.Excel.Application();
            _Workbook workbook = app.Workbooks.Add();
            _Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Worksheets[1];
            worksheet.Name = "Doanh thu";
            for (int i = 0; i < dataGridView4.Columns.Count; i++)
            {
                worksheet.Cells[1, i+1] = dataGridView4.Columns[i].HeaderText;
            }
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView4.Columns.Count; j++)
                {
                    worksheet.Cells[i+2, j+1] = dataGridView4[j, i].Value.ToString();
                }
            }
            workbook.SaveAs(System.Windows.Forms.Application.StartupPath + "\\DoanhThuCacHangXe.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            app.Quit();
            MessageBox.Show("File đã được lưu tại "+System.Windows.Forms.Application.StartupPath.ToString());
        }
    }
}
