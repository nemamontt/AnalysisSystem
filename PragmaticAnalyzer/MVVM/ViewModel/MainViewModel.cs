using PragmaticAnalyzer.Abstractions;
using PragmaticAnalyzer.Configs;
using PragmaticAnalyzer.Databases;
using PragmaticAnalyzer.DTO;
using PragmaticAnalyzer.Services;
using System.Collections.ObjectModel;
using System.IO;
using ViewModels;
namespace PragmaticAnalyzer.MVVM.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMessageService _messageService;
        private readonly IFileService _fileService;
        private VulConfig _vulConfig;
        private ThreatConfig _threatConfig;
        private ExploitConfig _exploitConfig;
        private LastUpdateConfig _lastUpdateConfig;
        public Action Initial;
        public Action Complete;
        public object? CurrentView { get => Get<object>(); private set => Set(value); }
        public IViewModelsService ViewModelsService { get => Get<IViewModelsService>(); private set => Set(value); } // проверить работает private

        public MainViewModel()
        {
            Initial += OnInitial;
            Complete += OnComplete;
            _vulConfig = new();
            _threatConfig = new();
            _exploitConfig = new();
            _lastUpdateConfig = new();
            _messageService = new MessageService();
            _fileService = new FileService();
            ViewModelsService = new ViewModelsService(SetCurrentViewAction, _lastUpdateConfig, _threatConfig, _exploitConfig, _vulConfig);
            CurrentView = ViewModelsService.SetVm;
        }

        public RelayCommand SetCurrentView => GetCommand(vm => SetCurrentViewAction(vm));

        private void SetCurrentViewAction(object? vm)
        {
            if (vm is not null)
            {
                CurrentView = vm;
            }
        }

        private async void OnInitial()
        {
            if (File.Exists(GlobalConfig.LastUpdateConfig))
            {
                _lastUpdateConfig = await _fileService.LoadDTOAsync<LastUpdateConfig>(GlobalConfig.LastUpdateConfig, DataType.LastUpdateConfig) ?? new();
            }
            if (File.Exists(GlobalConfig.ExploitConfigPath))
            {
                _exploitConfig = await _fileService.LoadDTOAsync<ExploitConfig>(GlobalConfig.ExploitConfigPath, DataType.ExploitConfig) ?? new();
            }
            if (File.Exists(GlobalConfig.ThreatConfigPath))
            {
                _threatConfig = await _fileService.LoadDTOAsync<ThreatConfig>(GlobalConfig.ThreatConfigPath, DataType.ThreatConfig) ?? new();
            }
            if (File.Exists(GlobalConfig.VulConfigPath))
            {
                _vulConfig = await _fileService.LoadDTOAsync<VulConfig>(GlobalConfig.VulConfigPath, DataType.VulConfig) ?? new();
            }
            if (!Directory.Exists(GlobalConfig.DatabasePath))
            {
                Directory.CreateDirectory(GlobalConfig.DatabasePath);
            }

           /* var vulDto = await _fileService.LoadFileAsync<DTO<ObservableCollection<Vulnerabilitie>>>(GlobalConfig.VulPath);
            if (vulDto != default)
            {
                foreach (var vul in vulDto.Value)
                {
                    ViewModelsService.VulnerabilitieVm.Vulnerabilities.Add(vul);
                }
                ViewModelsService.SetVm.UpdateConfig?.Invoke(vulDto.DateCreation.ToString("f"), DataType.Vulnerabilitie);
            }*/
            var threatDto = await _fileService.LoadFileAsync<DTO<ObservableCollection<Threat>>>(GlobalConfig.ThreatPath);
            if (threatDto != default)
            {
                foreach (var threat in threatDto.Value)
                {
                    ViewModelsService.ThreatVm.Threats.Add(threat);
                }
                ViewModelsService.SetVm.UpdateConfig?.Invoke(threatDto.DateCreation.ToString("f"), DataType.Threat);
            }
            var protectionMeasureDto = await _fileService.LoadFileAsync<DTO<ObservableCollection<ProtectionMeasure>>>(GlobalConfig.ProtectionMeasurePath);
            if (protectionMeasureDto != default)
            {
                foreach (var protectionMeasure in protectionMeasureDto.Value)
                {
                    ViewModelsService.ProtectionMeasureVm.ProtectionMeasures.Add(protectionMeasure);
                }
                ViewModelsService.SetVm.UpdateConfig?.Invoke(protectionMeasureDto.DateCreation.ToString("f"), DataType.ProtectionMeasures);
            }
            var techniquesTacticDto = await _fileService.LoadFileAsync<DTO<ObservableCollection<Tactic>>>(GlobalConfig.TacticPath);
            if (techniquesTacticDto != default)
            {
                foreach (var techniquesTactic in techniquesTacticDto.Value)
                {
                    ViewModelsService.TacticVm.Tactics.Add(techniquesTactic);
                }
                ViewModelsService.SetVm.UpdateConfig?.Invoke(techniquesTacticDto.DateCreation.ToString("f"), DataType.Tactic);
            }
            var exploitDto = await _fileService.LoadFileAsync<DTO<ObservableCollection<Exploit>>>(GlobalConfig.ExploitPath);
            if (exploitDto != default)
            {
                foreach (var exploit in exploitDto.Value)
                {
                    ViewModelsService.ExploitVm.Exploits.Add(exploit);
                }
                ViewModelsService.SetVm.UpdateConfig?.Invoke(exploitDto.DateCreation.ToString("f"), DataType.Exploit);
            }
            var outcomesDto = await _fileService.LoadFileAsync<DTO<Outcomes>>(GlobalConfig.OutcomesPath);
            if (outcomesDto != default)
            {
                foreach (var technology in outcomesDto.Value.Technologys)
                {
                    ViewModelsService.OutcomeVm.Outcomes.Technologys.Add(technology);
                }
                foreach (var consequence in outcomesDto.Value.Consequences)
                {
                    ViewModelsService.OutcomeVm.Outcomes.Consequences.Add(consequence);
                }
                ViewModelsService.SetVm.UpdateConfig?.Invoke(outcomesDto.DateCreation.ToString("f"), DataType.Outcomes);
            }
        }

        private async void OnComplete()
        {
            await _fileService.SaveDTOAsync(_lastUpdateConfig, DataType.LastUpdateConfig, GlobalConfig.LastUpdateConfig);
            await _fileService.SaveDTOAsync(_exploitConfig, DataType.ExploitConfig, GlobalConfig.ExploitConfigPath);
            await _fileService.SaveDTOAsync(_threatConfig, DataType.ThreatConfig, GlobalConfig.ThreatConfigPath);
            await _fileService.SaveDTOAsync(_vulConfig, DataType.VulConfig, GlobalConfig.VulConfigPath);
            await _fileService.SaveDTOAsync(ViewModelsService.ThreatVm.Threats, DataType.Threat, GlobalConfig.ThreatPath);
            await _fileService.SaveDTOAsync(ViewModelsService.ProtectionMeasureVm.ProtectionMeasures, DataType.ProtectionMeasures, GlobalConfig.ProtectionMeasurePath);
            await _fileService.SaveDTOAsync(ViewModelsService.TacticVm.Tactics, DataType.Tactic, GlobalConfig.TacticPath);
            await _fileService.SaveDTOAsync(ViewModelsService.ExploitVm.Exploits, DataType.Exploit, GlobalConfig.ExploitPath);
            await _fileService.SaveDTOAsync(ViewModelsService.VulnerabilitieVm.Vulnerabilities, DataType.Vulnerabilitie, GlobalConfig.VulPath);
            await _fileService.SaveDTOAsync(ViewModelsService.ViolatorVm.Violators, DataType.Violator, GlobalConfig.ViolatorPath);
            await _fileService.SaveDTOAsync(ViewModelsService.CurrentStatusVm.CurrentsStatus, DataType.CurrentStatus, GlobalConfig.CurStatPath);
            await _fileService.SaveDTOAsync(ViewModelsService.ReferenceStatusVm.ReferencesStatus, DataType.ReferenceStatus, GlobalConfig.RefStatPath);
            await _fileService.SaveDTOAsync(ViewModelsService.SpecialistVm.Specialists, DataType.Specialist, GlobalConfig.SpecialistPath);
            await _fileService.SaveDTOAsync(ViewModelsService.OutcomeVm.Outcomes, DataType.Outcomes, GlobalConfig.OutcomesPath);
        }
    }
}