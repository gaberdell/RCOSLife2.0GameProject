using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is the base of all spawner classes. Things will spawn according to these rules, but things may be added.
public class SpawnerBase : MonoBehaviour
{
    public GameObject SpawnObj;
    protected Vector2 spawnerPos;
    public float spawnsize;
    public float timeDelay;
    protected float time;
    protected List<string> type; //The type of spawner it is, land, water, etc.

    // Update is called once per frame
    void Update()
    {
        time = time + 1f * Time.deltaTime;
        if (time >= timeDelay)
        {
            float x = Random.Range(-spawnsize, spawnsize);
            float y = Random.Range(-spawnsize, spawnsize);
            Instantiate(SpawnObj, new Vector3(x + spawnerPos.x, y + spawnerPos.y, 0), Quaternion.identity);
            time = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //If the spawner spawns in water, it will move. 
    {
        if (!type.Contains("water"))
        {
            if (collision.gameObject.tag == "Water")
            {
                float genx = Random.Range(-5f, 5f);
                float geny = Random.Range(-5f, 5f);
                Vector2 temp = new Vector2(genx, geny);
                this.transform.position = temp;
                spawnerPos = this.transform.position;
            }
        }
    }
}
