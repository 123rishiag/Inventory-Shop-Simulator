using System;
using UnityEngine;

namespace ServiceLocator.Sound
{
    [CreateAssetMenu(fileName = "SoundScriptableObject", menuName = "ScriptableObjects/SoundScriptableObject")]
    public class SoundScriptableObject : ScriptableObject
    {
        public Sounds[] audioList;
    }

    [Serializable]
    public struct Sounds
    {
        public SoundType soundType;
        public AudioClip audio;
    }

    public enum SoundType
    {
        BackgroundMusic,
        ButtonClick,
        TabSwitch,
        QuantitySelect,
        ConfirmationPopup,
        ItemSold,
        ItemBought,
        ErrorFeedback,
        GatherResource
    }
}