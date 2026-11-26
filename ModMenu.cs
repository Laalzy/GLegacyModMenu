using Il2Cpp;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModMenu : MonoBehaviour
{
	public static bool isEnabled;

	private static bool isFadingIn;

	private static bool isFadingOut;

	private static float fadeAlpha;

	private static float fadeDuration;

	private static float fadeSpeed;

	private static GUIStyle buttonStyle;

	private static bool IsStuckEnabled;

	private static bool IsGodmodeEnabled;

	private static bool IsNoClipEnabled;

	private static bool IsTeleportMenuEnabled;

	private static bool IsEspEnabled;

	private static string godmodetext;

	private static string stucktext;

	private static string nocliptext;

	private static string tpmenutext;

	private static string selectobjecttext;

	private static string enemyesptext;

	private static float grannydefaultdistance;

	private static float granpadefaultdistance;

	private static float grannystoppingdistance;

	private static GameObject selectedObject;

	private static GameObject items;

	private static double timeText;

	private static int eyecolor;

	private static string eyecolortext;

	private static string omg;

	public static Texture2D imageLogo;

	public static bool isDragging;

	public static Vector2 dragOffset;

	public static Rect sssss;

	public static int timeDrag;

	public static void Start()
	{
		isEnabled = false;
		fadeSpeed = 1f / fadeDuration;
	}

	public static void LateUpdate()
	{
		if (IsGodmodeEnabled)
		{
			if ((Object)(object)GameObject.Find("Entities/Granny-Stuff/GrannyParent") != (Object)null)
			{
				GameObject.Find("Entities/Granny-Stuff/GrannyParent").GetComponent<AI_Granny>().TimerBedCaught = 0f;
			}
			if ((Object)(object)GameObject.Find("Entities/Grandpa-Stuff/GranpaParent") != (Object)null)
			{
				GameObject.Find("Entities/Grandpa-Stuff/GranpaParent").GetComponent<AI_Grandpa>().TimerBedCaught = 0f;
			}
		}
	}

	public static void SceneLoaded()
	{
	}

	public static void Update()
	{
		if (Paused.IsPaused)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = (float)timeText;
		}
		if (IsNoClipEnabled)
		{
			GameObject gameObject = GameObject.Find("PlayerStuff/Player").gameObject;
			GameObject val = GameObject.Find("PlayerStuff/Player/CameraShakeAnim/CameraPivot/Main Camera");
			gameObject.GetComponent<MobileFPS>().AbleToMove = false;
			if (Input.GetKey((KeyCode)119))
			{
				gameObject.transform.Translate(Vector3.forward * 0.1f, val.transform);
			}
			if (Input.GetKey((KeyCode)115))
			{
				gameObject.transform.Translate(Vector3.back * 0.1f, val.transform);
			}
			if (Input.GetKey((KeyCode)97))
			{
				gameObject.transform.Translate(Vector3.left * 0.1f, val.transform);
			}
			if (Input.GetKey((KeyCode)100))
			{
				gameObject.transform.Translate(Vector3.right * 0.1f, val.transform);
			}
			if (Input.GetKey((KeyCode)101))
			{
				gameObject.transform.Translate(Vector3.up * 0.1f);
			}
			if (Input.GetKey((KeyCode)113))
			{
				gameObject.transform.Translate(Vector3.down * 0.1f);
			}
		}
		Scene activeScene = SceneManager.GetActiveScene();
		if (((Scene)(ref activeScene)).name == "Scene" && IsGodmodeEnabled && (Object)(object)GameObject.Find("Entities/Grandpa-Stuff/GranpaParent") != (Object)null)
		{
			GameObject.Find("Entities/Grandpa-Stuff/GranpaParent").GetComponent<AI_Grandpa>().IsShooting = true;
		}
		activeScene = SceneManager.GetActiveScene();
		if (((Scene)(ref activeScene)).name == "Scene" && IsStuckEnabled)
		{
			if ((Object)(object)GameObject.Find("Entities/Granny-Stuff/GrannyParent") != (Object)null)
			{
				((Behaviour)GameObject.Find("Entities/Granny-Stuff/GrannyParent").GetComponent<AI_Granny>()).enabled = false;
			}
			if ((Object)(object)GameObject.Find("Entities/Grandpa-Stuff/GranpaParent") != (Object)null)
			{
				((Behaviour)GameObject.Find("Entities/Grandpa-Stuff/GranpaParent").GetComponent<AI_Grandpa>()).enabled = false;
			}
			if ((Object)(object)GameObject.Find("Entities/Spider_Mom-Stuff/MomSpiderHeadParent") != (Object)null)
			{
				((Behaviour)GameObject.Find("Entities/Spider_Mom-Stuff/MomSpiderHeadParent").GetComponent<AI_MomSpider>()).enabled = false;
			}
		}
		if (Input.GetKeyDown((KeyCode)277) || Input.GetKeyDown((KeyCode)111))
		{
			isEnabled = !isEnabled;
			if (isEnabled)
			{
				Cursor.lockState = (CursorLockMode)0;
				Cursor.visible = true;
				isFadingIn = true;
				isFadingOut = false;
			}
			else
			{
				isFadingIn = false;
				isFadingOut = true;
			}
			if (!isEnabled && !Paused.IsPaused)
			{
				Cursor.lockState = (CursorLockMode)1;
				Cursor.visible = false;
			}
		}
		if (isFadingIn)
		{
			fadeAlpha = Mathf.Lerp(fadeAlpha, 0.1f, fadeSpeed * Time.deltaTime);
		}
		if (isFadingOut)
		{
			fadeAlpha = Mathf.Lerp(fadeAlpha, 0f, fadeSpeed * Time.deltaTime);
		}
	}

	public static void HandleDrag()
	{
		Event current = Event.current;
		if (current.isMouse && (int)current.type == 0 && ((Rect)(ref sssss)).Contains(current.mousePosition))
		{
			MelonLogger.Msg("WHY NOT OWRK");
			isDragging = true;
			dragOffset = current.mousePosition - new Vector2(((Rect)(ref sssss)).x, ((Rect)(ref sssss)).y);
		}
		if (current.isMouse && (int)current.type == 1 && ((Rect)(ref sssss)).Contains(current.mousePosition))
		{
			isDragging = false;
			isEnabled = !isEnabled;
		}
		if (isDragging && (int)current.type == 3)
		{
			((Rect)(ref sssss)).x = current.mousePosition.x - dragOffset.x;
			((Rect)(ref sssss)).y = current.mousePosition.y - dragOffset.y;
		}
	}

	public static void OnGUI()
	{
		float num = ((Rect)(ref sssss)).x - 0f;
		float num2 = ((Rect)(ref sssss)).y + 50f;
		Event current = Event.current;
		HandleDrag();
		GUI.Button(sssss, "Open Menu");
		if (isEnabled)
		{
			Rect val = new Rect(40f + num, 60f + num2, 210f, 40f);
			Rect val2 = new Rect(40f + num, 104f + num2, 210f, 40f);
			Rect val3 = new Rect(260f + num, 60f + num2, 210f, 40f);
			Rect val4 = new Rect(260f + num, 104f + num2, 210f, 40f);
			Rect val5 = new Rect(40f + num, 60f + num2, 210f, 40f);
			Rect val6 = new Rect(40f + num, 104f + num2, 210f, 40f);
			Rect val7 = new Rect(40f + num, 148f + num2, 210f, 40f);
			Rect val8 = new Rect(40f + num, 192f + num2, 210f, 40f);
			Rect val9 = new Rect(40f + num, 236f + num2, 210f, 40f);
			Rect val10 = new Rect(40f + num, 280f + num2, 210f, 40f);
			Rect val11 = new Rect(40f + num, 324f + num2, 210f, 40f);
			Rect val12 = new Rect(40f + num, 368f + num2, 210f, 40f);
			Rect val13 = new Rect(40f + num, 412f + num2, 210f, 40f);
			Rect val14 = new Rect(40f + num, 456f + num2, 210f, 40f);
			Rect val15 = new Rect(40f + num, 500f + num2, 210f, 40f);
			Rect val16 = new Rect(80f + num, 544f + num2, 130f, 40f);
			Rect val17 = new Rect(40f + num, 544f + num2, 35f, 40f);
			Rect val18 = new Rect(215f + num, 544f + num2, 35f, 40f);
			Rect val19 = new Rect(40f + num, 588f + num2, 210f, 40f);
			Rect val20 = new Rect(40f + num, 632f + num2, 210f, 40f);
			Rect val21 = new Rect(260f + num, 60f + num2, 210f, 40f);
			Rect val22 = new Rect(260f + num, 104f + num2, 210f, 40f);
			Rect val23 = new Rect(30f + num, 0f + num2, 450f, 682f);
			Scene activeScene;
			if (current.isMouse && (int)current.type == 0)
			{
				GameObject val24 = GameObject.Find("PlayerStuff/Player");
				activeScene = SceneManager.GetActiveScene();
				if (((Scene)(ref activeScene)).name == "Menu")
				{
					if (((Rect)(ref val3)).Contains(current.mousePosition))
					{
						PlayerPrefs.SetInt("EX_Door", 1);
						PlayerPrefs.SetInt("Unseen", 1);
						PlayerPrefs.SetInt("EX_Car", 1);
						PlayerPrefs.SetInt("EX_Both", 1);
						PlayerPrefs.SetInt("NoWP", 1);
						PlayerPrefs.SetInt("NoDie", 1);
						PlayerPrefs.SetInt("IM_Both", 1);
						PlayerPrefs.SetInt("EX_Pipe", 1);
						PlayerPrefs.SetInt("EX_Robo", 1);
					}
					if (((Rect)(ref val4)).Contains(current.mousePosition))
					{
						PlayerPrefs.SetInt("EX_Door", 0);
						PlayerPrefs.SetInt("Unseen", 0);
						PlayerPrefs.SetInt("EX_Car", 0);
						PlayerPrefs.SetInt("EX_Both", 0);
						PlayerPrefs.SetInt("NoWP", 0);
						PlayerPrefs.SetInt("NoDie", 0);
						PlayerPrefs.SetInt("IM_Both", 0);
						PlayerPrefs.SetInt("EX_Pipe", 0);
						PlayerPrefs.SetInt("EX_Robo", 0);
					}
					if (((Rect)(ref val)).Contains(current.mousePosition))
					{
						PlayerPrefs.SetFloat("Coins", 1E+09f);
					}
					if (((Rect)(ref val2)).Contains(current.mousePosition))
					{
						PlayerPrefs.SetFloat("Coins", 0f);
					}
				}
				else
				{
					activeScene = SceneManager.GetActiveScene();
					if (((Scene)(ref activeScene)).name == "Scene")
					{
						if (((Rect)(ref val22)).Contains(current.mousePosition))
						{
							IsEspEnabled = !IsEspEnabled;
							if (IsEspEnabled)
							{
								enemyesptext = "Enemy ESP: ON";
								if ((Object)(object)GameObject.Find("Entities/Granny-Stuff/GrannyParent") != (Object)null)
								{
									GameObject.Find("Entities/Granny-Stuff/GrannyParent").AddComponent<Outline>();
								}
								if ((Object)(object)GameObject.Find("Entities/Grandpa-Stuff/GranpaParent") != (Object)null)
								{
									GameObject.Find("Entities/Grandpa-Stuff/GranpaParent").AddComponent<Outline>();
								}
								if ((Object)(object)GameObject.Find("Entities/Spider_Mom-Stuff/MomSpiderHeadParent") != (Object)null)
								{
									GameObject.Find("Entities/Spider_Mom-Stuff/MomSpiderHeadParent").AddComponent<Outline>();
								}
							}
							else
							{
								enemyesptext = "Enemy ESP: OFF";
								if ((Object)(object)GameObject.Find("Entities/Granny-Stuff/GrannyParent") != (Object)null)
								{
									Object.Destroy((Object)(object)GameObject.Find("Entities/Granny-Stuff/GrannyParent").GetComponent<Outline>());
								}
								if ((Object)(object)GameObject.Find("Entities/Grandpa-Stuff/GranpaParent") != (Object)null)
								{
									Object.Destroy((Object)(object)GameObject.Find("Entities/Grandpa-Stuff/GranpaParent").GetComponent<Outline>());
								}
								if ((Object)(object)GameObject.Find("Entities/Spider_Mom-Stuff/MomSpiderHeadParent") != (Object)null)
								{
									Object.Destroy((Object)(object)GameObject.Find("Entities/Spider_Mom-Stuff/MomSpiderHeadParent").GetComponent<Outline>());
								}
							}
						}
						if (((Rect)(ref val21)).Contains(current.mousePosition))
						{
							if (eyecolor == 7)
							{
								eyecolor = 0;
								eyecolortext = "White";
								GameObject.Find("Entities/Granny-Stuff/GrannyParent/GrannyTest6/SlendrinaMom/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Spine3/Bip01 Neck/Bip01 Head/Eyes").gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f);
							}
							else
							{
								eyecolor++;
								if (eyecolor == 1)
								{
									eyecolortext = "Red";
									GameObject.Find("Entities/Granny-Stuff/GrannyParent/GrannyTest6/SlendrinaMom/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Spine3/Bip01 Neck/Bip01 Head/Eyes").gameObject.GetComponent<Renderer>().material.color = new Color(0.2264151f, 0f, 0f);
								}
								if (eyecolor == 2)
								{
									eyecolortext = "Dark";
									GameObject.Find("Entities/Granny-Stuff/GrannyParent/GrannyTest6/SlendrinaMom/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Spine3/Bip01 Neck/Bip01 Head/Eyes").gameObject.GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f);
								}
								if (eyecolor == 3)
								{
									eyecolortext = "Yellow";
									GameObject.Find("Entities/Granny-Stuff/GrannyParent/GrannyTest6/SlendrinaMom/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Spine3/Bip01 Neck/Bip01 Head/Eyes").gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
								}
								if (eyecolor == 4)
								{
									eyecolortext = "Blue";
									GameObject.Find("Entities/Granny-Stuff/GrannyParent/GrannyTest6/SlendrinaMom/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Spine3/Bip01 Neck/Bip01 Head/Eyes").gameObject.GetComponent<Renderer>().material.color = new Color(0f, 81f / 255f, 1f);
								}
								if (eyecolor == 5)
								{
									eyecolortext = "Cyan";
									GameObject.Find("Entities/Granny-Stuff/GrannyParent/GrannyTest6/SlendrinaMom/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Spine3/Bip01 Neck/Bip01 Head/Eyes").gameObject.GetComponent<Renderer>().material.color = new Color(0f, 14f / 15f, 1f);
								}
								if (eyecolor == 6)
								{
									eyecolortext = "Purple";
									GameObject.Find("Entities/Granny-Stuff/GrannyParent/GrannyTest6/SlendrinaMom/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Spine3/Bip01 Neck/Bip01 Head/Eyes").gameObject.GetComponent<Renderer>().material.color = new Color(72f / 255f, 0f, 1f);
								}
								if (eyecolor == 7)
								{
									eyecolortext = "Pink";
									GameObject.Find("Entities/Granny-Stuff/GrannyParent/GrannyTest6/SlendrinaMom/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Spine3/Bip01 Neck/Bip01 Head/Eyes").gameObject.GetComponent<Renderer>().material.color = new Color(1f, 0f, 14f / 15f);
								}
							}
						}
						if (((Rect)(ref val20)).Contains(current.mousePosition))
						{
							GameObject dropPoint = GameObject.Find("PlayerStuff/HandHoldObjects/DropPoint");
							ItemSpawn component = GameObject.Find("PlayerStuff/Player/CameraShakeAnim/CameraPivot/Main Camera/PickRay").GetComponent<PickRay>().ItemDrop.GetComponent<ItemSpawn>();
							Drop(component.baton, dropPoint);
							Drop(component.battery, dropPoint);
							Drop(component.book, dropPoint);
							Drop(component.carbattery, dropPoint);
							Drop(component.carkey, dropPoint);
							Drop(component.code, dropPoint);
							Drop(component.crossbow, dropPoint);
							Drop(component.cutter, dropPoint);
							Drop(component.data, dropPoint);
							Drop(component.eckey, dropPoint);
							Drop(component.gas, dropPoint);
							Drop(component.hammer, dropPoint);
							Drop(component.kugg1, dropPoint);
							Drop(component.kugg2, dropPoint);
							Drop(component.mas, dropPoint);
							Drop(component.meat, dropPoint);
							Drop(component.melon, dropPoint);
							Drop(component.padlock, dropPoint);
							Drop(component.plank, dropPoint);
							Drop(component.playh, dropPoint);
							Drop(component.plier, dropPoint);
							Drop(component.remote, dropPoint);
							Drop(component.rusty, dropPoint);
							Drop(component.safe, dropPoint);
							Drop(component.screw, dropPoint);
							Drop(component.seed, dropPoint);
							Drop(component.shotgun, dropPoint);
							Drop(component.shotgun2, dropPoint);
							Drop(component.sp1, dropPoint);
							Drop(component.sp2, dropPoint);
							Drop(component.sp3, dropPoint);
							Drop(component.spark, dropPoint);
							Drop(component.special, dropPoint);
							Drop(component.spider, dropPoint);
							Drop(component.spray, dropPoint);
							Drop(component.stick, dropPoint);
							Drop(component.syringe, dropPoint);
							Drop(component.t1, dropPoint);
							Drop(component.t2, dropPoint);
							Drop(component.t3, dropPoint);
							Drop(component.t4, dropPoint);
							Drop(component.teddy, dropPoint);
							Drop(component.text, dropPoint);
							Drop(component.topp, dropPoint);
							Drop(component.vas, dropPoint);
							Drop(component.vas2, dropPoint);
							Drop(component.vas3, dropPoint);
							Drop(component.wheel, dropPoint);
							Drop(component.winch, dropPoint);
							Drop(component.wp, dropPoint);
							Drop(component.wrench, dropPoint);
						}
						if (((Rect)(ref val19)).Contains(current.mousePosition))
						{
							IsTeleportMenuEnabled = !IsTeleportMenuEnabled;
							if (IsTeleportMenuEnabled)
							{
								tpmenutext = "Teleport Menu: ON";
							}
							else
							{
								tpmenutext = "Teleport Menu: OFF";
							}
						}
						if (((Rect)(ref val18)).Contains(current.mousePosition))
						{
							timeText += 0.1;
						}
						if (((Rect)(ref val17)).Contains(current.mousePosition))
						{
							timeText -= 0.1;
						}
						if (((Rect)(ref val15)).Contains(current.mousePosition))
						{
							IsNoClipEnabled = !IsNoClipEnabled;
							if (IsNoClipEnabled)
							{
								nocliptext = "NoClip: ON";
							}
							else
							{
								nocliptext = "NoClip: OFF";
								GameObject gameObject = GameObject.Find("PlayerStuff/Player").gameObject;
								gameObject.GetComponent<MobileFPS>().AbleToMove = true;
							}
						}
						if (((Rect)(ref val14)).Contains(current.mousePosition) && (Object)(object)selectedObject != (Object)null)
						{
							selectedObject.transform.position = GameObject.Find("PlayerStuff/Player").gameObject.transform.position;
						}
						if (((Rect)(ref val11)).Contains(current.mousePosition) && (Object)(object)selectedObject != (Object)null)
						{
							selectedObject.transform.rotation = new Quaternion(selectedObject.transform.rotation.x + 90f, selectedObject.transform.rotation.y, selectedObject.transform.rotation.z, selectedObject.transform.rotation.w);
						}
						if (((Rect)(ref val12)).Contains(current.mousePosition) && (Object)(object)selectedObject != (Object)null)
						{
							selectedObject.transform.rotation = new Quaternion(selectedObject.transform.rotation.x, selectedObject.transform.rotation.y + 90f, selectedObject.transform.rotation.z, selectedObject.transform.rotation.w);
						}
						if (((Rect)(ref val13)).Contains(current.mousePosition) && (Object)(object)selectedObject != (Object)null)
						{
							selectedObject.transform.rotation = new Quaternion(selectedObject.transform.rotation.x, selectedObject.transform.rotation.y, selectedObject.transform.rotation.z + 90f, selectedObject.transform.rotation.w);
						}
						if (((Rect)(ref val9)).Contains(current.mousePosition) && (Object)(object)selectedObject != (Object)null)
						{
							Object.Destroy((Object)(object)selectedObject);
						}
						if (((Rect)(ref val10)).Contains(current.mousePosition) && (Object)(object)selectedObject != (Object)null)
						{
							Object.Instantiate<GameObject>(selectedObject, selectedObject.transform.position, selectedObject.transform.rotation);
						}
						if (((Rect)(ref val6)).Contains(current.mousePosition))
						{
							IsGodmodeEnabled = !IsGodmodeEnabled;
							MelonLogger.Msg("OMG GODMODE TRIGGWER");
							if (IsGodmodeEnabled)
							{
								godmodetext = "God Mode: ON";
								if ((Object)(object)GameObject.Find("Entities/Granny-Stuff/GrannyParent") != (Object)null)
								{
									grannydefaultdistance = GameObject.Find("Entities/Granny-Stuff/GrannyParent").GetComponent<AI_Granny>().DistanceJumpscare;
									GameObject.Find("Entities/Granny-Stuff/GrannyParent").GetComponent<AI_Granny>().DistanceJumpscare = 0f;
									grannystoppingdistance = GameObject.Find("Entities/Granny-Stuff/GrannyParent").GetComponent<AI_Granny>().DistanceFromNav;
									GameObject.Find("Entities/Granny-Stuff/GrannyParent").GetComponent<AI_Granny>().DistanceFromNav = 3f;
								}
								if ((Object)(object)GameObject.Find("Entities/Grandpa-Stuff/GranpaParent") != (Object)null)
								{
									granpadefaultdistance = GameObject.Find("Entities/Grandpa-Stuff/GranpaParent").GetComponent<AI_Grandpa>().DistanceJumpscare;
									GameObject.Find("Entities/Grandpa-Stuff/GranpaParent").GetComponent<AI_Grandpa>().DistanceJumpscare = 0f;
									GameObject.Find("Entities/Grandpa-Stuff/GranpaParent").GetComponent<AI_Grandpa>().IsShooting = true;
								}
								if ((Object)(object)GameObject.Find("Entities/Spider_Mom-Stuff/MomSpiderHeadParent") != (Object)null)
								{
									GameObject.Find("Entities/Spider_Mom-Stuff/MomSpiderHeadParent").GetComponent<AI_MomSpider>().IsEating = true;
								}
							}
							else
							{
								godmodetext = "God Mode: OFF";
								if ((Object)(object)GameObject.Find("Entities/Granny-Stuff/GrannyParent") != (Object)null)
								{
									GameObject.Find("Entities/Granny-Stuff/GrannyParent").GetComponent<AI_Granny>().DistanceJumpscare = grannydefaultdistance;
									GameObject.Find("Entities/Granny-Stuff/GrannyParent").GetComponent<AI_Granny>().DistanceFromPlayer = grannystoppingdistance;
									GameObject.Find("Entities/Granny-Stuff/GrannyParent").GetComponent<AI_Granny>().TimerBedCaught = 0f;
								}
								if ((Object)(object)GameObject.Find("Entities/Grandpa-Stuff/GranpaParent") != (Object)null)
								{
									GameObject.Find("Entities/Grandpa-Stuff/GranpaParent").GetComponent<AI_Grandpa>().DistanceJumpscare = granpadefaultdistance;
									GameObject.Find("Entities/Grandpa-Stuff/GranpaParent").GetComponent<AI_Grandpa>().IsShooting = false;
								}
								if ((Object)(object)GameObject.Find("Entities/Spider_Mom-Stuff/MomSpiderHeadParent") != (Object)null)
								{
									GameObject.Find("Entities/Spider_Mom-Stuff/MomSpiderHeadParent").GetComponent<AI_MomSpider>().IsEating = false;
								}
							}
						}
						if (((Rect)(ref val8)).Contains(current.mousePosition))
						{
							GameObject gameObject2 = GameObject.Find("PlayerStuff/Player/CameraShakeAnim/CameraPivot/Main Camera/PickRay").gameObject;
							RaycastHit val25 = default;
							if (Physics.Raycast(gameObject2.transform.position, gameObject2.transform.TransformDirection(Vector3.forward), ref val25))
							{
								selectobjecttext = "Object: " + ((Object)((Component)((RaycastHit)(ref val25)).collider).gameObject).name;
								selectedObject = ((Component)((RaycastHit)(ref val25)).collider).gameObject;
							}
						}
						if (((Rect)(ref val5)).Contains(current.mousePosition))
						{
							IsStuckEnabled = !IsStuckEnabled;
							if (IsStuckEnabled)
							{
								stucktext = "Stuck: ON";
							}
							else
							{
								stucktext = "Stuck: OFF";
								if ((Object)(object)GameObject.Find("Entities/Granny-Stuff/GrannyParent") != (Object)null)
								{
									((Behaviour)GameObject.Find("Entities/Granny-Stuff/GrannyParent").GetComponent<AI_Granny>()).enabled = true;
								}
								if ((Object)(object)GameObject.Find("Entities/Grandpa-Stuff/GranpaParent") != (Object)null)
								{
									((Behaviour)GameObject.Find("Entities/Grandpa-Stuff/GranpaParent").GetComponent<AI_Grandpa>()).enabled = true;
								}
								if ((Object)(object)GameObject.Find("Entities/Spider_Mom-Stuff/MomSpiderHeadParent") != (Object)null)
								{
									((Behaviour)GameObject.Find("Entities/Spider_Mom-Stuff/MomSpiderHeadParent").GetComponent<AI_MomSpider>()).enabled = true;
								}
							}
						}
						if (((Rect)(ref val7)).Contains(current.mousePosition))
						{
							GameObject.Find("PlayerStuff/HandHoldObjects/AvbitareHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/HammerHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/SafeKeyHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/HanglockKeyHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/playHouseKey").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/HouseKeyHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/CodePlattaHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/PlankaHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/ArmborstHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/VapennyckelHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/ScrewdriverHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/BatteryHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/MelonHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/AvbitareHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/Kugg1Hand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/Kugg2Hand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/BrunnsVevHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/AvbitareHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/CarkeyHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/TopplockHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/CarBatteryHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/BensindunkHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/WrenchHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/SparkPlugHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/MeatHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/SpecialKeyHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/BookHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/RemoteControlHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/Wheel_SpiderCellarHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/PadlockKeySpiderCellarHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/IronDoorSpinlockStickHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/SpiderMomKeyHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/ChainCutterHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/ElectricCoilKeyHand").SetActive(true);
							GameObject.Find("PlayerStuff/HandHoldObjects/ElectricBatonHand").SetActive(true);
						}
					}
				}
			}
			Color color = GUI.color;
			GUI.color = new Color(0f, 0f, 0f, 0.5f);
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), (Texture)(object)Texture2D.whiteTexture);
			GUI.color = color;
			GUI.backgroundColor = Color.black;
			GUI.contentColor = Color.white;
			GUI.color = Color.white;
			GUI.Box(val23, "Bluelands_MC Mod Menu");
			activeScene = SceneManager.GetActiveScene();
			if (((Scene)(ref activeScene)).name == "Menu")
			{
				GUI.backgroundColor = Color.gray;
				GUI.Button(val, "Max Money");
				GUI.Button(val2, "Reset Money");
				GUI.Button(val3, "Unlock All Achievements");
				GUI.Button(val4, "Remove All Achievements");
			}
			else
			{
				activeScene = SceneManager.GetActiveScene();
				if (((Scene)(ref activeScene)).name == "Scene")
				{
					GUI.backgroundColor = Color.gray;
					GUI.Button(val5, stucktext);
					GUI.Button(val6, godmodetext);
					GUI.Button(val7, "Get all items");
					GUI.Button(val8, selectobjecttext);
					GUI.Button(val9, "Destroy Object");
					GUI.Button(val10, "Clone Object");
					GUI.Button(val11, "Rotate Object X");
					GUI.Button(val12, "Rotate Object Y");
					GUI.Button(val13, "Rotate Object Z");
					GUI.Button(val15, nocliptext);
					GUI.Button(val14, "Teleport Object To Me");
					GUI.Button(new Rect(val16), "Game Speed: " + timeText);
					GUI.Button(new Rect(val17), "-");
					GUI.Button(new Rect(val18), "+");
					GUI.Button(val19, tpmenutext);
					GUI.Button(val20, "Spawn all items");
					GUI.Button(val21, "Eye Color: " + eyecolortext);
					GUI.Button(val22, enemyesptext);
				}
			}
		}
		if (!IsTeleportMenuEnabled || !isEnabled)
		{
			return;
		}
		GameObject val26 = GameObject.Find("PlayerStuff/Player");
		GameObject val27 = GameObject.Find("Entities/Grandpa-Stuff/GranpaParent");
		GameObject val28 = GameObject.Find("Entities/Granny-Stuff/GrannyParent");
		Rect val29 = new Rect(550f, 20f, 150f, 20f);
		Rect val30 = new Rect(400f, 20f, 140f, 20f);
		Rect val31 = new Rect(400f, 45f, 140f, 20f);
		Rect val32 = new Rect(550f, 45f, 150f, 20f);
		Rect val33 = new Rect(550f, 70f, 150f, 20f);
		Rect val34 = new Rect(400f, 70f, 140f, 20f);
		Rect val35 = new Rect(400f, 95f, 140f, 20f);
		if (current.isMouse && (int)current.type == 0)
		{
			float timeScale = Time.timeScale;
			if (((Rect)(ref val29)).Contains(current.mousePosition))
			{
				val26.SetActive(false);
				val27.SetActive(false);
				val27.gameObject.transform.position = val26.transform.position;
				val26.SetActive(true);
				val27.SetActive(true);
			}
			if (((Rect)(ref val30)).Contains(current.mousePosition))
			{
				val26.SetActive(false);
				val28.SetActive(false);
				val28.gameObject.transform.position = val26.transform.position;
				val26.SetActive(true);
				val28.SetActive(true);
			}
			if (((Rect)(ref val35)).Contains(current.mousePosition))
			{
				val26.SetActive(false);
				val26.transform.position = new Vector3(7.5179f, 22.0764f, -7.4663f);
				val26.SetActive(true);
			}
			if (((Rect)(ref val32)).Contains(current.mousePosition))
			{
				val26.SetActive(false);
				val26.transform.position = val27.transform.position;
				val26.SetActive(true);
			}
			if (((Rect)(ref val31)).Contains(current.mousePosition))
			{
				val26.SetActive(false);
				val26.transform.position = val28.transform.position;
				val26.SetActive(true);
			}
			if (((Rect)(ref val33)).Contains(current.mousePosition))
			{
				val26.SetActive(false);
				val26.transform.position = new Vector3(-17.0521f, 2.3214f, -10.3002f);
				val26.SetActive(true);
			}
			if (((Rect)(ref val34)).Contains(current.mousePosition))
			{
				val26.SetActive(false);
				val26.transform.position = new Vector3(14.2468f, 15.1891f, -17.3296f);
				val26.SetActive(true);
			}
		}
		GUI.Box(new Rect(400f, 0f, 300f, 200f), "Teleport Menu");
		GUI.Button(val30, "Teleport Granny to me");
		GUI.Button(val29, "Teleport Grandpa to me");
		GUI.Button(val31, "Teleport to Granny");
		GUI.Button(val32, "Teleport to Grandpa");
		GUI.Button(val33, "Teleport to Spider Cellar");
		GUI.Button(val34, "Teleport to Outside");
		GUI.Button(val35, "Teleport to Bedroom");
	}

	public static void Drop(GameObject Item, GameObject DropPoint)
	{
		Object.Instantiate<GameObject>(Item, DropPoint.transform.position, DropPoint.transform.rotation).SetActive(true);
	}

	static ModMenu()
	{
		isFadingIn = false;
		isFadingOut = false;
		fadeAlpha = 0f;
		fadeDuration = 0.2f;
		godmodetext = "God Mode: OFF";
		stucktext = "Stuck: OFF";
		nocliptext = "NoClip: OFF";
		tpmenutext = "Teleport Menu: OFF";
		selectobjecttext = "Select Object: ";
		enemyesptext = "Enemy ESP: OFF";
		timeText = 1.0;
		eyecolor = 0;
		eyecolortext = "White";
		omg = "0";
		isDragging = false;
		sssss = new Rect(0f, 0f, 200f, 40f);
	}
}
