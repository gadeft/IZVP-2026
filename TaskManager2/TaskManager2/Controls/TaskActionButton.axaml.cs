using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TaskManager2.Controls;

public partial class TaskActionButton : UserControl
{
    public static readonly StyledProperty<string> ButtonTextProperty =
        AvaloniaProperty.Register<TaskActionButton, string>(
            nameof(ButtonText), "Дія");

    public string ButtonText
    {
        get => GetValue(ButtonTextProperty);
        set => SetValue(ButtonTextProperty, value);
    }

    public static readonly StyledProperty<object?> ParameterProperty =
        AvaloniaProperty.Register<TaskActionButton, object?>(
            nameof(Parameter));

    public object? Parameter
    {
        get => GetValue(ParameterProperty);
        set => SetValue(ParameterProperty, value);
    }

    public event EventHandler? Clicked;

    public TaskActionButton()
    {
        InitializeComponent();
        ActionButton.Content = ButtonText;
    }

    protected override void OnPropertyChanged(
        AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == ButtonTextProperty &&
            ActionButton != null)
        {
            ActionButton.Content = ButtonText;
        }
    }

    private void ActionButton_Click(
        object? sender,
        RoutedEventArgs e)
    {
        Clicked?.Invoke(this, EventArgs.Empty);
    }
}