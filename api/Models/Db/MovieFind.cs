using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models.Db
{
    [Table("movie_find")]
    [Index("ConversationId", Name = "fk_conversation_moviefind_idx")]
    public partial class MovieFind
    {
        [Key]
        [Column("id")]
        public int Id { get; set; } = default!;
        [Column("conversationId")]
        public int ConversationId { get; set; } = default!;
        [Column("movieToSearchReceived")]
        public int? MovieToSearchReceived { get; set; } = default!;
        [Column("triesResults", TypeName = "text")]
        public string? TriesResults { get; set; } = default!;
        [Column("result", TypeName = "text")]
        public string? Result { get; set; } = default!;

        [ForeignKey("ConversationId")]
        [InverseProperty("MovieFinds")]
        public virtual Conversation Conversation { get; set; } = null!;
    }
}
