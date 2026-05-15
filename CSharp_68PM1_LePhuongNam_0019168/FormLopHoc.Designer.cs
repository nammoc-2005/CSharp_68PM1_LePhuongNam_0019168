namespace CSharp_68PM1_LePhuongNam_0019168
{
    partial class FormLopHoc
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

            var lblInfo = new Label
            {
                Text = "Thông tin lớp học",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            };

            // Mã ID
            var lblMaID = new Label { Text = "Mã ID:", Location = new Point(20, 55), AutoSize = true };
            txtMaID = new TextBox
            {
                Location = new Point(20, 75),
                Width = 390,
                Height = 32,
                ReadOnly = true,
                BackColor = Color.AliceBlue,
                Font = new Font("Segoe UI", 10)
            };

            // Mã lớp
            var lblMaLop = new Label { Text = "Mã lớp:", Location = new Point(20, 120), AutoSize = true };
            txtMaLop = new TextBox
            {
                Location = new Point(20, 140),
                Width = 390,
                Height = 32,
                Font = new Font("Segoe UI", 10)
            };

            // Tên lớp
            var lblTenLop = new Label { Text = "Tên lớp:", Location = new Point(20, 185), AutoSize = true };
            txtTenLop = new TextBox
            {
                Location = new Point(20, 205),
                Width = 390,
                Height = 32,
                Font = new Font("Segoe UI", 10)
            };

            // Ghi chú
            var lblGhiChu = new Label { Text = "Ghi chú:", Location = new Point(20, 250), AutoSize = true };
            txtGhiChu = new TextBox
            {
                Location = new Point(20, 270),
                Width = 390,
                Height = 32,
                Font = new Font("Segoe UI", 10)
            };

            // ===== CÁC NÚT =====
            btnThem = new Button
            {
                Text = "Thêm",
                Location = new Point(20, 340),
                Width = 185,
                Height = 50,
                BackColor = Color.FromArgb(30, 136, 229),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnThem.FlatAppearance.BorderSize = 0;

            btnSua = new Button
            {
                Text = "Sửa",
                Location = new Point(225, 340),
                Width = 185,
                Height = 50,
                BackColor = Color.FromArgb(56, 142, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSua.FlatAppearance.BorderSize = 0;

            btnXoa = new Button
            {
                Text = "Xóa",
                Location = new Point(20, 405),
                Width = 185,
                Height = 50,
                BackColor = Color.FromArgb(211, 47, 47),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnXoa.FlatAppearance.BorderSize = 0;

            btnLamMoi = new Button
            {
                Text = "Làm mới",
                Location = new Point(225, 405),
                Width = 185,
                Height = 50,
                BackColor = Color.FromArgb(117, 117, 117),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLamMoi.FlatAppearance.BorderSize = 0;

            btnXemSinhVien = new Button
            {
                Text = "Xem danh sách sinh viên",
                Location = new Point(20, 470),
                Width = 390,
                Height = 50,
                BackColor = Color.FromArgb(0, 121, 107),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnXemSinhVien.FlatAppearance.BorderSize = 0;

            // Gán sự kiện
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnXemSinhVien.Click += btnXemSinhVien_Click;

            pnlLeft.Controls.AddRange(new Control[]
            {
                lblInfo,
                lblMaID, txtMaID,
                lblMaLop, txtMaLop,
                lblTenLop, txtTenLop,
                lblGhiChu, txtGhiChu,
                btnThem, btnSua,
                btnXoa, btnLamMoi,
                btnXemSinhVien
            });

            // ===== PANEL PHẢI =====
            var pnlRight = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };

            // Thanh tìm kiếm
            var pnlSearch = new Panel { Height = 65, Dock = DockStyle.Top };
            var lblTimKiem = new Label
            {
                Text = "Tìm kiếm (Mã ID / Mã lớp / Tên lớp):",
                Location = new Point(0, 8),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            txtTimKiem = new TextBox
            {
                Location = new Point(0, 30),
                Width = 380,
                Height = 32,
                Font = new Font("Segoe UI", 10)
            };
            btnTim = new Button
            {
                Text = "Tìm",
                Location = new Point(390, 28),
                Width = 90,
                Height = 36,
                BackColor = Color.FromArgb(55, 71, 79),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnTim.FlatAppearance.BorderSize = 0;
            btnTim.Click += btnTim_Click;
            pnlSearch.Controls.AddRange(new Control[] { lblTimKiem, txtTimKiem, btnTim });

            // DataGridView
            dgvLopHoc = new DataGridView
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
            dgvLopHoc.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvLopHoc.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(207, 226, 255);
            dgvLopHoc.EnableHeadersVisualStyles = false;
            dgvLopHoc.CellClick += dgvLopHoc_CellClick;

            // Phân trang
            var pnlPage = new Panel { Height = 55, Dock = DockStyle.Bottom };
            btnDau = new Button { Text = "<<", Location = new Point(0, 12), Width = 50, Height = 32, FlatStyle = FlatStyle.Flat };
            btnTruoc = new Button { Text = "<", Location = new Point(55, 12), Width = 50, Height = 32, FlatStyle = FlatStyle.Flat };
            lblTrang = new Label
            {
                Text = "Trang 1/1  |  0 bản ghi",
                Location = new Point(115, 18),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            btnSau = new Button { Text = ">", Location = new Point(350, 12), Width = 50, Height = 32, FlatStyle = FlatStyle.Flat };
            btnCuoi = new Button { Text = ">>", Location = new Point(405, 12), Width = 50, Height = 32, FlatStyle = FlatStyle.Flat };

            btnDau.Click += btnDau_Click;
            btnTruoc.Click += btnTruoc_Click;
            btnSau.Click += btnSau_Click;
            btnCuoi.Click += btnCuoi_Click;

            pnlPage.Controls.AddRange(new Control[] { btnDau, btnTruoc, lblTrang, btnSau, btnCuoi });

            pnlRight.Controls.Add(dgvLopHoc);
            pnlRight.Controls.Add(pnlPage);
            pnlRight.Controls.Add(pnlSearch);

            // ===== FORM =====
            Controls.Add(pnlRight);
            Controls.Add(pnlLeft);
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 650);
            Name = "FormLopHoc";
            Text = "Quản lý Lớp Học";
            BackColor = Color.White;
        }

        private TextBox txtMaID, txtMaLop, txtTenLop, txtGhiChu, txtTimKiem;
        private DataGridView dgvLopHoc;
        private Button btnThem, btnSua, btnXoa, btnLamMoi, btnTim, btnXemSinhVien;
        private Button btnDau, btnTruoc, btnSau, btnCuoi;
        private Label lblTrang;
    }
}