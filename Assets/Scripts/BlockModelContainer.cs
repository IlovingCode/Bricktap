// using System;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEditor;
// using UnityEngine;

// public class BlockModelContainer : MonoBehaviour
// {
//     public enum AccentType
//     {
//         Disabled,
//         Random,
//         Gradient,
//         Volume,
//         Direction,
//         GradientStraight
//     }

//     [Serializable]
//     public struct ColorSet
//     {
//         public Color BackgroundColor;
//         public AccentType AccentType;
//         public Gradient AccentColorGradient;
//         public TypeColorOverride[] BlockTypeOverrides;
//     }

//     public enum UiThemeId
//     {
//         Default,
//         DarkSkin,
//     }

//     [Serializable]
//     public struct TypeColorOverride
//     {
//         public BlockType Type;
//         public Color Color;
//     }

//     [Serializable]
//     public class BlockModel
//     {
//         public string Name;
//         public BlockType Type;
//         public GameObject Model;
//     }

//     public BlockModel[] Models;

//     public ColorSet[] ColorSets;

//     public UiThemeId UiTheme;

//     public GameObject CameraEnvironment;

//     public bool DisableFog;

// #if UNITY_EDITOR

//     public void OnDrawGizmos()
//     {
//         if (Models != null)
//         {
//             foreach (var model in Models)
//             {
//                 if (!model.Model) continue;

//                 var volume = model.Type.GetLocalVolume();
//                 var worldVolume =
//                     BlockService.GetWorldVolume(volume,
//                         model.Model.transform.position,
//                         model.Model.transform.rotation);
//                 foreach (var wolume in worldVolume)
//                 {
//                     Gizmos.DrawWireCube(wolume, Vector3.one);
//                 }

//                 Handles.Label(model.Model.transform.position - Vector3.up * .75f, model.Name);
//             }
//         }
//     }

//     public void OnValidate()
//     {
//         if (Models == null)
//             return;
//         for (var i = 0; i < Models.Length; i++)
//         {
//             var blockModel = Models[i];
//             var targetName = $"[{i}] {blockModel.Type}";
//             if (!targetName.Equals(blockModel.Name))
//             {
//                 blockModel.Name = targetName;
//             }
//         }
//     }

// #endif

//     public GameObject InstantiateModel(
//         Transform parent,
//         BlockType type,
//         Vector3 positionFactor,
//         Quaternion rotation,
//         int levelColorIndex,
//         int modelIndex,
//         out List<GameObject> instantiatedEffects,
//         params GameObject[] effectPrefabs)
//     {
//         var model = Instantiate(GetModel(type, modelIndex), parent);
//         var t = model.transform;
//         instantiatedEffects = new List<GameObject>();
//         foreach (var accessory in effectPrefabs)
//         {
//             if (accessory)
//             {
//                 instantiatedEffects.Add(Instantiate(accessory, t, false));
//             }
//         }

//         t.localPosition = Vector3.zero;
//         t.localRotation = Quaternion.identity;
//         if (ColorSets.Length > 0)
//         {
//             var colorSet = ColorSets.GetAtIndexWrapped(levelColorIndex);
//             if (colorSet.AccentType != AccentType.Disabled)
//             {
//                 var targetColor = GetTargetColor(colorSet, type, positionFactor, rotation);
//                 foreach (var colorable in model.GetComponentsInChildren<IColorableBlock>())
//                     colorable.SetColor(targetColor);
//             }
//         }

//         return model;
//     }

//     public static Color GetTargetColor(ColorSet colorSet, BlockType blockType, Vector3 positionFactor, Quaternion rotation)
//     {
//         if (colorSet.BlockTypeOverrides.Any(e => e.Type == blockType))
//         {
//             return RandomService.View
//                 .Element(colorSet.BlockTypeOverrides.Where(e => e.Type == blockType).ToList())
//                 .Color;
//         }

//         switch (colorSet.AccentType)
//         {
//             case AccentType.Disabled:
//                 return default;
//             case AccentType.Random:
//                 return colorSet.AccentColorGradient.Evaluate(RandomService.View.Float());
//             case AccentType.Gradient:
//                 return colorSet.AccentColorGradient.Evaluate((positionFactor.x + positionFactor.y + positionFactor.z) / 3f);
//             case AccentType.Volume:
//                 return colorSet.AccentColorGradient.Evaluate(blockType.GetLocalVolume().Length / 10f);
//             case AccentType.Direction:
//                 var direction = rotation * Vector3.forward;
//                 var targetFactor = -1f;
//                 for (var i = 0; i < BlockService.Directions.Length; i++)
//                 {
//                     if (Vector3.Angle(BlockService.Directions[i], direction) < 46)
//                     {
//                         targetFactor = i / ((float)BlockService.Directions.Length - 1);
//                         break;
//                     }
//                 }

//                 if (targetFactor < 0)
//                     Debug.LogWarning($"unable to find target color for direction: {direction}");
//                 return colorSet.AccentColorGradient.Evaluate(targetFactor);
//             case AccentType.GradientStraight:
//                 return colorSet.AccentColorGradient.Evaluate(positionFactor.y);
//             default:
//                 throw new ArgumentOutOfRangeException();
//         }
//     }

//     public GameObject GetModel(BlockType type, int modelIndex)
//     {
//         var gameObjects = Models.Where(model => model.Type == type).Select(bm => bm.Model).ToArray();
//         switch (gameObjects.Length)
//         {
//             case 0:
//                 if (type == BlockType.CoinBlock1x1)
//                 {
//                     return Contexts.sharedInstance.config.cubism.DefaultCoinBlockModel;
//                 }

//                 Debug.LogError($"{gameObject} has no model assigned for BlockType {type}");
//                 return null;
//             case 1:
//                 return gameObjects[0];
//             default:
// #if UNITY_EDITOR
//                 return modelIndex >= 0
//                     ? gameObjects.GetAtIndexWrapped(modelIndex)
//                     : Application.isPlaying
//                         ? RandomService.View.Element(gameObjects)
//                         : gameObjects[UnityEngine.Random.Range(0, gameObjects.Length)];
// #else
//                 return modelIndex >= 0 ?
//                     gameObjects.GetAtIndexWrapped(modelIndex)
//                     : RandomService.View.Element(gameObjects);
// #endif
//         }
//     }
// }
