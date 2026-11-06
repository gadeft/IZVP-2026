using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using TaskManager2.Models;

namespace TaskManager2.Controls;

public partial class TaskCard : UserControl
{
    public static readonly StyledProperty<TodoItem?> TaskProperty =
        AvaloniaProperty.Register<TaskCard, TodoItem?>(
            nameof(Task));

    public TodoItem? Task
    {
        get => GetValue(TaskProperty);
        set => SetValue(TaskProperty, value);
    }

    public event EventHandler? StatusChanged;

    static TaskCard()
    {
        TaskProperty.Changed.AddClassHandler<TaskCard>(
            (control, _) => control.OnTaskChanged());
    }

    public TaskCard()
    {
        InitializeComponent();
    }

    private void OnTaskChanged()
    {
        if (Task == null)
            return;

        Task.PropertyChanged += Task_PropertyChanged;

        TitleLabel.Text = Task.Title;

        UpdateStatus();
    }

    private void Task_PropertyChanged(
        object? sender,
        PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TodoItem.IsCompleted))
        {
            UpdateStatus();
        }
    }

    private void UpdateStatus()
    {
        if (Task == null)
            return;

        StatusLabel.Text = Task.IsCompleted
            ? "Виконано"
            : "Не виконано";

        StatusLabel.Foreground = Task.IsCompleted
            ? Brushes.Green
            : Brushes.OrangeRed;
    }

    private void ChangeStatus_Click(
        object? sender,
        EventArgs e)
    {
        if (Task == null)
            return;

        Task.IsCompleted = !Task.IsCompleted;

        StatusChanged?.Invoke(this, EventArgs.Empty);
    }
}