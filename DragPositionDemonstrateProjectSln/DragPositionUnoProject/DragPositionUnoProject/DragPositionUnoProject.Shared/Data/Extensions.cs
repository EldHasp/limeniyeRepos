using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace DragPositionUnoProject.Data
{
    public static class Extensions
    {
        public static T GetParent<T>(this DependencyObject element, bool excluding = true)
            where T : DependencyObject
        {
            if (excluding)
                element = VisualTreeHelper.GetParent(element);

            while (element != null && !(element is T))
            {
                element = VisualTreeHelper.GetParent(element);
            }

            return (T) element;
        }
        public static DependencyObject GetWithParent<T>(this DependencyObject element)
           where T : DependencyObject
        {
            DependencyObject parent;

            while (element != null && !((parent = VisualTreeHelper.GetParent(element)) is T))
            {
                element = parent;
            }

            return element;
        }
    }
}