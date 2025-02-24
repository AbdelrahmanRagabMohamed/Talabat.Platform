namespace Talabat.APIs.DTOs;

public class AddressDto
{
    public int Id { get; set; }   // For Iqula it with Adress Id to update not insert new. 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
}
