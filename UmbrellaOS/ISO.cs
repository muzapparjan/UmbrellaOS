using System.Text;

namespace UmbrellaOS
{
    public sealed class ISO : IBinaryStreamWriter
    {
        private static readonly byte[] STANDARD_IDENTIFIER = Encoding.ASCII.GetBytes("CD001");

        public sealed class DateAndTime : IBinaryStreamWriter
        {
            public int Year
            {
                get => data[0] * 1000 + data[1] * 100 + data[2] * 10 + data[3];
                set
                {
                    if (value < 0 || value > 9999)
                        throw new ArgumentOutOfRangeException(nameof(value), "year should be from 0 to 9999");
                    data[0] = (byte)(value / 1000 % 10);
                    data[1] = (byte)(value / 100 % 10);
                    data[2] = (byte)(value / 10 % 10);
                    data[3] = (byte)(value % 10);
                }
            }
            public int Month
            {
                get => data[4] * 10 + data[5];
                set
                {
                    if (value < 0 || value > 12)
                        throw new ArgumentOutOfRangeException(nameof(value), "month should be from 0 to 12");
                    data[4] = (byte)(value / 10 % 10);
                    data[5] = (byte)(value % 10);
                }
            }
            public int Day
            {
                get => data[6] * 10 + data[7];
                set
                {
                    if (value < 0 || value > 31)
                        throw new ArgumentOutOfRangeException(nameof(value), "day should be from 0 to 31");
                    data[6] = (byte)(value / 10 % 10);
                    data[7] = (byte)(value % 10);
                }
            }
            public int Hour
            {
                get => data[8] * 10 + data[9];
                set
                {
                    if (value < 0 || value > 23)
                        throw new ArgumentOutOfRangeException(nameof(value), "hour should be from 0 to 23");
                    data[8] = (byte)(value / 10 % 10);
                    data[9] = (byte)(value % 10);
                }
            }
            public int Minute
            {
                get => data[10] * 10 + data[11];
                set
                {
                    if (value < 0 || value > 59)
                        throw new ArgumentOutOfRangeException(nameof(value), "minute should be from 0 to 59");
                    data[10] = (byte)(value / 10 % 10);
                    data[11] = (byte)(value % 10);
                }
            }
            public int Second
            {
                get => data[12] * 10 + data[13];
                set
                {
                    if (value < 0 || value > 59)
                        throw new ArgumentOutOfRangeException(nameof(value), "second should be from 0 to 59");
                    data[12] = (byte)(value / 10 % 10);
                    data[13] = (byte)(value % 10);
                }
            }
            public int HundredthsOfASecond
            {
                get => data[14] * 10 + data[15];
                set
                {
                    if (value < 0 || value > 99)
                        throw new ArgumentOutOfRangeException(nameof(value), "hundredths of a second should be from 0 to 99");
                    data[14] = (byte)(value / 10 % 10);
                    data[15] = (byte)(value % 10);
                }
            }
            public int OffsetFromGreenwichMeanTimeInNumberOf15MinIntervals
            {
                get => (sbyte)data[17];
                set
                {
                    if (value < -48 || value > 52)
                        throw new ArgumentOutOfRangeException(nameof(value), "offset should be from -48 to 52");
                    data[16] = (byte)value;
                }
            }

            private readonly byte[] data = new byte[17];

            public DateAndTime() { }
            public DateAndTime(int year = 0, int month = 0, int day = 0, int hour = 0, int minute = 0, int second = 0, int hundredthsOfASecond = 0, int offsetFromGreenwichMeanTimeInNumberOf15MinIntervals = 0)
            {
                Year = year;
                Month = month;
                Day = day;
                Hour = hour;
                Minute = minute;
                Second = second;
                HundredthsOfASecond = hundredthsOfASecond;
                OffsetFromGreenwichMeanTimeInNumberOf15MinIntervals = offsetFromGreenwichMeanTimeInNumberOf15MinIntervals;
            }

            public void Write(Stream stream)
            {
                stream.Write(data);
            }
            public async Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
            {
                await stream.WriteAsync(data, cancellationToken);
            }
        }
        public sealed class VolumeFlags : IBinaryStreamWriter
        {
            public bool ContainsEscapeCharNotInISO2375
            {
                get => code.GetBitLittleEndian(0);
                set => code.SetBitLittleEndian(0, value);
            }

