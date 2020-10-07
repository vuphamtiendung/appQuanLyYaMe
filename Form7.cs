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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            this.CenterToScreen();
        }
        //Chuỗi Kết Nối
        string strConnectionString = "Data Source = DESKTOP-5PSSEM5\\SQLEXPRESS; Initial Catalog=QLBanGiayDep; Integrated Security = True";
        //Đối tượng kết nối
        SqlConnection conn = null;
        //Đối tượng đưa dữ liệu vào  DataTable dtSanPham.
        SqlDataAdapter daSanPham = null;
        // Đối tượng hiển thị dữ liệu Form.
        DataTable dtSanPham = null;
        // Khai báo biến kiểm tra việc Thêm hay Sửa dữ liệu.
        bool Them;
        void LoadData()
        {
            try
            {
                // Khởi động kết nối connection.
                conn = new SqlConnection(strConnectionString);
                // Vận chuyển dữ liệu lên DataTable.
                daSanPham = new SqlDataAdapter("Select * From SanPham", conn);
                dtSanPham = new DataTable();
                dtSanPham.Clear();
                daSanPham.Fill(dtSanPham);
                // Đưa dữ liệu lên DataGridView
                dgvSANPHAM.DataSource = dtSanPham;
                // Thay đổi độ rộng của cột.
                dgvSANPHAM.AutoResizeColumns();
                // Xóa trống các đối tượng trong Panel.
                this.txtMaSP.ResetText();
                this.txtTenSP.ResetText();
                this.txtDonvitinh.ResetText();
                this.txtDongia.ResetText();
                // không cho thao tác trên các nút Lưu/Hủy Bỏ/ và Panel.
                this.btnLuu.Enabled = true;
                this.btnHuyBo.Enabled = true;
                this.panel.Enabled = true;
                // Cho thao tác trên các nút Thêm/Xóa/Sửa/Trở Về.
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                this.btnTroVe.Enabled = true;
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table SanPham. Lỗi Rồi !!!!");
            }
        }
        // Xử lý Form Load
        private void Form7_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLBanGiayDepDataSet4.SanPham' table. You can move, or remove it, as needed.
            this.sanPhamTableAdapter.Fill(this.qLBanGiayDepDataSet4.SanPham);
            LoadData();
        }
        // Xử lý sự kiện Form Closing
        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Giải phóng tài nguyên.
            dtSanPham.Dispose();
            dtSanPham = null;
            // Hủy Kết Nối.
            conn = null;
        }
        // Xử lý buttons Reload
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        // Xử lý nút trở về
        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Xử lý nút Buttons Thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            // Xóa trống các đối tượng trong panel.
            this.txtMaSP.ResetText();
            this.txtTenSP.ResetText();
            this.txtDonvitinh.ResetText();
            this.txtDongia.ResetText();
            // Cho thao tác trên các nút Lưu/Hủy Bỏ/ và Panel.
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.panel.Enabled = true;
            // Không cho thao tác trên các nút Thêm/Xóa/Trở Về.
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnTroVe.Enabled = false;
            // Đưa con trỏ lên TextField txtMaNV.
            this.txtMaSP.Focus();
        }
        // Xử lý buttons Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến Sửa.
            Them = false;
            // Cho Thao tác trên panel.
            this.panel.Enabled = true;
            // Thứ tự dạng hiện hành.
            int r = dgvSANPHAM.CurrentCell.RowIndex;
            // Chuyển thông tin lên panel.
            this.txtMaSP.Text = dgvSANPHAM.Rows[r].Cells[0].Value.ToString();
            this.txtTenSP.Text = dgvSANPHAM.Rows[r].Cells[1].Value.ToString();
            this.txtDonvitinh.Text = dgvSANPHAM.Rows[r].Cells[2].Value.ToString();
            this.txtDongia.Text = dgvSANPHAM.Rows[r].Cells[3].Value.ToString();
            // Cho thao tác lên các nút Lưu/Hủy Bỏ/ Panel.
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.panel.Enabled = true;
            // Không cho thao tác trên các nút Thêm/Sửa/Xóa/Trở Về.
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnTroVe.Enabled = false;
            // Đưa con trỏ đến TextField txtMaNV.
            this.txtMaSP.Focus();
        }
        // Xử lý Buttons Lưu
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
                    cmd.CommandText = System.String.Concat("Insert Into SanPham Values(" + "'" + this.txtMaSP.Text.ToString() + "','" + this.txtTenSP.Text.ToString() + "','" + this.txtDonvitinh.Text.ToString() + "','" + this.txtDongia.Text.ToString() + "')");
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
                    int r = dgvSANPHAM.CurrentCell.RowIndex;
                    // MaKH hiện hành.
                    string strMaSP = dgvSANPHAM.Rows[r].Cells[0].Value.ToString();
                    // Câu Lệnh SQL.
                    cmd.CommandText = System.String.Concat("Update SanPham Set TenSP='" + this.txtTenSP.Text.ToString() + "', Donvitinh = '" + this.txtDonvitinh.Text.ToString() + "', Dongia = '" + this.txtDongia.Text.ToString() +  "' Where MaSP = '" + strMaSP + "'");
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
        // Xử lý Buttons Hủy Bỏ.
        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            // Xóa trống các đối tượng trong panel.
            this.txtMaSP.ResetText();
            this.txtTenSP.ResetText();
            this.txtDonvitinh.ResetText();
            this.txtDongia.ResetText();
            // Cho thao tác trên các nút Thêm/Xóa/Trở Về.
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            this.btnTroVe.Enabled = true;
            // Không cho thao tác trên các nút Lưu/Hủy Bỏ/Panel.
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.panel.Enabled = false;
        }
        // Xử lý Buttons Xóa.
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
                int r = dgvSANPHAM.CurrentCell.RowIndex;
                // Lấy MaSP của Record hiện hành.
                string strMaSP = dgvSANPHAM.Rows[r].Cells[0].Value.ToString();
                // Viết Câu Lệnh SQL.
                cmd.CommandText = System.String.Concat("Delete From SanPham Where MaSP = '" + strMaSP + "'");
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
    }
}
