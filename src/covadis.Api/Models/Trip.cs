using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace covadis.Api.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }

        public int ReservationId { get; set; }
        [ForeignKey(nameof(ReservationId))]
        public Reservation Reservation { get; set; }

        public int OdometerStart { get; set; }
        public int OdometerEnd { get; set; }

        public virtual ICollection<Address>? Addresses { get; set; }
    }
}
