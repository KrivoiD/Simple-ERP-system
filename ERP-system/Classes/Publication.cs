using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace ERPsystem
{
    [Serializable]
    public class Publication
    {
        string title;
        ObservableCollection<Employee> authors;
        string journal;
        DateTime publicationDate;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        
        public ObservableCollection<Employee> Authors
        {
            get { return authors; }
            set { authors = value; }
        }
        
        public string Journal
        {
            get { return journal; }
            set { journal = value; }
        }

        public DateTime PublicationDate
        {
            get { return publicationDate; }
            set { publicationDate = value; }
        }

        public Publication()
        {
            authors = new ObservableCollection<Employee>();
        }
        public override string ToString()
        {
            return title;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return this == (obj as Publication);
        }

        public static bool operator ==(Publication pub1, Publication pub2)
        {
            if (pub1 as Object == null || pub2 as Object == null)
                return false;
            if(pub1.title == pub2.title &&
                pub1.journal == pub2.journal &&
                pub1.publicationDate == pub2.publicationDate &&
                pub1.Authors.Count == pub2.Authors.Count)
            {
                    foreach (Employee author in pub2.Authors)
                    {
                        if (!pub1.Authors.Contains(author))
                            return false;
                    }
                    return true;   
            }
            return false;
        }

        public static bool operator !=(Publication pub1, Publication pub2)
        {
            if (pub1 == pub2)
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
