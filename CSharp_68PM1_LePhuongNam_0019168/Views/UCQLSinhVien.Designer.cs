namespace CSharp_68PM1_LePhuongNam_0019168
{
    partial class UCQLSinhVien
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ===== PANEL TRÁI =====
            var pnlLeft = new Panel
            {
                Width = 440,
                Dock = DockStyle.Left,
                BackColor = Color.FromArgb(245, 245, 245),
                Padding = new Padding(20)
            };

            var lblInfo = new Label { Text = "Thông tin sinh viên", Font = new Font("Segoe UI", 11, FontStyle.Bold), Location = new Point(20, 15), AutoSize = true };

            var lblMaSV = new Label { Text = "Mã sinh viên:", Location = new Point(20, 55), AutoSize = true };
            txtMaSV = new TextBox { Location = new Point(20, 75), Width = 390, Height = 32, ReadOnly = true, BackColor = Color.AliceBlue, Font = new Font("Segoe UI", 10) };

            var lblHoTen = new Label { Text = "Họ và tên:", Location = new Point(20, 120), AutoSize = true };
            txtHoTen = new TextBox { Location = new Point(20, 140), Width = 390, Height = 32, Font = new Font("Segoe UI", 10) };

            var lblNgaySinh = new Label { Text = "Ngày sinh:", Location = new Point(20, 185), AutoSize = true };
            dtpNgaySinh = new DateTimePicker { Location = new Point(20, 205), Width = 390, Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 10) };

            var lblGioiTinh = new Label { Text = "Giới tính:", Location = new Point(20, 250), AutoSize = true };
            cboGioiTinh = new ComboBox { Location = new Point(20, 270), Width = 390, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cboGioiTinh.Items.AddRange(new[] { "Nam", "Nữ" });
            cboGioiTinh.SelectedIndex = 0;

            var lblLop = new Label { Text = "Lớp:", Location = new Point(20, 315), AutoSize = true };
            cboLop = new ComboBox { Location = new Point(20, 335), Width = 390, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };

            btnThem = new Button { Text = "Thêm", Location = new Point(20, 410), Width = 185, Height = 50, BackColor = Color.FromArgb(30, 136, 229), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11, FontStyle.Bold), Cursor = Cursors.Hand };
            btnThem.FlatAppearance.BorderSize = 0;

            btnSua = new Button { Text = "Sửa", Location = new Point(225, 410), Width = 185, Height = 50, BackColor = Color.FromArgb(56, 142, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11, FontStyle.Bold), Cursor = Cursors.Hand };
            btnSua.FlatAppearance.BorderSize = 0;

            btnXoa = new Button { Text = "Xóa", Location = new Point(20, 475), Width = 185, Height = 50, BackColor = Color.FromArgb(211, 47, 47), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11, FontStyle.Bold), Cursor = Cursors.Hand };
            btnXoa.FlatAppearance.BorderSize = 0;

            btnLamMoi = new Button { Text = "Làm mới", Location = new Point(225, 475), Width = 185, Height = 50, BackColor = Color.FromArgb(117, 117, 117), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11, FontStyle.Bold), Cursor = Cursors.Hand };
            btnLamMoi.FlatAppearance.BorderSize = 0;

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;

            pnlLeft.Controls.AddRange(new Control[] { lblInfo, lblMaSV, txtMaSV, lblHoTen, txtHoTen, lblNgaySinh, dtpNgaySinh, lblGioiTinh, cboGioiTinh, lblLop, cboLop, btnThem, btnSua, btnXoa, btnLamMoi });

            // ===== PANEL PHẢI =====
            var pnlRight = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };

            var pnlSearch = new Panel { Height = 65, Dock = DockStyle.Top };
            var lblTimKiem = new Label { Text = "Tìm kiếm (Tên / Mã SV / Lớp):", Location = new Point(0, 8), AutoSize = true, Font = new Font("Segoe UI", 10) };
            txtTimKiem = new TextBox { Location = new Point(0, 30), Width = 380, Height = 32, Font = new Font("Segoe UI", 10) };
            btnTim = new Button { Text = "Tìm", Location = new Point(390, 28), Width = 90, Height = 36, BackColor = Color.FromArgb(55, 71, 79), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };
            btnTim.FlatAppearance.BorderSize = 0;
            btnTim.Click += btnTim_Click;
            pnlSearch.Controls.AddRange(new Control[] { lblTimKiem, txtTimKiem, btnTim });

            dgvSinhVien = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 10),
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 35 }
            };
            dgvSinhVien.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvSinhVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(207, 226, 255);
            dgvSinhVien.EnableHeadersVisualStyles = false;
            dgvSinhVien.CellClick += dgvSinhVien_CellClick;

            var pnlPage = new Panel { Height = 55, Dock = DockStyle.Bottom };
            btnDau = new Button { Text = "<<", Location = new Point(0, 12), Width = 50, Height = 32, FlatStyle = FlatStyle.Flat };
            btnTruoc = new Button { Text = "<", Location = new Point(55, 12), Width = 50, Height = 32, FlatStyle = FlatStyle.Flat };
            lblTrang = new Label { Text = "Trang 1/1  |  0 bản ghi", Location = new Point(115, 18), AutoSize = true, Font = new Font("Segoe UI", 10) };
            btnSau = new Button { Text = ">", Location = new Point(350, 12), Width = 50, Height = 32, FlatStyle = FlatStyle.Flat };
            btnCuoi = new Button { Text = ">>", Location = new Point(405, 12), Width = 50, Height = 32, FlatStyle = FlatStyle.Flat };

            btnDau.Click += btnDau_Click; btnTruoc.Click += btnTruoc_Click;
            btnSau.Click += btnSau_Click; btnCuoi.Click += btnCuoi_Click;

            pnlPage.Controls.AddRange(new Control[] { btnDau, btnTruoc, lblTrang, btnSau, btnCuoi });
            pnlRight.Controls.Add(dgvSinhVien);
            pnlRight.Controls.Add(pnlPage);
            pnlRight.Controls.Add(pnlSearch);

            // ===== USERCONTROL =====
            Controls.Add(pnlRight);
            Controls.Add(pnlLeft);
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            this.Size = new Size(1100, 650);
            Name = "UCQLSinhVien";
            BackColor = Color.White;
        }

        private TextBox txtMaSV, txtHoTen, txtTimKiem;
        private ComboBox cboGioiTinh, cboLop;
        private DateTimePicker dtpNgaySinh;
        private DataGridView dgvSinhVien;
        private Button btnThem, btnSua, btnXoa, btnLamMoi, btnTim;
        private Button btnDau, btnTruoc, btnSau, btnCuoi;
        private Label lblTrang;
    }
}