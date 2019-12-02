using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostTwitter.api.Domain.Models
{
    [Table("Usuario", Schema = "Twitter")]
    public class Usuario
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }

        public int QtdeSeguidores { get; set; }
        public string Cd_Twitter { get; set; }
    }
}
