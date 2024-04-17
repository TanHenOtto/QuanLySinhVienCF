using Microsoft.EntityFrameworkCore;

namespace QuanLySinhVien
{
    public class Lop 
    {
        public int Id { get; set; }
        public string Ten { get; set; }

        public int KhoaId { get; set; }
        public virtual Khoa Khoa { get; set; }
        // Danh sách SinhVien trong lớp
        public virtual ICollection<SinhVien>? SinhViens { get; set; }
    }
}
