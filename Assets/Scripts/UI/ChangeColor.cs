using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] GameObject group;
    [SerializeField] int id;
    [SerializeField] SerializableColor color;
    [SerializeField] SerializableColor mainColor;
    ChangeColor instance;

    [System.Serializable]
    public class SerializableColor
    {

        public float[] colorStore = new float[4] { 1F, 1F, 1F, 1F };
        public Color Color
        {
            get { return new Color(colorStore[0], colorStore[1], colorStore[2], colorStore[3]); }
            set { colorStore = new float[4] { value.r, value.g, value.b, value.a }; }
        }

        //makes this class usable as Color, Color normalColor = mySerializableColor;
        public static implicit operator Color(SerializableColor instance)
        {
            return instance.Color;
        }

        //makes this class assignable by Color, SerializableColor myColor = Color.white;
        public static implicit operator SerializableColor(Color color)
        {
            return new SerializableColor { Color = color };
        }
    }
    public void Change()
    {
        for(int i = 0; i<group.transform.childCount; i++)
        { 
            if(i != id)
            {
                group.transform.GetChild(i).GetComponent<Image>().color = mainColor;
            }
            else if (i == id)
            {
                
                group.transform.GetChild(id).GetComponent<Image>().color = color;
                
            }
        }
       
    }
}
