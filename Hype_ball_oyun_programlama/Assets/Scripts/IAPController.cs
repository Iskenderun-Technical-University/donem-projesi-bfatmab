using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;

public class IAPController : MonoBehaviour,IStoreListener
{
    IStoreController controller;
    public UIManagerr managerr;
    public string[] product;

    public void Start()
    {
        IAPStart();
    }

    private void IAPStart()
    {
        var module = StandardPurchasingModule.Instance();
       ConfigurationBuilder builder= ConfigurationBuilder.Instance(module);

        foreach (string item in product)
        {
            builder.AddProduct(item, ProductType.Consumable);
        }

        UnityPurchasing.Initialize(this, builder);

    }


    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Failed Initiliaze");
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("Purchuase failed");

    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        if (string.Equals(e.purchasedProduct.definition.id, product[0],StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("moneyy", PlayerPrefs.GetInt("moneyy") + 2500);
            managerr.CoinTextUpdate();
             return  PurchaseProcessingResult.Complete;

        }

        else if (string.Equals(e.purchasedProduct.definition.id, product[1], StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("moneyy", PlayerPrefs.GetInt("moneyy") + 5000);
            managerr.CoinTextUpdate();
            return PurchaseProcessingResult.Complete;
        }

        else if (string.Equals(e.purchasedProduct.definition.id, product[2], StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("moneyy", PlayerPrefs.GetInt("moneyy") + 10000);
            managerr.CoinTextUpdate();
            return PurchaseProcessingResult.Complete;
        }

        else if (string.Equals(e.purchasedProduct.definition.id, product[3], StringComparison.Ordinal))
        {
            if (PlayerPrefs.HasKey("NoAds") == true)
            {
                PlayerPrefs.SetInt("NoAds", 1);
                managerr.NoAdsRemove();
            }
            return PurchaseProcessingResult.Complete;
        }

        else
        {
           return PurchaseProcessingResult.Pending;
        }
    }


    public void IAPButton (string id)
    {
        Product product = controller.products.WithID(id);
        if(product !=null && product.availableToPurchase)
        {
            controller.InitiatePurchase(product);
            Debug.Log("Buying");
        }
        else
        {
            Debug.Log("Not Buying");
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }
}
