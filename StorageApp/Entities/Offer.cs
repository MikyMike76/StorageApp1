
using System.Xml.Linq;

namespace StorageApp.Entities
{
    public class Offer : EntityBase
    {
        public string NameOfCompany { get; set; }
        public override string ToString() => $"Id: {Id}, Name: {NameOfCompany}";

    }
}
