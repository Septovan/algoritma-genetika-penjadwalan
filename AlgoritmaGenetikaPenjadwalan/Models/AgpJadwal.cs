using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AlgoritmaGenetikaPenjadwalan.Models
{
    public partial class AgpJadwal
    {
        public int Id { get; set; }
        public int? IdSettingJadwalKelas { get; set; }
        public int? IdSettiingDosen { get; set; }
        public int? IdRuang { get; set; }
        public int? IdSettingJam { get; set; }
        public int? IdSettingHari { get; set; }

        public virtual AgpRuang IdRuangNavigation { get; set; }
        public virtual AgpSettingsDosen IdSettiingDosenNavigation { get; set; }
        public virtual AgpSettingsHari IdSettingHariNavigation { get; set; }
        public virtual AgpSettingsJadwalKelas IdSettingJadwalKelasNavigation { get; set; }
        public virtual AgpSettingsJam IdSettingJamNavigation { get; set; }
    }
}
