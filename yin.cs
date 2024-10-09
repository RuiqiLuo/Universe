using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustableGravitySimulation : MonoBehaviour
{
    public float G; // ������������  
    public float n; // r���ݴΣ����Ե������ֵ���۲첻ͬn�µ��˶��켣  
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

                // ʹ��Mathf.Pow������r��n�η�  
                float forceMagnitude = G * rb.mass * otherRb.mass / Mathf.Pow(distance, n);
                totalForce += direction * forceMagnitude;
            }

            rb.AddForce(totalForce);
        }
    }
}