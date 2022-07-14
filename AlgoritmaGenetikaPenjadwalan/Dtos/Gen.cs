using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoritmaGenetikaPenjadwalan.Dtos
{
    public class Gen
    {
        public int Dosen { get; set; }
        public string Kelas { get; set; }
        public int KapasitasKelas { get; set; }
        public string TipeKelas { get; set; }
        public int DurasiKelas { get; set; }
        public int Ruang { get; set; }
        public int KapasitasRuang { get; set; }
        public string TipeRuang { get; set; }
        public int Hari { get; set; }
        public int Waktu { get; set; }
        public int DurasiWaktu { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}
