using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int deltaMultiplayer=5;
    [SerializeField] int camWavingMultiplayer=3;
    [SerializeField] float angleWave=4;
    [Tooltip("This Var always should be lower than [Angle Wave] (e.g AW=4 AWCO=3.9f) because it's prevent from errors and  looks better")]
    [SerializeField] float angleWaveCutOut=3.6f;
    [SerializeField] bool isDec;

    [SerializeField] float maxDistanceBetweenCoursorAndPlayer ;

    [SerializeField]GameObject camera;
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y+1 ), Time.deltaTime*deltaMultiplayer);
            CameraWaving();
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y-1 ), Time.deltaTime * deltaMultiplayer);
            CameraWaving();
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x-1 , transform.position.y ), Time.deltaTime * deltaMultiplayer);
            CameraWaving();
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x+1 , transform.position.y ), Time.deltaTime * deltaMultiplayer);
            CameraWaving();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            CameraOnBounds();
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            CameraOutOfBounds();
        }
    }

    private void CameraOutOfBounds()
    {
        camera.transform.position = new Vector3(Vector3.Lerp(transform.position, new Vector3(0, 0,0), Time.deltaTime).x, Vector3.Lerp(transform.position, new Vector3(0, 0, 0), Time.deltaTime).y, -10);

    }
    void CameraOnBounds()
    {
        //calculate coursor pos 
        Vector2 cPos= Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //cheack distance
        Debug.Log(Vector2.Distance(transform.position, cPos));
        if (Vector2.Distance(transform.position, cPos) < maxDistanceBetweenCoursorAndPlayer)
        {
          }
        
     
    }
    void CameraWaving()
    {
        Vector3 angle = camera.transform.eulerAngles;
    
        float z = angle.z;
        if (angle.z > 180)
        {
            z = angle.z - 360f;
        }
        if (z >= angleWaveCutOut)
        {
            isDec = true;
        }
        else if (z <= -angleWaveCutOut)
        {
            isDec = false;
        }
        
        if (!isDec)
        {
            camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, Quaternion.Euler(0, 0, angleWave), Time.deltaTime * camWavingMultiplayer);
        }
        else 
        {
            camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, Quaternion.Euler(0, 0, -angleWave), Time.deltaTime * camWavingMultiplayer);
        } 
    
    }
}
