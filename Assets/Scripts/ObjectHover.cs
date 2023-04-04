using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHover : MonoBehaviour
{
    private float maxDist = 100f;
    //int layerMask = LayerMask.GetMask("Interractable");
    [SerializeField] private Material temphighlight;
    [SerializeField] private Transform Destination;
    private Material originalMaterial;
    private Transform currentSelection;
    private Collider Collider;
    private Rigidbody RigidBody;
    public bool keyCheck = false;
    private GameObject currentObject;

    void Start()
    {
        //Collider = GetComponent<Collider>();
        //RigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (currentSelection != null && originalMaterial != null)
        {
            var selecRenderer = currentSelection.GetComponent<Renderer>();
            selecRenderer.material = originalMaterial;
            currentSelection = null;
        }

        var layerMask = LayerMask.GetMask("Interractable");
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDist, layerMask))
        {

            var selection = hit.transform;
            var selecRenderer = selection.GetComponent<Renderer>();
            
            if (selecRenderer != null)
            {
                originalMaterial = selecRenderer.material;
                selecRenderer.material = temphighlight;
               

                if (Input.GetButtonDown("E") && keyCheck == false)
                {
                    currentObject = hit.collider.gameObject;
                    Collider = currentObject.GetComponent<Collider>();
                    RigidBody = currentObject.GetComponent<Rigidbody>();
                   
                    keyCheck = true;
                    Collider.enabled = false;
                    RigidBody.useGravity = false;
                    currentObject.transform.position = Destination.position;
                    currentObject.transform.parent = GameObject.Find("Destination").transform;
                }
            }
            currentSelection = selection;
        }

        //implement a check to make sure if the player picks up a key item they cannot drop it 

        if (Input.GetButtonDown("Q") && keyCheck == true)
        {
            keyCheck = false;
            currentObject.transform.parent = null;
            RigidBody.useGravity = true;
            Collider.enabled = true;

            currentObject = null;
            Collider = null;
            RigidBody = null;

        }
    }
}
