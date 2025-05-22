public class ItemModel
{

    public int id;
    public string title;
    public string description;
    public int price;
    public int currency;
    public string image;
    public int place;
    public string group;
    public int health;
    public int power;

    // temporarily not working feature
    public int xPover;

    public ItemModel(int id, string title, string description, int price, int currency, string image, int place, string group, int health, int power, int xPover)
    {

        this.id = id;
        this.title = title;
        this.description = description;
        this.price = price;
        this.currency = currency;
        this.image = image;
        this.place = place;
        this.group = group;
        this.health = health;
        this.power = power;
        this.xPover = xPover;
    }
}