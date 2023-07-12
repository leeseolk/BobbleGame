using System;
using System.Collections;
using System.Collections.Generic;
//�� ���� �����̽��� TextMeshPro ���� ��ɿ� �����ϴ� �� ���˴ϴ�. 
using TMPro;
using UnityEngine;
//Scene���� ����� ����ϱ� ���� �ʿ��� ���ӽ����̽� �����ϴ� �����Դϴ�.
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //����Ű�� ������ ������ �ٽ� �����ϰų� �ٸ� ������ �Ѿ�� ���� �ش� ��ũ��Ʈ�� ����Ͽ����ϴ�.
    //Ű �Է��� �����ϰ� �ش� Ű�� ���� ������ �����մϴ�.
    void Update()
    {
        //'Input.GetKey' �Լ��� ����Ͽ� "Return"Ű(Enter Ű)�� ���ȴ����� Ȯ���մϴ�.
        if (Input.GetKey(KeyCode.Return))
        {
            //'SceneManager.LoadScene' �Լ��� ����Ͽ� "Openning"�̶�� ���� �ε��մϴ�.
            SceneManager.LoadScene("Openning");
        }
    }
}

/* �ؽ�Ʈ�� ���� �����ϰ� �ð��� ȿ���� �ֱ� ���� TextMeshPro �ؽ�Ʈ �ַ���� ����Ͽ����ϴ�.
 * ���� �ȼ� ��Ʈ�� ����ϰ�, ����, Outline����, �ִϸ��̼� ȿ���� �־����ϴ�. */
