namespace covadis.Shared.Requests
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateUserRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
