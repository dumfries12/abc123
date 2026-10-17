
using System.Collections.Generic;
using System.Linq;

namespace bt3.DAL
{
    public class LopHocDAL
    {
        private static readonly List<LopHoc> lopHocs =
            new List<LopHoc>
            {
                new LopHoc
                {
                    MaLop = "CSE0001",
                    TenLop = "Kỹ thuật phần mềm 01"
                },
                new LopHoc
                {
                    MaLop = "CSE0002",
                    TenLop = "Kỹ thuật phần mềm 02"
                },
                new LopHoc
                {
                    MaLop = "CSE0003",
                    TenLop = "Khoa học dữ liệu 01"
                }
            };

        public List<LopHoc> GetAll()
        {
            return lopHocs;
        }

        public LopHoc GetByMaLop(string maLop)
        {
            return lopHocs.FirstOrDefault(
                x => x.MaLop == maLop);
        }

        public void Add(LopHoc lop)
        {
            lopHocs.Add(lop);
        }

        public void Update(LopHoc lop)
        {
            LopHoc existing = GetByMaLop(lop.MaLop);

            if (existing != null)
            {
                existing.TenLop = lop.TenLop;
            }
        }

        public void Delete(string maLop)
        {
            LopHoc lop = GetByMaLop(maLop);

            if (lop != null)
            {
                lopHocs.Remove(lop);
            }
        }
    }
}
