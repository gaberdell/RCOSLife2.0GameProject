using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfPackManager : MonoBehaviour
{
    public GameObject wolfPrefab;
    public int numWolves = 3;
    public Wolf[] allWolves;
    public Vector2 roamLimits = new Vector2(2, 2);
    public float time = 0f;
    public float timeDelay = 1f;
    public Vector2 packLocation;

    // Start is called before the first frame update
    void Start()
    {
        packLocation = Vector2.zero;

        allWolves = new Wolf[numWolves];
        for (int i = 0; i < numWolves; i++) {
            Vector2 pos = (Vector2)this.transform.position + new Vector2(
                Random.Range(-roamLimits.x, roamLimits.x),
                Random.Range(-roamLimits.y, roamLimits.y)
            );
            
            GameObject go = Instantiate(wolfPrefab, pos, Quaternion.identity);
            allWolves[i] = go.GetComponent<Wolf>();
            // Debug.Log("here1");
            // while (allWolves[i].dc.IsTouchingLayers()) {
            //     Debug.Log("here2");
            //     pos = (Vector2)this.transform.position + new Vector2(
            //         Random.Range(-roamLimits.x, roamLimits.x),
            //         Random.Range(-roamLimits.y, roamLimits.y)
            //     );
            //     allWolves[i].teleportTo(pos);
            // }
        }
    }

    // Update is called once per frame
    void Update()
    {
        time = time + 1f * Time.deltaTime;

        // 10% chance to override the pack location and generate a new one
        int gen = Random.Range(0, 100);
        if (gen < 10) {
            packLocation = Vector2.zero;
        }

        // Update once every second
        if (time >= timeDelay) {
            time = 0f;
            foreach (var wolf in allWolves) {
                //If the wolf is idling, it has a 30% chance to start wandering
                // Debug.Log(wolf.currState);
                if (wolf.currState == AnimalBase.State.Idling) {
                    gen = Random.Range(0, 100);
                    // Debug.Log(gen);
                    if (gen < 30) {
                        wolf.Walk();
                    }
                }

                //If the wolf is wandering, it has a 10% chance of stopping.
                if (wolf.currState == AnimalBase.State.Walking) {
                    // Move to a random location near the location of the pack,
                    // or generate a new one
                    if (packLocation.Equals(Vector2.zero)) {
                        // Debug.Log("a");
                        wolf.PositionChange();
                        packLocation = wolf.newposition;
                    } else {
                        // Debug.Log("b");
                        wolf.PositionChange(packLocation);
                    }
                    gen = Random.Range(0, 100);
                    if (gen < 10) {
                        wolf.Idle();
                    }
                }
            }
            
        }
        // TODO: replace with event
        // bool attacked = false;
        // foreach (var wolf in allWolves) {
        //     if (wolf.currHealth < wolf.currMaxHealth) {
        //         attacked = true;
        //     }
        // }

        // if (attacked) {
        //     foreach (var wolf in allWolves) {
        //         wolf.currState = wolf.State.Following;
        //     }
        // }
    }
}
