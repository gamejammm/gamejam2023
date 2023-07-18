using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromotionManager : MonoBehaviour
{

    public int promotionDurationInS;
    public int parallelPromotions;

    public int cooldownInS;

    public List<float> discounts;

    private float currentCooldown;

    private List<Promotion> runningPromotions;

    private Map map;

    void Start() {
        //map = GetComponent<Map>();
        //currentCooldown = cooldownInS;
        //runningPromotions = new List<Promotion>();
    }

    // Update is called once per frame
    void Update()
    {
        //var deltaTime = Time.deltaTime;

        //currentCooldown -= deltaTime;
        //var index = 0;
        //while (index < runningPromotions.Count)
        //{
        //    var promo = runningPromotions[index];
        //    promo.validUntil -= deltaTime;
        //    if (promo.validUntil < 0) {
        //        promo.item.SetDiscount(0f);
        //        runningPromotions.RemoveAt(index);      
        //        print("promotion ended"); 
        //    } else {
        //        index += 1;
        //    }
        //}

        //if (currentCooldown < 0 && runningPromotions.Count < parallelPromotions) {
        //    StartPromotion();
        //    currentCooldown = cooldownInS;
        //}
    }

    private void StartPromotion() {
        Shelf shelf = map.GetRandomFilledShelf();

        if (shelf == null) {
            return;
        }

        float discount = discounts[Random.Range(0, discounts.Count)];

        Item item = (Item) shelf.shelfItem;
        item.SetDiscount(discount);

        runningPromotions.Add(new Promotion(item, promotionDurationInS));

        print("new promotion");
    }
}
