using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySimulation : MonoBehaviour
{
    private GameObject[] planets;
    private Rigidbody[] planetRigidbodies;
    private ConstantForce[] planetForces; // ��������һ����Ч�����  
    public float G; // ��������������ͨ��ʹ��Сдg��ʾ�������ٶȣ�����ʹ�ô�дG��ʾ������������  

    void Start()
    {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        planetRigidbodies = new Rigidbody[planets.Length];
        planetForces = new ConstantForce[planets.Length];

        for (int i = 0; i < planets.Length; i++)
        {
            planetRigidbodies[i] = planets[i].GetComponent<Rigidbody>();
            planetForces[i] = planets[i].GetComponent<ConstantForce>(); // ȷ��ÿ�����Ƕ���������  
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
                if (distanceSq == 0f) continue; // ���������  

                float forceMagnitude = G * planetRigidbodies[i].mass * planetRigidbodies[j].mass / distanceSq;
                gravityForce += direction.normalized * forceMagnitude;
            }

            planetForces[i].force = gravityForce; // Ӧ���ܵ�������ÿ������  
        }
    }
}