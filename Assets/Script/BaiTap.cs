using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaiTap : MonoBehaviour
{
    bool Run = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Run)
        {
            Run = false;
            Invoke(nameof(Test), 2f);
        }
    }
    void Test()
    {
        Debug.Log("Looppp");
    }
}
