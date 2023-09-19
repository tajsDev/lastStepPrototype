using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{

    public GameObject hook ; 
    public LineRenderer chains;
    public GameObject grappleGun; 
    CharacterController player;
    public float speed = 0.1f;
    Vector3 hookPos;
    Vector3 playerPos;
    Vector3 grapplePos;
    RaycastHit hit;

    bool isGrappled; 
    // Start is called before the first frame update
    void Awake()
    {
        chains = GetComponent<LineRenderer>();
        chains.enabled = false;
        player = GetComponent<CharacterController>();
        player.enabled = true;
        isGrappled = false;
    }

    // Update is called once per frame
    void Update()
    {

            if(Input.GetButtonDown("Fire1") && !isGrappled ) {

                grapplePos = grappleGun.transform.position;
                if(Physics.Raycast(grapplePos,transform.forward, out hit) ) {
                if(hit.transform.gameObject.CompareTag("Wall") ){
                    hook.transform.position = hit.point;
                    hookPos = hook.transform.position;
                    isGrappled = true;

                }
                }
        }
        if(isGrappled) {
            hook.transform.position = hookPos;
            playerPos = player.transform.position;
            player.enabled = false; 
            grapplePos = grappleGun.transform.position;
            chains.enabled = true;
            chains.SetPosition(0,grapplePos);
            chains.SetPosition(1,hookPos);
            transform.position = Vector3.MoveTowards(playerPos,hookPos,speed);
            float distance = Vector3.Distance(playerPos,hookPos);
            if(distance <= 0 )
            {
                isGrappled = false; 
            }


        }
        else
        {
            hook.transform.position = grappleGun.transform.position; 
            chains.enabled = false;
            player.enabled = true; 


        }

    }
}
