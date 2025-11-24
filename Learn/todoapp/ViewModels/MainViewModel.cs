using System.Collections.ObjectModel;
using System.Runtime.InteropServices.JavaScript;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TodoApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
       public MainViewModel()
    {
        if (Design.IsDesignMode)
            ToDoItems = new ObservableCollection<TodoItemViewModel>
            {
                new () { Content = "Hello" },
                new () { Content = "Avalonia", IsChecked = true}
            };
    }
    
    public ObservableCollection<TodoItemViewModel> ToDoItems { get; } = new ();

    [RelayCommand (CanExecute = nameof(CanAddItem))]
    private void AddItem()
    {
        ToDoItems.Add(new TodoItemViewModel {Content = NewItemContent});
        NewItemContent = null;
    }

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(AddItemCommand))] 
    private string? _newItemContent;

    private bool CanAddItem() => !System.String.IsNullOrEmpty(NewItemContent);
    
    [RelayCommand]
    private void RemoveItem(TodoItemViewModel item)
    {
        ToDoItems.Remove(item);
    }
}