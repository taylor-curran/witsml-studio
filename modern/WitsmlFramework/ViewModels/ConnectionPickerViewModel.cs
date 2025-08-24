using System;
using Caliburn.Micro;
using PDS.WITSMLstudio.Desktop.Core.Runtime;
using Energistics.DataAccess;

namespace PDS.WITSMLstudio.Desktop.Core.ViewModels
{
    public class ConnectionPickerViewModel : Screen
    {
        private readonly IRuntimeService _runtimeService;
        
        public ConnectionPickerViewModel(IRuntimeService runtimeService)
        {
            _runtimeService = runtimeService;
        }
        
        public ConnectionPickerViewModel(IRuntimeService runtimeService, object parent)
        {
            _runtimeService = runtimeService;
            Parent = parent;
        }
        
        public Connection SelectedConnection { get; set; } = new Connection();
        
        public bool AutoConnectEnabled { get; set; }
        
        public System.Action OnConnectionChanged { get; set; }
    }
}
