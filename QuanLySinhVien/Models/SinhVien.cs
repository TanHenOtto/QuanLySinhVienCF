using Microsoft.EntityFrameworkCore;

namespace QuanLySinhVien
{
    public class SinhVien 
    {
        
            public int Id { get; set; }
            public string Ten { get; set; }
            public int Tuoi { get; set; }

            // Id của lớp mà SinhVien thuộc về
            public int LopId { get; set; }
            public virtual Lop Lop { get; set; }

    }
}
