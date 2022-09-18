using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 origin;
    int t = 0;
    float x = 0;
    float y = 0;
    float g = 9.8f;
    float angle;
    float v0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void UpdatePosition() {
        x = v0 * Mathf.Cos(angle) * t * Time.deltaTime;
        y = v0 * Mathf.Sin(angle) * t * Time.deltaTime - 0.5f * g * Mathf.Pow(t * Time.deltaTime, 2);

        transform.position = origin + new Vector3(x, y, 0);
        t++;
    }

    private void FixedUpdate()
    {
        // UpdatePosition();
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAngle(float angle)
    {
        this.angle = angle;
    }

    public void SetInitialVelocity(float v0)
    {
        this.v0 = v0;
    }
}
