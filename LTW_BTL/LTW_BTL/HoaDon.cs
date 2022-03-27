using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace LTW_BTL
{
    public partial class HoaDon : Form
    {
        public SendDoanhThu senddt;
        public SendSLHD sendhd;
        public int slhd = 0;
        HoaDonBLL bllHoaDon;
        public HoaDon()
        {
            InitializeComponent();
            bllHoaDon = new HoaDonBLL();
        }
        public HoaDon(SendDoanhThu senderdt, SendSLHD sendhd)
        {
            InitializeComponent();
            bllHoaDon = new HoaDonBLL();
            this.senddt = senderdt;
            this.sendhd = sendhd;
        }
        public void ShowAllHoaDon()
        {
            DataTable dt = bllHoaDon.getALLHoaDon();
            dgvHoaDon.DataSource = dt;
        }
        public bool CheckData()
        {

            if (string.IsNullOrEmpty(txtMaHD.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaHD.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenKH.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenKH.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cbGT.Text))
            {
                MessageBox.Show("Bạn chưa nhập giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbGT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTongTien.Text))
            {
                MessageBox.Show("Bạn chưa nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTongTien.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cbDiaChi.Text))
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbDiaChi.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNgayBan.Text))
            {
                MessageBox.Show("Bạn chưa nhập ngày", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNgayBan.Focus();
                return false;
            }

            return true;

        }
        private void bttThem_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                tblHoaDon hoadon = new tblHoaDon();
                hoadon.MaHD = txtMaHD.Text;
                hoadon.TenKH = txtTenKH.Text;
                hoadon.GioiTinh = cbGT.Text;
                hoadon.TongTien = int.Parse(txtTongTien.Text);
                hoadon.DiaChi = cbDiaChi.Text;
                hoadon.NgayBan = txtNgayBan.Text;
                if (bllHoaDon.InsertHoaDon(hoadon))
                {
                    senddt(int.Parse(txtTongTien.Text));
                    slhd++;
                    sendhd(slhd);
                    ShowAllHoaDon();
                }
                else
                    MessageBox.Show("Đã có lỗi xảy ra, xin thử lại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            ShowAllHoaDon();
        }



        private void btXóa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                tblHoaDon hoadon = new tblHoaDon();
                hoadon.MaHD = txtMaHD.Text;
                if (bllHoaDon.DeleteThucAn(hoadon))
                {
                    slhd--;
                    sendhd(slhd);
                    ShowAllHoaDon();
                }
                else
                    MessageBox.Show("Đã có lỗi xảy ra, xin thử lại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }   
    }
}
