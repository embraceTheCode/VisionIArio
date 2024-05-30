using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Product
{
    public Product(string name, string code, float unitPrice, float discounntAmount, int quantity)
    {
        this.name = name;
        this.code = code;
        unit_price = unitPrice;
        discounnt_amount = discounntAmount;
        this.quantity = quantity;
    }

    [FormerlySerializedAs("Name")] public string name;
    [FormerlySerializedAs("Code")] public string code;
    [FormerlySerializedAs("SinglePrice")] public float unit_price;
    public string discount_name;

    public float TotalWithDiscount
    {
        get {return unit_price * quantity * (1 - discounnt_amount);}
    }

    public float TotalWithoutDiscount
    {
        get { return unit_price * quantity; }
    }

    public Sprite Image
    {
        get { return ShoppingCartManager.Instance.ProductImages.GetSprite(name); }
    }

    [FormerlySerializedAs("Discount")] public float discounnt_amount;

    [FormerlySerializedAs("Amount")] public int quantity;

    public void print()
    {
        Debug.Log(name);
        Debug.Log(code);
        Debug.Log(unit_price);
        Debug.Log(discounnt_amount);
        Debug.Log(quantity);
    }
}
