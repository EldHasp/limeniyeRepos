using System.ComponentModel;
using Windows.UI.Xaml.Controls;

namespace CommonControls
{
    public interface IDragRectanglesTypeWithUserControl : INotifyPropertyChanged
    {
        ContentPresenter ContentPresenter { get; }
    }
}
