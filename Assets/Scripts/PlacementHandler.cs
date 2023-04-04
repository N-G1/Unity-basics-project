using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ObjectHover;

public class PlacementHandler : MonoBehaviour
{
    [SerializeField] private GameObject Destination, Placement;
    [HideInInspector] public int scoreCount = 0;
    private GameObject objInHand;
    public Text prompt;
    private Transform specificPlacement;
    public ObjectHover check;
    private string child;
    private bool placed;

    //no interface text currently implemented 

    void Update()
    {
        if (prompt.text != "Placeholder") 
        {
            prompt.enabled = false;
        }

        var layerMask = LayerMask.GetMask("Placement");
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10, layerMask))
        {
            if (Destination.transform.childCount != 0)
            {
                placed = false;
                child = Destination.transform.GetChild(0).name;
                if (child.Substring(0, 8) == "sandwich")
                //if (Destination.transform.Find("sandwich") != null)
                {
                    //var layerMask = LayerMask.GetMask("Placement");
                    //var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
                    //RaycastHit hit;
                    //if (Physics.Raycast(ray, out hit, 10, layerMask))
                    {
                        prompt.enabled = true;
                        prompt.text = "Press E to interract"; // needs to fade in and out
                        if (Input.GetButtonDown("E"))
                        {
                            prompt.enabled = false;
                            prompt.text = "Placeholder";
                            objInHand = Destination.transform.GetChild(0).gameObject;
                            //bool placed = false;
                            check.keyCheck = false;

                            {
                                while (placed == false)
                                {
                                    for (int i = 0; i <= Placement.transform.childCount - 1; i++)
                                    {
                                        specificPlacement = Placement.transform.GetChild(i);
                                        if (specificPlacement.transform.childCount == 0)
                                        {
                                            objInHand.transform.parent = specificPlacement;             //
                                            objInHand.transform.position = specificPlacement.position;  // rotation bug needs fixing here 
                                            objInHand.layer = 6;                                        //
                                            placed = true;                                              //

                                            //text for telling the user they incremented the score 

                                            //needs to be stored elswhere, currently isnt 
                                            scoreCount += 1;
                                        }

                                    }
                                }
                            }

                        }
                    }
                }
                //else if (Input.GetButtonDown("E") && )
                //{
                //    prompt.enabled = true;
                //    prompt.text = "You need to steal sandwiches and place them here";       // declare raycast outside of the if statement or this will do absolutely nothing 
                //    Invoke("disableText", 3f);                                                    // dont invoke the disable text function in the update function, pretty sure i dont even need this line 
                //}
            }
        }


    }

    void disableText()
    {
        prompt.enabled = false;
    }
}
