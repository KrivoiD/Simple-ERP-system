using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ERPsystem
{
    [Serializable]
    public class Laboratory
    {
        string name;
        ObservableCollection<Employee> employees;
        Employee head;
        ObservableCollection<Publication> publications;
        ObservableCollection<Project> projects;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public ObservableCollection<Employee> Employees
        {
            get { return employees; }
            set { employees = value; }
        }

        public Employee Head
        {
            get { return head; }
            set { head = value; }
        }

        public ObservableCollection<Publication> Publications
        {
            get { return publications; }
            set { publications = value; }
        }

        public ObservableCollection<Project> Projects
        {
            get { return projects; }
            set { projects = value; }
        }

        public Laboratory()
        {
            publications = new ObservableCollection<Publication>();
            employees = new ObservableCollection<Employee>();
            projects = new ObservableCollection<Project>();
        }

    }
}
