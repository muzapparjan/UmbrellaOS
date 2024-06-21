#pragma warning disable CS8618

using System.Collections;
using System.Numerics;

namespace UmbrellaOS.Lab.Brains.Stupid;

public sealed class Tensor<T> : IComparable<Tensor<T>>, IEquatable<Tensor<T>>, IEnumerable<T>, ICloneable where T : INumber<T>
{
    public int Rank => rank;
    public bool IsScalar => Rank == 0;
    public bool IsVector => Rank == 1;
    public bool IsMatrix => Rank == 2;
    public int Length => data.Length;
    public Tensor<int> Shape => shape.Clone();

    private ref T this[int index] => ref data[index];
    public Tensor<T> this[Tensor<int> index]
    {
        get
        {
            if ((!index.IsScalar && !index.IsVector) || index.Length == 0 || index.Length > shape.Length)
                throw new ArgumentException("invalid tensor index");
            var offset = 0;
            for(var i=0;i<index.Length;i++)
            {
                if (index[i] < 0 || index[i] >= shape[i])
                    throw new ArgumentException("invalid tensor index");
                //TODO
            }
            //for(var i=index.Length-1;i >= 0; i--)
            //{
            //    if (index[i] < 0 || index[i] >= shape[i])
            //        throw new ArgumentException("invalid tensor index");
            //TODO
            //}

            //TODO
            throw new NotImplementedException();
        }
    }

    private int rank;
    private Tensor<int> shape;
    private T[] data;

    private Tensor() { }
    public Tensor(params int[] shape)
    {
        rank = shape.Length - 1;
        if (rank < 0)
            throw new ArgumentException("invalid tensor shape");
        this.shape = Tensor<int>.Vector(shape);
        data = new T[this.shape.Product()];
    }
    public Tensor(Tensor<int> shape)
    {
        if (!shape.IsScalar && !shape.IsVector)
            throw new ArgumentException("invalid tensor shape");
        rank = shape.data.Length - 1;
        if (rank < 0)
            throw new ArgumentException("invalid tensor shape");
        this.shape = shape.Clone();
        data = new T[this.shape.Product()];
    }

    public T Sum()
    {
        var sum = T.Zero;
        for (var i = 0; i < data.Length; ++i)
            sum += data[i];
        return sum;
    }
    public T Product()
    {
        var product = T.Zero;
        for (var i = 0; i < data.Length; ++i)
            product *= data[i];
        return product;
    }
    public Tensor<T> Flatten() => Vector(data);

    public static Tensor<T> Scalar(T value) => new()
    {
        rank = 0,
        shape = Shapes.Scalar,
        data = [value]
    };
    public static Tensor<T> Vector(int length) => new()
    {
        rank = 1,
        shape = Shapes.Vector(length),
        data = new T[length]
    };
    public static Tensor<T> Vector(params T[] values) => new()
    {
        rank = 1,
        shape = Tensor<T>.Shapes.Vector(values.Length),
        data = values.Clone<T>()
    };
    public static Tensor<T> Matrix(int height, int width) => new()
    {
        rank = 2,
        shape = Shapes.Matrix(height, width),
        data = new T[height * width]
    };

    public static implicit operator T[](Tensor<T> tensor) => tensor.data.Clone<T>();
    public static implicit operator Tensor<T>(T[] data) => Vector(data);
    public static implicit operator T(Tensor<T> tensor)
    {
        if (!tensor.IsScalar || tensor.data.Length < 1)
            throw new ArgumentException("failed to convert tensor to scalar");
        return tensor.data[0];
    }
    public static implicit operator Tensor<T>(T value) => Scalar(value);

    public Tensor<T> Clone() => new()
    {
        rank = rank,
        shape = shape.Clone(),
        data = data.Clone<T>()
    };
    object ICloneable.Clone() => Clone();

    int IComparable<Tensor<T>>.CompareTo(Tensor<T>? other)
    {
        ArgumentNullException.ThrowIfNull(other);
        if (ReferenceEquals(this, other))
            return 0;
        if (!IsScalar || !other.IsScalar)
            throw new ArgumentException($"only scalars can compare to each other");
        return data[0].CompareTo(other.data[0]);
    }

    bool IEquatable<Tensor<T>>.Equals(Tensor<T>? other)
    {
        if (ReferenceEquals(this, other))
            return true;
        if (other == null || rank != other.rank)
            return false;
        if (IsScalar && other.IsScalar)
            return data[0].Equals(other.data[0]);
        if (!shape.Equals(other.shape))
            return false;
        for (var i = 0; i < data.Length; i++)
            if (!data[i].Equals(other.data[i]))
                return false;
        return true;
    }
    public override bool Equals(object? obj) => obj is Tensor<T> tensor && Equals(tensor);
    public override int GetHashCode()
    {
        var hashCode = data[0].GetHashCode();
        if (IsScalar)
            return hashCode;
        for (var i = 0; i < data.Length - 6; i += 7)
        {
            hashCode = HashCode.Combine(hashCode,
                data[i].GetHashCode(),
                data[i + 1].GetHashCode(),
                data[i + 2].GetHashCode(),
                data[i + 3].GetHashCode(),
                data[i + 4].GetHashCode(),
                data[i + 5].GetHashCode(),
                data[i + 6].GetHashCode());
        }
        for (var i = data.Length - 6; i < data.Length; i++)
            hashCode = HashCode.Combine(hashCode, data[i]);
        return hashCode;
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        foreach (var element in data)
            yield return element;
    }
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)data).GetEnumerator();

    public static class Shapes
    {
        public static Tensor<int> Scalar { get; }

        static Shapes()
        {
            Scalar = new()
            {
                rank = 0,
                data = [1]
            };
            Scalar.shape = Scalar;
        }

        public static Tensor<int> Vector(int length) => Tensor<int>.Scalar(length);
        public static Tensor<int> Matrix(int height, int width) => Tensor<int>.Vector(height, width);
    }
}