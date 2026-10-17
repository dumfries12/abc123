
using System;
using System.Collections.Generic;
using System.Linq;

namespace bt3.DAL
{
    public class SinhVienDAL
    {
        private static readonly List<SinhVien> sinhViens =
            new List<SinhVien>
            {
                new SinhVien
                {
                    MaSV = "SV0001",
                    HoTen = "Nguyễn Văn An",
                    NgaySinh = new DateTime(2005, 1, 15),
                    GioiTinh = "Nam",
                    Email = "an@example.com",
                    SoDienThoai = "0912345678",
                    Diem = 8.5,
                    MaLop = "CSE0001",
                    TrangThai = "Đang học"
                },
                new SinhVien
                {
                    MaSV = "SV0002",
                    HoTen = "Trần Thị Bình",
                    NgaySinh = new DateTime(2005, 5, 20),
                    GioiTinh = "Nữ",
                    Email = "binh@example.com",
                    SoDienThoai = "0987654321",
                    Diem = 9.0,
                    MaLop = "CSE0002",
                    TrangThai = "Đang học"
                },
                new SinhVien
                {
                    MaSV = "SV0003",
                    HoTen = "Lê Minh Hoàng",
                    NgaySinh = new DateTime(2004, 9, 10),
                    GioiTinh = "Nam",
                    Email = "hoang@example.com",
                    SoDienThoai = "0901234567",
                    Diem = 7.5,
                    MaLop = "CSE0001",
                    TrangThai = "Đang học"
                },
                new SinhVien
                {
                    MaSV = "SV0004",
                    HoTen = "Phạm Thu Hà",
                    NgaySinh = new DateTime(2005, 3, 8),
                    GioiTinh = "Nữ",
                    Email = "ha@example.com",
                    SoDienThoai = "0934567890",
                    Diem = 8.0,
                    MaLop = "CSE0003",
                    TrangThai = "Đang học"
                }
            };

        public List<SinhVien> GetAll()
        {
            return sinhViens;
        }

        public SinhVien GetByMaSV(string maSV)
        {
            return sinhViens.FirstOrDefault(
                x => x.MaSV == maSV);
        }

        public List<SinhVien> GetByMaLop(string maLop)
        {
            return sinhViens
                .Where(x => x.MaLop == maLop)
                .ToList();
        }

        public void Add(SinhVien sv)
        {
            sinhViens.Add(sv);
        }

        public void Update(SinhVien sv)
        {
            SinhVien existing = GetByMaSV(sv.MaSV);

            if (existing != null)
            {
                existing.HoTen = sv.HoTen;
                existing.NgaySinh = sv.NgaySinh;
                existing.GioiTinh = sv.GioiTinh;
                existing.Email = sv.Email;
                existing.SoDienThoai = sv.SoDienThoai;
                existing.Diem = sv.Diem;
                existing.MaLop = sv.MaLop;
                existing.TrangThai = sv.TrangThai;
            }
        }

        public void Delete(string maSV)
        {
            SinhVien sv = GetByMaSV(maSV);

            if (sv != null)
            {
                sinhViens.Remove(sv);
            }
        }
    }
}
