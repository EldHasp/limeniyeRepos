using DtoTypes;
using Simplified;
using System.Collections.Generic;
using System.Linq;

namespace TempDemostratedProject
{
    public class MainViewModel : BaseInpc
    {
        private readonly Model model;
        private IList<RateDto> allRates => model.Rates.ToList();

        /// <summary> Разбивае целый список на подмассивы каждой сраницы и её элементов. </summary>
        /// <param name="rows"> Максимальное число строк. </param>
        /// <param name="columns"> Максимальное число колонок. </param>
        /// <returns> Массив готовых страниц с заполненными ячейками. </returns>
        /// <remarks> В это сестеме есть и недоработки. Например если будет только 1 страница, то кнопки навигации тоже будут отображаться. Будет исправлено в следующем обновлении. </remarks>
        private IEnumerable<ICellViewModel[]> GetAllPages(int rows, int columns)
        {
            int count = rows * columns;
            int pages = allRates.Count % count - 2 == 0 ? allRates.Count / (count - 2) : (allRates.Count / (count - 2)) + 1;
            ICellViewModel[][] resultList = new CellViewModel[pages][];
            int startIndex = 0;

            for (int page = 0; page < pages; page++)
            {
                resultList[page] = new CellViewModel[count];
                int indexInMatrix = 0;
                for (int i = startIndex; i < (count - 2) * (page + 1) && i < allRates.Count; i++)
                {
                    resultList[page][indexInMatrix] = new CellViewModel(allRates[i], CellTypesEnum.Content);
                    indexInMatrix++;
                }
                resultList[page][indexInMatrix] = new CellViewModel(null, CellTypesEnum.Back);
                resultList[page][indexInMatrix + 1] = new CellViewModel(null, CellTypesEnum.Next);

                startIndex += count - 2;
            }

            return resultList;
        }

        public MainViewModel(Model model)
        {
            this.model = model;

            //res -- это разбивание коллекции на готовые страницы с элементами. 
            var res = GetAllPages(3, 3);
        }
    }
}
