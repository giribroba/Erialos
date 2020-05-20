using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float vel;
    [SerializeField] private GameObject espada;
    private Rigidbody2D rigPlayer;
    private Vector2 movimento, pMouse;
    void Start()
    {
        Cursor.visible = false;
        rigPlayer = this.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        pMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        movimento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rigPlayer.velocity = movimento.normalized * vel;
        print(movimento.normalized);
        espada.transform.up = new Vector2(pMouse.x, pMouse.y);
    }
}
