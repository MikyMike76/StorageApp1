using StorageApp.Data;
using StorageApp.Entities;
using StorageApp.Repos;
using StorageApp.Repos.Extensions;

var medicineRepo = new SqlRepository<Medicine>(new StorageAppDbContext());
medicineRepo.Deserialize();
medicineRepo.itemAdded += OnMedicineAdded;
medicineRepo.itemUpdated += OnMedicineUpdated;
medicineRepo.itemRemoved += OnMedicineRemoved;
medicineRepo.itemSavedInJson += OnItemSavedInJson;
medicineRepo.Save();

var offerRepo = new SqlRepository<Offer>(new StorageAppDbContext());
offerRepo.Deserialize();
offerRepo.itemAdded += OnOfferAdded;
offerRepo.itemUpdated += OnOfferUpdated;
offerRepo.itemRemoved += OnOfferRemoved;
offerRepo.itemSavedInJson += OnItemSavedInJson;
offerRepo.Save();

Console.WriteLine("------------------------MENU------------------------");
Console.WriteLine("Welcome to StorageApp!");
string choiceEntity =  EnterEntity();
static string EnterEntity()
{
    Console.WriteLine("Choose what entity you want to enter:");
    Console.WriteLine("Press \"1\" - enter \"Medicine\"");
    Console.WriteLine("Press \"2\" - enter \"Offers\"");
    var choiceEntity = Console.ReadLine();
    if (int.TryParse(choiceEntity, out int choiceEntityInt))
    {
        switch (choiceEntityInt)
        {
            case 1:
                Console.WriteLine("You've entered \"Medicine\"");
                return choiceEntity;
            case 2:
                Console.WriteLine("You've entered \"Offers\"");
                return choiceEntity;
            default:
                Console.WriteLine("Wrong input");
                EnterEntity();
                return null;
        }
    }
    else
    {
        Console.WriteLine();
        return null;
    }
}

Console.WriteLine("Now press the appropriate digit for the action you want to perform from the list below: ");
Console.WriteLine("Press \"1\" - to Add new item");
Console.WriteLine("Press \"2\" - to Update item");
Console.WriteLine("Press \"3\" - to Find and display item");
Console.WriteLine("Press \"4\" - to Delete item");
Console.WriteLine("Press \"5\" - to List all items");

