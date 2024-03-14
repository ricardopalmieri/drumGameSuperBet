using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kinect = Windows.Kinect;

public class handControl : MonoBehaviour
{

    private kinectManager km;
    private GameObject Hand;
    public string handName;
    private float speed = 0.5f;
    RectTransform rb;
    Image sR;
    private Vector2 position = new Vector2(0f, 0f);
    private Vector3 objPos = new Vector3(0f, -30f, 0f);

    private Vector3 handPos;
    // Start is called before the first frame update
    void Start()
    {
        km = GameObject.Find("BodyManager").GetComponent<kinectManager>();
        rb = GetComponent<RectTransform>();
        sR = GetComponent<Image>();
        sR.enabled = false;
        // Hand = GameObject.Find(handName);

    }

    // Update is called once per frame
    void Update()
    {

        if (km.GetDistance() < 0.1f)
        {
            rb.anchoredPosition = new Vector2(200f, -1000f);
        }
        else
        {

            if (handPos == null)
            {
                // Hand = GameObject.Find(handName);

                HandSelect(handName);


            }
            else
            {

                HandSelect(handName);

                objPos = handPos;
                Vector2 canvasPosition = new Vector2(objPos.x * 1000f, objPos.y * 1000f);
                rb.anchoredPosition = Vector2.Lerp(rb.anchoredPosition, canvasPosition, speed);
                sR.enabled = true;



            }
        }

        //AJUSTE z KINECT
        //  Debug.Log(handPos.z);
       // Debug.Log("km: " + km.GetHandLeftPosition() + ", Hand: " + handPos + ", distKm: " + km.GetDistance());



    }


    private void HandSelect(string h)
    {

        if (h == "HandLeft")
        {
            handPos = km.GetHandLeftPosition();
        }
        else if (h == "HandRight")
        {
            handPos = km.GetHandRightPosition();
        }

    }


}
