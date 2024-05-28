namespace covadis.Api.Models
{
    public class Registration
    {
        public Guid Id { get; set; }
        public int StartKM { get; set; }
        public int EndKM { get; set; }
        public ICollection<Adress> Adresses { get; set; }
    }
}
