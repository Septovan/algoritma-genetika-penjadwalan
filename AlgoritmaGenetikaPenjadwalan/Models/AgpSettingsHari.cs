using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AlgoritmaGenetikaPenjadwalan.Models
{
    public partial class AgpSettingsHari
    {
        public AgpSettingsHari()
        {
            AgpJadwal = new HashSet<AgpJadwal>();
        }

        public int Id { get; set; }
        public string Hari { get; set; }

        public virtual ICollection<AgpJadwal> AgpJadwal { get; set; }
    }
}
