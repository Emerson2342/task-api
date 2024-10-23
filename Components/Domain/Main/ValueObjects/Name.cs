namespace TaskList.Components.Domain.Main.ValueObjects
{
    public class Name
    {
        public string FirstName { get; set; }  =string.Empty;
        public string LastName { get; set; } = string.Empty;
        protected Name() { }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
