// using UnityEngine;

// namespace Cubism.Views
// {
//     public class SkinEnvironmentView : MonoBehaviour
//         , IAnySkinSelectedListener
//     {
//         GameEntity _gameListener;
//         Contexts _contexts;

//         (BlockModelContainer, GameObject) _currentEnvironment;

//         void Awake()
//         {
//             _contexts = Contexts.sharedInstance;
//             _gameListener = _contexts.game.CreateListenerEntity(this);
//             _gameListener.AddAnySkinSelectedListener(this);
//         }

//         void OnDestroy()
//         {
//             _gameListener?.DestroyListenerEntity(this);
//         }

//         void OnEnable()
//         {
//             UpdateEnvironment();
//         }

//         public void OnAnySkinSelected(GameEntity entity, string type, string id)
//         {
//             UpdateEnvironment();
//         }

//         void UpdateEnvironment()
//         {
//             var selectedBlockSkinData = _contexts.GetSelectedBlockSkinData();
//             if (selectedBlockSkinData != null)
//             {
//                 var blockModelContainer = selectedBlockSkinData.BlockModels;
//                 if (_currentEnvironment.Item1 != blockModelContainer)
//                 {
//                     if (_currentEnvironment.Item2)
//                         Destroy(_currentEnvironment.Item2);
//                     _currentEnvironment = (blockModelContainer,
//                         blockModelContainer.CameraEnvironment
//                             ? Instantiate(blockModelContainer.CameraEnvironment, transform)
//                             : null);
//                     RenderSettings.fog = !blockModelContainer.DisableFog;
//                 }
//             }
//         }
//     }
// }
