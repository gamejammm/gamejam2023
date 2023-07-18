using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitterManager : MonoBehaviour
{

    public int cooldownInS;

    public float litterZ;

    public GameObject litterPrefab;

    private float currentCooldown;

    private Map map;

    private GameManager manager;

    public GameObject BoundsToSpawnLitter;

    private Bounds spawnignBounds;

    void Start() {

        manager = GetComponent<GameManager>();

        if(manager.GenerateMap)
            map = GetComponent<Map>();
        currentCooldown = cooldownInS;
        spawnignBounds  = BoundsToSpawnLitter.GetComponent<Collider>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        var deltaTime = Time.deltaTime;

        currentCooldown -= deltaTime;

        if (currentCooldown < 0) {
            //DropLitter();
            DropLitter2();
            currentCooldown = cooldownInS;
        }
    }

    private void DropLitter() {
        var optCoords = map.GetRandomFreeFloor();

        if (optCoords == null) {
            return;
        }

        Vector2Int coords = (Vector2Int) optCoords;
        GameObject litter = Instantiate(litterPrefab, new Vector3(coords.x, litterZ, coords.y), Quaternion.identity);
    }

    private void DropLitter2()
    {
        Vector3 litterSpawnPoint = RandomPointInBounds(spawnignBounds);
        GameObject litter = Instantiate(litterPrefab, litterSpawnPoint, Quaternion.identity);

    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
