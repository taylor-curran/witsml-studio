using Caliburn.Micro;
using PDS.WITSMLstudio.Desktop.Core.Runtime;

namespace PDS.WITSMLstudio.Desktop.Core.ViewModels
{
    public class DataGridViewModel : Screen
    {
        private readonly IRuntimeService _runtimeService;
        
        public DataGridViewModel(IRuntimeService runtimeService)
        {
            _runtimeService = runtimeService;
        }
        
        public object DataSource { get; set; } = new object();
    }
}
