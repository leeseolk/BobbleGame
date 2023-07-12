using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어 스크립트 입니다. 이를 통해 플레이어를 제어하고, 이동 및 점프 애니메이션을 적용합니다.
public class PlayerControl : MonoBehaviour
{
    //발사된 총알의 프리팹을 참조하는 게임 오브젝트 변수입니다.
    public GameObject BulletPrefab;
    //총알 발사 사운드 저장하는 게임 오브젝트입니다.
    public GameObject bulletSound;
    //초기화 상태를 나타내는 bool변수입니다.
    private bool IsInit = false;
    //총알 발사 상태를 나타내는 bool변수 입니다. 
    private bool Bobble = false;
    //스크립트의 코드를 수정하지 않고도 인스펙터 창에서 변수값을 조정하여 원하는 동작을 얻기위해[SerializeField] 를 사용하였습니다.
    [SerializeField] float jumpHeight = 2f; // 점프 높이를 조절합니다.
    [SerializeField] float jumpDuration = 0.5f; // 점프 애니메이션의 지속 시간을 조절합니다.
    //점프 애니메이션의 진행시간을 저장하는 변수를 선언하고 초기값을 0으로 설정합니다.
    private float jumpTimer = 0f;
    //점프중인지 여부를 나타내는 변수를 선언하고 'false'로 초기화 합니다.
    private bool isJumping = false;
 
    void Start()
    {
        //이동과 점프 래이메이션을 초기화 하는 메서드입니다.
        // 플레이어 게임오브젝트가 생성된 직후 실행
        InitMotion();
    }

    //플레이어의 초기 위치를 설정합니다.
    /*InitMotion() 메서드에서는 초기 위치 설정과 초기화 시작을 담당합니다.
     * 특정 시간이 지난 후에 초기화를 시작하는 이유는 게임이 시작된 직후에 
     * 플레이어를 바로 이동시키는 것이 아니라, 일정한 대기 시간을 두고 초기화를 시작하기 위해서입니다.
     */
    private void InitMotion()
    {
        //플레이어의 초기 위치를(-16f, 0, 0)으로 설정합니다.
        transform.position = new Vector3(-16f, 0, 0);
        //Invoke는 시정된 시간후에 지정된 메소드를 호출하는 함수입니다.
        //플레이어의 초기 위치 설정이 완료된 후에 'StartInit'메서드를 호출하기 위해서 입니다.
        //1초후에 'StartInit' 메서드를 실행합니다.
        Invoke("StartInit", 1f);
    }

    //'StartInit'메서드를 선언합니다. 초기화를 시작하는데 사용됩니다.
    private void StartInit()
    {
        //'IsInit'변수를 'true'로 설정합니다.
        //이 변수는 초기화 상태를 나타내며, 초기화가 시작되었음을 나타냅니다. 
        IsInit = true;
        //1초후에 "StopInit" 메서드를 실행하는 'Invoke'함수를 사용합니다.
        //초기화가 완료된 후에 "StopInit"메서드를 호출하기 위해 사용됩니다.
        Invoke("StopInit", 1f);
    }

    //
    private void StopInit()
    {
        //'IsInit'변수를 'false'로 설정합니다.
        //이 변수는 초기화 상태를 나타내며, 초기화가 중지되었음을 나타냅니다.
        IsInit = false;
    }

