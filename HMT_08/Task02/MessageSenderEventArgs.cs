namespace Task02
{
    using System;

    /// <summary>
    /// Passes time value.
    /// </summary>
    public class MessageSenderEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the MessageSenderEventArgs class.
        /// </summary>
        /// <param name="time">Passes time value.</param>
        public MessageSenderEventArgs(DateTime time)
        {
            this.Time = time;
        }

        /// <summary>
        /// Gets time value.
        /// </summary>
        public DateTime Time { get; set; }
    }
}
