using System.Windows;

namespace HelpAssistantGui
{
  /// <summary>
  /// Interaction logic for WindowInfoWithCheckbox.xaml
  /// </summary>
  public partial class WindowInfoWithCheckbox: Window
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="WindowInfoWithCheckbox"/> class.
    /// </summary>
    public WindowInfoWithCheckbox()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Handles the Click event of the buttonOk control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
    private void buttonOk_Click( object sender, RoutedEventArgs e )
    {
      if ( checkBoxDoNotWantToShowAgain.IsChecked == true )
      {
        Properties.Settings.Default.doNotWantToSeeTheMessageAgain = true;
        Properties.Settings.Default.Save();
        this.Close();
      }
      else
      {
        this.Close();
      }
    }

    /// <summary>
    /// Handles the Click event of the buttonCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
    private void buttonCancel_Click( object sender, RoutedEventArgs e )
    {
      this.Close();
    }

  }
}
