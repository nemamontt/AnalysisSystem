using PragmaticAnalyzer.Databases;
using PragmaticAnalyzer.DTO;
using System.Collections.ObjectModel;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel
{
    public class OntologyViewModel : ViewModelBase
    {
        public ObservableCollection<Ontology> Ontologys { get; set; }

        public OntologyViewModel(ObservableCollection<Ontology> ontologys)
        {
            Ontologys = ontologys;
        }

        public RelayCommand LoadCommand => GetCommand(o =>
        {
          
        });
    }
}