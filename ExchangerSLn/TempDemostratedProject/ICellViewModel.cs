namespace TempDemostratedProject
{
    public interface ICellViewModel
    {
        int Row { get; }
        int Column { get; }
        CellTypesEnum CellType { get; }
    }
}
