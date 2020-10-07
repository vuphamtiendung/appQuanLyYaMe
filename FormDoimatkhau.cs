using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBanGiayDepThuongHieuYAME2
{
    public partial class FormDoimatkhau : Form
    {
        public FormDoimatkhau()
        {
            InitializeComponent();
            this.CenterToScreen();
        }
        public string userName
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        private void changePassword()
        {
            if (textbox7.Text == userName)
            {
                if (textbox7.Text == Password)
                {
                    if (textbox7.Text == textbox9.Text)
                    {
                        MessageBox.Show("Password đã được thay đổi");
                        Password = textbox7.Text;
                    }
                    else
                    {
                        textbox9.Focus();
                        MessageBox.Show("Password repeat không giống nhau ");
                    }
                }
                else
                {
                    textbox7.Focus();
                    MessageBox.Show("Sai Password !");
                }
            }
            else
            {
                textbox6.Focus();
                MessageBox.Show("Không tồn tại User này");
            }
        }
        // Xử lý nút đồng ý
        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (textbox6.Text == " ")
            {
                MessageBox.Show("Bạn Chưa Đăng Nhập 'tên đăng nhập'!");
                textbox6.Focus();
            }
            else if (textbox7.Text == " ")
            {
                MessageBox.Show("Bạn Chưa Nhập 'Mật Khẩu Cũ'!");
                textbox7.Focus();
            }
            else if (textbox8.Text == " ")
            {
                MessageBox.Show("Bạn Chưa Đăng Nhập 'mật khẩu mới'!");
                textbox8.Focus();
            }
            else if (textbox9.Text == " ")
            {
                MessageBox.Show("Bạn chưa lập lại mật khẩu ");
                textbox9.Focus();
            }
            MessageBox.Show("Đổi Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.None);
        }
        // Xử lý nút thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
