﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Module.Reviews.Persistence;

#nullable disable

namespace Module.Reviews.Persistence.Migrations
{
    [DbContext(typeof(ReviewsDbContext))]
    [Migration("20240531224728_Create_Database")]
    partial class Create_Database
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("reviews")
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Module.Reviews.Domain.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<int>("RequestorId")
                        .HasColumnType("int");

                    b.Property<int>("RevieweeId")
                        .HasColumnType("int");

                    b.Property<int>("ReviewerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Reviews", "reviews");
                });

            modelBuilder.Entity("Module.Reviews.Domain.ReviewResult", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ReviewResults", "reviews");
                });

            modelBuilder.Entity("Module.Reviews.Domain.ReviewTask", b =>
                {
                    b.Property<int>("ReviewId")
                        .HasColumnType("int");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("ReviewId", "TaskId");

                    b.ToTable("ReviewTasks", "reviews");
                });

            modelBuilder.Entity("Module.Reviews.Domain.ReviewResult", b =>
                {
                    b.HasOne("Module.Reviews.Domain.Review", "Review")
                        .WithOne("ReviewResult")
                        .HasForeignKey("Module.Reviews.Domain.ReviewResult", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Review");
                });

            modelBuilder.Entity("Module.Reviews.Domain.ReviewTask", b =>
                {
                    b.HasOne("Module.Reviews.Domain.Review", "Review")
                        .WithMany("ReviewTasks")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Review");
                });

            modelBuilder.Entity("Module.Reviews.Domain.Review", b =>
                {
                    b.Navigation("ReviewResult");

                    b.Navigation("ReviewTasks");
                });
#pragma warning restore 612, 618
        }
    }
}