using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Material Effect Dictionary")]
public class MaterialEffects : ScriptableObject
{
    [System.Serializable]
    struct MaterialEffect
    {
        public Material Material;
        public GameObject Effect;

        MaterialEffect(Material Material, GameObject Effect)
        {
            this.Material = Material;
            this.Effect = Effect;
        }
    }

    [SerializeField] GameObject defaultEffect;
    [SerializeField] List<MaterialEffect> effects;

    public int GetIndex(Material material)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].Material == material)
            {
                return i;
            }
        }

        return -1;
    }

    public GameObject GetEffect(Material material)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].Material == material)
            {
                return effects[i].Effect;
            }
        }

        return defaultEffect;
    }

    public GameObject GetEffect(int index)
    {
        if (index == -1)
        {
            return defaultEffect;
        }

        return effects[index].Effect;
    }
}
