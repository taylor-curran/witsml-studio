using System;
using System.Threading.Tasks;

namespace PDS.WITSMLstudio.Desktop.Core.Runtime
{
    public interface IRuntimeService
    {
        string DataFolderPath { get; }
        string OutputFolderPath { get; set; }
        object Container { get; }
        object Shell { get; }
        
        void Invoke(Action action);
        Task InvokeAsync(Func<Task> action);
        bool ShowDialog(object viewModel);
        bool ShowConfirm(string message);
        void ShowError(string message);
        void ShowWarning(string message);
        void ShowInfo(string message);
        void ShowBusy(bool isBusy = true);
        void EnsureDataFolder();
    }
}
