using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLBanGiayDepThuongHieuYAME2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            Form frm = new frmLogin();
            frm.ShowDialog();
        }
        //Hàm Xem Danh Mục
        void XemDanhMuc(int intDanhMuc)
        {
            Form frm = new Form3();
            frm.Text = intDanhMuc.ToString();
            frm.ShowDialog();
        }
        // Form 1 -> Hệ Thống -> Đổi mật khẩu
        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new FormDoimatkhau();
            frm.Text = "Doi mat khau ";
            frm.ShowDialog();
        }
        // danh mục thành phố
        private void danhMụcThànhPhốToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XemDanhMuc(1);
        }
        // danh mục khách hàng
        private void danhMụcKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XemDanhMuc(2);
        }
        // danh mục Nhân Viên
        private void danhMụcNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XemDanhMuc(3);
        }
        // Xem danh mục Sản Phẩm
        private void danhMụcSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XemDanhMuc(4);
        }
        // Xem danh mục Hóa Đơn
        private void danhMụcHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XemDanhMuc(5);
        }
        // Xem danh mục chi tiết hóa đơn
        private void danhMụcChiTiếtHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XemDanhMuc(6);
        }
        // Form 1 - > Quản Lý Danh Mục -> Thành Phố
        private void danhMụcThànhPhốToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new Form4();
            frm.Text = "Quản Lý Danh Mục Thành Phố";
            frm.ShowDialog();
        }
        // Form 1 -> Quản Lý Danh Mục -> Khách Hàng
        private void danhMụcKháchHàngToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new Form5();
            frm.Text = "Quản Lý Danh Mục Khách Hàng";
            frm.ShowDialog();
        }
        // Form 1 -> Quản Lý Danh Mục -> Nhân Viên
        private void danhMụcNhânViênToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new Form6();
            frm.Text = "Quản Lý danh mục Nhân Viên";
            frm.ShowDialog();
        }

        private void danhMụcSảnPhẩmToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new Form7();
            frm.Text = "Quản Lý danh mục Sản Phẩm";
            frm.ShowDialog();
        }

        private void danhMụcHóaĐơnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new Form8();
            frm.Text = "Quản Lý danh mục Hóa Đơn";
            frm.ShowDialog();
        }

        private void danhMụcChiTiếtHóaĐơnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new Form9();
            frm.Text = "Quản Lý danh mục Chi Tiết Hóa Đơn";
            frm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
    }
}
