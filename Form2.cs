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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            this.CenterToScreen();
        }
        // Xử lý nút đăng nhập
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if ((this.txtUser.Text == "vuphamtiendung@gmail.com") && (this.txtPass.Text == "@_lethanhbinh_1992"))
            {
                MessageBox.Show("Bạn đã đăng nhập thành công", "Thông báo");
                this.Close();
            }
            else
            {
                MessageBox.Show("Không đúng tên đăng nhập / Mật khẩu !!!", "Thông Báo");
                this.txtUser.Focus();
            }
        }
        // Xử lý nút thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("   Chắc Không ?", "Trả Lời", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
