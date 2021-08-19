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

namespace Приложение
{
    /// <summary>
    /// Логика взаимодействия для prekol.xaml
    /// </summary>
    public partial class prekol : Page
    {
        public prekol()
        {
            InitializeComponent();
            //DGridAquaPark.ItemsSource = AquaparkEntities2.GetContext().Аквапарк.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            klsik.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Аквапарк));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            klsik.MainFrame.Navigate(new AddEditPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var aquaparksForRemoving = DGridAquaPark.SelectedItems.Cast<Аквапарк>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {aquaparksForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AquaparkEntities2.GetContext().Аквапарк.RemoveRange(aquaparksForRemoving);
                    AquaparkEntities2.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_VisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AquaparkEntities2.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridAquaPark.ItemsSource = AquaparkEntities2.GetContext().Аквапарк.ToList();
            }
        }
    }
}
