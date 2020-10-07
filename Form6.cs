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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            this.CenterToScreen();
        }
        //Chuỗi Kết Nối
        string strConnectionString = "Data Source = DESKTOP-5PSSEM5\\SQLEXPRESS; Initial Catalog=QLBanGiayDep; Integrated Security = True";
        //Đối tượng kết nối
        SqlConnection conn = null;
        //Đối tượng đưa dữ liệu vào  DataTable dtNhanVien.
        SqlDataAdapter daNhanVien = null;
        // Đối tượng hiển thị dữ liệu Form.
        DataTable dtNhanVien = null;
        // Khai báo biến kiểm tra việc Thêm hay Sửa dữ liệu.
        bool Them;
        void LoadData()
        {
            try
            {
                // Khởi động kết nối connection.
                conn = new SqlConnection(strConnectionString);
                // Vận chuyển dữ liệu lên DataTable.
                daNhanVien = new SqlDataAdapter("Select * From NhanVien", conn);
                dtNhanVien = new DataTable();
                dtNhanVien.Clear();
                daNhanVien.Fill(dtNhanVien);
                // Đưa dữ liệu lên DataGridView
                dgvNHANVIEN.DataSource = dtNhanVien;
                // Thay đổi độ rộng của cột.
                dgvNHANVIEN.AutoResizeColumns();
                // Xóa trống các đối tượng trong Panel.
                this.txtMaNV.ResetText();
                this.txtHolot.ResetText();
                this.txtTen.ResetText();
                this.txtNgaysinh.ResetText();
                this.txtDiachi.ResetText();
                this.txtDienthoai.ResetText();
                // không cho thao tác trên các nút Lưu/Hủy Bỏ/ và Panel.
                this.btnLuu.Enabled = true;
                this.btnHuyBo.Enabled = true;
                this.panel1.Enabled = true;
                // Cho thao tác trên các nút Thêm/Xóa/Sửa/Trở Về.
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                this.btnTroVe.Enabled = true;
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table NhanVien. Lỗi Rồi !!!!");
            }
        }
        // Xử lý Form 6
        private void Form6_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLBanGiayDepDataSet3.NhanVien' table. You can move, or remove it, as needed.
            this.nhanVienTableAdapter.Fill(this.qLBanGiayDepDataSet3.NhanVien);
            LoadData();
        }
        // Xử lý sự kiện Form Closing
        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Giải phóng tài nguyên.
            dtNhanVien.Dispose();
            dtNhanVien = null;
            // Hủy Kết Nối.
            conn = null;
        }
        // Xử lý buttons Reload.
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        // Xử lý buttons Trở về.
        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Xử lý button Thêm.
        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            // Xóa trống các đối tượng trong panel.
            this.txtMaNV.ResetText();
            this.txtHolot.ResetText();
            this.txtTen.ResetText();
            this.txtNgaysinh.ResetText(); 
            this.txtDiachi.ResetText(); 
            this.txtDienthoai.ResetText();
            // Cho thao tác trên các nút Lưu/Hủy Bỏ/ và Panel.
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.panel1.Enabled = true;
            // Không cho thao tác trên các nút Thêm/Xóa/Trở Về.
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnTroVe.Enabled = false;
            // Đưa con trỏ lên TextField txtMaNV.
            this.txtMaNV.Focus();
        }
        // Xử lý nút button Sua.
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến Sửa.
            Them = false;
            // Cho Thao tác trên panel.
            this.panel1.Enabled = true;
            // Thứ tự dạng hiện hành.
            int r = dgvNHANVIEN.CurrentCell.RowIndex;
            // Chuyển thông tin lên panel.
            this.txtMaNV.Text = dgvNHANVIEN.Rows[r].Cells[0].Value.ToString();
            this.txtHolot.Text = dgvNHANVIEN.Rows[r].Cells[1].Value.ToString();
            this.txtTen.Text = dgvNHANVIEN.Rows[r].Cells[2].Value.ToString();
            this.txtNgaysinh.Text = dgvNHANVIEN.Rows[r].Cells[3].Value.ToString();
            this.txtDiachi.Text = dgvNHANVIEN.Rows[r].Cells[4].Value.ToString();
            this.txtDienthoai.Text = dgvNHANVIEN.Rows[r].Cells[5].Value.ToString();
            // Cho thao tác lên các nút Lưu/Hủy Bỏ/ Panel.
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.panel1.Enabled = true;
            // Không cho thao tác trên các nút Thêm/Sửa/Xóa/Trở Về.
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnTroVe.Enabled = false;
            // Đưa con trỏ đến TextField txtMaNV.
            this.txtMaNV.Focus();
        }
        // Xử lý nút Buttons Hủy Bỏ.
        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            // Xóa trống các đối tượng trong panel.
            this.txtMaNV.ResetText();
            this.txtHolot.ResetText();
            this.txtTen.ResetText();
            this.txtNgaysinh.ResetText();
            this.txtDiachi.ResetText();
            this.txtDienthoai.ResetText();
            // Cho thao tác trên các nút Thêm/Xóa/Trở Về.
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            this.btnTroVe.Enabled = true;
            // Không cho thao tác trên các nút Lưu/Hủy Bỏ/Panel.
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.panel1.Enabled = false;
        }
        // Xử lý nút Xóa.
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
                int r = dgvNHANVIEN.CurrentCell.RowIndex;
                // Lấy MaNV của Record hiện hành.
                string strMaNV = dgvNHANVIEN.Rows[r].Cells[0].Value.ToString();
                // Viết Câu Lệnh SQL.
                cmd.CommandText = System.String.Concat("Delete From NhanVien Where MaNV = '" + strMaNV + "'");
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
        // Xử lý Buttons Lưu.
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
                    cmd.CommandText = System.String.Concat("Insert Into NhanVien Values(" + "'" + this.txtMaNV.Text.ToString() + "','" + this.txtHolot.Text.ToString() + "','" + this.txtDiachi.Text.ToString() + "','" + this.txtTen.Text.ToString() + "','" + this.txtDienthoai.Text.ToString() + "')");
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
                    int r = dgvNHANVIEN.CurrentCell.RowIndex;
                    // MaKH hiện hành.
                    string strMaNV = dgvNHANVIEN.Rows[r].Cells[0].Value.ToString();
                    // Câu Lệnh SQL.
                    cmd.CommandText = System.String.Concat("Update NhanVien Set Holot='" + this.txtHolot.Text.ToString() + "', DiaChi = '" + this.txtDiachi.Text.ToString() + "', Ten = '" + this.txtTen.Text.ToString() + "', DienThoai = '" + this.txtDienthoai.Text.ToString() + "' Where MaNV = '" + strMaNV + "'");
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
    }
}
