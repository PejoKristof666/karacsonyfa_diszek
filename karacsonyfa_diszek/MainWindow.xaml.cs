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
using System.Net.Http;
using Newtonsoft.Json;

namespace karacsonyfa_diszek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        disz diszs = new disz() { name = "Kolbász", price = 2000, stock = 50000 };
        int sumNum = 0;
        Dictionary<int, int> soldItems = new Dictionary<int, int>();

        List<disz> allDisz = new List<disz>();
        public MainWindow()
        {
            InitializeComponent();
            Load();
            //Test();
        }
        void Test()
        {
            
            createStoreItem(diszs);
            
        }

        async void Load()
        {
            HttpClient client = new HttpClient();
            try
            {
                string url = "http://127.1.1.1:3000/disz";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseMessage = await response.Content.ReadAsStringAsync();
                allDisz = JsonConvert.DeserializeObject<List<disz>>(responseMessage);
                foreach (disz item in allDisz)
                {
                    createStoreItem(item);
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }

        void createStoreItem(disz oneDisz)
        {
            Border oneBorder = new Border();
            oneBorder.Background = new SolidColorBrush(Colors.Azure);
            oneBorder.CornerRadius = new CornerRadius(15);
            oneBorder.Padding = new Thickness(10);
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
            Grid count = new Grid();
            Button BuyButton = new Button();
            oneGrid.Children.Add(name);
            oneGrid.Children.Add(price);
            oneGrid.Children.Add(count);
            oneGrid.Children.Add(BuyButton);

            Grid.SetRow(price, 1);
            Grid.SetRow(count, 2);
            Grid.SetRow(BuyButton, 3);

            TextBlock countText = new TextBlock();
            TextBox countBox = new TextBox();
            count.Children.Add(countText);
            count.Children.Add(countBox);

            ColumnDefinition gridOne = new ColumnDefinition();
            ColumnDefinition gridTwo = new ColumnDefinition();

            count.ColumnDefinitions.Add(gridOne);
            count.ColumnDefinitions.Add(gridTwo);

            Grid.SetColumn(countBox, 1);

            name.Text = "Dísz neve: " + oneDisz.name;
            price.Text = "Ár: " + oneDisz.price;
            countText.Text = "Darabszám: ";
            countBox.Text = "1";
            BuyButton.Content = "Vásárlás";

            BuyButton.Click += (s, e) =>
            {
                if (soldItems.ContainsKey(oneDisz.id))
                {
                    MessageBox.Show("Már megvettem");
                }
                else
                {
                    soldItems.Add(oneDisz.id, int.Parse(countBox.Text));
                    if (int.Parse(countBox.Text) <= oneDisz.stock)
                    {
                        createCartItem(oneDisz, countBox.Text);
                        sumNum += oneDisz.price * int.Parse(countBox.Text);
                        UpdateSum();
                    }
                    else
                    {
                        MessageBox.Show("Nincs elegendő készlet");
                    }
                }
                
            };

            name.TextAlignment = TextAlignment.Center;
            price.TextAlignment = TextAlignment.Center;
            countText.TextAlignment = TextAlignment.Center;
            countBox.TextAlignment = TextAlignment.Center;
            countBox.Margin = new Thickness(5, -12, 5, 5);
            countBox.Height = 20;
        }

        void UpdateSum()
        {
            sum.Text = $"Végösszeg: {sumNum} $";
        }

        void createCartItem(disz oneDisz, string num)
        {
            Border oneBorder = new Border();
            oneBorder.Background = new SolidColorBrush(Colors.Azure);
            oneBorder.CornerRadius = new CornerRadius(15);
            oneBorder.Padding = new Thickness(10);
            Cart.Children.Add(oneBorder);
            Grid oneGrid = new Grid();

            oneBorder.Child = oneGrid;

            RowDefinition firstRow = new RowDefinition();
            RowDefinition secondRow = new RowDefinition();
            RowDefinition thirdRow = new RowDefinition();
            RowDefinition fourthRow = new RowDefinition();
            RowDefinition fifthRow = new RowDefinition();
            oneGrid.RowDefinitions.Add(firstRow);
            oneGrid.RowDefinitions.Add(secondRow);
            oneGrid.RowDefinitions.Add(thirdRow);
            oneGrid.RowDefinitions.Add(fourthRow);
            oneGrid.RowDefinitions.Add(fifthRow);

            TextBlock name = new TextBlock();
            TextBlock price = new TextBlock();
            Grid count = new Grid();
            Button moreButton = new Button();
            Button lessButton = new Button();
            oneGrid.Children.Add(name);
            oneGrid.Children.Add(price);
            oneGrid.Children.Add(count);
            oneGrid.Children.Add(moreButton);
            oneGrid.Children.Add(lessButton);

            Grid.SetRow(price, 1);
            Grid.SetRow(count, 2);
            Grid.SetRow(moreButton, 3);
            Grid.SetRow(lessButton, 4);

            TextBlock countText = new TextBlock();
            TextBox countBox = new TextBox();
            count.Children.Add(countText);
            count.Children.Add(countBox);

            ColumnDefinition gridOne = new ColumnDefinition();
            ColumnDefinition gridTwo = new ColumnDefinition();

            count.ColumnDefinitions.Add(gridOne);
            count.ColumnDefinitions.Add(gridTwo);

            Grid.SetColumn(countBox, 1);

            name.Text = "Dísz neve: " + oneDisz.name;
            price.Text = "Ár: " + oneDisz.price;
            countText.Text = "Darabszám: ";
            countBox.Text = num;
            moreButton.Content = "Több";
            moreButton.Padding = new Thickness(5);
            lessButton.Content = "Kevesebb";
            lessButton.Padding = new Thickness(5);

            moreButton.Click += (s, e) =>
            {
                if (int.Parse(countBox.Text) <=oneDisz.stock)
                {
                    soldItems[oneDisz.id]++;
                    int currentValue = int.Parse(countBox.Text);
                    currentValue++;
                    countBox.Text = currentValue.ToString();
                    sumNum += oneDisz.price;
                    UpdateSum();
                }
            };
            lessButton.Click += (s, e) =>
            {
                int currentValue = int.Parse(countBox.Text);
                currentValue--;
                sumNum -= oneDisz.price;
                UpdateSum();
                soldItems[oneDisz.id]++;
                if (currentValue < 1)
                {
                    soldItems.Remove(oneDisz.id);
                    Cart.Children.Remove(oneBorder);
                }
                countBox.Text = currentValue.ToString();
                
            };

            name.TextAlignment = TextAlignment.Center;
            price.TextAlignment = TextAlignment.Center;
            countText.TextAlignment = TextAlignment.Center;
            countBox.TextAlignment = TextAlignment.Center;
            countBox.Margin = new Thickness(5, -12, 5, 5);
            countBox.Height = 20;
        }
        void Buy(Object s, EventArgs e)
        {
            
        }
    }
}
