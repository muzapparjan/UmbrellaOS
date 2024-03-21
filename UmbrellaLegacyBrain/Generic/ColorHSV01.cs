using System.Buffers.Binary;
using UmbrellaLegacyBrain.Generic.Interfaces;

namespace UmbrellaLegacyBrain.Generic
{
    /**
     * <summary>
     * Variant of an HSV color, with each channel limited to [0, 1].
     * </summary>
     */
    public struct ColorHSV01(float h = 0, float s = 0, float v = 0) : ISerializable
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

        public float this[int index]
        {
            readonly get => index switch
            {
                0 => h,
                1 => s,
                2 => v,
                _ => throw new IndexOutOfRangeException()
            };
            set
            {
                switch (index)
                {
                    case 0: h = value; break;
                    case 1: s = value; break;
                    case 2: v = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public static readonly ulong SerializedSize = sizeof(float) * 3;

        public readonly void Serialize(Span<byte> data)
        {
            BinaryPrimitives.WriteSingleLittleEndian(data.Slice(sizeof(float) * 0, sizeof(float)), h);
            BinaryPrimitives.WriteSingleLittleEndian(data.Slice(sizeof(float) * 1, sizeof(float)), s);
            BinaryPrimitives.WriteSingleLittleEndian(data.Slice(sizeof(float) * 2, sizeof(float)), v);
        }
        public static ColorHSV01 Deserialize(Span<byte> data) => new(
            BinaryPrimitives.ReadSingleLittleEndian(data.Slice(sizeof(float) * 0, sizeof(float))),
            BinaryPrimitives.ReadSingleLittleEndian(data.Slice(sizeof(float) * 1, sizeof(float))),
            BinaryPrimitives.ReadSingleLittleEndian(data.Slice(sizeof(float) * 2, sizeof(float))));
    }
}