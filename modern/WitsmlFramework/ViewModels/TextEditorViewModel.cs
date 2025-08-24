using Caliburn.Micro;
using ICSharpCode.AvalonEdit.Document;
using PDS.WITSMLstudio.Desktop.Core.Runtime;

namespace PDS.WITSMLstudio.Desktop.Core.ViewModels
{
    public class TextEditorViewModel : Screen
    {
        public TextEditorViewModel(IRuntimeService runtime, string language, bool isReadOnly = false)
        {
            Runtime = runtime;
            Language = language;
            IsReadOnly = isReadOnly;
            Document = new TextDocument();
        }

        public IRuntimeService Runtime { get; }
        public string Language { get; }
        public bool IsReadOnly { get; }
        public bool IsPrettyPrintAllowed { get; set; }
        public bool IsPrettyPrintEnabled { get; set; } = true;
        public TextDocument Document { get; set; }
        
        public string Text 
        { 
            get => Document.Text; 
            set => Document.Text = value ?? string.Empty; 
        }
        
        public bool ShowWriteSettings { get; set; } = false;
        
        public void SetText(string text)
        {
            Document.Text = text ?? string.Empty;
        }
        
        public string GetText()
        {
            return Document.Text;
        }
        
        public void Append(string text)
        {
            Document.Text += text ?? string.Empty;
        }
    }
}
