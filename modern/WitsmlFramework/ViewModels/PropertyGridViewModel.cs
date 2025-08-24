using System;
using System.ComponentModel;
using System.Windows;
using Caliburn.Micro;
using PDS.WITSMLstudio.Desktop.Core.Runtime;

namespace PDS.WITSMLstudio.Desktop.Core.ViewModels
{
    public class PropertyGridViewModel : Screen
    {
        public PropertyGridViewModel(IRuntimeService runtime)
        {
            Runtime = runtime;
        }
        
        public PropertyGridViewModel(object selectedObject, string displayName)
        {
            SelectedObject = selectedObject;
            DisplayName = displayName;
        }

        public IRuntimeService Runtime { get; }
        
        private object _selectedObject;
        public object SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                _selectedObject = value;
                NotifyOfPropertyChange(() => SelectedObject);
            }
        }

        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                _isReadOnly = value;
                NotifyOfPropertyChange(() => IsReadOnly);
            }
        }

        public void SetObject(object obj, bool readOnly = false)
        {
            SelectedObject = obj;
            IsReadOnly = readOnly;
        }
        
        public void SetCurrentObject(object obj)
        {
            SelectedObject = obj;
        }
    }
}
