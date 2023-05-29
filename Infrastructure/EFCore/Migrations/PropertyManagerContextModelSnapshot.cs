﻿// <auto-generated />
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.EFCore.Migrations
{
    [DbContext(typeof(PropertyManagerContext))]
    partial class PropertyManagerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Repository.Entities.ArchivedChoreStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CompletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerChoreId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoneBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RowCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RowModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("RowVersion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ArchivedChoreStatuses");
                });

            modelBuilder.Entity("Core.Repository.Entities.Area", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CityId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RowCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RowModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("RowVersion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("Core.Repository.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RowCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RowModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("RowVersion")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Core.Repository.Entities.Chore", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Reference")
                        .HasColumnType("int");

                    b.Property<DateTime>("RowCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RowModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("RowVersion")
                        .HasColumnType("int");

                    b.Property<string>("SubCategoryId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Chores");
                });

            modelBuilder.Entity("Core.Repository.Entities.ChoreComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerChoreId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RowCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RowModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("RowVersion")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ChoreComments");
                });

            modelBuilder.Entity("Core.Repository.Entities.ChoreStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CompletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerChoreId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoneBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RowCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RowModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("RowVersion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ChoreStatuses");
                });

            modelBuilder.Entity("Core.Repository.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RowCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RowModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("RowVersion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Core.Repository.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AreaId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RowCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RowModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("RowVersion")
                        .HasColumnType("int");

                    b.Property<string>("TeamId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Core.Repository.Entities.CustomerChore", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChoreId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<string>("PeriodicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RowCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RowModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("RowVersion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CustomerChores");
                });

            modelBuilder.Entity("Core.Repository.Entities.Periodic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RowCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RowModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("RowVersion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Periodics");
                });

            modelBuilder.Entity("Core.Repository.Entities.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RowCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RowModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("RowVersion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Core.Repository.Entities.TeamMember", b =>
                {
                    b.Property<string>("TeamId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsTemporary")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RowCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RowModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("RowVersion")
                        .HasColumnType("int");

                    b.HasKey("TeamId", "UserId");

                    b.ToTable("TeamMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
