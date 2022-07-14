using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoritmaGenetikaPenjadwalan.Dtos
{
    public class Individu
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Gen Gen { get; set; }
        public decimal Fitness { get; set; } = 0.0m;
    }
}
