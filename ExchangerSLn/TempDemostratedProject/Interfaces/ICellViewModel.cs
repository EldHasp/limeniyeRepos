using TempDemostratedProject.Enums;

namespace TempDemostratedProject.Interfaces
{
    /// <summary>
    /// Интерфейс каждой ячейки
    /// </summary>
    public interface ICellViewModel
    {
        string Margin { get; }
        CellTypesEnum CellType { get; }
    }
}
