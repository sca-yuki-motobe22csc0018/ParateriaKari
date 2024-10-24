using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject GiriJumpPoint;
    private Vector3 GiriJumpPointPosition = new Vector3(-1.5f, 0, 0);
    GameController controller;
    float speed;
    public  float ThisSpeed;
    private bool MoveStart;
    public string StartTag;
    public string FinishTag;
    public GameObject thisCollider;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent = null;
        MoveStart = false;
        controller = FindObjectOfType<GameController>();
        GiriJumpPoint.transform.parent = null;
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = ThisSpeed + controller.StageSpeed;
        if (GiriJumpPoint!=null)
        {
            GiriJumpPoint.transform.position = this.transform.position + GiriJumpPointPosition;
        }
        if (MoveStart)
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position += new Vector3(-controller.StageSpeed * Time.deltaTime, 0, 0);
        }
        if (thisCollider != null)
        {
            thisCollider.transform.position = this.transform.position;
        }
        else
        {
            Destroy(GiriJumpPoint);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(StartTag))
        {
            MoveStart = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(FinishTag))
        {
            Destroy(GiriJumpPoint.gameObject);
            Destroy(this.gameObject);
        }
    }
}
