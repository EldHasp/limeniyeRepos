using DtoTypes;
using System.Collections.ObjectModel;

namespace Common.Interfaces.ViewModel
{
    public interface IRatesViewModel
    {
        ObservableCollection<RateDto> Rates { get; }
    }
}
