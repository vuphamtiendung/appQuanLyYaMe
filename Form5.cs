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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            this.CenterToScreen();
        }
        //Chuỗi Kết Nối
        string strConnectionString = "Data Source = DESKTOP-5PSSEM5\\SQLEXPRESS; Initial Catalog=QLBanGiayDep; Integrated Security = True";
        //Đối tượng kết nối
        SqlConnection conn = null;
        //Đối tượng đưa dữ liệu vào DataTable dtKhachHang
        SqlDataAdapter daKhachHang = null;
        // Đối tượng hiển thị dữ liệu lên Form
        DataTable dtKhachHang = null;
        // Đối Tượng Hiển Thị Dữ Liệu vào DataTable dtThanhPho.
        SqlDataAdapter daThanhPho = null;
        // Đối tượng hiển thị dữ liệu lên Form.
        DataTable dtThanhPho = null;
        bool Them;
        void LoadData()
        {
            try
            {
                // Khởi Động connection
                conn = new SqlConnection(strConnectionString);
                // Vận chuyển dữ liệu vào DataTable dtThanhPho
                daThanhPho = new SqlDataAdapter("Select * From TinhThanhPho", conn);
                dtThanhPho = new DataTable();
                dtThanhPho.Clear();
                daThanhPho.Fill(dtThanhPho);
                // Vận chuyển dữ liệu vào DataTable dtKhachHang
                daKhachHang = new SqlDataAdapter("Select * From KhachHang", conn);
                dtKhachHang = new DataTable();
                dtKhachHang.Clear();
                daKhachHang.Fill(dtKhachHang);
                // Đưa dữ liệu lên DataGridView
                dgvKHACHHANG.DataSource = dtKhachHang;
                // Xóa Trống Đối tượng trong Panel
                this.txtMaKH.ResetText();
                this.txtTenCty.ResetText();
                this.txtDiachi.ResetText();
                this.txtDienthoai.ResetText();
                // Không cho thao tác trên các nút lưu/ hủy bỏ.
                this.btnLuu.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.panel1.Enabled = false;
                // Cho các thao tác trên các nút Thêm/Sửa/Xóa/Trở Về.
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                this.btnTroVe.Enabled = true;
            }
            catch (SqlException)
            {
                MessageBox.Show("không lấy được nội dung trong Table KhachHang. Lỗi Rồi !");
            }
        }
        // Xử lý Form 5
        private void Form5_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        // Xử lý Button Reload
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        // Xử lý Button Thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            // Xóa trống Các đối tượng Trong Panel.
            this.txtMaKH.ResetText();
            this.txtTenCty.ResetText();
            this.txtDiachi.ResetText();
            this.txtDienthoai.ResetText();
            // Cho thao tác trên các nút Lưu/ Hủy Bỏ.
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.panel1.Enabled = true;
            // Không cho thao tác trên các nút Thêm/Xóa/Thoát.
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnTroVe.Enabled = false;
            // Đưa dữ liệu lên ComboBox.
            this.cbThanhPho.DataSource = dtThanhPho;
            this.cbThanhPho.DisplayMember = "TenTinh/ThanhPho";
            this.cbThanhPho.ValueMember = "ThanhPho";
            // Đưa con trỏ đến TextField txtMaKH.
            this.txtMaKH.Focus();
        }
        // Xử lý button Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến sửa
            Them = false;
            // Đưa dữ liệu lên ComboBox.
            this.cbThanhPho.DataSource = dtThanhPho;
            this.cbThanhPho.DisplayMember = "TenTinh/ThanhPho";
            this.cbThanhPho.ValueMember = "ThanhPho";
            //Cho thao tác trên Panel
            this.panel1.Enabled = true;
            // Thứ tự dòng hiện hành.
            int r = dgvKHACHHANG.CurrentCell.RowIndex;
            // Chuyển thông tin lên Panel.
            this.txtMaKH.Text = dgvKHACHHANG.Rows[r].Cells[0].Value.ToString();
            this.txtTenCty.Text = dgvKHACHHANG.Rows[r].Cells[1].Value.ToString();
            this.txtDiachi.Text = dgvKHACHHANG.Rows[r].Cells[2].Value.ToString(); 
            this.cbThanhPho.Text = dgvKHACHHANG.Rows[r].Cells[3].Value.ToString();
            this.txtDienthoai.Text = dgvKHACHHANG.Rows[r].Cells[4].Value.ToString();
            // Cho Thao Tác Trên Các Nút Lưu/Hủy bỏ và Panel.
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.panel1.Enabled = true;
            // Không cho thao tác trên các nút Thêm/Xóa/Trở Về.
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnTroVe.Enabled = false;
            // Đưa con trỏ đến TextField txtMakH
            this.txtMaKH.Focus();
        }
        // Xử lý button Lưu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            //Mở kết nối
            conn.Open();
            // Thêm dữ liệu
            if (Them)
            {
                try
                {
                    // Thực hiện lệnh
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    // Lệnh Insert Into
                    cmd.CommandText = System.String.Concat("Insert Into KhachHang Values(" + "'" + this.txtMaKH.Text.ToString() + "','" + this.txtTenCty.Text.ToString() + "','" + this.txtDiachi.Text.ToString() + "','" + this.cbThanhPho.SelectedValue.ToString() + "','" + this.txtDienthoai.Text.ToString() + "')");
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    //Load dữ liệu lên DataGridView.
                    LoadData();
                    // Thông Báo
                    MessageBox.Show("Đã thêm xong");
                }
                catch (SqlException)
                {
                    MessageBox.Show("Không thêm được. Lỗi rồi !");
                }
            }
            if (!Them)
            {
                try
                {
                    // Thực hiện lệnh.
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    // Thực hiện dòng lệnh hiện hành.
                    int r = dgvKHACHHANG.CurrentCell.RowIndex;
                    // MaKH hiện hành.
                    string strMaKH = dgvKHACHHANG.Rows[r].Cells[0].Value.ToString();
                    // Câu Lệnh SQL.
                    cmd.CommandText = System.String.Concat("Update KhachHang Set TenCty='" + this.txtTenCty.Text.ToString() + "', DiaChi = '" + this.txtDiachi.Text.ToString() + "', ThanhPho = '" + this.cbThanhPho.SelectedValue.ToString() + "', DienThoai = '" + this.txtDienthoai.Text.ToString() + "' Where MaKH = '" + strMaKH + "'");
                    // Cập Nhật.
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    // Load dữ liệu lên DataGridView
                    LoadData();
                    // Thông Báo.
                    MessageBox.Show("Đã sửa xong !");
                }
                catch (SqlException)
                {
                    MessageBox.Show("Không thêm được. Lỗi rồi !");
                }
            }
            // Đóng Kết Nối
            conn.Close();
        }
        // Xử lý button Hủy Bỏ
        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            // Xóa trống các đối tượng trong panel.
            this.txtMaKH.ResetText();
            this.txtTenCty.ResetText();
            this.txtDiachi.ResetText();
            this.txtDienthoai.ResetText();
            // Cho thao tác trên các nút Thêm/Sửa/Xóa/Trở Về.
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            this.btnTroVe.Enabled = true;
            // Không cho thao tác trên các nút Lưu/Hủy Bỏ/ và Panel.
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.panel1.Enabled = false;
        }
        // Xử lý Button Xóa.
        private void btnXoa_Click(object sender, EventArgs e)
        {
            //Mở kết nối.
            conn.Open();
            try
            {
                // Thực hiện lệnh
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                // Lấy thứ tự record hiện hành.
                int r = dgvKHACHHANG.CurrentCell.RowIndex;
                // Lấy MaKH của Record hiện hành.
                string strMaKH = dgvKHACHHANG.Rows[r].Cells[0].Value.ToString();
                // Viết Câu Lệnh SQL.
                cmd.CommandText = System.String.Concat("Delete From KhachHang Where MaKH = '" + strMaKH + "'");
                cmd.CommandType = CommandType.Text;
                // Thực hiện lệnh SQL.
                cmd.ExecuteNonQuery();
                // Cập nhật lại DataGridView.
                LoadData();
                // Thông Báo.
                MessageBox.Show("Đã xóa xong !");
            }
            catch (SqlException)
            {
                MessageBox.Show("Không Xóa được rồi !");
            }
            // Đóng kết nối
            conn.Close();
        }
        // Xử lý Button TroVe.
        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
