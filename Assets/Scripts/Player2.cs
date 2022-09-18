using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{

    private Rigidbody2D canon;
    public float MoveSpeed = 15f;
    public float v0 = 1.4142f;
    public GameObject target;

    private float horizontal;
    private float vertical;
    private float shotSpeedForV0 = 158;
    private float shotSpeedForAngle = 158;

    // Start is called before the first frame update
    void Start()
    {
        canon = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");


        if (Input.GetKey(KeyCode.Q))
        {
            canon.transform.Rotate(new Vector3(0, 0, 1));
        }
        if (Input.GetKey(KeyCode.E))
        {
            canon.transform.Rotate(new Vector3(0, 0, -1));
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Rigidbody2D rb = Instantiate(Resources.Load<GameObject>("Prefabs/Bullet"), transform.position, transform.rotation).GetComponent<Rigidbody2D>();
            float x0 = transform.position.x;
            float xf = target.transform.position.x;
            float y0 = transform.position.y;
            float yf = target.transform.position.y;
            float g = rb.gravityScale;

            float v0 = (xf - x0) / Mathf.Sqrt(1 / g * (xf - x0 + y0 - yf));

            rb.AddForce(transform.right * v0 * shotSpeedForV0);
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            Rigidbody2D rb = Instantiate(Resources.Load<GameObject>("Prefabs/Bullet"), transform.position, transform.rotation).GetComponent<Rigidbody2D>();
            float x0 = transform.position.x;
            float xf = target.transform.position.x;
            float y0 = transform.position.y;
            float yf = target.transform.position.y;

            float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
            float g = rb.gravityScale;

            float v0 = (xf - x0) / Mathf.Sqrt(1 / g * (xf - x0 + y0 - yf));
            Debug.Log(v0);
            rb.AddForce(transform.right * v0 * shotSpeedForV0);
        }
    }

    private void FixedUpdate()
    {
        canon.velocity = new Vector2(horizontal * MoveSpeed, vertical * MoveSpeed);
    }
}
