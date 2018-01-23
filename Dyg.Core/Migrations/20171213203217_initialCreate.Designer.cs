﻿// <auto-generated />
using Dyg.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Dyg.Core.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171213203217_initialCreate")]
    partial class initialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dyg.Core.Model.Brand", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<bool>("IsPublished")
                        .HasMaxLength(200);

                    b.Property<string>("Name");

                    b.Property<DateTime>("PublishDate");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Dyg.Core.Model.BrandCategory", b =>
                {
                    b.Property<string>("BrandId");

                    b.Property<string>("CategoryId");

                    b.HasKey("BrandId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("BrandCategories");
                });

            modelBuilder.Entity("Dyg.Core.Model.Category", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<bool>("IsPublished")
                        .HasMaxLength(200);

                    b.Property<string>("Name");

                    b.Property<string>("ParentCategoryId");

                    b.Property<DateTime>("PublishDate");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Dyg.Core.Model.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrandId")
                        .IsRequired();

                    b.Property<string>("CategoryId")
                        .IsRequired();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<bool>("IsPublished")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<string>("Photo")
                        .HasMaxLength(200);

                    b.Property<string>("Price")
                        .IsRequired();

                    b.Property<DateTime>("PublishDate");

                    b.Property<string>("Stock")
                        .IsRequired();

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Dyg.Core.Model.BrandCategory", b =>
                {
                    b.HasOne("Dyg.Core.Model.Brand", "Brand")
                        .WithMany("BrandCategories")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Dyg.Core.Model.Category", "Category")
                        .WithMany("BrandCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dyg.Core.Model.Category", b =>
                {
                    b.HasOne("Dyg.Core.Model.Category", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("ParentCategoryId");
                });

            modelBuilder.Entity("Dyg.Core.Model.Product", b =>
                {
                    b.HasOne("Dyg.Core.Model.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dyg.Core.Model.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
