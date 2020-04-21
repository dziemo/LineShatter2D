using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject hitParticles;

    public IntVariable score, multiplier;
    public FloatVariable multiplierTimer, maxMultiplierTimer;
    public BoolVariable dead;
    
    Vector3 initPos;
    Vector3 desiredPos;
    Vector3 lastPos;
    Vector3 startPos;

    Collider2D lastColl;
    Rigidbody2D rb;
    Camera cam;

    bool leftSide = true;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();

        startPos = transform.position;
        lastPos = startPos;
        desiredPos = startPos;
        initPos = startPos;
    }
    
    void Update()
    {
        if (!dead.RuntimeValue)
        {
            CollisionCheck();

            if ((leftSide && transform.position.x < 0) || (!leftSide && transform.position.x > 0))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ChangeSide();
                }
            }

            if (transform.position != desiredPos)
            {
                transform.position = Vector3.Lerp(transform.position, desiredPos, 0.05f);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, desiredPos - transform.position), 0.4f);
            }

            if (Vector3.Distance(transform.position, desiredPos) < 0.2f)
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }

            if (multiplierTimer.RuntimeValue > 0)
            {
                multiplierTimer.RuntimeValue -= Time.deltaTime * multiplier.RuntimeValue / 20;
            }
            else
            {
                multiplierTimer.RuntimeValue = 0;
                multiplier.RuntimeValue = 1;
            }
        } else
        {
            var camRelativePos = cam.WorldToViewportPoint(transform.position);

            if (camRelativePos.x > 1.1f || camRelativePos.y > 1.1f || camRelativePos.x < -0.1f || camRelativePos.y < -0.1f)
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.SetRotation(0);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetPlayer();
            }
        }
    }

    void ChangeSide ()
    {
        leftSide = !leftSide;
        var tempPos = initPos;
        tempPos.x = leftSide ? Mathf.Abs(tempPos.x) * -1 : Mathf.Abs(tempPos.x);
        desiredPos = tempPos;
        lastColl = null;
    }

    void ResetPlayer ()
    {
        rb.SetRotation(0);
        rb.velocity = Vector3.zero;
        transform.position = startPos;
        lastPos = startPos;
        desiredPos = startPos;
        initPos = startPos;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        rb.isKinematic = true;
        dead.RuntimeValue = false;
        leftSide = true;
    }

    void CollisionCheck ()
    {
        var hitCheck = Physics2D.Linecast(transform.position, lastPos);

        if (hitCheck)
        {
            if (lastColl)
            {
                if (lastColl == hitCheck.collider)
                {
                    return;
                } else
                {
                    if (hitCheck.collider.CompareTag("GoodSegment"))
                    {
                        Instantiate(hitParticles, hitCheck.point, transform.rotation);
                        if (multiplierTimer.RuntimeValue == 0)
                        {
                            multiplierTimer.RuntimeValue = maxMultiplierTimer.RuntimeValue;
                        }

                        score.RuntimeValue += multiplier.RuntimeValue;

                        multiplierTimer.RuntimeValue = maxMultiplierTimer.RuntimeValue;
                        multiplier.RuntimeValue += 1;
                        lastColl = hitCheck.collider;
                    }
                }
            } else
            {
                if (hitCheck.collider.CompareTag("GoodSegment"))
                {
                    Instantiate(hitParticles, hitCheck.point, transform.rotation);
                    if (multiplierTimer.RuntimeValue == 0)
                    {
                        multiplierTimer.RuntimeValue = maxMultiplierTimer.RuntimeValue;
                    }

                    score.RuntimeValue += multiplier.RuntimeValue;

                    multiplierTimer.RuntimeValue = maxMultiplierTimer.RuntimeValue;
                    multiplier.RuntimeValue += 1;
                    lastColl = hitCheck.collider;
                }
                else if (hitCheck.collider.CompareTag("BadSegment"))
                {
                    Die();
                }
            }
        }

        lastPos = transform.position;
    }

    void Die ()
    {
        dead.RuntimeValue = true;
        rb.isKinematic = false;
        rb.AddForce(-transform.up * 10f, ForceMode2D.Impulse);
        rb.AddTorque(5f, ForceMode2D.Impulse);

        score.RuntimeValue = score.InitialValue;
        multiplier.RuntimeValue = multiplier.InitialValue;
    }
}
