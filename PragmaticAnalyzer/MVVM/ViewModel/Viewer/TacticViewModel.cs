﻿using PragmaticAnalyzer.Configs;
using PragmaticAnalyzer.Databases;
using PragmaticAnalyzer.DTO;
using PragmaticAnalyzer.MVVM.Views.Viewer;
using System.Collections.ObjectModel;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel.Viewer
{
    public class TacticViewModel : ViewModelBase
    {
        private readonly Action<IEntity, string> Add;
        private readonly Action<IEntity> Change;
        private readonly Func<string, DataType, Task> UpdateConfig;

        public ObservableCollection<Tactic> Tactics { get; set; }
        public IEntity? SelectedItem { get => Get<IEntity>(); set => Set(value); }

        public TacticViewModel(ObservableCollection<Tactic> tactics, Func<string, DataType, Task> updateConfig)
        {
            Tactics = tactics;
            Add += OnAdd;
            Change += OnChange;
            UpdateConfig += updateConfig;
        }

        public RelayCommand AddCommand => GetCommand(o =>
        {
            TacticManagerView view = new(Add, Change, true, this);
            view.ShowDialog();
        });
        public RelayCommand ChangeCommand => GetCommand(o =>
        {
            if (SelectedItem is null) return;
            TacticManagerView view = new(Add, Change, false, this);
            view.ShowDialog();
        });
        public RelayCommand DeleteCommand => GetCommand(o =>
        {
            if (SelectedItem is Tactic selectedTactic)
            {
                Tactics.Remove(selectedTactic);
            }
            else if (SelectedItem is Technique selectedTechnique)
            {
                var tactic = Tactics.FirstOrDefault(t => t.Techniques.Contains(selectedTechnique));
                tactic?.Techniques.Remove(selectedTechnique);
            }
        });

        public void OnAdd(IEntity newElement, string name)
        {
            if (newElement is Tactic tactic)
            {
                tactic.Techniques = [];
                Tactics.Add(tactic);
            }
            else if (newElement is Technique technique)
            {
                for (int i = 0; i < Tactics.Count; i++)
                {
                    if (Tactics[i].Name == name)
                    {
                        Tactics[i].Techniques.Add(technique);
                        return;
                    }
                }
            }
            //await _fileService.SaveDTOAsync(Tactics, DataType.TechniquesTactics, GlobalConfig.TacticPath);
            UpdateConfig?.Invoke(DateTime.Now.ToString("f"), DataType.Tactic);
        }
        public void OnChange(IEntity modifiedElement)
        {
            if (modifiedElement is Tactic tactic)
            {
                int index = Tactics.IndexOf(tactic);
                if (index >= 0)
                {
                    Tactics[index] = tactic;
                }
            }
            else if (modifiedElement is Technique technique)
            {
                var parentTactic = Tactics.FirstOrDefault(t => t.Techniques.Contains(technique));
                if (parentTactic is not null)
                {
                    int index = parentTactic.Techniques.IndexOf(technique);
                    if (index >= 0)
                    {
                        parentTactic.Techniques[index] = technique;
                    }
                }
            }
            //await _fileService.SaveDTOAsync(Tactics, DataType.TechniquesTactics, GlobalConfig.TacticPath);
            UpdateConfig?.Invoke(DateTime.Now.ToString("f"), DataType.Tactic);
        }
    }
}