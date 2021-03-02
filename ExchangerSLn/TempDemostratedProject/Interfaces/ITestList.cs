using System.Collections.ObjectModel;

namespace TempDemostratedProject.Interfaces
{
    public interface ITestList
    {
        int Rows { get; }
        int Columns { get; }
        ObservableCollection<ICellViewModel> Page { get; }
    }
}
