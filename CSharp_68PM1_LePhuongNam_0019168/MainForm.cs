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
            ShowForm(new FormSinhVien());
        }

        private void mnuLopHoc_Click(object sender, EventArgs e)
        {
            ShowForm(new FormLopHoc());
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form1().Show();
        }

        private void ShowForm(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(form);
            form.Show();
        }
    }
}