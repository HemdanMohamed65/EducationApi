using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EduApplication.Models;

public partial class EduApplicationContext : DbContext
{
    public EduApplicationContext()
    {
    }

    public EduApplicationContext(DbContextOptions<EduApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alphabet> Alphabets { get; set; }
    public virtual DbSet<Animal> Animals { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Child> Children { get; set; }
    public virtual DbSet<Child_Content> Child_Contents { get; set; }
    public virtual DbSet<Number> Numbers { get; set; }
    public virtual DbSet<Parents> Parents { get; set; }
    public virtual DbSet<Story> Stories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alphabet>(entity =>
        {
            entity.HasKey(e => e.letter_id).HasName("PK__Alphabet__A6F3C31C44939557");

            entity.HasIndex(e => e.letter, "UQ__Alphabet__853DC438315AEE15").IsUnique();

            entity.Property(e => e.audio_url)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.letter)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.photo_url)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.category).WithMany(p => p.Alphabets)
                .HasForeignKey(d => d.category_id)
                .HasConstraintName("FK__Alphabets__categ__412EB0B6");
        });

        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.animal_id).HasName("PK__Animals__DE680F9270C62114");

            entity.HasIndex(e => e.animal_name, "UQ__Animals__FB9F6D4C77F05DB0").IsUnique();

            entity.Property(e => e.animal_name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.audio_url)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.photo_url)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.category).WithMany(p => p.Animals)
                .HasForeignKey(d => d.category_id)
                .HasConstraintName("FK__Animals__categor__48CFD27E");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.category_id).HasName("PK__Category__D54EE9B456306F9F");

            entity.ToTable("Category");

            entity.HasIndex(e => e.category_name, "UQ__Category__5189E255AC09005F").IsUnique();

            entity.Property(e => e.category_name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.child_id).HasName("PK__Children__015ADC0512E1D134");

            entity.Property(e => e.name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.parent).WithMany(p => p.Children)
                .HasForeignKey(d => d.parent_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Children__parent__3D5E1FD2");
        });

        modelBuilder.Entity<Child_Content>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Child_Co__3213E83F4D9B9CC0");

            entity.ToTable("Child_Content");

            entity.HasOne(d => d.category).WithMany(p => p.Child_Contents)
                .HasForeignKey(d => d.category_id)
                .HasConstraintName("FK__Child_Con__categ__4F7CD00D");

            entity.HasOne(d => d.child).WithMany(p => p.Child_Contents)
                .HasForeignKey(d => d.child_id)
                .HasConstraintName("FK__Child_Con__child__4E88ABD4");
        });

        modelBuilder.Entity<Number>(entity =>
        {
            entity.HasKey(e => e.number_id).HasName("PK__Numbers__4BE613D35F479957");

            entity.HasIndex(e => e.number_value, "UQ__Numbers__2C8F03EC91AC93AB").IsUnique();

            entity.Property(e => e.audio_url)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.photo_url)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.category).WithMany(p => p.Numbers)
                .HasForeignKey(d => d.category_id)
                .HasConstraintName("FK__Numbers__categor__44FF419A");
        });

        modelBuilder.Entity<Parents>(entity =>
        {
            entity.HasKey(e => e.parent_id).HasName("PK__Parents__F2A60819CB14ED86");

            entity.HasIndex(e => e.email, "UQ__Parents__AB6E616432BAB267").IsUnique();

            entity.Property(e => e.email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.password_hash)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            // إضافة الحقل OTP هنا
            entity.Property(e => e.otp)
                .HasMaxLength(6)  // مثلاً إذا كان طوله 6 أرقام
                .IsUnicode(false); // إذا كانت الـ OTP تتكون من أرقام فقط
        });

        modelBuilder.Entity<Story>(entity =>
        {
            entity.HasKey(e => e.story_id).HasName("PK__Stories__66339C5643A8DF6C");

            entity.Property(e => e.photo_url)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.title)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.video_url)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.category).WithMany(p => p.Stories)
                .HasForeignKey(d => d.category_id)
                .HasConstraintName("FK__Stories__categor__4BAC3F29");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
