using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetColor : MonoBehaviour {

	private SetColor mControllerManager;

	private Image mImgController;

	// Use this for initialization
	void Start () {

		mImgController = gameObject.GetComponent<Image>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setColor(Color32 color){

		mImgController.color = color;

	}

	public Color32 setBlackColor(){

		float r = mImgController.color.r;
		float g = mImgController.color.g;
		float b = mImgController.color.b;

		r -= 0.01f;
		g -= 0.01f;
		b -= 0.01f;

		if(r < 0){
			r = 0;
		}

		if(g < 0){
			g = 0;
		}

		if(b < 0){
			b = 0;
		}

		mImgController.color = new Color32( (byte)(r * 255), (byte)(g * 255), (byte)(b * 255), 255 );

		return mImgController.color;

	}

}