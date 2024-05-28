namespace covadis.Api.Models
{
    public class Auto
    {
        public Guid Id { get; set; }
        public string Kenteken { get; set; }
        public string Merk { get; set; }
        public string Model { get; set; }
        public virtual ICollection<Rit>? Rides { get; set; }
    }
}
