using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using DynamicData.Binding;

namespace pocbindingdmo.ViewModels
{
    public class ViewModelBase : AbstractNotifyPropertyChanged
    {
        private readonly ViewModelBase _vm;

        public ViewModelBase() : base()
        {
            
        }
        public ViewModelBase(ViewModelBase vm)
        {
            _vm = vm;
            SetAndRaise(ref _vm, vm);
        }
    }
}
