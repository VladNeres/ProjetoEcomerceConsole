﻿// <auto-generated />
using CategoriaApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CategoriaApi.Migrations
{
    [DbContext(typeof(CategoriaContext))]
    [Migration("20220830192855_Categoria")]
    partial class Categoria
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("CategoriaApi.Model.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DataAlteracao")
                        .HasColumnType("text");

                    b.Property<string>("DataCriacao")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });
#pragma warning restore 612, 618
        }
    }
}
