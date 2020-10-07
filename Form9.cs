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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
            this.CenterToScreen();
        }
        //Chuỗi Kết Nối
        string strConnectionString = "Data Source = DESKTOP-5PSSEM5\\SQLEXPRESS; Initial Catalog=QLBanGiayDep; Integrated Security = True";
        //Đối tượng kết nối
        SqlConnection conn = null;
        //Đối tượng đưa dữ liệu vào  DataTable dtSanPham.
        SqlDataAdapter daChiTietHoaDon = null;
        DataTable dtChiTietHoaDon = null;
        //Đối tượng đưa dữ liệu vào DataTable dtHoaDon.
        SqlDataAdapter daHoaDon = null;
        DataTable dtHoaDon = null;
        // Đối tượng đưa dữ liệu vào DataTable stSanPham.
        SqlDataAdapter daSanPham = null;
        //Đối tượng hiển thị dữ liệu lên Form.
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
                daHoaDon = new SqlDataAdapter("Select * From HoaDon", conn);
                dtHoaDon = new DataTable();
                dtHoaDon.Clear();
                daHoaDon.Fill(dtHoaDon);
                // Vận chuyển dữ liệu vào DataTable dtSanPham.
                daSanPham = new SqlDataAdapter("Select * From SanPham", conn);
                dtSanPham = new DataTable();
                dtSanPham.Clear();
                daSanPham.Fill(dtSanPham);
                // vận chuyển dữ liệu vào DataTable dtChiTietHoaDon.
                daChiTietHoaDon = new SqlDataAdapter("Select * From ChiTietHoaDon", conn);
                dtChiTietHoaDon = new DataTable();
                dtChiTietHoaDon.Clear();
                daChiTietHoaDon.Fill(dtChiTietHoaDon);
                // Đưa dữ liệu lên DataGridView
                dgvCHITIETHOADON.DataSource = dtChiTietHoaDon;
                // Thay đổi độ rộng của cột.
                dgvCHITIETHOADON.AutoResizeColumns();
                // Xóa trống các đối tượng trong Panel.
                this.txtSoluong.ResetText();
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
        // Xử lý sự kiện Form Load.
        private void Form9_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        // Xử lý sự kiện Form Closing.
        private void Form9_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Giải phóng tài nguyên.
            dtHoaDon.Dispose();
            dtHoaDon = null;
            // Hủy Kết Nối.
            conn = null;
        }
        // Xử lý sự kiện nút Reload.
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        // Xử lý sự kiện Trở Về.
        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Xử lý sự kiện nút Thêm.
        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            // Xóa trống các đối tượng trong panel.
            this.txtSoluong.ResetText();
            // Cho thao tác trên các nút Lưu/Hủy Bỏ/ và Panel.
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.panel.Enabled = true;
            // Không cho thao tác trên các nút Thêm/Xóa/Trở Về.
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnTroVe.Enabled = false;
            this.cbMaHD.DataSource = dtHoaDon;
            this.cbMaHD.DisplayMember = "MaHD";
            this.cbMaSP.DataSource = dtSanPham;
            this.cbMaSP.DisplayMember = "MaSP";
            // Đưa con trỏ lên TextField txtMaNV.
            this.txtSoluong.Focus();
        }
        // Xử lý buttons Sửa.
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến Sửa.
            Them = false;
            // Đưa dữ liệu lên ComboBox.
            this.cbMaHD.DataSource = dtHoaDon;
            this.cbMaHD.DisplayMember = "MaHD";
            this.cbMaSP.DataSource = dtSanPham;
            this.cbMaSP.DisplayMember = "MaSP";
            // Cho Thao tác trên panel.
            this.panel.Enabled = true;
            // Thứ tự dạng hiện hành.
            int r = dgvCHITIETHOADON.CurrentCell.RowIndex;
            // Chuyển thông tin lên panel.
            this.txtSoluong.Text = dgvCHITIETHOADON.Rows[r].Cells[0].Value.ToString();
            this.cbMaHD.SelectedValue = dgvCHITIETHOADON.Rows[r].Cells[1].Value.ToString();
            this.cbMaSP.SelectedValue = dgvCHITIETHOADON.Rows[r].Cells[2].Value.ToString();
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
            this.txtSoluong.Focus();
        }
        // Xử lý Button Lưu.
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
                    cmd.CommandText = System.String.Concat("Insert Into ChiTietHoaDon Values(" + "'" + this.txtSoluong.Text.ToString() + "','" + this.cbMaHD.SelectedValue.ToString() + "','" + this.cbMaSP.SelectedValue.ToString() + "')");
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
                    int r = dgvCHITIETHOADON.CurrentCell.RowIndex;
                    // MaKH hiện hành.
                    string strMaHD = dgvCHITIETHOADON.Rows[r].Cells[0].Value.ToString();
                    // Câu Lệnh SQL.
                    cmd.CommandText = System.String.Concat("Update ChiTietHoaDon Set Soluong='" + this.txtSoluong.Text.ToString() + "', MaHD = '" + this.cbMaHD.Text.ToString() + "', MaSP = '" + this.cbMaSP.SelectedValue.ToString() + "' Where MaHD = '" + strMaHD + "'");
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
        // Xử lý Buttons Hủy Bỏ
        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            // Xóa trống các đối tượng trong panel.
            this.txtSoluong.ResetText();
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
                int r = dgvCHITIETHOADON.CurrentCell.RowIndex;
                // Lấy MaSP của Record hiện hành.
                string strMaHD = dgvCHITIETHOADON.Rows[r].Cells[0].Value.ToString();
                // Viết Câu Lệnh SQL.
                cmd.CommandText = System.String.Concat("Delete From ChiTietHoaDon Where MaHD = '" + strMaHD + "'");
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
