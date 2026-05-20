namespace CSharp_68PM1_LePhuongNam_0019168
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void mnuSinhVien_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            var uc = new UCQLSinhVien { Dock = DockStyle.Fill };
            pnlMain.Controls.Add(uc);
        }

        private void mnuLopHoc_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            var uc = new UCQLLH { Dock = DockStyle.Fill };
            pnlMain.Controls.Add(uc);
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form1().Show();
        }
    }
}