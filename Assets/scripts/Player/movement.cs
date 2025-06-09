using UnityEngine;
using UnityEngine.Tilemaps;

public class movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private Animator animator;
    private Tilemap Tilemap;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
        Tilemap = GameObject.Find("flour").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

            animator.StopPlayback();
            animator.SetFloat("x", x);
            animator.SetFloat("y", y);
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
