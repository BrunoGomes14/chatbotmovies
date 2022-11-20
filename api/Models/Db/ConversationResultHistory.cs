using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models.Db
{
    [Table("conversation_result_history")]
    public partial class ConversationResultHistory
    {
        [Key]
        [Column("id")]
        public int Id { get; set; } = default!;
        [Column("peopleId", TypeName = "text")]
        public string PeopleId { get; set; } = null!;
        [Column("result", TypeName = "text")]
        public string Result { get; set; } = null!;
        [Column("functionId")]
        public int FunctionId { get; set; } = default!;
        [Column("createdAt", TypeName = "datetime")]
        public DateTime CreatedAt { get; set; } = default!;
    }
}
