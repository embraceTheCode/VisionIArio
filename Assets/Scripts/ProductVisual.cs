using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ProductVisual : MonoBehaviour
{
    [HideInInspector] public Product ProductInformation;
    
    [SerializeField] private TextMeshProUGUI _nameAndCode;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private TextMeshProUGUI _amount;
    [SerializeField] private TextMeshProUGUI _discountPrice;
    [SerializeField] private GameObject _discountBubble;
    [SerializeField] private TextMeshProUGUI _discountAmount;
    [SerializeField] private Image _image;

    [SerializeField] private GameObject _addButton;
    [SerializeField] private GameObject _reduceButton;
    [SerializeField] private GameObject _modifyButton;
    [SerializeField] private GameObject _deleteButton;
    
    public void DisplayInformation()
    {
        _image.sprite = ProductInformation.Image;
        _nameAndCode.text = ProductInformation.name + "\n<size=26><color=#BEB5B6>" + ProductInformation.code + "</color></size>";
        _price.text = ProductInformation.TotalWithoutDiscount.ToString("F2") + "\n<size=26><color=#BEB5B6>@" + ProductInformation.unit_price + "</color></size>";
        _amount.text = ProductInformation.quantity.ToString();

        if (ProductInformation.discounnt_amount != 0)
        {
            //Strikeout the normal text
            _price.text = "<s>" + _price.text + "</s>";
            
            //Show discounted text
            _discountPrice.gameObject.SetActive(true);
            _discountPrice.text = ProductInformation.TotalWithDiscount.ToString("F2");
            
            //Show discount percentage bubble
            _discountBubble.gameObject.SetActive(true);
            _discountAmount.text = ProductInformation.discounnt_amount * 100 + "%";
        }
        else
        {
            _discountPrice.gameObject.SetActive(false);
            _discountBubble.gameObject.SetActive(false);
        }
    }

    public void ActivateModify()
    {
        
    }

    public void DeactivateModify()
    {
        
    }

    public void AddAmount()
    {
        ShoppingCartManager.Instance.ModifyAmount(ProductInformation, +1);
        DisplayInformation();
    }

    public void ReduceAmount()
    {
        ShoppingCartManager.Instance.ModifyAmount(ProductInformation, -1);
        DisplayInformation();
    }
}
