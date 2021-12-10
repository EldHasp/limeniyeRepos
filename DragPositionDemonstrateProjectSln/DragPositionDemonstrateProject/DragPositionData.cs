using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace DragPositionDemonstrateProject
{
    public class DragPositionData
    {
        private object zoomFactor;
        private object baseParent;

        public object ZoomFactor
        {
            get => zoomFactor;
            set
            {
                if ((value ?? 0.0) is double ||
                    value is BindingBase ||
                    (double.TryParse(value.ToString(), out double val) && (value = val) != null))
                    zoomFactor = value;
                else
                    throw new ArgumentException(nameof(value));
            }
        }
        public object BaseParent
        {
            get => baseParent;
            set
            {
                if (value is UIElement || value is BindingBase)
                    baseParent = value;
                else
                    throw new ArgumentException(nameof(value));
            }
        }
    }
}
