using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Core.SqliteEntities
{
    [Table("Response")]
    public class Response
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Data { get; set; }
        public string? Type { get; set; }
    }
}
