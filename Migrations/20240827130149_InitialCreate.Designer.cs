﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherForecastProvider.DataStorage;

#nullable disable

namespace WeatherForecastProvider.Migrations
{
    [DbContext(typeof(ForecastContext))]
    [Migration("20240827130149_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.33");

            modelBuilder.Entity("WeatherForecastProvider.DataStorage.WeatherForecastDbModel", b =>
                {
                    b.Property<int>("WeatherForecastId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AirportCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("IssueTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("RawTAF")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ValidTo")
                        .HasColumnType("TEXT");

                    b.HasKey("WeatherForecastId");

                    b.ToTable("WeatherForecasts");
                });
#pragma warning restore 612, 618
        }
    }
}
