using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : TSingletonMono<BackgroundManager>
{
    #region Fields
    public bool IsBGMove { get; set; } = true;
    public float BGSpeed { get; set; } = 3;

    Transform[] backgrounds = new Transform[3];
    int startIndex;
    int endIndex;
    float viewHeight;
    #endregion

    #region Init Method
    protected override void OnInitialize()
    {
        viewHeight = Camera.main.orthographicSize * 2;
        GenerateBG();
        startIndex = 0;
        endIndex = backgrounds.Length - 1;
        IsLoad = true;
    }
    #endregion

    #region Unity Method
    private void Update()
    {
        if (IsBGMove==false) return;
        MoveBG();
    }
#endregion

    #region BackGround Method   
    void GenerateBG()
    {
        var origin = Resources.Load<Transform>("Prefabs/Background");
        for(int i=0;i<3;i++ )
        {
            var bg=Instantiate(origin,this.transform);
            bg.name = "Background_" + (i + 1);
            bg.localPosition = Vector3.up * (viewHeight - (i* viewHeight));
            backgrounds[i] = bg;
        }
    }
    void MoveBG()
    {
        transform.Translate(Vector3.down * BGSpeed * Time.deltaTime);

        if (backgrounds[endIndex].position.y < -viewHeight)
            ResetBG();
    }
    void ResetBG()
    {
        Vector3 newPos = backgrounds[startIndex].localPosition + Vector3.up * viewHeight;
        backgrounds[endIndex].localPosition = newPos;

        startIndex = endIndex;
        endIndex = (endIndex == 0) ? backgrounds.Length - 1 : (endIndex - 1);
    }
    #endregion
}
