using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop : MonoBehaviour
{
    private float distance;
    private float timeToWall;

    private void Update()
    {
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z - (Time.deltaTime / timeToWall * distance));
    }

    internal void InitializeTroop(float distance, float timeToWall)
    {
        this.distance = distance;
        this.timeToWall = timeToWall;

        // activate walking animation
    }
}
