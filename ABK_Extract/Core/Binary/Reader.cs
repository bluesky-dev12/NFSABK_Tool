

namespace ABK_Extract.Binary
{
    using System;
    using System.IO;
    using System.Text;

    public class Reader : IDisposable
    {
        private BinaryReader reader;
        private byte[] header;

        public Reader(string header)
        {
            // Initialize the BinaryReader with a MemoryStream containing the header
            FileStream fs = new FileStream(header, FileMode.Open);
            reader = new BinaryReader(fs);
        }

        public byte[] Header
        {
            get { return header; }
            set { header = value; }
        }

        public byte ReadByte()
        {
            return reader.ReadByte();
        }
        public byte[] ReadByteArray(int length)
        {
            // Return a portion of the header based on the specified length
            byte[] result = reader.ReadBytes(length);
            return result;
        }

        public short ReadShort()
        {
            return reader.ReadInt16();
        }
        public ushort ReadUShort()
        {
            return reader.ReadUInt16();
        }

        public int ReadInt32()
        {
            return reader.ReadInt32();
        }

        public string ReadStringByLenght(int length)
        {
            byte[] array = ReadByteArray(length);
            bool allBytesAreFF = true;

            foreach (byte b in array)
            {
                if (b != 0xFF)
                {
                    allBytesAreFF = false;
                    break;
                }
            }

            if (allBytesAreFF)
            {
                // If all bytes are 0xFF, return "(None)"
                return "(None)";
            }
            else
            {
                // If any byte is not 0xFF, return the string representation
                return Encoding.ASCII.GetString(array);
            }
        }
        public string ReadString()
        {
            return reader.ReadString();
        }

        public string IntToHex()
        {
            int intavalue = reader.ReadInt32();
            string hexString = intavalue.ToString("X");
            string hexvalue = "0x" + hexString;
            return hexvalue;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    reader?.Dispose();
                }

                disposed = true;
            }
        }

        public string ReadEnum(byte value)
        {
            int intValue = (int)value;
            if (Enum.IsDefined(typeof(ABKHeader.PlatformEnum), intValue))
            {
                return ((ABKHeader.PlatformEnum)intValue).ToString();
            }
            else
            {
                // Handle invalid enum value
                throw new InvalidDataException("Invalid enum value");
            }
        }

        public void Position()
        {
            // Now you can use the reader object safely
            long offset = reader.BaseStream.Position;
            string hexOffset = $"0x{offset:X}";
            Console.WriteLine($"Current offset: {hexOffset}");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
