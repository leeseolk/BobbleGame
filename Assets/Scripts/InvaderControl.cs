using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Scene관련 기능을 사용하기 위해 필요한 네임스페이스 선언하는 구문입니다.
using UnityEngine.SceneManagement;

//이 스크립트는 인베이더에 들어가며 프리펩으로 저장해 게임마스터에 저장됩니다.
public class InvaderControl : MonoBehaviour
{
	//플레이어 폭발 사운드 이펙트를 저장하는 게임 오브젝트 입니다.
	public GameObject PlayerExplosionSound;
	//플레이어 폭발 특수 효과를 저장하는 게임 오브젝트 입니다.
	public GameObject PlayerExplosionFX;
	//인베이더 폭발 사운드 이펙트를 저장하는 게임 오브젝트입니다.
	public GameObject InvaderExplosionSound;
	//인베이더 폭발 특수 효과를 저장하는 게임 오브젝트입니다.
	public GameObject InvaderExplosionFX;
	//이 변수는 인베이더가 파괴되었는지를 나타내는 플래그 입니다.
	private bool IsDestroyed = false;
	//이 변수는 인베이더의 이동 방향을 나타내는 플래그입니다. 위로 이동할때는 true, 아래로 이동할때는 false입니다.
	//스테이지 2에서 난이도를 높이기 위해 인베이더가 위 아래로 움직이며 등장하도록 추가하였습니다.
	private bool movingUp = true;
	//인베이더의 이동 속도를 결정하는 값입니다.
	public float movementSpeed = 2f;

	//스크립트가 활성화된 후 최초 한 번만 실행됩니다.
	//여기서 Rigidbody에 힘을 가하여 인베이더를 초기 이동 방향으로 이동시키고, 초기 회전을 설정합니다.
	void Start()
   {
		//스크립트가 연결된 인베이더의 Rigidbody 컴포넌트를 가져와서, AddForce 매서드를 사용하여 힘을 가합니다.
		//x축 방향으로 -900의 힘을 가하여 왼쪽 방향으로 이동하도록 설정하였습니다.
		GetComponent<Rigidbody>().AddForce(-900f, 0, 0);
		//회전을 설정하는 부분입니다. 이미지가 뒤집혀 있어 회전하면서 등장하도록 하였습니다.
		//초기에 y축을 기준으로 90도 회전한 상태로 시작하여 인베이더가 뒤집히지 않고 왼쪽으로 이동합니다.
		transform.rotation = Quaternion.Euler(0, 90, 0);
	}
	//매 프레임마다 호출되는 업데이트 로직이 들어갑니다.
	//Stage2인 경우에만 MoveUpDown 매서드를 호출하여 인베이더의 상하 이동을 처리하도록 했습니다.
	void Update()
	{
		/*우선 유니티 씬의 이름으로 현재 활성화된 씬을 확인할 수 있습니다. 
if       (SceneManager.GetActiveScene().name == "씬 이름")의 형태로 확인 합니다.*/
		//씬의 이름이 "Stage2"와 일치하는 경우에만 조건문 내부의 코드가 실행됩니다.
		if (SceneManager.GetActiveScene().name == "Stage2")
		{
			//MoveUpDown 메서드를 호출합니다. 이를 통해 인베이더는 "Stage2"씬에서만 수직으로 이동하게 됩니다.
			//다른 씬에서는 해당 코드가 실행되지 않습니다.
			MoveUpDown();
		}
	}

