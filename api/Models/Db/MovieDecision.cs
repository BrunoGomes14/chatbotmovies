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
        public int Id { get; set; } = default!;
        [Column("conversationId")]
        public int ConversationId { get; set; } = default!;
        [Column("genre")]
        public int? Genre { get; set; } = default!;
        [Column("withWatchProvider")]
        public int? WithWatchProvider { get; set; } = default!;
        [Column("peopleAssociated")]
        public int? PeopleAssociated { get; set; } = default!;
        [Column("withCompany")]
        public int? WithCompany { get; set; } = default!;
        [Column("step")]
        public int? Step { get; set; } = default!;
        [Column("movieDecided", TypeName = "text")]
        public string? MovieDecided { get; set; } = default!;

        [ForeignKey("ConversationId")]
        [InverseProperty("MovieDecisions")]
        public virtual Conversation Conversation { get; set; } = null!;
    }
}
