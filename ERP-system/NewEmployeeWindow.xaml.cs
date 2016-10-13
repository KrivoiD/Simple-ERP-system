using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ERPsystem
{
    /// <summary>
    /// Interaction logic for NewEmployeeWindow.xaml
    /// </summary>
    public partial class NewEmployeeWindow : Window
    {
        public NewEmployeeWindow()
        {
            InitializeComponent();
        }

        private void btnPhotoView_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dial = new OpenFileDialog()
            {
                CheckFileExists = true,
                Filter = "JPEG|*.jpg|PNG|*.png|BMP|*.bmp",
                FilterIndex = 0,
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                Title = "Загрузка из файла базы данных лаборатории",
                Multiselect = false,
                AddExtension = true
            };
            if (dial.ShowDialog().Value == true)
                tbPhotoPath.Text = dial.FileName; 
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (DataBase.AddNewEmployee(tbPhotoPath.Text))
            {
                this.Close();
                DataBase.NewEmployee = new Employee();
            }
            else
                MessageBox.Show("Возможно пользователь существует или неправильно задан файл фотографии.",
                    "Не удалось создать запись сотрудника", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnDeny_Click(object sender, RoutedEventArgs e)
        {
            DataBase.NewEmployee = new Employee();
            this.Close();
        }
    }
}
