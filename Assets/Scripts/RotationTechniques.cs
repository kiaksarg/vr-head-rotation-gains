


using System;
namespace Utils
{

    public static class RotationTechniques
    {


        public static float velocityGuidedGain(float velocity, float minGain, float maxGain, float halfRotation)
        {
            if (velocity <= 26.7) return 2.95f;

            if (26.7 < velocity && velocity <= 41.1)
            {
                return 2.55f + (41.1f - velocity) / (41.1f - 26.7f) * (2.95f - 2.55f);
            }

            if (41.1 < velocity && velocity < 62.2)
            {
                return 2.22f + (62.2f - velocity) / (62.2f - 41.1f) * (2.55f - 2.22f);
            }

            if (velocity >= 62.2) return 2.22f;

            return 0;

        }


        public static float dynamicNonLinearGain(float virtualRotationAngle, float minGain, float maxGain, float halfRotation)
        {
            return (
                minGain * Math.Abs((virtualRotationAngle - halfRotation) / halfRotation) +
                maxGain * (1 - Math.Abs((virtualRotationAngle - halfRotation) / halfRotation))
            );
        }
    }
}