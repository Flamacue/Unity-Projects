using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundary
{
    public float minX, maxX, minZ, maxZ;

}

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    public float speed;
    public float tilt;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;
    private float nextFire;
    public float fireRate;
    private AudioSource audio;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;



        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.minX, boundary.maxX),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.minZ, boundary.maxZ)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audio.Play();
        }
    }
}
