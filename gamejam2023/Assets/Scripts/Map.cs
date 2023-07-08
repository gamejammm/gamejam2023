using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FloorPlan = System.Collections.Generic.Dictionary<UnityEngine.Vector2Int, MapElementType>;
using LitterPlan = System.Collections.Generic.Dictionary<UnityEngine.Vector2Int, Item>;
using ShelfPlan = System.Collections.Generic.Dictionary<UnityEngine.Vector2Int, Shelf>;

public class Map : MonoBehaviour
{
    
    private FloorPlan floorPlan = new FloorPlan();
    private LitterPlan litterPlan = new LitterPlan();

    private List<Vector2Int> floorCoords = new List<Vector2Int>();
    private ShelfPlan shelfPlan = new ShelfPlan();

    
    public void SetFloorPlan(FloorPlan floorPlan, ShelfPlan shelfPlan) {
        this.floorPlan = floorPlan;
        this.shelfPlan = shelfPlan;

        floorCoords = new List<Vector2Int>();

        foreach (var item in floorPlan)
        {
            if (item.Value == MapElementType.Floor) {
                floorCoords.Add(item.Key);
            }
        }
    }

    public void AddLitterAt(int x, int y, Item item) {
        litterPlan[new Vector2Int(x, y)] = item;
    }

    public void RemoveLitterAt(int x, int y) {
        litterPlan.Remove(new Vector2Int(x, y));
    }

    public Vector2Int GetRandomFreeFloor() {
        var coords = floorCoords[Random.Range(0, floorCoords.Count - 1)];

        while (litterPlan.ContainsKey(coords)) {
            coords = floorCoords[Random.Range(0, floorCoords.Count - 1)];
        }

        return coords;
    }

    public Shelf GetRandomFilledShelf() {
        List<Vector2Int> keyList = new List<Vector2Int>(shelfPlan.Keys);
        var coords = keyList[Random.Range(0, keyList.Count - 1)];

        while (shelfPlan[coords].shelfItem == null) {
            coords = keyList[Random.Range(0, keyList.Count - 1)];
        }

        return shelfPlan[coords];
    }

}