            private byte code = 0;

            public VolumeFlags() { }
            public VolumeFlags(bool containsEscapeCharNotInISO2375 = false)
            {
                ContainsEscapeCharNotInISO2375 = containsEscapeCharNotInISO2375;
            }

            public void Write(Stream stream)
            {
                stream.WriteByte(code);
            }
            public async Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
            {
                await stream.WriteAsync(new byte[1] { code }, cancellationToken);
            }
        }
        public sealed class RecordingDateAndTime : IBinaryStreamWriter
        {
            public int YearSince1900
            {
                get => data[0];
                set
                {
                    if (value < 0 || value > 255)
                        throw new ArgumentOutOfRangeException(nameof(value), "year since 1900 should be from 0 to 255");
                    data[0] = (byte)value;
                }
            }
            public int Month
            {
                get => data[1];
                set
                {
                    if (value < 0 || value > 12)
                        throw new ArgumentOutOfRangeException(nameof(value), "month should be from 0 to 12");
                    data[1] = (byte)value;
                }
            }
            public int Day
            {
                get => data[2];
                set
                {
                    if (value < 0 || value > 31)
                        throw new ArgumentOutOfRangeException(nameof(value), "day should be from 0 to 31");
                    data[2] = (byte)value;
                }
            }
            public int Hour
            {
                get => data[3];
                set
                {
                    if (value < 0 || value > 23)
                        throw new ArgumentOutOfRangeException(nameof(value), "hour should be from 0 to 23");
                    data[3] = (byte)value;
                }
            }
            public int Minute
            {
                get => data[4];
                set
                {
                    if (value < 0 || value > 59)
                        throw new ArgumentOutOfRangeException(nameof(value), "minute should be from 0 to 59");
                    data[4] = (byte)value;
                }
            }
            public int Second
            {
                get => data[5];
                set
                {
                    if (value < 0 || value > 59)
                        throw new ArgumentOutOfRangeException(nameof(value), "second should be from 0 to 59");
                    data[5] = (byte)value;
                }
            }
            public int OffsetFromGreenwichMeanTimeInNumberOf15MinIntervals
            {
                get => (sbyte)data[6];
                set
                {
                    if (value < -48 || value > 52)
                        throw new ArgumentOutOfRangeException(nameof(value), "offset should be from -48 to 52");
                    data[6] = (byte)value;
                }
            }

            private readonly byte[] data = new byte[7];

            public RecordingDateAndTime() { }
            public RecordingDateAndTime(int yearSince1900 = 0, int month = 0, int day = 0, int hour = 0, int minute = 0, int second = 0, int offsetFromGreenwichMeanTimeInNumberOf15MinIntervals = 0)
            {
                YearSince1900 = yearSince1900;
                Month = month;
                Day = day;
                Hour = hour;
                Minute = minute;
                Second = second;
                OffsetFromGreenwichMeanTimeInNumberOf15MinIntervals = offsetFromGreenwichMeanTimeInNumberOf15MinIntervals;
            }

            public void Write(Stream stream)
            {
                stream.Write(data);
            }
            public async Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
            {
                await stream.WriteAsync(data, cancellationToken);
            }
        }
        public sealed class FileFlags : IBinaryStreamWriter
        {
            public bool Existence
            {
                get => code.GetBitLittleEndian(0);
                set => code.SetBitLittleEndian(0, value);
            }
            public bool Directory
            {
                get => code.GetBitLittleEndian(1);
                set => code.SetBitLittleEndian(1, value);
            }
            public bool AssociatedFile
            {
                get => code.GetBitLittleEndian(2);
                set => code.SetBitLittleEndian(2, value);
            }
            public bool Record
            {
                get => code.GetBitLittleEndian(3);
                set => code.SetBitLittleEndian(3, value);
            }
            public bool Protection
            {
                get => code.GetBitLittleEndian(4);
                set => code.SetBitLittleEndian(4, value);
            }
            public bool MultiExtent
            {
                get => code.GetBitLittleEndian(7);
                set => code.SetBitLittleEndian(7, value);
            }

            private byte code = 0;

