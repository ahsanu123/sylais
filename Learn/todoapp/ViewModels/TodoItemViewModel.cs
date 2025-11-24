using CommunityToolkit.Mvvm.ComponentModel;
using TodoApp.Models;

namespace TodoApp.ViewModels;

public partial class TodoItemViewModel : ObservableObject
{
    [ObservableProperty]
    private string? _content;
    
    [ObservableProperty]
    private bool _isChecked;
    
    public TodoItemViewModel(){}

    public TodoItemViewModel(TodoItem todoItem)
    {
        IsChecked = todoItem.IsChecked;
        Content = todoItem.Content;
    }

    public TodoItem GetTodoItem() => new TodoItem()
    {
        IsChecked = IsChecked, Content = Content
    };
}