	//MoveUpDown메서드로, 인베이더의 수직 이동을 처리하는 부분입니다.
	private void MoveUpDown()
	{
		//변수의 이름을 newYPosition으로 설정했습니다.
		//먼저 현재 인베이더의 y축 위치를 저장하는 변수인 'newYPosition'을 초기화합니다.
		float newYPosition = transform.position.y;
		//인베이더가 위로 이동하는 경우를 처리합니다.
		//'movingUp'변수가 참일때 
		if (movingUp)
		{
			//Time.deltaTime을 이용하면, PC의 성능과는 무관하게 동등한 조건이 되게 되므로 똑같은 결과 값을 내기위해 사용하였습니다.
			//'newYPosition'에 'movementSpeed * Time.deltaTime' 값을 더하여 인베이더를 위로 이동시킵니다.
			newYPosition += movementSpeed * Time.deltaTime;
			//이동한 후 'newYPosition'이 3보다 크거나 같으면
			if (newYPosition >= 3f)
			{
				//'movingUp'변수를 거짓으로 변경하여 하향으로 이동할 수 있도록 합니다.
				movingUp = false;
			}
		}
		//인베이더가 아래로 이동하는 경우를 처리합니다.
		//'movingUp'변수가 거짓일때 
		else
		{
			//'newYPosition'에 'movementSpeed * Time.deltaTime' 값을 빼서 인베이더를 아래로 이동시킵니다.
			newYPosition -= movementSpeed * Time.deltaTime;
			//이동한 후 'newYPosition'이 -7보다 작거나 같으면
			if (newYPosition <= -7f)
			{
				//'movingUp'변수를 참으로 변경하여 상향으로 이동할 수 있도록 합니다.
				movingUp = true;
			}
			//인베이더가 문제없이 실행되는지 확인하기 위래 인베이더1,2를 디버그 로그로 출력하였습니다.
			Debug.Log("Invader");
			Debug.Log("Invader1");
		}
		//인베이더의 위치를 업데이트 하여 실제로 이동시킵니다.
		//현재의 x,y,z값을 'newYPosition'으로 업데이트 하여 x,z축은 그대로 두고 y축만 이동시킵니다.
		transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
	}
	//OnTriggerEnter메서드로, 충돌이 감지되었을 때 동작을 처리합니다.
	/* OnTriggerEnter 함수를 사용하는 경우 두 개의 게임 오브젝트 모두 collider 컴포넌트를 가지고 있어야하고,
	   그 중 하나는 isTtigger가 활성화 되어 있어야 합니다.
	   또 적어도 하나의 게임 오브젝트가 Rigidbody컴포넌트를 가지고 있어야합니다.*/
	/*'Collider other'는 충돌이 감지된 객체를 나타내는 매개변수 입니다.
	   충돌체와 충돌이 발생했을 때 호출되는 콜백 메서드로, 이 때 충돌된 객체 정보를 전달받기 위해 
	   'Collider other'를 매개변수로 사용합니다.*/
	private void OnTriggerEnter(Collider other)
	{
		//디버그 로그를 통해 충돌하는 대상이 Bullet인지 확인합니다.
		Debug.Log("인베이더 충돌 오브젝트 = " + other.gameObject.tag);

		//이 부분은 인베이더가 아직 파괴되지 않았을 때에만 동작을 수행합니다.
		if (!IsDestroyed)
		{
			//인베이더와 플레이어가 충돌했을 때의 동작을 처리합니다.
			if (other.gameObject.tag == "Player")
			{
				//인베이더와 플레이어 오브젝트를 각각 파괴합니다.
				Destroy(gameObject); // Invader
				Destroy(other.gameObject); // Player
				//플레이어 폭발 효과음과 폭발효과를 생성합니다.
				Instantiate(PlayerExplosionSound);
				Instantiate(PlayerExplosionFX, other.transform.position, Quaternion.identity);

				//GameMaster.instance의 OnPlayerDestroy메서드를 호출하여 플레이어 파괴 이벤트를 처리합니다.
				GameMaster.instance.OnPlayerDestroy();
			}
			//인베이더와 총알이 충돌했을 때의 동작을 처리합니다.
			else if (other.gameObject.tag == "Bullet")
			{
				//IsDestroyed변수를 true로 설정하여 인베이더의 파괴를 표시합니다.
				/*중복처리를 방지하기 위한 부분입니다. 
				  충돌 처리가 프레임 단위로 이루어지므로, 같은 충돌이 여러 번 발생할 수 있습니다.
				  'IsDestroyed'변수는 이러한 중복처리를 방지하기 위함입니다. 
				   이를 통해 중복된 충돌 이벤트를 무시하고 단 한번의 처리만 수행하게 됩니다.*/
				IsDestroyed = true;
				//총알과 인베이더 오브젝트를 각각 파괴합니다.
				Destroy(gameObject); // Bullet
				Destroy(other.gameObject); // Invader
				//인베이더 폭발 효과음과 폭발효과를 생성합니다.
				Instantiate(InvaderExplosionSound);
				Instantiate(InvaderExplosionFX, other.transform.position, Quaternion.identity);
				//GameMaster.instance의 AddScore메서드를 호출하여 점수를 증가시킵니다.
				GameMaster.instance.AddScore();
			}
		}
	}
}
