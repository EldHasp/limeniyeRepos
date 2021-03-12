using Common.Enums;

namespace Common.Interfaces.ViewModel
{
    /// <summary>
    /// Интерфейс каждой ячейки
    /// </summary>
    public interface ICellViewModel
    {
        object Data { get; }
        CellTypesEnum CellType { get; }
    }
}
