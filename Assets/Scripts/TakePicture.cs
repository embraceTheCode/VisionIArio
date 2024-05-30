using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePicture : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ShoppingCartManager.Instance.TakePicture();
    }
}
