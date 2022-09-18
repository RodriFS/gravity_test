using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    public float MoveSpeed = 15f;
    public float v0 = 1.4142f;
    public GameObject target;

    private float horizontal;
    private float vertical;
    private float shotSpeedForV0 = 352;
    private float shotSpeedForAngle = 158;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        

        if (Input.GetKey(KeyCode.Q))
        {
            rb.transform.Rotate(new Vector3(0, 0, 1));
        }
        if (Input.GetKey(KeyCode.E))
        {
            rb.transform.Rotate(new Vector3(0, 0, -1));
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {

            Rigidbody2D rb = Instantiate(Resources.Load<GameObject>("Prefabs/Bullet"), transform.position, transform.rotation).GetComponent<Rigidbody2D>();
            float x = transform.position.x - target.transform.position.x;
            float y = transform.position.y - target.transform.position.y;
            float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
            float gravityScale = rb.gravityScale;

            float v0 = Mathf.Sqrt(
                (x + x * gravityScale)
                /
                (x * Mathf.Sin(2 * angle) - 2 * y * Mathf.Pow(Mathf.Cos(angle), 2) - transform.position.x)
                );
            Debug.Log(v0);
            rb.AddForce(transform.right * v0 * shotSpeedForV0 - new Vector3(0, y));

            rb.SetRotation(Vector3.Angle(transform.position, target.transform.position));
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            Rigidbody2D rb = Instantiate(Resources.Load<GameObject>("Prefabs/Bullet"), transform.position, transform.rotation).GetComponent<Rigidbody2D>();
            float x = target.transform.position.x - transform.position.x;
            float y = target.transform.position.y - transform.position.y;
            float g = rb.gravityScale;
            Debug.Log($"{Mathf.Pow(v0, 4) - g * (g * x * x + 2 * y * v0 * v0)}, {g * x}");
            float angle = Mathf.Atan(
                (v0 * v0 - Mathf.Sqrt(Mathf.Pow(v0, 4) - g * (g * x * x + 2 * y * v0 * v0)))
                /
                (g * x)
                );
            Debug.Log(angle * Mathf.Rad2Deg);
            transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);
            rb.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);
            rb.AddForce(transform.right * v0 * shotSpeedForAngle);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * MoveSpeed, vertical * MoveSpeed);
    }
}
