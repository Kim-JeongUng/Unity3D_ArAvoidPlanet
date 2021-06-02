using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGE : MonoBehaviour
{
    MyDefaultTrackableEventHandler myEH;
    public GameObject GuidePlane;
    // Start is called before the first frame update
    void Start()
    {
        myEH = GameObject.Find("ImageTarget").GetComponent<MyDefaultTrackableEventHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!myEH.isAttach)
            GuidePlane.SetActive(true);

        else if (myEH.isAttach)
        {
            GuidePlane.SetActive(false);
        }
    }
}
