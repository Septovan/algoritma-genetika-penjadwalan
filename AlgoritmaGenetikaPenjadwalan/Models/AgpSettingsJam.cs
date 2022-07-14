using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AlgoritmaGenetikaPenjadwalan.Models
{
    public partial class AgpSettingsJam
    {
        public AgpSettingsJam()
        {
            AgpJadwal = new HashSet<AgpJadwal>();
        }

        public int Id { get; set; }
        public int Tipe { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }

        public virtual ICollection<AgpJadwal> AgpJadwal { get; set; }
    }
}
