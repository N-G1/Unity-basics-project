using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{

    public Transform Player;
    float MoveSpeed = 4;
    float MaxDist = 1;
    // float MinDist = 2;


    void Start()
    {

    }

    void Update()
    {
        transform.LookAt(Player);

        if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            // if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
            {
                
            }

        }
    }
}