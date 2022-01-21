using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Threading;
using Avalonia.Utilities;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace pocbindingdmo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // public SourceList<ViewModelBase> ListTypes { get; set; }
        // public IObservableList<ViewModelBase> ObservableList { get; set; }
        // public IObservable<IChangeSet<ViewModelBase>> ListBindedTypes { get; set; }
        // public SourceList<Object1> ListObject1 { get; set; }
        // public SourceList<Object2> ListObject2 { get; set; }
        private readonly ReadOnlyObservableCollection<ViewModelBase> _testBind;
        public ReadOnlyObservableCollection<ViewModelBase> TestBind => _testBind;
        
        public IObservable<IChangeSet<ViewModelBase>> SeveralListTypes { get; set; }
        
        public IObservable<IChangeSet<ViewModelBase>> SeveralListTypes2 { get; set; }
        public IObservable<IChangeSet<ViewModelBase>> SeveralListTypes3 { get; set; }

        public ObservableCollection<ViewModelBase> ListTypesObject1 { get; set; } 
        public ObservableCollection<ViewModelBase> ListTypesObject2 { get; set; }
        public ObservableCollection<ViewModelBase> ListTypesObject3 { get; set; }
        public IObservable<IChangeSet<ViewModelBase>> InBoth { get; set; }
        
        
        private readonly ReadOnlyObservableCollection<ViewModelBase> _testBindTypes;
        public ReadOnlyObservableCollection<ViewModelBase> TestBindTypes => _testBindTypes;
        public MainWindowViewModel()
        {

            // TODO : those object collections should be of the real type and not from ancestor
            // ListTypesObject1 = new ObservableCollection<Object1>()

            ListTypesObject1 = new ObservableCollection<ViewModelBase>()
            {
                new Object1(),
            };
            
            ListTypesObject2 = new ObservableCollection<ViewModelBase>()
            {
                new Object2(),
            };
            
            ListTypesObject3 = new ObservableCollection<ViewModelBase>()
            {
                new Object3(),
            };


            // Change observableCollection to IObservable to be running with engine ReactiveUI
            SeveralListTypes = ListTypesObject1.ToObservableChangeSet();
            SeveralListTypes2 = ListTypesObject2.ToObservableChangeSet();
            SeveralListTypes3 = ListTypesObject3.ToObservableChangeSet();
            
            //Group All Observable into one with Or operator 
            InBoth = SeveralListTypes.Or(SeveralListTypes2).Or(SeveralListTypes3);

            // Bind => output to Binded Property for xaml
            // Subscribe => to be notified when changes
            var t = InBoth.Bind(out _testBindTypes)
                .DisposeMany()
                .Subscribe();
        }
        
        public void AddObject1()
        {
            var obj1 = new Object1("Added Object 1");
            ListTypesObject1.Add(obj1);
        }
        public void AddObject2()
        {
            var obj2 = new Object2("Added Object 2");
            ListTypesObject2.Add(obj2);
        }
        public void AddObject3()
        {
            if (ListTypesObject3 == null)
                return;
            var obj3 = new Object3("Added Object 3");
            ListTypesObject3.Add(obj3);
        }

        public void DeleteObject1()
        {
            if(ListTypesObject1 != null && ListTypesObject1.Count > 0)
                ListTypesObject1.RemoveAt(0);
        }
        public void DeleteObject2()
        {
            if (ListTypesObject2 != null && ListTypesObject2.Count > 0)
                ListTypesObject2.RemoveAt(0);
        }
        public void DeleteObject3()
        {
            if (ListTypesObject3 != null && ListTypesObject3.Count > 0)
                ListTypesObject3.RemoveAt(0);
        }
        
        public void DeleteObjectClear()
        {
            if (ListTypesObject3 == null)
                return;
            ListTypesObject3.Clear();
            ListTypesObject3 = null;
            ListTypesObject3 = new ObservableCollection<ViewModelBase>()
            {
                new Object3("Added object 3 from new list 3"),
            };
            SeveralListTypes3 = ListTypesObject3.ToObservableChangeSet();
            InBoth = InBoth.Or(SeveralListTypes3);
            // TODO : the collection we want to remove is still binded, the new one is not

        }

        public void DeleteObject3List()
        {
            if (ListTypesObject3 == null)
                return;
            ListTypesObject3.Clear();
            ListTypesObject3 = null;
            // TODO : remove the Object3List from DynamicData
        }

        public void CreateObject3List()
        {
            if (ListTypesObject3 != null)
                return;
            ListTypesObject3 = new ObservableCollection<ViewModelBase>()
            {
                new Object3("Added object 3 from new list 3"),
            };
            SeveralListTypes3 = ListTypesObject3.ToObservableChangeSet();
            InBoth = InBoth.Or(SeveralListTypes3);
            // TODO : the collection we want to remove is still binded, the new one is not

        }

    }
    
}