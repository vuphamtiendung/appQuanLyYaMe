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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            this.CenterToScreen();
        }
        //Chuỗi Kết Nối
        string strConnectionString = "Data Source = DESKTOP-5PSSEM5\\SQLEXPRESS; Initial Catalog=QLBanGiayDep; Integrated Security = True";
        //Đối tượng kết nối
        SqlConnection conn = null;
        //Đối Tượng Dữ Liệu DataTable dtThanhPho
        SqlDataAdapter daTinhThanhPho = null;
        //Đối tượng hiển thị dữ liệu lên Form.
        DataTable dtTinhThanhPho = null;
        bool Them; //Khai báo biến kiểm tra việc thêm hay sửa dữ liệu
        void LoadData()
        {
            try
            {
                //Khởi động connection
                conn = new SqlConnection(strConnectionString);
                //Vận chuyển dữ liệu lên DataTable dtThanhPho
                daTinhThanhPho = new SqlDataAdapter("Select * From THANHPHO", conn);
                dtTinhThanhPho = new DataTable();
                dtTinhThanhPho.Clear();
                daTinhThanhPho.Fill(dtTinhThanhPho);
                //đưa dữ liệu lên DataGridView.
                dgvTHANHPHO.DataSource = dtTinhThanhPho;
                //Thay Đổi độ rộng của cột
                dgvTHANHPHO.AutoResizeColumns();
                // Xóa trống các đối tượng trong Panel
                this.txtThanhPho.ResetText();
                this.txtTenThanhPho.ResetText();
                //Không cho thao tác trên các nút Lưu / Hủy.
                this.btnLuu.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.panel.Enabled = false;
                // Cho thao tác trên các nút Thêm/Sửa/Xóa/Trở Về
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                this.btnTroVe.Enabled = true;
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table THANHPHO. Lỗi Rồi !!!");
            }
        }
        // Xử Lý Form Load
        private void Form4_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        // Xử lý sự kiện Form Closing
        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Giải phóng tài nguyên
            dtTinhThanhPho.Dispose();
            dtTinhThanhPho = null;
            //Hủy Kết Nối
            conn = null;
        }
        // Xử lý sự kiện Reload
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        // Xử lý sự kiện nút Trở Về
        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Buttons Thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            //Kích hoạt biến thêm
            Them = true;
            //Xóa trống đối tượng trong Panel
            this.txtThanhPho.ResetText();
            this.txtTenThanhPho.ResetText();
            //Cho thao tác trên các nút Lưu/Hủy và Panel
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.panel.Enabled = true;
            //Không cho thao tác trên các nút Thêm/Xóa/Trở Về/
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnTroVe.Enabled = false;
            //Đưa con trỏ đến TextField txtThanhPho.
            this.txtThanhPho.Focus();
        }
        // Buttons Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến Sửa.
            Them = false;
            // Cho các thao tác trên Panel.
            this.panel.Enabled = true;
            // thứ tự dòng hiện hành.
            int r = dgvTHANHPHO.CurrentCell.RowIndex;
            // Chuyển Thông tin quan Panel.
            this.txtThanhPho.Text = dgvTHANHPHO.Rows[r].Cells[0].Value.ToString();
            this.txtTenThanhPho.Text = dgvTHANHPHO.Rows[r].Cells[1].Value.ToString();
            // Cho thao tác trên các nút Lưu/Hủy và Panel.
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.panel.Enabled = true;
            // Không cho thao tác trên các nút Thêm/Xóa/Sửa/Trở Về.
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnTroVe.Enabled = false;
            // Đưa con trỏ đến TextField txtMaKH
            this.txtThanhPho.Focus();
        }
        // Button Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Mở Kết Nối
            conn.Open();
            try
            {
                // Thực Hiện Lệnh
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                // Lấy thứ tự Record hiện hành
                int r = dgvTHANHPHO.CurrentCell.RowIndex;
                // Lấy MaKH của Record hiện hành
                string strTHANHPHO = dgvTHANHPHO.Rows[r].Cells[0].Value.ToString();
                // Viết Câu Lệnh SQL
                cmd.CommandText = System.String.Concat("Delete From ThanhPho Where TinhThanhPho =' " + strTHANHPHO + "'");
                cmd.CommandType = CommandType.Text;
                // Thực hiện câu lệnh SQL
                cmd.ExecuteNonQuery();
                // Cập nhật lại DataGridView.
                LoadData();
                // Thông Báo
                MessageBox.Show("Đã Xóa Xong !");
            }
            catch (SqlException)
            {
                MessageBox.Show("Không Xóa Được. Lỗi Rồi !");
            }
            // Đóng Kết Nối
            conn.Close();
        }
        // Buttons Hủy Bỏ
        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            // Xóa trống đối tượng trong Panel
            this.txtThanhPho.ResetText();
            this.txtTenThanhPho.ResetText();
            // Cho thao tác trên các nút Thêm/Xóa/Sửa/Trở Về
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnTroVe.Enabled = true;
            // Không cho thao tác trên các nút Lưu/Hủy Bỏ và Panel.
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.panel.Enabled = false;
        }
        // Buttons Lưu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Mở Kết Nối.
            conn.Open();
            // Thêm dữ liệu.
            if (Them)
            {
                try
                {
                    // Thực hiện lệnh.
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    // Lệnh Insert into
                    cmd.CommandText = System.String.Concat("Insert Into TinhThanhPho values(" + "'" + this.txtThanhPho.Text.ToString() + "', '" + this.txtTenThanhPho.Text.ToString() + "')");
                    cmd.ExecuteNonQuery();
                    // Load dữ liệu DataGridView.
                    LoadData();
                    // Thông Báo.
                    MessageBox.Show("Đã Thêm Xong !");
                }
                catch (SqlException)
                {
                    MessageBox.Show("không thêm được, lỗi rồi !");
                }
            }
            if (!Them)
            {
                // Thực Hiện Lệnh
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                // Thứ tự dòng hiện hành.
                int r = dgvTHANHPHO.CurrentCell.RowIndex;
                // MaKH hiện hành.
                String strTHANHPHO = dgvTHANHPHO.Rows[r].Cells[0].Value.ToString();
                //Câu Lệnh SQL
                cmd.CommandText = System.String.Concat("Update ThanhPho set TenThanhPho = '" + this.txtTenThanhPho.Text.ToString() + "' Where ThanhPho = '" + strTHANHPHO + "'");
                // Cập Nhật
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                // Load lại dữ liệu trên DataGridView.
                LoadData();
                // Thông Báo
                MessageBox.Show("Đã sửa xong !");
            }
            // Đóng kết nối
            conn.Close();
        }

        private void Form4_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLBanGiayDepDataSet1.TinhThanhPho' table. You can move, or remove it, as needed.
            this.tinhThanhPhoTableAdapter.Fill(this.qLBanGiayDepDataSet1.TinhThanhPho);

        }

        
    }
}
