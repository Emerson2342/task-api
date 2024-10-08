using System.Text.RegularExpressions;
using TaskList.Components.Domain.Main.UseCases.Response;

namespace TaskList.Components.Domain.Main.ValueObjects
{
    public partial class Email
    {
        private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public string Address { get; private set; } = string.Empty;

        protected Email() { }
        public Email(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new Exception("Favor preencher o campo email corretamente!");

            Address = address.Trim().ToLower();

            if (!EmailRegex().IsMatch(Address))
                throw new Exception("E-mail inválido!");


        }

        [GeneratedRegex(Pattern)]
        private static partial Regex EmailRegex();
    }
}
