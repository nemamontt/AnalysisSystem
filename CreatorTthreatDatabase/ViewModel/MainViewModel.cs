using Common.Databases;
using Common.DTO;
using Common.Enums.ForSolution;
using CreatorTthreatDatabase.Model;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows;
using ViewModels;

namespace CreatorTthreatDatabase.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public bool IsIndeterminateProgressBar { get => Get<bool>(); set => Set(value); }
        public bool IsEnabledSaveButton { get => Get<bool>(); set => Set(value); }
        public ObservableCollection<Threat>? Threats { get => Get<ObservableCollection<Threat>>(); set => Set(value); }
        private readonly BackgroundWorker worker;
        private readonly ThreatModel model;

        public MainViewModel()
        {
            model = new();
            IsEnabledSaveButton = false;
            IsIndeterminateProgressBar = false;
            worker = new()
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            worker.DoWork += new(StartExecution);
            worker.RunWorkerCompleted += new(CompletedExecution);
        }

        public RelayCommand CreatCommand => GetCommand(o =>
        {
            worker.RunWorkerAsync();
        });
        public RelayCommand ViewCommand => GetCommand(o =>
        {
            LoadFile();
        });
        public RelayCommand SaveCommand => GetCommand(o =>
        {
            SaveFile();
            MessageBox.Show("Файл успешно сохранен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        });

        private void CompletedExecution(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                IsIndeterminateProgressBar = false;
                MessageBox.Show(e.Error.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                IsIndeterminateProgressBar = false;
                IsEnabledSaveButton = true;
                MessageBox.Show("База данных успешно сформирована", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void StartExecution(object? sender, DoWorkEventArgs e)
        {
            IsIndeterminateProgressBar = true;
            Threats = model.CreateDatabase();
        }
        private void SaveFile()
        {
            SaveFileDialog saveFileDialog = new() { Filter = "Json files (*.json)|*.json" };
            if (saveFileDialog.ShowDialog() is true)
            {
                var filePath = saveFileDialog.FileName;
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                DTO<Threat> dtoThreat = new()
                {
                    Type = Common.Enums.ForSolution.FileType.Threat,
                    Value = Threats
                };
                var jsonString = JsonSerializer.Serialize(dtoThreat, options);
                File.WriteAllText(filePath, jsonString);
            }
        }
        private void LoadFile()
        {
            OpenFileDialog openFileDialog = new() { Filter = "Json files (*.json)|*.json" };
            if (openFileDialog.ShowDialog() is false) return;
            var rawDto = JsonSerializer.Deserialize<DTO<Threat>>(File.ReadAllText(openFileDialog.FileName));
            if(rawDto.Type != Common.Enums.ForSolution.FileType.Threat) return;
            Threats = rawDto.Value;
        }
    }
}