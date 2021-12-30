using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;

namespace DragPositionWPFSolution.DragPosition
{
    /// <summary>Дополнитеьные данные (специфичные для конкретного использования)
    /// для создания привязок в DragPositionBehavior.</summary>
    public class DragPositionDataBindingCanvas : DragPositionData
    {
        public DragPositionDataBindingCanvas()
        {
            BindingAction = PrivateBindingAction;
        }

        public Binding LeftBinding { get; set; }
        public Binding TopBinding { get; set; }

        private void PrivateBindingAction(DependencyObject d)
        {
            BindingOperations.SetBinding(d, Canvas.LeftProperty, LeftBinding);
            BindingOperations.SetBinding(d, Canvas.TopProperty, TopBinding);
        }

    }

}
