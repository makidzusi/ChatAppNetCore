﻿// <auto-generated />
using ChatApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChatApp.Migrations
{
    [DbContext(typeof(ChatAppContext))]
    [Migration("20230206112406_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ChatApp.DataAccess.Entities.Message", b =>
                {
                    b.Property<int>("SenderId")
                        .HasColumnType("integer");

                    b.Property<int>("RecipientId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("SenderId", "RecipientId");

                    b.HasIndex("RecipientId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ChatApp.DataAccess.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ChatApp.DataAccess.Entities.Message", b =>
                {
                    b.HasOne("ChatApp.DataAccess.Entities.User", "Recipient")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChatApp.DataAccess.Entities.User", "Sender")
                        .WithMany("SendedMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("ChatApp.DataAccess.Entities.User", b =>
                {
                    b.Navigation("ReceivedMessages");

                    b.Navigation("SendedMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
