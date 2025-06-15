using Common.Core;
using Common.Databases;
using DatabaseAggregator.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ViewModels;

namespace DatabaseAggregator.ViewModel
{
    public class ViewerViewModel : ViewModelBase
    {
        private readonly ModelAggregator model;
        private readonly MessageService messageService;
        private readonly BackgroundWorker worker;
        public bool IsIndeterminate { get => Get<bool>(); set => Set(value); }
        public bool IsEnabledCreate { get => Get<bool>(); set => Set(value); }
        public bool IsEnabledSave { get => Get<bool>(); set => Set(value); }
        public ObservableCollection<Vulnerabilitie> CurrentView { get => Get<ObservableCollection<Vulnerabilitie>>(); set => Set(value); }

        public ViewerViewModel()
        {
            IsIndeterminate = false;
            IsEnabledCreate = true;
            IsEnabledSave = false;
            messageService = new();
            model = new();
            worker = new()
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true,
            };
            worker.DoWork += new DoWorkEventHandler(StartExecution);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompletedExecution);
        }

        public RelayCommand ViewCommand => GetCommand(o =>
        {
            try
            {
                CurrentView = ModelAggregator.GetDatabases() ?? throw new ArgumentNullException();
            }
            catch (ArgumentNullException ex)
            {
                messageService.ShowWarningMessage(ex.Message);
            }
            catch (Exception ex) { }
        });
        public RelayCommand CreateCommand => GetCommand(o =>
        {
            IsIndeterminate = true;
            worker.RunWorkerAsync();
        });
        public RelayCommand SaveCommannd => GetCommand(o =>
        {
            if (CurrentView is null || CurrentView.Count == 0)
                return;
            model.SaveFIle(CurrentView);
            messageService.ShowInfoMessage("База данных успешно сохранена");
        });

        private void CompletedExecution(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                IsIndeterminate = false;
                IsEnabledCreate = true;
                messageService.ShowErrorMessage(e.Error.Message);
            }
            else
            {
                IsIndeterminate = false;
                IsEnabledCreate = true;
                IsEnabledSave = true;
                messageService.ShowInfoMessage("База данных успешно сформирована");
            }
        }
        private void StartExecution(object? sender, DoWorkEventArgs e)
        {
            IsEnabledCreate = false;
            CurrentView = model.CreateDatabase();
        }
    }
}