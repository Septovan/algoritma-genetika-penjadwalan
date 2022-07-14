using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AlgoritmaGenetikaPenjadwalan.Models
{
    public partial class AgpMatakuliah
    {
        public AgpMatakuliah()
        {
            AgpKelas = new HashSet<AgpKelas>();
        }

        public int Id { get; set; }
        public string Kode { get; set; }
        public string Nama { get; set; }
        public int Kapasitas { get; set; }

        public virtual ICollection<AgpKelas> AgpKelas { get; set; }
    }
}
