using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;

    private Vector3 cameraTargetPos = new Vector3();


    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target.position.y > -50 && target.position.y < 1 || target.position.x > -20 && target.position.x < 55)
        {
            Vector3 targetPos = SetPos(cameraTargetPos, target.position.x, target.position.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);
        }
      

     

    }

    private Vector3 SetPos(Vector3 pos, float x, float y, float z)
    {
        pos.x = x;
        pos.y = y;
        pos.z = z;
        return pos;
    }
    private Vector3 SetPosX(Vector3 pos, float x, float y, float z)
    {
        pos.x = x;
        pos.z = z;
        return pos;
    }
}
