namespace DragPositionUnoProject.Skia.Wpf.Host.Views
{
    public class DragRectanglesTypeWithUserControl : OnNotifyPropertyChanged, IDragRectanglesTypeWithUserControl
    {
        private object _content;
        public object Content
        {
            get => _content;
            set => SetProperty( ref _content, value); 
        }

        public DragRectanglesTypeWithUserControl()
        {
            Content = new UserControl1();
        }
    }
}