namespace CSharp_68PM1_LePhuongNam_0019168
{
    public partial class FormLopHoc : Form
    {
        private int currentPage = 1;
        private const int pageSize = 10;
        private List<LopHoc> danhSachHienThi = new();

        public FormLopHoc()
        {
            InitializeComponent();
            LoadDanhSach();
        }

        private void LoadDanhSach(string tuKhoa = "")
        {
            danhSachHienThi = DataStore.DanhSachLop
                .Where(l => string.IsNullOrEmpty(tuKhoa) ||
                    l.MaID.ToString().Contains(tuKhoa) ||
                    l.MaLop.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                    l.TenLop.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
                .ToList();

            int total = danhSachHienThi.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling((double)total / pageSize));
            if (currentPage > totalPages) currentPage = totalPages;

            dgvLopHoc.DataSource = null;
            dgvLopHoc.DataSource = danhSachHienThi
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .Select(l => new {
                    l.MaID,
                    l.MaLop,
                    l.TenLop,
                    l.GhiChu,
                    SoSinhVien = DataStore.GetSVCuaLop(l.MaLop).Count
                }).ToList();

            lblTrang.Text = $"Trang {currentPage}/{totalPages}  |  {total} bản ghi";
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadDanhSach(txtTimKiem.Text.Trim());
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text))
            { MessageBox.Show("Vui lòng nhập Mã lớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (string.IsNullOrWhiteSpace(txtTenLop.Text))
            { MessageBox.Show("Vui lòng nhập Tên lớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (DataStore.DanhSachLop.Any(l => l.MaLop == txtMaLop.Text.Trim()))
            { MessageBox.Show("Mã lớp đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            int maID = DataStore.DanhSachLop.Count > 0 ? DataStore.DanhSachLop.Max(l => l.MaID) + 1 : 1;
            DataStore.DanhSachLop.Add(new LopHoc
            {
                MaID = maID,
                MaLop = txtMaLop.Text.Trim(),
                TenLop = txtTenLop.Text.Trim(),
                GhiChu = txtGhiChu.Text.Trim()
            });

            MessageBox.Show("Thêm lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDanhSach();
            LamMoi();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaID.Text, out int maID))
            { MessageBox.Show("Vui lòng chọn lớp cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var lop = DataStore.DanhSachLop.FirstOrDefault(l => l.MaID == maID);
            if (lop == null) return;

            // Cập nhật MaLop trong SinhVien nếu MaLop thay đổi
            string maLopCu = lop.MaLop;
            string maLopMoi = txtMaLop.Text.Trim();
            if (maLopCu != maLopMoi)
            {
                if (DataStore.DanhSachLop.Any(l => l.MaLop == maLopMoi && l.MaID != maID))
                { MessageBox.Show("Mã lớp đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                // Cập nhật MaLop cho tất cả sinh viên thuộc lớp cũ
                foreach (var sv in DataStore.DanhSachSV.Where(s => s.MaLop == maLopCu))
                    sv.MaLop = maLopMoi;
            }

            lop.MaLop = maLopMoi;
            lop.TenLop = txtTenLop.Text.Trim();
            lop.GhiChu = txtGhiChu.Text.Trim();

            MessageBox.Show("Sửa lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDanhSach();
            LamMoi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaID.Text, out int maID))
            { MessageBox.Show("Vui lòng chọn lớp cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var lop = DataStore.DanhSachLop.FirstOrDefault(l => l.MaID == maID);
            if (lop == null) return;

            int soSV = DataStore.GetSVCuaLop(lop.MaLop).Count;
            string thongBao = soSV > 0
                ? $"Lớp '{lop.TenLop}' có {soSV} sinh viên.\nXóa lớp sẽ xóa toàn bộ sinh viên thuộc lớp này!\nBạn có chắc chắn?"
                : $"Xác nhận xóa lớp '{lop.TenLop}'?";

            if (MessageBox.Show(thongBao, "Xóa lớp",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                // Xóa sinh viên thuộc lớp trước (quan hệ 1-nhiều)
                DataStore.DanhSachSV.RemoveAll(sv => sv.MaLop == lop.MaLop);
                DataStore.DanhSachLop.RemoveAll(l => l.MaID == maID);

                MessageBox.Show("Xóa lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSach();
                LamMoi();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e) => LamMoi();

        private void LamMoi()
        {
            txtMaID.Text = "";
            txtMaLop.Text = "";
            txtTenLop.Text = "";
            txtGhiChu.Text = "";
            txtMaLop.ReadOnly = false;
        }

        private void dgvLopHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvLopHoc.Rows[e.RowIndex];
            txtMaID.Text = row.Cells["MaID"].Value?.ToString();
            txtMaLop.Text = row.Cells["MaLop"].Value?.ToString();
            txtTenLop.Text = row.Cells["TenLop"].Value?.ToString();
            txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString();
        }

        private void btnXemSinhVien_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLop.Text))
            { MessageBox.Show("Vui lòng chọn lớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var svList = DataStore.GetSVCuaLop(txtMaLop.Text);
            if (svList.Count == 0)
            {
                MessageBox.Show($"Lớp {txtMaLop.Text} chưa có sinh viên.", "Thông báo");
                return;
            }

            // Hiện form danh sách sinh viên của lớp
            var frmXem = new Form
            {
                Text = $"Sinh viên lớp {txtMaLop.Text}",
                Size = new Size(700, 400),
                StartPosition = FormStartPosition.CenterParent
            };
            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = svList.Select(sv => new {
                    sv.MaSV,
                    sv.HoTen,
                    sv.GioiTinh,
                    NgaySinh = sv.NgaySinh.ToString("dd/MM/yyyy"),
                    sv.MaLop
                }).ToList()
            };
            frmXem.Controls.Add(dgv);
            frmXem.ShowDialog();
        }

        // Phân trang
        private void btnDau_Click(object sender, EventArgs e) { currentPage = 1; LoadDanhSach(txtTimKiem.Text); }
        private void btnTruoc_Click(object sender, EventArgs e) { if (currentPage > 1) { currentPage--; LoadDanhSach(txtTimKiem.Text); } }
        private void btnSau_Click(object sender, EventArgs e) { currentPage++; LoadDanhSach(txtTimKiem.Text); }
        private void btnCuoi_Click(object sender, EventArgs e)
        {
            currentPage = Math.Max(1, (int)Math.Ceiling((double)danhSachHienThi.Count / pageSize));
            LoadDanhSach(txtTimKiem.Text);
        }
    }
}