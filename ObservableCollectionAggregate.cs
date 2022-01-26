using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using pocbindingdmo.ViewModels;

namespace pocbindingdmo
{
    public class ObservableCollectionAggregate : ObservableCollection<ViewModelBase>
    {
        private ObservableCollection<Object1> _subCollection1;
        private ObservableCollection<Object2> _subCollection2;
        private ObservableCollection<Object3> _subCollection3;


        public ObservableCollectionAggregate()
        {
            _subCollection1 = null;
            _subCollection2 = null;
            _subCollection3 = null;
        }

        public void UnassignCollectionObject1()
        {
            if (_subCollection1 != null)
            {
                RemoveItems(_subCollection1);
                _subCollection1.CollectionChanged -= OnSubCollectionChanged;
                _subCollection1 = null;
            }
        }

        public void AssignCollectionObject1(ObservableCollection<Object1> collection)
        {
            if (_subCollection1 != null)
            {
                UnassignCollectionObject1();
            }

            _subCollection1 = collection;
            AddItems(_subCollection1);
            _subCollection1.CollectionChanged += OnSubCollectionChanged;

        }

        public void UnassignCollectionObject2()
        {
            if (_subCollection2 != null)
            {
                RemoveItems(_subCollection2);
                _subCollection2.CollectionChanged -= OnSubCollectionChanged;
                _subCollection2 = null;
            }
        }

        public void AssignCollectionObject2(ObservableCollection<Object2> collection)
        {
            if (_subCollection2 != null)
            {
                UnassignCollectionObject2();
            }

            _subCollection2 = collection;
            AddItems(_subCollection2);
            _subCollection2.CollectionChanged += OnSubCollectionChanged;

        }

        public void UnassignCollectionObject3()
        {
            if (_subCollection3 != null)
            {
                RemoveItems(_subCollection3);
                _subCollection3.CollectionChanged -= OnSubCollectionChanged;
                _subCollection3 = null;
            }
        }

        public void AssignCollectionObject3(ObservableCollection<Object3> collection)
        {
            if (_subCollection3 != null)
            {
                UnassignCollectionObject3();
            }

            _subCollection3 = collection;
            AddItems(_subCollection3);
            _subCollection3.CollectionChanged += OnSubCollectionChanged;

        }

        private void AddItems(IEnumerable<ViewModelBase> items)
        {
            foreach (ViewModelBase me in items)
                Add(me);
        }

        private void RemoveItems(IEnumerable<ViewModelBase> items)
        {
            foreach (ViewModelBase me in items)
                Remove(me);
        }

        private void OnSubCollectionChanged(object source, NotifyCollectionChangedEventArgs args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddItems(args.NewItems.Cast<ViewModelBase>());
                    break;

                case NotifyCollectionChangedAction.Remove:
                    RemoveItems(args.OldItems.Cast<ViewModelBase>());
                    break;
                case NotifyCollectionChangedAction.Replace:
                    RemoveItems(args.OldItems.Cast<ViewModelBase>());
                    AddItems(args.NewItems.Cast<ViewModelBase>());
                    break;
                case NotifyCollectionChangedAction.Move:
                    throw new NotImplementedException();
                case NotifyCollectionChangedAction.Reset:
                    if (source is ObservableCollection<Object1>)
                    {
                        RemoveItems(this.Where(c => c is Object3).ToList());
                    }
                    if (source is ObservableCollection<Object2>)
                    {
                        RemoveItems(this.Where(c => c is Object3).ToList());
                    }
                    if (source is ObservableCollection<Object3>)
                    {
                        RemoveItems(this.Where(c => c is Object3).ToList());
                    }
                    break;
            }
        }
    }
}