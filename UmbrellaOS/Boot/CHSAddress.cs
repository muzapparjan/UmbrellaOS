using UmbrellaOS.Boot.Interfaces;

namespace UmbrellaOS.Boot
{
    public struct CHSAddress : ICHSAddress
    {
        public int Cylinder
        {
            get => cylinder;
            set
            {
                if (value < 0 || value > 1023)
                    throw new ArgumentOutOfRangeException(nameof(value), "cylinder should be from 0 to 1023");
                cylinder = value;
            }
        }
        public int Head
        {
            get => head;
            set
            {
                if (value < 0 || value > 254)
                    throw new ArgumentOutOfRangeException(nameof(value), "head should be from 0 to 254");
                head = value;
            }
        }
        public int Sector
        {
            get => sector;
            set
            {
                if (value < 1 || value > 63)
                    throw new ArgumentOutOfRangeException(nameof(value), "sector should be from 1 to 63");
                sector = value;
            }
        }
        public byte[] Value
        {
            get
            {
                var bytes = new byte[3];
                bytes[2] = (byte)(Cylinder >> 2);
                throw new NotImplementedException();//下班下班!
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        private int cylinder = 0;
        private int head = 0;
        private int sector = 1;

        public CHSAddress(int cylinder, int head, int sector)
        {
            Cylinder = cylinder;
            Head = head;
            Sector = sector;
        }

        public void Write(Stream stream)
        {
            stream.Write(Value);
        }
        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            await stream.WriteAsync(Value, cancellationToken);
        }
    }
}