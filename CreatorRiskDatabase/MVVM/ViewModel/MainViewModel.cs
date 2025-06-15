using Common.Core;
using Common.Databases;
using Common.DTO;
using Common.Enums.ForSolution;
using CreatorOutcomesDatabase.MVVM.View;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using ViewModels;

namespace CreatorOutcomesDatabase.MVVM.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public object? CurrentView { get => Get<object>(); private set => Set(value); }
        public Outcomes SourceData { get; set; }
        private MessageService MsgService { get; set; }
        public TechnologyView TechnView { get; set; }
        public TechnologyViewModel technologyViewModel;
        public ConsequencesView ConsView { get; set; }
        public ConsequenceViewModel consequenceViewModel;

        public MainViewModel()
        {
            technologyViewModel = new();
            consequenceViewModel = new();
            MsgService = new();
            SourceData = new()
            {
                Technologys = [],
                Consequences = []
            };
            TechnView = new(technologyViewModel);
            ConsView = new(consequenceViewModel);
            CurrentView = TechnView;
        }

        public RelayCommand SetCurrentView => GetCommand(o => CurrentView = o);
        public RelayCommand OpenFile => GetCommand(o =>
        {
            OpenFileDialog openFileDialog = new() { Filter = "Json files (*.json)|*.json" };
            if (openFileDialog.ShowDialog() is true)
            {
                var rawDto = JsonSerializer.Deserialize<DTO<Outcomes>>(File.ReadAllText(openFileDialog.FileName));
                if (rawDto.Type != Common.Enums.ForSolution.FileType.Outcomes) return;

                SourceData = rawDto.Value[0];
                foreach (var item in SourceData.Consequences)
                    consequenceViewModel.Consequences.Add(item);
                foreach (var item in SourceData.Technologys)
                    technologyViewModel.Technologys.Add(item);
            }
        });
        public RelayCommand SaveFile => GetCommand(o =>
        {
            SourceData.Technologys = technologyViewModel.Technologys ?? [];
            SourceData.Consequences = consequenceViewModel.Consequences ?? [];
            SaveFileDialog saveFileDialog = new() { Filter = "Json files (*.json)|*.json" };
            if (saveFileDialog.ShowDialog() is true)
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                ObservableCollection<Outcomes> value = [];
                value.Add(SourceData);

                DTO<Outcomes> outcomesDto = new()
                {
                    Type = Common.Enums.ForSolution.FileType.Outcomes,
                    Value = value
                };
                File.WriteAllText(saveFileDialog.FileName, JsonSerializer.Serialize(outcomesDto, options));
            }
        });
    }
}