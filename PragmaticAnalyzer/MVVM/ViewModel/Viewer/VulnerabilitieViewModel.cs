using PragmaticAnalyzer.Abstractions;
using PragmaticAnalyzer.Configs;
using PragmaticAnalyzer.Databases;
using PragmaticAnalyzer.DTO;
using PragmaticAnalyzer.MVVM.Model;
using PragmaticAnalyzer.Services;
using System.Collections.ObjectModel;
using System.IO;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel.Viewer
{
    public class VulnerabilitieViewModel : ViewModelBase
    {
        private readonly VulnerabilitieModel _model;
        private readonly IFileService _fileService;
        private readonly Func<string, DataType, Task> UpdateConfig;
        public ObservableCollection<Vulnerabilitie> Vulnerabilities { get; }
        public Vulnerabilitie? SelectedVulnerabilitie { get => Get<Vulnerabilitie?>(); set => Set(value); }
        public string? Status { get => Get<string>(); set => Set(value); }
        public bool IsIndeterminateProgressBar { get => Get<bool>(); set => Set(value); }

        public VulnerabilitieViewModel(ObservableCollection<Vulnerabilitie> vulnerabilities, Func<string, DataType, Task> updateConfig, VulConfig vulConfig)
        {
            _fileService = new FileService();
            _model = new(vulConfig);

            Vulnerabilities = vulnerabilities;
            UpdateConfig = updateConfig;
            IsIndeterminateProgressBar = false;
        }

        public RelayCommand Update => GetCommand(async o =>
        {
            IsIndeterminateProgressBar = true;
            var newVulnerabilities = await _model.GetDatabase();
            if (newVulnerabilities is null) return;
            Vulnerabilities.Clear();
            foreach (var value in newVulnerabilities)
            {
                Vulnerabilities.Add(value);
            }
            await _fileService.SaveDTOAsync(Vulnerabilities, DataType.Vulnerabilitie, GlobalConfig.VulPath);
            UpdateConfig?.Invoke(DateTime.Now.ToString("f"), DataType.Vulnerabilitie);
            IsIndeterminateProgressBar = false;
        });
    }
}