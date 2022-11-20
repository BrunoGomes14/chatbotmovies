using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models.Db
{
    [Table("conversation")]
    public partial class Conversation
    {
        public Conversation()
        {
            MovieDecisions = new HashSet<MovieDecision>();
            MovieFinds = new HashSet<MovieFind>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; } = default!;
        [Column("peopleId", TypeName = "text")]
        public string PeopleId { get; set; } = null!;
        [Column("status")]
        public int Status { get; set; } = default!;
        [Column("lastReceive", TypeName = "datetime")]
        public DateTime LastReceive { get; set; } = default!;
        [Column("from")]
        [StringLength(45)]
        public string From { get; set; } = null!;
        [Column("functionDecision")]
        public int FunctionDecision { get; set; } = default!;

        [InverseProperty("Conversation")]
        public virtual ICollection<MovieDecision> MovieDecisions { get; set; } = default!;
        [InverseProperty("Conversation")]
        public virtual ICollection<MovieFind> MovieFinds { get; set; } = default!;
    }
}
