namespace Task02
{
    using System;

    /// <summary>
    /// Basic person class.
    /// </summary>
    public class Person : IMessageWriter
    {
        /// <summary>
        /// Refers to a method that works with MessageSenderEventArgs.
        /// </summary>
        /// <param name="sender">Passes sender value.</param>
        /// <param name="e">Passes MessageSenderEventArgs value.</param>
        public delegate void MessageSenderEventHandler(object sender, MessageSenderEventArgs e);

        /// <summary>
        /// Contains arrival event.
        /// </summary>
        public event MessageSenderEventHandler Arrival;

        /// <summary>
        /// Contains leaving event.
        /// </summary>
        public event EventHandler<EventArgs> Leaving;

        /// <summary>
        /// Gets person name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets arrival time.
        /// </summary>
        private static DateTime ArrivalTime { get; set; }

        /// <summary>
        /// Handles arrival event.
        /// </summary>
        /// <param name="e">Passes MessageSenderEventArgs value.</param>
        public void Arrived(MessageSenderEventArgs e)
        {
            this.Arrival += this.ArrivedMessage;
            this.OnArrival(e);
            Person.ArrivalTime = e.Time;
            Office.AddPerson(this);
        }

        /// <summary>
        /// Handles leaving event.
        /// </summary>
        public void Left()
        {
            this.Leaving += this.LeftMessage;
            this.OnLeaving();
            Office.RemovePerson(this);
        }

        /// <summary>
        /// Greets a specific person given the time.
        /// </summary>
        /// <param name="person">Passes specific person.</param>
        public void GreetPerson(Person person)
        {
            if (!this.Equals(person))
            {
                string partOfDay;
                if (ArrivalTime.Hour > 16)
                {
                    partOfDay = "evening";//todo pn хардкод (в константу)
                }
                else if (ArrivalTime.Hour > 11)
                {
                    partOfDay = "afternoon";//todo pn хардкод (в константу)
				}
                else
                {
                    partOfDay = "morning";//todo pn хардкод (в константу)
				}

                this.Write(string.Format("\"Good {0}, {1}\" - said {2}.", partOfDay, person.Name, this.Name));
            }
        }

        /// <summary>
        /// Says goodbye to a specific person.
        /// </summary>
        /// <param name="person">Passes specific person.</param>
        public void FarewellPerson(Person person)
        {
            if (!this.Equals(person))
            {
                this.Write(string.Format("\"Goodbye, {0}\" - said {1}.", person.Name, this.Name));
            }
        }

        public void Write(string inputString)
        {
            Console.WriteLine(inputString);//todo pn не заметил, наверное)
		}

        /// <summary>
        /// Raises the event associated with the MessageSenderEventHandler delegate.
        /// </summary>
        /// <param name="e">Passes MessageSenderEventArgs parameter.</param>
        protected virtual void OnArrival(MessageSenderEventArgs e)
        {
            this.Arrival?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the event associated with the EventHandler delegate.
        /// </summary>
        protected virtual void OnLeaving()
        {
            this.Leaving?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Messages about person arrival.
        /// </summary>
        /// <param name="sender">Passes sender value.</param>
        /// <param name="e">Passes MessageSenderEventArgs value.</param>
        private void ArrivedMessage(object sender, MessageSenderEventArgs e)
        {
            this.Write(string.Format("{0} arrived.", this.Name));
        }

        /// <summary>
        /// Messages about person leaving.
        /// </summary>
        /// <param name="sender">Passes sender value.</param>
        /// <param name="e">Passes EventArgs value.</param>
        private void LeftMessage(object sender, EventArgs e)
        {
            this.Write(string.Format("{0} left.", this.Name));
        }
    }
}