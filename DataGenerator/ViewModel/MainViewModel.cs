using Common.Core;
using Common.Databases;
using Common.DTO;
using Common.Enums.ForSolution;
using DataGenerator.Infrastructure;
using DataGenerator.Lists;
using DataGenerator.Model;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using ViewModels;

namespace DataGenerator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly MessageService messageService;
        private ObservableCollection<Specialist> specialists;
        private ObservableCollection<CurrentStatus> currentStatus;
        private ObservableCollection<ReferenceStatus> referenceStatus;
        private ObservableCollection<Violator> violators;
        public ListViolator SourceDataViolator { get; set; }
        public ListSpecialist SourceDataSpecialist { get; set; }
        public ListStatus SourceDataStatus { get; set; }
        public int CountViolator { get; set; }
        public int CountSpecialist { get; set; }
        public int CountCurrentStatus { get; set; }
        public int CountReferenceStatus { get; set; }

        public MainViewModel()
        {
            specialists = [];
            currentStatus = [];
            referenceStatus = [];
            violators = [];

            SourceDataViolator = new();
            SourceDataSpecialist = new();
            SourceDataStatus = new();

            CountViolator = 1;
            CountSpecialist = 1;
            CountCurrentStatus = 1;
            CountReferenceStatus = 1;

            messageService = new();
        }

        public RelayCommand CreateSpecialist => GetCommand(o =>
        {
            ObservableCollection<ProtectionMeasure> protectionMeasures = [];
            messageService.ShowInfoMessage("Выберите базу данных мер защиты");
            OpenFileDialog openFileDialog = new();
            if (openFileDialog.ShowDialog() is true)
            {
                protectionMeasures = (ObservableCollection<ProtectionMeasure>)DGManager.OpenDatabase(openFileDialog.FileName);
            }
            specialists = GenerationModel.CreateSpecialist(SourceDataSpecialist, CountSpecialist, protectionMeasures);
            SaveFileDialog saveFileDialog = new();
            if (saveFileDialog.ShowDialog() is true)
            {
                DTO<Specialist> rawDto = new()
                {
                    Type = Common.Enums.ForSolution.FileType.Specialist,
                    Value = specialists
                };
                DGManager.SaveDatabase(saveFileDialog.FileName, rawDto);
                messageService.ShowInfoMessage($"Быза данных специалистов ЗИ успешно сохранена");
            }
        });
        public RelayCommand CreateViolator => GetCommand(o =>
        {
            violators = GenerationModel.CreateViolator(SourceDataViolator, CountViolator);
            SaveFileDialog saveFileDialog = new();
            if (saveFileDialog.ShowDialog() is true)
            {
                DTO<Violator> rawDto = new()
                {
                    Type = Common.Enums.ForSolution.FileType.Violator,
                    Value = violators
                };
                DGManager.SaveDatabase(saveFileDialog.FileName, rawDto);
                messageService.ShowInfoMessage($"Быза данных нарушителей успешно сохранена");
            }
        });
        public RelayCommand CreateCurrentStatus => GetCommand(o =>
        {
            currentStatus = GenerationModel.CreateCurrentStatus(SourceDataStatus, CountCurrentStatus);
            SaveFileDialog saveFileDialog = new();
            if (saveFileDialog.ShowDialog() is true)
            {
                DTO<CurrentStatus> rawDto = new()
                {
                    Type = Common.Enums.ForSolution.FileType.CurrentStatus,
                    Value = currentStatus
                };
                DGManager.SaveDatabase(saveFileDialog.FileName, rawDto);
                messageService.ShowInfoMessage($"Быза данных текущего состояния успешно сохранена");
            }
        });
        public RelayCommand CreateReferenceStatus => GetCommand(o =>
        {
            referenceStatus = GenerationModel.CreateReferenceStatus(SourceDataStatus, CountReferenceStatus);
            SaveFileDialog saveFileDialog = new();
            if (saveFileDialog.ShowDialog() is true)
            {
                DTO<ReferenceStatus> rawDto = new()
                {
                    Type = Common.Enums.ForSolution.FileType.ReferenceStatus,
                    Value = referenceStatus
                };
                DGManager.SaveDatabase(saveFileDialog.FileName, rawDto);
                messageService.ShowInfoMessage($"Быза данных эталонного состояния сохранена");
            }
        });
        public RelayCommand LoadSourceDataSpecialist => GetCommand(o =>
        {
            OpenFileDialog openFileDialog = new();
            if (openFileDialog.ShowDialog() is true)
            {
                SourceDataSpecialist = (ListSpecialist)DGManager.OpenDatabase(openFileDialog.FileName);
                messageService.ShowInfoMessage($"Конфигурационный файл успешно загружен");
            }
        });
        public RelayCommand LoadSourceDataViolator => GetCommand(o =>
        {
            OpenFileDialog openFileDialog = new();
            if (openFileDialog.ShowDialog() is true)
            {
                SourceDataViolator = (ListViolator)DGManager.OpenDatabase(openFileDialog.FileName);
                messageService.ShowInfoMessage($"Конфигурационный файл успешно загружен");
            }
        });
        public RelayCommand LoadSourceDataStatus => GetCommand(o =>
        {
            OpenFileDialog openFileDialog = new();
            if (openFileDialog.ShowDialog() is true)
            {
                SourceDataStatus = (ListStatus)DGManager.OpenDatabase(openFileDialog.FileName);
                messageService.ShowInfoMessage($"Конфигурационный файл успешно загружен");
            }
        });
    }
}