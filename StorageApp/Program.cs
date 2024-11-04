using System.Collections.Immutable;
using System.Xml.Linq;
using StorageApp.Data;
using StorageApp.Entities;
using StorageApp.Repos;

var medicineRepo = new SqlRepository<Medicine>(new StorageAppDbContext());
medicineRepo.itemAdded += OnMedicineAdded;
medicineRepo.Add(new Medicine { Name = "Ibuprofen", Amount = 5 });
medicineRepo.Add(new Medicine { Name = "Augmentin", Amount = 3 });
medicineRepo.Save();

var itemMedicineUpdated = new Medicine { Id = 1, Name = "Ibuprofen", Amount = 2 };
medicineRepo.itemUpdated += OnMedicineUpdated;


medicineRepo.Update(itemMedicineUpdated);
medicineRepo.Save();

var offerRepo = new SqlRepository<Offer>(new StorageAppDbContext());
offerRepo.itemAdded += OnOfferAdded;
offerRepo.Add(new Offer { NameOfCompany = "Neuca" });
offerRepo.Add(new Offer { NameOfCompany = "Medi" });
offerRepo.Save();

var itemOfferUpdated = new Offer { Id = 2, NameOfCompany = "Medi_System" };
offerRepo.itemUpdated += OnOfferUpdated;
offerRepo.Update(itemOfferUpdated);
offerRepo.Save();

var repo = medicineRepo.GetAll();
foreach (var item in repo)
{
    Console.WriteLine(item);
}

var repo1 = offerRepo.GetAll();
foreach (var item in repo1)
{
    Console.WriteLine(item);
}

static void OnMedicineAdded(object? sender, Medicine medicine)
{
    Console.WriteLine($"New medicine {medicine.Name} was added! Amount: {medicine.Amount}");
}

static void OnOfferAdded(object? sender, Offer offer)
{
    Console.WriteLine($"New medicine {offer.NameOfCompany} was added!");
}

void OnMedicineUpdated(object? sender, Medicine medicine)
{
    Console.WriteLine($"Medicine Id: \"{medicine.Id}\" was updated! Actual name: {medicine.Name}, amount: {medicine.Amount}");
}

void OnOfferUpdated(object? sender, Offer offer)
{
    Console.WriteLine($"Offer Id: \"{offer.Id}\" was updated! Actual name: {offer.NameOfCompany}");
}
