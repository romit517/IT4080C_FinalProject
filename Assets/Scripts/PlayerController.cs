using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using SpriteGlow;




public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float resetSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private GameObject dashRecharge;

    [SerializeField] private PhotonView view;
    private Animator anim;
    private Health healthScript;
    
    LineRenderer rend;

    [SerializeField] private float minX, maxX, minY, maxY;

    [SerializeField] private TextMeshProUGUI nameDisplay;

    private bool canDash;

    private SpriteGlowEffect spriteGlow;
    private UnityEngine.Rendering.Universal.Light2D glowLight;

    private GameObject pauseMenuGo;
    private PauseMenu pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        //view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        healthScript = FindObjectOfType<Health>();
        rend = FindObjectOfType<LineRenderer>();
        resetSpeed = speed;
        if (view.IsMine) {
            nameDisplay.text = PhotonNetwork.NickName;
        } else {
            nameDisplay.text = view.Owner.NickName;
        }
        canDash = true;
        spriteGlow = GetComponent<SpriteGlowEffect>();
        glowLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
    if (view.IsMine) {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
        transform.position += (Vector3)moveAmount;

        Wrap();

        if (Input.GetKeyDown(KeyCode.Space) && moveInput != Vector2.zero && canDash) {
            StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.MenuEnabled == false) {
            //pauseMenuGo.SetActive(true);
            pauseMenu.EnableImagesAndTexts();
            print("enable");
        }
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.MenuEnabled == true){
            //pauseMenuGo.SetActive(false);
            pauseMenu.DisableImagesAndTexts();
            print("disable");
        }

        anim.SetFloat("Horizontal", moveInput.x);
        anim.SetFloat("Vertical", moveInput.y);
        anim.SetFloat("Speed", moveInput.sqrMagnitude);

        rend.SetPosition(0, transform.position);
       
    } else {rend.SetPosition(1, transform.position);}
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (view.IsMine) {
            if (collision.CompareTag("Enemy")) {
                healthScript.TakeDamage();
            }
        }
    }

    IEnumerator Dash() {
        StartCoroutine(EnableDash());
        canDash = false;
        speed = dashSpeed;
        dashRecharge.SetActive(true);
        yield return new WaitForSeconds(dashTime);
        speed = resetSpeed;
    }

    void Wrap() {
        if(transform.position.x < minX) {
            transform.position = new Vector2(maxX, transform.position.y);
        }
        if (transform.position.x > maxX) {
            transform.position = new Vector2(minX, transform.position.y);
        }
        if (transform.position.y < minY) {
            transform.position = new Vector2(transform.position.x, maxY);
        }
        if (transform.position.y > maxY) {
            transform.position = new Vector2(transform.position.x, minY);
        }
    }
    IEnumerator EnableDash() {
        yield return new WaitForSeconds(1.0f);
        canDash = true;
        dashRecharge.SetActive(false);
    }

    public void IncreaseSpeed(float duration, float multiplier) {
        view.RPC(nameof(IncreaseSpeedRPC), RpcTarget.All, duration, multiplier);
    }
    [PunRPC]
    void IncreaseSpeedRPC(float duration, float multiplier) {
        StartCoroutine(IncreaseSpeedCo(duration, multiplier));
    }
    IEnumerator IncreaseSpeedCo(float duration, float multiplier) {
        speed *= multiplier;
        spriteGlow.OutlineWidth = 0.1f;
        glowLight.intensity = 1;
        StartCoroutine(LightFlickerCo(duration));
        yield return new WaitForSeconds(duration);
        speed /= multiplier;
    }
    IEnumerator LightFlickerCo(float duration) {
        yield return new WaitForSeconds(duration - 2f);
        spriteGlow.OutlineWidth = 0.0f;
        glowLight.intensity = 0;
        yield return new WaitForSeconds(0.5f);
        spriteGlow.OutlineWidth = 0.1f;
        glowLight.intensity = 1;
        yield return new WaitForSeconds(0.5f);
        spriteGlow.OutlineWidth = 0.0f;
        glowLight.intensity = 0;
        yield return new WaitForSeconds(0.5f);
        spriteGlow.OutlineWidth = 0.1f;
        glowLight.intensity = 1;
        yield return new WaitForSeconds(0.5f);
        spriteGlow.OutlineWidth = 0.0f;
        glowLight.intensity = 0;
    }
}
