using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bullet에 부착하는 스크립트입니다.
public class Shoot : MonoBehaviour
{
	//'Rigidbody'타입의 'rb'라는 이름의 비공개(접근제한)멤버 변수를 선언합니다.
	//변수 'rb'는 'Rigidbody'컴포넌트의 참조를 저장하기 위해 사용됩니다. 
	//이를 통해 'Rigidbody'에 대한 다양한 동작을 수행할 수 있습니다.
	private Rigidbody rb;
	private void Start()
	{
		//GetComponent메서드를 사용하여 현제 게임 오브젝트에 부착된 'Rigidbody'컴포넌트를 가져와 'rb'변수에 할당하는 코드입니다.
		rb = GetComponent<Rigidbody>();
		//'rb'에 저장된 'Rigidbody'에 힘을 가합니다.
		//'AddForce'함수를 사용하여 300의 힘을 x축 방향으로 가하는 것을 의미합니다.
		rb.AddForce(300f, 0, 0);
	}

}
