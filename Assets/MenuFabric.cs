using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuFabric : MonoBehaviour {

	public GameObject menu4;
	public GameObject menu3;
	public GameObject menu2;


	private RMF_RadialMenu active=null;

	private Stack<int> menuHistory;

	private Hashtable successor;

	public List<GameObject> spawnObjects = new List<GameObject>();

	private int nullinteger = int.MinValue;

	private float cooldownTime = 0.5f;
	private float cooldownStart;

	private string lastText;
	private RMF_RadialMenuElement lastElem;

	private PointerEventData pointer;

	void Awake(){
		pointer = new PointerEventData(EventSystem.current);
	}

	// Use this for initialization
	void Start () {
		menuHistory = new Stack<int> ();
		menuHistory.Push (0);

		successor = new Hashtable ();


		// int[]   placeholder, elem select 1, elem 2, elem 3, elem 4
		successor.Add (0, new int[]{0,1,2,3,4});
		successor.Add (2, new int[]{0,0,22});
		successor.Add (22, new int[]{0,0,-1,-2,-3});
//		successor.Add (4, new int[]{0,41,42,43,44}); //da müssen noch elemente rein
		
	}
	
	// Update is called once per frame
	void Update () {

		if (lastElem != null & (Time.realtimeSinceStartup - cooldownStart) >= cooldownTime) {

			unhighlightKlick ();
		
		}
		
	}

	public RMF_RadialMenu getRootMenu(Transform parent){
		destroyPrev ();
		GameObject newMenu = Instantiate (menu4, parent);

		RMF_RadialMenu radialManu = newMenu.GetComponent<RMF_RadialMenu> ();

		radialManu.id = 0;

		radialManu.setItemText (1, "1");
		radialManu.setItemText (2, "2");
		radialManu.setItemText (3, "3");
		radialManu.setItemText (4, "4");
		active = radialManu;
		return radialManu;

	
	}


	public RMF_RadialMenu get1Menu(Transform parent){
		destroyPrev ();
		GameObject newMenu = Instantiate (menu4, parent);

		RMF_RadialMenu radialManu = newMenu.GetComponent<RMF_RadialMenu> ();

		radialManu.id = 1;

		radialManu.setItemText (1, "back");
		radialManu.setItemText (2, "1.2");
		radialManu.setItemText (3, "1.3");
		radialManu.setItemText (4, "1.4");
		active = radialManu;
		return radialManu;
	}

	public RMF_RadialMenu get2Menu(Transform parent){
		destroyPrev ();
		GameObject newMenu = Instantiate (menu2, parent);

		RMF_RadialMenu radialManu = newMenu.GetComponent<RMF_RadialMenu> ();

		radialManu.id = 2;
		radialManu.setItemText (1, "back");
		radialManu.setItemText (2, "2.2");
//		radialManu.setItemText (3, "2.3");
//		radialManu.setItemText (4, "2.4");
		active = radialManu;
		return radialManu;
	}

	public RMF_RadialMenu get22Menu(Transform parent){
		destroyPrev ();
		GameObject newMenu = Instantiate (menu4, parent);

		RMF_RadialMenu radialManu = newMenu.GetComponent<RMF_RadialMenu> ();

		radialManu.id = 22;
		radialManu.setItemText (1, "back");
		radialManu.setItemText (2, "Atom");
		radialManu.setItemText (3, "Bombe");
		radialManu.setItemText (4, "Cola");
		active = radialManu;
		return radialManu;
	}



	public RMF_RadialMenu get3Menu(Transform parent){
		destroyPrev ();
		GameObject newMenu = Instantiate (menu3, parent);

		RMF_RadialMenu radialManu = newMenu.GetComponent<RMF_RadialMenu> ();

		radialManu.id = 3;
		radialManu.setItemText (1, "back");
		radialManu.setItemText (2, "3.2");
		radialManu.setItemText (3, "3.3");
//		radialManu.setItemText (4, "3.4");
		active = radialManu;
		return radialManu;
	}

	public RMF_RadialMenu get4Menu(Transform parent){
		destroyPrev ();

		GameObject newMenu = Instantiate (menu4, parent);

		RMF_RadialMenu radialManu = newMenu.GetComponent<RMF_RadialMenu> ();

		radialManu.id = 4;
		radialManu.setItemText (1, "back");
		radialManu.setItemText (2, "4.2");
		radialManu.setItemText (3, "4.3");
		radialManu.setItemText (4, "4.4");
		active = radialManu;
		return radialManu;
	}

	public void destroyPrev(){
		if (active != null) {
			GameObject.Destroy (active.gameObject);
		}
	}

	public RMF_RadialMenu defaultMenu(Transform parent){
		return getRootMenu (parent);
	}

	/**
	 * gets the successor for the menuID base on the "selected" -item. If 
	 * the menu has no successor for the selected item, 0 will be returned
	 * */
	public int next(int menuID, int selected){
		Debug.Log ("MenueFactory:next--> menuID:" + menuID + " selected: " + selected);
		if (successor.ContainsKey (menuID)) {
			int[] tmp =(int[]) successor [menuID];

			if (selected <= tmp.Length) {
				return tmp [selected];
			}
		}
		return nullinteger;
	}
	public RMF_RadialMenu menu(int menuID, Transform parent){

		switch (menuID) {
		case 0:
			menuHistory.Push (0);
			return defaultMenu (parent);
			break;
		case 1:
			menuHistory.Push (1);
			return get1Menu (parent);
			break;
		case 2:
			menuHistory.Push (2);
			return get2Menu (parent);
			break;
		case 3:
			menuHistory.Push (3);
			return get3Menu (parent);
			break;
		case 4:
			menuHistory.Push (4);
			return get4Menu (parent);
			break;
		case 22:
			menuHistory.Push (22);
			return get22Menu (parent);
			break;
		default:
			return defaultMenu (parent);
		
		}
	}
	public RMF_RadialMenu prevMenu(Transform parent){

		Debug.Log ("Stacksize s far: " + menuHistory.Count + " last elem is: " + menuHistory.Peek ());
		if (menuHistory.Count > 1) {
			Debug.Log ("menuHistory popped");
			//popping the pills to get the menu, that was BEFORE menuHistory.Pop, because that needs to be called
			//(not the top of the stack, but the 2nd first)
			menuHistory.Pop ();

			return menu (menuHistory.Pop (), parent);

		}
//		else if (menuHistory.Count == 1) {
//
//
//			return menu (menuHistory.Pop (), parent);
//		} 
		else {
			
			destroyPrev ();
			return null;
		}
	}
	public bool isMenu(int id){
		return id >= 0;
	}
	public bool isObject(int id){
		return id < 0;
	}

	public GameObject getObject(int id){
	
//		int tmpId = id % spawnObjects.Count;
//
//		return spawnObjects [tmpId];

//		switch (id) {
//		case -1:
//			//bombe
//
//			return spawnObjects[0];
//			break;
//
//		case -2:
//			//atom ball
//
//			return spawnObjects [1];
//			break;
//
//		case -3:
//			//cola can
//
//			return spawnObjects [2];
//			break;
//		default:
////			return null;
//
//		}
		if(id<0){
			id = Mathf.Abs (id);

			if (id <= spawnObjects.Count) {
				return spawnObjects [id-1];
			}
		}
		return null;

	}
	public bool isNullInteger(int i){
		return i == nullinteger;
	}
	public RMF_RadialMenu showMenu(Transform parent){
		if (menuHistory.Count == 0) {
			menuHistory.Push (0);
		}
		return menu (menuHistory.Pop (), parent);
	
	}
	public void highlightKlick(int elem){
//		unhighlightKlick ();
//
//	
//		lastElem = active.elements [elem - 1];
//
//		Text elemText = lastElem.GetComponentInChildren<Text> ();
//
//		lastText = elemText.text;
//	
//
//		elemText.text = "click";
//
//		cooldownStart = Time.realtimeSinceStartup;

		ExecuteEvents.Execute(active.elements[elem-1].button.gameObject, pointer, ExecuteEvents.submitHandler);

	
	}
	private void unhighlightKlick(){
	
		if (lastElem != null) {

			Text elemText = lastElem.GetComponentInChildren<Text> ();
			elemText.text = lastText;

			lastElem = null;
		}


	}
}
