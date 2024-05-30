using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateProductBuyList : MonoBehaviour
{
    [SerializeField] private GameObject _productPrefab;
    [SerializeField] private Transform _productListParent;
    
    private void Start()
    {
        ShoppingCartManager.Instance.OnShoppingCartAnalyzed += Populate;
        ShoppingCartManager.Instance.OnShoppingCartListModified += Populate;
    }

    private void Populate(List<Product> products)
    {
        foreach (Transform child in _productListParent.transform)
        {
            Destroy(child.gameObject);
        }
        
        foreach (Product product in products)
        {
            ProductVisual productVisual = Instantiate(_productPrefab, _productListParent).GetComponent<ProductVisual>();
            productVisual.ProductInformation = product;
            productVisual.DisplayInformation();
        }
    }
}
