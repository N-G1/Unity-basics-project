using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVMovementHandler : MonoBehaviour
{
    public Transform player;
    private Animator anim;
    public float maxAngle, maxRadius,MoveSpeed = 4;
    private bool isInFOV = false;
    public StunHandler stunCheck;

    void Start()
    {
        anim = GetComponent<Animator>();
        stunCheck = GameObject.Find("Stun Handler").GetComponent<StunHandler>();
    }
    void Update()
    {
        if(stunCheck.stun == false)
        {
            isInFOV = inFOV(transform, player, maxAngle, maxRadius);

            if (isInFOV)
            {
                transform.LookAt(player);
                anim.Play("Placeholder Walk Cycle");
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            }
            else
                anim.Play("Placeholder Idle Animation");
        }
        else
        {
            anim.Play("Placeholder Idle Animation");
            StartCoroutine(StunEnd());
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);

        if (!isInFOV)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;

        
        Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * maxRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * maxRadius);
    }

    public static bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
    {
        var layerMask = LayerMask.GetMask("Player");
        Collider[] overlaps = new Collider[30];
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps, layerMask);

        for (int i = 0; i < count + 1; i++)
        {
            if (overlaps[i] != null)
            {
                if (overlaps[i].transform == target)
                {
                    Vector3 dirBetween = (target.position - checkingObject.position).normalized;
                    //dirBetween.y *= 0; //not sure if this is needed just yet will have to test

                    float angle = Vector3.Angle(checkingObject.forward, dirBetween);

                    if (angle <= maxAngle)
                    {
                        Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, maxRadius))
                        {
                            if (hit.transform == target)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }

        return false;
    }
    private IEnumerator StunEnd()
    {
        yield return new WaitForSeconds(2f);
        stunCheck.stun = false;
    }

}
