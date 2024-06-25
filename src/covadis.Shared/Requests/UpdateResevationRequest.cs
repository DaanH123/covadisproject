namespace covadis.Shared.Requests
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateReservationRequest
    {
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime Until { get; set; }
    }
}
