using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialManager : MonoBehaviour
{
    public SkinnedMeshRenderer GirlRenderer;

    public SkinnedMeshRenderer ShoeRenderer;

    // Toggle buttons from left(Colors) and right(Textures) scrollbars
    public Toggle [] DressToggle;
    public Toggle [] ColorToggle;

    // Arrays of dress materials to apply
    public Material[] Dress1;
    public Material[] Dress2;
    public Material[] Dress3;
    public Material[] Dress4;
    public Material[] Dress5;
    public Material[] Dress6;

    // shoes material
    public Material[] Shoes;

    // active material indexes
    private int textureIndex = 0;
    private int colorIndex = 0;

    private int shoesIndex = 0;

    //actual materials of the girl
    private Material [] bodyMaterial;

    private Material shoesMaterial;

    private RoomType roomType;


    void Start()
    {
        bodyMaterial = GirlRenderer.materials;
        shoesMaterial = ShoeRenderer.material;

        roomType = RoomType.Dress;
    }

    public void SetRoom(RoomType room)
    {
        roomType = room;
    }

    public void SetActiveToggleShoes()
    {
        ColorToggle[shoesIndex].isOn = true;
    }

    public void SetActiveToggleColor()
    {
        ColorToggle[colorIndex].isOn = true;
    }

    public void SetActiveToggleDress()
    {
        DressToggle[textureIndex].isOn = true;
    }


    // toggle button callback
    public void OnColorChanged(int index)
    {
        SetMaterial(index, false);
    }

    public void OnDressChanged(int index)
    {
        SetMaterial(index, true);
    }

    // Apply  material
    public void SetMaterial(int index, bool isTextureChanged)
    {

        if (roomType == RoomType.Shoes)  // change shoes
        {
            shoesIndex = index;
            shoesMaterial= Shoes[shoesIndex];
            ShoeRenderer.material = shoesMaterial;
            return;
        }

        // Change dress

        if (isTextureChanged)
            textureIndex = index;
        else
            colorIndex = index;


        switch (textureIndex)
        {
            case 0:
                bodyMaterial[1] = Dress1[colorIndex];
                break;

            case 1:
                bodyMaterial[1] = Dress2[colorIndex];
                break;

            case 2:
                bodyMaterial[1] = Dress3[colorIndex];
                break;

            case 3:
                bodyMaterial[1] = Dress4[colorIndex];
                break;

            case 4:
                bodyMaterial[1] = Dress5[colorIndex];
                break;

            case 5:
                bodyMaterial[1] = Dress6[colorIndex];
                break;
        }

        GirlRenderer.materials = bodyMaterial;
    }

    bool IsAnimPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }
}
