using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AlgoritmaGenetikaPenjadwalan.Models
{
    public partial class AgpSettingsJadwalKelas
    {
        public AgpSettingsJadwalKelas()
        {
            AgpJadwal = new HashSet<AgpJadwal>();
        }

        public int Id { get; set; }
        public int IdKelas { get; set; }
        public int Iterasi { get; set; }
        public string Tipe { get; set; }
        public int Durasi { get; set; }

        public virtual AgpKelas IdKelasNavigation { get; set; }
        public virtual ICollection<AgpJadwal> AgpJadwal { get; set; }
    }
}
