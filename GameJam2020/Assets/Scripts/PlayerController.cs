using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool _canMove = true;

    public float speed = 10f;
    public float rotationSpeed = 10f;
    
    private Rigidbody rb;
    private Animator anim;
    private Vector3 targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        targetRotation = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_canMove)
        {
            var horizontal = 0;
            var vertical = 0;
            var horizontalRaw = 0;
            var verticalRaw = 0;
            var move = false;
            if (Input.GetKey(KeyCode.W))
            {
                vertical += 1;
                verticalRaw += 1;
                move = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                horizontal -= 1;
                horizontalRaw -= 1;
                move = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                vertical -= 1;
                verticalRaw -= 1;
                move = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                horizontal += 1;
                horizontalRaw += 1;
                move = true;
            }
            var input = new Vector3(horizontal, 0f, vertical);
            var inputRaw = new Vector3(horizontalRaw, 0f, verticalRaw);
            if (input.sqrMagnitude > 1f)
            {
                input.Normalize();
            }
            if (inputRaw.sqrMagnitude > 1f)
            {
                inputRaw.Normalize();
            }
            if (inputRaw != Vector3.zero)
            {
                targetRotation = Quaternion.LookRotation(input).eulerAngles;
            }

            rb.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(targetRotation.x, Mathf.Round(targetRotation.y / 45) * 45, targetRotation.z), Time.deltaTime * rotationSpeed);

            if (move)
            {
                rb.velocity = transform.forward * speed;
            }
            else
            {
                rb.velocity = new Vector3(0f, 0f, 0f);
            }

            anim.SetBool("IsMoving", move);
        }
    }

    public void SetCanMove(bool move)
    {
        _canMove = move;
        anim.SetBool("IsMoving", move);
    }
}
