using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheepocalypse : MonoBehaviour
{
    public GameObject SheepObj;
    private Vector2 spawnerPos;
    private float gensize;
    public float timeDelay;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        SheepObj = Resources.Load("Prefab/ScuffedSheep") as GameObject;
        time = 29f;
        timeDelay = 10f;
        float genx = Random.Range(-10f, 10f);
        float geny = Random.Range(-10f, 10f);
        Vector2 temp = new Vector2(genx, geny);
        this.transform.position = temp;
        spawnerPos = this.transform.position;
        gensize = Random.Range(0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        time = time + 1f * Time.deltaTime;
        if (time >= timeDelay)
        {
            float x = Random.Range(-gensize, gensize);
            float y = Random.Range(-gensize, gensize);
            var growGrass = Instantiate(SheepObj, new Vector3(x + spawnerPos.x, y + spawnerPos.y, 0), Quaternion.identity);
            time = 0;
        }
    }
}
