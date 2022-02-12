using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{

    private GameObject playerObj;
    public float xMin, xMax, yMin, yMax;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate() //Called at end of update system
    {
        float x = Mathf.Clamp(playerObj.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(playerObj.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}
