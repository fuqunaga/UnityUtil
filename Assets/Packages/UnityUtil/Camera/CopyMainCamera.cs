using UnityEngine;
using System.Collections;

public class CopyMainCamera : MonoBehaviour {

    void Update()
    {
        CopyMainCameraParameters();
    }

    void CopyMainCameraParameters()
    {
        var src = Camera.main;

        GetComponent<Camera>().orthographicSize = src.orthographicSize;
        transform.position = src.transform.position;

    }

}
