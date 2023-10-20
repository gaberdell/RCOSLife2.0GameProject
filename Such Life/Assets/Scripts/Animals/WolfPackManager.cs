using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfPackManager : MonoBehaviour
{
    public GameObject wolfPrefab;
    public int numWolves = 3;
    public GameObject[] allWolves;
    public Vector2 roamLimits = new Vector2(3, 3);

    // Start is called before the first frame update
    void Start()
    {
        allWolves = new GameObject[numWolves];
        for (int i = 0; i < numWolves; i++) {
            Vector2 pos = (Vector2)this.transform.position + new Vector2(
                Random.Range(-roamLimits.x, roamLimits.x),
                Random.Range(-roamLimits.y, roamLimits.y)
            );
            
            allWolves[i] = Instantiate(wolfPrefab, pos, Quaternion.identity);
            // while (allWolves[i].Class.dc.GetContacts().Length > 0) {
            //     Vector2 pos = (Vector2)this.transform.position + new Vector2(
            //         Random.Range(-roamLimits.x, roamLimits.x),
            //         Random.Range(-roamLimits.y, roamLimits.y)
            //     );
            //     allWolves[i].Class.teleportTo(pos);
            // }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: replace with event
        // bool attacked = false;
        // foreach (var wolf in allWolves) {
        //     if (wolf.Class.currHealth < wolf.Class.currMaxHealth) {
        //         attacked = true;
        //     }
        // }

        // if (attacked) {
        //     foreach (var wolf in allWolves) {
        //         wolf.Class.currState = wolf.Class.State.Following;
        //     }
        // }
    }
}
