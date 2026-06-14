using CSharp_68PM1_LePhuongNam_0019168;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;   // ← Quan trọng: Thêm dòng này
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLonC_Winform
{
    public partial class UCQuanLySinhVien : UserControl
    {
        private DatabaseDataContext db = new DatabaseDataContext();

        private int currentPage = 1;
        private const int pageSize = 10;
        private List<dynamic> danhSachHienThi = new List<dynamic>();

        public UCQuanLySinhVien()
        {
            InitializeComponent();
        }

        private void UCQuanLySinhVien_Load(object sender, EventArgs e)
        {
            try
            {
                dateNgaySinh.Format = DateTimePickerFormat.Custom;
                dateNgaySinh.CustomFormat = "dd/MM/yyyy";

                LoadDSLH();
                LoadDanhSach();

                DsSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                if (DsSinhVien.Columns["ngaysinh"] != null)
                    DsSinhVien.Columns["ngaysinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void LoadDSLH()
        {
            try
            {
                var dsLH = db.tbl_lophocs.ToList();
                cbLop.DataSource = dsLH;
                cbLop.DisplayMember = "tenlop";
                cbLop.ValueMember = "malop";
                if (cbLop.Items.Count > 0) cbLop.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách lớp: " + ex.Message);
            }
        }

        private void LoadDanhSach(string tuKhoa = "")
        {
            var query = from sv in db.tbl_sinhviens
                        join lop in db.tbl_lophocs on sv.malop equals lop.malop into lj
                        from lop in lj.DefaultIfEmpty()
                        where string.IsNullOrEmpty(tuKhoa) ||
                              (sv.hoten != null && sv.hoten.Contains(tuKhoa)) ||
                              sv.id.ToString().Contains(tuKhoa) ||
                              (sv.malop != null && sv.malop.Contains(tuKhoa))
                        select new
                        {
                            sv.id,
                            sv.hoten,
                            sv.gioitinh,
                            sv.ngaysinh,
                            sv.malop,
                            TenLop = lop != null ? lop.tenlop : ""
                        };

            danhSachHienThi = query.ToList();

            int total = danhSachHienThi.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling((double)total / pageSize));
            if (currentPage > totalPages) currentPage = totalPages;

            DsSinhVien.DataSource = danhSachHienThi
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            lblTrang.Text = $"Trang {currentPage}/{totalPages}  |  {total} bản ghi";
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadDanhSach(txtTimKiem.Text.Trim());
        }

        private void btnThemSinhVien_Click(object sender, EventArgs e)
        {
            // ... (giữ nguyên code thêm sinh viên như trước)
            if (string.IsNullOrWhiteSpace(txtMSSV.Text) || !int.TryParse(txtMSSV.Text.Trim(), out int mSSV))
            {
                MessageBox.Show("MSSV phải là số nguyên!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbGioiTinh.SelectedIndex <= 0 || cbLop.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (db.tbl_sinhviens.Any(s => s.id == mSSV))
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var sv = new tbl_sinhvien
                {
                    id = mSSV,
                    hoten = txtHoTen.Text.Trim(),
                    gioitinh = cbGioiTinh.Text,
                    ngaysinh = dateNgaySinh.Value,
                    malop = cbLop.SelectedValue.ToString()
                };

                db.tbl_sinhviens.InsertOnSubmit(sv);
                db.SubmitChanges();

                MessageBox.Show("Thêm sinh viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSach();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaSV.Text, out int maSV))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa!", "Nhắc nhở",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var sv = db.tbl_sinhviens.FirstOrDefault(s => s.id == maSV);
                if (sv == null)
                {
                    MessageBox.Show("Không tìm thấy sinh viên!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                sv.hoten = txtHoTen.Text.Trim();
                sv.gioitinh = cboGioiTinh.Text;
                sv.ngaysinh = dtpNgaySinh.Value;
                sv.malop = cboLop.SelectedValue?.ToString();

                db.SubmitChanges();
                MessageBox.Show("Sửa sinh viên thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSach();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaSV.Text, out int maSV))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Nhắc nhở",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var sv = db.tbl_sinhviens.FirstOrDefault(s => s.id == maSV);
                if (sv == null)
                {
                    MessageBox.Show("Không tìm thấy sinh viên!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show($"Xóa sinh viên '{sv.hoten}'?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    db.tbl_sinhviens.DeleteOnSubmit(sv);
                    db.SubmitChanges();
                    MessageBox.Show("Xóa sinh viên thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSach();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvSinhVien.Rows[e.RowIndex];
            txtMaSV.Text = row.Cells["id"].Value?.ToString();
            txtHoTen.Text = row.Cells["hoten"].Value?.ToString();
            cboGioiTinh.Text = row.Cells["gioitinh"].Value?.ToString();

            if (DateTime.TryParse(row.Cells["ngaysinh"].Value?.ToString(), out DateTime dt))
                dtpNgaySinh.Value = dt;

            string malop = row.Cells["malop"].Value?.ToString() ?? "";
            for (int i = 0; i < cboLop.Items.Count; i++)
            {
                if (cboLop.Items[i] is tbl_lophoc lop && lop.malop == malop)
                {
                    cboLop.SelectedIndex = i;
                    break;
                }
            }
        }

        private void ClearForm()
        {
            txtMSSV.Clear();
            txtHoTen.Clear();
            cbGioiTinh.SelectedIndex = 0;
            dateNgaySinh.Value = DateTime.Now;
            if (cbLop.Items.Count > 0) cbLop.SelectedIndex = 0;
        }

        // Phân trang
        private void btnDau_Click(object sender, EventArgs e) { currentPage = 1; LoadDanhSach(txtTimKiem.Text.Trim()); }
        private void btnTruoc_Click(object sender, EventArgs e) { if (currentPage > 1) { currentPage--; LoadDanhSach(txtTimKiem.Text.Trim()); } }
        private void btnSau_Click(object sender, EventArgs e) { currentPage++; LoadDanhSach(txtTimKiem.Text.Trim()); }
        private void btnCuoi_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)danhSachHienThi.Count / pageSize);
            currentPage = Math.Max(1, totalPages);
            LoadDanhSach(txtTimKiem.Text.Trim());
        }
    }
}