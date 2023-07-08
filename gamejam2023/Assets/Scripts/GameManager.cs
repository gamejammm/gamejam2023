using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Map ShopMap;

    public Player ShopPlayer;

    // Start is called before the first frame update
    void Start()
    {
        ShopMap = this.GetComponent<Map>();
        if(ShopMap.initialized == true) 
        {
            SetPlayer();
        }

        else
        {
            ShopMap.isInitializationDone.AddListener(SetPlayer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetPlayer()
    {
        ShopPlayer.transform.position = ShopMap.GetEntrance();
        Debug.LogError(ShopPlayer.transform.position);
    }
}
