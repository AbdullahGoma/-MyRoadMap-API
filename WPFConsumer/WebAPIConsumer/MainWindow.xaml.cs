using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace WebAPIConsumer
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

        // Server Must be running
        private async void DepartmentListItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Department department = (Department)DepartmentListItem.SelectedItem;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // Get Data from service provider "URL" http://localhost:22686/api/Department
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:22686/api/Department");
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                List<Department> departments =
                    JsonSerializer.Deserialize<List<Department>>(message) ?? new List<Department>();
                DepartmentListItem.ItemsSource = departments;
            }
            else
            {
                MessageBox.Show("Not Found");
            }
        }
    }
}
