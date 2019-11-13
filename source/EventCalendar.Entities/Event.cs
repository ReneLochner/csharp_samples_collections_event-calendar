using System;
using System.Collections.Generic;

namespace EventCalendar.Entities
{
    public class Event : IComparable
    {
        // Person invitor, string title, DateTime dateTime, int maxParticipators = 0

        /*
        /// Ein Event mit dem angegebenen Titel und dem Termin wird für den Einlader angelegt.
        /// Der Titel muss innerhalb der Veranstaltungen eindeutig sein und das Datum darf nicht
        /// in der Vergangenheit liegen.
        /// Mit dem optionalen Parameter maxParticipators kann eine Obergrenze für die Teilnehmer festgelegt
        /// werden.
        */
        // private DateTime _dateTime;

        private Person Invitor { get; set; }
        public int MaxParticipators { get; set; }
        public List<Person> Participants;

        public Event(Person invitor, string title, DateTime dateTime, int maxParticipators) : this(invitor, title, dateTime)
        {
            this.MaxParticipators = maxParticipators;
        }

        public Event(Person invitor, string title, DateTime dateTime)
        {
            this.Invitor = invitor;
            this.Title = title;
            this.DateTime = dateTime;
            this.MaxParticipators = -1;
            this.Participants = new List<Person>();
        }

        public string Title { get; }


        public DateTime DateTime
        {
            get; set;
        }

        public int CompareTo(object obj)
        {
            Event event2 = (Event)obj;

            if(this.DateTime.CompareTo(event2.DateTime) < 0)
            {
                return -1;
            }
            return 1;
        }
    }
}