            public FileFlags() { }
            public FileFlags(bool existence = false, bool directory = false, bool associatedFile = false, bool record = false, bool protection = false, bool multiExtent = false)
            {
                Existence = existence;
                Directory = directory;
                AssociatedFile = associatedFile;
                Record = record;
                Protection = protection;
                MultiExtent = multiExtent;
            }

            public void Write(Stream stream)
            {
                stream.WriteByte(code);
            }
            public async Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
            {
                await stream.WriteAsync(new byte[1] { code }, cancellationToken);
            }
        }
        public sealed class Permissions : IBinaryStreamWriter
        {
            //TODO
            private readonly byte[] data = new byte[2] { 0xAA, 0xAA };

            public void Write(Stream stream)
            {
                //TODO
                throw new NotImplementedException();
            }
            public Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
            {
                //TODO
                throw new NotImplementedException();
            }
        }

        public abstract class VolumeDescriptor : IBinaryStreamWriter
        {
            public abstract void Write(Stream stream);
            public abstract Task WriteAsync(Stream stream, CancellationToken cancellationToken = default);
        }
        public sealed class BootRecord : VolumeDescriptor
        {
            public string BootSystemIdentifier
            {
                get => bootSystemIdentifier;
                set
                {
                    if (!IsACharString(value))
                        throw new ArgumentException("characters in boot system identifier should be a-characters", nameof(value));
                    bootSystemIdentifier = value;
                    bsi.FillWithZero();
                    Encoding.ASCII.GetBytes(bootSystemIdentifier).CopyTo(bsi, 0);
                }
            }
            public string BootIdentifier
            {
                get => bootIdentifier;
                set
                {
                    if (!IsACharString(value))
                        throw new ArgumentException("characters in boot identifier should be a-characters", nameof(value));
                    bootIdentifier = value;
                    bi.FillWithZero();
                    Encoding.ASCII.GetBytes(bootIdentifier).CopyTo(bi, 0);
                }
            }
            public byte[] Data
            {
                get => data;
                set
                {
                    data.FillWithZero();
                    value.CopyTo(data, 0);
                }
            }

            private string bootSystemIdentifier = string.Empty;
            private string bootIdentifier = string.Empty;
            private readonly byte[] data = new byte[1977];
            private readonly byte[] bsi = new byte[32];
            private readonly byte[] bi = new byte[32];

            public BootRecord(string bootSystemIdentifier, string bootIdentifier, byte[] data)
            {
                BootSystemIdentifier = bootSystemIdentifier;
                BootIdentifier = bootIdentifier;
                Data = data;
            }

