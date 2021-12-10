using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace DragPositionDemonstrateProject
{
    public class OffsetBinding
    {
        public DependencyProperty PropertyX { get; set; }
        public DependencyProperty PropertyY { get; set;}

        public BindingBase BindingX { get; set; }
        public BindingBase BindingY { get; set; }
    }
}
