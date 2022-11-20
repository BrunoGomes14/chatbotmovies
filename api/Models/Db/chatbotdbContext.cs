using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api.Models.Db
{
    public partial class chatbotdbContext : DbContext
    {
        public chatbotdbContext()
        {
        }

        public chatbotdbContext(DbContextOptions<chatbotdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Conversation> Conversations { get; set; } = null!;
        public virtual DbSet<ConversationResultHistory> ConversationResultHistories { get; set; } = null!;
        public virtual DbSet<MovieDecision> MovieDecisions { get; set; } = null!;
        public virtual DbSet<MovieFind> MovieFinds { get; set; } = null!;
        public virtual DbSet<MovieList> MovieLists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseMySql("server=localhost;user=root;password=root123;database=chatbotdb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<MovieDecision>(entity =>
            {
                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.MovieDecisions)
                    .HasForeignKey(d => d.ConversationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_conversation_movie");
            });

            modelBuilder.Entity<MovieFind>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.MovieFinds)
                    .HasForeignKey(d => d.ConversationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_conversation_moviefind");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
