using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour
{
    public float maxStretch = 3.0f;

    private Vector2 prevVel;

    private float maxStretchSqr;
    public LineRenderer cFront;
    public LineRenderer cBack;

    private Transform catapult;

    private Ray rayToMouse;
    private Ray leftCatapultToProjectile;

    private SpringJoint2D spring;
    private Rigidbody2D rb;

    public AudioClip charge;

    bool clickedOn;
    // Start is called before the first frame update
    private void Awake()
    {
        cFront = GameObject.Find("Mano_i").GetComponent<LineRenderer>();
        cBack = GameObject.Find("Mano_d").GetComponent<LineRenderer>();

        spring = GetComponent<SpringJoint2D>();

        spring.connectedBody = GameObject.Find("Punto_anclaje").GetComponent<Rigidbody2D>();

        catapult = spring.connectedBody.transform;

        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rayToMouse = new Ray(catapult.position, Vector3.zero);
        leftCatapultToProjectile = new Ray(cFront.transform.position, Vector3.zero);

        maxStretchSqr = maxStretch * maxStretch;

        LineRendererSetup();
    }

    // Update is called once per frame
    void Update()
    {
        if (spring != null && Input.GetKeyDown(KeyCode.Mouse0))
        {
            spring.enabled = false;
            clickedOn = true;
        }
        else if (spring != null && Input.GetKeyUp(KeyCode.Mouse0))
        {
            spring.enabled = true;
            rb.isKinematic = false;
            clickedOn = false;
        }
        if (spring != null && clickedOn)
        {
            Dragging();
        }

        if (spring != null)
        {
            if (!rb.isKinematic && prevVel.sqrMagnitude > rb.velocity.sqrMagnitude)
            {
                GameManager.Instance.PlayClip(GetComponent<Jugador>().wii);
                Destroy(spring);
                rb.velocity = prevVel;
                GameManager.Instance.Lanzado();
            }
            if (!clickedOn)
            {
                prevVel = rb.velocity;
            }
            LineRendererUpdate();
        }
        else
        {
            cFront.enabled = false;
            cBack.enabled = false;
        }
    }

    void LineRendererSetup()
    {
        cFront.enabled = true;
        cBack.enabled = true;

        cFront.SetPosition(0, cFront.transform.position);
        cBack.SetPosition(0, cBack.transform.position);
        cFront.SetPosition(1, transform.position);
        cBack.SetPosition(1, transform.position);

        cFront.sortingLayerName = "Player";
        cBack.sortingLayerName = "Player";

        cFront.sortingOrder = 3;
        cBack.sortingOrder = 1;
    }

    void LineRendererUpdate()
    {
        Vector2 catapultToProjectile = transform.position - cFront.transform.position;
        leftCatapultToProjectile.direction = catapultToProjectile;
        Vector3 holdPoint = leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude);
        cFront.SetPosition(1, holdPoint);
        cBack.SetPosition(1, holdPoint);
    }

    void Dragging()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 catapulToMouse = mouseWorldPoint - catapult.position;

        if (catapulToMouse.sqrMagnitude > maxStretchSqr)
        {
            rayToMouse.direction = catapulToMouse;
            mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
        }

        mouseWorldPoint.z = 0f;

        transform.position = mouseWorldPoint;
    }
}
