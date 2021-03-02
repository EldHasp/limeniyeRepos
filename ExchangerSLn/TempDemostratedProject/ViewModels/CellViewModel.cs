using DtoTypes;
using Simplified;
using TempDemostratedProject.Enums;
using TempDemostratedProject.Interfaces;

namespace TempDemostratedProject.ViewModels
{
    /// <summary>
    /// Класс описывающий ячейку
    /// </summary>
    public class CellViewModel : BaseInpc, ICellViewModel
    {
        private object _data;
        private CellTypesEnum _type;

        public object Data { get => _data; set => Set(ref _data, value); }
        public CellTypesEnum CellType { get => _type; set => Set(ref _type, value); }

        public CellViewModel()
        {
            CellType = CellTypesEnum.Empty;
        }
        public CellViewModel(object data, CellTypesEnum cellType)
        {
           Data = data; CellType = cellType;
        }
    }
}
