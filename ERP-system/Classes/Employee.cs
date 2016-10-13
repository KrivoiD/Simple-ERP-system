using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;

namespace ERPsystem
{
    [Serializable]
    public class Employee
    {
        string firstName;
        string lastName;
        DateTime birthday;
        string scientificDegree;
        string relativePath;
        Uri photoPath;
        ObservableCollection<Project> projects;
        ObservableCollection<Publication> publications;
        Laboratory laboratory;

        public Laboratory Laboratory
        {
            get { return laboratory; }
            set { laboratory = value; }
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

        public Uri PhotoPath
        {
            get { return photoPath; }
            set { photoPath = value; }
        }

        public string RelativePath
        {
            get { return relativePath; }
            set { relativePath = value; }
        }

        public string ScientificDegree
        {
            get { return scientificDegree; }
            set { scientificDegree = value; }
        }

        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public Employee()
        {
            projects = new ObservableCollection<Project>();
            publications = new ObservableCollection<Publication>();
        }

        public override string ToString()
        {
            return firstName + " " + lastName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return this == obj as Employee;
        }

        public static bool operator ==(Employee emp1, Employee emp2)
        {
            if (emp1 as Object == null || emp2 as Object == null)
                return false;
            if (emp1.firstName == emp2.firstName &&
                emp1.lastName == emp2.lastName)
                return true;
            return false;
        }

        public static bool operator !=(Employee emp1, Employee emp2)
        {
            if (emp1 as Object == null || emp2 as Object == null)
                return false;
            if (emp1.firstName == emp2.firstName &&
                emp1.lastName == emp2.lastName)
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
