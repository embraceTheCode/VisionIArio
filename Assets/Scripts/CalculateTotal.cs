using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CalculateTotal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalText; 
    
    private void Start()
    {
        ShoppingCartManager.Instance.OnShoppingCartAnalyzed += ShowTotal;
        ShoppingCartManager.Instance.OnValueChanged += ShowTotal;
        ShoppingCartManager.Instance.OnShoppingCartListModified += ShowTotal;
    }

    private void ShowTotal(List<Product> products)
    {
        float total = 0;
        foreach (var product in products)
        {
            total += product.TotalWithDiscount;
        }

        _totalText.text = "$" + total.ToString("F2");
    }
}
