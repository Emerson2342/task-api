namespace TaskList.Components.Domain.Main.ValueObjects
{
    public class Name
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        protected Name() { }

        public Name(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new Exception("Favor preencher NOME e SOBRENOME");
            }

            FirstName = firstName;
            LastName = lastName;
        }
    }
}
