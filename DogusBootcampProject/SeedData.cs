using DogusBootcampProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DogusBootcampProject.Data
{
    public static class SeedData
    {
        public static void Initialize(BlogDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Id = 1, Name = "Teknoloji" },
                    new Category { Id = 2, Name = "Doğa" },
                    new Category { Id = 3, Name = "Spor" },
                    new Category { Id = 4, Name = "Eğitim" }
                );
                context.SaveChanges();
            }

            if (!context.Blogs.Any())
            {
                context.Blogs.AddRange(
                    new Blog
                    {
                        Title = "Yapay Zeka Çağına Giriş",
                        Content = "Yapay zeka teknolojileri artık hayatımızın her alanında aktif rol oynamaktadır. Bu yazıda temel kavramlara giriş yapılacaktır.",
                        CategoryId = 1004, // Teknoloji
                        PublishDate = DateTime.Now,
                        UserId = 1, // Admin user
                        ImageUrl = "/uploads/ad535db9-eb09-4abd-a600-08fd967ddec3.jpg"
                    },
                    new Blog
                    {
                        Title = "Veri Bilimi Eğitimi Rehberi",
                        Content = "Eğitimde veri biliminin nasıl kullanılabileceğini ve temel veri bilimi kurslarına nasıl başlanabileceğini bu blog yazısında bulabilirsiniz.",
                        CategoryId = 1007, 
                        PublishDate = DateTime.Now,
                        UserId = 2, 
                        ImageUrl = "/uploads/ef7f019a-6808-4f68-b5fe-b51025364145.png"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
