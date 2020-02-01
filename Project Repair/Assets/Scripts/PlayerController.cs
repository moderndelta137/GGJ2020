using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Need to specifiy which player is controlling this character;
    public int Which_player;

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
    //Tag definition
    //Piece = 破片
    //Item = 邪魔用アイテム
    //Object =　ピックアップできるものの

    //Carry rotate
    public Vector3 Carry_rotate_speed;

    //Wincondition
    private PieceManager piecemanager;

    // Start is called before the first frame update
    void Start()
    {
        Carrying = false;
        Pickup_prompt.SetActive(false);
        piecemanager = GameObject.Find("PieceManager").GetComponent<PieceManager>();
        rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (knockBacking) return;
        
        //Handle Movement
        //Get Axis input
        move_input.z = Input.GetAxis("Vertical" + Which_player.ToString());
        move_input.x = Input.GetAxis("Horizontal" + Which_player.ToString());
        //move_input = Vector3.Normalize(move_input);
        move_transform = move_input * Move_speed;
        //Move character
        transform.Translate(move_transform, Space.World);
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
                    pickup_object.GetComponent<Collider>().isTrigger = false;
                    pickup_object.transform.SetParent(this.gameObject.transform);
                    pickup_object.transform.localPosition = Carrying_position_offset;
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
    }

    /// <summary>
    /// プレイヤー同士が衝突した際の吹っ飛び処理
    /// </summary>
    void KnockBack()
    {
        if (knockBacking) return;

        knockBacking = true;

        // 後ろ向きの力を加える
        float power = 10.0f;
        rigidbody.AddForce(- transform.forward * power, ForceMode.Impulse);
        Invoke("returnKnockBack", 1.0f);
    }
    /// <summary>
    /// 数秒後にKnockBackから回復する。knockBackingフラグを戻す。
    /// </summary>
    /// <returns></returns>
    void returnKnockBack()
    {
        knockBacking = false;
    }

    public void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                KnockBack();
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
                    Pickup_prompt.SetActive(true);
                    Pickup_prompt.transform.position = other.transform.position + Pickup_prompt_position_offset;
                    pickup_object = other.gameObject;
                    break;
                case "Item":
                    // アイテム触れたらアイテムObj消す
                    Destroy(other.gameObject);
                    //
                    Move_speed = Move_speed * 2;
                    /*
                    can_pickup = true;
                    Pickup_prompt.SetActive(true);
                    Pickup_prompt.transform.position = other.transform.position + Pickup_prompt_position_offset;
                    pickup_object = other.gameObject;
                    */
                    break;
                case "Player":
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
                    Pickup_prompt.SetActive(false);
                    break;
                case "Item":
                    can_pickup = false;
                    Pickup_prompt.SetActive(false);
                    break;
                case "Player":
                    break;
            }
        }
    }


}
