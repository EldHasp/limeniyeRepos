using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TilesBox
{
    public partial class TilesPage : ContentControl
    {
        /// <summary>Возвращает тип контента <paramref name="tile"/>.</summary>
        /// <param name="tile"><see cref="TilesPageItem"/>.</param>
        /// <returns><see cref="TileTypeContentEnum"/>, если <paramref name="tile"/> 
        /// или <see langword="null"/>.</returns>
        public static TileTypeContentEnum? GetTileType(TilesPageItem tile)
        {
            return (TileTypeContentEnum?)tile.GetValue(TileStatusProperty);
        }

        protected static void SetTileType(TilesPageItem tile, TileTypeContentEnum? value)
        {
            tile.SetValue(TileStatusPropertyKey, value);
        }

        /// <summary><see cref="DependencyPropertyKey"/> для <see cref="SetTileType(TilesPageItem, TileTypeContentEnum?)"/>.</summary>
        protected static readonly DependencyPropertyKey TileStatusPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(nameof(GetTileType).Substring(3), typeof(TileTypeContentEnum?), typeof(TilesPage), new PropertyMetadata(null, TileTypeChanged));
        /// <summary><see cref="DependencyProperty"/> для <see cref="GetTileType(TilesPageItem)"/>.</summary>
        public static readonly DependencyProperty TileStatusProperty = TileStatusPropertyKey.DependencyProperty;

        private static void TileTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is TilesPageItem tile))
                throw new InvalidCastException($"Только для экземпляров совместимых с типом {typeof(TilesPageItem)}.");
        }


        /// <summary>Возвращает значение присоединённого свойства IsTileEnabled для <paramref name="tile"/>.</summary>
        /// <param name="tile">TilesPageItem значение свойства которого будет возвращено.</param>
        /// <returns><see cref="bool?"/> значение свойства.</returns>
        public static bool? GetIsTileEnabled(TilesPageItem tile)
        {
            return (bool?)tile.GetValue(IsTileEnabledProperty);
        }

        /// <summary>Задаёт значение присоединённого свойства IsTileEnabled для <paramref name="tile"/>.</summary>
        /// <param name="tile"><see cref="TilesPageItem"/>  значение свойства которого будет возвращено.</param>
        /// <param name="value"><see cref="bool?"/> значение для свойства.</param>
        protected static void SetIsTileEnabled(TilesPageItem tile, bool? value)
        {
            tile.SetValue(IsTileEnabledPropertyKey, value);
        }

        /// <summary><see cref="DependencyPropertyKey"/> для метода <see cref="SetIsTileEnabled(TilesPageItem, bool?)"/>.</summary>
        public static readonly DependencyPropertyKey IsTileEnabledPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(nameof(GetIsTileEnabled).Substring(3), typeof(bool?), typeof(TilesPage), new PropertyMetadata(null, IsTileEnabledChanged));

        private static void IsTileEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is TilesPageItem tile))
                throw new InvalidCastException($"Только для экземпляров совместимых с типом {typeof(TilesPageItem)}.");
        }

        /// <summary><see cref="DependencyProperty"/> для метода <see cref="GetIsTileEnabled(TilesPageItem)"/>.</summary>
        public static readonly DependencyProperty IsTileEnabledProperty = IsTileEnabledPropertyKey.DependencyProperty;

    }

    /// <summary>Тип контента <see cref="TilesPageItem"/>.</summary>
    public enum TileTypeContentEnum
    {
        None = 0,
        Item = 1,
        PreviousButton = 2,
        NextButton = 3
    }
}
