using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float vel, cdDashP, tempoDash;
    [SerializeField] private Animator animCoracao, animDogao;
    [SerializeField] private Transform dash;
    [SerializeField] private Vector2 fimDaTela;
    [SerializeField] private GameObject espada, spritePlayer, spriteDogao;
    [SerializeField] private SpriteRenderer rendererPlayer, rendererDogao;
    [SerializeField] private Image cardio;
    private float cdDashA, batimentos = 100;
    private bool bDash, dogao;
    private Rigidbody2D rigPlayer;
    private Animator animPlayer;
    private Vector2 movimento, pMouse;
    void Start()
    {
        rigPlayer = this.GetComponent<Rigidbody2D>();
        animPlayer = this.GetComponent<Animator>();
    }

    private void Update()
    {
        spriteDogao.SetActive(dogao);
        spritePlayer.SetActive(!dogao);
        espada.SetActive(!dogao);

        if (batimentos == 100)
            dogao = true;
        else if (batimentos == 20)
            dogao = false;

        batimentos = Mathf.Clamp(batimentos - Time.deltaTime * 3, 20, 100);
        cardio.fillAmount = batimentos / 100;
        animCoracao.speed = batimentos / 100;
        cdDashA -= Time.deltaTime;

        if(dogao)
        {
            rendererDogao.flipX = movimento.x != 0 ? movimento.x < 0 : rendererDogao.flipX;
            animDogao.SetBool("Andando", movimento != Vector2.zero);
        }
        else
        {
            rendererPlayer.flipX = movimento.x != 0 ? movimento.x < 0 : rendererPlayer.flipX;
            animPlayer.SetBool("Andando", movimento != Vector2.zero);

            if (Input.GetMouseButtonDown(0) && cdDashA <= 0)
            {
                bDash = true;
                cdDashA = cdDashP;
            }
        }

        if (Mathf.Abs(this.transform.position.x) > fimDaTela.x)
        {
            if (this.transform.position.x < 0)
            {
                this.transform.position = new Vector2(-fimDaTela.x + 0.1f, this.transform.position.y);
            }
            if (this.transform.position.x > 0)
            {
                this.transform.position = new Vector2(fimDaTela.x - 0.1f, this.transform.position.y);
            }
        }
        if (Mathf.Abs(this.transform.position.y) > fimDaTela.y)
        {
            if (this.transform.position.y < 0)
            {
                this.transform.position = new Vector2(this.transform.position.x, -fimDaTela.y + 0.1f);
            }
            if (this.transform.position.y > 0)
            {
                this.transform.position = new Vector2(this.transform.position.x, fimDaTela.y - 0.1f);
            }
        }

        movimento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        pMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        if (!bDash)
        {
            rigPlayer.velocity = movimento.normalized * (dogao? vel * 2 : vel);
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
        bDash = false;
    }
}
