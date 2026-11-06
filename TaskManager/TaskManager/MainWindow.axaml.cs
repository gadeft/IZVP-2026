using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using TaskManager.Models;

namespace TaskManager;

public partial class MainWindow : Window
{
    private readonly ObservableCollection<TodoItem> _tasks = new();

    public MainWindow()
    {
        InitializeComponent();

        TaskList.ItemsSource = _tasks;

        _tasks.CollectionChanged += Tasks_CollectionChanged;

        UpdateCounter();
    }

    // Додавання нового завдання
    private void AddButton_Click(
        object? sender,
        RoutedEventArgs e)
    {
        string title = TaskInput.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(title))
        {
            CounterText.Text = "Введіть назву завдання!";
            return;
        }

        _tasks.Add(new TodoItem(title));

        TaskInput.Text = "";

        UpdateCounter();
    }

    // Видалення вибраного завдання
    private void DeleteButton_Click(
        object? sender,
        RoutedEventArgs e)
    {
        if (TaskList.SelectedItem is TodoItem selectedTask)
        {
            _tasks.Remove(selectedTask);
            UpdateCounter();
        }
    }

    // Відстеження змін у списку
    private void Tasks_CollectionChanged(
        object? sender,
        NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (TodoItem item in e.NewItems)
            {
                item.PropertyChanged += Task_PropertyChanged;
            }
        }

        if (e.OldItems != null)
        {
            foreach (TodoItem item in e.OldItems)
            {
                item.PropertyChanged -= Task_PropertyChanged;
            }
        }

        UpdateCounter();
    }

    // Відстеження зміни стану завдання
    private void Task_PropertyChanged(
        object? sender,
        PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TodoItem.IsCompleted))
        {
            UpdateCounter();
        }
    }

    // Оновлення лічильника
    private void UpdateCounter()
    {
        int count = 0;

        foreach (var task in _tasks)
        {
            if (!task.IsCompleted)
            {
                count++;
            }
        }

        CounterText.Text =
            $"Невиконаних завдань: {count}";
    }
}