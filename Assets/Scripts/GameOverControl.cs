using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Scene관련 기능을 사용하기 위해 필요한 네임스페이스 선언하는 구문입니다.
using UnityEngine.SceneManagement;

//게임오버씬에서 "Stage1"버튼이 클릭되었을 때 호출되는 "Restart"메서드를 가지고 있는 GameOverControl 클래스입니다.
public class GameOverControl : MonoBehaviour
{
    //public으로 선언되어 외부에서 접근 가능합니다. 
    public void Restart()
    {
        //'GameMaster'클래스의'instance'를 통해 게임 관리자의 인스턴스에 접근하여 'ResetGame'메서드를 호출합니다.
        //이를 통해 게임을 재설정하고 다시 시작할 수 있습니다.
        GameMaster.instance.ResetGame();
    }
}
