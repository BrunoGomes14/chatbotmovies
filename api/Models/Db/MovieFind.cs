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
        public int Id { get; set; }
        [Column("conversationId")]
        public int ConversationId { get; set; }
        [Column("movieToSearchReceived")]
        public int? MovieToSearchReceived { get; set; }
        [Column("triesResults", TypeName = "text")]
        public string? TriesResults { get; set; }
        [Column("result", TypeName = "text")]
        public string? Result { get; set; }

        [ForeignKey("ConversationId")]
        [InverseProperty("MovieFinds")]
        public virtual Conversation Conversation { get; set; } = null!;
    }
}
