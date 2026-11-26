using MelonLoader;

namespace LegacyModMenu
{
	public class Core : MelonMod
	{
		private bool isMenuOn = false;

		public override void OnInitializeMelon()
		{
			((MelonBase)this).LoggerInstance.Msg("Initialized.");
			ModMenu.Start();
		}

		public override void OnSceneWasInitialized(int buildIndex, string sceneName)
		{
			((MelonMod)this).OnSceneWasInitialized(buildIndex, sceneName);
		}

		public override void OnLateUpdate()
		{
			ModMenu.LateUpdate();
			((MelonBase)this).OnLateUpdate();
		}

		public override void OnUpdate()
		{
			ModMenu.Update();
			((MelonBase)this).OnUpdate();
		}

		public override void OnGUI()
		{
			ModMenu.OnGUI();
			((MelonBase)this).OnGUI();
		}

		public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
			if (sceneName == "Scene")
			{
				ModMenu.SceneLoaded();
			}
			((MelonMod)this).OnSceneWasLoaded(buildIndex, sceneName);
		}
	}
}
