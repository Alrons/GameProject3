using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addedItemModel
{
    public int id;
    public int userId;
    public string title;
    public string description;
    public int price; // Change the type of price to int
    public int �urrency;
    public string image;
    public int place;
    public int health;
    public int power;
    public int xPower;

    public addedItemModel(int id, int userId, string title, string description, int price, int currency, string image, int place, int health, int power, int xPower)
    {
        this.id = id;
        this.userId = userId;
        this.title = title;
        this.description = description;
        this.price = price;
        this.�urrency = currency;
        this.image = image;
        this.place = place;
        this.health = health;
        this.power = power;
        this.xPower = xPower;
    }
}
