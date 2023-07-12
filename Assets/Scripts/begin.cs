using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Scene관련 기능을 사용하기 위해 필요한 네임스페이스 선언하는 구문입니다.
using UnityEngine.SceneManagement;

public class begin : MonoBehaviour
{
    //변수 이름앞에 '_'(언더스코어)를 사용한 이유는 가독성을 높이기 위해 사용하였습니다.
    //경과 시간을 저장하는 변수입니다.
    private float _time;
    //게임이 일시 정지 상태인지 나타내는 bool변수입니다.
    //일시정지, 재생 두가지 값을 가지기 위해 'bool'타입 변수를 사용했습니다.
    //참인지 거짓인지 확인하여 동작을 수행할 수 있습니다.
    private bool _isPaused = false;
    //'AudioSource' 컴포넌트를 가진 모든 게임오브젝트의 배열입니다.
    //Scene에 있는 오디오 소스를 가져오기 위해 사용하였습니다.
    private AudioSource[] _audioSources;

    void Start()
    {
        // 모든 AudioSource 컴포넌트를 가져옵니다.
        //모든 오디오소스 컴포넌트를 '_audioSources' 배열에 저장합니다.
        _audioSources = FindObjectsOfType<AudioSource>();
    }

    void Update()
    {
        //오디오 소스 관련 기능은 PauseAudioSources()와 ResumeAudioSources() 메서드에서 처리됩니다.
        //인트로 부분을 일시정지 하기 위해 작성하였습니다.
        //만약 키보드의 'p'키가 눌려졌을 때
        if (Input.GetKeyDown(KeyCode.P))
        {
            //'_isPaused'변수를 반전시킵니다. 즉, 일시정지 상태와 재생 상태를 번갈아가며 전환합니다.
            _isPaused = !_isPaused;
            //'_isPaused'변수가 'true'인 경우(일시정지 상태인 경우)
            if (_isPaused)
            {
                //게임 시간의 흐름을 일시 정지시킵니다. 즉, 게임이 일시정지 됩니다.
                Time.timeScale = 0f;
                //맞게 작동하는지 확인하기 위해 콘솔에 "일시정지"라는 메시지를 출력하도록 했습니다.
                Debug.Log("일시정지");
                //'PauseAudioSources()'메서드를 호출하여 오디오소스 또한 일시 정지 시킵니다.
                PauseAudioSources();
            }
            //'_isPaused'변수가 'false'인 경우(재생 상태인 경우)
            else
            {
                //게임시간의 흐름을 정상 속도로 재개합니다.
                //이 부분의 값을 조절하여 속도를 느리거나 빠르게 할 수 있습니다.
                Time.timeScale = 1f;
                //맞게 재생되는지 확인하기 위해 콘솔에 "재생"이라는 메시지를 출력합니다.
                Debug.Log("재생");
                //' ResumeAudioSources()'메서드를 호출하여 일시정지된 오디오 소스를 재개시킵니다.
                ResumeAudioSources();
            }
        }
        //만약 게임이 일시정지 상태인 경우, 아래의 로직을 처리하지 않고, Update()메서드를 종료합니다.
        if (_isPaused)
            return;
        //정지하지 않는다면 인트로 부분은 8초동안 보여주고 스테이지 1으로 넘어갑니다.
        //'_time' 변수에 경과된 시간을 누적합니다.
        //'Time.deltaTime'은 이전 프레임부터 현재 프레임까지 걸린 시간을 의미합니다.
        _time += Time.deltaTime;
        //만약 '_time' 변수가 8보다 크다면
        if (_time > 5)
        {
            //"Stage1"이라는 씬을 로드합니다. 따라서 8초가 지나면 "Stage1"씬으로 이동하게 됩니다.
            SceneManager.LoadScene("Stage1");
        }
    }

   
    //'PauseAudioSources()' 함수는 '_audioSources' 배열에 저장된 모든 'AudioSource' 컴포넌트를 일시정지시킵니다.
    private void PauseAudioSources()
    {
        //'foreach'반복문을 사용하여 '_audioSources' 배열에 저장된 각 'AudioSource' 컴포넌트를 차례로 접근합니다.
        //var는 "variable'의 줄임말로 암시적 타입 지역 변수입니다. 즉 사용할 변수명을 지정하는 것 입니다.
        //in은 'foreach'루프의 키워드로 사용됩니다. 'in'다음에는 반복할 컬렉션이나 배열을 지정합니다.
        foreach (var audioSource in _audioSources)
        {
            //반복문 내부에서는 현재 반복중인 'audioSource'를 가져와 'Pause()' 함수를 호출하여 오디오소스를 일시정지시킵니다.
            audioSource.Pause();
        }
    }

    //위와 반대로 일시정지한 오디오소스를 다시 재생시킵니다.
    private void ResumeAudioSources()
    {
        // 모든 AudioSource 컴포넌트를 재생
        foreach (var audioSource in _audioSources)
        {
            audioSource.UnPause();
        }
    }
}
