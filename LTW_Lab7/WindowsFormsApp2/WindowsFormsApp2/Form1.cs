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

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        SqlConnection conn = null;
        SqlDataAdapter adapter = null;
        DataTable dt = null;
        List<HangXe> hangXeList = new List<HangXe>();
        public Form1()
        {
            InitializeComponent();
        }
        ~Form1()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.TabStop = false;
                label1.TabStop = label2.TabStop = label3.TabStop = label4.TabStop = label5.TabStop = false;
                string cnt = "Data Source = DESKTOP-KNN7K79; Initial Catalog = QLOTO; Integrated Security = True";
                conn = new SqlConnection(cnt);
                conn.Open();
                string cmd = "Select MaXe as 'ID', TenXe as 'Ten Xe', TenHang as 'Ten Hang', GiaXe as 'Gia Xe', NamSX as 'Nam SX' from Oto o join HangXe h on o.IdHangXe = h.Id";
                adapter = new SqlDataAdapter(cmd, conn);
                dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                comboBox1.DisplayMember = "Name";
                comboBox1.SelectedIndex = -1;
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot connect to the database");
                Application.Exit();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellCollection myRow = dataGridView1.SelectedCells[0].OwningRow.Cells;
            textBox1.Text = myRow[0].Value.ToString();
            textBox2.Text = myRow[1].Value.ToString();
            HangXe a = comboBox1.SelectedItem as HangXe;
            comboBox1.SelectedItem = a;
            comboBox1.DisplayMember = "Name";
            textBox4.Text = myRow[3].Value.ToString();
            textBox5.Text = myRow[4].Value.ToString().Split(' ')[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = string.Format("insert into Oto (MaXe,TenXe,IdHangXe,GiaXe,NamSX) values ('{0}','{1}','{2}', {3}, '{4}')", "X"+(dataGridView1.Rows.Count+1), textBox2.Text, ((HangXe)comboBox1.SelectedItem).ID, textBox4.Text, textBox5.Text);
                cmd.ExecuteNonQuery();
                dt.Clear();
                adapter.Fill(dt);
                MessageBox.Show("Thêm thành công");
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm không thành công");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Ban hay chon oto can xoa");
            }
            else
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = string.Format("delete Oto where MaXe = '{0}'", dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value.ToString());
                    cmd.ExecuteNonQuery();
                    dt.Clear();
                    adapter.Fill(dt);
                    MessageBox.Show("Xóa thành công");
                    textBox1.Text = textBox2.Text = textBox4.Text = textBox5.Text = "";
                    comboBox1.SelectedIndex = -1;
                }
                catch (Exception)
                {
                    MessageBox.Show("Xóa không thành công");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Ban chua chon oto can sua");
            }
            else
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = string.Format("update  Oto set MaXe = '{0}', TenXe = '{1}', IdHangXe = '{2}', GiaXe = {3}, NamSX = '{4}' where MaXe = '{5}'", textBox1.Text, textBox2.Text, ((HangXe)comboBox1.SelectedItem).ID, textBox4.Text, textBox5.Text, dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value.ToString());
                    cmd.ExecuteNonQuery();
                    dt.Clear();
                    adapter.Fill(dt);
                    MessageBox.Show("Sửa thành công");
                    textBox1.Text = textBox2.Text = textBox4.Text = textBox5.Text = "";
                    comboBox1.SelectedItem = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    MessageBox.Show("Sửa không thành công");
                }
            }
        }
        public void setCbb(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                hangXeList.Add(new HangXe() { ID = dt.Rows[i][0].ToString(), Name = dt.Rows[i][1].ToString(), Country = dt.Rows[i][2].ToString() });
            }
            comboBox1.DataSource = hangXeList;
            comboBox1.DisplayMember = "TenHang";
        }
    }
    class HangXe
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
