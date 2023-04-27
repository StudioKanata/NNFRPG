using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //�V���O���g��
    //�Q�[������1�������݂��Ȃ�����(�����Ǘ�������̂Ȃ�)
    //���p�ꏊ : �V�[���Ԃł̃f�[�^���L/�I�u�W�F�N�g���L
    //������
    public static SoundManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // --�V���O���g���I���--

    public AudioSource audioSourceBGM; //BGM�̃X�s�[�J�[
    public AudioClip [] audioClipsBGM; //BGM�̑f��(0:Title, 1:Town, 2:Quest, 3:Battle)

    public AudioSource audioSourceSE; // SE�̃X�s�[�J
    public AudioClip [] audioClipsSE; // �炷�f��

    public void StopBGM()
    {
        audioSourceBGM.Stop();
    }

    public void PlayBGM(string sceneName)
    {
        audioSourceBGM.Stop();
        switch (sceneName)
        {
            default:
            case "Title":
                audioSourceBGM.clip = audioClipsBGM[0];
                break;
            case "Town":
                audioSourceBGM.clip = audioClipsBGM[1];
                break;
            case "Quest":
                audioSourceBGM.clip = audioClipsBGM[2];
                break;
            case "Battle":
                audioSourceBGM.clip = audioClipsBGM[3];
                break;
        }
        audioSourceBGM.Play();
    }
    public void PlaySE(int index)
    {
        audioSourceSE.PlayOneShot(audioClipsSE[index]); // SE����x�Ȃ炷
    }
}