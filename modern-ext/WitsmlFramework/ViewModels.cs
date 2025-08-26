using System.ComponentModel;

namespace WitsmlFramework.ViewModels
{
    public interface ITreeViewContextMenuManipulator
    {
        void Manipulate(object context);
    }
    
    public interface IRuntimeService
    {
        void ShowError(string message);
        void ShowMessage(string message);
        void Invoke(Action action);
        void ShowBusy(string message = "");
        Task InvokeAsync(Func<Task> action);
        RuntimeShell Shell { get; }
        string OutputFolderPath { get; set; }
        IContainer Container { get; }
    }
    
    public class RuntimeShell
    {
        public string StatusBarText { get; set; } = "";
        public bool RetrievePartialResults { get; set; } = false;
        public object LogQuery { get; set; } = new object();
        public object LogResponse { get; set; } = new object();
        
        public void ShowBusy(bool busy = true) { }
        public void ShowBusy(string message) { }
        public void SetApplicationTitle(string title) { }
    }
    
    public interface IContainer
    {
        T[] ResolveAll<T>();
        T Resolve<T>();
    }

    public class TextEditorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public string Text { get; set; } = "";
        public bool IsReadOnly { get; set; } = false;
        public string Language { get; set; } = "xml";
        public bool IsPrettyPrintAllowed { get; set; } = true;
        public bool IsPrettyPrintEnabled { get; set; } = true;
        public object Document { get; set; } = new object();
        public bool ShowWriteSettings { get; set; } = false;
        
        public TextEditorViewModel(IRuntimeService runtime, string language, bool isReadOnly)
        {
            Language = language;
            IsReadOnly = isReadOnly;
        }
        
        public TextEditorViewModel(IRuntimeService runtime, string language)
        {
            Language = language;
            IsReadOnly = false;
        }
        
        public void Append(string text) { }
        public void SetText(string text) { Text = text; }
    }

    public class ConnectionPickerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public Connection SelectedConnection { get; set; }
        public IEnumerable<Connection> Connections { get; set; } = new List<Connection>();
        public bool AutoConnectEnabled { get; set; } = false;
        public Action OnConnectionChanged { get; set; } = () => { };
        
        public ConnectionPickerViewModel(IRuntimeService runtime, object connectionTypes)
        {
            SelectedConnection = new Connection();
        }
    }

    public class PropertyGridViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public object SelectedObject { get; set; }
        
        public PropertyGridViewModel(IRuntimeService runtime, object dataObject)
        {
            SelectedObject = dataObject;
        }
        
        public void SetCurrentObject(object obj)
        {
            SelectedObject = obj;
        }
        
        public void SetCurrentObject(object obj, string objectType, string family, string version, string wellUid, string wellboreUid, string objectUid)
        {
            SelectedObject = obj;
        }
    }

    public class DataGridViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public object ItemsSource { get; set; }
        
        public DataGridViewModel(IRuntimeService runtime)
        {
            ItemsSource = new object[0];
        }
        
        public void SetCurrentObject(string objectType, object currentObject, bool keepGridData, bool retrieveObjectSelection, Action<WitsmlFramework.WitsmlException> errorHandler)
        {
        }
        
        public void ClearDataTable()
        {
            ItemsSource = null;
        }
    }

    public class WitsmlTreeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public object TreeData { get; set; }
        public bool IsContextMenuEnabled { get; set; } = true;
        public bool DisableIndicatorQueries { get; set; } = false;
        public object WitsmlTreeViewContextManipulators { get; set; } = new object();
        public object Context { get; set; } = new object();
        public int? MaxDataRows { get; set; }
        public int? RequestLatestValues { get; set; }
        public string ExtraOptionsIn { get; set; } = "";
        
        public WitsmlTreeViewModel(IRuntimeService runtime)
        {
            TreeData = new object();
        }
        
        public void CreateContext(object connection, string version) { }
        public void CreateContext(object connection, WMLSVersion version) { }
        public void SetDataObjects(object data) { }
        public void OnViewReady() { }
    }

    public class GrowingObjectQueryProvider<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public bool IsCancelled { get; set; } = false;
        public object Context { get; set; }
        
        public GrowingObjectQueryProvider() { }
        public GrowingObjectQueryProvider(object context, object provider, object settings) 
        {
            Context = context;
        }
        
        public void UpdateDataQuery(string query) { }
    }
}
