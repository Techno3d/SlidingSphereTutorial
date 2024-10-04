using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSphere : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    float maxAccel = 10f;
    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;
    [SerializeField]
    Rect bounding = new Rect(-5f, -5f, 10f, 10f);
    [SerializeField, Range(0f, 1f)]
	float bounciness = 0.5f;
    Vector3 velocity = new Vector3(0f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerInp = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 desired = new Vector3(playerInp.x, 0f, playerInp.y);
        desired = Vector3.ClampMagnitude(desired, 1f);
        desired *= maxSpeed;

        //Actual vel
        velocity.x = Mathf.MoveTowards(velocity.x, desired.x, maxAccel*Time.deltaTime);
        velocity.z = Mathf.MoveTowards(velocity.z, desired.z, maxAccel*Time.deltaTime);
        Vector3 newPos = transform.localPosition + velocity * Time.deltaTime;
        if(newPos.x < bounding.xMin || newPos.x > bounding.xMax) {
            velocity.x *= -1*bounciness;
        }
        if(newPos.z < bounding.yMin || newPos.z > bounding.xMax) {
            velocity.z *= -1*bounciness;
        }
        if(!bounding.Contains(new Vector2(newPos.x, newPos.z))) {
            newPos.x = Mathf.Clamp(newPos.x, bounding.xMin, bounding.xMax);
            newPos.z = Mathf.Clamp(newPos.z, bounding.yMin, bounding.yMax);
        }
        transform.localPosition = newPos;
    }
}
