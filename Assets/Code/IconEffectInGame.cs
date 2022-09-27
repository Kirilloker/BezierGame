using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IconEffectInGame : MonoBehaviour
{
    [SerializeField]
    Image imageIcon;

    [SerializeField]
    Image imageFon;

    SpawnEffectIcon spawnEffectIcon;
    UpgradeEffect effect;
    public IEnumerator CreateEffect(float time, Sprite sprite, UpgradeEffect effect, SpawnEffectIcon spawnEffectIcon)
    {
        this.effect = effect;
        this.spawnEffectIcon = spawnEffectIcon;

        imageIcon.sprite = sprite;
        imageFon.sprite = sprite;

        float time_itteration = 0.05f;

        int count_itteration =  (int) (time / time_itteration);

        for (int i = 0; i < count_itteration; i++)
        {
            if (liveEffect == false)
            {
                yield break;
            }

            imageIcon.fillAmount =  1f - (float)i / count_itteration;

            yield return new WaitForSeconds(time_itteration);
        }

        Delete();
    }


    bool liveEffect = true;
    public void Delete()
    {
        liveEffect = false;
        Destroy(gameObject);

        spawnEffectIcon.DeleteEffect(effect);
    }


}
