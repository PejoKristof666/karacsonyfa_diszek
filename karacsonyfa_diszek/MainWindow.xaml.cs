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

namespace karacsonyfa_diszek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void createStoreItem(disz oneDisz)
        {
            Border oneBorder = new Border();
            Store.Children.Add(oneBorder);
            Grid oneGrid = new Grid();
            oneBorder.Child = oneGrid;

            RowDefinition firstRow = new RowDefinition();
            RowDefinition secondRow = new RowDefinition();
            RowDefinition thirdRow = new RowDefinition();
            RowDefinition fourthRow = new RowDefinition();
            oneGrid.RowDefinitions.Add(firstRow);
            oneGrid.RowDefinitions.Add(secondRow);
            oneGrid.RowDefinitions.Add(thirdRow);
            oneGrid.RowDefinitions.Add(fourthRow);
            TextBlock name = new TextBlock();
            TextBlock price = new TextBlock();
            TextBlock count = new TextBlock();
            oneGrid.Children.Add(name);
            oneGrid.Children.Add(price);
            oneGrid.Children.Add(count);
            Button BuyButton = new Button();
            oneGrid.Children.Add(BuyButton);
        }
    }
}
