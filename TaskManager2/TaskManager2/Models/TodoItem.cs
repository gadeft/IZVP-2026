using System.ComponentModel;

namespace TaskManager2.Models;

public class TodoItem : INotifyPropertyChanged
{
    private bool _isCompleted;

    public string Title { get; set; }

    public bool IsCompleted
    {
        get => _isCompleted;
        set
        {
            if (_isCompleted != value)
            {
                _isCompleted = value;

                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(IsCompleted))
                );
            }
        }
    }

    public TodoItem(string title)
    {
        Title = title;
        IsCompleted = false;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
