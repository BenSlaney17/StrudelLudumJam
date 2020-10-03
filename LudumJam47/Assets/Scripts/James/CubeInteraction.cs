using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeInteraction : MonoBehaviour
{

    bool rotating = false;
    float rot = 0;
    float currentPosition = 0;

    bool isCorrectPos = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        rotating = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -rot);

            rot++;

            switch (rot)
            {
              
                case 90:
                    rotating = false;
                    currentPosition = 1;
                    break;
                case 180:
                    rotating = false;
                    currentPosition = 2;
                    break;
                case 270:
                    rotating = false;
                    currentPosition = 3;
                    break;
                case 360:
                    rotating = false;
                    currentPosition = 0;
                    rot = 0;
                    break;
                default:
                    break;
            }

        }

        if (transform.localEulerAngles.z == 0)
        {
            isCorrectPos = true;
        }

    }
}
