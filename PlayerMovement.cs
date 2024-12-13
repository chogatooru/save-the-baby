using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator animator;
    public AudioClip walkSound; // 走路时的音效
    private AudioSource audioSource;

    private string direction; // 移动方向

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found!");
        }
    }

    private void Update()
    {
        // 处理 WASD 移动
        HandleMovementInput();

        // // 处理鼠标点击
        // HandleMouseClick();
    }

    void HandleMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        movement.Normalize(); // 防止对角线移动时速度过快

        // 如果有移动输入，进行移动和更新动画状态
        if (movement.sqrMagnitude > 0)
        {
            transform.Translate(movement * moveSpeed * Time.deltaTime);
            UpdateAnimationState(movement);
            PlayWalkSound();
        }
        else
        {
            // 如果没有移动输入，停止移动并停止动画
            transform.Translate(Vector3.zero);
            audioSource.Stop(); // 停止音效播放
            UpdateAnimationState(Vector2.zero); // 停止动画
        }
    }

    // void HandleMouseClick()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //         RaycastHit hit;

    //         if (Physics.Raycast(ray, out hit))
    //         {
    //             // 检查是否点击到可行走区域
    //             if (hit.collider.CompareTag("Ground"))
    //             {
    //                 Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //                 targetPosition.z = 0f;

    //                 // 检查是否点击到障碍物，然后移动到点击位置
    //                 StartCoroutine(MoveToPositionCoroutine(targetPosition));
    //             }
    //         }
    //     }
    // }

    // void MoveToPosition(Vector3 targetPosition)
    // {
    //     // 这里你可以使用你的路径寻找算法，或者直接使用插值移动来移动到目标位置
    //     // 这里简化为直接使用插值移动
    //     StartCoroutine(MoveToPositionCoroutine(targetPosition));
    // }

    void UpdateAnimationState(Vector2 movement)
    {
        // 根据移动方向选择性更新动画状态
        if (animator != null)
        {
            SetDirection(movement);
            animator.SetBool("Up", direction == "Up");
            animator.SetBool("Down", direction == "Down");
            animator.SetBool("Left", direction == "Left");
            animator.SetBool("Right", direction == "Right");
        }
    }

    void SetDirection(Vector2 movement)
    {
        if (movement.y > 0)
        {
            direction = "Up";
        }
        else if (movement.y < 0)
        {
            direction = "Down";
        }
        else if (movement.x < 0)
        {
            direction = "Left";
        }
        else if (movement.x > 0)
        {
            direction = "Right";
        }
    }

    System.Collections.IEnumerator MoveToPositionCoroutine(Vector3 targetPosition)
    {
        float journeyTime = 1.0f;
        float elapsedTime = 0;

        Vector3 startingPos = transform.position;

        while (elapsedTime < journeyTime)
        {
            // 手动计算移动向量
            Vector3 movement = (targetPosition - startingPos).normalized;
            transform.position = Vector3.Lerp(startingPos, targetPosition, elapsedTime / journeyTime);
            elapsedTime += Time.deltaTime;

            // 手动更新动画状态
            UpdateAnimationState(movement);

            yield return null;
        }

        transform.position = targetPosition;
    }

    void PlayWalkSound()
    {
        if (walkSound != null && !audioSource.isPlaying)
        {
            audioSource.clip = walkSound;
            audioSource.Play();
        }
    }
}
