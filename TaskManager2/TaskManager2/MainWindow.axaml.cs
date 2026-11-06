using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

using Avalonia.Controls;
using Avalonia.Interactivity;

using TaskManager2.Models;
using TaskManager2.Controls;

namespace TaskManager2;

public partial class MainWindow : Window
{
    public ObservableCollection<TodoItem> Tasks { get; } = new();

    public MainWindow()
    {
        InitializeComponent();

        DataContext = this;

        Tasks.CollectionChanged += Tasks_CollectionChanged;

        UpdateCounter();
    }

    // Додавання завдання
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

        Tasks.Add(new TodoItem(title));

        TaskInput.Text = "";

        UpdateCounter();
    }

    // Зміна статусу
    private void TaskCard_StatusChanged(
        object? sender,
        EventArgs e)
    {
        UpdateCounter();
    }

    // Видалення завдання
    private void DeleteTask_Click(
        object? sender,
        EventArgs e)
    {
        if (sender is TaskActionButton button &&
            button.Parameter is TodoItem task)
        {
            Tasks.Remove(task);
            UpdateCounter();
        }
    }

    // Відстеження змін колекції
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

    // Відстеження зміни властивостей
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
        int count = Tasks.Count(task => !task.IsCompleted);

        CounterText.Text =
            $"Невиконаних завдань: {count}";
    }
}