using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LECHO.Infrastructure
{
    public partial class LECHOContext : DbContext
    {
        public LECHOContext()
        {
        }

        public LECHOContext(DbContextOptions<LECHOContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Choices> Choices { get; set; }
        public virtual DbSet<Faculties> Faculties { get; set; }
        public virtual DbSet<Favourites> Favourites { get; set; }
        public virtual DbSet<Lecturers> Lecturers { get; set; }
        public virtual DbSet<Period> Period { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=LECHO;Username=postgres;Password=v3lamalu");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Choices>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.SubjectId })
                    .HasName("choises_pkey");

                entity.ToTable("choices");

                entity.HasIndex(e => e.SubjectId)
                    .HasName("fki_choises_subject_subject_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("fki_choises_student_user_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Choices)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("choises_subject_subject_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Choices)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("choises_student_user_id");
            });

            modelBuilder.Entity<Faculties>(entity =>
            {
                entity.HasKey(e => e.FacultyId)
                    .HasName("faculties_pkey");

                entity.ToTable("faculties");

                entity.Property(e => e.FacultyId).HasColumnName("faculty_id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(255);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.MapLocationX).HasColumnName("map_location_x");

                entity.Property(e => e.MapLocationY).HasColumnName("map_location_y");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Favourites>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.SubjectId })
                    .HasName("favourites_pkey");

                entity.ToTable("favourites");

                entity.HasIndex(e => e.SubjectId)
                    .HasName("fki_favourites_subject_subject_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("fki_favourites_student_user_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favourites_subject_subject_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favourites_student_user_id");
            });

            modelBuilder.Entity<Lecturers>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("lecturer_pkey");

                entity.ToTable("lecturers");

                entity.HasIndex(e => e.Faculty)
                    .HasName("fki_faculty_id_fkey");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Faculty).HasColumnName("faculty");

                entity.Property(e => e.ProfileLink)
                    .IsRequired()
                    .HasColumnName("profile_link")
                    .HasMaxLength(2049);

                entity.HasOne(d => d.FacultyNavigation)
                    .WithMany(p => p.Lecturers)
                    .HasForeignKey(d => d.Faculty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lecturer_faulty_faculty_id_fkey");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Lecturers)
                    .HasForeignKey<Lecturers>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lecturer_user_user_id_fkey");
            });

            modelBuilder.Entity<Period>(entity =>
            {
                entity.ToTable("period");

                entity.Property(e => e.PeriodId).HasColumnName("period_id");

                entity.Property(e => e.PeriodBegining).HasColumnName("period_begining");

                entity.Property(e => e.PeriodEnd).HasColumnName("period_end");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("student_pkey");

                entity.ToTable("students");

                entity.HasIndex(e => e.Faculty)
                    .HasName("fki_student_faculty_faculty_id_fkey");

                entity.HasIndex(e => e.UserId)
                    .HasName("fki_user_id_fkey");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Course).HasColumnName("course");

                entity.Property(e => e.Faculty).HasColumnName("faculty");

                entity.Property(e => e.GradeBookId)
                    .HasColumnName("grade_book_id")
                    .HasMaxLength(15);

                entity.HasOne(d => d.FacultyNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.Faculty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("faculty_id_fkey");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Students)
                    .HasForeignKey<Students>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_user_user_id_fkey");
            });

            modelBuilder.Entity<Subjects>(entity =>
            {
                entity.HasKey(e => e.SubjectId)
                    .HasName("subjects_pkey");

                entity.ToTable("subjects");

                entity.HasIndex(e => e.FacultyId)
                    .HasName("fki_subject_faculty_faculty_id");

                entity.HasIndex(e => e.LecturerId)
                    .HasName("fki_subject_lecturer_user_id");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.FacultyId).HasColumnName("faculty_id");

                entity.Property(e => e.LecturerId).HasColumnName("lecturer_id");

                entity.Property(e => e.MaxNumberOfStudents).HasColumnName("max_number_of_students");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(70);

                entity.Property(e => e.NumberOfStudents).HasColumnName("number_of_students");

                entity.Property(e => e.Semester).HasColumnName("semester");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subject_faculty_faculty_id");

                entity.HasOne(d => d.Lecturer)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.LecturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subject_lecturer_user_id");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("users_pkey");

                entity.ToTable("users");

                entity.HasIndex(e => e.Login)
                    .HasName("users_login_key")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(255);

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasColumnName("middle_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Role).HasColumnName("role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