    //플레이어 이동 및 점프에 관한 부분입니다.
    void Update()
    {
        //IsInit이 true인 경우, 이동과 점프 애니메이션을 적용합니다.
        if (IsInit)
        {
            //'moveSpeed'변수는 이동 속도를 조절하는 값으로 0.1로 설정하였습니다.
            float moveSpeed = 0.1f;
            //'yOffset'변수는 플레이어의 y축 위치 조정을 위한 변수입니다.
            float yOffset = 0f;

            // 플레이어 이동
            //'+='연산자는 현재위치 'transform.position'에 이동 백터를 더해 새로운 위치를 구하는 역할을 합니다.
            //'new Vector3(-9.23f, yOffset, 0)'는 이동하고자 하는 목표 위치입니다.
            //'transform.position'는 현재 플레이어의 위치입니다.
            //두 백터 차이를 구하면 플레이어가 목표 위치까지 얼마나 이동해야 하는지 백터로 표현할 수 있습니다.
            //이동속도는 'moveSpeed'값에 의해 조절됩니다.
            transform.position += (new Vector3(-9.23f, yOffset, 0) - transform.position) * moveSpeed;


            // 통통 튀는 점프 애니메이션
            //'isJumping'이 true인 경우, 점프애니메이션을 처리합니다.
            if (isJumping)
            {
                //점프 타이머를 증가시킵니다.
                //'Time.deltaTime'은 이전 프레임 부터 현재 프레임까지의 시간 간격을 나타내며, 점프 애니메이션의 진행시간을 측정하기 위해 사용됩니다.
                jumpTimer += Time.deltaTime;
                //점프 애니메이션의 진행상태를 나타내는 변수 'jumpProgress'를 계산합니다.
                //'jumpDuration'은 점프 애니메이션의 지속 시간을 나타내는 변수입니다.
                float jumpProgress = jumpTimer / jumpDuration;

                // 점프 애니메이션 진행 중인 경우
                //점프 애니메이션의 진행이 완료되지 않는 경우를 체크하는 조건문입니다.
                //jumpProgress가 1보다 작은 경우에만 실행됩니다.
                if (jumpProgress < 1f)
                {
                    //점프 애니메이션에 사용될 곡선 값을 계산합니다. 사인 함수를 이용하여 통통튀는 점프 효과를 나타냈습니다.
                    /* 'jumpProgress'는 점프 애니메이션의 진행 상태를 나타내는 변수이며,0부터 1까지 변화합니다.
                     * 'Mathf.PI'는 원주율(pi)값입니다.
                     * 'jumpProgress'에'Mathf.PI'를 곱해서 점프 애니메이션의 곡선 범위를 조절합니다.
                     * 결론적으로 곡선 값은 y축 위치 조정에 활용되어 점프 동작이 부드럽게 구현됩니다.
                     * */
                    float jumpCurve = Mathf.Sin(jumpProgress * Mathf.PI);
                    //'jumpCurve'는 위에서 계산한 곡선 값이며, 'jumpHeight'는 점프의 높이를 나타내는 변수입니다.
                    //두 값을 곱하여 점프의 높이에 따라 플레이어의 y축 위치를 조정합니다.
                    yOffset = jumpCurve * jumpHeight;
                }
                else
                {
                    // 점프 애니메이션 완료 후 다시 이동 애니메이션으로 전환
                    isJumping = false;
                }
            }
            else
            {
                // 점프 애니메이션을 시작하기 위한 조건
                //x축 위치가 -9보다 작거나 같은 경우에만 실행됩니다.
                if (transform.position.x <= -9f)
                {
                    //점프 애니메이션을 실행하기 위해 'isJumping'변수를 'true'로 설정합니다.
                    isJumping = true;
                    //점프 타이머를 초기화 합니다.
                    //점프 애니메이션을 시작할 때 마다 타이머를 0으로 재설정 하여 애니메이션의 진행 시간을 재측정 합니다.
                    jumpTimer = 0f;
                }
            }
           /* 플레이어가 점프 애니메이션을 진행하고, 애니메이션의 진행에 따라 플레이어의 위치를 조정합니다.
            * 점프 애니메이션이 완료되면 다시 이동 애니메이션으로 전환됩니다.*/

            // 플레이어 위치 및 점프 애니메이션 적용
            transform.position = new Vector3(transform.position.x, yOffset, 0f);
        }

        //플레이어의 동작을 구현하는 부분입니다.
        //키보드의 UpArrow키가 눌렸을 경우
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //위로 0.1f만큼 이동시킵니다.
            transform.Translate(0, 0.1f, 0);

        }
        //만약 DownArrow키가 눌렸을 경우
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //아래로 0.1f만큼 이동시킵니다.
            transform.Translate(0, -0.1f, 0);

        }

        //스페이스 한번에 bullet하나씩 나오도록 코드를 수정하였습니다.
        //만약 스페이스키가 눌리고'Bobble'변수가 false인경우 다음 동작을 실행합니다.
        if (Input.GetKeyDown(KeyCode.Space) && !Bobble)
        {
            //'BulletPrefb'을 현재 플레이어 위치 transform.position에 생성합니다. 이를 통해 총알을 발사하는 효과를 구현합니다.
            Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            //총알이 발사될때마다 사운드가 재생되도록 추가하였습니다.
            //bulletSound 프리펩을 제작하였습니다.
            Instantiate(bulletSound);
            // Bobble변수를 true로 설정하여 총알이 발사되었음을 표시합니다.
            Bobble = true;
        }

        //스페이스키가 떼었을 경우 다음 동작을 실행합니다.
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            // 스페이스 키를 놓으면 다시 총알을 발사할 수 있도록 플래그 리셋
            Bobble = false;
        }
    }
}