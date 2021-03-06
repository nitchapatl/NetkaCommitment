﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NetkaCommitment.Data.EFModels
{
    public partial class NetkaCommitmentContext : DbContext
    {
        public NetkaCommitmentContext()
        {
        }

        public NetkaCommitmentContext(DbContextOptions<NetkaCommitmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MCompanyLm> MCompanyLm { get; set; }
        public virtual DbSet<MCompanyWig> MCompanyWig { get; set; }
        public virtual DbSet<MDepartment> MDepartment { get; set; }
        public virtual DbSet<MDepartmentLm> MDepartmentLm { get; set; }
        public virtual DbSet<MDepartmentWig> MDepartmentWig { get; set; }
        public virtual DbSet<MParentUser> MParentUser { get; set; }
        public virtual DbSet<MUser> MUser { get; set; }
        public virtual DbSet<TAccessLog> TAccessLog { get; set; }
        public virtual DbSet<TAccessToken> TAccessToken { get; set; }
        public virtual DbSet<TApprove> TApprove { get; set; }
        public virtual DbSet<TCommitment> TCommitment { get; set; }
        public virtual DbSet<TFirebaseNotification> TFirebaseNotification { get; set; }
        public virtual DbSet<TNotificationLog> TNotificationLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                /*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=db;database=netkacommitment;user=root;password=1234;treattinyasboolean=true", x => x.ServerVersion("8.0.19-mysql"));
                */
                optionsBuilder.UseMySql("server=10.1.8.31;database=netkacommitment;user=nksnms;password=G4xK$8qLa");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MCompanyLm>(entity =>
            {
                entity.HasKey(e => e.CompanyLmId)
                    .HasName("PRIMARY");

                entity.ToTable("m_company_lm");

                entity.HasIndex(e => e.CompanyWigId)
                    .HasName("FK_m_company_lm_m_company_wig");

                entity.Property(e => e.CompanyLmId).HasColumnName("COMPANY_LM_ID");

                entity.Property(e => e.CompanyLmDescription)
                    .HasColumnName("COMPANY_LM_DESCRIPTION")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CompanyLmName)
                    .IsRequired()
                    .HasColumnName("COMPANY_LM_NAME")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CompanyLmSequence).HasColumnName("COMPANY_LM_SEQUENCE");

                entity.Property(e => e.CompanyLmValue).HasColumnName("COMPANY_LM_VALUE");

                entity.Property(e => e.CompanyWigId).HasColumnName("COMPANY_WIG_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.CompanyWig)
                    .WithMany(p => p.MCompanyLm)
                    .HasForeignKey(d => d.CompanyWigId)
                    .HasConstraintName("FK_m_company_lm_m_company_wig");
            });

            modelBuilder.Entity<MCompanyWig>(entity =>
            {
                entity.HasKey(e => e.CompanyWigId)
                    .HasName("PRIMARY");

                entity.ToTable("m_company_wig");

                entity.Property(e => e.CompanyWigId).HasColumnName("COMPANY_WIG_ID");

                entity.Property(e => e.CompanyWigDescription)
                    .HasColumnName("COMPANY_WIG_DESCRIPTION")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CompanyWigName)
                    .IsRequired()
                    .HasColumnName("COMPANY_WIG_NAME")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CompanyWigSequence).HasColumnName("COMPANY_WIG_SEQUENCE");

                entity.Property(e => e.CompanyWigYear).HasColumnName("COMPANY_WIG_YEAR");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<MDepartment>(entity =>
            {
                entity.HasKey(e => e.DepartmentId)
                    .HasName("PRIMARY");

                entity.ToTable("m_department");

                entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.DepartmentDescription)
                    .HasColumnName("DEPARTMENT_DESCRIPTION")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DepartmentName)
                    .HasColumnName("DEPARTMENT_NAME")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DepartmentSequence).HasColumnName("DEPARTMENT_SEQUENCE");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<MDepartmentLm>(entity =>
            {
                entity.HasKey(e => e.LmId)
                    .HasName("PRIMARY");

                entity.ToTable("m_department_lm");

                entity.HasIndex(e => e.DepartmentWigId)
                    .HasName("FK_m_lm_m_wig");

                entity.Property(e => e.LmId).HasColumnName("LM_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.DepartmentWigId).HasColumnName("DEPARTMENT_WIG_ID");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.LmDescription)
                    .HasColumnName("LM_DESCRIPTION")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LmName)
                    .IsRequired()
                    .HasColumnName("LM_NAME")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LmSequence).HasColumnName("LM_SEQUENCE");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.DepartmentWig)
                    .WithMany(p => p.MDepartmentLm)
                    .HasForeignKey(d => d.DepartmentWigId)
                    .HasConstraintName("FK_m_department_lm_m_department_wig");
            });

            modelBuilder.Entity<MDepartmentWig>(entity =>
            {
                entity.HasKey(e => e.DepartmentWigId)
                    .HasName("PRIMARY");

                entity.ToTable("m_department_wig");

                entity.HasIndex(e => e.CompanyLmId)
                    .HasName("FK_m_department_wig_m_company_lm");

                entity.HasIndex(e => e.CompanyWigId)
                    .HasName("FK_m_department_wig_m_company_wig");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("FK_m_department_wig_m_department");

                entity.Property(e => e.DepartmentWigId).HasColumnName("DEPARTMENT_WIG_ID");

                entity.Property(e => e.CompanyLmId).HasColumnName("COMPANY_LM_ID");

                entity.Property(e => e.CompanyWigId).HasColumnName("COMPANY_WIG_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");

                entity.Property(e => e.DepartmentWigDescription)
                    .HasColumnName("DEPARTMENT_WIG_DESCRIPTION")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DepartmentWigName)
                    .IsRequired()
                    .HasColumnName("DEPARTMENT_WIG_NAME")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DepartmentWigSequence).HasColumnName("DEPARTMENT_WIG_SEQUENCE");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.CompanyLm)
                    .WithMany(p => p.MDepartmentWig)
                    .HasForeignKey(d => d.CompanyLmId)
                    .HasConstraintName("FK_m_department_wig_m_company_lm");

                entity.HasOne(d => d.CompanyWig)
                    .WithMany(p => p.MDepartmentWig)
                    .HasForeignKey(d => d.CompanyWigId)
                    .HasConstraintName("FK_m_department_wig_m_company_wig");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.MDepartmentWig)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_m_department_wig_m_department");
            });

            modelBuilder.Entity<MParentUser>(entity =>
            {
                entity.HasKey(e => e.ParentUserId)
                    .HasName("PRIMARY");

                entity.ToTable("m_parent_user");

                entity.HasIndex(e => e.ParentId)
                    .HasName("FK_m_parent_user_m_user_2");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_m_parent_user_m_user");

                entity.Property(e => e.ParentUserId).HasColumnName("PARENT_USER_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ParentId).HasColumnName("PARENT_ID");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.MParentUserParent)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_parent_user_m_user_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MParentUserUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_m_parent_user_m_user");
            });

            modelBuilder.Entity<MUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("m_user");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("FK_m_user_m_department");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.AllowNotification)
                    .HasColumnName("ALLOW_NOTIFICATION")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");

                entity.Property(e => e.DoNotDisturb)
                    .HasColumnName("DO_NOT_DISTURB")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserCode)
                    .IsRequired()
                    .HasColumnName("USER_CODE")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserFirstName)
                    .IsRequired()
                    .HasColumnName("USER_FIRST_NAME")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserFirstNameEn)
                    .IsRequired()
                    .HasColumnName("USER_FIRST_NAME_EN")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserLastName)
                    .IsRequired()
                    .HasColumnName("USER_LAST_NAME")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserLastNameEn)
                    .IsRequired()
                    .HasColumnName("USER_LAST_NAME_EN")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("USER_NAME")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasColumnName("USER_PASSWORD")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'netka123'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserPasswordResetToken)
                    .HasColumnName("USER_PASSWORD_RESET_TOKEN")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.MUser)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_m_user_m_department");
            });

            modelBuilder.Entity<TAccessLog>(entity =>
            {
                entity.HasKey(e => e.AccessLogId)
                    .HasName("PRIMARY");

                entity.ToTable("t_access_log");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_t_access_log_m_user");

                entity.Property(e => e.AccessLogId).HasColumnName("ACCESS_LOG_ID");

                entity.Property(e => e.AccessLogCreatedDate)
                    .HasColumnName("ACCESS_LOG_CREATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.AccessLogDevice)
                    .IsRequired()
                    .HasColumnName("ACCESS_LOG_DEVICE")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AccessLogKey)
                    .IsRequired()
                    .HasColumnName("ACCESS_LOG_KEY")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AccessLogUrl)
                    .IsRequired()
                    .HasColumnName("ACCESS_LOG_URL")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TAccessLog)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_access_log_m_user");
            });

            modelBuilder.Entity<TAccessToken>(entity =>
            {
                entity.HasKey(e => e.AccessTokenId)
                    .HasName("PRIMARY");

                entity.ToTable("t_access_token");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_t_access_token_m_user");

                entity.Property(e => e.AccessTokenId).HasColumnName("ACCESS_TOKEN_ID");

                entity.Property(e => e.AccessTokenCreatedDate)
                    .HasColumnName("ACCESS_TOKEN_CREATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.AccessTokenDevice)
                    .IsRequired()
                    .HasColumnName("ACCESS_TOKEN_DEVICE")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.AccessTokenExpriedDate)
                    .HasColumnName("ACCESS_TOKEN_EXPRIED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.AccessTokenKey)
                    .IsRequired()
                    .HasColumnName("ACCESS_TOKEN_KEY")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.AccessTokenUpdatedDate)
                    .HasColumnName("ACCESS_TOKEN_UPDATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TAccessToken)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_t_access_token_m_user");
            });

            modelBuilder.Entity<TApprove>(entity =>
            {
                entity.HasKey(e => e.ApproveId)
                    .HasName("PRIMARY");

                entity.ToTable("t_approve");

                entity.HasIndex(e => e.ApproveUserId)
                    .HasName("FK_t_approve_m_user");

                entity.HasIndex(e => e.CommitmentId)
                    .HasName("FK_t_approve_t_commitment");

                entity.Property(e => e.ApproveId).HasColumnName("APPROVE_ID");

                entity.Property(e => e.ApproveNo)
                    .HasColumnName("APPROVE_NO")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ApproveRemark)
                    .HasColumnName("APPROVE_REMARK")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ApproveStatus)
                    .IsRequired()
                    .HasColumnName("APPROVE_STATUS")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'Watting for approval.'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ApproveType)
                    .IsRequired()
                    .HasColumnName("APPROVE_TYPE")
                    .HasColumnType("varchar(500)")
                    .HasDefaultValueSql("'ยาก'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ApproveUserId).HasColumnName("APPROVE_USER_ID");

                entity.Property(e => e.CommitmentId).HasColumnName("COMMITMENT_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.ApproveUser)
                    .WithMany(p => p.TApprove)
                    .HasForeignKey(d => d.ApproveUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_approve_m_user");

                entity.HasOne(d => d.Commitment)
                    .WithMany(p => p.TApprove)
                    .HasForeignKey(d => d.CommitmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_approve_t_commitment");
            });

            modelBuilder.Entity<TCommitment>(entity =>
            {
                entity.HasKey(e => e.CommitmentId)
                    .HasName("PRIMARY");

                entity.ToTable("t_commitment");

                entity.HasIndex(e => e.CommitmentLm)
                    .HasName("FK_t_commitment_m_department_lm");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_t_commitment_m_user");

                entity.Property(e => e.CommitmentId).HasColumnName("COMMITMENT_ID");

                entity.Property(e => e.CommitmentDescription)
                    .HasColumnName("COMMITMENT_DESCRIPTION")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CommitmentFinishDate)
                    .HasColumnName("COMMITMENT_FINISH_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.CommitmentIsDeleted)
                    .HasColumnName("COMMITMENT_IS_DELETED")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.CommitmentLm).HasColumnName("COMMITMENT_LM");

                entity.Property(e => e.CommitmentName)
                    .IsRequired()
                    .HasColumnName("COMMITMENT_NAME")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CommitmentNo)
                    .HasColumnName("COMMITMENT_NO")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.CommitmentRemark)
                    .HasColumnName("COMMITMENT_REMARK")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CommitmentStartDate)
                    .HasColumnName("COMMITMENT_START_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.CommitmentStatus)
                    .IsRequired()
                    .HasColumnName("COMMITMENT_STATUS")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'Watting for approval.'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.CommitmentLmNavigation)
                    .WithMany(p => p.TCommitment)
                    .HasForeignKey(d => d.CommitmentLm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_commitment_m_department_lm");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TCommitment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_commitment_m_user");
            });

            modelBuilder.Entity<TFirebaseNotification>(entity =>
            {
                entity.HasKey(e => e.FirebaseId)
                    .HasName("PRIMARY");

                entity.ToTable("t_firebase_notification");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_t_firebase_m_user");

                entity.Property(e => e.FirebaseId).HasColumnName("FIREBASE_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.FirebaseFcmKey)
                    .IsRequired()
                    .HasColumnName("FIREBASE_FCM_KEY")
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FirebaseMacAddress)
                    .IsRequired()
                    .HasColumnName("FIREBASE_MAC_ADDRESS")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FirebasePlatform)
                    .IsRequired()
                    .HasColumnName("FIREBASE_PLATFORM")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TFirebaseNotification)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_t_firebase_m_user");
            });

            modelBuilder.Entity<TNotificationLog>(entity =>
            {
                entity.HasKey(e => e.NotificationId)
                    .HasName("PRIMARY");

                entity.ToTable("t_notification_log");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_t_notification_m_user");

                entity.Property(e => e.NotificationId).HasColumnName("NOTIFICATION_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.NotificationCount)
                    .HasColumnName("NOTIFICATION_COUNT")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.NotificationDescription)
                    .HasColumnName("NOTIFICATION_DESCRIPTION")
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NotificationFcmKey)
                    .IsRequired()
                    .HasColumnName("NOTIFICATION_FCM_KEY")
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NotificationIsSent)
                    .HasColumnName("NOTIFICATION_IS_SENT")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.NotificationLimit)
                    .HasColumnName("NOTIFICATION_LIMIT")
                    .HasDefaultValueSql("'3'");

                entity.Property(e => e.NotificationName)
                    .IsRequired()
                    .HasColumnName("NOTIFICATION_NAME")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NotificationPlatform)
                    .IsRequired()
                    .HasColumnName("NOTIFICATION_PLATFORM")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TNotificationLog)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_notification_m_user");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
