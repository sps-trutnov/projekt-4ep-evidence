﻿// <auto-generated />
using System;
using EvidenceProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EvidenceProject.Migrations
{
    [DbContext(typeof(ProjectContext))]
    [Migration("20221023154826_StringLimitation")]
    partial class StringLimitation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EvidenceProject.Data.DataModels.Achievement", b =>
                {
                    b.Property<string>("name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("projectid")
                        .HasColumnType("int");

                    b.HasKey("name");

                    b.HasIndex("projectid");

                    b.ToTable("Achievement");
                });

            modelBuilder.Entity("EvidenceProject.Data.DataModels.DbFile", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("fileData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("fileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("files");
                });

            modelBuilder.Entity("EvidenceProject.Data.DataModels.DialCode", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("_color")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("dialInfoid")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id");

                    b.HasIndex("dialInfoid");

                    b.HasIndex("name")
                        .IsUnique();

                    b.ToTable("dialCodes");
                });

            modelBuilder.Entity("EvidenceProject.Data.DataModels.DialInfo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("desc")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id");

                    b.HasIndex("name")
                        .IsUnique();

                    b.ToTable("dialInfos");
                });

            modelBuilder.Entity("EvidenceProject.Data.DataModels.Project", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("Technology")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("github")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("slack")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("id");

                    b.HasIndex("State");

                    b.HasIndex("Technology");

                    b.HasIndex("Type");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("EvidenceProject.Data.DataModels.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("contactDetails")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("studyField")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("ProjectUser", b =>
                {
                    b.Property<int>("Projectsid")
                        .HasColumnType("int");

                    b.Property<int>("assigneesid")
                        .HasColumnType("int");

                    b.HasKey("Projectsid", "assigneesid");

                    b.HasIndex("assigneesid");

                    b.ToTable("ProjectUser");
                });

            modelBuilder.Entity("EvidenceProject.Data.DataModels.AuthUser", b =>
                {
                    b.HasBaseType("EvidenceProject.Data.DataModels.User");

                    b.Property<bool?>("globalAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasIndex("username")
                        .IsUnique()
                        .HasFilter("[username] IS NOT NULL");

                    b.HasDiscriminator().HasValue("AuthUser");
                });

            modelBuilder.Entity("EvidenceProject.Data.DataModels.Achievement", b =>
                {
                    b.HasOne("EvidenceProject.Data.DataModels.Project", "project")
                        .WithMany("projectAchievements")
                        .HasForeignKey("projectid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("project");
                });

            modelBuilder.Entity("EvidenceProject.Data.DataModels.DialCode", b =>
                {
                    b.HasOne("EvidenceProject.Data.DataModels.DialInfo", "dialInfo")
                        .WithMany("dialCodes")
                        .HasForeignKey("dialInfoid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("dialInfo");
                });

            modelBuilder.Entity("EvidenceProject.Data.DataModels.Project", b =>
                {
                    b.HasOne("EvidenceProject.Data.DataModels.DialCode", "projectState")
                        .WithMany()
                        .HasForeignKey("State")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EvidenceProject.Data.DataModels.DialCode", "projectTechnology")
                        .WithMany()
                        .HasForeignKey("Technology")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EvidenceProject.Data.DataModels.DialCode", "projectType")
                        .WithMany()
                        .HasForeignKey("Type")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("projectState");

                    b.Navigation("projectTechnology");

                    b.Navigation("projectType");
                });

            modelBuilder.Entity("ProjectUser", b =>
                {
                    b.HasOne("EvidenceProject.Data.DataModels.Project", null)
                        .WithMany()
                        .HasForeignKey("Projectsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EvidenceProject.Data.DataModels.User", null)
                        .WithMany()
                        .HasForeignKey("assigneesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EvidenceProject.Data.DataModels.DialInfo", b =>
                {
                    b.Navigation("dialCodes");
                });

            modelBuilder.Entity("EvidenceProject.Data.DataModels.Project", b =>
                {
                    b.Navigation("projectAchievements");
                });
#pragma warning restore 612, 618
        }
    }
}
