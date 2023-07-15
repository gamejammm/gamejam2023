using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Map ShopMap;

    public Player ShopPlayer;

    public float StartMoney = 50f;

    private float currentMonetas;

    private Inventory inventory;

    private StatusUI statusUI;


    // Start is called before the first frame update
    void Start()
    {
        inventory = this.GetComponent<Inventory>();
        ShopMap = this.GetComponent<Map>();
        statusUI  =FindObjectOfType<StatusUI>();

        if (ShopMap.initialized == true) 
        {
            SetPlayer();
        }

        else
        {
            ShopMap.isInitializationDone.AddListener(SetPlayer);
        }

        currentMonetas = StartMoney;
        statusUI.SetMonetasValue(currentMonetas);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ItemCollected(Item item)
    {
        if(SetMonetas(item.Price))
        {
            inventory.NewItemCollected(item);
            statusUI.SetMonetasValue(currentMonetas);
            return true;
        }
        else
        {
            Debug.LogError("Item is Too expensive");
            return false;
        }

    }

    //private void ItemPayedOut(Item item)
    //{
    //    if (SetMonetas(-item.Price))
    //    {
    //        inventory.DropItem(item);
    //        return true;
    //    }
    //}

    private void SetPlayer()
    {
        ShopPlayer.transform.position = ShopMap.GetEntrance();
        Debug.LogError(ShopPlayer.transform.position);
    }

    private bool SetMonetas(float priceToPay)
    {
        if (priceToPay > currentMonetas)
        {
            Debug.LogError("Not enough Money");
            return false;
        }

        else
        {
            currentMonetas -= priceToPay;  
        }
        return true;
    }
}
