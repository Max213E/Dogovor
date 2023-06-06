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

namespace StuffApp
{
    /// <summary>
    /// Логика взаимодействия для PageForUser.xaml
    /// </summary>
    public partial class PageForUser : Page
    {
        public PageForUser()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as contracts));

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var stuffForRemoving = DGridStuff.SelectedItems.Cast<contracts>().ToList();

            if (MessageBox.Show("Удалить элементы из базы данных?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ContractsBaseEntities.GetContext().contracts.RemoveRange(stuffForRemoving);
                    ContractsBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGridStuff.ItemsSource = ContractsBaseEntities.GetContext().contracts.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ContractsBaseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridStuff.ItemsSource = ContractsBaseEntities.GetContext().contracts.ToList();
            }
        }
    }
}
    

