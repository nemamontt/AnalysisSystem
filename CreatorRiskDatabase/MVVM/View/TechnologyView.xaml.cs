using CreatorOutcomesDatabase.MVVM.ViewModel;

namespace CreatorOutcomesDatabase.MVVM.View
{
    public partial class TechnologyView
    {
        public TechnologyView(TechnologyViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
