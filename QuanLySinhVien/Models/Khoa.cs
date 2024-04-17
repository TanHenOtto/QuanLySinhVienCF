using Microsoft.EntityFrameworkCore;

namespace QuanLySinhVien
{
    public class Khoa 
    {
        public int Id { get; set; }
        public string Ten { get; set; }

        // Virtual property
        public virtual ICollection<Lop>? Lops { get; set; }
    }
}
