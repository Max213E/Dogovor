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
using Excel = Microsoft.Office.Interop.Excel;

namespace StuffApp
{
    /// <summary>
    /// Логика взаимодействия для StuffPage.xaml
    /// </summary>
    public partial class StuffPage : Page
    {
        private ContractsBaseEntities _context = new ContractsBaseEntities();
        public StuffPage()
        {
            InitializeComponent();
            // DGridStuff.ItemsSource = StuffBaseEntities.GetContext().materials.ToList();
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

            if (MessageBox.Show("Удалить элементы из базы данных?", "Внимание", MessageBoxButton.YesNo,MessageBoxImage.Question)==MessageBoxResult.Yes)
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
            if(Visibility == Visibility.Visible)
            {
                ContractsBaseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridStuff.ItemsSource = ContractsBaseEntities.GetContext().contracts.ToList();
            }
           
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            var allContracts = _context.contracts.ToList();
            var db = new ContractsBaseEntities();
            var application = new Excel.Application();

            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);

            int startRowIndex = 1;         
            //for (int i = 0; i < allContracts.Count(); i++)
            // {
            Excel.Worksheet worksheet = application.Worksheets.Item[1];
                worksheet.Cells[1].ColumnWidth = 25;
                worksheet.Cells[4].ColumnWidth = 15;
                worksheet.Cells[5].ColumnWidth = 15;
                worksheet.Cells[6].ColumnWidth = 15;
                worksheet.Cells[7].ColumnWidth = 25;

                worksheet.Cells[1][1] = "Название";
                worksheet.Cells[2][1] = "Номер";
                worksheet.Cells[3][1] = "Цена";
                worksheet.Cells[4][1] = "Срок действия";
                worksheet.Cells[5][1] = "Организация";
                worksheet.Cells[6][1] = "Ответственный";
                worksheet.Cells[7][1] = "Дата";

                worksheet.Cells[1][2] = "Договор купли продажи";
                worksheet.Cells[2][2] = "1";
                worksheet.Cells[3][2] = "2000";
                worksheet.Cells[4][2] = "11.05.2023";
                worksheet.Cells[5][2] = "Воркута";
                worksheet.Cells[6][2] = "Кушнир";
                worksheet.Cells[7][2] = "11.05.2023-12.05.2024";

                worksheet.Cells[1][3] = "Договор присоединения";
                worksheet.Cells[2][3] = "2";
                worksheet.Cells[3][3] = "10000.00";
                worksheet.Cells[4][3] = "28.10.2023";
                worksheet.Cells[5][3] = "Сити";
                worksheet.Cells[6][3] = "Иванов";
                worksheet.Cells[7][3] = "28.10.2023-17.05.2025";

                worksheet.Cells[1][4] = "Предворительный договор";
                worksheet.Cells[2][4] = "3";
                worksheet.Cells[3][4] = "0.00";
                worksheet.Cells[4][4] = "11.05.2023";
                worksheet.Cells[5][4] = "Путь";
                worksheet.Cells[6][4] = "Петров";
                worksheet.Cells[7][4] = "11.05.2023-12.06.2023";


            startRowIndex++;

           // }

            application.Visible = true;
        }
    }
}
