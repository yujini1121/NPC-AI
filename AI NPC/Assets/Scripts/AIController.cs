using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class AIController : MonoBehaviour
{
    /*array_text_function_descriptions_ko = [
    "이 함수는 NPC가 평화로운 상태일 때 호출합니다.",
    "이 함수는 NPC가 플레이어와 거래를 할 때 호출합니다.",
    "이 함수는 NPC가 플레이어를 공격하려고 할때 호출합니다.",
    "이 함수는 NPC가 플레이어를 따라다니려고 할 때 호출합니다."]*/

    [Header("Check & Assign")]
    [SerializeField] private int inputNumber;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject playerInputField;
    [SerializeField] private GameObject goblinInputField;
    [SerializeField] private GameObject tradePanel;

    [Header("Follow")]
    [SerializeField] private float followSpeed;
    [SerializeField] private float stoppingDistance; // 플레이어와 유지할 최소 거리

    [Header("Attack")]
    [SerializeField] private GameObject stonePrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float attackInterval = 3f; // 공격 간격 (초)
    private float lastAttackTime = 0f; // 마지막 공격 시간


    private void FixedUpdate()
    {
        switch (inputNumber)
        {
            case 0:
                StayIdle();
                break;
            case 1:
                StartTrade();
                break;
            case 2:
                AttackPlayer();
                break;
            case 3:
                FollowPlayer();
                break;
            default:
                Debug.LogError("IndexOutOfRange: inputNumber를 0~3으로 맞춰주세요.");
                break;
        }    
    }


    // 0 : 평화로운 상태
    public void StayIdle()
    {
        CloseTradeUI();
    }

    // 1 : 플레이어와 거래하는 상태
    public void StartTrade()
    {
        OpenTradeUI();
    }

    // 2 : 플레이어를 공격하는 상태
    public void AttackPlayer()
    {
        CloseTradeUI();

        if (Time.time < lastAttackTime + attackInterval) return;    // 공격 간격 체크
        lastAttackTime = Time.time;                                 // 마지막 공격 시간 갱신

        if (player == null || stonePrefab == null || throwPoint == null)
        {
            Debug.LogWarning("AttackPlayer() 호출 실패");
            return;
        }

        GameObject rock = Instantiate(stonePrefab, throwPoint.position, Quaternion.identity);
        Vector3 direction = (player.position - throwPoint.position).normalized;

        Rigidbody rockRb = rock.GetComponent<Rigidbody>();
        if (rockRb != null)
        {
            rockRb.AddForce(direction * throwForce, ForceMode.Impulse);
        }
    }

    // 3 : 플레이어를 따라가는 상태
    public void FollowPlayer()
    {
        CloseTradeUI();


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
    void OpenTradeUI()
    {
        tradePanel.SetActive(true);
    }

    void CloseTradeUI()
    {
        tradePanel.SetActive(false);
    }
    #endregion
}