﻿using PragmaticAnalyzer.Databases;
using PragmaticAnalyzer.DTO;
using System.Collections.ObjectModel;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel.Viewer
{
    public class CurrentStatusViewModel : ViewModelBase
    {
        private readonly Func<string, DataType, Task> UpdateConfig;
        public ObservableCollection<CurrentStatus> CurrentsStatus { get; set; }
        public CurrentStatus? SelectedCurrentStatus { get => Get<CurrentStatus?>(); set => Set(value); }

        public CurrentStatusViewModel(ObservableCollection<CurrentStatus> currentStatus, Func<string, DataType, Task> updateConfig)
        {
            CurrentsStatus = currentStatus;
            UpdateConfig += updateConfig;
        }
    }
}