using AlgoritmaGenetikaPenjadwalan.Dtos;
using AlgoritmaGenetikaPenjadwalan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoritmaGenetikaPenjadwalan.Services
{
    public class GeneticAlgorithmService
    {
        private static PENJADWALANContext _dbContext;
        private static List<AgpRuang> _listOfRuang;
        private static List<AgpSettingsHari> _listOfHari;
        private static List<AgpSettingsJam> _listOfJam;
        private static List<AgpSettingsDosen> _listOfSettingDosen;

        private static Random _random;

        public GeneticAlgorithmService()
        {
            _dbContext = new PENJADWALANContext();
            _listOfRuang = _dbContext.AgpRuang.ToList();
            _listOfHari = _dbContext.AgpSettingsHari.ToList();
            _listOfJam = _dbContext.AgpSettingsJam.ToList();
            _listOfSettingDosen = _dbContext.AgpSettingsDosen.ToList();

            _random = new Random();
        }

        #region PRIVATE METHODS
        private AgpRuang GenerateRandomRuang(string tipeRuang)
        {
            var listOfRuangPerTipe = _listOfRuang.Where(x => x.Jenis.Equals(tipeRuang)).ToList();
            var indexRuang = _random.Next(listOfRuangPerTipe.Count);

            return listOfRuangPerTipe[indexRuang];
        }

        private AgpSettingsHari GenerateRandomHari()
        {
            var indexHari = _random.Next(_listOfHari.Count);

            return _listOfHari[indexHari];
        }

        private AgpSettingsJam GenerateRandomWaktu(int durasiWaktu) 
        {
            var listOfJamPerDurasi = _listOfJam.Where(x => x.Tipe == durasiWaktu).ToList();
            var indexJam = _random.Next(listOfJamPerDurasi.Count);

            return listOfJamPerDurasi[indexJam];
        }
        #endregion

        public Gen CreateGen(VJadwalToBeCalculated data)
        {
            var gen = new Gen
            {
                Dosen = data.IdDosen,
                Kelas = $"{data.IdKelas}.{data.IterasiKelas}",
                KapasitasKelas = data.KapasitasKelas,
                TipeKelas = data.TipeKelas,
                DurasiKelas = data.DurasiKelas
            };

            var randomRuang = GenerateRandomRuang(data.TipeKelas);
            gen.Ruang = randomRuang.Id;
            gen.KapasitasRuang = randomRuang.Kapasitas;
            gen.TipeRuang = randomRuang.Jenis;

            var randomHari = GenerateRandomHari();
            gen.Hari = randomHari.Id;
            
            var randomWaktu = GenerateRandomWaktu(data.DurasiKelas);
            gen.Waktu = randomWaktu.Id;
            gen.DurasiWaktu = randomWaktu.Tipe;
            gen.StartTime = randomWaktu.StartTime;
            gen.EndTime = randomWaktu.EndTime;

            return gen;
        }        

        public List<Individu> CalculateFitness(List<Individu> individu, List<Individu> population)
        {
            foreach (var item in individu)
            {
                var numberOfViolate = 0;
                var searchQuery = population.Where(x => x.Id != item.Id).AsQueryable();

                #region #1 Ruang, Hari, Time tidak bisa overlap
                var searchQuery_1 = searchQuery
                    .Where(x => item.Gen.Ruang == x.Gen.Ruang)
                    .Where(x => item.Gen.Hari == x.Gen.Hari).AsQueryable();

                var violatedRule1 = searchQuery_1.Any(x => item.Gen.StartTime <= x.Gen.EndTime && item.Gen.EndTime >= x.Gen.StartTime);
                if (violatedRule1)
                    numberOfViolate++;
                #endregion

                #region #2 Dosen, Hari, Time tidak bisa overlap
                var searchQuery_3 = searchQuery
                    .Where(x => item.Gen.Dosen == x.Gen.Dosen)
                    .Where(x => item.Gen.Hari == x.Gen.Hari).AsQueryable();

                var violatedRule3 = searchQuery_3.Any(x => item.Gen.StartTime <= x.Gen.EndTime && item.Gen.EndTime >= x.Gen.StartTime);
                if (violatedRule3)
                    numberOfViolate++;
                #endregion

                #region #3 Kapasitas Ruang >= Kapasitas Kelas
                //if (item.Gen.KapasitasRuang < item.Gen.KapasitasKelas)
                //    numberOfViolate++;
                #endregion

                item.Fitness = 1m / (1m + numberOfViolate);
            }

            return individu;
        }

        public List<Individu> CrossOver(Gen parent1, Gen parent2)
        {
            var childern = new List<Individu>();

            var child1 = new Gen
            {
                Dosen = parent1.Dosen,
                Kelas = parent1.Kelas,
                KapasitasKelas = parent1.KapasitasKelas,
                TipeKelas = parent1.TipeKelas,
                DurasiKelas = parent1.DurasiKelas,                

                Ruang = parent2.Ruang,
                TipeRuang = parent2.TipeRuang,
                KapasitasRuang = parent2.KapasitasRuang,

                Hari = parent2.Hari,

                Waktu = parent2.Waktu,
                DurasiWaktu = parent2.DurasiWaktu,
                StartTime = parent2.StartTime,
                EndTime = parent2.EndTime
            };            

            var child2 = new Gen
            {
                Dosen = parent2.Dosen,
                Kelas = parent2.Kelas,
                KapasitasKelas = parent2.KapasitasKelas,
                TipeKelas = parent2.TipeKelas,
                DurasiKelas = parent2.DurasiKelas,                

                Ruang = parent1.Ruang,
                TipeRuang = parent1.TipeRuang,
                KapasitasRuang = parent1.KapasitasRuang,

                Hari = parent1.Hari,

                Waktu = parent1.Waktu,
                DurasiWaktu = parent1.DurasiWaktu,
                StartTime = parent1.StartTime,
                EndTime = parent1.EndTime
            };

            childern.Add(new Individu() { Gen = child1 });
            childern.Add(new Individu() { Gen = child2 });

            return childern;
        }

        public List<Individu> Mutate(List<Individu> children, List<Individu> population)
        {
            var mutants = children;

            foreach (var mutant in mutants)
            {
                // #0 Generate random ruang
                var randomRuang = GenerateRandomRuang(mutant.Gen.TipeRuang);
                mutant.Gen.Ruang = randomRuang.Id;
                mutant.Gen.TipeRuang = randomRuang.Jenis;
                mutant.Gen.KapasitasRuang = randomRuang.Kapasitas;

                // #1 Get hari kosong per ruang [DARI POPULATION]
                var unavailableHariPerRuang = population
                    .Where(x => x.Gen.Ruang == mutant.Gen.Ruang)
                    .Select(x => x.Gen.Hari)
                    .Distinct()
                    .ToList();

                var availableHariPerRuang = _listOfHari
                    .Where(x => !unavailableHariPerRuang.Contains(x.Id))
                    .ToList();

                if (availableHariPerRuang.Any())
                    mutant.Gen.Hari = availableHariPerRuang.First().Id;
                else
                    mutant.Gen.Hari = GenerateRandomHari().Id;

                // #2 Get waktu kosong per ruang dan hari [DARI POPULATION]
                var unavailableWaktuPerHari_Ruang = population
                    .Where(x => x.Gen.Hari == mutant.Gen.Hari)
                    .Where(x => x.Gen.Ruang == mutant.Gen.Ruang)
                    .Select(x => new { x.Gen.StartTime, x.Gen.EndTime })
                    .Distinct()
                    .ToList();

                var unavailableWaktuPerHari_Dosen = population
                    .Where(x => x.Gen.Hari == mutant.Gen.Hari)
                    .Where(x => x.Gen.Dosen == mutant.Gen.Dosen)
                    .Select(x => new { x.Gen.StartTime, x.Gen.EndTime })
                    .Distinct()
                    .ToList();

                unavailableWaktuPerHari_Dosen.AddRange(unavailableWaktuPerHari_Ruang);
                var unavailableWaktu = unavailableWaktuPerHari_Dosen.Distinct().ToList();

                var availableWaktuPerHari_Ruang = _listOfJam
                    .Where(a => a.Tipe == mutant.Gen.DurasiWaktu)
                    .Where(a => !unavailableWaktu.Any(u => a.StartTime <= u.EndTime && a.EndTime >= u.StartTime))
                    .ToList();

                if (availableWaktuPerHari_Ruang.Any())
                {
                    var randomIndex = _random.Next(availableWaktuPerHari_Ruang.Count());
                    var waktu = availableWaktuPerHari_Ruang[randomIndex];
                    mutant.Gen.Waktu = waktu.Id;
                    mutant.Gen.DurasiWaktu = waktu.Tipe;
                    mutant.Gen.StartTime = waktu.StartTime;
                    mutant.Gen.EndTime = waktu.EndTime;
                }
            }

            return mutants;
        }
    }
}
