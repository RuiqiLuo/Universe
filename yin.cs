using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustableGravitySimulation : MonoBehaviour
{
    public float G; // 万有引力常数  
    public float n; // r的幂次，可以调节这个值来观察不同n下的运动轨迹  
    private GameObject[] planets;
    private Rigidbody[] planetRigidbodies;

    void Start()
    {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        planetRigidbodies = new Rigidbody[planets.Length];

        for (int i = 0; i < planets.Length; i++)
        {
            planetRigidbodies[i] = planets[i].GetComponent<Rigidbody>();
        }
    }

    void FixedUpdate()
    {
        foreach (Rigidbody rb in planetRigidbodies)
        {
            Vector3 totalForce = Vector3.zero;

            foreach (Rigidbody otherRb in planetRigidbodies)
            {
                if (rb == otherRb) continue;

                Vector3 direction = otherRb.position - rb.position;
                float distance = direction.magnitude;
                direction.Normalize();

                // 使用Mathf.Pow来计算r的n次方  
                float forceMagnitude = G * rb.mass * otherRb.mass / Mathf.Pow(distance, n);
                totalForce += direction * forceMagnitude;
            }

            rb.AddForce(totalForce);
        }
    }
}