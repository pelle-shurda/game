using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb; //Deklarerar en Rigidbody-variabel
    private BoxCollider2D coll; //Deklarerar en BoxCollider-variabel
    private SpriteRenderer sprite; //Deklarerar en Rigidbody-variabel
    private Animator anim; //Deklarerar en Animator-variabel

    [SerializeField] private LayerMask jumpableGround; //Deklarerar en LayerMask-variabel för marklagret

    [SerializeField] private AudioSource jumpSoundEffect; //Deklarerar en AudioSource-variabel för hoppljudet
    
        [SerializeField] private float speed = 5f; //Deklarerar en float som ska styra spelarens hastighet
        // SerializeField gör att variabeln går att ändra från editorn
        [SerializeField] private float jumpHeight = 10f; //Deklarerar en float som ska styra spelarens hastighet

        private float horizontalInput; //En variabel som lagrar åt vilket håll användaren trycker (-1 är vänster, 1 är höger)

        private enum MovementState { idle, running, jumping, falling } //En uppräkning av olika rörelsetillstånd för spelaren
        MovementState state = MovementState.idle; //Variabel som lagrar spelarens nuvarande rörelsetillstånd
    void Start()
        {
            rb = GetComponent<Rigidbody2D>(); //Hämtar Rigidbody-komponenten på spelobjektet och lagrar i rb
            coll = GetComponent<BoxCollider2D>(); //Hämtar BoxCollider-komponenten på spelobjektet och lagrar i coll
            sprite = GetComponent<SpriteRenderer>(); //Hämtar Sprite-komponenten på spelobjektet och lagrar i sprite
            anim = GetComponent<Animator>(); //Hämtar Animator-komponenten på spelobjektet och lagrar i anim
        }
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //Lyssnar efter om användaren trycker till höger eller till vänster och sparar i horizontalInput
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y); //Sätter en hastighet på rigidbodyn på spelaren
        state = MovementState.idle; //Standardtillstånd är idle


        //Om användaren rör sig till vänster - Vänd spelarens sprite och spela "springanimationen"
        if (rb.linearVelocity.x < 0)
        {
            sprite.flipX = true;
            state = MovementState.running;
        }
        if (rb.linearVelocity.x > 0)
        {
            sprite.flipX = false;
            state = MovementState.running;
        }

        //Om användaren trycker space - Hoppa
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play(); //Spela hoppljudet
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
        }


        anim.SetInteger("state", (int)state); //Sätter animationen baserat på spelarens rörelsetillstånd
    }

    private bool IsGrounded()
    {
        //Kolla om spelaren är på marken
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}
