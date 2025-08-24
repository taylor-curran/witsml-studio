using Caliburn.Micro;
using PDS.WITSMLstudio.Desktop.Core.Runtime;

namespace PDS.WITSMLstudio.Desktop.Core.ViewModels
{
    public class WitsmlTreeViewModel : Screen
    {
        private readonly IRuntimeService _runtimeService;
        
        public WitsmlTreeViewModel(IRuntimeService runtimeService)
        {
            _runtimeService = runtimeService;
        }
        
        public object TreeData { get; set; } = new object();
        public bool IsContextMenuEnabled { get; set; } = true;
        public bool DisableIndicatorQueries { get; set; } = false;
        public object WitsmlTreeViewContextManipulators { get; set; } = new object();
        public int MaxDataRows { get; set; } = 1000;
        public int RequestLatestValues { get; set; } = 0;
        public string ExtraOptionsIn { get; set; } = string.Empty;
        public object Context { get; set; }
        
        public void CreateContext(object connection)
        {
            Context = connection;
        }
        
        public void CreateContext(object connection, object version)
        {
            Context = connection;
        }
        
        public void SetDataObjects(object dataObjects)
        {
        }
        
        public void OnViewReady(object view)
        {
        }
    }
}
