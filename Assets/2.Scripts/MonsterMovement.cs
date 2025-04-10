using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float climbHeight = 0.4f;
    public float climbSpeed = 5f;
    public float pushBackDistance = 1f;
    public float pushBackSpeed = 3f;
    public LayerMask monsterLayer;

    [SerializeField] Transform tf;
    private bool isClimbing = false;
    private Vector3 targetPosition;
    
    void Update()
    {
        // 기본 이동
        if (!isClimbing)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        CheckFrontAndClimb();
        CheckUnderAndPush();
    }

    void CheckFrontAndClimb()
    {
        // 앞에 몬스터가 있는지 확인 (수평 방향)
        RaycastHit2D hit = Physics2D.Raycast(tf.position, Vector2.left, 0.2f, monsterLayer);
        if(hit.collider !=null )
        {
            float yDiff = Mathf.Abs(hit.collider.transform.position.y - transform.position.y);

            // 거의 같은 y좌표(같은 라인)일 때
            if (yDiff < 0.2f)
            {
                // climb 처리
                StartClimb();
            }
        }
    }

    void StartClimb()
    {
        if (!isClimbing)
        {
            isClimbing = true;
            targetPosition = new Vector3(transform.position.x, transform.position.y + climbHeight, transform.position.z);
            StartCoroutine(ClimbOver());
        }
    }

    IEnumerator ClimbOver()
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, climbSpeed * Time.deltaTime);
            yield return null;
        }
        isClimbing = false;
    }

    void CheckUnderAndPush()
    {
        // 아래에 몬스터가 있는지 확인
        RaycastHit2D hit = Physics2D.Raycast(tf.position + Vector3.down * 1f, Vector2.down, 0.2f, monsterLayer);

        if (hit.collider != null)
        {
            MonsterMovement other = hit.collider.GetComponent<MonsterMovement>();
            if (other != null)
                other.ApplyPushBack();
        }
    }

    public void ApplyPushBack()
    {
        Vector3 newPos = transform.position;
        newPos.x -= pushBackDistance * Time.deltaTime * pushBackSpeed;
        transform.position = newPos;
    }

}
