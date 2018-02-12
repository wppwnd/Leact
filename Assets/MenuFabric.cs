using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFabric : MonoBehaviour {

	public GameObject plainMenu;

	private RMF_RadialMenu active=null;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public RMF_RadialMenu getRootMenu(Transform parent){
		destroyPrev ();
		GameObject newMenu = Instantiate (plainMenu, parent);

		RMF_RadialMenu radialManu = newMenu.GetComponent<RMF_RadialMenu> ();

		radialManu.id = 0;

		radialManu.setItemText (1, "1");

		radialManu.setItemText (2, "2");
		radialManu.setItemText (3, "3");
		radialManu.setItemText (4, "4");
		active = radialManu;
		return radialManu;

	
	}


	public RMF_RadialMenu getAMenu(Transform parent){
		destroyPrev ();
		GameObject newMenu = Instantiate (plainMenu, parent);

		RMF_RadialMenu radialManu = newMenu.GetComponent<RMF_RadialMenu> ();

		radialManu.id = 1;

		radialManu.setItemText (1, "1.1");
		radialManu.setItemText (2, "1.2");
		radialManu.setItemText (3, "1.3");
		radialManu.setItemText (4, "1.4");
		active = radialManu;
		return radialManu;
	}

	public RMF_RadialMenu getBMenu(Transform parent){
		destroyPrev ();
		GameObject newMenu = Instantiate (plainMenu, parent);

		RMF_RadialMenu radialManu = newMenu.GetComponent<RMF_RadialMenu> ();

		radialManu.id = 2;
		radialManu.setItemText (1, "2.1");
		radialManu.setItemText (2, "2.2");
		radialManu.setItemText (3, "2.3");
		radialManu.setItemText (4, "2.4");
		active = radialManu;
		return radialManu;
	}

	public RMF_RadialMenu getCMenu(Transform parent){
		destroyPrev ();
		GameObject newMenu = Instantiate (plainMenu, parent);

		RMF_RadialMenu radialManu = newMenu.GetComponent<RMF_RadialMenu> ();

		radialManu.id = 3;
		radialManu.setItemText (1, "3.1");
		radialManu.setItemText (2, "3.2");
		radialManu.setItemText (3, "3.3");
		radialManu.setItemText (4, "3.4");
		active = radialManu;
		return radialManu;
	}

	public RMF_RadialMenu getDMenu(Transform parent){
		destroyPrev ();

		GameObject newMenu = Instantiate (plainMenu, parent);

		RMF_RadialMenu radialManu = newMenu.GetComponent<RMF_RadialMenu> ();

		radialManu.id = 4;
		radialManu.setItemText (1, "4.1");
		radialManu.setItemText (2, "4.2");
		radialManu.setItemText (3, "4.3");
		radialManu.setItemText (4, "4.4");
		active = radialManu;
		return radialManu;
	}

	private void destroyPrev(){
		if (active != null) {
			GameObject.Destroy (active.gameObject);
		}
	}

	public RMF_RadialMenu defaultMenu(Transform parent){
		return getRootMenu (parent);
	}

	public RMF_RadialMenu next(int menuID, int selectedItem, Transform parent){
	//gets the Successor for the menuID
		//this means a shitTon of if's or switch/cases

		//this means the current menu is the rootMenu
		if (menuID == 0) {
			switch (selectedItem) {
			case 1:
				//this means back to last menu, so it will be ignored
				break;
			case 2:
				return menu (1, parent);
				break;
			case 3:
				return  menu (2, parent);
				break;
			case 4:
				return  menu (3, parent);
				break;
			default:
				return defaultMenu (parent);
				break;
			}
		
		}
		return defaultMenu (parent);
	}
	public RMF_RadialMenu menu(int menuID, Transform parent){

		switch(menuID){
		case 0:
			return defaultMenu (parent);
			break;
		case 1:
			return getAMenu (parent);
			break;
		case 2:
			return getBMenu (parent);
			break;
		case 3:
			return getCMenu (parent);
			break;
		default:
			return defaultMenu(parent);
			break;
		}

//		return null;

	}
}
