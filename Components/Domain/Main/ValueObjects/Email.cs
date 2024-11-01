using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using TaskList.Components.Domain.Main.UseCases.ResponseCase;

namespace TaskList.Components.Domain.Main.ValueObjects
{
    public class Email
    {      
        public string Address { get; private set; } = string.Empty;

        protected Email() { }
        public Email(string address)
        {
            Address = address.Trim().ToLower();
        }
    }
}
