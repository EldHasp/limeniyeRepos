using DtoTypes;
using Simplified;
using System.Collections.Generic;

namespace TempDemostratedProject
{
    public class MainViewModel : BaseInpc
    {
        private readonly Model model;
        private IReadOnlyCollection<RateDto> allRates => model.Rates;

        private IEnumerable<IEnumerable<ICellViewModel>> GetAllPages(int rows, int columns)
        {
            int count = rows * columns;

            List<List<ICellViewModel>> tempMainList = new List<List<ICellViewModel>>();
            List<ICellViewModel> tempList = new List<ICellViewModel>();
            for(int i = 0; i < allRates.Count; i ++)
            {
                if( i % (count - 1) == 0) // back
                    tempList.Add(new CellViewModel())
            }

            return null;
        }

        public MainViewModel(Model model)
        {
            this.model = model;
        }
    }
}
