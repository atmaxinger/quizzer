﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quizzer.TriviaServer.Entities;

#nullable disable

namespace Quizzer.TriviaServer.Migrations
{
    [DbContext(typeof(TriviaContext))]
    partial class TriviaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("Quizzer.TriviaServer.Entities.Answer", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("QuestionId", "Order");

                    b.ToTable("Answers");

                    b.HasData(
                        new
                        {
                            QuestionId = 1,
                            Order = 0,
                            IsCorrect = false,
                            Text = "1992"
                        },
                        new
                        {
                            QuestionId = 1,
                            Order = 1,
                            IsCorrect = true,
                            Text = "1981"
                        },
                        new
                        {
                            QuestionId = 1,
                            Order = 2,
                            IsCorrect = false,
                            Text = "1979"
                        },
                        new
                        {
                            QuestionId = 1,
                            Order = 3,
                            IsCorrect = false,
                            Text = "1984"
                        },
                        new
                        {
                            QuestionId = 2,
                            Order = 0,
                            IsCorrect = false,
                            Text = "1990"
                        },
                        new
                        {
                            QuestionId = 2,
                            Order = 1,
                            IsCorrect = false,
                            Text = "1981"
                        },
                        new
                        {
                            QuestionId = 2,
                            Order = 2,
                            IsCorrect = false,
                            Text = "1979"
                        },
                        new
                        {
                            QuestionId = 2,
                            Order = 3,
                            IsCorrect = true,
                            Text = "1984"
                        });
                });

            modelBuilder.Entity("Quizzer.TriviaServer.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Information Technology"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Geography"
                        });
                });

            modelBuilder.Entity("Quizzer.TriviaServer.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Difficulty")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuestionType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Questions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Difficulty = 1,
                            QuestionType = 1,
                            Text = "Which year was the IBM PC introduced?"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Difficulty = 1,
                            QuestionType = 1,
                            Text = "Which year was the Macintosh introduced?"
                        });
                });

            modelBuilder.Entity("Quizzer.TriviaServer.Entities.Answer", b =>
                {
                    b.HasOne("Quizzer.TriviaServer.Entities.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Quizzer.TriviaServer.Entities.Question", b =>
                {
                    b.HasOne("Quizzer.TriviaServer.Entities.Category", "Category")
                        .WithMany("Questions")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Quizzer.TriviaServer.Entities.Category", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Quizzer.TriviaServer.Entities.Question", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}
