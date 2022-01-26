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
        private readonly ReadOnlyObservableCollection<ViewModelBase> _testBind;
        public ReadOnlyObservableCollection<ViewModelBase> TestBind => _testBind;
        
        public ObservableCollectionAggregate AggregatedCollection { get; set; }
        public IObservable<IChangeSet<ViewModelBase>> AggregatedChangeSetFull { get; set; }

        public ObservableCollection<Object1> ListTypesObject1 { get; set; } 
        public ObservableCollection<Object2> ListTypesObject2 { get; set; }
        public ObservableCollection<Object3> ListTypesObject3 { get; set; }


        private readonly ReadOnlyObservableCollection<ViewModelBase> _testBindTypes;
        public ReadOnlyObservableCollection<ViewModelBase> TestBindTypes => _testBindTypes;
        public MainWindowViewModel()
        {

            ListTypesObject1 = new ObservableCollection<Object1>()
            {
                new Object1(),
            };
            
            ListTypesObject2 = new ObservableCollection<Object2>()
            {
                new Object2(),
            };
            

            AggregatedCollection = new ObservableCollectionAggregate();

            AggregatedCollection.AssignCollectionObject1(ListTypesObject1);
            AggregatedCollection.AssignCollectionObject2(ListTypesObject2);


            AggregatedChangeSetFull = AggregatedCollection.ToObservableChangeSet();


            // Bind => output to Binded Property for xaml
            // Subscribe => to be notified when changes
            var t = AggregatedChangeSetFull
                .DisposeMany()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _testBindTypes)
                .Subscribe();

        }

        public void AddObject1()
        {
            if (ListTypesObject1 == null)
                return;
            var obj1 = new Object1("Added Object 1");
            ListTypesObject1.Add(obj1);
        }
        public void AddObject2()
        {
            if (ListTypesObject2 == null)
                return;
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

        public void DeleteObject3List()
        {
            if (ListTypesObject3 == null)
                return;
            ListTypesObject3.Clear();
            ListTypesObject3 = null;
            AggregatedCollection.UnassignCollectionObject3();
        }
        
        public void CreateObject3List()
        {
            if (ListTypesObject3 != null)
                return;
            ListTypesObject3 = new ObservableCollection<Object3>()
            {
                new Object3("Added object 3 from new list 3"),
            };
            AggregatedCollection.AssignCollectionObject3(ListTypesObject3);
        }


    }

}