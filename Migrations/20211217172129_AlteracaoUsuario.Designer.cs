﻿// <auto-generated />
using System;
using LeagueOfLegends_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LeagueOfLegends_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211217172129_AlteracaoUsuario")]
    partial class AlteracaoUsuario
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("LeagueOfLegends_API.Models.Habilidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Cooldown")
                        .HasColumnType("double");

                    b.Property<double>("Dano")
                        .HasColumnType("double");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<int?>("PersonagemId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("TipoDano")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonagemId");

                    b.ToTable("Habilidades");
                });

            modelBuilder.Entity("LeagueOfLegends_API.Models.Personagem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Classe")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Personagens");
                });

            modelBuilder.Entity("LeagueOfLegends_API.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("LeagueOfLegends_API.Models.Habilidade", b =>
                {
                    b.HasOne("LeagueOfLegends_API.Models.Personagem", null)
                        .WithMany("Habilidades")
                        .HasForeignKey("PersonagemId");
                });

            modelBuilder.Entity("LeagueOfLegends_API.Models.Personagem", b =>
                {
                    b.Navigation("Habilidades");
                });
#pragma warning restore 612, 618
        }
    }
}
