using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerController playerController;
    Rigidbody rb;
    public int health;
    public float speed = 0f;

    public AudioClip Backup;
    public AudioClip Drive;
    public AudioClip MachienGun;
    public AudioClip BigShot;
    public AudioSource audioSource;

    public int knockBack;

    private Vector3 moveDirection = Vector3.zero;

    private float elapsedTime = 1f;
    private float intervalTime = 1.0f;
    private bool Bdead;

    float maxSpeed = 10f;
    public float accelerationTime = 20;
    private float minSpeed;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireMachineGun());
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        minSpeed = speed;
        time = 0;
        Bdead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Bdead)
        {
            var rotationSpeed = 80;

            if (Input.GetMouseButtonDown(0))
            {
                audioSource.PlayOneShot(BigShot);
            }

            if (Input.GetKey(KeyCode.W))
            {
                Vector3 mover = transform.position + (-transform.forward * speed * Time.deltaTime);
                //this.transform.position += new Vector3(0.0f, 0.0f,-1.0f*Time.deltaTime);
                speed = Mathf.SmoothStep(minSpeed, maxSpeed, time / accelerationTime);
                time += Time.deltaTime;
                rb.MovePosition(mover);
            }
            else
            {
                speed = minSpeed;
                time = 0;
            }
            if (Input.GetKey(KeyCode.S))
            {
                Vector3 mover = transform.position + (transform.forward * speed * Time.deltaTime);
                //this.transform.position += new Vector3(0.0f, 0.0f, 1.0f * Time.deltaTime);
                rb.MovePosition(mover);

                elapsedTime += Time.deltaTime;

                if (elapsedTime >= intervalTime)
                {
                    audioSource.PlayOneShot(Backup);
                    elapsedTime = 0.0f;
                }

            }
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Rotate(-Vector3.up * (rotationSpeed * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
            }
            //if(health < 0)
            //{
            //    moveDirection = new Vector3(Input.GetAxis("Horozontal"), 0.0f, 0.0f);
            //    moveDirection *= speed;
            //}
            //playerController.transform.position += (moveDirection * Time.deltaTime);

            if (Input.GetKey(KeyCode.Space))
            {
                transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
            }

            if (health <= 0)
            {
                Bdead = true;
                dead();
            }
        }
    }

    void dead()
    {

    }

    IEnumerator FireMachineGun()
    {
        while (true)
        {
            if (Input.GetMouseButton(1))
            {
                audioSource.PlayOneShot(MachienGun);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

}
