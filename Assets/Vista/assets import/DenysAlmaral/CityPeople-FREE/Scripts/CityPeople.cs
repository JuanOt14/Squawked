using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CityPeople
{
    public class CityPeople : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Overrides palette materials, skips other objects")]
        private Material PaletteOverride;
        public string CurrentPaletteName { get; private set; }

        private Animator animator;
        public const string people_pal_prefix = "people_pal";
        private List<Renderer> _paletteMeshes;

        // Nombres de animaciones (puedes cambiarlos desde el Inspector si lo deseas)
        [Header("Animaciones")]
        public string idleAnimation = "idle_f_1_150f";
        public string celebrationAnimation = "Festejo";
        public string errorAnimation = "Negacion";

        private void Awake()
        {
            var AllRenderers = gameObject.GetComponentsInChildren<Renderer>();
            _paletteMeshes = new List<Renderer>();
            foreach (Renderer r in AllRenderers)
            {
                var matName = r.sharedMaterial.name;
                var len = Math.Min(people_pal_prefix.Length, matName.Length);
                if (matName[0..len] == CityPeople.people_pal_prefix)
                {
                    _paletteMeshes.Add(r);
                }
            }
            if (_paletteMeshes.Count > 0)
            {
                CurrentPaletteName = _paletteMeshes[0].sharedMaterial.name;
            }

            if (PaletteOverride != null)
            {
                SetPalette(PaletteOverride);
            }
        }

        void Start()
        {
            animator = GetComponent<Animator>();
            // Al iniciar, solo reproducir la animación idle
            if (animator != null && !string.IsNullOrEmpty(idleAnimation))
            {
                animator.CrossFadeInFixedTime(idleAnimation, 0.1f);
            }

            // Si quieres mantener el collider para clicks, puedes dejar esto:
            CapsuleCollider collider = gameObject.AddComponent<CapsuleCollider>();
            collider.center = new Vector3(0f, 0.8f, 0f);
            collider.radius = 0.3f;
            collider.height = 1.77f;
            collider.direction = 1;
        }

        public void SetPalette(Material mat)
        {
            if (mat != null)
            {
                if (mat.name[0..people_pal_prefix.Length] == CityPeople.people_pal_prefix)
                {
                    CurrentPaletteName = mat.name;
                    foreach (Renderer r in _paletteMeshes)
                    {
                        r.material = mat;
                    }
                }
                else
                {
                    Debug.Log("Material name should start with 'palete_pal...' by convention.");
                }
            }
        }

        // Llama esta función para animación de festejo
        public void PlayCelebrationAnimation()
        {
            if (animator != null && !string.IsNullOrEmpty(celebrationAnimation))
            {
                animator.CrossFadeInFixedTime(celebrationAnimation, 0.1f);
            }
        }

        // Llama esta función para animación de error
        public void PlayErrorAnimation()
        {
            if (animator != null && !string.IsNullOrEmpty(errorAnimation))
            {
                animator.CrossFadeInFixedTime(errorAnimation, 0.1f);
            }
        }
    }
}
