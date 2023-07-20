using AcmeCorpAPI.Interfaces;

namespace AcmeCorpAPI.Models
{
    public class Customer : IContactInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
       public List<Order> Orders { get; set; }
    }
}
