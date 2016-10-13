using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace ERPsystem
{
    [Serializable]
    public class Project
    {
        string name;
        Employee leader;
        ObservableCollection<Employee> executors;
        DateTime projectStart;
        DateTime projectEnd;
        decimal projectCost;

        public string Name
        {
          get { return name; }
          set { name = value; }
        }
        
        public Employee Leader
        {
            get { return leader; }
            set { leader = value; }
        }
        
        public ObservableCollection<Employee> Executors
        {
            get { return executors; }
            set { executors = value; }
        }
        
        public DateTime ProjectStart
        {
            get { return projectStart; }
            set { projectStart = value; }
        } 

        public DateTime ProjectEnd
        {
            get { return projectEnd; }
            set { projectEnd = value; }
        }
       

        public decimal ProjectCost
        {
            get { return projectCost; }
            set { projectCost = value; }
        }

        public Project()
        {
            executors = new ObservableCollection<Employee>();
            leader = new Employee();
            projectStart = new DateTime();
            projectEnd = new DateTime();
        }

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return this == obj as Project;
        }

        public static bool operator ==(Project proj1, Project proj2)
        {
            if (proj1 as Object == null || proj2 as Object == null)
                return false;
            if (proj1.name == proj2.name ||
                proj1.leader == proj2.leader ||
                proj1.projectStart == proj2.projectStart)
                return true;
            return false;
        }

        public static bool operator !=(Project proj1, Project proj2)
        {
            if (proj1 == proj2)
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
