using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class itemModel
{

    public int Id;
    public string Title;
    public string Description;
    public int Price;
    public int Ñurrency;
    public string Image;
    public int Place;
    public int Health;
    public int Power;

    // temporarily not working feature
    public int XPover;

    public itemModel(int Id, string Title, string Description, int Price, int Ñurrency, string Image, int Place, int Health, int Power, int XPover)
    {

        this.Id = Id;
        this.Title = Title;
        this.Description = Description;
        this.Price = Price;
        this.Ñurrency = Ñurrency;
        this.Image = Image;
        this.Place = Place;
        this.Health = Health;
        this.Power = Power;
        this.XPover = XPover;
    }
}