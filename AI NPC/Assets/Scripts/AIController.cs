using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    /*array_text_function_descriptions_ko = [
        "이 함수는 NPC가 평화로운 상태일 때 호출합니다.",
        "이 함수는 NPC가 플레이어와 거래를 할 때 호출합니다.",
        "이 함수는 NPC가 플레이어를 공격하려고 할때 호출합니다.",
        "이 함수는 NPC가 플레이어를 따라다니려고 할 때 호출합니다."]*/

    public enum NPCState
    {
        Peaceful,
        Trading,
        Attacking,
        Following
    }

    [Header("Check & Assign")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject playerInputField;
    [SerializeField] private GameObject goblinInputField;
    [SerializeField] private GameObject storePanel;

    [Header("Follow")]
    [SerializeField] private bool isFollowing;
    [SerializeField] private float followSpeed;
    [SerializeField] private float stoppingDistance; // 플레이어와 유지할 최소 거리

    private NPCState currentState;

    private void Start()
    {
        SetState(NPCState.Peaceful); // 기본 상태 설정
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case NPCState.Peaceful:
                StayIdle();
                break;
            case NPCState.Trading:
                // 거래 상태는 FixedUpdate에서 별다른 처리가 필요 없음
                break;
            case NPCState.Attacking:
                AttackPlayer();
                break;
            case NPCState.Following:
                FollowPlayer();
                break;
        }
    }

    // 상태 설정 메서드
    public void SetState(object state)
    {
        if (state is NPCState enumState)
        {
            currentState = enumState;
            HandleStateChange(enumState);
        }
        else if (state is int intState && System.Enum.IsDefined(typeof(NPCState), intState))
        {
            currentState = (NPCState)intState;
            HandleStateChange(currentState);
        }
        else if (state is string stringState && System.Enum.TryParse(stringState, out NPCState parsedState))
        {
            currentState = parsedState;
            HandleStateChange(currentState);
        }
        else
        {
            Debug.LogError("Invalid state provided to SetState");
        }
    }

    private void HandleStateChange(NPCState newState)
    {
        switch (newState)
        {
            case NPCState.Peaceful:
                OpenChatUI();
                CloseStoreUI();
                break;
            case NPCState.Trading:
                CloseChatUI();
                OpenStoreUI();
                break;
            case NPCState.Attacking:
                CloseChatUI();
                CloseStoreUI();
                break;
            case NPCState.Following:
                CloseChatUI();
                CloseStoreUI();
                break;
        }
    }

    // 0 : 평화로운 상태
    public void StayIdle()
    {
        // 평화 상태의 행동 (애니메이션 또는 기타 동작 추가 가능)
    }

    // 1 : 플레이어와 거래하는 상태
    public void StartTrade()
    {
        // 거래 상태에서 별도의 FixedUpdate 처리 없이 UI 관리만
    }

    // 2 : 플레이어를 공격하는 상태
    public void AttackPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * followSpeed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);

        // 플레이어와의 거리 계산 후 공격 애니메이션 트리거 추가 가능
    }

    // 3 : 플레이어를 따라가는 상태
    public void FollowPlayer()
    {
        if (!isFollowing) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stoppingDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 newPosition = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
            transform.position = newPosition;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);
        }
    }

    #region Open / Close UI
    private void OpenChatUI()
    {
        playerInputField.gameObject.SetActive(true);
        goblinInputField.gameObject.SetActive(true);
    }

    private void CloseChatUI()
    {
        playerInputField.gameObject.SetActive(false);
        goblinInputField.gameObject.SetActive(false);
    }

    private void OpenStoreUI()
    {
        storePanel.SetActive(true);
    }

    private void CloseStoreUI()
    {
        storePanel.SetActive(false);
    }
    #endregion
}
