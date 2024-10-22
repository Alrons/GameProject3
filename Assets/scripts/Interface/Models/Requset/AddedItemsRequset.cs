public class AddedItemsRequest
    {
    public int userId;

    public string title;

    public string description;

    public int price;

    public int currency;

    public string image;

    public int place;

    public int health;

    public int power;

    public int xPover;

    public AddedItemsRequest(int userId, string title, string description, int price, int currency, string image, int place, int health, int power, int xPover)
    {
        this.userId = userId;
        this.title = title;
        this.description = description;
        this.price = price;
        this.currency = currency;
        this.image = image;
        this.place = place;
        this.health = health;
        this.power = power;
        this.xPover = xPover;
    }
}

