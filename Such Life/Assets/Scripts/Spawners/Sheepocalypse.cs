using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheepocalypse : SpawnerBase
{
    // Start is called before the first frame update
    void Start()
    {
        SpawnObj = Resources.Load("Prefab/ScuffedSheep") as GameObject;
        time = 29f;
        timeDelay = 10f;
        float genx = Random.Range(-10f, 10f);
        float geny = Random.Range(-10f, 10f);
        Vector2 temp = new Vector2(genx, geny);
        this.transform.position = temp;
        spawnerPos = this.transform.position;
        spawnsize = Random.Range(0f, 5f);
        type.Add("land");
    }


}
