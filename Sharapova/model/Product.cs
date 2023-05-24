namespace Sharapova.model
{
    public class Product
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Product(int Id, string Title, string Description, decimal Price)
        {
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            this.Price = Price;
        }

    }
}
