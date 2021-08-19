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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Аквапарк _currentAqupark = new Аквапарк();
        public AddEditPage(Аквапарк selectedAquapark)
        {
            InitializeComponent();

            if (selectedAquapark != null)
                _currentAqupark = selectedAquapark;

            DataContext = _currentAqupark;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentAqupark.Название))
                errors.AppendLine("Укажите называние аквапарка");
            if (_currentAqupark.Страна == null)
                errors.AppendLine("Укажите страну");
            if (string.IsNullOrWhiteSpace(_currentAqupark.Город))
                errors.AppendLine("Укажите называние города");
            if (string.IsNullOrWhiteSpace(_currentAqupark.Улица))
                errors.AppendLine("Укажите называние улицы");
            if (_currentAqupark.Дом == 0 || _currentAqupark.Дом < 0)
                errors.AppendLine("Дом не может иметь значение меньше или равное нулю");
            if (_currentAqupark.id_абонимента > 10 || _currentAqupark.id_абонимента <= 0)
                errors.AppendLine("Укажите верный абонемент");
            if(_currentAqupark.id_места > 10 || _currentAqupark.id_места <= 0)
                errors.AppendLine("Укажите верное место работы");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentAqupark.id_аквапарка == 0)
                AquaparkEntities2.GetContext().Аквапарк.Add(_currentAqupark);


            try
            {
                AquaparkEntities2.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
