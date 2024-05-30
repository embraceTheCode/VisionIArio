using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnAction : MonoBehaviour
{
    private void Start()
    {
        ShoppingCartManager.Instance.OnShoppingCartAnalyzed += Hide;
    }

    private void Hide(List<Product> _)
    {
        gameObject.SetActive(false);
    }
}
