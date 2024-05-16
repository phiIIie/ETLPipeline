﻿// <auto-generated />
using System;
using ETLPipeline.Repository.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ETLPipeline.Repository.Migrations
{
    [DbContext(typeof(ETLPipelineContext))]
    partial class ETLPipelineContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ETLPipeline.Repository.Models.Flights", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double?>("BaroAltitude")
                        .HasColumnType("float");

                    b.Property<string>("Callsign")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CollectedDate")
                        .HasColumnType("datetime2");

                    b.Property<double?>("GeoAltitude")
                        .HasColumnType("float");

                    b.Property<string>("Icao24")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LastContact")
                        .HasColumnType("int");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<bool?>("OnGround")
                        .HasColumnType("bit");

                    b.Property<string>("OriginCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PositionSource")
                        .HasColumnType("int");

                    b.Property<string>("Sensors")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Spi")
                        .HasColumnType("bit");

                    b.Property<string>("Squawk")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TimePosition")
                        .HasColumnType("int");

                    b.Property<double?>("TrueTrack")
                        .HasColumnType("float");

                    b.Property<double?>("Velocity")
                        .HasColumnType("float");

                    b.Property<double?>("VerticalRate")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Flights");
                });
#pragma warning restore 612, 618
        }
    }
}