            public override void Write(Stream stream)
            {
                stream.WriteByte(0);
                stream.Write(STANDARD_IDENTIFIER);
                stream.WriteByte(1);
                stream.Write(bsi);
                stream.Write(bi);
                stream.Write(data);
            }
            public override async Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
            {
                await stream.WriteAsync(new byte[1] { 0 }, cancellationToken);
                await stream.WriteAsync(STANDARD_IDENTIFIER, cancellationToken);
                await stream.WriteAsync(new byte[1] { 1 }, cancellationToken);
                await stream.WriteAsync(bsi, cancellationToken);
                await stream.WriteAsync(bi, cancellationToken);
                await stream.WriteAsync(data, cancellationToken);
            }
        }
        public sealed class VolumeDescriptorSetTerminator : VolumeDescriptor
        {
            public override void Write(Stream stream)
            {
                stream.WriteByte(255);
                stream.Write(STANDARD_IDENTIFIER);
                stream.WriteByte(1);
                stream.Write(new byte[1977]);
            }
            public override async Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
            {
                await stream.WriteAsync(new byte[1] { 255 }, cancellationToken);
                await stream.WriteAsync(STANDARD_IDENTIFIER, cancellationToken);
                await stream.WriteAsync(new byte[1] { 1 }, cancellationToken);
                await stream.WriteAsync(new byte[1977], cancellationToken);
            }
        }
        public sealed class PrimaryVolumeDescriptor : VolumeDescriptor
        {
            public string SystemIdentifier
            {
                get => systemIdentifier;
                set
                {
                    if (!IsACharString(value))
                        throw new ArgumentException("characters in system identifier should be a-characters", nameof(value));
                    systemIdentifier = value;
                    si.FillWithZero();
                    Encoding.ASCII.GetBytes(systemIdentifier).CopyTo(si, 0);
                }
            }
            public string VolumeIdentifier
            {
                get => volumeIdentifier;
                set
                {
                    if (!IsDCharString(value))
                        throw new ArgumentException("characters in volume identifier should be d-characters", nameof(value));
                    volumeIdentifier = value;
                    vi.FillWithZero();
                    Encoding.ASCII.GetBytes(volumeIdentifier).CopyTo(vi, 0);
                }
            }
            public ulong VolumeSpaceSize
            {
                get => volumeSpaceSize;
                set => volumeSpaceSize = value;
            }
            public uint VolumeSetSize
            {
                get => volumeSetSize;
                set => volumeSetSize = value;
            }
            public uint VolumeSequenceNumber
            {
                get => volumeSequenceNumber;
                set => volumeSequenceNumber = value;
            }
            public uint LogicalBlockSize
            {
                get => logicalBlockSize;
                set => logicalBlockSize = value;
            }
            public ulong PathTableSize
            {
                get => pathTableSize;
                set => pathTableSize = value;
            }
            public uint LocationOfOccurrenceOfTypeLPathTable
            {
                get => locationOfOccurrenceOfTypeLPathTable;
                set => locationOfOccurrenceOfTypeLPathTable = value;
            }
            public uint LocationOfOptionalOccurrenceOfTypeLPathTable
            {
                get => locationOfOptionalOccurrenceOfTypeLPathTable;
                set => locationOfOptionalOccurrenceOfTypeLPathTable = value;
            }
            public uint LocationOfOccurrenceOfTypeMPathTable
            {
                get => locationOfOccurrenceOfTypeMPathTable;
                set => locationOfOccurrenceOfTypeMPathTable = value;
            }
            public uint LocationOfOptionalOccurrenceOfTypeMPathTable
            {
                get => locationOfOptionalOccurrenceOfTypeMPathTable;
                set => locationOfOptionalOccurrenceOfTypeMPathTable = value;
            }

            private string systemIdentifier = string.Empty;
            private string volumeIdentifier = string.Empty;
            private ulong volumeSpaceSize = 0;
            private uint volumeSetSize = 0;
            private uint volumeSequenceNumber = 0;
            private uint logicalBlockSize = 0;
            private ulong pathTableSize = 0;
            private uint locationOfOccurrenceOfTypeLPathTable = 0;
            private uint locationOfOptionalOccurrenceOfTypeLPathTable = 0;
            private uint locationOfOccurrenceOfTypeMPathTable = 0;
            private uint locationOfOptionalOccurrenceOfTypeMPathTable = 0;
            //TODO

            private readonly byte[] si = new byte[32];
            private readonly byte[] vi = new byte[32];

            public override void Write(Stream stream)
            {
                stream.WriteByte(1);
                stream.Write(STANDARD_IDENTIFIER);
                stream.WriteByte(1);
                stream.WriteByte(0);
                stream.Write(si);
                stream.Write(vi);
                stream.Write(new byte[8]);
                stream.Write(volumeSpaceSize.GetBytesLittleEndian());
                stream.Write(new byte[32]);
                stream.Write(volumeSetSize.GetBytesLittleEndian());
                stream.Write(volumeSequenceNumber.GetBytesLittleEndian());
                stream.Write(logicalBlockSize.GetBytesLittleEndian());
                stream.Write(pathTableSize.GetBytesLittleEndian());
                stream.Write(locationOfOccurrenceOfTypeLPathTable.GetBytesLittleEndian());
                stream.Write(locationOfOptionalOccurrenceOfTypeLPathTable.GetBytesLittleEndian());
                stream.Write(locationOfOccurrenceOfTypeMPathTable.GetBytesLittleEndian());
                stream.Write(locationOfOptionalOccurrenceOfTypeMPathTable.GetBytesLittleEndian());
                //TODO
                throw new NotImplementedException();
            }
            public override Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }
        }
        public sealed class SupplementaryVolumeDescriptor : VolumeDescriptor
        {
            //TODO

