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
    public delegate void SendDoanhThu(int value);
    public delegate void SendSLHD(int value);
    public delegate void SendSLM(int value);
    public partial class FormMain : Form
    {
        public bool isExit = true;
        public event EventHandler Logout;
        public FormMain()
        {
            InitializeComponent();
            DoanhThu.Text = "0";
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
           if(isExit)
            Application.Exit();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExit)
            {
                if (MessageBox.Show("Bạn muốn thoát chươnng trình?", "Cảnh báo", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    e.Cancel = true;
            }    
                
        }
        
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logout(this, new EventArgs());
        }

        private void thứcĂnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThucAn thucan = new ThucAn(SetSLM);
            thucan.Show();
            
        }
        private void SetDoanhThu(int value)
        {
            DoanhThu.Text = (int.Parse(DoanhThu.Text) + value).ToString(); 
        }
        private void SetHD(int value)
        {
            SLHD.Text = value.ToString();
        }
        private void SetSLM(int value)
        {
            SLMon.Text = value.ToString();
        }
        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HoaDon hoadon = new HoaDon(SetDoanhThu, SetHD);
            hoadon.Show();
        }
    }
}
