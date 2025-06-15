using Common.Core;
using Common.Databases;
using Common.DTO;
using Common.Enums.ForSolution;
using CreatorProtectionMeasuresDatabase.MVVM.View;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows;

namespace CreatorProtectionMeasuresDatabase.MVVM.ViewModel
{
    public class ViewModelPM : ObservedObject
    {
        public int FontSize
        {
            get => fontSize;
            set
            {
                fontSize = value;
                OnPropertyChanged();
            }
        }
        private int fontSize;
        public ObservableCollection<ProtectionMeasure> ProtectionMeasures { get; set; }
        public ProtectionMeasure? SelectedItemListBox
        {
            get => selectedItemListBox;
            set
            {
                selectedItemListBox = value;
                OnPropertyChanged();
            }
        }
        private ProtectionMeasure? selectedItemListBox;

        public ViewModelPM()
        {
            ProtectionMeasures = [];
            FontSize = 15;
        }

        public void SaveFile()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            DTO<ProtectionMeasure> dTO = new()
            {
                Type = Common.Enums.ForSolution.FileType.ProtectionMeasures,
                Value = ProtectionMeasures
            };
            var json = JsonSerializer.Serialize(dTO, options);

            SaveFileDialog saveFileDialog = new()
            {
                Filter = "Json files (*.json)|*.json|All files (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, json);
            }
        }
        public void OpenFile()
        {
            try
            {
                OpenFileDialog openFileDialog = new() { Filter = "Json files (*.json)|*.json" };
                if (openFileDialog.ShowDialog() == true)
                {
                    string jsonString = File.ReadAllText(openFileDialog.FileName);
                    var rawDto = JsonSerializer.Deserialize<DTO<ProtectionMeasure>>(jsonString);
                    if (rawDto.Type is not Common.Enums.ForSolution.FileType.ProtectionMeasures) throw new Exception();
                    ProtectionMeasures.Clear();
                    foreach (var item in rawDto.Value)
                    {
                        ProtectionMeasures.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void DeleteElement()
        {
            if (selectedItemListBox is null)
                return;

            for (int i = 0; i < ProtectionMeasures.Count; i++)
            {
                if (ProtectionMeasures[i].Name == SelectedItemListBox.Name)
                {
                    ProtectionMeasures.Remove(ProtectionMeasures[i]);
                    break;
                }
            }
        }
        public void ShowAddWindow(bool append)
        {
            if (!append && SelectedItemListBox == null)
                return;

            AddView addView = new(this, append, selectedItemListBox);
            addView.ShowDialog();
        }
        public void AddElement(ProtectionMeasure protectionMeasure)
        {
            ProtectionMeasures.Add(protectionMeasure);
        }
        public void ChangeElement(ProtectionMeasure protectionMeasure)
        {
            for (int i = 0; i < ProtectionMeasures.Count; i++)
            {
                if (protectionMeasure.Id == ProtectionMeasures[i].Id)
                {
                    ProtectionMeasures.RemoveAt(i);
                    ProtectionMeasures.Add(protectionMeasure);
                }
            }
        }
    }
}
