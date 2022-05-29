using UnityEngine;
using DG.Tweening;

public class CameraView : MonoBehaviour
{
    public Camera Camera;
    public float CameraSpeed = 10;
    public Vector3 CameraPos1;
    public Vector3 CameraRot1;
    public Vector3 CameraPos2;
    public Vector3 CameraRot2;
    private bool hasLevelBounds;
    private bool hasPosition3;
    private Vector3 position3;
    private Bounds levelBounds;

    // ConfigEntity _configListener;

    private void Update()
    {
        // if (_contexts != null && _contexts.game.hasCamera && _contexts.game.cameraEntity.hasPosition3 && !_contexts.game.hasCameraReset)
        if (hasPosition3)
        {
            var targetPosition = position3;
            var lerp = Time.deltaTime * CameraSpeed;
            var t = transform;
            t.position = Vector3.Lerp(t.position, targetPosition, lerp);
            if (hasLevelBounds)
            {
                var boundsCenter = levelBounds.center;
                var currentToBounds = t.position - boundsCenter;
                var targetToBounds = targetPosition - boundsCenter;
                t.position = boundsCenter + currentToBounds.normalized * targetToBounds.magnitude;
                t.LookAt(boundsCenter, t.up);
            }
            else
            {
                t.position = Vector3.Lerp(t.position, targetPosition, lerp);
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Camera.gameObject.transform.DOLocalMove(new Vector3(CameraPos1.x, CameraPos1.y, CameraPos1.z), 1f).SetEase(Ease.InOutQuad);
            Camera.gameObject.transform.DOLocalRotate(CameraRot1, 1).SetEase(Ease.InOutQuad);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            Camera.gameObject.transform.DOLocalMove(new Vector3(CameraPos2.x, CameraPos2.y, CameraPos2.z), 1f).SetEase(Ease.InOutQuad);
            Camera.gameObject.transform.DOLocalRotate(CameraRot2, 1).SetEase(Ease.InOutQuad);
        }
        
    }

    // public override void Link(Contexts contexts, GameEntity entity)
    // {
    //     base.Link(contexts, entity);

    //     _configListener = _contexts.config.CreateListenerEntity(this);
    //     _configListener.AddAnyThemeConfigListener(this);

    //     entity.AddAnySkinSelectedListener(this);
    //     UpdateBackgroundColor();
    //     OnAnyThemeConfig(null, Contexts.sharedInstance.config.theme);
    // }

    // public override void OnDestroyed(GameEntity entity)
    // {
    //     base.OnDestroyed(entity);
    //     _configListener.DestroyListenerEntity(this);
    // }

    // public virtual void OnAnyThemeConfig(ConfigEntity entity, IThemeConfig value)
    // {
    //     // Camera.backgroundColor = value.CameraBackgroundColor;
    // }

    // public void OnAnySkinSelected(GameEntity entity, string type, string id)
    // {
    //     UpdateBackgroundColor();
    // }

    // void UpdateBackgroundColor()
    // {
    //     Camera.backgroundColor = _contexts.GetBackgroundColor();
    //     RenderSettings.fogColor = _contexts.GetBackgroundColor();
    // }
}
