namespace WebApi.Models;

using System.ComponentModel.DataAnnotations;

public class CreateRequest
{

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

}