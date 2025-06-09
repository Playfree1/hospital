using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private Animator animator;
    private Tilemap Tilemap;
    private bool isdashing = false,canDash = true;
    float x;
    float y;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
        Tilemap = GameObject.Find("flour").GetComponent<Tilemap>();
    }

    private void Update()
    {
        if (!canDash) return;
        if(isdashing) return;
        if (Input.GetAxisRaw("Fire3") > 0.1f)
            StartCoroutine(nameof(Dash));
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isdashing = true;
        Vector2 direction = new Vector2(x, y);
        direction.Normalize();
        rb.linearVelocity = direction * speed * 3;
        yield return new WaitForSeconds(0.1f);
        isdashing = false;
        yield return new WaitForSeconds(5);
        canDash = true;
    }

    void FixedUpdate()
    {
        if (isdashing) return;
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("x", x);
            animator.SetFloat("y", y);
        animator.SetBool("isMoving",(Mathf.Abs(x)>0.1f)||(Mathf.Abs(y) >0.1f));
            Vector2 direction = new Vector2(x, y);
            direction.Normalize();
        Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;

        // Проверяем, есть ли тайл в новой позиции
        Vector3Int cellPosition = Tilemap.WorldToCell(newPosition);
        if (Tilemap.HasTile(cellPosition))
        {
            rb.MovePosition(newPosition);
        }


    }
}
