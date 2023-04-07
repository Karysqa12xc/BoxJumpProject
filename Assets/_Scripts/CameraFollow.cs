using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]private GameObject target;
    private Vector3 offset;
    [SerializeField]private float smooothSpeed = .125f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        offset = target.transform.position - transform.position;

    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 desirePos = target.transform.position - offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desirePos, smooothSpeed);
        if(target.transform.position.y < 3f){ //highest is 4
            smoothPos.y = transform.position.y;
        }   
        transform.position = desirePos;
    }

}
