using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnim : MonoBehaviour
{
    [SerializeField]
    float degreeRotation = 4.0f;

    [SerializeField]
    float sizeAmplitude = 0.04f;

    [SerializeField]
    float waveNumberForRotation = 0.2f;

    [SerializeField]
    float waveNumberForSize = 0.1f;

    Vector3 baseScale;

    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0,0, degreeRotation * Mathf.Sin(waveNumberForRotation * Time.time));


        float sizeToAdd = sizeAmplitude * Mathf.Sin(waveNumberForSize * Time.time);

        transform.localScale = new Vector3(baseScale.x + sizeToAdd, baseScale.y + sizeToAdd, baseScale.z);
    }
}
