using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float vel, cdDashP, tempoDash;
    [SerializeField] private Transform dash;
    [SerializeField] private GameObject espada;
    private float cdDashA;
    private bool mover;
    private Rigidbody2D rigPlayer;
    private Vector2 movimento, pMouse;
    void Start()
    {
        rigPlayer = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        cdDashA -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && cdDashA <= 0)
        {
            mover = false;
            cdDashA = cdDashP;
        }
        movimento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        pMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        if (mover)
        {
            rigPlayer.velocity = movimento.normalized * vel;
            espada.transform.up = new Vector2(pMouse.x - this.transform.position.x, pMouse.y - this.transform.position.y);
        }
        else
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, dash.position, vel/10);
        yield return new WaitForSeconds(tempoDash);
        mover = true;
    }
}
