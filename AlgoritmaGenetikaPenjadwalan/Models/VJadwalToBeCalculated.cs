using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AlgoritmaGenetikaPenjadwalan.Models
{
    public partial class VJadwalToBeCalculated
    {
        public int Id { get; set; }
        public int IdDosen { get; set; }
        public int IdKelas { get; set; }
        public int IterasiKelas { get; set; }
        public string TipeKelas { get; set; }
        public int DurasiKelas { get; set; }
        public int KapasitasKelas { get; set; }
    }
}
