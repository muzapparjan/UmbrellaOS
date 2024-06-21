namespace UmbrellaOS.Lab.Brains.Stupid;

internal static class Extensions
{
    public static T[] Clone<T>(this T[] original)
    {
        var cloned = new T[original.Length];
        for (var i = 0; i < original.Length; i++)
        {
            if (original[i] is ICloneable cloneable)
                cloned[i] = (T)cloneable.Clone();
            else
                cloned[i] = original[i];
        }
        return cloned;
    }
}