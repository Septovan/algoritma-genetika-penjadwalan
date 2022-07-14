using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AlgoritmaGenetikaPenjadwalan.Models
{
    public partial class AgpRuang
    {
        public AgpRuang()
        {
            AgpJadwal = new HashSet<AgpJadwal>();
        }

        public int Id { get; set; }
        public string Kode { get; set; }
        public int Kapasitas { get; set; }
        public string Jenis { get; set; }

        public virtual ICollection<AgpJadwal> AgpJadwal { get; set; }
    }
}
