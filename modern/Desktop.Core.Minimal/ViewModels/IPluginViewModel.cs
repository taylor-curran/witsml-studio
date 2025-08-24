using Caliburn.Micro;

namespace PDS.WITSMLstudio.Desktop.Core.ViewModels
{
    public interface IPluginViewModel : IScreen
    {
        new string DisplayName { get; set; }
    }
}
