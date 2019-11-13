using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using EventCalendar.Entities;
using static System.String;

namespace EventCalendar.Logic
{
    public class Controller
    {
        private readonly ICollection<Event> _events;
        public int EventsCount {
            get
            {
                return _events.Count;
            }
        }

        public Controller()
        {
            _events = new List<Event>();
        }

        /// <summary>
        /// Ein Event mit dem angegebenen Titel und dem Termin wird für den Einlader angelegt.
        /// Der Titel muss innerhalb der Veranstaltungen eindeutig sein und das Datum darf nicht
        /// in der Vergangenheit liegen.
        /// Mit dem optionalen Parameter maxParticipators kann eine Obergrenze für die Teilnehmer festgelegt
        /// werden.
        /// </summary>
        /// <param name="invitor"></param>
        /// <param name="title"></param>
        /// <param name="dateTime"></param>
        /// <param name="maxParticipators"></param>
        /// <returns>Wurde die Veranstaltung angelegt</returns>
        public bool CreateEvent(Person invitor, string title, DateTime dateTime, int maxParticipators = 0)
        {
            if (invitor == null || title == null || title.Length == 0 || dateTime < DateTime.Now)
            {
                return false;
            }
            foreach (Event evnt in _events)
            {
                if(evnt.Title.Equals(title))
                {
                    return false;
                }
            }
            if(maxParticipators == 0)
            {
                _events.Add(new Event(invitor, title, dateTime));
            } else
            {
                _events.Add(new Event(invitor, title, dateTime, maxParticipators));
            }
            return true;
        }


        /// <summary>
        /// Liefert die Veranstaltung mit dem Titel
        /// </summary>
        /// <param name="title"></param>
        /// <returns>Event oder null, falls es keine Veranstaltung mit dem Titel gibt</returns>
        public Event GetEvent(string title)
        {
            foreach(Event evnt in _events)
            {
                if(evnt.Title.Equals(title))
                {
                    return evnt;
                }
            }

            return null;
        }

        /// <summary>
        /// Person registriert sich für Veranstaltung.
        /// Eine Person kann sich zu einer Veranstaltung nur einmal registrieren.
        /// </summary>
        /// <param name="person"></param>
        /// <param name="ev">Veranstaltung</param>
        /// <returns>War die Registrierung erfolgreich?</returns>
        public bool RegisterPersonForEvent(Person person, Event ev)
        {
            if(ev == null || person == null || ev.Participants.Contains(person) || (ev.Participants.Count >= ev.MaxParticipators && ev.MaxParticipators != -1))
            {
                return false;
            }

            ev.Participants.Add(person);

            return true;
        }

        /// <summary>
        /// Person meldet sich von Veranstaltung ab0000000123456100
        /// </summary>
        /// <param name="person"></param>
        /// <param name="ev">Veranstaltung</param>
        /// <returns>War die Abmeldung erfolgreich?</returns>
        public bool UnregisterPersonForEvent(Person person, Event ev)
        {
            if(ev != null && ev.Participants.Contains(person))
            {
                ev.Participants.Remove(person);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Liefert alle Teilnehmer an der Veranstaltung.
        /// Sortierung absteigend nach der Anzahl der Events der Personen.
        /// Bei gleicher Anzahl nach dem Namen der Person (aufsteigend).
        /// </summary>
        /// <param name="ev"></param>
        /// <returns>Liste der Teilnehmer oder null im Fehlerfall</returns>
        public IList<Person> GetParticipatorsForEvent(Event ev)
        {
            if(ev == null)
            {
                return null;
            }
            List<Person> res = ev.Participants;
            res.Sort();
            return res;
        }

        /// <summary>
        /// Liefert alle Veranstaltungen der Person nach Datum (aufsteigend) sortiert.
        /// </summary>
        /// <param name="person"></param>
        /// <returns>Liste der Veranstaltungen oder null im Fehlerfall</returns>
        public List<Event> GetEventsForPerson(Person person)
        {
            if(person == null)
            {
                return null;
            }
            List<Event> res = new List<Event>();

            foreach(Event ev in _events)
            {
                if(ev.Participants.Contains(person))
                {
                    res.Add(ev);
                }
            }
            res.Sort();
            return res;
        }

        /// <summary>
        /// Liefert die Anzahl der Veranstaltungen, für die die Person registriert ist.
        /// </summary>
        /// <param name="participator"></param>
        /// <returns>Anzahl oder 0 im Fehlerfall</returns>
        public int CountEventsForPerson(Person participator)
        {
            int count = 0;

            if(participator == null)
            {
                return 0;
            }

            foreach(Event evnt in _events)
            {
                foreach(Person prticpnt in evnt.Participants)
                {
                    if(prticpnt.Equals(participator))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

    }
}
