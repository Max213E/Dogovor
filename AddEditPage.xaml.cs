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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private contracts _currentСontracts = new contracts();

        public AddEditPage(contracts selectedMaterial)
        {
            InitializeComponent();
            if(selectedMaterial!=null)
                _currentСontracts = selectedMaterial;

            DataContext = _currentСontracts;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder erros = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentСontracts.Title))
                erros.AppendLine("Укажите навазние договора");
            if (string.IsNullOrWhiteSpace(_currentСontracts.Type))
                erros.AppendLine("Укажите вид материала");
            if (string.IsNullOrWhiteSpace(_currentСontracts.Price.ToString()))
                erros.AppendLine("Укажите цену");

            if(erros.Length > 0)
            {
                MessageBox.Show(erros.ToString());
                return;
            }

            if (_currentСontracts.id == 0)
            {
                ContractsBaseEntities.GetContext().contracts.Add(_currentСontracts);
            }

            try
            {
                ContractsBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }
    }
}
