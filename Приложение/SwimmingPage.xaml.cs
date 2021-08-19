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
    /// Логика взаимодействия для SwimmingPage.xaml
    /// </summary>
    public partial class SwimmingPage : Page
    {
        public SwimmingPage()
        {
            InitializeComponent();

            var allTypes = AquaparkEntities2.GetContext().Тип_аквапарка.ToList();
            allTypes.Insert(0, new Тип_аквапарка
            {
                Название = "Все типы"
            });
            ComboType.ItemsSource = allTypes;

            CheckPos.IsChecked = true;
            ComboType.SelectedIndex = 0;

            var currentAqupark = AquaparkEntities2.GetContext().Аквапарк.ToList();
            LViewSwimming.ItemsSource = currentAqupark;
        }

        private void UpdateSwimming()
        {
            var currentAqua = AquaparkEntities2.GetContext().Аквапарк.ToList();

            // ComboBox не работает

            //if (ComboType.SelectedIndex > 0)
               // currentAqua = currentAqua.Where(p => p.Название.Contains(ComboType.SelectedItem as Тип_аквапарка)).ToList();

            currentAqua = currentAqua.Where(p => p.Название.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if (CheckPos.IsChecked.Value)
                currentAqua = currentAqua.Where(p => (bool)p.Обслуживание_инвалидов).ToList();

            LViewSwimming.ItemsSource = currentAqua.OrderBy(p => p.Обслуживание_инвалидов).ToList();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSwimming();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSwimming();
        }

        private void CheckPos_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSwimming();
        }

        private void CheckPos_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSwimming();
        }
    }
}
