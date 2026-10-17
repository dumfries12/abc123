
using System;
using System.Collections.Generic;
using System.Linq;
using bt3.DAL;

namespace bt3.BUS
{
    public class LopHocBUS
    {
        private readonly LopHocDAL dal = new LopHocDAL();

        public List<LopHoc> GetAll()
        {
            return dal.GetAll();
        }

        public LopHoc GetByMaLop(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                return null;

            return dal.GetByMaLop(maLop.Trim());
        }

        public void Add(LopHoc lop)
        {
            ValidateClass(lop);

            if (dal.GetByMaLop(lop.MaLop.Trim()) != null)
                throw new Exception("Mã lớp đã tồn tại.");

            lop.MaLop = lop.MaLop.Trim();
            lop.TenLop = lop.TenLop.Trim();

            dal.Add(lop);
        }

        public void Update(LopHoc lop)
        {
            ValidateClass(lop);

            if (dal.GetByMaLop(lop.MaLop.Trim()) == null)
                throw new Exception("Không tìm thấy lớp cần sửa.");

            lop.MaLop = lop.MaLop.Trim();
            lop.TenLop = lop.TenLop.Trim();

            dal.Update(lop);
        }

        public void Delete(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                throw new Exception("Vui lòng chọn lớp.");

            if (dal.GetByMaLop(maLop.Trim()) == null)
                throw new Exception("Lớp không tồn tại.");

            if (dal.GetByMaLop(maLop.Trim()).SinhViens.Count > 0)
                throw new Exception("Không thể xóa lớp đang có sinh viên.");

            dal.Delete(maLop.Trim());
        }

        private void ValidateClass(LopHoc lop)
        {
            if (lop == null)
                throw new Exception("Thông tin lớp không hợp lệ.");

            lop.MaLop = (lop.MaLop ?? "").Trim();
            lop.TenLop = (lop.TenLop ?? "").Trim();

            var errors = lop.IsInValid();

            if (errors.Count > 0)
            {
                throw new Exception(
                    string.Join(
                        Environment.NewLine,
                        errors.Select(e => e.ErrorMessage)));
            }
        }
    }
}
