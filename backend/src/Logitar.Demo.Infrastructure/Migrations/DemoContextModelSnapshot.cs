﻿// <auto-generated />
using System;
using Logitar.Demo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Logitar.Demo.Infrastructure.Migrations
{
    [DbContext(typeof(DemoContext))]
    partial class DemoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Logitar.Demo.Infrastructure.Entities.ActorEntity", b =>
                {
                    b.Property<int>("ActorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ActorId"));

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(65535)
                        .HasColumnType("character varying(65535)");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Picture")
                        .HasMaxLength(65535)
                        .HasColumnType("character varying(65535)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("ActorId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("Logitar.Demo.Infrastructure.Entities.RealmEntity", b =>
                {
                    b.Property<int>("RealmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RealmId"));

                    b.Property<string>("AggregateId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ClaimMappings")
                        .HasColumnType("jsonb");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CustomAttributes")
                        .HasColumnType("jsonb");

                    b.Property<string>("DefaultLocale")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PasswordSettings")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<bool>("RequireConfirmedAccount")
                        .HasColumnType("boolean");

                    b.Property<bool>("RequireUniqueEmail")
                        .HasColumnType("boolean");

                    b.Property<string>("Secret")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("UniqueName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("UniqueNameNormalized")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("jsonb");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Url")
                        .HasMaxLength(65535)
                        .HasColumnType("character varying(65535)");

                    b.Property<string>("UsernameSettings")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("RealmId");

                    b.HasIndex("AggregateId")
                        .IsUnique();

                    b.HasIndex("CreatedById");

                    b.HasIndex("CreatedOn");

                    b.HasIndex("DisplayName");

                    b.HasIndex("UniqueName");

                    b.HasIndex("UniqueNameNormalized")
                        .IsUnique();

                    b.HasIndex("UpdatedById");

                    b.HasIndex("UpdatedOn");

                    b.ToTable("Realms");
                });
#pragma warning restore 612, 618
        }
    }
}
