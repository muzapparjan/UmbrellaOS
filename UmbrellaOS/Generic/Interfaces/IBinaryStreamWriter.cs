﻿namespace UmbrellaOS.Generic.Interfaces
{
    /** <summary>Indicates the ability to write bytes to a stream synchronously and/or asynchronously.</summary> */
    public interface IBinaryStreamWriter
    {
        /**
         * <summary>Write bytes to the stream synchronously.</summary>
         * <param name="stream">the binary stream to write into</param>
         */
        public void Write(Stream stream);
        /**
         * <summary>Write bytes to the stream asynchronously.</summary>
         * <param name="stream">the binary stream to write into</param>
         * <param name="cancellationToken">the token indicating whether the task has been cancelled</param>
         */
        public Task WriteAsync(Stream stream, CancellationToken cancellationToken = default);
    }
}