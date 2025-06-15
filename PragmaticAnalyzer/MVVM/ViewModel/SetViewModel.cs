using Common.Core;
using Common.Databases;
using Common.DTO;
using Common.Enums.ForDatabase;
using Common.Enums.ForSolution;
using Microsoft.Win32;
using PragmaticAnalyzer.Core;
using PragmaticAnalyzer.MVVM.ViewModel.Viewer;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows.Media;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel
{
    public class SetViewModel : ViewModelBase
    {
        private readonly string settingPuth = Path.Combine(Environment.CurrentDirectory, "setting.json");
        private readonly MessageService messageService;
        public Paths PathsDatabases { get => Get<Paths>(); set => Set(value); }
        public Brush VulnerabilitieButtonBackground { get => Get<Brush>(); set => Set(value); }
        public Brush ThreatButtonBackground { get => Get<Brush>(); set => Set(value); }
        public Brush TacticButtonBackground { get => Get<Brush>(); set => Set(value); }
        public Brush ProtectionMeasureButtonBackground { get => Get<Brush>(); set => Set(value); }
        public Brush OutcomesButtonBackground { get => Get<Brush>(); set => Set(value); }
        public Brush ExploitButtonBackground { get => Get<Brush>(); set => Set(value); }
        public Brush ViolatorButtonBackground { get => Get<Brush>(); set => Set(value); }
        public Brush CurrentStatusButtonBackground { get => Get<Brush>(); set => Set(value); }
        public Brush ReferenceStatusButtonBackground { get => Get<Brush>(); set => Set(value); }
        public Brush SpecialistButtonBackground { get => Get<Brush>(); set => Set(value); }
        public VulViewModel VulVm { get; private set; }
        public ThreatViewModel ThretVm { get; private set; }
        public TacticViewModel TacticVm { get; private set; }
        public ProtectionMeasureViewModel ProtectionMeasureVm { get; private set; }
        public OutcomesViewModel OutcomesVm { get; private set; }
        public ExploitViewModel ExploitVm { get; private set; }
        public ViolatorViewModel ViolatorVm { get; private set; }
        public SpecialistViewModel SpecialistVm { get; private set; }
        public ReferenceStatusViewModel ReferenceStatusVm { get; private set; }


        public SetViewModel(VulViewModel vulViewModel, ThreatViewModel threatViewModel, TacticViewModel tacticViewModel,
                                            ProtectionMeasureViewModel protectionMeasureViewModel, OutcomesViewModel outcomesViewModel,
                                            ExploitViewModel exploitViewModel, ViolatorViewModel violatorViewModel, SpecialistViewModel specialistVewModel, Paths pathsDatabases,
                                            ReferenceStatusViewModel referenceStatusViewModel)
        {
            PathsDatabases = pathsDatabases;
            messageService = new();

            VulVm = vulViewModel;
            ThretVm = threatViewModel;
            TacticVm = tacticViewModel;
            ProtectionMeasureVm = protectionMeasureViewModel;
            OutcomesVm = outcomesViewModel;
            ExploitVm = exploitViewModel;
            ViolatorVm = violatorViewModel;
            SpecialistVm = specialistVewModel;
            ReferenceStatusVm = referenceStatusViewModel;

            ThreatButtonBackground = Brushes.Red;
            VulnerabilitieButtonBackground = Brushes.Red;
            TacticButtonBackground = Brushes.Red;
            ProtectionMeasureButtonBackground = Brushes.Red;
            OutcomesButtonBackground = Brushes.Red;
            ExploitButtonBackground = Brushes.Red;
            ViolatorButtonBackground = Brushes.Red;
            SpecialistButtonBackground = Brushes.Red;
            ReferenceStatusButtonBackground = Brushes.Red;
            CurrentStatusButtonBackground = Brushes.Red;

            if (PathsDatabases.OutcomesPath is not null)
            {
                if (!LoadFile(PathsDatabases.OutcomesPath, "Outcomes"))
                    PathsDatabases.OutcomesPath = null;
            }
            if (PathsDatabases.ProtectionMeasurePath is not null)
            {
                if (!LoadFile(PathsDatabases.ProtectionMeasurePath, "ProtectionMeasure"))
                    PathsDatabases.ProtectionMeasurePath = null;
            }
            if (PathsDatabases.VulPath is not null)
            {
                if (!LoadFile(PathsDatabases.VulPath, "Vulnerabilitie"))
                    PathsDatabases.VulPath = null;
            }
            if (PathsDatabases.TacticPath is not null)
            {
                if (!LoadFile(PathsDatabases.TacticPath, "Tactic"))
                    PathsDatabases.TacticPath = null;
            }
            if (PathsDatabases.ThreatPath is not null)
            {
                if (!LoadFile(PathsDatabases.ThreatPath, "Threat"))
                    PathsDatabases.ThreatPath = null;
            }
            if (PathsDatabases.ExploitPath is not null)
            {
                if (!LoadFile(PathsDatabases.ExploitPath, "Exploit"))
                    PathsDatabases.ExploitPath = null;
            }
            if (PathsDatabases.ViolatorPath is not null)
            {
                if (!LoadFile(PathsDatabases.ViolatorPath, "Violator"))
                    PathsDatabases.ViolatorPath = null;
            }
            if (PathsDatabases.SpecialistPath is not null)
            {
                if (!LoadFile(PathsDatabases.SpecialistPath, "Specialist"))
                    PathsDatabases.SpecialistPath = null;
            }
            if (PathsDatabases.ReferenceStatusPath is not null)
            {
                if (!LoadFile(PathsDatabases.ReferenceStatusPath, "ReferenceStatus"))
                    PathsDatabases.ReferenceStatusPath = null;
            }
            SaveFile();

            //////////////////////////
            Random rnd = new();
            /* ViolatorSource violatorSource = (ViolatorSource)rnd.Next(1, 2);
             ViolatorPotential violatorPotential = (ViolatorPotential)rnd.Next(1, 4);
             ViolatorVm.Violators = [
     new()
      {
         Id = rnd.Next(0, 999),
         Source = violatorSource,
         Potential = violatorPotential,
          Target = "Финансовые учреждения в Северной Америке",
          UsingTools = "Эксплойт вызывающий переполнение стекового буфера при обработке файлов расширения ThinApp",
          PreviousAttacks ="Нет"
      },
      new()
      {
          Id = rnd.Next(0, 999),
         Source = violatorSource,
         Potential = violatorPotential,
          Target = "Правительственные сети в Азии",
          UsingTools = "Не известно",
          PreviousAttacks ="Криптоботы для добычи Monero"
      },
      new()
      {
          Id = rnd.Next(0, 999),
         Source = violatorSource,
         Potential = violatorPotential,
          Target = "Облачные инфраструктурные провайдеры",
          UsingTools = "Атака на слабые пароли",
          PreviousAttacks ="Эксплуатация чересчур разрешительных политик IAM"
      },
      new()
      {
          Id = rnd.Next(0, 999),
         Source = violatorSource,
         Potential = violatorPotential,
          Target = "Критически важная инфраструктура в Европе",
          UsingTools = "",
          PreviousAttacks ="Нет"
      },
      new()
      {
          Id = rnd.Next(0, 999),
         Source = violatorSource,
         Potential = violatorPotential,
          Target = "Медицинские базы данных в Южной Америке",
          UsingTools = "Не известно",
          PreviousAttacks ="Нет"
      },
  ];
             SpecialistVm.Specialists = [
     new()
      {
          NameOrgan = "в/ч55555",
          NameHighestOrgan = "в/ч23462",
          NameSubordinateOrgan = "в/ч63733",
          StatusVulnerability = "Уязвимость выявлен",
          ActionsTaken = "Производится переконфигурирование системы",
          NameSoftware = "Kaspersky Endpoint Security для Windows 11.0.0.6499",
          UsingMeasures =new() {ProtectionMeasureVm.ProtectionMeasures[5],
          ProtectionMeasureVm.ProtectionMeasures[16]}

      },
      new()
      {
            NameOrgan = "в/ч12356",
          NameHighestOrgan = "в/ч94632",
          NameSubordinateOrgan = "в/ч11766",
          StatusVulnerability = "Уязвимость выявлен",
          ActionsTaken = "Производится анализ системы",
          NameSoftware = "Kaspersky  Security Center для Windows 10.0.0.1",
          UsingMeasures =new() {ProtectionMeasureVm.ProtectionMeasures[7],
          ProtectionMeasureVm.ProtectionMeasures[13] }
      },
      new()
      {
           NameOrgan = "в/ч15864",
          NameHighestOrgan = "в/ч15176",
          NameSubordinateOrgan = "в/ч15762",
          StatusVulnerability = "Уязвимость выявлен",
          ActionsTaken = "Не приняты",
          NameSoftware = "Kaspersky Endpoint Security для Linux 1.0.0.43",
          UsingMeasures = new(){ProtectionMeasureVm.ProtectionMeasures[6],
          ProtectionMeasureVm.ProtectionMeasures[14] }
      },
      new()
      {
            NameOrgan = "в/ч54682",
          NameHighestOrgan = "в/ч28648",
          NameSubordinateOrgan = "в/ч15784",
          StatusVulnerability = "Уязвимость выявлен",
          ActionsTaken = "Производится резервное копирование системы",
          NameSoftware = "Kaspersky Security Centerдля Linux 1.0.3.5",
          UsingMeasures =new() {ProtectionMeasureVm.ProtectionMeasures[4],
          ProtectionMeasureVm.ProtectionMeasures[2] }
      },
      new()
      {
           NameOrgan = "в/ч25486",
          NameHighestOrgan = "в/ч22824",
          NameSubordinateOrgan = "в/ч54863",
          StatusVulnerability = "Уязвимость выявлен",
          ActionsTaken = "Не приняты",
          NameSoftware = "Kaspersky Endpoint Security для Windows 11.0.0.6499",
          UsingMeasures = new(){ProtectionMeasureVm.ProtectionMeasures[1],
          ProtectionMeasureVm.ProtectionMeasures[12] }
      },
  ];*/

          /*  ObservableCollection<ReferenceStatus> referenceStatus = [
                new ReferenceStatus()
                {
                    NameSoftware = "Типовая инструкция по настройке Kaspersky Endpoint Security для Windows 11.0.0.6499 в ВС РФ, п. 116",
                    ArrivalTime = DateTime.Now,
                },
                  new ReferenceStatus()
                {
                    NameSoftware = "Типовая инструкция по настройке Kaspersky Security Center для Windows 10.2.0.1 в ВС РФ, п. 112",
                    ArrivalTime = DateTime.Now,
                },
                  new ReferenceStatus()
                {
                    NameSoftware = "Типовая инструкция по настройке Kaspersky Endpoint Security для Linux 3.2.0 в ВС РФ, п. 23",
                    ArrivalTime = DateTime.Now,
                },
                ];*/

           /* var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            DTO<Violator> b = new()
            {
                Type = TypeDatabase.Violator,
                Value = ViolatorVm.Violators
            };
            DTO<Specialist> c = new()
            {
                Type = TypeDatabase.Specialist,
                Value = SpecialistVm.Specialists
            };
            DTO<ReferenceStatus> d = new()
            {
                Type = TypeDatabase.ReferenceStatus,
                Value = referenceStatus
            };
            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "ReferenceStatus.json"), JsonSerializer.Serialize(d, options));*/
            // File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "Violators.json"), JsonSerializer.Serialize(b, options));
            //File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "Specialists.json"), JsonSerializer.Serialize(c, options));
        }

        public RelayCommand Set => GetCommand(o =>
        {
            OpenFileDialog openFileDialog = new() { Filter = "Json files (*.json)|*.json" };
            if (openFileDialog.ShowDialog() is not true) return;
            try
            {
                LoadFile(openFileDialog.FileName, (string)o);
                SaveFile();
            }
            catch (Exception ex)
            {
                messageService.ShowErrorMessage(ex.Message);
            }
        });

        private void SaveFile()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            File.WriteAllText(settingPuth, JsonSerializer.Serialize(PathsDatabases, options));
        }
        private bool LoadFile(string puth, string nameDatabase)
        {
            if (!Path.Exists(puth))
                return false;

            var rawDto = JsonSerializer.Deserialize<DTO<object>>(File.ReadAllText(puth));
            if (rawDto.Type is Common.Enums.ForSolution.FileType.Threat && nameDatabase is "Threat")
            {
                ThretVm.Threats = JsonSerializer.Deserialize<DTO<Threat>>(File.ReadAllText(puth)).Value;
                PathsDatabases.ThreatPath = puth;
                ThreatButtonBackground = Brushes.Green;
            }
            else if (rawDto.Type is Common.Enums.ForSolution.FileType.Vulnerabilitie && nameDatabase is "Vulnerabilitie")
            {
                VulVm.Vulnerabilities = JsonSerializer.Deserialize<DTO<Vulnerabilitie>>(File.ReadAllText(puth)).Value;
                PathsDatabases.VulPath = puth;
                VulnerabilitieButtonBackground = Brushes.Green;
            }
            else if (rawDto.Type is Common.Enums.ForSolution.FileType.TechniquesTactics && nameDatabase is "Tactic")
            {
                TacticVm.Tactics = JsonSerializer.Deserialize<DTO<Tactic>>(File.ReadAllText(puth)).Value;
                PathsDatabases.TacticPath = puth;
                TacticButtonBackground = Brushes.Green;
            }
            else if (rawDto.Type is Common.Enums.ForSolution.FileType.ProtectionMeasures && nameDatabase is "ProtectionMeasure")
            {
                ProtectionMeasureVm.ProtectionMeasures = JsonSerializer.Deserialize<DTO<ProtectionMeasure>>(File.ReadAllText(puth)).Value;
                PathsDatabases.ProtectionMeasurePath = puth;
                ProtectionMeasureButtonBackground = Brushes.Green;
            }
            else if (rawDto.Type is Common.Enums.ForSolution.FileType.Outcomes && nameDatabase is "Outcomes")
            {
                OutcomesVm.Outcomes = JsonSerializer.Deserialize<DTO<Outcomes>>(File.ReadAllText(puth)).Value[0];
                PathsDatabases.OutcomesPath = puth;
                OutcomesButtonBackground = Brushes.Green;
            }
            else if (rawDto.Type is Common.Enums.ForSolution.FileType.Exploit && nameDatabase is "Exploit")
            {
                ExploitVm.Exploits = JsonSerializer.Deserialize<DTO<Exploit>>(File.ReadAllText(puth)).Value;
                PathsDatabases.ExploitPath = puth;
                ExploitButtonBackground = Brushes.Green;
            }
            else if (rawDto.Type is Common.Enums.ForSolution.FileType.Violator && nameDatabase is "Violator")
            {
                ViolatorVm.Violators = JsonSerializer.Deserialize<DTO<Violator>>(File.ReadAllText(puth)).Value;
                PathsDatabases.ViolatorPath = puth;
                ViolatorButtonBackground = Brushes.Green;
            }
            else if (rawDto.Type is Common.Enums.ForSolution.FileType.Specialist && nameDatabase is "Specialist")
            {
                SpecialistVm.Specialists = JsonSerializer.Deserialize<DTO<Specialist>>(File.ReadAllText(puth)).Value;
                PathsDatabases.SpecialistPath = puth;
                SpecialistButtonBackground = Brushes.Green;
            }
            else if (rawDto.Type is Common.Enums.ForSolution.FileType.ReferenceStatus && nameDatabase is "ReferenceStatus")
            {
                ReferenceStatusVm.ReferencesStatus = JsonSerializer.Deserialize<DTO<ReferenceStatus>>(File.ReadAllText(puth)).Value;
                PathsDatabases.ReferenceStatusPath = puth;
                ReferenceStatusButtonBackground = Brushes.Green;
            }
            else
                throw new InvalidOperationException($"Тип данных не соответсвует: {rawDto.Type}");

            return true;
        }
    }
}