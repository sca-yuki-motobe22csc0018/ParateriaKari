using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMove : MonoBehaviour
{
    public float moveSize;
    public float moveSpeed;
    private Vector3 move;
    float cos;
    float thisTime;
    // Start is called before the first frame update
    void Start()
    {
        thisTime = 0;
    }
    // Update is called once per frame
    void Update()
    {
        thisTime += Time.deltaTime;
        cos = Mathf.Cos(thisTime*moveSpeed);
        move = move = new Vector3(0, Time.deltaTime*moveSize*cos, 0);
        this.transform.position += move;
    }
}
