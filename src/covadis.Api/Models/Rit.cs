namespace covadis.Api.Models
{
    public class Rit
    {
        public Guid Id { get; set; }
        public Registration registratie { get; set; }
        public Auto auto { get; set; }
        public Werknemer bestuurder { get; set; }
    }
}
