namespace WeChatPro.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<SysModule> SysModule { get; set; }
        public virtual DbSet<SysSample> SysSample { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysModule>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<SysModule>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<SysModule>()
                .Property(e => e.EnglishName)
                .IsUnicode(false);

            modelBuilder.Entity<SysModule>()
                .Property(e => e.ParentId)
                .IsUnicode(false);

            modelBuilder.Entity<SysModule>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<SysModule>()
                .Property(e => e.Iconic)
                .IsUnicode(false);

            modelBuilder.Entity<SysModule>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<SysModule>()
                .Property(e => e.CreatePerson)
                .IsUnicode(false);

            modelBuilder.Entity<SysModule>()
                .Property(e => e.Version)
                .IsFixedLength();

            modelBuilder.Entity<SysModule>()
                .HasMany(e => e.SysModule1)
                .WithOptional(e => e.SysModule2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<SysSample>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<SysSample>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<SysSample>()
                .Property(e => e.Photo)
                .IsUnicode(false);

            modelBuilder.Entity<SysSample>()
                .Property(e => e.Note)
                .IsUnicode(false);
        }
    }
}
