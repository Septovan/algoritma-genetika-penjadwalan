using AlgoritmaGenetikaPenjadwalan.Dtos;
using AlgoritmaGenetikaPenjadwalan.Models;
using AlgoritmaGenetikaPenjadwalan.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AlgoritmaGenetikaPenjadwalan
{
    class Program
    {
        private static GeneticAlgorithmService _service;
        private static PENJADWALANContext _dbContext;
        private static Random _random;
        private static Stopwatch _stopwatch;

        static void Main(string[] args)
        {
            _service = new GeneticAlgorithmService();
            _dbContext = new PENJADWALANContext();
            _random = new Random();
            _stopwatch = new Stopwatch();

            _stopwatch.Start();

            var listOfJadwalToBeCalculated = _dbContext.VJadwalToBeCalculated.ToList();
            var populasi = new List<Individu>();                        

            // Create Populasi
            foreach (var data in listOfJadwalToBeCalculated)
            {
                var gen = _service.CreateGen(data);
                populasi.Add(new Individu { Gen = gen });
            }

            // Calculate Fitness
            populasi = _service.CalculateFitness(populasi, populasi);

            var generation = 1;
            // Loop
            while (populasi.Any(x => x.Fitness < 1))
            {
                Console.WriteLine($"Generation: {generation}");
                Console.WriteLine($"Populasi == 1 : {populasi.Where(x => x.Fitness == 1).Count()}");
                Console.WriteLine($"Populasi < 1 : {populasi.Where(x => x.Fitness < 1).Count()}");

                // Selection
                var badPopulation = populasi.Where(x => x.Fitness != 1).ToList();
                var iRandomParent1 = _random.Next(badPopulation.Count);
                var parent1 = badPopulation[iRandomParent1];

                badPopulation = badPopulation.Where(x => x.Id != parent1.Id)
                    .Where(x => x.Gen.TipeRuang.Equals(parent1.Gen.TipeKelas))
                    .Where(x => x.Gen.DurasiWaktu.Equals(parent1.Gen.DurasiKelas))
                    .ToList();
                var iRandomParent2 = _random.Next(badPopulation.Count);                                
                var parent2 = (badPopulation.Any()) ? badPopulation[iRandomParent2] : null;

                // Remove the selected parents
                populasi = populasi.Except(new List<Individu> { parent1, parent2 }).ToList();

                if (parent2 == null)
                {
                    // Mutation
                    var mutants = _service.Mutate(new List<Individu> { parent1 }, populasi);

                    // Regeneration
                    mutants = _service.CalculateFitness(mutants, populasi);
                    populasi.AddRange(mutants);
                }
                else
                {
                    // Cross-over
                    var children = _service.CrossOver(parent1.Gen, parent2.Gen);

                    // Mutation
                    var mutants = _service.Mutate(children, populasi);

                    // Regeneration
                    mutants = _service.CalculateFitness(mutants, populasi);
                    populasi.AddRange(mutants);
                }

                generation++;
            }

            _stopwatch.Stop();
            Console.WriteLine($"========================");
            Console.WriteLine($"Generation: {generation}");
            Console.WriteLine($"Time elapsed: {_stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
