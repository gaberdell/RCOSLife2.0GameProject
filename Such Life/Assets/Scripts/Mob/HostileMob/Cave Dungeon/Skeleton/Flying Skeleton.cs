using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSkeleton : BaseSkeleton
{
    // Adjust specific properties for the flying skeleton
    public float flyingSpeed = 5.0f; // Speed of the flying skeleton
    public float hoverHeight = 10.0f; // Height at which the skeleton hovers

    // Optional: Add any flying-related variables or behaviors unique to the flying skeleton

    protected override void Start()
    {
        // Call the base class's Start method
        base.Start();

        // Additional initialization specific to the flying skeleton
        currMaxSpeed = flyingSpeed;
    }

    protected override void Update()
    {
        // Call the base class's Update method
        base.Update();

        // Implement flying skeleton behavior (e.g., hovering at a certain height)
        HoverAtHeight();
    }

    // Implement the hover behavior
    void HoverAtHeight()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = hoverHeight;
        transform.position = newPosition;
    }
}

