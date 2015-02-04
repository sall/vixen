using System;
using Vixen.Sys;

namespace Vixen.Commands
{
    [Serializable]
    public class StringCommand : Dispatchable<StringCommand>, ICommand
    {
        public StringCommand(string value)
        {
            CommandValue = value;
            CommandID = Guid.NewGuid();
        }

        public string CommandValue { get; set; }

        public Guid CommandID { get; set; }
        object ICommand.CommandValue
        {
            get
            {
                return new
                {
                    CommandValue = CommandValue,
                    ID = CommandID
                };
            }
            set
            {
                CommandValue = ((dynamic)value).CommandValue;
                CommandID = ((dynamic)value).ID;
            }
        }
    }
}
