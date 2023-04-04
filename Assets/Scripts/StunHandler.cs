using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunHandler : MonoBehaviour
{
    [SerializeField] private GameObject Destination;
    [HideInInspector] public bool stun;
    void Start()
    {
        stun = false;
    }
    void Update()
    {
        //stun = false;
        if (Destination.transform.Find("mesh") != null && Input.GetButtonDown("L") && stun != true)
        {
            var layerMask = LayerMask.GetMask("Enemy");
            var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10, layerMask))
            {
                stun = true;
            }
        }
        else
        {
            return;
        }
    }
}
