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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataBase.LoadFromFile("DataBase.db");
            InitializeComponent();
            
        }

        private void lstEmployees_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                EmployeerView();
        }
        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter as string != "SaveAs")
                DataBase.SaveToFile("DataBase.db");
            else
            {
                SaveFileDialog dial = new SaveFileDialog()
                {
                    CheckPathExists = true,
                    Filter = "DataBase of Laboratory (*.db)|*.db",
                    FilterIndex = 0,
                    InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    Title = "Сохранение базы данных лаборатории",
                    OverwritePrompt = true,
                    AddExtension = true
                };
                if (dial.ShowDialog().Value == true)
                    DataBase.SaveToFile(dial.FileName);

            }
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dial = new OpenFileDialog()
            {
                CheckFileExists = true,
                Filter = "DataBase of Laboratory (*.db)|*.db",
                FilterIndex = 0,
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                Title = "Загрузка из файла базы данных лаборатории",
                Multiselect = false,
                AddExtension = true
            };
            if (dial.ShowDialog().Value == true)
            {
                DataBase.LoadFromFile(dial.FileName);
                lstEmployees.ItemsSource = DataBase.Laboratory.Employees;
            }
        }


        private void EmployeerView_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EmployeerView();
        }

        private void NewEmployeer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NewEmployeeWindow newEmpl = new NewEmployeeWindow();
            newEmpl.ShowDialog();
        }

        private void RemoveEmployeer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Уволить выделенного в списке сотрудника.\nВы уверены?", "Уволнение сотрудника",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                DataBase.RemoveEmployee(lstEmployees.SelectedItem as Employee);
            }
        }

        private void AddRow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataGrid dg = new DataGrid();
            if ((string)e.Parameter == "Publications")
            {
                DataBase.LastAddedPublications.Clear();
                dg = dgAddPublications;
                
            }
            if ((string)e.Parameter == "Projects")
            {
                DataBase.LastAddedProjects.Clear();
                dg = dgAddProjects;

            }
            dg.Visibility = Visibility.Visible;
            dg.CurrentCell = new DataGridCellInfo(dg.Items[0], dg.Columns[0]);
            dg.BeginEdit();
        }

        private void RemoveRow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if ((string)e.Parameter == "Publications")
            {
                DataBase.RemovePublication(dgPublications.SelectedItem as Publication);
            }
            if ((string)e.Parameter == "Projects")
            {
                DataBase.RemoveProject(dgProjects.SelectedItem as Project);
            }

        }
        private void SaveChanges_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if ((string)e.Parameter == "Publications")
            {
                dgAddPublications.Visibility = Visibility.Collapsed;
                DataBase.AddLastAddedPublications();
            }
            if ((string)e.Parameter == "Projects")
            {
                dgAddProjects.Visibility = Visibility.Collapsed;
                DataBase.AddLastAddedProjects();
            }

        }

        private void EmployeerView()
        {
            EmployeeWindow viewEmployees = new EmployeeWindow();
            if (lstEmployees.SelectedIndex > -1)
                viewEmployees.lstEmployees.SelectedIndex = lstEmployees.SelectedIndex;
            viewEmployees.ShowDialog();

        }

        private void RemoveEmployeer_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (lstEmployees != null && lstEmployees.SelectedIndex > -1)
                e.CanExecute = true;
        }

        private void AddRow_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dgAddPublications == null || dgAddProjects == null)
            {
                e.CanExecute = false;
                return;
            }
            if (dgAddPublications.Visibility == System.Windows.Visibility.Collapsed &&
                dgAddProjects.Visibility == System.Windows.Visibility.Collapsed)
            {
                if ((e.Parameter as string) == "Publications")
                    e.CanExecute = true;
                if ((e.Parameter as string) == "Projects")
                    e.CanExecute = true;
            }
            
        }

        private void RemoveRow_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if ((e.Parameter as string) == "Publications" &&
                dgPublications.SelectedItem != null)
                e.CanExecute = true;
            if ((e.Parameter as string) == "Projects" &&
                dgProjects.SelectedItem != null)
                e.CanExecute = true;
        }

        private void SaveChanges_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if ((e.Parameter as string) == "Publications" &&
                dgAddPublications.Visibility == System.Windows.Visibility.Visible)
            {
                dgAddPublications.CommitEdit();
                e.CanExecute = true;
            }
            if ((e.Parameter as string) == "Projects" &&
                dgAddProjects.Visibility == System.Windows.Visibility.Visible)
            {
                dgAddProjects.CommitEdit();
                e.CanExecute = true;
            }

        }
    }
}
