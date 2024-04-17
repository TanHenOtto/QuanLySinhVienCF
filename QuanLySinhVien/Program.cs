using Microsoft.EntityFrameworkCore;
using System.Text;

namespace QuanLySinhVien
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Build the host
            var host = CreateHostBuilder(args).Build();

            // Run the migration to ensure the database is created and up-to-date
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<QuanLySinhVienContext>();
                    dbContext.Database.Migrate(); // Apply any pending migrations
                    if (!dbContext.Khoas.Any())
                    {
                        // Nếu bảng trống, chèn dữ liệu
                        dbContext.Khoas.AddRange(
                            new Khoa { Ten = "CNTT" },
                            new Khoa { Ten = "Kinh Te" },
                            new Khoa { Ten = "Ngoai Ngu" }
                        // Thêm các khoa khác nếu cần
                        );
                        dbContext.SaveChanges();
                        Console.WriteLine("Inserted data into the Khoa table.");
                    }
                    if (!dbContext.Lops.Any())
                    {
                        // Nếu bảng trống, chèn dữ liệu
                        dbContext.Lops.AddRange(
                            new Lop { Ten = "L01" ,KhoaId =1},
                            new Lop { Ten = "L02" , KhoaId = 2 },
                            new Lop { Ten = "L03" , KhoaId = 1 },
                            new Lop { Ten = "L04", KhoaId = 3 }
                        // Thêm các lớp khác nếu cần
                        );
                        dbContext.SaveChanges();
                        Console.WriteLine("Inserted data into the Lop table.");
                    }
                    if (!dbContext.SinhViens.Any())
                    {
                        // Nếu bảng trống, chèn dữ liệu
                        dbContext.SinhViens.AddRange(
                            new SinhVien { Ten = "Nguyen Van An", Tuoi = 19, LopId = 1 },
                            new SinhVien { Ten = "Tran Thi Bich", Tuoi = 20, LopId = 2 },
                            new SinhVien { Ten = "Hoang Van Cao", Tuoi = 18, LopId = 1 },
                            new SinhVien { Ten = "Le Van Duy", Tuoi = 19, LopId = 1 },
                            new SinhVien { Ten = "Tran Thi Em", Tuoi = 20, LopId = 2 },
                            new SinhVien { Ten = "Hoang Van Giang", Tuoi = 18, LopId = 3 }
                        // Thêm sinh viên khác nếu cần
                        );
                        dbContext.SaveChanges();
                        Console.WriteLine("Inserted data into the SinhVien table.");
                    }
                    try
                    {
                        var KhoaList = dbContext.Khoas.ToList();
                        var LopList = dbContext.Lops.ToList();
                        var sinhVienList = dbContext.SinhViens.ToList();
                        foreach (var lop in LopList)
                        {
                            Console.WriteLine($"Danh sách sinh viên trong lớp {lop.Ten}:");
                            if (lop.SinhViens != null )
                            {
                                foreach (var sv in lop.SinhViens)
                                {
                                    Console.WriteLine($"Tên: {sv.Ten}, Tuổi: {sv.Tuoi}, LopID: {sv.LopId}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Không có sinh viên.");
                            }
                            Console.WriteLine();
                        }
                        var sinhVienCNTTList = dbContext.SinhViens
                                .Include(sv => sv.Lop) // Bao gồm thông tin về lớp của sinh viên
                                .ThenInclude(lop => lop.Khoa) // Bao gồm thông tin về khoa của lớp
                                .Where(sv => sv.Lop.Khoa.Ten == "CNTT" && sv.Tuoi >= 18 && sv.Tuoi <= 20) // Lọc sinh viên thuộc khoa CNTT
                                .ToList();
                        // Xử lý dữ liệu ở đây
                        Console.WriteLine("Danh sách  thông tin Sinh Viên thuộc khoa CNTT và có tuổi từ 18 đến 20.");
                        foreach (var sinhVien in sinhVienCNTTList)
                        {
                            Console.WriteLine($"Tên: {sinhVien.Ten}, Tuổi: {sinhVien.Tuoi}, Lớp: {sinhVien.Lop.Ten}, Khoa: {sinhVien.Lop.Khoa.Ten}");
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while migrating the database.");
                    Console.WriteLine(ex.Message);
                }
            }
         
            // After the database is created or migrated, perform some operations

            // Run the web application
            host.Run();
           

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