            public override void Write(Stream stream)
            {
                stream.WriteByte(2);
                stream.Write(STANDARD_IDENTIFIER);
                stream.WriteByte(1);
                //TODO
                throw new NotImplementedException();
            }
            public override Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
            {
                //TODO
                throw new NotImplementedException();
            }
        }
        public sealed class EnhancedVolumeDescriptor : VolumeDescriptor
        {
            //TODO

            public override void Write(Stream stream)
            {
                stream.WriteByte(2);
                stream.Write(STANDARD_IDENTIFIER);
                stream.WriteByte(2);
                //TODO
                throw new NotImplementedException();
            }
            public override Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
            {
                //TODO
                throw new NotImplementedException();
            }
        }
        public sealed class VolumePartitionDescriptor : VolumeDescriptor
        {
            //TODO

            public override void Write(Stream stream)
            {
                //TODO
                throw new NotImplementedException();
            }
            public override Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
            {
                //TODO
                throw new NotImplementedException();
            }
        }

        public sealed class DirectoryRecord : IBinaryStreamWriter
        {
            public byte LengthOfDirectoryRecord
            {
                get => lengthOfDirectoryRecord;
                set => lengthOfDirectoryRecord = value;
            }
            public byte ExtendedAttributeRecordLength
            {
                get => extendedAttributeRecordLength;
                set => extendedAttributeRecordLength = value;
            }
            public ulong LocationOfExtent
            {
                get => locationOfExtent;
                set => locationOfExtent = value;
            }
            public ulong DataLength
            {
                get => dataLength;
                set => dataLength = value;
            }
            public RecordingDateAndTime RecordingDateAndTime
            {
                get => recordingDateAndTime;
                set => recordingDateAndTime = value ?? throw new ArgumentNullException(nameof(value));
            }
            public FileFlags FileFlags
            {
                get => fileFlags;
                set => fileFlags = value ?? throw new ArgumentNullException(nameof(value));
            }
            public byte FileUnitSize
            {
                get => fileUnitSize;
                set => fileUnitSize = value;
            }
            public byte InterleaveGapSize
            {
                get => interleaveGapSize;
                set => interleaveGapSize = value;
            }
            public uint VolumeSequenceNumber
            {
                get => volumeSequenceNumber;
                set => volumeSequenceNumber = value;
            }

            private byte lengthOfDirectoryRecord;
            private byte extendedAttributeRecordLength;
            private ulong locationOfExtent;
            private ulong dataLength;
            private RecordingDateAndTime recordingDateAndTime = new();
            private FileFlags fileFlags = new();
            private byte fileUnitSize;
            private byte interleaveGapSize;
            private uint volumeSequenceNumber;
            //TODO

            public void Write(Stream stream)
            {
                //TODO
                throw new NotImplementedException();
            }
            public Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
            {
                //TODO
                throw new NotImplementedException();
            }
        }
        public sealed class PathTableRecord : IBinaryStreamWriter
        {
            //TODO

            public void Write(Stream stream)
            {
                //TODO
                throw new NotImplementedException();
            }
            public Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
            {
                //TODO
                throw new NotImplementedException();
            }
        }
        public sealed class ExtendedAttributeRecord : IBinaryStreamWriter
        {
            //TODO

            public void Write(Stream stream)
            {
                //TODO
                throw new NotImplementedException();
            }
            public Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
            {
                //TODO
                throw new NotImplementedException();
            }
        }

        public void Write(Stream stream)
        {
            throw new NotImplementedException();
        }
        public Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        private static bool IsAChar(char c)
        {
            if (c >= 0x20 && c <= 0x22)
                return true;
            if (c >= 0x25 && c <= 0x3F)
                return true;
            if (c >= 0x41 && c <= 0x5A)
                return true;
            if (c == 0x5F)
                return true;
            return false;
        }
        private static bool IsDChar(char c)
        {
            if (c >= 0x30 && c <= 0x39)
                return true;
            if (c >= 0x41 && c <= 0x5A)
                return true;
            if (c == 0x5F)
                return true;
            return false;
        }
        private static bool IsACharString(string s)
        {
            var chars = s.ToCharArray();
            foreach (var c in chars)
                if (!IsAChar(c))
                    return false;
            return true;
        }
        private static bool IsDCharString(string s)
        {
            var chars = s.ToCharArray();
            foreach (var c in chars)
                if (!IsDChar(c))
                    return false;
            return true;
        }
    }
}