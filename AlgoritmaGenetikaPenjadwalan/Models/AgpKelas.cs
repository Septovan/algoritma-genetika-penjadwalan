using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AlgoritmaGenetikaPenjadwalan.Models
{
    public partial class AgpKelas
    {
        public AgpKelas()
        {
            AgpSettingsDosen = new HashSet<AgpSettingsDosen>();
            AgpSettingsJadwalKelas = new HashSet<AgpSettingsJadwalKelas>();
        }

        public int Id { get; set; }
        public int IdMatakuliah { get; set; }
        public string Kode { get; set; }

        public virtual AgpMatakuliah IdMatakuliahNavigation { get; set; }
        public virtual ICollection<AgpSettingsDosen> AgpSettingsDosen { get; set; }
        public virtual ICollection<AgpSettingsJadwalKelas> AgpSettingsJadwalKelas { get; set; }
    }
}
