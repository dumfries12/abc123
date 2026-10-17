
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bt3
{
    public class LopHoc
    {
        [Required(ErrorMessage = "Mã lớp không được để trống")]
        [StringLength(20, ErrorMessage = "Mã lớp tối đa 20 ký tự")]
        public string MaLop { get; set; }

        [Required(ErrorMessage = "Tên lớp không được để trống")]
        [StringLength(100, ErrorMessage = "Tên lớp tối đa 100 ký tự")]
        public string TenLop { get; set; }

        public List<SinhVien> SinhViens { get; set; }
            = new List<SinhVien>();

        public List<ValidationResult> IsInValid()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);

            Validator.TryValidateObject(
                this,
                context,
                results,
                true
            );

            return results;
        }
    }
}
