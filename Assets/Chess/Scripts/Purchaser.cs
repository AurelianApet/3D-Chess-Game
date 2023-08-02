using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour, IStoreListener
{

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;
    public const string skuThreeForCom = "com.threecom.formation";
    public const string skuThreeForPlayer = "com.threeplayer.formation";
    public const string skuCrossForCom = "com.crosscom.formation";
    public const string skuCrossForPlayer = "com.crossplayer.formation";
    public const string skuAllUnLock = "com.unlockall.formation";


    // Use this for initialization
    void Start()
    {
        if (m_StoreController == null)
        {
            InitialzePurchasing();
        }
    }

    void InitialzePurchasing()
    {
        if (IsInitialized())
            return;
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(skuThreeForCom, ProductType.NonConsumable);
        builder.AddProduct(skuThreeForPlayer, ProductType.NonConsumable);
        builder.AddProduct(skuCrossForCom, ProductType.NonConsumable);
        builder.AddProduct(skuCrossForPlayer, ProductType.NonConsumable);
        builder.AddProduct(skuAllUnLock, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyNonConsumable(string pid)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(pid);//com.kd.allformations
            if (product != null && product.availableToPurchase)
            {
                Debug.Log("---Starting Purchase : " + pid);
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }
    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) =>
            {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
        ProductMetadata product = m_StoreController.products.WithID(skuThreeForCom).metadata;
        Debug.Log("************************Price of the products "+product.localizedPriceString);
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, skuThreeForCom, StringComparison.Ordinal))
        {
            Debug.Log("All formations in app purchase successful ");
            //if(HomeUIControllerScript.instance != null && HomeUIControllerScript.instance.messagePopup != null){
            //    HomeUIControllerScript.instance.ShowMessagePopup("All formations purchase successful"); 
            //}
            comunication.info.Save_Purchses(1, 1);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, skuThreeForPlayer, StringComparison.Ordinal))
        {
            //if (HomeUIControllerScript.instance != null && HomeUIControllerScript.instance.messagePopup != null)
            //{
            //    HomeUIControllerScript.instance.ShowMessagePopup("All formations Player to Player One Device purchase successful");
            //}
            Debug.Log("PlayerVsPlayer Same Device in app purchase successful ");
            comunication.info.Save_Purchses(2, 1);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, skuCrossForCom, StringComparison.Ordinal))
        {
            Debug.Log("PlayerVsPlayer Online in app purchase successful ");
            //if (HomeUIControllerScript.instance != null && HomeUIControllerScript.instance.messagePopup != null)
            //{
            //    HomeUIControllerScript.instance.ShowMessagePopup("All formations Player to Player Online purchase successful");
            //}
            comunication.info.Save_Purchses(3, 1);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, skuCrossForPlayer, StringComparison.Ordinal))
        {
            Debug.Log("PlayerVsPlayer in app purchase successful ");
            //if (HomeUIControllerScript.instance != null && HomeUIControllerScript.instance.messagePopup != null)
            //{
            //    HomeUIControllerScript.instance.ShowMessagePopup("Purchase successful");
            //}
            comunication.info.Save_Purchses(4, 1);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, skuAllUnLock, StringComparison.Ordinal))
        {
            Debug.Log("all k fortmations in app purchase successful ");
            //if (HomeUIControllerScript.instance != null && HomeUIControllerScript.instance.messagePopup != null)
            //{
            //    HomeUIControllerScript.instance.ShowMessagePopup("All Cross formations vs computer purchase successful");
            //}
            comunication.info.Save_Purchses(5, 1);
        }
        else
        {
            Debug.Log("Purchase Failed");
        }
        return PurchaseProcessingResult.Complete;
    }

}
