using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    public float moveSpeed;
    public bool faceRight = true;

    public LayerMask ladderMask;
    public float UpSpeed;
    public float radiusCir = 1f;
    public bool isClimbable = false;
    public bool inClimb = false;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Collider2D temp = Physics2D.OverlapCircle(transform.position, radiusCir, ladderMask);
        isClimbable = (temp != null);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isClimbable && !inClimb)
            {
                ladderSetPos temp1 = temp.GetComponent<ladderSetPos>();
                if (Mathf.Abs(temp1.upPos.y - transform.position.y) > Mathf.Abs(temp1.downPos.y - transform.position.y))
                {
                    transform.position = temp1.downPos;
                }
                else
                {
                    transform.position = temp1.upPos;
                }
                m_rigidbody.gravityScale = 0f;
                inClimb = true;
                this.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else if (inClimb)
            {
                inClimb = false;
                m_rigidbody.gravityScale = 1f;
                this.GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
    }

    private void FixedUpdate()
    {
        float moveh = Input.GetAxis("Horizontal");
        float movev = Input.GetAxis("Vertical");
        if (!inClimb)
        {
            m_rigidbody.velocity = new Vector2(moveh * moveSpeed, m_rigidbody.velocity.y);
            if (moveh > 0 && !faceRight || moveh < 0 && faceRight)
            {
                Filp();
            }
        }
        else
        {
            m_rigidbody.MovePosition(transform.position + new Vector3(0, movev) * UpSpeed * Time.fixedDeltaTime);
        }
    }

    private void Filp()
    {
        faceRight = !faceRight;
        Vector3 thisScale = this.transform.localScale;
        thisScale.x *= -1;
        this.transform.localScale = thisScale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusCir);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print(collision.gameObject.layer);
        if (Mathf.Pow(2,collision.gameObject.layer) ==  ladderMask&& inClimb)
        {
            inClimb = false;
            m_rigidbody.gravityScale = 1f;
            this.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
