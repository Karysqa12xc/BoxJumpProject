using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]private GameObject target;
    private Vector3 offset;
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
        transform.position = desirePos;
    }

}
