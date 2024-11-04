using System.Collections.Immutable;
using System.Xml.Linq;
using StorageApp.Data;
using StorageApp.Entities;
using StorageApp.Repos;
using StorageApp.Repos.Extensions;

var medicineRepo = new SqlRepository<Medicine>(new StorageAppDbContext());
medicineRepo.itemAdded += OnMedicineAdded;
var offerRepo = new SqlRepository<Offer>(new StorageAppDbContext());
offerRepo.itemAdded += OnOfferAdded;
Console.WriteLine("------------------------MENU------------------------");
Console.WriteLine("Welcome to StorageApp!");
Console.WriteLine("Choose what entity you want to enter:");
Console.WriteLine("Press \"1\" - enter \"Medicine\"");
Console.WriteLine("Press \"2\" - enter \"Offers\"");
var choiceEntity = Console.ReadLine();
if (float.TryParse(choiceEntity, out float choiceEntityFloat))
{
    switch(choiceEntityFloat)
    {
        case 1:
            Console.WriteLine("You've entered \"Medicine\"");
            break;
        case 2:
            Console.WriteLine("You've entered \"Offers\"");
            break ;
        default:
            Console.WriteLine("Wrong input");
            break;
    }
}

Console.WriteLine("Now press the appropriate digit for the action you want to perform from the list below: ");
Console.WriteLine("Press \"1\" - to Add new item");
Console.WriteLine("Press \"2\" - to Update item");
Console.WriteLine("Press \"3\" - to Find and display item");
Console.WriteLine("Press \"4\" - to Delete item");
Console.WriteLine("Press \"5\" - to Save changes");
var choiceAction = Console.ReadLine();
if (choiceEntity == "1") //Enter Medicine
{
    if (float.TryParse(choiceAction, out float choiceActionFloat))
    {
        switch (choiceActionFloat)
        {
            case 1:
                Console.WriteLine("You've entered \"Add\"");
                Console.Write("Provide the name of new Medicine: ");
                var nameOfNewMedicine = Console.ReadLine();
                Console.Write($"Now provide the amount of {nameOfNewMedicine}: ");
                var amountOfNewMedicine = Console.ReadLine();
                if (int.TryParse(amountOfNewMedicine, out int amountOfNewMedicineFloat))
                {
                    var medicines = new[]
                    {
                        new Medicine {Name =  nameOfNewMedicine, Amount = amountOfNewMedicineFloat},
                    };
                    medicineRepo.AddBatch(medicines);
                }
                else
                {
                    Console.WriteLine("Wrong input");
                }
                break;
            case 2:
                Console.WriteLine("You've entered \"Offers\"");
                break;
            case 3:
                Console.WriteLine("You've entered \"Medicine\"");
                break;
            case 4:
                Console.WriteLine("You've entered \"Medicine\"");
                break;
            case 5:
                Console.WriteLine("You've entered \"Medicine\"");
                break;
            default:
                Console.WriteLine("Wrong input");
                break;
        }
    }
    else
    {
        Console.WriteLine("Wrong input");
    }
}
else //choiceEntity == "2"; //Enter Offers
{
    if (float.TryParse(choiceAction, out float choiceActionFloat))
    {
        switch (choiceActionFloat)
        {
            case 1:
                Console.WriteLine("You've entered \"Add\"");
                Console.Write("Provide the new Offer company name: ");
                var nameOfNewCompany = Console.ReadLine();
                var offers = new[]
                {
                   new Offer {NameOfCompany =  nameOfNewCompany},
                };
                offerRepo.AddBatch(offers);
                break;
            case 2:
                Console.WriteLine("You've entered \"Offers\"");
                break;
            case 3:
                Console.WriteLine("You've entered \"Medicine\"");
                break;
            case 4:
                Console.WriteLine("You've entered \"Medicine\"");
                break;
            case 5:
                Console.WriteLine("You've entered \"Medicine\"");
                break;
            default:
                Console.WriteLine("Wrong input");
                break;
        }
    }
    else
    {
        Console.WriteLine("Wrong input");
    }
}


//var medicineRepo = new SqlRepository<Medicine>(new StorageAppDbContext());
//medicineRepo.itemAdded += OnMedicineAdded;
medicineRepo.Add(new Medicine { Name = "Ibuprofen", Amount = 5 });
medicineRepo.Add(new Medicine { Name = "Augmentin", Amount = 3 });
medicineRepo.Save();

//var itemMedicineUpdated = new Medicine { Id = 1, Name = "Ibuprofen", Amount = 2 };
//medicineRepo.itemUpdated += OnMedicineUpdated;


//medicineRepo.Update(itemMedicineUpdated);
//medicineRepo.Save();

//var offerRepo = new SqlRepository<Offer>(new StorageAppDbContext());
//offerRepo.itemAdded += OnOfferAdded;
//offerRepo.Add(new Offer { NameOfCompany = "Neuca" });
//offerRepo.Add(new Offer { NameOfCompany = "Medi" });
//offerRepo.Save();

//var itemOfferUpdated = new Offer { Id = 2, NameOfCompany = "Medi_System" };
//offerRepo.itemUpdated += OnOfferUpdated;
//offerRepo.Update(itemOfferUpdated);
//offerRepo.Save();

//var repo = medicineRepo.GetAll();
//foreach (var item in repo)
//{
//    Console.WriteLine(item);
//}

//var repo1 = offerRepo.GetAll();
//foreach (var item in repo1)
//{
//    Console.WriteLine(item);
//}

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
