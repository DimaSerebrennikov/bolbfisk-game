using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UIElements;

public class IAPpremium : MonoBehaviour, IDetailedStoreListener
{
    [SerializeField] AwardsScript awardsScript;
    [SerializeField] PopUp _popup;
    string id = "get_premium";
    void Awake()
    {
        SetupBuild();
        CheckAlreadyPurchased();
    }
    IStoreController storeController;
    void SetupBuild()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(id, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        print("initialized");
        storeController = controller;
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;
        if (product.definition.id == id)
        {
            Purchase();
        }
        return PurchaseProcessingResult.Complete;
    }
    public void PayAskPurchase() //iap button
    {
        try
        {
            if (!PremiumActivatorScript.ActivationStatus)
                storeController.InitiatePurchase(id);
        }
        catch (Exception ex)
        {
            //_popup.ShowPopup(ex.Message);
        }
    }
    void CheckAlreadyPurchased()
    {
        if (storeController != null)
        {
            var product = storeController.products.WithID(id);
            if (product != null)
            {
                if (product.hasReceipt)
                {
                    Purchase();
                }
            }
        }
    }
    void Purchase()
    {
        PremiumActivatorScript.ActivationStatus = true;
        awardsScript.ShowPremiumForIap();
    }
    public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureDescription failureDescription)
    {
        //_popup.ShowPopup("Connection failed");
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        //_popup.ShowPopup("Connection failed");
    }
    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        //_popup.ShowPopup("Connection failed");
    }
    public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason)
    {
       //_popup.ShowPopup("Connection failed");
    }
}
