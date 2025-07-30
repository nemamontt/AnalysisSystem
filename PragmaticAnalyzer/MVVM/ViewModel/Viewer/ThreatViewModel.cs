using PragmaticAnalyzer.Abstractions;
using PragmaticAnalyzer.Configs;
using PragmaticAnalyzer.Databases;
using PragmaticAnalyzer.DTO;
using PragmaticAnalyzer.MVVM.Model;
using PragmaticAnalyzer.Services;
using System.Collections.ObjectModel;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel.Viewer
{
    public class ThreatViewModel : ViewModelBase
    {
        private ThreatConfig _threatConfig;
        private readonly ThreatModel _model;
        private readonly IFileService _fileService;
        private readonly Func<string, DataType, Task> UpdateConfig;
        public ObservableCollection<Threat> Threats { get; }
        public Threat? SelectedThreat { get => Get<Threat?>(); set => Set(value); }
        public string? Status { get => Get<string>(); set => Set(value); } //дописать
        public bool IsIndeterminateProgressBar { get => Get<bool>(); set => Set(value); }


        public ThreatViewModel(ObservableCollection<Threat> threats, Func<string, DataType, Task> updateConfig, ThreatConfig threatConfig)
        {
            _fileService = new FileService();
            _model = new();
            _threatConfig = threatConfig;
            Threats = threats;
            UpdateConfig = updateConfig;
            IsIndeterminateProgressBar = false;
        }

        public RelayCommand Update => GetCommand(async o =>
        {
            IsIndeterminateProgressBar = true;
            var newThreats = await _model.CreateDatabase(_threatConfig.ParsingUrl);
            if (newThreats is null) return;
            Threats.Clear();
            foreach (var value in newThreats)
            {
                Threats.Add(value);
            }
            await _fileService.SaveDTOAsync(Threats, DataType.Threat, GlobalConfig.ThreatPath);
            UpdateConfig?.Invoke(DateTime.Now.ToString("f"), DataType.Threat);
            IsIndeterminateProgressBar = false;
        });
    }
}