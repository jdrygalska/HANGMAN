using System.Windows;
using System.Windows.Controls;

public class InputDialog : Window
{
    public string Answer => inputTextBox.Text;

    private TextBox inputTextBox;

    public InputDialog(string title, string prompt)
    {
        Title = title;
        Width = 300;
        Height = 150;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;

        StackPanel stackPanel = new StackPanel();

        TextBlock promptTextBlock = new TextBlock
        {
            Text = prompt,
            Margin = new Thickness(5),
        };
        stackPanel.Children.Add(promptTextBlock);

        inputTextBox = new TextBox
        {
            Margin = new Thickness(5),
        };
        stackPanel.Children.Add(inputTextBox);

        Button okButton = new Button
        {
            Content = "OK",
            Width = 80,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(5),
        };
        okButton.Click += (sender, e) => Close();
        stackPanel.Children.Add(okButton);

        Content = stackPanel;
    }
}
