namespace CSharp_68PM1_LePhuongNam_0019168
{
    public partial class UCQLSinhVien : UserControl
    {
        private int currentPage = 1;
        private const int pageSize = 10;
        private List<SinhVien> danhSachHienThi = new();

        public UCQLSinhVien()
        {
            InitializeComponent();
            LoadDanhSachLop();
            LoadDanhSach();
        }

        private void LoadDanhSachLop()
        {
            cboLop.Items.Clear();
            foreach (var lop in DataStore.DanhSachLop)
                cboLop.Items.Add($"{lop.MaLop} – {lop.TenLop}");
            if (cboLop.Items.Count > 0) cboLop.SelectedIndex = 0;
        }

        private void LoadDanhSach(string tuKhoa = "")
        {
            danhSachHienThi = DataStore.DanhSachSV
                .Where(sv => string.IsNullOrEmpty(tuKhoa) ||
                    sv.HoTen.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                    sv.MaSV.ToString().Contains(tuKhoa) ||
                    sv.MaLop.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
                .ToList();

            int total = danhSachHienThi.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling((double)total / pageSize));
            if (currentPage > totalPages) currentPage = totalPages;

            dgvSinhVien.DataSource = null;
            dgvSinhVien.DataSource = danhSachHienThi
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .Select(sv => new {
                    sv.MaSV,
                    sv.HoTen,
                    sv.GioiTinh,
                    NgaySinh = sv.NgaySinh.ToString("dd/MM/yyyy"),
                    sv.MaLop,
                    TenLop = DataStore.GetLopCuaSV(sv.MaLop)?.TenLop ?? ""
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
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            { MessageBox.Show("Vui lòng nhập họ tên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (cboLop.SelectedIndex < 0)
            { MessageBox.Show("Vui lòng chọn lớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            int maSV = DataStore.DanhSachSV.Count > 0 ? DataStore.DanhSachSV.Max(s => s.MaSV) + 1 : 1;
            string maLop = cboLop.SelectedItem!.ToString()!.Split('–')[0].Trim();

            DataStore.DanhSachSV.Add(new SinhVien
            {
                MaSV = maSV,
                HoTen = txtHoTen.Text.Trim(),
                GioiTinh = cboGioiTinh.Text,
                NgaySinh = dtpNgaySinh.Value,
                MaLop = maLop
            });

            MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDanhSach();
            LamMoi();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaSV.Text, out int maSV))
            { MessageBox.Show("Vui lòng chọn sinh viên cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var sv = DataStore.DanhSachSV.FirstOrDefault(s => s.MaSV == maSV);
            if (sv == null) return;

            sv.HoTen = txtHoTen.Text.Trim();
            sv.GioiTinh = cboGioiTinh.Text;
            sv.NgaySinh = dtpNgaySinh.Value;
            sv.MaLop = cboLop.SelectedItem!.ToString()!.Split('–')[0].Trim();

            MessageBox.Show("Sửa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDanhSach();
            LamMoi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaSV.Text, out int maSV))
            { MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var sv = DataStore.DanhSachSV.FirstOrDefault(s => s.MaSV == maSV);
            if (sv == null) return;

            if (MessageBox.Show($"Xác nhận xóa sinh viên '{sv.HoTen}'?", "Xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataStore.DanhSachSV.RemoveAll(s => s.MaSV == maSV);
                MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSach();
                LamMoi();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e) => LamMoi();

        private void LamMoi()
        {
            txtMaSV.Text = "";
            txtHoTen.Text = "";
            cboGioiTinh.SelectedIndex = 0;
            dtpNgaySinh.Value = DateTime.Today;
            if (cboLop.Items.Count > 0) cboLop.SelectedIndex = 0;
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvSinhVien.Rows[e.RowIndex];
            txtMaSV.Text = row.Cells["MaSV"].Value?.ToString();
            txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();
            cboGioiTinh.Text = row.Cells["GioiTinh"].Value?.ToString();
            if (DateTime.TryParseExact(row.Cells["NgaySinh"].Value?.ToString(),
                "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dt))
                dtpNgaySinh.Value = dt;
            string maLop = row.Cells["MaLop"].Value?.ToString() ?? "";
            for (int i = 0; i < cboLop.Items.Count; i++)
                if (cboLop.Items[i].ToString()!.StartsWith(maLop))
                { cboLop.SelectedIndex = i; break; }
        }

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