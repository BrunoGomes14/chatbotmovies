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
        public int Id { get; set; }
        [Column("peopleId", TypeName = "text")]
        public string PeopleId { get; set; } = null!;
        [Column("status")]
        public int Status { get; set; }
        [Column("lastReceive", TypeName = "datetime")]
        public DateTime LastReceive { get; set; }
        [Column("from")]
        [StringLength(45)]
        public string From { get; set; } = null!;
        [Column("functionDecision")]
        public int FunctionDecision { get; set; }

        [InverseProperty("Conversation")]
        public virtual ICollection<MovieDecision> MovieDecisions { get; set; }
        [InverseProperty("Conversation")]
        public virtual ICollection<MovieFind> MovieFinds { get; set; }
    }
}
