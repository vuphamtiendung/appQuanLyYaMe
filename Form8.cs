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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            this.CenterToScreen();
        }
        //Chuỗi Kết Nối
        string strConnectionString = "Data Source = DESKTOP-5PSSEM5\\SQLEXPRESS; Initial Catalog=QLBanGiayDep; Integrated Security = True";
        //Đối tượng kết nối
        SqlConnection conn = null;
        //Đối tượng đưa dữ liệu vào  DataTable dtSanPham.
        SqlDataAdapter daHoaDon= null;
        // Đối tượng hiển thị dữ liệu Form.
        DataTable dtHoaDon = null;
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
                // Đưa dữ liệu lên DataGridView
                dgvHOADON.DataSource = dtHoaDon;
                // Thay đổi độ rộng của cột.
                dgvHOADON.AutoResizeColumns();
                // Xóa trống các đối tượng trong Panel.
                this.txtMaHD.ResetText();
                this.txtMaKH.ResetText();
                this.txtMaNV.ResetText();
                this.txtngaynhapHD.ResetText();
                this.txtngayNH.ResetText();
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
        // Xử lý Form Load.
        private void Form8_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        // Xử lý sự kiện Form Closing.
        private void Form8_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Giải phóng tài nguyên.
            dtHoaDon.Dispose();
            dtHoaDon = null;
            // Hủy Kết Nối.
            conn = null;
        }
        // Xử lý sự kiện Reload
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        // Xử lý sự kiện trở về.
        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Xử lý sự kiện nút thêm.
        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            // Xóa trống các đối tượng trong panel.
            this.txtMaHD.ResetText();
            this.txtMaKH.ResetText();
            this.txtMaNV.ResetText();
            this.txtngaynhapHD.ResetText();
            this.txtngayNH.ResetText();
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
            this.txtMaHD.Focus();
        }
        // Xử lý sự kiện nút Sửa.
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến Sửa.
            Them = false;
            // Cho Thao tác trên panel.
            this.panel.Enabled = true;
            // Thứ tự dạng hiện hành.
            int r = dgvHOADON.CurrentCell.RowIndex;
            // Chuyển thông tin lên panel.
            this.txtMaHD.Text = dgvHOADON.Rows[r].Cells[0].Value.ToString();
            this.txtMaNV.Text = dgvHOADON.Rows[r].Cells[1].Value.ToString();
            this.txtngaynhapHD.Text = dgvHOADON.Rows[r].Cells[2].Value.ToString();
            this.txtngayNH.Text = dgvHOADON.Rows[r].Cells[3].Value.ToString();
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
            this.txtMaHD.Focus();
        }
        // Xử lý sự kiện nút Lưu
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
                    cmd.CommandText = System.String.Concat("Insert Into HoaDon Values(" + "'" + this.txtMaHD.Text.ToString() + "','" + this.txtMaKH.Text.ToString() + "','" + this.txtMaNV.Text.ToString() + "','" + this.txtngaynhapHD.Text.ToString() + "','" + this.txtngayNH.Text.ToString() + "')");
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
                    int r = dgvHOADON.CurrentCell.RowIndex;
                    // MaKH hiện hành.
                    string strMaHD = dgvHOADON.Rows[r].Cells[0].Value.ToString();
                    // Câu Lệnh SQL.
                    cmd.CommandText = System.String.Concat("Update HoaDon Set MaKH='" + this.txtMaKH.Text.ToString() + "', MaNV = '" + this.txtMaNV.Text.ToString() + "', ngaynhapHD = '" + this.txtngaynhapHD.Text.ToString() + "', ngayNH = '" + this.txtngayNH.Text.ToString() +"' Where MaHD = '" + strMaHD + "'");
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
        // Xử lý sự kiện nút Hủy Bỏ.
        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            // Xóa trống các đối tượng trong panel.
            this.txtMaHD.ResetText();
            this.txtMaKH.ResetText();
            this.txtMaNV.ResetText();
            this.txtngaynhapHD.ResetText();
            this.txtngayNH.ResetText();
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
        // Xử lý sự kiện Buttons Xóa.
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
                int r = dgvHOADON.CurrentCell.RowIndex;
                // Lấy MaSP của Record hiện hành.
                string strMaHD = dgvHOADON.Rows[r].Cells[0].Value.ToString();
                // Viết Câu Lệnh SQL.
                cmd.CommandText = System.String.Concat("Delete From HoaDon Where MaHD = '" + strMaHD + "'");
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
