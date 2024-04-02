using System.Collections;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb2D;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected AudioSource audioSource;

    [SerializeField] protected PlayerDetection playerDetection;

    [SerializeField] protected LayerMask raycastHitLayer;

    public enum CurrentState { ROAMING, CHASING, AIMING, RELOADING, SHOOTING, ICE };
    [SerializeField] public CurrentState currentState = CurrentState.ROAMING;

    protected Transform target;

    [SerializeField] protected SpriteRenderer foundTargetAlert;
    [SerializeField] protected SpriteRenderer lostTargetAlert;
    [SerializeField] protected float alertTime = 0.8f;

    //MOVEMENT
    protected float moveSpeed;

    //ROAMING
    [SerializeField] protected float roamingSpeed;
    [SerializeField] protected GameObject roamingPoints;
    [SerializeField] protected Transform pointA;
    [SerializeField] protected Transform pointB;

    //CHASING
    [SerializeField] protected float chasingSpeed;
    [SerializeField] protected float startChasingDistance = 5f;
    [SerializeField] protected float stopChasingDistance = 15f;

    //Hit    
    [SerializeField] GameObject freezeParticles;

    //Ice
    public bool canFreeze = true;


    public virtual void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        target = pointA;
    }

    public virtual void Movement()
    {
        
    }

    public virtual void Roaming()
    {
       
    }

    public virtual void Chasing()
    {      

    }
    public void FlipX()
    {
        if (rb2D.velocity.x > 0) spriteRenderer.flipX = false;
        else if (rb2D.velocity.x == 0)
        {
            if (target.position.x > transform.position.x) spriteRenderer.flipX = false;
            else spriteRenderer.flipX = true;
        }
        else spriteRenderer.flipX = true;
    }


    public void FlipByTarget()
    {
        if (target.position.x > transform.position.x) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;
    }


    protected IEnumerator FreezeRecover()
    {
        canFreeze = false;
        yield return new WaitForSecondsRealtime(6.0f);
        canFreeze = true;
    }
    protected bool RaycastPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (playerDetection.playerTransform.transform.position - transform.position).normalized, 100, raycastHitLayer);
        return hit.rigidbody != null && hit.rigidbody.CompareTag("Player");
    }

    public IEnumerator Ice()
    {
        currentState = CurrentState.ICE;
        StartCoroutine(FreezeRecover());
        GameObject a = Instantiate(freezeParticles, this.transform.position, Quaternion.identity);
        a.transform.SetParent(transform, true);
        GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.6f, 0.9f, 1);

        yield return new WaitForSeconds(2f);
        GetComponent<SpriteRenderer>().color = Color.white;

        currentState = CurrentState.ROAMING;
    }
    protected IEnumerator EnableAlert(SpriteRenderer sp)
    {
        sp.enabled = true;
        yield return new WaitForSecondsRealtime(alertTime);
        sp.enabled = false;
    }

 

    public virtual void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(pointA.transform.position, 0.2f);
        Gizmos.DrawSphere(pointB.transform.position, 0.2f);
    }

}