var choiceAction = Console.ReadLine();
if (choiceEntity == "1") //Enter Medicine
{
    if (int.TryParse(choiceAction, out int choiceActionInt))
    {
        switch (choiceActionInt)
        {
            case 1:
                Console.WriteLine("You've entered \"Add\"");
                Console.WriteLine();
                Console.Write("Provide the name of new Medicine: ");
                var nameOfNewItem = Console.ReadLine();
                Console.Write($"Now provide the amount of {nameOfNewItem}: ");
                var amountOfNewItem = Console.ReadLine();
                if (int.TryParse(amountOfNewItem, out int amountOfNewItemInt))
                {
                    var items = new[]
                    {
                        new Medicine {Name =  nameOfNewItem, Amount = amountOfNewItemInt},
                    };
                    medicineRepo.AddBatch(items);
                    Console.WriteLine("Do you want Save the result in .json?");
                    Console.WriteLine("1 -\"yes\", 2 -\"no\"");
                    var saveInFile = Console.ReadLine();
                    if (saveInFile == "1")
                    {
                        medicineRepo.SaveInFile();
                    }
                    else
                    {
                        Console.WriteLine("The data you've added wasn't saved in file!");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input in Id or/also Amount. Use only digits without spaces!");
                }
                break;
            case 2:
                Console.WriteLine("You've entered \"Update\"");
                Console.WriteLine();
                Console.Write("Now provide the Id number of item, you want to update: ");
                var idOfItemToUpdate = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Now provide the new name of Medicine: ");
                var newNameOfItem = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Now provide the new amount of Medicine: ");
                var newAmountOfItem = Console.ReadLine();
                if (int.TryParse(newAmountOfItem, out int newAmountOfItemInt) && int.TryParse(idOfItemToUpdate, out int idOfItemToUpdateInt))
                {
                    var itemToUpdate = new Medicine { Id = idOfItemToUpdateInt,  Name = newNameOfItem, Amount = newAmountOfItemInt };
                    medicineRepo.Update(itemToUpdate);
                    medicineRepo.Save();
                    Console.WriteLine("Do you want Save the result in .json?");
                    Console.WriteLine("1 -\"yes\", 2 -\"no\"");
                    var saveInFile = Console.ReadLine();
                    if (saveInFile == "1")
                    {
                        medicineRepo.SaveInFile();
                    }
                    else
                    {
                        Console.WriteLine("The data you've added wasn't saved in file!");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input in Amount. Use only digits without spaces!");
                }
                break;
            case 3:
                Console.WriteLine("You've entered \"Find and Display Item\"");
                Console.WriteLine();
                Console.Write("Now provide the Id number of item, you want to Find and Display: ");
                var idOfItemToFind = Console.ReadLine();
                Console.WriteLine();
                if (int.TryParse(idOfItemToFind, out int idOfItemToFindInt))
                {
                    var itemToDisplay = medicineRepo.GetById(idOfItemToFindInt);
                    if(itemToDisplay != null)
                    {
                        Console.WriteLine(itemToDisplay.ToString());
                    }
                    else
                    {
                        Console.WriteLine("There's no item with that ID");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input in Id or/also Amount. Use only digits without spaces!");
                }
                break;
            case 4:
                Console.WriteLine("You've entered \"Delete Item\"");
                Console.WriteLine();
                Console.Write("Now provide the Id number of item, you want to Delete: ");
                var idOfItemToDelete = Console.ReadLine();
                Console.WriteLine();
                if (int.TryParse(idOfItemToDelete, out int idOfItemToDeleteInt))
                {
                    medicineRepo.Remove(idOfItemToDeleteInt);
                    medicineRepo.Save();
                    Console.WriteLine("Do you want Save the result in .json?");
                    Console.WriteLine("1 -\"yes\", 2 -\"no\"");
                    var saveInFile = Console.ReadLine();
                    if (saveInFile == "1")
                    {
                        medicineRepo.SaveInFile();
                    }
                    else
                    {
                        Console.WriteLine("The data you've added wasn't saved in file!");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input in Id. Use only digits without spaces!");
                }
                break;
            case 5:
                Console.WriteLine("All Items from \"Medicine\" are listed below:");
                Console.WriteLine();
                var list = medicineRepo.GetAll();
                foreach (var item in list)
                {
                    Console.WriteLine(item.ToString());
                }
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
    if (int.TryParse(choiceAction, out int choiceActionInt))
    {
        switch (choiceActionInt)
        {
            case 1:
                Console.WriteLine("You've entered \"Add\"");
                Console.WriteLine();
                Console.Write("Provide the name of new Offer: ");
                var nameOfNewItem = Console.ReadLine();
                var items = new[]
                {
                new Offer {NameOfCompany =  nameOfNewItem},
                };
                offerRepo.AddBatch(items);
                Console.WriteLine("Do you want Save the result in .json?");
                Console.WriteLine("1 -\"yes\", 2 -\"no\"");
                var saveInFile = Console.ReadLine();
                if (saveInFile == "1")
                {
                    offerRepo.SaveInFile();
                }
                else
                {
                    Console.WriteLine("The data you've added wasn't saved in file!");
                }
                break;
            case 2:
                Console.WriteLine("You've entered \"Update\"");
                Console.WriteLine();
                Console.Write("Now provide the Id number of item, you want to update: ");
                var idOfItemToUpdate = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Now provide the new name of Medicine: ");
                var newNameOfItem = Console.ReadLine();
                Console.WriteLine();
                if (int.TryParse(idOfItemToUpdate, out int idOfItemToUpdateInt))
                {
                    var itemToUpdate = new Offer { Id = idOfItemToUpdateInt, NameOfCompany = newNameOfItem };
                    offerRepo.Update(itemToUpdate);
                    offerRepo.Save();
                    Console.WriteLine("Do you want Save the result in .json?");
                    Console.WriteLine("1 -\"yes\", 2 -\"no\"");
                    var saveInFileOffer = Console.ReadLine();
                    if (saveInFileOffer == "1")
                    {
                        offerRepo.SaveInFile();
                    }
                    else
                    {
                        Console.WriteLine("The data you've added wasn't saved in file!");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input in ID. Use only digits without spaces!");
                }
                break;
            case 3:
                Console.WriteLine("You've entered \"Find and Display Item\"");
                Console.WriteLine();
                Console.Write("Now provide the Id number of item, you want to Find and Display: ");
                var idOfItemToFind = Console.ReadLine();
                Console.WriteLine();
                if (int.TryParse(idOfItemToFind, out int idOfItemToFindInt))
                {
                    var itemToDisplay = offerRepo.GetById(idOfItemToFindInt);
                    if (itemToDisplay != null)
                    {
                        Console.WriteLine(itemToDisplay.ToString());
                    }
                    else
                    {
                        Console.WriteLine("There's no item with that ID");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input in Id or/also Amount. Use only digits without spaces!");
                }
                break;
            case 4:
                Console.WriteLine("You've entered \"Delete Item\"");
                Console.WriteLine();
                Console.Write("Now provide the Id number of item, you want to Delete: ");
                var idOfItemToDelete = Console.ReadLine();
                Console.WriteLine();
                if (int.TryParse(idOfItemToDelete, out int idOfItemToDeleteInt))
                {
                    offerRepo.Remove(idOfItemToDeleteInt);
                    offerRepo.Save();
                    Console.WriteLine("Do you want Save the result in .json?");
                    Console.WriteLine("1 -\"yes\", 2 -\"no\"");
                    var saveInFileOffer = Console.ReadLine();
                    if (saveInFileOffer == "1")
                    {
                        offerRepo.SaveInFile();
                    }
                    else
                    {
                        Console.WriteLine("The data you've added wasn't saved in file!");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input in Id. Use only digits without spaces!");
                }
                break;
            case 5:
                Console.WriteLine("All Items from \"Medicine\" are listed below:");
                Console.WriteLine();
                var list = offerRepo.GetAll();
                foreach (var item in list)
                {
                    Console.WriteLine(item.ToString());
                }
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

static void OnMedicineAdded(object? sender, Medicine medicine)
{
    Console.WriteLine($"New medicine {medicine.Name} was added! Amount: {medicine.Amount}, Id: \"{medicine.Id}\"");
}

static void OnOfferAdded(object? sender, Offer offer)
{
    Console.WriteLine($"New medicine {offer.NameOfCompany} was added! Id: \"{offer.Id}\"");
}

static void OnMedicineUpdated(object? sender, Medicine medicine)
{
    Console.WriteLine($"Medicine Id: \"{medicine.Id}\" was updated! Actual name: {medicine.Name}, amount: {medicine.Amount}");
}

static void OnOfferUpdated(object? sender, Offer offer)
{
    Console.WriteLine($"Offer Id: \"{offer.Id}\" was updated! Actual name: {offer.NameOfCompany}");
}
static void OnMedicineRemoved(object? sender, Medicine medicine)
{
    Console.WriteLine($"Medicine Id: \"{medicine.Id}\" name: {medicine.Name}, amount: {medicine.Amount} was removed!");
}

static void OnOfferRemoved(object? sender, Offer offer)
{
    Console.WriteLine($"Offer Id: \"{offer.Id}\" name: {offer.NameOfCompany} was removed!");
}
static void OnItemSavedInJson(object? sender, EventArgs e)
{
    Console.WriteLine("Data was saved in .json");
}