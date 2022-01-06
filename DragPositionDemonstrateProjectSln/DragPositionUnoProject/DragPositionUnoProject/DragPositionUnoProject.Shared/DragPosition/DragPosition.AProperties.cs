using System;
using System.ComponentModel;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace DragPosition
{
    public partial class DragPosition
    {
        #region DragPosition

#if WINDOWS_UWP
        public static DragPositionData GetDragPosition(UIElement element)
        {
            return (DragPositionData)element.GetValue(DragPositionProperty);
        }

        public static void SetDragPosition(UIElement element, DragPositionData value)
        {
            element.SetValue(DragPositionProperty, value);
        }

        // Using a DependencyProperty as the backing store for DragPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragPositionProperty =
            DependencyProperty.RegisterAttached(nameof(SetDragPosition).Substring(3), typeof(DragPositionData), typeof(DragPosition), new PropertyMetadata(null, DragPositionChanged));

        private static void DragPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine($"{nameof(DragPositionChanged)} dependencyObject:{d}; NewValue:{e.NewValue?.GetType().Name ?? "null"}");

            UIElement element = (UIElement)d;
            //DragPositionData odata = (DragPositionData)e.OldValue;
            //if (odata != null)
            //    odata.PropertyDataChanged -= (s, p, o, n) => OnPropertyDataChanged(element, (DragPositionData)s, p, o, n);
            DragPositionData data = (DragPositionData)e.NewValue;
            if (data != null)
                data.PropertyChanged += (s, arg) => OnPropertyDataChanged(element, (DragPositionData)s, arg);

            //if (data.BaseParent is UIElement parent)
            //    SetBaseParent(element, parent);
            //else
            //    BindingOperations.SetBinding(element, BaseParentProperty, (BindingBase)data.BaseParent);

            //if (data.OffsetX is double x)
            //    SetOffsetX(element, x);
            //else
            //    BindingOperations.SetBinding(element, OffsetXProperty, (BindingBase)data.OffsetX);

            //if (data.OffsetY is double y)
            //    SetOffsetY(element, y);
            //else
            //    BindingOperations.SetBinding(element, OffsetYProperty, (BindingBase)data.OffsetY);

            OnPropertyDataChanged(element, data, null);
            data.BindingAction?.Invoke(element);



            //HandlersData handlersData = new HandlersData(element, GetBaseParent(element));
            //SetHandlersData(element, handlersData);
        }

        private static void OnPropertyDataChanged(UIElement element, DragPositionData data, PropertyChangedEventArgs arg)
        {
            string propertyName = arg?.PropertyName;

            if (CheckName(propertyName, nameof(DragPositionData.BaseParent)))
            {
                if (!(data.BaseParent is BindingBase binding))
                {
                    binding = new Binding()
                    {
                        Source = data,
                        Path = new PropertyPath(nameof(DragPositionData.BaseParent))
                    };
                }
                BindingOperations.SetBinding(element, BaseParentProperty, binding);
            }
             if (CheckName(propertyName, nameof(DragPositionData.OffsetX)))
            {
                if (!(data.OffsetX is BindingBase binding))
                {
                    binding = new Binding()
                    {
                        Source = data,
                        Path = new PropertyPath(nameof(DragPositionData.OffsetX))
                    };
                }
                BindingOperations.SetBinding(element, OffsetXProperty, binding);
            }
            if (CheckName(propertyName, nameof(DragPositionData.OffsetY)))
            {
                if (!(data.OffsetY is BindingBase binding))
                {
                    binding = new Binding()
                    {
                        Source = data,
                        Path = new PropertyPath(nameof(DragPositionData.OffsetY))
                    };
                }
                BindingOperations.SetBinding(element, OffsetYProperty, binding);
            }
       }

        private static bool CheckName(string propertyName, string nameof)
        {
            return string.IsNullOrEmpty(propertyName) ||
                propertyName == nameof;
        }

#else
        public static DragPositionData GetDragPosition(UIElement element)
        {
            return (DragPositionData)element.GetValue(DragPositionProperty);
        }

        public static void SetDragPosition(UIElement element, DragPositionData value)
        {
            element.SetValue(DragPositionProperty, value);
        }

        // Using a DependencyProperty as the backing store for DragPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragPositionProperty =
            DependencyProperty.RegisterAttached(nameof(SetDragPosition).Substring(3), typeof(DragPositionData), typeof(DragPosition), new PropertyMetadata(null, DragPositionChanged));

        private static void DragPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
#endif
        #endregion

        #region Offset properties
        public static double GetOffsetX(UIElement element)
        {
            return (double)element.GetValue(OffsetXProperty);
        }

        public static void SetOffsetX(UIElement element, double value)
        {
            element.SetValue(OffsetXProperty, value);
        }

        // Using a DependencyProperty as the backing store for OffsetX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetXProperty =
            DependencyProperty.RegisterAttached(nameof(GetOffsetX).Substring(3), typeof(double), typeof(DragPosition), new PropertyMetadata(0.0));



        public static double GetOffsetY(UIElement element)
        {
            return (double)element.GetValue(OffsetYProperty);
        }

        public static void SetOffsetY(UIElement element, double value)
        {
            element.SetValue(OffsetYProperty, value);
        }

        // Using a DependencyProperty as the backing store for OffsetY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetYProperty =
            DependencyProperty.RegisterAttached(nameof(GetOffsetY).Substring(3), typeof(double), typeof(DragPosition), new PropertyMetadata(0.0, OffsetYChanged));

        private static void OffsetYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: Отладочный вывод.
            Debug.WriteLine($"OffsetYChanged: {e.NewValue}");
        }

        #endregion

        #region BaseParent
        /// <summary>Возвращает значение присоединённого свойства BaseParent для <paramref name="element"/>.</summary>
        /// <param name="element"><see cref="UIElement"/> значение свойства которого будет возвращено.</param>
        /// <returns><see cref="UIElement"/> значение свойства.</returns>
        public static UIElement GetBaseParent(UIElement element)
        {
            return (UIElement)element.GetValue(BaseParentProperty);
        }

        /// <summary>Задаёт значение присоединённого свойства BaseParent для <paramref name="element"/>.</summary>
        /// <param name="element"><see cref="UIElement"/> значение свойства которого будет возвращено.</param>
        /// <param name="value"><see cref="UIElement"/> значение для свойства.</param>
        public static void SetBaseParent(UIElement element, UIElement value)
        {
            element.SetValue(BaseParentProperty, value);
        }

        /// <summary><see cref="DependencyProperty"/> для методов <see cref="GetBaseParent(UIElement)"/> и <see cref="SetBaseParent(UIElement, UIElement)"/>.</summary>
        public static readonly DependencyProperty BaseParentProperty =
            DependencyProperty.RegisterAttached(nameof(GetBaseParent).Substring(3), typeof(UIElement), typeof(DragPosition), new PropertyMetadata(null, BaseParentChanged));

        private static void BaseParentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = (UIElement)d;

            HandlersData oldData = GetHandlersData(element);
            if (oldData != null)
            {
                oldData.Dispose();
            }

            if (e.NewValue is UIElement parent)
            {
                HandlersData newData = new HandlersData(element, parent);
                SetHandlersData(element, newData);
            }
            else
            {
                element.ClearValue(BaseParentProperty);
            }
        }
        #endregion
    }
}
