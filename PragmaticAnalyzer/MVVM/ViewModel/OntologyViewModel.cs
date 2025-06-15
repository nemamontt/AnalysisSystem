using Common.Databases;
using Common.DTO;
using Common.Enums.ForSolution;
using Microsoft.Win32;
using PragmaticAnalyzer.Core;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel
{
    public class OntologyViewModel : ViewModelBase
    {
        private readonly string settingPuth = Path.Combine(Environment.CurrentDirectory, "setting.json");
        public ObservableCollection<Ontology> Ontologys { get; set; }
        private Paths PathsDatabases { get; }

        public OntologyViewModel(Paths pathsDatabases)
        {
            Ontologys = [];
            PathsDatabases = pathsDatabases;
            if (PathsDatabases.OntologiesPaths is null)
                PathsDatabases.OntologiesPaths = [];
            else
            {
                foreach (var path in PathsDatabases.OntologiesPaths)
                {
                    if (!Load(path))
                        PathsDatabases.OntologiesPaths.Remove(path);
                }
            }          
        }

        public RelayCommand LoadCommand => GetCommand(_ =>
        {
            OpenFileDialog openFileDialog = new() { Filter = "Json files (*.json)|*.json" };
            if (openFileDialog.ShowDialog() is false) return;
            Load(openFileDialog.FileName);
            PathsDatabases.OntologiesPaths.Add(openFileDialog.FileName);
            SaveFile();
        });

        private bool Load(string path)
        {
            if (!Path.Exists(path))
                return false;

            var rawDto = JsonSerializer.Deserialize<DTO<Ontology>>(File.ReadAllText(path));
            if (rawDto.Type != Common.Enums.ForSolution.FileType.Ontology)
                return false;

            foreach (var item in rawDto.Value)
                Ontologys.Add(item);

            return true;
        }
        private void SaveFile()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            File.WriteAllText(settingPuth, JsonSerializer.Serialize(PathsDatabases, options));
        }
    }
}
