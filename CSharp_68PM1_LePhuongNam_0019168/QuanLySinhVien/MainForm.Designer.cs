namespace CSharp_68PM1_LePhuongNam_0019168
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            mnuSinhVien = new ToolStripMenuItem();
            mnuLopHoc = new ToolStripMenuItem();
            mnuDangXuat = new ToolStripMenuItem();
            pnlMain = new Panel();

            menuStrip1.SuspendLayout();
            SuspendLayout();

            // menuStrip1
            menuStrip1.Items.AddRange(new ToolStripItem[] { mnuSinhVien, mnuLopHoc, mnuDangXuat });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1000, 28);

            // mnuSinhVien
            mnuSinhVien.Name = "mnuSinhVien";
            mnuSinhVien.Text = "Quản lý Sinh Viên";
            mnuSinhVien.Click += mnuSinhVien_Click;

            // mnuLopHoc
            mnuLopHoc.Name = "mnuLopHoc";
            mnuLopHoc.Text = "Quản lý Lớp Học";
            mnuLopHoc.Click += mnuLopHoc_Click;

            // mnuDangXuat
            mnuDangXuat.Name = "mnuDangXuat";
            mnuDangXuat.Text = "Đăng xuất";
            mnuDangXuat.Click += mnuDangXuat_Click;

            // pnlMain
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 28);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1000, 572);

            // MainForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(pnlMain);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Quản lý Sinh Viên";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private MenuStrip menuStrip1;
        private ToolStripMenuItem mnuSinhVien;
        private ToolStripMenuItem mnuLopHoc;
        private ToolStripMenuItem mnuDangXuat;
        private Panel pnlMain;
    }
}