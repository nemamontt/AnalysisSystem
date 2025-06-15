using CreatorOutcomesDatabase.MVVM.ViewModel;

namespace CreatorOutcomesDatabase.MVVM.View
{
    public partial class ConsequencesView
    {
        public ConsequencesView(ConsequenceViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
