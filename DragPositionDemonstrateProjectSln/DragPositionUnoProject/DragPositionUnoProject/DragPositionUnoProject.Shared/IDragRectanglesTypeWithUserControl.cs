﻿using System.ComponentModel;
using Windows.UI.Xaml.Controls;

namespace DragPositionUnoProject
{
    public interface IDragRectanglesTypeWithUserControl : INotifyPropertyChanged
    {
        object Content { get; }
    }
}
