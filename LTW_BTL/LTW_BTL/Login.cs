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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        bool CheckLogin(string userName, string passWord)
        {
            for (int i = 0; i < ListUser.Intance.ListaccUser.Count; i++)
            {
                if (userName == ListUser.Intance.ListaccUser[i].UserName && passWord == ListUser.Intance.ListaccUser[i].PassWord)
                    return true;
            }    
            return false;
        }

        private void bttDangNhap_Click(object sender, EventArgs e)
        {
            string userName = txtDangNhap.Text;
            string passWord = txtMatKhau.Text;
            if(CheckLogin(userName , passWord))
            {
                FormMain f = new FormMain();
                f.Show();
                this.Hide();
                f.Logout += F_Logout;
            }  
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu", "Lỗi", MessageBoxButtons.OK);
                txtDangNhap.Focus();
                return;
            }    

        }

        private void F_Logout(object sender, EventArgs e)
        {
            (sender as FormMain).isExit = false;
            (sender as FormMain).Close();
            this.Show();
        }

        private void bttThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                txtMatKhau.UseSystemPasswordChar = false;

            if(!checkBox1.Checked)
                txtMatKhau.UseSystemPasswordChar = true;
        }


    }
}
