using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using LibraryManager.Controller;
using LibraryManager.Data;
using LibraryManager.Model;
using LibraryManager.Shared;

namespace LibraryManager;

public partial class MainWindow : Window
{
    private readonly LibraryController _controller;

    public ObservableCollection<Book> Books { get; set; }


    public MainWindow()
    {
        _controller = new LibraryController();
        _controller.LogCreated += this.Log;
        Books = new ObservableCollection<Book>(BookRepository.GetAll());
        Closed += OnClosed;
        InitializeComponent();
        DataContext = this;
    }

    private void Log(object? sender, EventArgs eventArgs)
    {
        var lastLog = LogRepository.GetAll().Last();

        Application.Current.Dispatcher.Invoke(() =>
        {
            Books.Clear();
            foreach (var book in BookRepository.GetAll())
            {
                Books.Add(book);
            }
            ConsoleTextBox.AppendText($"[{lastLog.DateTime:hh:mm:ss}]: {lastLog.Title} - {lastLog.Message}\n");
        });
    }

    private void OnClosed(object? sender, EventArgs e)
    {
        JsonUtils.SaveLogs(LogRepository.GetAll());
        _controller.Stop();
    }

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        _controller.Start();
        StartButton.IsEnabled = false;
    }
}
