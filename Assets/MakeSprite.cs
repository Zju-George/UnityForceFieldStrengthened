using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MakeSprite : MonoBehaviour
{
    SpriteRenderer sr;

    public Texture2D hexgon;
    public Texture2D leftbottom;
    public Texture2D lefttop;
    public Texture2D rightbottom;
    public Texture2D righttop;
    Color[] hexgonArray;
    Color[] leftbottomArray;
    Color[] lefttopArray;
    Color[] rightbottomArray;
    Color[] righttopArray;
    Texture2D dest;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        hexgonArray = hexgon.GetPixels();
        leftbottomArray = leftbottom.GetPixels();
        lefttopArray = lefttop.GetPixels();
        rightbottomArray = rightbottom.GetPixels();
        righttopArray = righttop.GetPixels();
        //一个bigBlock的宽和高分别是60，34
        dest = new Texture2D(60*9, 34*15);
        //set base Hex
        for(int j=0;j<15;j++)
        {
            for (int i = 0; i < 9; i++)
            {
                SetHexgon(60 * i, 34*j);
            }
        }
        //set interval Hex
        for (int j = 0; j < 14; j++)
        {
            for (int i = 0; i < 8; i++)
            {
                SetHexgon(30+60*i,17+34*j);
            }
        }
        //set 4 Corners
        float cornerT = Random.Range(0f, 1f);
        MySetPixels(0, 0, leftbottom.width, leftbottom.height, leftbottomArray, cornerT);
        MySetPixels(0, 34*15-17, lefttop.width, lefttop.height, lefttopArray, cornerT);
        MySetPixels(60 * 9-20, 0, rightbottom.width, rightbottom.height, rightbottomArray, cornerT);
        MySetPixels(60 * 9 - 20,34 * 15 - 17, righttop.width, righttop.height, righttopArray, cornerT);
        //set bottom Side
        for(int i=0;i<8;i++)
        {
            SetBotSide(40+60*i, 0);
        }
        //set left side
        for(int i=0;i<14;i++)
        {
            SetLeftSide(0, 17 + 34 * i);
        }
        //set top side
        for (int i = 0; i < 8; i++)
        {
            SetTopSide(40 + 60 * i, 34 * 15 - 17);
        }
        //set right side
        for (int i = 0; i < 14; i++)
        {
            SetRightSide(60 * 9-20, 17 + 34 * i);
        }
        dest.Apply();

        dest.wrapMode = TextureWrapMode.Clamp;

        SavePng_Sprite();
    }

    private void SetRightSide(int x, int y)
    {
        float t = Random.Range(0f, 1f);
        MySetPixels(x , y, righttop.width, righttop.height, righttopArray, t);
        MySetPixels(x, y+17, rightbottom.width, rightbottom.height, rightbottomArray, t);
    }

    void SetHexgon(int x,int y)
    {
        float t = Random.Range(0f, 1f);
        //MySetPixels(x, y, leftbottom.width, leftbottom.height, leftbottomArray,t);
        MySetPixels(x+10, y, hexgon.width, hexgon.height, hexgonArray,t);
        //MySetPixels(x, y+17, lefttop.width, lefttop.height, lefttopArray,t);
        //MySetPixels(x+40, y, rightbottom.width, rightbottom.height, rightbottomArray,t);
        //MySetPixels(x+40, y+17, righttop.width, righttop.height, righttopArray,t);
    }
    void SetBotSide(int x,int y)
    {
        float t = Random.Range(0f, 1f);
        MySetPixels(x, y, rightbottom.width, rightbottom.height, rightbottomArray, t);
        MySetPixels(x+20, y, leftbottom.width, leftbottom.height, leftbottomArray, t);
    }
    void SetLeftSide(int x, int y)
    {
        float t = Random.Range(0f, 1f);
        MySetPixels(x, y, lefttop.width, lefttop.height, lefttopArray, t);
        MySetPixels(x, y+17, leftbottom.width, leftbottom.height, leftbottomArray, t);
    }
    void SetTopSide(int x,int y)
    {
        float t = Random.Range(0f, 1f);
        MySetPixels(x, y, righttop.width, righttop.height, righttopArray, t);
        MySetPixels(x+20, y, lefttop.width, lefttop.height, lefttopArray, t);
    }
    private void MySetPixels(int x, int y, int blockwidth, int blockheight, Color[] colors,float t)
    {
        for(int i = x;i<x+blockwidth;i++)
        {
            for(int j=y;j<y+blockheight;j++)
            {
                if(colors[i - x + (j - y) * blockwidth].a>0)
                {
                    Color origin = colors[i - x + (j - y) * blockwidth];
                    Color col = new Color(origin.r * t, origin.g * t, origin.b * t, origin.a);
                    dest.SetPixel(i, j, col);
                }
            }
        }
    }
    void SavePng_Sprite()
    {
        byte[] bytes = dest.EncodeToPNG();
        FileStream file = File.Open(Application.dataPath + "/test.png", FileMode.Create);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(bytes);
        file.Close();
        Sprite newSprite = Sprite.Create(dest, new Rect(0, 0, dest.width, dest.height), Vector2.one * 0.5f);
        sr.sprite = newSprite;
    }
}
