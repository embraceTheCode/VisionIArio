using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Images", menuName = "ScriptableObjects")]
public class ProductImages : SerializedScriptableObject
{
    [field: SerializeField] public Dictionary<string, Sprite> ProductImage = new Dictionary<string, Sprite>();

    public Sprite GetSprite(string name)
    {
        return ProductImage[name];
    }
}
