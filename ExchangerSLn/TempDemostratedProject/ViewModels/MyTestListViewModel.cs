using DtoTypes;
using Simplified;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TempDemostratedProject.Enums;
using TempDemostratedProject.Interfaces;
using TempDemostratedProject.Models;

namespace TempDemostratedProject.ViewModels
{
    public class MyTestListViewModel : BaseInpc, ITestList
    {
        #region Private Fields
        private readonly Model model;
        private int _rows, _columns, _currentPage;
        private ICellViewModel _selectedCell;
        #endregion

        #region Public Properties
        public int Rows { get => _rows; set => Set(ref _rows, value); }
        public int Columns { get => _columns; set => Set(ref _columns, value); }
        public int CurrentPageIndex { get => _currentPage; set => Set(ref _currentPage, value); }
        #endregion

        #region Collections
        private IList<RateDto> allRates => model.Rates.ToList();
        private IList<ICellViewModel[]> pages { get; }
        public ObservableCollection<ICellViewModel> Page { get; private set; }
        #endregion

        #region Methods

        /// <summary> Разбивает целый список на подмассивы каждой сраницы и её элементов. </summary>
        /// <param name="rows"> Максимальное число строк. </param>
        /// <param name="columns"> Максимальное число колонок. </param>
        /// <returns> Массив готовых страниц с заполненными ячейками. </returns>
        /// <remarks> В этой системе есть и недоработки. Например если будет только 1 страница, то кнопки навигации тоже будут отображаться. Будет исправлено в следующем обновлении. </remarks>
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

        /// <summary> Берёт из всех готовых сраниц нужную.</summary>
        /// <param name="page"> Индекс страницы </param>
        /// <returns> Страница с индексом <see cref="page"/> </returns>
        private ObservableCollection<ICellViewModel> GetPage(int page)
        {
            return new ObservableCollection<ICellViewModel>(pages[page]);
        }

        /// <summary> Метод обработки выбранной ячейки </summary>
        /// <param name="cell"> Выбранная ячейка. </param>
        private void CellSelectedMethod(ICellViewModel cell)
        {
            switch (cell.CellType)
            {
                case CellTypesEnum.Back:
                    if (CurrentPageIndex <= 0)
                        CurrentPageIndex = pages.Count - 1;
                    else
                        CurrentPageIndex--;
                    Page = GetPage(CurrentPageIndex);
                    break;
                case CellTypesEnum.Next:
                    if (CurrentPageIndex >= pages.Count - 1)
                        CurrentPageIndex = 0;
                    else
                        CurrentPageIndex++;
                    Page = GetPage(CurrentPageIndex);
                    break;
            }
        }
        #endregion

        #region RelayCommands

        private RelayCommand<ICellViewModel> _selectCellCommand;
        public RelayCommand<ICellViewModel> SelectCellCommand => _selectCellCommand ?? (_selectCellCommand = new RelayCommand<ICellViewModel>(CellSelectedMethod));

        #endregion

        public MyTestListViewModel(Model model, int rows, int columns)
        {
            this.model = model;
            Rows = rows; Columns = columns;
            pages = GetAllPages(Rows, Columns).ToList(); // готовые, заполенные кнопками страницы. 
            CurrentPageIndex = 0;
            Page = GetPage(CurrentPageIndex);
        }
    }
}
