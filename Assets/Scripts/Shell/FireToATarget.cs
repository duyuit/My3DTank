using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireToATarget : MonoBehaviour
{
    float initialAngle = 45;
 
    public void Fire(Vector3 targetPosition)
    {
        var rigid = GetComponent<Rigidbody>();

        Vector3 p = targetPosition;

        float gravity = Physics.gravity.magnitude;
        // Selected angle in radians
        float angle = initialAngle * Mathf.Deg2Rad;

        // Positions of this object and the target on the same plane
        Vector3 planarTarget = new Vector3(p.x, 0, p.z);
        Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

        // Planar distance between objects
        float distance = Vector3.Distance(planarTarget, planarPostion);
        // Distance along the y axis between objects
        float yOffset = transform.position.y - p.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));
        // To (added "* (p.x > transform.position.x ? 1 : -1)")
        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion) * (p.x > transform.position.x ? 1 : -1);
        // Rotate our velocity to match the direction between the two objects
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        // Fire!
        rigid.velocity = finalVelocity;
        //rigid.AddForce(finalVelocity * rigid.mass, ForceMode.Impulse);

    }
    void Start () {
      
 
        // Alternative way:
        // rigid.AddForce(finalVelocity * rigid.mass, ForceMode.Impulse);
    }
}
