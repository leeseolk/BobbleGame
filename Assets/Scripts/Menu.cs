using System;
using System.Collections;
using System.Collections.Generic;
//이 네임 스페이스는 TextMeshPro 관련 기능에 접근하는 데 사용됩니다. 
using TMPro;
using UnityEngine;
//Scene관련 기능을 사용하기 위해 필요한 네임스페이스 선언하는 구문입니다.
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //엔터키를 누르고 게임을 다시 시작하거나 다른 씬으로 넘어가기 위해 해당 스크립트를 사용하였습니다.
    //키 입력을 감지하고 해당 키에 대한 동작을 수행합니다.
    void Update()
    {
        //'Input.GetKey' 함수를 사용하여 "Return"키(Enter 키)가 눌렸는지를 확인합니다.
        if (Input.GetKey(KeyCode.Return))
        {
            //'SceneManager.LoadScene' 함수를 사용하여 "Openning"이라는 씬을 로드합니다.
            SceneManager.LoadScene("Openning");
        }
    }
}

/* 텍스트를 좀더 유연하고 시각적 효과를 주기 위해 TextMeshPro 텍스트 솔루션을 사용하였습니다.
 * 게임 픽셀 폰트를 사용하고, 굵기, Outline색상, 애니메이션 효과를 주었습니다. */
