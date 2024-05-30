using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Converters;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine.SceneManagement;

public class ShoppingCartManager : MonoBehaviour
{
    public static ShoppingCartManager Instance;

    public Action<List<Product>> OnShoppingCartAnalyzed; 
    public Action<List<Product>> OnShoppingCartListModified; 
    public Action<List<Product>> OnValueChanged;

    public List<Product> ShoppingCart { get; private set; } = new List<Product>();
    public ProductImages ProductImages;
    
    private string _path;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void TakePicture()
    {
        //Call the API
        //StartCoroutine(nameof(AnalyzeShoppingCart));
        NativeCamera.CameraCallback x;
        x = temp;
        NativeCamera.TakePicture(x, -1, true, NativeCamera.PreferredCamera.Rear);
    }

    void temp(string path)
    {
        _path = path;
        
        string filePath = Path.Combine(Application.persistentDataPath, _path);
        byte[] fileBytes = File.ReadAllBytes(filePath);
        
        StartCoroutine(PostRequest("https://yolo-api-g104.onrender.com/uploadfile/"));
    }

    IEnumerator PostRequest(string uri)
    {
        WWWForm form = new WWWForm();

        // Load the PNG file from the persistent data path
        string filePath = Path.Combine(Application.persistentDataPath, _path);
        byte[] imageBytes = File.ReadAllBytes(filePath);

        // Add the PNG file to the form
        form.AddBinaryData("file", imageBytes, _path, "image/png");

        using UnityWebRequest www = UnityWebRequest.Post(uri, form);
        yield return www.SendWebRequest();
        
        if (www.result != UnityWebRequest.Result.Success)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            Debug.LogError(www.error);
        }
        else
        {
            string receivedMessage = www.downloadHandler.text;
            
            try
            {
                ShoppingCart = JsonConvert.DeserializeObject<List<Product>>(receivedMessage);
            }
            catch (Exception e)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
        
        OnShoppingCartAnalyzed?.Invoke(ShoppingCart);
    }
    
    public void ModifyAmount(Product product, int delta)
    {
        for (int i = 0; i < ShoppingCart.Count; i++)
        {
            if (ShoppingCart[i].code == product.code)
            {
                if (product.quantity + delta == 0)
                {
                    RemoveProductAt(i);
                }
                else
                {
                    ShoppingCart[i].quantity += delta;
                    OnValueChanged?.Invoke(ShoppingCart);
                }
                return;
            }
        }
    }
    
    public void RemoveProduct(Product product)
    {
        for (int i = 0; i < ShoppingCart.Count; i++)
        {
            if (ShoppingCart[i].code == product.code)
            {
                RemoveProductAt(i);
                return;
            }
        }
    }
    
    public void RemoveProductAt(int index)
    {
        ShoppingCart.RemoveAt(index);
        OnShoppingCartListModified?.Invoke(ShoppingCart);
    }
}
