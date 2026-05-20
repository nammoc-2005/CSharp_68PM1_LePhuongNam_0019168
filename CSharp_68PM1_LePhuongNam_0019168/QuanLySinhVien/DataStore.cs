namespace CSharp_68PM1_LePhuongNam_0019168
{
    public static class DataStore
    {
        public static List<LopHoc> DanhSachLop = new List<LopHoc>
        {
            new LopHoc { MaID = 1, MaLop = "68PM1", TenLop = "Lớp 68PM1", GhiChu = "abc" },
            new LopHoc { MaID = 2, MaLop = "68PM2", TenLop = "Lớp 68PM2", GhiChu = "xyz" },
        };

        public static List<SinhVien> DanhSachSV = new List<SinhVien>
        {
            new SinhVien { MaSV = 1, HoTen = "Nguyễn Văn A", GioiTinh = "Nam", NgaySinh = new DateTime(2005,3,11), MaLop = "68PM1" },
            new SinhVien { MaSV = 2, HoTen = "Nguyễn Văn B", GioiTinh = "Nam", NgaySinh = new DateTime(2005,3,11), MaLop = "68PM2" },
            new SinhVien { MaSV = 3, HoTen = "Trần Văn C",   GioiTinh = "Nam", NgaySinh = new DateTime(2005,3,21), MaLop = "68PM2" },
        };

        // Lấy lớp của sinh viên (quan hệ 1-nhiều)
        public static LopHoc? GetLopCuaSV(string maLop)
            => DanhSachLop.FirstOrDefault(l => l.MaLop == maLop);

        // Lấy danh sách sinh viên của 1 lớp
        public static List<SinhVien> GetSVCuaLop(string maLop)
            => DanhSachSV.Where(sv => sv.MaLop == maLop).ToList();
    }
}