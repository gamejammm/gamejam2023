using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using FloorPlan = System.Collections.Generic.Dictionary<UnityEngine.Vector2Int, MapElementType>;
using LitterPlan = System.Collections.Generic.Dictionary<UnityEngine.Vector2Int, Item>;
using ShelfPlan = System.Collections.Generic.Dictionary<UnityEngine.Vector2Int, Shelf>;

public class Map : MonoBehaviour
{
    
    public UnityEvent isInitializationDone;

    public bool initialized = false;

    private FloorPlan floorPlan = new FloorPlan();
    private LitterPlan litterPlan = new LitterPlan();

    private List<Vector2Int> floorCoords = new List<Vector2Int>();
    private ShelfPlan shelfPlan = new ShelfPlan();
    private Vector3 entrance = new Vector3();

    
    public void Setup(FloorPlan floorPlan, ShelfPlan shelfPlan, Vector3 entrance) {
        this.floorPlan = floorPlan;
        this.shelfPlan = shelfPlan;
        this.entrance = entrance;

        floorCoords = new List<Vector2Int>();

        foreach (var item in floorPlan)
        {
            if (item.Value == MapElementType.Floor) {
                floorCoords.Add(item.Key);
            }
        }

        initialized = true;
        isInitializationDone?.Invoke();
    }

    public Vector3 GetEntrance() {
        return entrance;
    }

    public void AddLitterAt(int x, int y, Item item) {
        litterPlan[new Vector2Int(x, y)] = item;
    }

    public void RemoveLitterAt(int x, int y) {
        litterPlan.Remove(new Vector2Int(x, y));
    }

    public Vector2Int? GetRandomFreeFloor() {
        var coords = floorCoords[Random.Range(0, floorCoords.Count - 1)];

        int maxRetries = 10;
        while (litterPlan.ContainsKey(coords) && maxRetries > 0) {
            coords = floorCoords[Random.Range(0, floorCoords.Count - 1)];
            maxRetries -= 1;
        }

        if (!litterPlan.ContainsKey(coords)) {
            return coords;
        } else {
            return null;
        }
    }

    #nullable enable
    public Shelf? GetRandomFilledShelf() {
        List<Vector2Int> keyList = new List<Vector2Int>(shelfPlan.Keys);
        var coords = keyList[Random.Range(0, keyList.Count - 1)];

        int maxRetries = 10;
        while (shelfPlan[coords].shelfItem == null && maxRetries > 0) {
            coords = keyList[Random.Range(0, keyList.Count - 1)];
            maxRetries -= 1;
        }

        if (shelfPlan[coords].shelfItem != null) {
            return shelfPlan[coords];
        } else {
            return null;
        }
    }
    #nullable disable

}
