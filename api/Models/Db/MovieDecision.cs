using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models.Db
{
    [Table("movie_decisions")]
    [Index("ConversationId", Name = "fk_conversation_movie_idx")]
    public partial class MovieDecision
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("conversationId")]
        public int ConversationId { get; set; }
        [Column("genre")]
        public int? Genre { get; set; }
        [Column("withWatchProvider")]
        public int? WithWatchProvider { get; set; }
        [Column("peopleAssociated")]
        public int? PeopleAssociated { get; set; }
        [Column("withCompany")]
        public int? WithCompany { get; set; }
        [Column("step")]
        public int? Step { get; set; }
        [Column("movieDecided", TypeName = "text")]
        public string? MovieDecided { get; set; }

        [ForeignKey("ConversationId")]
        [InverseProperty("MovieDecisions")]
        public virtual Conversation Conversation { get; set; } = null!;
    }
}
