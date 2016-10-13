using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace ERPsystem
{
    [Serializable]
    public static class DataBase
    {
        static Laboratory laboratory;
        static Employee newEmployee;
        static ObservableCollection<Publication> lastAddedPublications;
        static ObservableCollection<Project> lastAddedProjects;

        public static ObservableCollection<Project> LastAddedProjects
        {
            get { return DataBase.lastAddedProjects; }
            set { DataBase.lastAddedProjects = value; }
        }

        public static ObservableCollection<Publication> LastAddedPublications
        {
            get { return DataBase.lastAddedPublications; }
            set { DataBase.lastAddedPublications = value; }
        }

        public static Employee NewEmployee
        {
            get { return DataBase.newEmployee; }
            set { DataBase.newEmployee = value; }
        }

        public static Laboratory Laboratory
        {
            get { return DataBase.laboratory; }
            set { DataBase.laboratory = value; }
        }

        static DataBase()
        {
            laboratory = new Laboratory();
            newEmployee = new Employee();
            lastAddedPublications = new ObservableCollection<Publication>();
            lastAddedProjects = new ObservableCollection<Project>();
        }

        //Работа с файлом
        public static bool SaveToFile(string Path)
        {
            try
            {
                using (FileStream file = new FileStream(Path, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(file, DataBase.laboratory);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка при сохранении", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        public static bool LoadFromFile(string Path)
        {
            try
            {
                if(File.Exists(Path))
                    using (FileStream file = new FileStream(Path, FileMode.Open))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        DataBase.laboratory = (Laboratory)bin.Deserialize(file);
                    }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка при загрузке", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            UpdatePhotoPath();
            return true;
        }

        //Работа с публикацией
        public static void AddLastAddedPublications()
        {
            foreach (Publication pub in DataBase.LastAddedPublications)
            {
                if (!(string.IsNullOrEmpty(pub.Title) ||
                    string.IsNullOrEmpty(pub.Journal) ||
                    pub.PublicationDate < new DateTime(1950, 1, 1) ||
                    pub.Authors.Count == 0))
                {
                    AddPublication(pub);
                }
            }
        }
        public static bool AddPublication(Publication pub)
        {
            if (laboratory.Publications.Contains(pub))
                return false;
            Employee[] authors = new Employee[pub.Authors.Count];
            pub.Authors.CopyTo(authors, 0);
            foreach (Employee author in authors)
                if (laboratory.Employees.Contains(author))
                {
                    Employee emp = laboratory.Employees[laboratory.Employees.IndexOf(author)];
                    emp.Publications.Add(pub);
                    pub.Authors.Remove(author);
                    pub.Authors.Add(emp);
                }
            laboratory.Publications.Add(pub);
            return true;
        }
        public static bool RemovePublication(Publication pub)
        {
            if (pub == null)
                return false;
            if (laboratory.Publications.Contains(pub))
            {
                foreach (Employee author in pub.Authors)
                    if (laboratory.Employees.Contains(author) && author.Publications.Contains(pub))
                        author.Publications.Remove(pub);
                laboratory.Publications.Remove(pub);
                return true;
            }
            return false;
        }
        
        //Работа с проектом
        public static void AddLastAddedProjects()
        {
            foreach (Project proj in DataBase.LastAddedProjects)
            {
                if (!(string.IsNullOrEmpty(proj.Name) ||
                    proj.ProjectStart < new DateTime(2000, 1, 1) ||
                    proj.ProjectEnd < new DateTime(2000, 1, 1) ||
                    proj.Executors.Count == 0))
                {
                    AddProject(proj);
                }
            }
        }
        public static bool AddProject(Project proj)
        {
            if (laboratory.Projects.Contains(proj))
                return false;
            Employee[] executors = new Employee[proj.Executors.Count];
            proj.Executors.CopyTo(executors, 0);
            foreach (Employee executor in executors)
                if (laboratory.Employees.Contains(executor))
                {
                    Employee emp = laboratory.Employees[laboratory.Employees.IndexOf(executor)];
                    emp.Projects.Add(proj);
                    proj.Executors.Remove(executor);
                    proj.Executors.Add(emp);
                }
            proj.Leader = proj.Executors[0];
            laboratory.Projects.Add(proj);
            return true;
        }
        public static bool RemoveProject(Project proj)
        {
            if (proj == null)
                return false;
            if (laboratory.Projects.Contains(proj))
            {
                foreach (Employee executor in proj.Executors)
                    if (laboratory.Employees.Contains(executor) && executor.Projects.Contains(proj))
                        executor.Projects.Remove(proj);
                laboratory.Projects.Remove(proj);
                return true;
            }
            return false;
        }

        //Работа с сотрудниками
        public static bool AddNewEmployee(string PhotoPath)
        {
            if (laboratory.Employees.Contains(newEmployee) ||
                string.IsNullOrEmpty(newEmployee.FirstName) ||
                string.IsNullOrEmpty(newEmployee.LastName))
                return false;
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            if (!Directory.Exists(directory + @"\photos"))
                Directory.CreateDirectory(directory + @"\photos");
            if (!File.Exists(PhotoPath))
                return false;
            
            string relativePath =  @"\photos\" + newEmployee.FirstName + " " + newEmployee.LastName + Path.GetExtension(PhotoPath);
            if(!File.Exists(directory + relativePath))
                File.Copy(PhotoPath,directory + relativePath, true);
            newEmployee.RelativePath = relativePath;
            newEmployee.PhotoPath = new Uri(directory + relativePath, UriKind.Absolute);
            newEmployee.Laboratory = laboratory;
            laboratory.Employees.Add(newEmployee);
            return true;

        }

        public static bool RemoveEmployee(Employee empl)
        {
            if (!laboratory.Employees.Contains(empl))
                return false;
            laboratory.Employees.Remove(empl);
            return true;
        }

        public static void UpdatePhotoPath()
        {
            foreach (Employee empl in laboratory.Employees)
                if(!string.IsNullOrEmpty(empl.RelativePath))
                    empl.PhotoPath = new Uri(AppDomain.CurrentDomain.BaseDirectory + empl.RelativePath, UriKind.Absolute);
        }
    }
}
