using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour
{
    public Animation Animation;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Animation.Play("ActivePlatform");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Animation.Play("NoActivePlatform");
        }
    }
}
