using UnityEngine;
using ServiceLocator.UI;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {
        private UIService uiService;

        private void Start()
        {
            // Create Services
            uiService = new UIService();

            uiService.Init();
        }
    }
}
