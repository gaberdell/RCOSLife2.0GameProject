using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public GameObject GrassObj;
    private Vector2 spawnerPos;
    public float patchsize;
    public float timeDelay;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        GrassObj = Resources.Load("Prefab/Grass") as GameObject;
        time = 29f;
        timeDelay = 3f;
        float genx = Random.Range(-5f, 5f);
        float geny = Random.Range(-5f, 5f);
        Vector2 temp = new Vector2(genx, geny);
        this.transform.position = temp;
        spawnerPos = this.transform.position;
        patchsize = Random.Range(0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        time = time + 1f * Time.deltaTime;
        if (time >= timeDelay)
        {
            float x = Random.Range(-patchsize, patchsize);
            float y = Random.Range(-patchsize, patchsize);
            var growGrass = Instantiate(GrassObj, new Vector3(x + spawnerPos.x, y + spawnerPos.y, 0), Quaternion.identity);
            time = 0;
        }
    }
}
