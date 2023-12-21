using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Labb_3___Skol_Databas.Models;

public partial class HogwartsSkolaContext : DbContext
{
    public HogwartsSkolaContext()
    {
    }

    public HogwartsSkolaContext(DbContextOptions<HogwartsSkolaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Courselist> Courselists { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Personalinfo> Personalinfos { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=HogwartsSkola;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK_Klass");

            entity.ToTable("Class");

            entity.Property(e => e.ClassName).HasMaxLength(20);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK_Kurs");

            entity.ToTable("Course");

            entity.Property(e => e.CourseName).HasMaxLength(50);
            entity.Property(e => e.FkemployeeId).HasColumnName("FKEmployeeId");

            entity.HasOne(d => d.Fkemployee).WithMany(p => p.Courses)
                .HasForeignKey(d => d.FkemployeeId)
                .HasConstraintName("FK_Kurs_Anställda");
        });

        modelBuilder.Entity<Courselist>(entity =>
        {
            entity.HasKey(e => e.CourseListId).HasName("PK_Kurslista");

            entity.ToTable("Courselist");

            entity.Property(e => e.FkcourseId).HasColumnName("FKCourseId");
            entity.Property(e => e.FkstudentId).HasColumnName("FKStudentId");
            entity.Property(e => e.GradeDate).HasColumnType("date");

            entity.HasOne(d => d.Fkcourse).WithMany(p => p.Courselists)
                .HasForeignKey(d => d.FkcourseId)
                .HasConstraintName("FK_Kurslista_Kurs");

            entity.HasOne(d => d.Fkstudent).WithMany(p => p.Courselists)
                .HasForeignKey(d => d.FkstudentId)
                .HasConstraintName("FK_Kurslista_Studenter");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK_Personal_1");

            entity.Property(e => e.EmploymentDate).HasColumnType("date");
            entity.Property(e => e.FkpersonId).HasColumnName("FKPersonId");
            entity.Property(e => e.FkpositionId).HasColumnName("FKPositionId");
            entity.Property(e => e.Salary)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.Fkperson).WithMany(p => p.Employees)
                .HasForeignKey(d => d.FkpersonId)
                .HasConstraintName("FK_Anställda_PersonInfo");

            entity.HasOne(d => d.Fkposition).WithMany(p => p.Employees)
                .HasForeignKey(d => d.FkpositionId)
                .HasConstraintName("FK_Anställda_Befattning1");
        });

        modelBuilder.Entity<Personalinfo>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK_Personal");

            entity.ToTable("Personalinfo");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumer).HasMaxLength(20);
            entity.Property(e => e.SocialSecurityNumber).HasMaxLength(30);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK_Befattning");

            entity.ToTable("Position");

            entity.Property(e => e.PositionName).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK_Studenter");

            entity.Property(e => e.FkclassId).HasColumnName("FKClassID");
            entity.Property(e => e.FkcourseId).HasColumnName("FKCourseID");
            entity.Property(e => e.FkpersonId).HasColumnName("FKPersonId");

            entity.HasOne(d => d.Fkclass).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkclassId)
                .HasConstraintName("FK_Studenter_Klass");

            entity.HasOne(d => d.Fkcourse).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkcourseId)
                .HasConstraintName("FK_Studenter_Kurs");

            entity.HasOne(d => d.Fkperson).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkpersonId)
                .HasConstraintName("FK_Studenter_PersonInfo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
