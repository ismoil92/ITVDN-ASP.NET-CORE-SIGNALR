using Microsoft.AspNetCore.SignalR.Client;
using System.Windows;


namespace WpfClientSignalR;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    #region FIELD
    private readonly HubConnection _hubConnection;
    #endregion
    #region CONSTRUCTOR
    public MainWindow()
    {
        InitializeComponent();
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7204/chat")
            .Build();


        // methods EnterToGroup and ExitFromGroup in class ChatHub
        _hubConnection.On<string>("Notify", message =>
        {
            Dispatcher.Invoke(() =>
            {
                chatListBox.Items.Add(message);
            });
        });

        //method SendMessage in class ChatHub
        _hubConnection.On<string, string>("Receive", (message, username) =>
        {
            Dispatcher.Invoke(() =>
            {
                chatListBox.Items.Add(username + ": " + message);
            });
        });


        //method SendExcept in class ChatHub
        _hubConnection.On<string>("ReceiveMessage", message =>
        {
            Dispatcher.Invoke(() =>
            {
                chatListBox.Items.Add(message);
            });
        });
    }
    #endregion

    #region METHODS
    /// <summary>
    /// Метод, для запуска чат для сообщении signalR
    /// </summary>
    /// <param name="sender">Источник события: объект, к которому привязан данный обработчик 
    /// (требует приведения типов, например, (Button)sender).</param>
    /// <param name="e"> Аргументы маршрутизируемого события, 
    /// содержащие дополнительную информацию (например, исходный элемент e.OriginalSource, 
    /// состояниеhandled и т.д.).</param>
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            _hubConnection.StartAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Connection Error:{ex.Message}");
        }
    }


    /// <summary>
    /// Метод, для входа в чат группу нажатием кнопкой
    /// </summary>
    /// <param name="sender">Источник события: объект, к которому привязан данный обработчик 
    /// (требует приведения типов, например, (Button)sender).</param>
    /// <param name="e"> Аргументы маршрутизируемого события, 
    /// содержащие дополнительную информацию (например, исходный элемент e.OriginalSource, 
    /// состояниеhandled и т.д.).</param>
    private void enterGroupBtn_Click(object sender, RoutedEventArgs e)
    {
        string _userName = userName.Text;
        string _groupName = groupName.Text;

        _hubConnection.InvokeAsync("EnterToGroup", _userName, _groupName);
        sendMessageBtn.IsEnabled = true;
    }


    /// <summary>
    /// Метод, для отправки сообщений в общий чат нажатием кнопкой
    /// </summary>
    /// <param name="sender">Источник события: объект, к которому привязан данный обработчик 
    /// (требует приведения типов, например, (Button)sender).</param>
    /// <param name="e"> Аргументы маршрутизируемого события, 
    /// содержащие дополнительную информацию (например, исходный элемент e.OriginalSource, 
    /// состояниеhandled и т.д.).</param>
    private void sendMessageBtn_Click(object sender, RoutedEventArgs e)
    {
        string _message = messageTextBox.Text;
        string _userName = userName.Text;
        string _groupName = groupName.Text;

        _hubConnection.InvokeAsync("SendMessage", _message, _userName, _groupName);
    }


    /// <summary>
    ///  Метод, для выхода из чат группы нажатием кнопкой
    /// </summary>
    /// <param name="sender">Источник события: объект, к которому привязан данный обработчик 
    /// (требует приведения типов, например, (Button)sender).</param>
    /// <param name="e"> Аргументы маршрутизируемого события, 
    /// содержащие дополнительную информацию (например, исходный элемент e.OriginalSource, 
    /// состояниеhandled и т.д.).</param>
    private void exitGroupBtn_Click(object sender, RoutedEventArgs e)
    {
        string _userName = userName.Text;
        string _groupName = groupName.Text;

        _hubConnection.InvokeAsync("ExitFromGroup", _userName, _groupName);
        sendMessageBtn.IsEnabled = false;
    }


    /// <summary>
    /// Метод, для оптравки сообщение отпределенным пользователем в чат группу нажатием кнопкой
    /// </summary>
    /// <param name="sender">Источник события: объект, к которому привязан данный обработчик 
    /// (требует приведения типов, например, (Button)sender).</param>
    /// <param name="e"> Аргументы маршрутизируемого события, 
    /// содержащие дополнительную информацию (например, исходный элемент e.OriginalSource, 
    /// состояниеhandled и т.д.).</param>
    private void sendExpButton_Click(object sender, RoutedEventArgs e)
    {
        string _message = expMessageTextBox.Text;
        string _groupName = expGroupNameTxTBox.Text;

        _hubConnection.InvokeAsync("SendExcept", _message, _groupName);
    }
}
#endregion