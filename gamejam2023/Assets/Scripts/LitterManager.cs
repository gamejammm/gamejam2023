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

    void Start() {
        map = GetComponent<Map>();
        currentCooldown = cooldownInS;
    }

    // Update is called once per frame
    void Update()
    {
        var deltaTime = Time.deltaTime;

        currentCooldown -= deltaTime;

        if (currentCooldown < 0) {
            DropLitter();
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
}
