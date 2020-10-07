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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.CenterToScreen();
        }
        // khai báo mức class
        //chuỗi Kết nối
        string strConnectionString = "Data Source = DESKTOP-5PSSEM5\\SQLEXPRESS; InitializationEventAttribute Catalog = QLBanGiayDep; Integrated Security = true";
        //Đối Tượng Kết Nối
        SqlConnection conn = null;
        //Đối tượng đưa dữ liệu vào Datatable dtTable
        SqlDataAdapter daTable = null;
        //Đối tượng hiển thị dữ liệu lên form
        DataTable dtTable = null;
        // Xử lý Form 3
        private void Form3_Load(object sender, EventArgs e)
        {
            try
            {
                //Khởi động connection
                conn = new SqlConnection(strConnectionString);
                //Xử lý danh mục
                int intDM = Convert.ToInt32(this.Text);
                switch (intDM)
                {
                    case 1:
                        lblDanhMuc.Text = "Danh mục Thành Phố";
                        daTable = new SqlDataAdapter("Select ThanhPho, TenThanhPho From TinhThanhPho", conn);
                        break;
                    case 2:
                        lblDanhMuc.Text = "Danh mục Khách Hàng";
                        daTable = new SqlDataAdapter("Select MaKH, TenCty From KhachHang", conn);
                        break;
                    case 3:
                        lblDanhMuc.Text = "Danh mục Nhân Viên";
                        daTable = new SqlDataAdapter("Select MaNV, HoLot, Ten From NhanVien", conn);
                        break;
                    case 4:
                        lblDanhMuc.Text = "Danh mục Sản Phẩm";
                        daTable = new SqlDataAdapter("Select MaSP, TenSP, DonViTinh, DonGia From SanPham", conn);
                        break;
                    case 5:
                        lblDanhMuc.Text = "Danh mục Hóa Đơn";
                        daTable = new SqlDataAdapter("Select MaHD, MaKH, MaNV From HoaDon", conn);
                        break;
                    case 6:
                        lblDanhMuc.Text = "Danh mục Chi Tiết Hóa Đơn";
                        daTable = new SqlDataAdapter("Select * From ChiTietHoaDon", conn);
                        break;
                    default:
                        break;
                }
                // Vận chuyển dữ liệu lên DataTable dtTable
                dtTable = new DataTable();
                dtTable.Clear();
                daTable.Fill(dtTable);
                //Đưa dữ liệu lên DataGridView
                dgvDanhMuc.AutoResizeColumns();
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong Table. Lỗi Rồi !!!");
            }

        }
        // Xử lý nút trở về
        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDanhMuc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
