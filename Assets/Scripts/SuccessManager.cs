using System;
using System.Collections;
using System.Collections.Generic;
//�� ���� �����̽��� TextMeshPro ���� ��ɿ� �����ϴ� �� ���˴ϴ�. 
using TMPro;
using UnityEngine;
//Scene���� ����� ����ϱ� ���� �ʿ��� ���ӽ����̽� �����ϴ� �����Դϴ�.
using UnityEngine.SceneManagement;

public class SuccessManager : MonoBehaviour
{
    //����Ű�� ������ ������ �ٽ� �����ϰų� �ٸ� ������ �Ѿ�� ���� �ش� ��ũ��Ʈ�� ����Ͽ����ϴ�.
    //Ű �Է��� �����ϰ� �ش� Ű�� ���� ������ �����մϴ�.
    void Update()
    {
        //'Input.GetKey' �Լ��� ����Ͽ� "Return"Ű(Enter Ű)�� ���ȴ����� Ȯ���մϴ�.
        if (Input.GetKey(KeyCode.Return))
        {
            //'SceneManager.LoadScene' �Լ��� ����Ͽ� "Win"�̶�� ���� �ε��մϴ�.
            SceneManager.LoadScene("Win");
        }
    }
}
