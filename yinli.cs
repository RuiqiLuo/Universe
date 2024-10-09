using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySimulation : MonoBehaviour
{
    private GameObject[] planets;
    private Rigidbody[] planetRigidbodies;
    private ConstantForce[] planetForces; // 假设这是一个有效的组件  
    public float G; // 万有引力常数，通常使用小写g表示重力加速度，这里使用大写G表示万有引力常数  

    void Start()
    {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        planetRigidbodies = new Rigidbody[planets.Length];
        planetForces = new ConstantForce[planets.Length];

        for (int i = 0; i < planets.Length; i++)
        {
            planetRigidbodies[i] = planets[i].GetComponent<Rigidbody>();
            planetForces[i] = planets[i].GetComponent<ConstantForce>(); // 确保每个行星都有这个组件  
        }
    }

    void Update()
    {
        for (int i = 0; i < planets.Length; i++)
        {
            Vector3 gravityForce = Vector3.zero;
            for (int j = 0; j < planets.Length; j++)
            {
                if (i == j) continue;

                Vector3 direction = planets[j].transform.position - planets[i].transform.position;
                float distanceSq = direction.sqrMagnitude;
                if (distanceSq == 0f) continue; // 避免除以零  

                float forceMagnitude = G * planetRigidbodies[i].mass * planetRigidbodies[j].mass / distanceSq;
                gravityForce += direction.normalized * forceMagnitude;
            }

            planetForces[i].force = gravityForce; // 应用总的引力到每个行星  
        }
    }
}