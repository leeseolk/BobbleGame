using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Scene���� ����� ����ϱ� ���� �ʿ��� ���ӽ����̽� �����ϴ� �����Դϴ�.
using UnityEngine.SceneManagement;

public class begin : MonoBehaviour
{
    //���� �̸��տ� '_'(������ھ�)�� ����� ������ �������� ���̱� ���� ����Ͽ����ϴ�.
    //��� �ð��� �����ϴ� �����Դϴ�.
    private float _time;
    //������ �Ͻ� ���� �������� ��Ÿ���� bool�����Դϴ�.
    //�Ͻ�����, ��� �ΰ��� ���� ������ ���� 'bool'Ÿ�� ������ ����߽��ϴ�.
    //������ �������� Ȯ���Ͽ� ������ ������ �� �ֽ��ϴ�.
    private bool _isPaused = false;
    //'AudioSource' ������Ʈ�� ���� ��� ���ӿ�����Ʈ�� �迭�Դϴ�.
    //Scene�� �ִ� ����� �ҽ��� �������� ���� ����Ͽ����ϴ�.
    private AudioSource[] _audioSources;

    void Start()
    {
        // ��� AudioSource ������Ʈ�� �����ɴϴ�.
        //��� ������ҽ� ������Ʈ�� '_audioSources' �迭�� �����մϴ�.
        _audioSources = FindObjectsOfType<AudioSource>();
    }

    void Update()
    {
        //����� �ҽ� ���� ����� PauseAudioSources()�� ResumeAudioSources() �޼��忡�� ó���˴ϴ�.
        //��Ʈ�� �κ��� �Ͻ����� �ϱ� ���� �ۼ��Ͽ����ϴ�.
        //���� Ű������ 'p'Ű�� �������� ��
        if (Input.GetKeyDown(KeyCode.P))
        {
            //'_isPaused'������ ������ŵ�ϴ�. ��, �Ͻ����� ���¿� ��� ���¸� �����ư��� ��ȯ�մϴ�.
            _isPaused = !_isPaused;
            //'_isPaused'������ 'true'�� ���(�Ͻ����� ������ ���)
            if (_isPaused)
            {
                //���� �ð��� �帧�� �Ͻ� ������ŵ�ϴ�. ��, ������ �Ͻ����� �˴ϴ�.
                Time.timeScale = 0f;
                //�°� �۵��ϴ��� Ȯ���ϱ� ���� �ֿܼ� "�Ͻ�����"��� �޽����� ����ϵ��� �߽��ϴ�.
                Debug.Log("�Ͻ�����");
                //'PauseAudioSources()'�޼��带 ȣ���Ͽ� ������ҽ� ���� �Ͻ� ���� ��ŵ�ϴ�.
                PauseAudioSources();
            }
            //'_isPaused'������ 'false'�� ���(��� ������ ���)
            else
            {
                //���ӽð��� �帧�� ���� �ӵ��� �簳�մϴ�.
                //�� �κ��� ���� �����Ͽ� �ӵ��� �����ų� ������ �� �� �ֽ��ϴ�.
                Time.timeScale = 1f;
                //�°� ����Ǵ��� Ȯ���ϱ� ���� �ֿܼ� "���"�̶�� �޽����� ����մϴ�.
                Debug.Log("���");
                //' ResumeAudioSources()'�޼��带 ȣ���Ͽ� �Ͻ������� ����� �ҽ��� �簳��ŵ�ϴ�.
                ResumeAudioSources();
            }
        }
        //���� ������ �Ͻ����� ������ ���, �Ʒ��� ������ ó������ �ʰ�, Update()�޼��带 �����մϴ�.
        if (_isPaused)
            return;
        //�������� �ʴ´ٸ� ��Ʈ�� �κ��� 8�ʵ��� �����ְ� �������� 1���� �Ѿ�ϴ�.
        //'_time' ������ ����� �ð��� �����մϴ�.
        //'Time.deltaTime'�� ���� �����Ӻ��� ���� �����ӱ��� �ɸ� �ð��� �ǹ��մϴ�.
        _time += Time.deltaTime;
        //���� '_time' ������ 8���� ũ�ٸ�
        if (_time > 5)
        {
            //"Stage1"�̶�� ���� �ε��մϴ�. ���� 8�ʰ� ������ "Stage1"������ �̵��ϰ� �˴ϴ�.
            SceneManager.LoadScene("Stage1");
        }
    }

   
    //'PauseAudioSources()' �Լ��� '_audioSources' �迭�� ����� ��� 'AudioSource' ������Ʈ�� �Ͻ�������ŵ�ϴ�.
    private void PauseAudioSources()
    {
        //'foreach'�ݺ����� ����Ͽ� '_audioSources' �迭�� ����� �� 'AudioSource' ������Ʈ�� ���ʷ� �����մϴ�.
        //var�� "variable'�� ���Ӹ��� �Ͻ��� Ÿ�� ���� �����Դϴ�. �� ����� �������� �����ϴ� �� �Դϴ�.
        //in�� 'foreach'������ Ű����� ���˴ϴ�. 'in'�������� �ݺ��� �÷����̳� �迭�� �����մϴ�.
        foreach (var audioSource in _audioSources)
        {
            //�ݺ��� ���ο����� ���� �ݺ����� 'audioSource'�� ������ 'Pause()' �Լ��� ȣ���Ͽ� ������ҽ��� �Ͻ�������ŵ�ϴ�.
            audioSource.Pause();
        }
    }

    //���� �ݴ�� �Ͻ������� ������ҽ��� �ٽ� �����ŵ�ϴ�.
    private void ResumeAudioSources()
    {
        // ��� AudioSource ������Ʈ�� ���
        foreach (var audioSource in _audioSources)
        {
            audioSource.UnPause();
        }
    }
}
