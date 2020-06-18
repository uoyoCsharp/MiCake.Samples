﻿// <auto-generated />
using System;
using MiCakeDemoApplication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MiCakeDemoApplication.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

            modelBuilder.Entity("MiCakeDemoApplication.Domain.BookBoundary.Aggregates.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("BookName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("MiCakeDemoApplication.Domain.UserBoundary.Aggregates.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Avatar")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MiCakeDemoApplication.Domain.UserBoundary.Aggregates.UserWithWechat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("UserID")
                        .HasColumnType("TEXT");

                    b.Property<string>("WeChatOpenID")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserWithWechats");
                });

            modelBuilder.Entity("MiCakeDemoApplication.Domain.BookBoundary.Aggregates.Book", b =>
                {
                    b.OwnsOne("MiCakeDemoApplication.Domain.BookBoundary.Aggregates.BookAuthor", "Author", b1 =>
                        {
                            b1.Property<Guid>("BookId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("FirstName")
                                .HasColumnType("TEXT");

                            b1.Property<string>("LastName")
                                .HasColumnType("TEXT");

                            b1.HasKey("BookId");

                            b1.ToTable("Books");

                            b1.WithOwner()
                                .HasForeignKey("BookId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
