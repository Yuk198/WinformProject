using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTW_BTL
{
    public partial class ThucAn : Form
    {
        ThucAnBLL bllThucAn;
        public ThucAn()
        {
            InitializeComponent();
            bllThucAn = new ThucAnBLL();
        }
        public void ShowAllThucAn()
        {
            DataTable dt = bllThucAn.getALLThucAn();
            dataGridView1.DataSource = dt;
        }

        private void ThucAn_Load(object sender, EventArgs e)
        {
            ShowAllThucAn();
        }
        public bool CheckData()
        {
            if(string.IsNullOrEmpty(txtMaMon.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã món ăn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaMon.Focus();
                return false;
            }
            if(string.IsNullOrEmpty(txtTenMon.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên món ăn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenMon.Focus();
                return false;
            }
            if(string.IsNullOrEmpty(txtSoLuong.Text))
            {
                MessageBox.Show("Bạn chưa nhập số lượng món ăn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoLuong.Focus();
                return false;
            }
            if(string.IsNullOrEmpty(txtDonGia.Text))
            {
                MessageBox.Show("Bạn chưa nhập đơn giá món ăn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDonGia.Focus();
                return false;
            }
            return true;

        }

        private void bttThem_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                tblThucAn thucan = new tblThucAn();
                thucan.MaMon = txtMaMon.Text;
                thucan.TenMon = txtTenMon.Text;
                thucan.SoLuong = int.Parse(txtSoLuong.Text);
                thucan.DonGia = int.Parse(txtDonGia.Text);
                if (bllThucAn.InsertThucAn(thucan))
                    ShowAllThucAn();
                else
                    MessageBox.Show("Đã có lỗi xảy ra, xin thử lại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
        }
        //Hiển thị lên form khi click vào dtgview 
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                txtMaMon.Text = dataGridView1.Rows[index].Cells["MaMon"].Value.ToString();
                txtTenMon.Text = dataGridView1.Rows[index].Cells["TenMon"].Value.ToString();
                txtSoLuong.Text = dataGridView1.Rows[index].Cells["SoLuong"].Value.ToString();
                txtDonGia.Text = dataGridView1.Rows[index].Cells["DonGia"].Value.ToString();
            }
        }

        private void bttSua_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                tblThucAn thucan = new tblThucAn();
                thucan.MaMon = txtMaMon.Text;
                thucan.TenMon = txtTenMon.Text;
                thucan.SoLuong = int.Parse(txtSoLuong.Text);
                thucan.DonGia = int.Parse(txtDonGia.Text);
                if (bllThucAn.UpdateThucAn(thucan))
                    ShowAllThucAn();
                else
                    MessageBox.Show("Đã có lỗi xảy ra, xin thử lại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bttXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes);
            {
                tblThucAn thucan = new tblThucAn();
                thucan.MaMon = txtMaMon.Text;
                if (bllThucAn.DeleteThucAn(thucan))
                    ShowAllThucAn();
                else
                    MessageBox.Show("Đã có lỗi xảy ra, xin thử lại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bttĐóng_Click(object sender, EventArgs e)
        {
            FormMain f = new FormMain();
            f.Show();
            this.Close();
        }
    }
}
