using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ERPsystem.Classes
{
    class Commands
    {
        private static RoutedUICommand employeerView;
        private static RoutedUICommand newEmployeer;
        private static RoutedUICommand removeEmployeer;
        private static RoutedUICommand addRow;
        private static RoutedUICommand saveChanges;
        private static RoutedUICommand removeRow;

        public static RoutedUICommand EmployeerView
        {
            get { return Commands.employeerView; }
        }

        public static RoutedUICommand NewEmployeer
        {
            get { return Commands.newEmployeer; }
        }

        public static RoutedUICommand RemoveEmployeer
        {
            get { return Commands.removeEmployeer; }
        }

        public static RoutedUICommand AddRow
        {
            get { return Commands.addRow; }
        }

        public static RoutedUICommand RemoveRow
        {
            get { return Commands.removeRow; }
        }

        public static RoutedUICommand SaveChanges
        {
            get { return Commands.saveChanges; }
        }

        static Commands()
        {
            employeerView = new RoutedUICommand("Обзор сотрудника", "EmployeerView", typeof(Commands));
            newEmployeer = new RoutedUICommand("Новый сотрудник", "NewEmployeer", typeof(Commands));
            removeEmployeer = new RoutedUICommand("Уволить сотрудника", "RemoveEmployeer", typeof(Commands));
            addRow = new RoutedUICommand("Добавить строку", "AddRow", typeof(Commands));
            removeRow = new RoutedUICommand("Удалить строку", "DeleteRow", typeof(Commands));
            saveChanges = new RoutedUICommand("Сохранить изменения", "SaveChanges", typeof(Commands));
        }
    }
}
