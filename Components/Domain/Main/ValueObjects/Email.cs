using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TaskList.Components.Domain.Main.ValueObjects
{
    public partial class Email
    {
        private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        [Required(ErrorMessage = "Favor, preencher o campo EMAIL")]
        public string Address { get; private set; } = string.Empty;

        protected Email() { }
        public Email(string address)
        {
            Address = address.Trim().ToLower();

            if (!EmailRegex().IsMatch(Address))
                throw new Exception("E-mail inválido!");


        }

        [GeneratedRegex(Pattern)]
        private static partial Regex EmailRegex();
    }
}
