
using System;
using System.Collections.Generic;
using System.Linq;
using bt3.DAL;

namespace bt3.BUS
{
    public class SinhVienBUS
    {
        private readonly SinhVienDAL dal = new SinhVienDAL();

        public List<SinhVien> GetAll()
        {
            return dal.GetAll();
        }

        public SinhVien GetByMaSV(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
                return null;

            return dal.GetByMaSV(maSV.Trim());
        }

        public List<SinhVien> GetByMaLop(string maLop)
        {
            return dal.GetByMaLop(maLop);
        }

        public List<SinhVien> Search(string keyword)
        {
            keyword = (keyword ?? "").Trim();

            return dal.GetAll()
                .Where(sv =>
                    sv.MaSV.IndexOf(keyword,
                        StringComparison.OrdinalIgnoreCase) >= 0 ||
                    sv.HoTen.IndexOf(keyword,
                        StringComparison.OrdinalIgnoreCase) >= 0 ||
                    sv.Email.IndexOf(keyword,
                        StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        public void Add(SinhVien sv)
        {
            ValidateStudent(sv);

            if (dal.GetByMaSV(sv.MaSV.Trim()) != null)
                throw new Exception("Mã sinh viên đã tồn tại.");

            Normalize(sv);
            dal.Add(sv);
        }

        public void Update(SinhVien sv)
        {
            ValidateStudent(sv);

            if (dal.GetByMaSV(sv.MaSV.Trim()) == null)
                throw new Exception("Không tìm thấy sinh viên cần sửa.");

            Normalize(sv);
            dal.Update(sv);
        }

        public void Delete(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
                throw new Exception("Vui lòng nhập mã sinh viên.");

            if (dal.GetByMaSV(maSV.Trim()) == null)
                throw new Exception("Sinh viên không tồn tại.");

            dal.Delete(maSV.Trim());
        }

        private void ValidateStudent(SinhVien sv)
        {
            if (sv == null)
                throw new Exception("Thông tin sinh viên không hợp lệ.");

            Normalize(sv);

            var errors = sv.IsInValid();

            if (errors.Count > 0)
            {
                throw new Exception(
                    string.Join(
                        Environment.NewLine,
                        errors.Select(e => e.ErrorMessage)));
            }

            if (sv.NgaySinh.Date > DateTime.Today)
                throw new Exception("Ngày sinh không được ở tương lai.");

            if (sv.GioiTinh != "Nam" && sv.GioiTinh != "Nữ")
                throw new Exception("Giới tính không hợp lệ.");

            if (string.IsNullOrWhiteSpace(sv.MaLop))
                throw new Exception("Vui lòng chọn lớp học.");
        }

        private void Normalize(SinhVien sv)
        {
            if (sv == null)
                return;

            sv.MaSV = (sv.MaSV ?? "").Trim();
            sv.HoTen = (sv.HoTen ?? "").Trim();
            sv.Email = (sv.Email ?? "").Trim();
            sv.SoDienThoai = (sv.SoDienThoai ?? "").Trim();
            sv.MaLop = (sv.MaLop ?? "").Trim();
            sv.GioiTinh = (sv.GioiTinh ?? "").Trim();
            sv.TrangThai = (sv.TrangThai ?? "").Trim();
        }
    }
}
