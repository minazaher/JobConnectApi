﻿// <auto-generated />
using System;
using JobConnectApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JobConnectApi.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("JobConnectApi.Models.Chat", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmployerId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("JobSeekerId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmployerId");

                    b.HasIndex("JobSeekerId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("JobConnectApi.Models.Job", b =>
                {
                    b.Property<string>("JobId")
                        .HasColumnType("TEXT");

                    b.Property<string>("AdminId")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmployerId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("JobType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PostDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Salray")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("JobId");

                    b.HasIndex("AdminId");

                    b.HasIndex("EmployerId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("JobConnectApi.Models.Message", b =>
                {
                    b.Property<string>("MessageId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ChatId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RecipientId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("TEXT");

                    b.HasKey("MessageId");

                    b.HasIndex("ChatId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("JobConnectApi.Models.Proposal", b =>
                {
                    b.Property<string>("ProposalId")
                        .HasColumnType("TEXT");

                    b.Property<string>("AttachmentPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CoverLetter")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("JobId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("JobSeekerId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("TEXT");

                    b.HasKey("ProposalId");

                    b.HasIndex("JobId");

                    b.HasIndex("JobSeekerId");

                    b.ToTable("Proposals");
                });

            modelBuilder.Entity("JobJobSeeker", b =>
                {
                    b.Property<string>("ApplicantsId")
                        .HasColumnType("TEXT");

                    b.Property<string>("AppliedJobsJobId")
                        .HasColumnType("TEXT");

                    b.HasKey("ApplicantsId", "AppliedJobsJobId");

                    b.HasIndex("AppliedJobsJobId");

                    b.ToTable("JobApplications", (string)null);
                });

            modelBuilder.Entity("JobJobSeeker1", b =>
                {
                    b.Property<string>("SavedById")
                        .HasColumnType("TEXT");

                    b.Property<string>("SavedJobsJobId")
                        .HasColumnType("TEXT");

                    b.HasKey("SavedById", "SavedJobsJobId");

                    b.HasIndex("SavedJobsJobId");

                    b.ToTable("SavedJobs", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("JobConnectApi.Models.Admin", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("JobConnectApi.Models.Employer", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Industry")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Employer");
                });

            modelBuilder.Entity("JobConnectApi.Models.JobSeeker", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.HasDiscriminator().HasValue("JobSeeker");
                });

            modelBuilder.Entity("JobConnectApi.Models.Chat", b =>
                {
                    b.HasOne("JobConnectApi.Models.Employer", "Employer")
                        .WithMany("Chats")
                        .HasForeignKey("EmployerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobConnectApi.Models.JobSeeker", "JobSeeker")
                        .WithMany("Chats")
                        .HasForeignKey("JobSeekerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employer");

                    b.Navigation("JobSeeker");
                });

            modelBuilder.Entity("JobConnectApi.Models.Job", b =>
                {
                    b.HasOne("JobConnectApi.Models.Admin", "Admin")
                        .WithMany("AcceptedJobs")
                        .HasForeignKey("AdminId");

                    b.HasOne("JobConnectApi.Models.Employer", "Employer")
                        .WithMany("PostedPosts")
                        .HasForeignKey("EmployerId");

                    b.Navigation("Admin");

                    b.Navigation("Employer");
                });

            modelBuilder.Entity("JobConnectApi.Models.Message", b =>
                {
                    b.HasOne("JobConnectApi.Models.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("JobConnectApi.Models.Proposal", b =>
                {
                    b.HasOne("JobConnectApi.Models.Job", "Job")
                        .WithMany("Proposals")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobConnectApi.Models.JobSeeker", "JobSeeker")
                        .WithMany("Proposals")
                        .HasForeignKey("JobSeekerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("JobSeeker");
                });

            modelBuilder.Entity("JobJobSeeker", b =>
                {
                    b.HasOne("JobConnectApi.Models.JobSeeker", null)
                        .WithMany()
                        .HasForeignKey("ApplicantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobConnectApi.Models.Job", null)
                        .WithMany()
                        .HasForeignKey("AppliedJobsJobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobJobSeeker1", b =>
                {
                    b.HasOne("JobConnectApi.Models.JobSeeker", null)
                        .WithMany()
                        .HasForeignKey("SavedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobConnectApi.Models.Job", null)
                        .WithMany()
                        .HasForeignKey("SavedJobsJobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobConnectApi.Models.Chat", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("JobConnectApi.Models.Job", b =>
                {
                    b.Navigation("Proposals");
                });

            modelBuilder.Entity("JobConnectApi.Models.Admin", b =>
                {
                    b.Navigation("AcceptedJobs");
                });

            modelBuilder.Entity("JobConnectApi.Models.Employer", b =>
                {
                    b.Navigation("Chats");

                    b.Navigation("PostedPosts");
                });

            modelBuilder.Entity("JobConnectApi.Models.JobSeeker", b =>
                {
                    b.Navigation("Chats");

                    b.Navigation("Proposals");
                });
#pragma warning restore 612, 618
        }
    }
}
