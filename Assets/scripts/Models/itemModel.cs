using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ItemModel
{

    public int id;
    public string title;
    public string description;
    public int price;
    public int currency;
    public string image;
    public int place;
    public int health;
    public int power;

    // temporarily not working feature
    public int xPover;

    public ItemModel(int Id, string Title, string Description, int Price, int Ñurrency, string Image, int Place, int Health, int Power, int XPover)
    {

        this.id = Id;
        this.title = Title;
        this.description = Description;
        this.price = Price;
        this.currency = Ñurrency;
        this.image = Image;
        this.place = Place;
        this.health = Health;
        this.power = Power;
        this.xPover = XPover;
    }
}