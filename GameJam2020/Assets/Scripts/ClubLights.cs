using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class ClubLights : MonoBehaviour
    {
        public Material GlassMaterial;

        private int Intensity;

        private System.Random Rand;

        public int ColorChangeFrequency;

        private bool Changing;

        private int IntensityIncrement;

        private List<Color> ClubColors;

        private Color CurrentColor;

        // Start is called before the first frame update
        void Start()
        {
            Intensity = 5;

            IntensityIncrement = -1;

            Changing = false;

            Rand = new System.Random();

            ClubColors = new List<Color>();

            ClubColors.Add(new Color(0.0f, 0.05f, 0.15f));
            ClubColors.Add(new Color(0.15f, 0.0f, 0.15f));
            ClubColors.Add(new Color(0.15f, 0.0f, 0.0f));
            ClubColors.Add(new Color(0.15f, 0.0f, 0.10f));

            CurrentColor = ClubColors[Rand.Next(ClubColors.Count())];

            GlassMaterial.SetColor("_EmissionColor", CurrentColor * Intensity);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!Changing && Rand.Next(ColorChangeFrequency) == 0)
            {
                // Start changing light
                Changing = true;
            }

            if (Changing)
            {

                Intensity += IntensityIncrement;

                GlassMaterial.SetColor("_EmissionColor", CurrentColor * Intensity);

                if (Intensity == 0)
                {
                    // When we hit 0, change the color
                    CurrentColor = ClubColors[Rand.Next(ClubColors.Count())];

                    IntensityIncrement = -IntensityIncrement;
                }
                else if (Intensity == 5)
                {
                    IntensityIncrement = -IntensityIncrement;
                    Changing = false;
                }
            }
        }
    }
}
