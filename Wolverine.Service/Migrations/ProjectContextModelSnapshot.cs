﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wolverine.Service.Model;

namespace Wolverine.Service.Migrations
{
    [DbContext(typeof(ProjectContext))]
    partial class ProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("Wolverine.Card", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("GroupID");

                    b.Property<int>("Order");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.HasIndex("GroupID");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Wolverine.Core.Group", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ProjectID");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.HasIndex("ProjectID");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Wolverine.Core.Project", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<string>("Name");

                    b.Property<string>("UnsortedGroupID");

                    b.HasKey("ID");

                    b.HasIndex("UnsortedGroupID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Wolverine.Core.SortSession", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Participant");

                    b.Property<string>("ProjectID");

                    b.Property<DateTimeOffset>("SessionDate");

                    b.HasKey("ID");

                    b.HasIndex("ProjectID");

                    b.ToTable("SortSessions");
                });

            modelBuilder.Entity("Wolverine.Card", b =>
                {
                    b.HasOne("Wolverine.Core.Group")
                        .WithMany("Cards")
                        .HasForeignKey("GroupID");
                });

            modelBuilder.Entity("Wolverine.Core.Group", b =>
                {
                    b.HasOne("Wolverine.Core.Project")
                        .WithMany("DefaultGroups")
                        .HasForeignKey("ProjectID");
                });

            modelBuilder.Entity("Wolverine.Core.Project", b =>
                {
                    b.HasOne("Wolverine.Core.Group", "UnsortedGroup")
                        .WithMany()
                        .HasForeignKey("UnsortedGroupID");
                });

            modelBuilder.Entity("Wolverine.Core.SortSession", b =>
                {
                    b.HasOne("Wolverine.Core.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID");
                });
#pragma warning restore 612, 618
        }
    }
}
