using System;
using System.Collections.Generic;
using System.Text;

namespace EventCalendar.Entities
{

    /// <summary>
    /// Person kann sowohl zu einer Veranstaltung einladen,
    /// als auch an Veranstaltungen teilnehmen
    /// </summary>
    public class Person : IComparable
    {
        public string LastName { get; }
        public string FirstName { get; }
        public string MailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public int EventCount { get; set; }

        public string FullName {
            get {
                return LastName + " " + FirstName;
            }
        }


        public Person(string lastName, string firstName)
        {
            LastName = lastName;
            FirstName = firstName;
        }

        public int CompareTo(object obj)
        {
            Person p2 = (Person)obj;

            if (this.EventCount < p2.EventCount)
            {
                return 1;
            }
            else if (this.EventCount > p2.EventCount)
            {
                return -1;
            }
            if (this.FullName.CompareTo(p2.FullName) < 0)
            {
                return -1;
            } else if (this.FullName.CompareTo(p2.FullName) > 0)
            {
                return 1;
            }
            return 0;
        }
    }
}



        /// <summary>
        /// Liefert alle Teilnehmer an der Veranstaltung.
        /// Sortierung absteigend nach der Anzahl der Events der Personen.
        /// Bei gleicher Anzahl nach dem Namen der Person (aufsteigend).
        /// </summary>
        /// <param name="ev"></param>
        /// <returns>Liste der Teilnehmer oder null im Fehlerfall</returns>