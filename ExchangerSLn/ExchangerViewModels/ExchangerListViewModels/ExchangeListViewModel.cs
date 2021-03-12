using Common.Enums;
using Common.Interfaces.Model;
using Common.Interfaces.ViewModel;
using DtoTypes;
using ExchangerModels;
using Simplified;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Common;
using System.Windows.Threading;
using System;
using Common.Dispatchers;
using System.Threading.Tasks;
using Common.EventsArgs;

namespace ExchangerViewModels.ExchangerListViewModels
{
    public class ExchangeListViewModel : BaseViewModel, IExchangerListViewModel
    {
        #region Private Fields
        private readonly IExchangerModel model;
        private int _rows, _columns, _currentPage;
        private ICellViewModel _selectedCell;
        public event ExchangeSelectedHandler ExchangeSelected;

        #endregion

        #region Public Properties
        public int Rows { get => _rows; set => Set(ref _rows, value); }
        public int Columns { get => _columns; set => Set(ref _columns, value); }
        public int CurrentPageIndex { get => _currentPage; set => Set(ref _currentPage, value); }
        #endregion

        #region Collections
        private IList<ExchangeDto> allRates => model.Exchanges.ToList();
        private IList<ICellViewModel[]> pages { get; }
        public ObservableCollection<ICellViewModel> Page { get; } = new ObservableCollection<ICellViewModel>();
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
            bool isMultiple = true;
            int startIndex = 0;
            int pages = 0;

            if (allRates.Count % (count - 1) == 0)
                pages = allRates.Count / (count - 1);
            else
            {
                pages = (allRates.Count / (count - 1)) + 1;
                isMultiple = false;
            }

            ICellViewModel[][] resultList = new CellViewModel[pages][];

            if (allRates.Count <= count)
            {
                resultList[0] = new CellViewModel[allRates.Count];
                for (int i = 0; i < allRates.Count; i++)
                {
                    resultList[0][i] = new CellViewModel(allRates[i], CellTypesEnum.Content);
                }
                return resultList;
            }

            int emptyElements = (count - 1) * pages - allRates.Count;

            for (int page = 0; page < pages; page++)
            {
                resultList[page] = new CellViewModel[count];
                int indexInMatrix = 0;
                for (int i = startIndex; i < (count - 1) * (page + 1) && i < allRates.Count; i++)
                {
                    resultList[page][indexInMatrix] = new CellViewModel(allRates[i], CellTypesEnum.Content);
                    indexInMatrix++;
                }

                if (!isMultiple && page +1 == pages)
                    for (int i = 0; i < emptyElements; i++)
                    {
                        resultList[page][indexInMatrix] = new CellViewModel(allRates[i], CellTypesEnum.Empty);
                        indexInMatrix++;
                    }

                resultList[page][indexInMatrix] = new CellViewModel(null, CellTypesEnum.Next);

                startIndex += (count - 1);
            }

            return resultList;
        }

        /// <summary> Берёт из всех готовых сраниц нужную.</summary>
        /// <param name="page"> Индекс страницы </param>
        /// <returns> Страница с индексом <see cref="page"/> </returns>
        private ObservableCollection<CellViewModel> GetPage(int page)
        {
            //явно указываю тип
            return new ObservableCollection<CellViewModel>((CellViewModel[])pages[page]);
        }

        /// <summary> Метод обработки выбранной ячейки </summary>
        /// <param name="cell"> Выбранная ячейка. </param>
        private void CellSelectedMethod(ICellViewModel cell)
        {
            switch (cell.CellType)
            {
                case CellTypesEnum.Content:
                    ExchangeSelected?.Invoke(this, (ExchangeDto)cell.Data);
                    break;
                case CellTypesEnum.Back:
                    if (CurrentPageIndex <= 0)
                        CurrentPageIndex = pages.Count - 1;
                    else
                        CurrentPageIndex--;
                    Page.Clear();

                    foreach (var item in GetPage(CurrentPageIndex))
                    {
                        Page.Add(item);
                    }
                    break;
                case CellTypesEnum.Next:
                    if (CurrentPageIndex >= pages.Count - 1)
                        CurrentPageIndex = 0;
                    else
                        CurrentPageIndex++;
                    Page.Clear();
                    
                    foreach (var item in GetPage(CurrentPageIndex))
                    {
                        Page.Add(item);
                    }
                    break;
            }
        }
        #endregion

        #region RelayCommands

        private RelayCommand<ICellViewModel> _selectCellCommand;

        

        public RelayCommand<ICellViewModel> SelectCellCommand => _selectCellCommand ?? (_selectCellCommand = new RelayCommand<ICellViewModel>(CellSelectedMethod));
        #endregion


        public ExchangeDto SelectedExchange => throw new System.NotImplementedException();


        public ExchangeListViewModel(IExchangerModel model, int rows, int columns)
        {
            this.model = model;

            Rows = rows; Columns = columns;
            pages = GetAllPages(Rows, Columns).ToList(); // готовые, заполенные кнопками страницы. 
            CurrentPageIndex = 0;

            foreach (var item in GetPage(CurrentPageIndex))
            {
                Page.Add(item);
            }
            model.ExchangesCnahged += Model_ExchangesCnahged;
        }

        private void Model_ExchangesCnahged(object sender, ChangedAction action, IEnumerable<ExchangeDto> newExchanges)
        {
            switch (action)
            {
                case ChangedAction.Clear:
                    pages.Clear();
                    break;
                case ChangedAction.AddedOrChanged:
                    foreach (var rate in newExchanges)
                        allRates.ReplaceOrAdd(r => r.Id == rate.Id, rate);

                    pages.Clear();

                    foreach (var item in GetAllPages(Rows, Columns).ToList())
                    {
                        pages.ReplaceOrAdd((x => x == item), item);
                    }


                    if (Dispatcher.CheckAccess())
                        AddToCollection(pages[CurrentPageIndex]);
                    else
                        Dispatcher.BeginInvoke((Action)(() => AddToCollection(pages[CurrentPageIndex])));

                    break;
                default:
                    break;
            }
        }

        private void AddToCollection(IEnumerable<ICellViewModel> newRates)
        {
            Page.Initial(newRates);
        }
    }
}
