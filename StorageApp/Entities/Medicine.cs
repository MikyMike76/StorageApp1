namespace StorageApp.Entities
{
    public class Medicine: EntityBase
    {
        public string? Name { get; set; }
        public int Amount { get; set; }
        public override string ToString() => $"Id: {Id}, Name: {Name}, Amount: {Amount}";
    }
}
