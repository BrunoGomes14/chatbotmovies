using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models.Db
{
    [Table("movie_list")]
    public partial class MovieList
    {
        [Key]
        [Column("id")]
        public int Id { get; set; } = default!;
        [Column("peopleId", TypeName = "text")]
        public string PeopleId { get; set; } = null!;
        [Column("movieId")]
        public long MovieId { get; set; } = default!;
        [Column("watched", TypeName = "bit(1)")]
        public ulong Watched { get; set; } = default!;
        [Column("createdAt", TypeName = "datetime")]
        public DateTime CreatedAt { get; set; } = default!;
    }
}
