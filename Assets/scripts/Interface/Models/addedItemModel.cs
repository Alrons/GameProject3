public class AddedItemModel
{
    public int id;
    public int userId;
    public string title;
    public string description;
    public int price; // Change the type of price to int
    public int currency;
    public string image;
    public int place;
    public string group;
    public int health;
    public int power;
    public int xPower;

    public AddedItemModel(int id, int userId, string title, string description, int price, int currency, string image, int place, string group, int health, int power, int xPower)
    {
        this.id = id;
        this.userId = userId;
        this.title = title;
        this.description = description;
        this.price = price;
        this.currency = currency;
        this.image = image;
        this.place = place;
        this.group = group;
        this.health = health;
        this.power = power;
        this.xPower = xPower;
    }
}
