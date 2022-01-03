using Windows.UI.Xaml.Controls;

namespace DragPositionUnoProject.Skia.Wpf.Host.Views
{
    public class DragRectanglesTypeWithUserControl : OnNotifyPropertyChanged, IDragRectanglesTypeWithUserControl
    {
        private ContentPresenter _contentPresenter;
        public ContentPresenter ContentPresenter
        {
            get => _contentPresenter;
            set => SetProperty( ref _contentPresenter, value); 
        }

        public DragRectanglesTypeWithUserControl()
        {
            ContentPresenter = new ContentPresenter();
            ContentPresenter.Content = new UserControl1();
        }
    }
}