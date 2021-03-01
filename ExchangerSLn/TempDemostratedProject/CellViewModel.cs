using DtoTypes;
using Simplified;

namespace TempDemostratedProject
{
    public class CellViewModel : BaseInpc, ICellViewModel
    {
        private int _row, _column;
        private object _data;
        private CellTypesEnum _type;

        public int Row { get => _row; set => Set(ref _row, value);}
        public int Column { get => _column; set => Set(ref _column, value); }
        public object Data { get => _data; set => Set(ref _data, value); }
        public CellTypesEnum CellType { get => _type; set => Set(ref _type, value); }

        public CellViewModel(int row, int column)
        {
            CellType = CellTypesEnum.Empty;
        }
        public CellViewModel(int row, int column, object data, CellTypesEnum cellType)
        {
            Row = row; Column = column; Data = data; CellType = cellType;
        }
    }
}
