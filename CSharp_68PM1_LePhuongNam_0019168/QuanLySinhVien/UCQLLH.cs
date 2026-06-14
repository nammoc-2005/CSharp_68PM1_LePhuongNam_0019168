using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CSharp_68PM1_LePhuongNam_0019168
{
    public partial class UCQLLH : UserControl
    {
        private DatabaseDataContext db = new DatabaseDataContext();
        private int currentPage = 1;
        private const int pageSize = 10;
        private List<tbl_lophoc> danhSachHienThi = new();

        public UCQLLH()
        {
            InitializeComponent();
        }

        private void UCQLLH_Load(object sender, EventArgs e)
        {
            LoadDanhSach();
            dgvLopHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadDanhSach(string tuKhoa = "")
        {
            try
            {
                danhSachHienThi = db.tbl_lophocs
                    .Where(l => string.IsNullOrEmpty(tuKhoa) ||
                                l.malop.Contains(tuKhoa) ||
                                l.tenlop.Contains(tuKhoa))
                    .ToList();

                int total = danhSachHienThi.Count;
                int totalPages = Math.Max(1, (int)Math.Ceiling((double)total / pageSize));
                if (currentPage > totalPages) currentPage = totalPages;

                dgvLopHoc.DataSource = danhSachHienThi
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(l => new {
                        l.malop,
                        l.tenlop,
                        l.ghichu,
                        SoSinhVien = db.tbl_sinhviens.Count(sv => sv.malop == l.malop)
                    }).ToList();

                lblTrang.Text = $"Trang {currentPage}/{totalPages}  |  {total} bản ghi";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadDanhSach(txtTimKiem.Text.Trim());
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text))
            { MessageBox.Show("Vui lòng nhập Mã lớp!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (string.IsNullOrWhiteSpace(txtTenLop.Text))
            { MessageBox.Show("Vui lòng nhập Tên lớp!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            try
            {
                if (db.tbl_lophocs.Any(l => l.malop == txtMaLop.Text.Trim()))
                { MessageBox.Show("Mã lớp đã tồn tại!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                var lop = new tbl_lophoc
                {
                    malop = txtMaLop.Text.Trim(),
                    tenlop = txtTenLop.Text.Trim(),
                    ghichu = txtGhiChu.Text.Trim()
                };

                db.tbl_lophocs.InsertOnSubmit(lop);
                db.SubmitChanges();
                MessageBox.Show("Thêm lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (string.IsNullOrEmpty(txtMaLop.Text))
            { MessageBox.Show("Vui lòng chọn lớp cần sửa!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            try
            {
                var lop = db.tbl_lophocs.FirstOrDefault(l => l.malop == txtMaLop.Text.Trim());
                if (lop == null) return;

                lop.tenlop = txtTenLop.Text.Trim();
                lop.ghichu = txtGhiChu.Text.Trim();

                db.SubmitChanges();
                MessageBox.Show("Sửa lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSach();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLop.Text))
            { MessageBox.Show("Vui lòng chọn lớp cần xóa!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            try
            {
                var lop = db.tbl_lophocs.FirstOrDefault(l => l.malop == txtMaLop.Text.Trim());
                if (lop == null) return;

                int soSV = db.tbl_sinhviens.Count(sv => sv.malop == lop.malop);
                string msg = soSV > 0
                    ? $"Lớp '{lop.tenlop}' có {soSV} sinh viên.\nXóa lớp sẽ xóa toàn bộ sinh viên!\nBạn có chắc chắn?"
                    : $"Xác nhận xóa lớp '{lop.tenlop}'?";

                if (MessageBox.Show(msg, "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    // Xóa sinh viên thuộc lớp trước (quan hệ 1-nhiều)
                    var svList = db.tbl_sinhviens.Where(sv => sv.malop == lop.malop).ToList();
                    db.tbl_sinhviens.DeleteAllOnSubmit(svList);
                    db.tbl_lophocs.DeleteOnSubmit(lop);
                    db.SubmitChanges();
                    MessageBox.Show("Xóa lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSach();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e) => ClearForm();

        private void ClearForm()
        {
            txtMaLop.Text = "";
            txtTenLop.Text = "";
            txtGhiChu.Text = "";
            txtMaLop.ReadOnly = false;
        }

        private void dgvLopHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvLopHoc.Rows[e.RowIndex];
            txtMaLop.Text = row.Cells["malop"].Value?.ToString();
            txtTenLop.Text = row.Cells["tenlop"].Value?.ToString();
            txtGhiChu.Text = row.Cells["ghichu"].Value?.ToString();
            txtMaLop.ReadOnly = true; // Không cho sửa mã lớp khi đang edit
        }

        private void btnDau_Click(object sender, EventArgs e) { currentPage = 1; LoadDanhSach(txtTimKiem.Text); }
        private void btnTruoc_Click(object sender, EventArgs e) { if (currentPage > 1) { currentPage--; LoadDanhSach(txtTimKiem.Text); } }
        private void btnSau_Click(object sender, EventArgs e)
        {
            int totalPages = Math.Max(1, (int)Math.Ceiling((double)danhSachHienThi.Count / pageSize));
            if (currentPage < totalPages) { currentPage++; LoadDanhSach(txtTimKiem.Text); }
        }
        private void btnCuoi_Click(object sender, EventArgs e)
        {
            currentPage = Math.Max(1, (int)Math.Ceiling((double)danhSachHienThi.Count / pageSize));
            LoadDanhSach(txtTimKiem.Text);
        }
    }
}