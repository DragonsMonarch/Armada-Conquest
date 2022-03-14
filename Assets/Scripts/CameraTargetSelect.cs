using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetSelect : MonoBehaviour
{
    private Vector3 targetPosition;
    //speeds
    [SerializeField]
    float boost;
    [SerializeField]
    float speed;
    [SerializeField]
    float  rotspeed;
    [SerializeField]
    List<GameObject> ships;
    private float rotmber = 0;

    //distances
    private float distance;
    private float nowdistance;
    //checkers
    private bool needMoving = false;
    private bool call = false;
    private bool inrotate = false;
    private bool movingwas = false;
    // Start is called before the first frame update
    void Start()
    {
        SelectObjects.newListUnit += NewListUnitReceiver;
    }
    // get permission for get target to unit
    void NewListUnitReceiver(List<GameObject> units)
    {        // if we have list with > 00 units
        if (units.Count > 0){
            //we check have we tha unit in list
            foreach (GameObject unit in units){
                if (unit == this.gameObject){
                   call = true;
                }
                else {
                    call = false;
                }
            }
        }
        else{
            call = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (call && !inrotate){
            GetTarget();
        }
        if (needMoving){
            //!Mathf.Approximately(transform.position.magnitude, targetPosition.magnitude)
            Debug.Log(nowdistance);
            if (nowdistance >= 5){
                MovementShipToFar();
            }
            else{
                rotmber = 0.0f;
                needMoving = false;
                inrotate = false;
            }
        }
        if (movingwas && !inrotate){
            rotToNormal();
        }
        
        
    }

    // get target at the map
    void GetTarget(){
        //when mouse buttton is click
        if (Input.GetMouseButtonDown(1)){
            Ray ray;
            RaycastHit hit;
            // shot ray
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // get pos
            if (Physics.Raycast(ray, out hit)){
                targetPosition = hit.point;
            }
            //getdistance
            distance = Vector3.Distance(targetPosition, transform.position);
            //for trigger
            nowdistance = Vector3.Distance(targetPosition, transform.position);
            //need moving
            needMoving = true;
        }
    }
    // void CheckVisible(){
    //     Vector3 fwd = transform.TransformDirection(Vector3.forward);
    //     if (Physics.Raycast(transform.position, fwd, 20)){
    //         transform.Translate(Vector3.up);
    //     }
    // }

    void MovementShipToFar(){
        nowdistance = Vector3.Distance(targetPosition, transform.position);
        rotateToTarget();
        if (distance / 3 * 2 > nowdistance && distance / 3 > nowdistance && distance > 1500){
            inrotate = true;
            movingwas = true;
            rotToStop();
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // rotate on target
    void rotateToTarget(){
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotspeed * Time.deltaTime);
    }
    // rotate ship 180 degrees to stop it
    void rotToStop(){
        foreach (GameObject ship in ships){
            if (rotmber < 180){
                //get position and - rot speed
                Quaternion rotation = Quaternion.Euler(0, ship.transform.rotation.eulerAngles.y - 0.1f , 0);
                //set rotation position
                ship.transform.rotation = rotation;
            }
        }
        rotmber += 0.1f;
    }
    void rotToNormal(){
        foreach (GameObject ship in ships){
            if (rotmber < 180){
                //get position and - rot speed
                Quaternion rotation = Quaternion.Euler(0, ship.transform.rotation.eulerAngles.y - 0.1f , 0);
                //set rotation position
                ship.transform.rotation = rotation;
            }
            else {
                movingwas = false;
                rotmber = 0.0f;
            }
        }
        rotmber += 0.1f;
    }
}