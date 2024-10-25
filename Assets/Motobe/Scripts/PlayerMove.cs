using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    //Rigidbody
    private Rigidbody rb;

    //GameController
    public GameObject gc;
    GameController gameController;

    //位置関係
    public float DefaultPosition;
    public float PlayerSpeed;
    float BackSpeed;
    public  float BackSpeedPlus;
    public float EndPositionY;
    //デバック用
    public float StartPositionY;

    //ジャンプ関連
    public float JumpForce;
    public int MaxJumpCount;
    private int thisJumpCount;
    private bool onWall;
    public string StageTag;
    public string WallTag;
    public string GiriJumpTag;
    public string GiriGiriJumpTag;
    private bool Jump;
    private bool GiriGiri;
    private float JumpCoolTime = 0.1f;
    private float JumpCoolTimer;

    //見た目関連
    public GameObject PlayerSkin;
    public float RotaSpeed;
    private bool Rota;
    public string LinePrefab;

    //敵との判定等
    public string EnemyTag;
    private bool DamageTrigger;
    public SpriteRenderer DamageEffectPlayer;
    public SpriteRenderer DamageEffect;
    public float DamageTime;
    public float DamageColor;

    //HP関連
    private int thisHP;
    public int StartHP;
    public int MAXHP;
    public GameObject[] HPObject;
    // Start is called before the first frame update
    void Start()
    {
        onWall = false;
        Rota = true;
        rb = GetComponent<Rigidbody>();
        Jump = false;
        GiriGiri = false;
        JumpCoolTimer = 0;
        DamageTrigger = true;
        thisHP = StartHP;
        gameController = FindObjectOfType<GameController>();
        
        /*
            for (int i = 0; i < MAXHP; i++)
            {
                HPObject[i].SetActive(false);
            }
            for (int i = 0; i < thisHP; i++)
            {
                HPObject[i].SetActive(true);
            }
        */
    }

    // Update is called once per frame
    void Update()
    {
        BackSpeed= BackSpeedPlus + gameController.StageSpeed;
        if (Jump)
        {
            JumpCoolTimer += Time.deltaTime;
            if (JumpCoolTimer > JumpCoolTime)
            {
                JumpCoolTimer = 0;
                Jump = false;
                DamageTrigger = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpAction();
        }
        if (!onWall)
        {
            if (this.transform.position.x < DefaultPosition)
            {
                this.transform.position += new Vector3(PlayerSpeed * Time.deltaTime, 0, 0);
            }
            if (this.transform.position.x > DefaultPosition)
            {
                this.transform.position -= new Vector3(PlayerSpeed * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            this.transform.position -= new Vector3(BackSpeed * Time.deltaTime, 0, 0);
        }
        
        if (Rota)
        {
            PlayerSkin.transform.Rotate(0, 0, -RotaSpeed * Time.deltaTime);
        }
        if (this.transform.position.y < EndPositionY)
        {
            this.transform.position += new Vector3(0, StartPositionY, 0);
        }
        //Line();
    }
    /*private void Line()
    {
        GameObject Line_prefab = Resources.Load<GameObject>(LinePrefab);
        GameObject Line = Instantiate(Line_prefab, this.transform.position, Quaternion.identity);
        return;
    }
    */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(StageTag) && !onWall)
        {
            thisJumpCount = 0;
            PlayerSkin.transform.rotation = Quaternion.identity;
            Rota = false;
            Jump = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GiriGiriJumpTag))
        {
            GiriGiri = true;
        }
        if (other.gameObject.CompareTag(WallTag))
        {
            onWall = true;
            Debug.Log("Wall");
        }
        if (other.gameObject.CompareTag(EnemyTag))
        {
            if (DamageTrigger)
            {
                Damage();
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(StageTag) && !onWall)
        {
            thisJumpCount = 0;
        }
        if (other.gameObject.CompareTag(WallTag))
        {
            onWall = false;
        }
        if (other.gameObject.CompareTag(GiriGiriJumpTag))
        {
            GiriGiri = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GiriJumpTag) && Jump)
        {
            if (GiriGiri)
            {
                Debug.Log("ギリギリ");
                Heal();
            }
            else
            {
                Debug.Log("ギリ");
            }
            DamageTrigger = false;
            Destroy(other.gameObject);
        }
    }
    public void JumpAction()
    {
        if (thisJumpCount < MaxJumpCount)
        {
            rb.velocity = new Vector3(0, JumpForce, 0);
            thisJumpCount++;
            Rota = true;
            Jump = true;
        }
    }

    void Damage()
    {
        thisHP--;
        if (thisHP > 0)
        {
            HPObject[thisHP].SetActive(false);
        }
        var sequence = DOTween.Sequence();
        sequence.Append(DamageEffect.DOFade(DamageColor, DamageTime));
        sequence.Append(DamageEffect.DOFade(0, DamageTime));
        sequence.Join(DamageEffectPlayer.DOFade(DamageColor, DamageTime));
        sequence.Append(DamageEffect.DOFade(DamageColor, DamageTime));
        sequence.Join(DamageEffectPlayer.DOFade(0, DamageTime));
        sequence.Append(DamageEffect.DOFade(0, DamageTime));
        sequence.Join(DamageEffectPlayer.DOFade(DamageColor, DamageTime));
        sequence.Append(DamageEffect.DOFade(DamageColor, DamageTime));
        sequence.Join(DamageEffectPlayer.DOFade(0, DamageTime));
        sequence.Append(DamageEffect.DOFade(0, DamageTime));
        sequence.Join(DamageEffectPlayer.DOFade(DamageColor, DamageTime));
        sequence.Append(DamageEffectPlayer.DOFade(0, DamageTime));
    }

    void Heal()
    {
        if (thisHP < MAXHP)
        {
            HPObject[thisHP].SetActive(true);
            thisHP++;
        }
    }
}
