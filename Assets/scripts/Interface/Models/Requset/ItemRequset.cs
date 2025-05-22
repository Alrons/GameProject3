public class ItemRequest
{
    public int UserId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int Price { get; set; }

    public int Currency { get; set; }

    public string Image { get; set; }

    public int Place { get; set; }

    public string Group { get; set; }

    public int Health { get; set; }

     public int Power { get; set; }

    public int XPower { get; set; }

    public ItemRequest(int userId, string title, string description, int price, int currency, string image, int place, string group,  int health, int power, int xPower) 
    {
        this.UserId = userId;
        this.Title = title;
        this.Description = description;
        this.Price = price;
        this.Currency = currency;
        this.Image = image;
        this.Place = place;
        this.Group = group;
        this.Health = health;
        this.Power = power;
        this.XPower = xPower;
    
    }
}
