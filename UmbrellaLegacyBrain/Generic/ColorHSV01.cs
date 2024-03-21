namespace UmbrellaLegacyBrain.Generic
{
    /**
     * <summary>
     * Variant of an HSV color, with each channel limited to [0, 1].
     * </summary>
     */
    public struct ColorHSV01(float h = 0, float s = 0, float v = 0)
    {
        /**
         * <summary>
         * Hue.<br/>
         * Hue represents the actual pure color perceived by our eyes.
         * </summary>
         */
        public float h = h;
        /**
         * <summary>Saturation.<br/>
         * Saturation is the colorfulness of that pure color
         * (i.e decreasing Saturation reduces the colorfulness from the color itself).
         * </summary>
         */
        public float s = s;
        /**
         * <summary>
         * Value.<br/>
         * Value is the intensity of the color, correlates with the its darkness.
         * </summary>
         */
        public float v = v;
    }
}