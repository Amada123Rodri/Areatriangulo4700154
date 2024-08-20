using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areatriangulo4700154
{
    [Table("triangulo")]
    public class Triangulo
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("lado1")]
        public double Lado1 { get; set; }

        [Column("lado2")]
        public double Lado2 { get; set; }

        [Column("lado3")]
        public double Lado3 { get; set; }

        [Column("area")]
        public double Area { get; set; }
    }
}
