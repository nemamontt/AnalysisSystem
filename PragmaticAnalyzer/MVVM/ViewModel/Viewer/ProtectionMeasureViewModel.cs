﻿using PragmaticAnalyzer.Databases;
using PragmaticAnalyzer.DTO;
using PragmaticAnalyzer.MVVM.Views.Viewer;
using System.Collections.ObjectModel;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel.Viewer
{
    public class ProtectionMeasureViewModel : ViewModelBase
    {
        private readonly Func<string, DataType, Task> UpdateConfig;
        private Action<ProtectionMeasure> Add;
        private Action<ProtectionMeasure> Change;

        public ObservableCollection<ProtectionMeasure> ProtectionMeasures { get; set; }
        public ProtectionMeasure? SelectedProtectionMeasure { get => Get<ProtectionMeasure?>(); set => Set(value); }

        public ProtectionMeasureViewModel(ObservableCollection<ProtectionMeasure> protectionMeasures, Func<string, DataType, Task> updateConfig)
        {
            Add += OnAdd;
            Change += OnChange;
            ProtectionMeasures = protectionMeasures;
            UpdateConfig += updateConfig;
        }

        public RelayCommand AddCommand => GetCommand(o =>
        {
            ProtectionMeasureManagerView view = new(OnAdd, OnChange ,true);
            view.ShowDialog();
        });
        public RelayCommand DeleteCommand => GetCommand(o =>
        {
            if (SelectedProtectionMeasure is null) return;
            for (int i = 0; i < ProtectionMeasures.Count; i++)
            {
                if (ProtectionMeasures[i].Name == SelectedProtectionMeasure.Name)
                {
                    ProtectionMeasures.Remove(ProtectionMeasures[i]);
                    break;
                }
            }
        });
        public RelayCommand ChangeCommand => GetCommand(o =>
        {
            if (SelectedProtectionMeasure is null) return;
            ProtectionMeasureManagerView view = new(OnAdd, OnChange, false, SelectedProtectionMeasure);
            view.ShowDialog();
        });

        public void OnAdd(ProtectionMeasure protectionMeasure)
        {
            ProtectionMeasures.Add(protectionMeasure);
        }
        public void OnChange(ProtectionMeasure protectionMeasure)
        {
            SelectedProtectionMeasure.NameGroup = protectionMeasure.NameGroup;
            SelectedProtectionMeasure.Name = protectionMeasure.Name;
            SelectedProtectionMeasure.Number = protectionMeasure.Number;
            SelectedProtectionMeasure.Description = protectionMeasure.Description;
            SelectedProtectionMeasure.SecurityClasses = protectionMeasure.SecurityClasses;
        }
    }
}