﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace DragPositionDemonstrateProject
{
    /// <summary>Дополнитеьные данные (специфичные для конкретного использования)
    /// для создания привязок в DragPositionBehavior.</summary>
    public class DragPositionDataBindingCanvas : DragPositionData
    {
        public DragPositionDataBindingCanvas()
        {
            BindingAction = bindingAction;
        }

        public Binding LeftBinding { get; set; }
        public Binding TopBinding { get; set; }

        private void bindingAction(DependencyObject d)
        {
            BindingOperations.SetBinding(d, Canvas.LeftProperty, LeftBinding);
            BindingOperations.SetBinding(d, Canvas.TopProperty, TopBinding);
        }

    }

}