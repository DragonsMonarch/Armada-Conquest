using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // screen dimensions
    private int screenWidth;
    private int screenHeight;
    //speed of camera
    private float speed = 100.0f;
    //rotation speed
    private float rotspeed = 7.0f;
    //zoomspeed
    private float Zoomspeed = 20.0f;


    //buttons to move the camera / / for parametrs
    private string forwardButton = "w";
    private string leftButton = "a";
    private string rightButton = "d";
    private string backButton = "s";
    // Start is called before the first frame update
    void Start()
    {   
        //get screen dimensions
        screenHeight = Screen.height;
        screenWidth = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        // get position of camera
        Vector3 camPos = Camera.main.transform.position;
        // zoomCamera
        if (Input.GetAxis("Mouse ScrollWheel") < 0.0f && camPos.y < 400)
        {
            // move up camera
            Camera.main.transform.Translate(Vector3.up * Zoomspeed, Space.World);
            //move the camera's platform back
            transform.Translate(Vector3.back * Zoomspeed);

        }
            
        else if (Input.GetAxis("Mouse ScrollWheel") > 0.0f && camPos.y > 100)
        {
            // move down camera
            Camera.main.transform.Translate(Vector3.down * Zoomspeed, Space.World);
            //move the camera's platform forward
            transform.Translate(Vector3.forward * Zoomspeed);
        }
        


        // move from mouse postion
        if (Input.GetMouseButton(2)){
            float rotationV = Input.GetAxis("Mouse Y") * rotspeed * -1;
            float rotationH = Input.GetAxis("Mouse X") * rotspeed ;
            transform.Rotate(0, rotationH, 0, Space.World);
            Camera.main.transform.Rotate(rotationV, 0, 0);
        }
        // management of camera
        if(Input.GetKey(leftButton))
        {
            transform.Translate(Vector3.left * Time.deltaTime* speed);
        }
        else if (Input.GetKey(rightButton))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else if (Input.GetKey(backButton))
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        else if (Input.GetKey(forwardButton))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }

}

