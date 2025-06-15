using Common.Databases;
using Common.Enums.ForDatabase;
using DataGenerator.Lists;
using System.Collections.ObjectModel;

namespace DataGenerator.Model
{
    public class GenerationModel
    {
        public static ObservableCollection<CurrentStatus> CreateCurrentStatus(ListStatus listStatus, int count)
        {
            ObservableCollection<CurrentStatus> currentStatus = [];



            return currentStatus;
        }
        public static ObservableCollection<ReferenceStatus> CreateReferenceStatus(ListStatus listStatus, int count)
        {
            ObservableCollection<ReferenceStatus> referenceStatus = [];



            return referenceStatus;
        }
        public static ObservableCollection<Violator> CreateViolator(ListViolator listViolator, int count)
        {
            ObservableCollection<Violator> violators = [];
            Random rnd = new();

            for (int i = 0; i < count; i++)
            {
                ViolatorSource violatorSource = (ViolatorSource)rnd.Next(1, 2);
                ViolatorPotential violatorPotential = (ViolatorPotential)rnd.Next(1, 4);
                violators.Add(new Violator()
                {
                    Id = i + 1,
                    Source = violatorSource,
                    Potential = violatorPotential,
                    Target = listViolator.Target[rnd.Next(0, listViolator.Target.Count)],
                    UsingTools = listViolator.UsedTools[rnd.Next(0, listViolator.UsedTools.Count)],
                    PreviousAttacks = listViolator.PreviousAttacks[rnd.Next(0, listViolator.PreviousAttacks.Count)]
                });
            }

            return violators;
        }
        public static ObservableCollection<Specialist> CreateSpecialist(ListSpecialist listSpecialist, int count, ObservableCollection<ProtectionMeasure> protectionMeasures)
        {
            ObservableCollection<Specialist> specialists = [];
            Random rnd = new();

            for(int i = 0; i < count; i++)
            {
                specialists.Add(new Specialist()
                {
                    NameOrgan = listSpecialist.NameOrgans[rnd.Next(0, listSpecialist.NameOrgans.Count)],
                    NameHighestOrgan = listSpecialist.NameOrgans[rnd.Next(0, listSpecialist.NameOrgans.Count)],
                    NameSubordinateOrgan = listSpecialist.NameOrgans[rnd.Next(0, listSpecialist.NameOrgans.Count)],
                    StatusVulnerability = listSpecialist.StatusVulnerabilitys[rnd.Next(0, listSpecialist.StatusVulnerabilitys.Count)],
                    ActionsTaken = listSpecialist.ActionsTakens[rnd.Next(0, listSpecialist.ActionsTakens.Count)],
                    NameSoftware = listSpecialist.NameSoftwares[rnd.Next(0, listSpecialist.NameSoftwares.Count)],
                });
               foreach(var specialist in specialists)
                {
                    for (int j = 0; j < rnd.Next(0, 5); j++)
                    {
                        specialist.NameInteractingOrgans.Add(rnd.Next(11111, 99999).ToString());
                    }
                    for (int j = 0; j < rnd.Next(1, 2); j++)
                    {
                        specialist.UsingMeasures.Add(protectionMeasures[rnd.Next(0, protectionMeasures.Count)]);
                    }
                }
            }

            return specialists;
        }
    }
}