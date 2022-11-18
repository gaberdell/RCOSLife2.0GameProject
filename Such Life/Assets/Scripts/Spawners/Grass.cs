using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : SpawnerBase
{
    // Start is called before the first frame update
    void Start()
    {
        SpawnObj = Resources.Load("Prefab/Grass") as GameObject;
        time = 29f;
        timeDelay = 3f;
        float genx = Random.Range(-5f, 5f);
        float geny = Random.Range(-5f, 5f);
        Vector2 temp = new Vector2(genx, geny);
        this.transform.position = temp;
        spawnerPos = this.transform.position;
        spawnsize = Random.Range(0f, 5f);
        type.Add("land");
    }



}
