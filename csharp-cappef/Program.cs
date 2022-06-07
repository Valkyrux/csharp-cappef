using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_cappef
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            string connectionString = "Data Source=DESKTOP-IN7U4B6;Initial Catalog=schoolDB;User ID=sa;Password=sa;Pooling=False";
            Console.WriteLine("Hello World!");
            using (SchoolContext db = new SchoolContext())
            {
                // Create
                Student nuovoStudente = new Student { Name = "babbano", Email="emaila"};
                db.Add(nuovoStudente);
                db.SaveChanges();
                // Read
                Console.WriteLine("Ottenere lista di Studenti");
                List<Student> students = db.Student.OrderBy(student => student.Name).ToList<Student>();
                foreach (Student student in students)
                {
                    Console.WriteLine(student.Name);
                }
            }*/
            long i = 0;
            long counter = 0;
            DateTime start = DateTime.Now;
            for (i = 0; i < 1000000000; i++)
            {
                if (i.ToString().Contains('2'))
                    counter++;
            }
           

            Console.WriteLine(counter);
            Console.WriteLine((DateTime.Now - start).TotalSeconds);

            counter = 0;
            i = 0;
            start = DateTime.Now;
            foreach (var arg in Genera())
            {
                if (i++ == 1000000000)
                    break;
                if (arg.ToString().Contains('2'))
                    counter++;
            }
            Console.WriteLine(counter);
            Console.WriteLine((DateTime.Now - start).TotalSeconds);
        }
            
            public static System.Collections.Generic.IEnumerable<long> Genera()
            {
                long result = 0;
                while (true)
                {
                    yield return result++;
                }
            }

        [Table("student")]
        [Index(nameof(Email), IsUnique = true)]
        public class Student
        {
            [Key]
            public int StudentId { get; set; }
            [Required]
            public string Name { get; set; }
            public string? Surname { get; set; }
            [Column("student_email")]
            public string Email { get; set; }
            public List<Course> FrequentedCourses { get; set; }
            public List<Review> Reviews { get; set; }
        }

        [Table("course")]
        public class Course
        {
            [Key]
            public int CourseId { get; set; }
            public string Name { get; set; }
            public CourseImage CourseImage { get; set; }

            public List<Student> StudentsEnrolled { get; set; }
        }

        [Table("course_image")]
        public class CourseImage
        {
            public int CourseImageId { get; set; }
            public byte[] Image { get; set; }
            public string Caption { get; set; }
            public int CourseId { get; set; }
            public Course Course { get; set; }
        }

        [Table("reviews")]
        public class Review
        {
            public int ReviewId { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }

            public Student Student { get; set; }
        }

        public class SchoolContext : DbContext
        {
            public DbSet<Student> Student { get; set; }
            public DbSet<Course> Course { get; set; }

            public DbSet<CourseImage> CourseImage { get; set; }

            public DbSet<Review> Review { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-IN7U4B6;Initial Catalog=schoolDB;User ID=sa;Password=sa;Pooling=False");
            }
        }
    }
}