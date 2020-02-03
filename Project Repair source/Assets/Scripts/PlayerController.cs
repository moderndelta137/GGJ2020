using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Need to specifiy which player is controlling this character;
    public int Which_player;

    // Current Pieces Count
    public int currentPiecesCount = 0;

    //Character Movement
    public float Move_speed;
    public float Rotate_speed;
    private Vector3 move_input=Vector3.zero;
    private Vector3 move_transform = Vector3.zero;


    //Pickup Related
    //Whether if the object can be picked up
    private bool can_pickup;
    private GameObject pickup_object;
    //Whether if character is carrying an item
    public bool Carrying;
    public Vector3 Carrying_position_offset;
    public Vector3 Putdown_position_offset;
    //Need to specify the Pickup_prompt gameobject;
    public GameObject Pickup_prompt;
    public Vector3 Pickup_prompt_position_offset;

    // For knock back
    private bool knockBacking = false;
    private Rigidbody rigidbody;

    // For drop items
    private GameObject dropped_object;

    public AudioClip itemGetSe;
    public AudioClip playerClashSe;

    //Tag definition
    //Piece = 破片
    //Item = 邪魔用アイテム
    //Object =　ピックアップできるものの

    //Carry rotate
    public Vector3 Carry_rotate_speed;

    //Wincondition
    private PieceManager piecemanager;

    private PlayerController player1Object;
    private PlayerController player2Object;

    private Animator player1Animator;
    private Animator player2Animator;

    public bool playerSpdFlg = false;
    public bool playerStunFlg = false;
    public bool playerToughFlg = false;

    // Effects
    public GameObject speedUpEffect;
    public GameObject toughEffect;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Carrying = false;
        Pickup_prompt.SetActive(false);
        rigidbody = this.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        player1Object = GameObject.Find("Player1").GetComponent<PlayerController>();
        player2Object = GameObject.Find("Player2").GetComponent<PlayerController>();
        player1Animator = GameObject.Find("Chara_4Hero").GetComponent<Animator>();
        player2Animator = GameObject.Find("ChaWitch").GetComponent<Animator>();
        speedUpEffect.SetActive(false);
        toughEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStunFlg) return;
        if (knockBacking) return;
        
        //Handle Movement
        //Get Axis input
        move_input.z = Input.GetAxis("Vertical" + Which_player.ToString());
        move_input.x = Input.GetAxis("Horizontal" + Which_player.ToString());
        //move_input = Vector3.Normalize(move_input);
        move_transform = move_input * Move_speed;
        //Move character
        //transform.Translate(move_transform, Space.World);//Move the translate function to FixedUpdate
        //Rotate character towards the moving direction;
        float singleStep = Rotate_speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, move_input, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        //Handle Pickup

        if (Input.GetButtonDown("Pickup" + Which_player.ToString()))
        {
            //Picking object up
            if (Carrying == false && can_pickup == true)
            {
                if (pickup_object != null)//Necessary???
                {
                    pickup_object.transform.SetParent(this.gameObject.transform);
                    pickup_object.transform.localPosition = Carrying_position_offset;
                    pickup_object.GetComponent<Rigidbody>().isKinematic = true;
                    pickup_object.GetComponent<PieceID>().lastTouchPlayer = Which_player;
                    can_pickup = false;
                    Carrying = true;
                    Pickup_prompt.SetActive(false);
                }
            }
            //Putting down
            else if (Carrying == true)
            {
                pickup_object.transform.localPosition = Putdown_position_offset;
                pickup_object.transform.SetParent(null);
                pickup_object.GetComponent<Rigidbody>().isKinematic = true;
                pickup_object.GetComponent<Collider>().isTrigger = true;
                Carrying = false;
            }
        }

        //Handle Pickup rotation
        if (Input.GetButton("RotateR" + Which_player.ToString()))
        {
            if (Carrying == true)
            {
                pickup_object.transform.Rotate(Carry_rotate_speed);

            }
        }

        // check whether pieces are dropped
        if(dropped_object)
        {
            checkPieces();
        }

    }

    //Move the character
    void FixedUpdate()
    {
        transform.Translate(move_transform, Space.World);
    }


    /// <summary>
    /// プレイヤー同士が衝突した際の吹っ飛び処理
    /// </summary>
    void KnockBack(Vector3 Hit_direction)
    {
        if (knockBacking) return;

        knockBacking = true;

        // 持っているアイテムを落とす
        if(Carrying)
        {

            Rigidbody rigidbody_dropped_obj = pickup_object.GetComponent<Rigidbody>();

            // アイテムを地面に置く
            pickup_object.transform.localPosition = Putdown_position_offset;
            pickup_object.transform.SetParent(null);
            pickup_object.GetComponent<Rigidbody>().isKinematic = false;
            pickup_object.GetComponent<Collider>().isTrigger = false;
            rigidbody_dropped_obj.useGravity = true;

            Carrying = false;

            // アイテムをランダムな方向に投げる
            float drop_angle = Random.Range(0, 360);
            float drop_power = 10.0f;
            Vector3 drop_vector = Quaternion.Euler(0, drop_angle, -60) * this.transform.forward * drop_power;
            rigidbody_dropped_obj.AddForce(drop_vector, ForceMode.Impulse);

            dropped_object = pickup_object;
        }

        // 後ろ向きの力を加える
        float power = 10.0f;
        Hit_direction.y = 0;
        Vector3.Normalize(Hit_direction);
        rigidbody.AddForce(-Hit_direction * power, ForceMode.Impulse);
        Invoke("returnKnockBack", 0.5f);

        //Damageアニメーション
        player1Animator.SetBool("IsDamage", true);
        player2Animator.SetBool("isDamage", true);

        player1Animator.SetBool("IsDamage", false);
        player2Animator.SetBool("isDamage", false);
    }

    /// <summary>
    /// 数秒後にKnockBackから回復する。knockBackingフラグを戻す。
    /// </summary>
    /// <returns></returns>
    void returnKnockBack()
    {
        knockBacking = false;
    }

    /// <summary>
    /// 飛んで行った破片が地面に落ちたか判定して再設置する
    /// </summary>
    void checkPieces()
    {
        if(dropped_object.GetComponent<Rigidbody>().velocity.magnitude < 0.1f)
        {
            dropped_object.GetComponent<Collider>().isTrigger = true;
            dropped_object.GetComponent<Rigidbody>().useGravity = false;
            dropped_object.GetComponent<Rigidbody>().isKinematic = true;
            dropped_object.transform.rotation = Quaternion.identity;
            Vector3 pos = dropped_object.transform.position;
            dropped_object.transform.position = new Vector3(pos.x, 0.4f, pos.z);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                if (!playerToughFlg)
                {
                    KnockBack(other.transform.position - this.transform.position);
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Carrying == false)
        {
            switch (other.tag)
            {
                case "Piece":
                    can_pickup = true;
                    pickup_object = other.gameObject;
                    Pickup_prompt.transform.parent = pickup_object.transform;
                    Pickup_prompt.transform.localPosition =  Pickup_prompt_position_offset;
                    Pickup_prompt.transform.rotation = Quaternion.Euler(40,0,0);
                    Pickup_prompt.SetActive(true);
                    break;
                case "SpeedUpItem":
                    playItemGetSe();
                    // アイテム触れたらアイテムObj消す
                    Destroy(other.gameObject);
                    //

                    if (playerSpdFlg) break;
                    playerSpdFlg = true;
                    Move_speed = Move_speed * 1.5f;
                    speedUpEffect.SetActive(true);
                    Debug.Log("speedup");
                    Invoke("returnMoveSpeed", 5.0f);
                    /*
                    can_pickup = true;
                    Pickup_prompt.SetActive(true);
                    Pickup_prompt.transform.position = other.transform.position + Pickup_prompt_position_offset;
                    pickup_object = other.gameObject;
                    */
                    break;
                case "ToughItem":
                    playItemGetSe();
                    Destroy(other.gameObject);
                    playerToughFlg = true;
                    toughEffect.SetActive(true);
                    Invoke("returnPlayerTough", 5.0f);
                    break;
                case "StunItem":
                    playItemGetSe();
                    // アイテム触れたらアイテムObj消す
                    Destroy(other.gameObject);
                    if (Which_player == 1)
                    {
                        player2Object.Stun();
                    }
                    else
                    {
                        player1Object.Stun();
                    }
                    break;
                case "Player":
                    playClashSe();
                    break;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (Carrying == false)
        {
            switch (other.tag)
            {
                case "Piece":
                    can_pickup = false;
                    Pickup_prompt.transform.parent = this.gameObject.transform;
                    Pickup_prompt.SetActive(false);
                    break;
                case "Item":
                    can_pickup = false;
                    Pickup_prompt.transform.parent = this.gameObject.transform;
                    Pickup_prompt.SetActive(false);
                    break;
                case "Player":
                    break;
            }
        }
    }

    public void Stun()
    {
        playerStunFlg = true;
        Invoke("returnPlayerStun", 5.0f);
    }

    public void returnPlayerStun()
    {
        playerStunFlg = false;
    }

    public void returnPlayerTough()
    {
        playerToughFlg = false;
        toughEffect.SetActive(false);
    }

    public void returnMoveSpeed()
    {
        playerSpdFlg = false;
        Move_speed = Move_speed / 1.5f;
        speedUpEffect.SetActive(false);
    }

    public void playItemGetSe()
    {
        audioSource.PlayOneShot(itemGetSe);

    }

    public void playClashSe()
    {
        audioSource.PlayOneShot(playerClashSe);

    }
}
