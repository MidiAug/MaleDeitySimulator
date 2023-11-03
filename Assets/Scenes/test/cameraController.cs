using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    public  Transform target;
    

    [Range(0,1)]
    public float smoothTime;

    public Vector3 positionOffset;


    // Start is called before the first frame update
    void Start()
    {

    }

    void LateUpdate()
    {
        if (target != null)
        {
            if(transform.position!=target.position)
            {
                Vector3 targetPosition = target.position + positionOffset;
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime);
            }            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
