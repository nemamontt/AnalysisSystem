using Common.Databases;
using CreatorTechniquesTacticsDatabase.MVVM.View;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows;
using ViewModels;

namespace CreatorTechniquesTacticsDatabase.MVVM.ViewModel
{
    public class ViewModelTT : ViewModelBase
    {
        public ObservableCollection<Tactic> Tactics { get; set; }
        public IEntity? SelectedItem { get => Get<IEntity>(); set => Set(value); }
        public bool IsEnabled { get => Get<bool>(); set => Set(value); }
        public bool IsEnabledComboBox { get => Get<bool>(); set => Set(value); }

        public ViewModelTT()
        {
            Tactics = [];
        }

        public RelayCommand LoadCommand => GetCommand(o =>
        {
            LoadFile();
        });
        public RelayCommand SaveCommand => GetCommand(o =>
        {
            SaveFile();
            MessageBox.Show("Файл успешно сохранен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        });
        public RelayCommand DeleteElement => GetCommand(o =>
        {
            if(SelectedItem is Tactic tactic)
            {
                for (int i = 0; i < Tactics.Count; i++)
                {
                    if (Tactics[i].Id == tactic.Id)
                    {
                        Tactics.RemoveAt(i);
                    }
                }
            }
            else if (SelectedItem is Technique technique)
            {
                for (int i = 0; i < Tactics.Count; i++)
                {
                    for (int j = 0; j < Tactics[i].Techniques.Count; j++)
                    {
                        if (Tactics[i].Techniques[j].Id == technique.Id)
                        {
                            Tactics[i].Techniques.RemoveAt(j);
                        }
                    }
                }
            }
            CheckedAction();
        });
        public RelayCommand ShowAddView => GetCommand((bool add) =>
        {
            if (add is false && SelectedItem is null)
                return;

            AddView addView = new(add, this);
            addView.ShowDialog();
        });
        public RelayCommand GetHelpCommand => GetCommand( o =>
        {
            MessageBox.Show($"Информацию о техниках и тактиках можно получить\nиз нормативного документа ФСТЭК России\n\"Методика оценки угроз безопасности информации\"", 
                "Справка", MessageBoxButton.OK, MessageBoxImage.Information);
        });
        public void AddElement(IEntity newElement, [Optional]Guid id)
        {
            if (newElement is Tactic tactic)
            {
                tactic.Techniques = [];
                Tactics.Add(tactic);             
            }
            else if(newElement is Technique technique) 
            {
                for (int i = 0; i < Tactics.Count; i++)
                {
                    if (Tactics[i].Id == id)
                    {
                        Tactics[i].Techniques.Add(technique);
                        return;
                    }
                }
            }
            CheckedAction();
        }
        public void ChangeElement(IEntity modifiedElement)
        {
            if (modifiedElement is Tactic tactics)
            {
                for (int i = 0; i < Tactics.Count; i++)
                {
                    if (Tactics[i].Id == modifiedElement.Id)
                    {
                        Tactics[i] = tactics;
                        return;
                    }               
                }          
            }
            else if (modifiedElement is Technique technique)
            {
                for (int i = 0; i < Tactics.Count; i++)
                {
                    for (int j = 0; j < Tactics[i].Techniques.Count; j++)
                    {
                        if (Tactics[i].Techniques[j].Id == modifiedElement.Id)
                        {
                            Tactics[i].Techniques[j] = technique;
                            return;
                        }
                    }
                }            
            }
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
                var jsonString = JsonSerializer.Serialize(Tactics, options);
                File.WriteAllText(filePath, jsonString);
            }
        }
        private void LoadFile()
        {
            OpenFileDialog openFileDialog = new() { Filter = "Json files (*.json)|*.json" };
            if (openFileDialog.ShowDialog() is true)
            {
                var filePath = openFileDialog.FileName;
                var jsonString = File.ReadAllText(filePath);
                var newTactics = JsonSerializer.Deserialize<ObservableCollection<Tactic>>(jsonString);
                Tactics.Clear();
                foreach (var tactic in newTactics)
                {
                    Tactics.Add(tactic);
                }
            }
            CheckedAction();
        }
        private void CheckedAction()
        {
            if (Tactics.Count is 0)
                IsEnabled = false;
            else
                IsEnabled = true;
        }
    }
}