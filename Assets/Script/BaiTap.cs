using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaiTap : MonoBehaviour
{
    public Transform ATransform;
    public Transform BTransform;
    public float speed;
    public bool isReturn;
    public Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = ATransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // target = isReturn ? BTransform.position : ATransform.position;
        // var dir = target - transform.position;
        // dir.Normalize();
        transform.position = Vector3.MoveTowards(transform.position, BTransform.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, BTransform.position) < 0.1f)
        {
            isReturn = !isReturn;
        }
    }

}
