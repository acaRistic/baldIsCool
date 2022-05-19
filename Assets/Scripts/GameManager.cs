using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SkinnedMeshRenderer GirlRenderer;
    public UIManager uiManager;
    public MaterialManager materialManager;

    public ParticleSystem ShoesParticles;

    public GameObject Girl;
    public Transform TargetGirl;

    public Transform GirlRotation;


    public Animator animator;


    public Transform CameraShoesTarget;
    public Image FadeImage;

    public Transform cameraStartTrans;
    public Transform CameraFaceTarget;

    private bool isSmileOn;
    private float smileValue;

    private RoomType currentRoom;

    void Start()
    {
        currentRoom = RoomType.Dress;
    }

    private void Update()
    {
        if(isSmileOn)
        {
            MakeSmile(2f);
        }
    }



    public void EnterRoom()
    {

        currentRoom = (currentRoom == RoomType.Dress) ? RoomType.Shoes : RoomType.Dress;

        materialManager.SetRoom(currentRoom);

        uiManager.MoveToRoom(currentRoom);

        if(currentRoom == RoomType.Dress)
        {
            StartCoroutine(SetGirlInDressRoom());
        }
        else if(currentRoom == RoomType.Shoes)
        {
            MoveCameraToFace();

            isSmileOn = true;

            StartCoroutine(SetGirlInShoesRoom());
        }
    }


    public void MoveCameraToShoes()
    {
        Camera.main.transform.DOMove(CameraShoesTarget.position, 0.5f);
        Camera.main.transform.DORotateQuaternion(CameraShoesTarget.rotation, 0.5f);

    }

    public void MoveCameraToStart()
    {
        Camera.main.transform.DOMove(cameraStartTrans.position, -1f);
        Camera.main.transform.DORotateQuaternion(cameraStartTrans.rotation, -1f);

    }

    public void MoveCameraToFace()
    {
        Camera.main.transform.DOMove(CameraFaceTarget.position, 0.5f);
        Camera.main.transform.DORotateQuaternion(CameraFaceTarget.rotation, 0.5f);      
    }

    IEnumerator SetGirlInShoesRoom()
    {
        yield return new WaitForSeconds(2f);

        StopSmile();

        animator.SetBool("isHappy", false);

        Girl.transform.DORotateQuaternion(TargetGirl.rotation, -1f);
        Girl.transform.DOMove(TargetGirl.position, -1f);

        MoveCameraToStart();

        yield return new WaitForSeconds(0.3f);

        materialManager.SetActiveToggleShoes();

        animator.SetBool("turnRight", true);
        RotateGirl();

       yield return new WaitForSeconds(0.5f);

        animator.SetBool("turnRight", false);

        MoveCameraToShoes();

    }

    IEnumerator SetGirlInDressRoom()
    {
        yield return new WaitForSeconds(0.5f);

        materialManager.SetActiveToggleColor();
        materialManager.SetActiveToggleDress();

        animator.SetBool("isHappy", false);
        animator.SetBool("turnRight", false);

        Girl.transform.DORotateQuaternion(TargetGirl.rotation, -1f);
        Girl.transform.DOMove(TargetGirl.position, -1f);

        MoveCameraToStart();

    }

    public void RotateGirl()
    { 
        Girl.transform.DORotateQuaternion(GirlRotation.rotation, 0.5f);
    }

    // toggle button callback
    public void OnColorChanged(int index)
    {
        materialManager.SetMaterial(index, false);

        if (currentRoom == RoomType.Dress )
        {
            if(IsAnimPlaying("Happy") == false)
                StartCoroutine(StartHappyAnim());
        }
        else if(currentRoom == RoomType.Shoes)
        {
            // start particle system
            StartParticles();
        }
    }

    // toggle button callback
    public void OnDressChanged(int index)
    {
        materialManager.SetMaterial(index, true);

        if (IsAnimPlaying("Happy") == false)
        {
            StartCoroutine(StartHappyAnim());
        }
    }

    bool IsAnimPlaying(string stateName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    IEnumerator StartHappyAnim()
    {
        animator.SetBool("isHappy", true);

        yield return new WaitForSeconds(0.2f);

        animator.SetBool("isHappy", false);
    }

    void MakeSmile(float speed)
    {

        smileValue += Time.deltaTime * speed * 100f ;

        GirlRenderer.SetBlendShapeWeight(0, Math.Min(smileValue, 100f));

        if(smileValue > 100f)
        {
            isSmileOn = false;
            smileValue = 0f;
        }

    }

    void StopSmile()
    {
        isSmileOn = false;
        GirlRenderer.SetBlendShapeWeight(0, 0f);

    }

    public void StartParticles()
    {
        if (!ShoesParticles.isPlaying)
        {
            ShoesParticles.Play();
        }
    }
}
