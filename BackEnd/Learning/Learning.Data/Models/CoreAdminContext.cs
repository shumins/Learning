using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Learning.Data.Models
{
    public partial class CoreAdminContext : DbContext
    {
        public CoreAdminContext()
        {
        }

        public CoreAdminContext(DbContextOptions<CoreAdminContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Advertisement> Advertisement { get; set; }
        public virtual DbSet<BlogArticle> BlogArticle { get; set; }
        public virtual DbSet<Guestbook> Guestbook { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<ModulePermission> ModulePermission { get; set; }
        public virtual DbSet<OperateLog> OperateLog { get; set; }
        public virtual DbSet<PasswordLib> PasswordLib { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleModulePermission> RoleModulePermission { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<TopicDetail> TopicDetail { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=123.206.78.244,1833;Initial Catalog=CoreAdmin;uid=sa;pwd=q7408695.;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertisement>(entity =>
            {
                entity.Property(e => e.Createdate).HasColumnType("datetime");

                entity.Property(e => e.ImgUrl).HasMaxLength(512);

                entity.Property(e => e.Title).HasMaxLength(64);

                entity.Property(e => e.Url).HasMaxLength(256);
            });

            modelBuilder.Entity<BlogArticle>(entity =>
            {
                entity.HasKey(e => e.BId);

                entity.Property(e => e.BId).HasColumnName("bID");

                entity.Property(e => e.BCreateTime)
                    .HasColumnName("bCreateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.BRemark).HasColumnName("bRemark");

                entity.Property(e => e.BUpdateTime)
                    .HasColumnName("bUpdateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Bcategory).HasColumnName("bcategory");

                entity.Property(e => e.BcommentNum).HasColumnName("bcommentNum");

                entity.Property(e => e.Bcontent)
                    .HasColumnName("bcontent")
                    .HasColumnType("text");

                entity.Property(e => e.Bsubmitter)
                    .HasColumnName("bsubmitter")
                    .HasMaxLength(60);

                entity.Property(e => e.Btitle)
                    .HasColumnName("btitle")
                    .HasMaxLength(256);

                entity.Property(e => e.Btraffic).HasColumnName("btraffic");
            });

            modelBuilder.Entity<Guestbook>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BlogId).HasColumnName("blogId");

                entity.Property(e => e.Body).HasColumnName("body");

                entity.Property(e => e.Createdate)
                    .HasColumnName("createdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ip).HasColumnName("ip");

                entity.Property(e => e.Isshow).HasColumnName("isshow");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.Qq).HasColumnName("QQ");

                entity.Property(e => e.Username).HasColumnName("username");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Icon).HasMaxLength(100);

                entity.Property(e => e.LinkUrl).HasMaxLength(100);

                entity.Property(e => e.ModifyBy).HasMaxLength(50);

                entity.Property(e => e.ModifyTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_dbo.Module_dbo.Module_ParentId");
            });

            modelBuilder.Entity<ModulePermission>(entity =>
            {
                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyBy).HasMaxLength(50);

                entity.Property(e => e.ModifyTime).HasColumnType("datetime");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.ModulePermission)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK_dbo.ModulePermission_dbo.Module_ModuleId");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.ModulePermission)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK_dbo.ModulePermission_dbo.Permission_PermissionId");
            });

            modelBuilder.Entity<OperateLog>(entity =>
            {
                entity.Property(e => e.Ipaddress).HasColumnName("IPAddress");

                entity.Property(e => e.LogTime).HasColumnType("datetime");

                entity.Property(e => e.UserUId).HasColumnName("User_uID");

                entity.HasOne(d => d.UserU)
                    .WithMany(p => p.OperateLog)
                    .HasForeignKey(d => d.UserUId)
                    .HasConstraintName("FK_dbo.OperateLog_dbo.sysUserInfo_User_uID");
            });

            modelBuilder.Entity<PasswordLib>(entity =>
            {
                entity.HasKey(e => e.Plid);

                entity.Property(e => e.Plid).HasColumnName("PLID");

                entity.Property(e => e.PlAccountName)
                    .HasColumnName("plAccountName")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PlCreateTime)
                    .HasColumnName("plCreateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.PlErrorCount).HasColumnName("plErrorCount");

                entity.Property(e => e.PlHintPwd)
                    .HasColumnName("plHintPwd")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PlHintquestion)
                    .HasColumnName("plHintquestion")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PlLastErrTime)
                    .HasColumnName("plLastErrTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.PlPwd)
                    .HasColumnName("plPWD")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PlStatus).HasColumnName("plStatus");

                entity.Property(e => e.PlUpdateTime)
                    .HasColumnName("plUpdateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.PlUrl)
                    .HasColumnName("plURL")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Test)
                    .HasColumnName("test")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Test3)
                    .IsRequired()
                    .HasColumnName("test3")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Component).HasMaxLength(50);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Icon).HasMaxLength(100);

                entity.Property(e => e.Key).HasMaxLength(50);

                entity.Property(e => e.ModifyBy).HasMaxLength(50);

                entity.Property(e => e.ModifyTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.ModifyBy).HasMaxLength(50);

                entity.Property(e => e.ModifyTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<RoleModulePermission>(entity =>
            {
                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyBy).HasMaxLength(50);

                entity.Property(e => e.ModifyTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.Property(e => e.TAuthor)
                    .HasColumnName("tAuthor")
                    .HasMaxLength(200);

                entity.Property(e => e.TCommend).HasColumnName("tCommend");

                entity.Property(e => e.TCreatetime)
                    .HasColumnName("tCreatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.TDetail)
                    .HasColumnName("tDetail")
                    .HasMaxLength(400);

                entity.Property(e => e.TGood).HasColumnName("tGood");

                entity.Property(e => e.TIsDelete).HasColumnName("tIsDelete");

                entity.Property(e => e.TLogo)
                    .HasColumnName("tLogo")
                    .HasMaxLength(200);

                entity.Property(e => e.TName)
                    .HasColumnName("tName")
                    .HasMaxLength(200);

                entity.Property(e => e.TRead).HasColumnName("tRead");

                entity.Property(e => e.TSectendDetail)
                    .HasColumnName("tSectendDetail")
                    .HasMaxLength(200);

                entity.Property(e => e.TUpdatetime)
                    .HasColumnName("tUpdatetime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TopicDetail>(entity =>
            {
                entity.Property(e => e.TdAuthor)
                    .HasColumnName("tdAuthor")
                    .HasMaxLength(200);

                entity.Property(e => e.TdCommend).HasColumnName("tdCommend");

                entity.Property(e => e.TdContent).HasColumnName("tdContent");

                entity.Property(e => e.TdCreatetime)
                    .HasColumnName("tdCreatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.TdDetail)
                    .HasColumnName("tdDetail")
                    .HasMaxLength(400);

                entity.Property(e => e.TdGood).HasColumnName("tdGood");

                entity.Property(e => e.TdIsDelete).HasColumnName("tdIsDelete");

                entity.Property(e => e.TdLogo)
                    .HasColumnName("tdLogo")
                    .HasMaxLength(200);

                entity.Property(e => e.TdName)
                    .HasColumnName("tdName")
                    .HasMaxLength(200);

                entity.Property(e => e.TdRead).HasColumnName("tdRead");

                entity.Property(e => e.TdSectendDetail)
                    .HasColumnName("tdSectendDetail")
                    .HasMaxLength(200);

                entity.Property(e => e.TdTop).HasColumnName("tdTop");

                entity.Property(e => e.TdUpdatetime)
                    .HasColumnName("tdUpdatetime")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.TopicDetail)
                    .HasForeignKey(d => d.TopicId)
                    .HasConstraintName("FK_dbo.TopicDetail_dbo.Topic_TopicId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Addr)
                    .HasColumnName("addr")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Birth)
                    .HasColumnName("birth")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.HeadImg)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.LastErrTime).HasColumnType("datetime");

                entity.Property(e => e.LoginName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.LoginPwd)
                    .HasColumnName("LoginPWD")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.RealName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.Sex).HasColumnName("sex");

                entity.Property(e => e.TdIsDelete).HasColumnName("tdIsDelete");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyBy).HasMaxLength(50);

                entity.Property(e => e.ModifyTime).HasColumnType("datetime");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.UserRole_dbo.Role_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.UserRole_dbo.sysUserInfo_UserId");
            });
        }
    }
}
