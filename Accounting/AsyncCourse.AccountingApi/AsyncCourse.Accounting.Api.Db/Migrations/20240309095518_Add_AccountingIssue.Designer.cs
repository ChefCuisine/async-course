﻿// <auto-generated />
using System;
using AsyncCourse.Accounting.Api.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AsyncCourse.Accounting.Api.Db.Migrations
{
    [DbContext(typeof(AccountingApiDbContext))]
    [Migration("20240309095518_Add_AccountingIssue")]
    partial class Add_AccountingIssue
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AsyncCourse.Accounting.Api.Db.Dbos.AccountingAccountDbo", b =>
                {
                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("account_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("surname");

                    b.HasKey("AccountId");

                    b.ToTable("accounts");
                });

            modelBuilder.Entity("AsyncCourse.Accounting.Api.Db.Dbos.AccountingIssueDbo", b =>
                {
                    b.Property<Guid>("IssueId")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("AssignedToAccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("assigned_to_accound_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("IssueId");

                    b.ToTable("issues");
                });
#pragma warning restore 612, 618
        }
    }
}