using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rigid;
    public Vector2 tempMovDir;
    public Vector2 temp;
    public float velocity=5f;
    public Vector3 moveDir;
    public float turnRate=0.25f;
    public GameObject explosive;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown("space"))
        {
            SpawnExplosive();
        }  
            
    }

    void FixedUpdate()
    {
        //rigid.velocity = moveDir * Time.fixedDeltaTime * velocity;
        rigid.MovePosition(this.transform.position + moveDir * Time.fixedDeltaTime * velocity);
    }

    private void Move()
    {
        tempMovDir.x = Input.GetAxis("Horizontal");
        tempMovDir.y = Input.GetAxis("Vertical");
        if (tempMovDir.x > 0.19 || tempMovDir.y > 0.19 || tempMovDir.x < -0.19 || tempMovDir.y < -0.19)
        {
            temp = SquareToCircle(tempMovDir);
            moveDir = Vector3.Slerp(moveDir, new Vector3(temp.x, 0, temp.y), 0.05f);
            this.transform.forward = Vector3.Slerp(this.transform.forward, moveDir, turnRate);
        }
        else
        {
            moveDir.x = 0;
            moveDir.z = 0;
        }
    }

    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 temp = Vector2.zero;
        temp.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        temp.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
   
        return temp;

    }

    private void SpawnExplosive()
    {
        Instantiate(explosive, new Vector3(this.transform.position.x,0, this.transform.position.z), this.transform.rotation);
    }
}
