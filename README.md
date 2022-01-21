## Goal of this repository

Find a way to use a "WPF's CompositeCollection like" using AvaloniaUI, ReactiveUI & Dynamic Data.
We need to bind different ViewModels to a canvas then using `<DataTemplate DataType="{x:Type xxx}">` for the binding.

### Current situation

In a WPF application, you can agregate multiple collections of different types in a CompositeCollection.
example : https://github.com/microsoft/WPF-Samples/blob/main/Data%20Binding/CompositeCollections/MainWindow.xaml

In avaloniaUI, there are no implementation of CollectionComposite

I try to reproduce this behaviour using AvaloniaUI, ReactiveUI & Dynamic Data.

### Issues

- DynamicData can only agregate collections of same type. So in my test, I had to agregate collections of a common ancestor. 
- In dynamic data, the .Bind(out xxx) is done once, but in my case, some of the collections will be deleted, recreate or added after the original agregation


### What I dream about :)

Bind differents ObservableCollection of different types in an unique collection as ItemsSource of an ItemsControl

Example :


    ObservableCollection<ViewModelRectangle> Rectangles
    ObservableCollection<ViewModelCircle> Circles 
    ObservableCollection<ViewModelPolyline> Polylines 

All those collections can be null at the beginning then when I create an element I also create the collection if the collection is null.
Add or remove items in collections at any time
I also need to completly flush and delete a collection at any time.

All those changes should be reflected in the dynamic data for the binding.

### About the POC

In this POC, there are no Canvas but a grid with textboxes to simplify.
In the MainWindowViewModel there are TODOs 