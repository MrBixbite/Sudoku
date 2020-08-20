using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        private Button _selectedButton; // The currently selected button
        private int[,] _cellValues = new int[9, 9];

        #endregion

        #region Constructors

        /// <summary>
        /// MainWindow constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitializeButtons();
        }

        #endregion

        /// <summary>
        /// Create, set and display buttons to board.
        /// </summary>
        private void InitializeButtons()
        {
            for (var row = 0; row < 9; row++)
            {
                for (var column = 0; column < 9; column++)
                {
                    var button = new Button();

                    StyleButton(button, row, column);
                    Container.Children.Add(button);
                }
            }
        }

        /// <summary>
        /// Sets the styling for the button.
        /// </summary>
        /// <param name="button">The button object</param>
        /// <param name="row">The row of the button</param>
        /// <param name="column">The column of the object</param>
        private void StyleButton(Button button, int row, int column)
        {
            button.SetValue(Grid.ColumnProperty, column);
            button.SetValue(Grid.RowProperty, row);
            button.Click += new RoutedEventHandler(Button_Click);
            button.PreviewKeyDown += OnKeyDownHandler;
            button.Background = Brushes.White;
            button.FontSize = 50;

            SetButtonBorders(button, row, column);
        }

        /// <summary>
        /// Sets the borders for the button to divide the board into quadrants.
        /// </summary>
        /// <param name="button">The button object</param>
        /// <param name="row">The row of the button</param>
        /// <param name="column">The column of the object</param>
        private void SetButtonBorders(Button button, int row, int column)
        {
            if ((row + 1) % 3 == 0 && (column + 1) % 3 == 0)
                button.BorderThickness = new Thickness(0.5, 0.5, 2, 2);
            else if ((row + 1) % 3 == 0)
                button.BorderThickness = new Thickness(0.5, 0.5, 0.5, 2);
            else if ((column + 1) % 3 == 0)
                button.BorderThickness = new Thickness(0.5, 0.5, 2, 0.5);
            else
                button.BorderThickness = new Thickness(0.5);
        }

        #region Event Handlers

        /// <summary>
        /// Listens for button click and stores the selected button.
        /// </summary>
        /// <param name="sender">The object</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _selectedButton = (Button)sender;
        }

        /// <summary>
        /// Listens for a key press and sets the selected button's
        /// content to a string conversion of that key.
        /// </summary>
        /// <param name="sender">The object</param>
        /// <param name="e">The KeyEventArgs</param>
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            // Checks if the button has been selected. If not, return.
            if (_selectedButton == null) return;

            // Converts the key code to a string.
            var converter = new KeyConverter();
            var keyAsString = converter.ConvertToString(e.Key);
            int keyAsInteger;

            // Checks if the string is a number, not including 0. If so, set the button content to that string.
            if (!int.TryParse(keyAsString, out keyAsInteger) || keyAsInteger == 0) return;

            _cellValues[Grid.GetColumn(_selectedButton), Grid.GetRow(_selectedButton)] = keyAsInteger;
            _selectedButton.Content = keyAsString;
            _selectedButton = null;
        }

        #endregion
    }
}
