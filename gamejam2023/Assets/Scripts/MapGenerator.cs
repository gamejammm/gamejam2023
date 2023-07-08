using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FloorPlan = System.Collections.Generic.Dictionary<UnityEngine.Vector2Int, MapElementType>;
using ShelfPlan = System.Collections.Generic.Dictionary<UnityEngine.Vector2Int, Shelf>;

public class MapGenerator : MonoBehaviour
{

    public Vector2Int mapSize;

    public GameObject shelfPrefab;
    public GameObject wallStraightPrefab;
    public GameObject wallInnerCornerPrefab;
    public GameObject wallOuterCornerPrefab;
    public GameObject entrancePrefab;
    public GameObject depositMachinePrefab;
    public GameObject floorPrefab;

    // string format: top-right-bottom-left
    private Dictionary<string, (GameObject, int)> wallPrefabByNeighborhood;
    // Start is called before the first frame update
    void Start()
    {
        wallPrefabByNeighborhood = new Dictionary<string, (GameObject, int)>(){
            { "0111", (wallStraightPrefab, 90)},
            { "1011", (wallStraightPrefab, 0)},
            { "1101", (wallStraightPrefab, 270)},
            { "1110", (wallStraightPrefab, 180)},
            { "0011", (wallInnerCornerPrefab, 90)},
            { "1001", (wallInnerCornerPrefab, 0)},
            { "1100", (wallInnerCornerPrefab, 270)},
            { "0110", (wallInnerCornerPrefab, 180)},
        };

        GenerateMap(mapSize.x, mapSize.y);
    }

    void GenerateMap(int width, int height) 
    {
        FloorPlan floorPlan = GenerateFloorPlan(width, height);
        ShelfPlan shelfPlan = new ShelfPlan();

        for (int x = 0; x < width; x++)
        {
        
            for (int z = 0; z < height; z++)
            {
                Vector2Int coords2d = new Vector2Int(x, z);
                Vector3 coords = new Vector3(x, 0, z);

                GameObject prefab = floorPrefab;
                Quaternion rotation = Quaternion.identity;

                switch (floorPlan[coords2d]) {
                    case MapElementType.DepositMachine: 
                        prefab = depositMachinePrefab;
                        break;
                    case MapElementType.Entrance: 
                        prefab = entrancePrefab;
                        break;
                    case MapElementType.Floor: 
                        prefab = floorPrefab;
                        break;
                    case MapElementType.Shelf:
                        prefab = shelfPrefab;
                        break;
                    case MapElementType.Wall:
                        var (p, r) = wallPrefabByNeighborhood[GetWallNeighborhoodAt(x, z, floorPlan)];
                        prefab = p;
                        rotation = Quaternion.Euler(0, r, 0);
                        break;
                }

                GameObject o = Instantiate(prefab, coords, rotation);

                if (floorPlan[coords2d] == MapElementType.Shelf) {
                    shelfPlan[coords2d] = (Shelf) o.GetComponent<Shelf>();
                }
            }
        }

        Map map = (Map) GetComponent<Map>();
        map.SetFloorPlan(floorPlan, shelfPlan);
        
    }

    private FloorPlan GenerateFloorPlan(int width, int height) {
        FloorPlan elements = new FloorPlan();

        int entranceX = Random.Range(2, width - 2);
        int depositX = Random.Range(2, width - 2);

        for (int x = 0; x < width; x++)
        {
        
            for (int z = 0; z < height; z++)
            {
                Vector2Int coords = new Vector2Int(x, z);

                MapElementType elementType = MapElementType.Floor;

                if (x == 0 || x == width - 1 || z == 0 || z == height - 1) {
                    elementType = MapElementType.Wall;
                } else if ((x == 1 || x == width - 2) && z > 0 && z < height - 1) {
                    elementType = MapElementType.Shelf;
                } else if (x > 0 && x < width - 1 && (z == 1 || z == height - 2)) {
                    elementType = MapElementType.Shelf;
                }

                if (x == entranceX && z == height - 1) {
                    elementType = MapElementType.Entrance;
                } else if (x == entranceX && z == height - 2) {
                    elementType = MapElementType.Floor;
                } else if (x == depositX && z == 1) {
                    elementType = MapElementType.DepositMachine;
                }


                elements[coords] = elementType;
            }
        }

        int isleWidth = 2;
        int numIsles = (int) ((height - 4 - isleWidth) / (2f + isleWidth));

        for (int isleIndex = 0; isleIndex < numIsles; isleIndex++)
        {
            int z = 2 + isleWidth + isleIndex * (2 + isleWidth);
            for (int x = (2 + isleWidth); x < width - (2 + isleWidth); x++)
            {
                elements[new Vector2Int(x, z)] = MapElementType.Shelf;                
                elements[new Vector2Int(x, z + 1)] = MapElementType.Shelf;
            }
        }

        return elements;
    }

    private string GetWallNeighborhoodAt(int x, int y, FloorPlan floorplan) {
        string nString = "";
        // top
        if (floorplan.ContainsKey(new Vector2Int(x, y - 1))) {
            nString += "1";
        } else {
            nString += "0";
        }

        // right
        if (floorplan.ContainsKey(new Vector2Int(x + 1, y))) {
            nString += "1";
        } else {
            nString += "0";
        }

        // bottom
        if (floorplan.ContainsKey(new Vector2Int(x, y + 1))) {
            nString += "1";
        } else {
            nString += "0";
        }

        // left
        if (floorplan.ContainsKey(new Vector2Int(x - 1, y))) {
            nString += "1";
        } else {
            nString += "0";
        }

        return nString;
    }

}
