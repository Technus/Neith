using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.IO
{
    /// <summary>
    /// MemoryTributary is a re-implementation of MemoryStream that uses a dynamic list of byte arrays as a backing store, instead of a single byte array, the allocation
    /// of which will fail for relatively small streams as it requires contiguous memory.
    /// </summary>
    public class MemoryTributary : Stream      /* http://msdn.microsoft.com/en-us/library/system.io.stream.aspx */
    {
        #region Constructors

        public MemoryTributary(){}

        public MemoryTributary(byte[] source)
        {
            Write(source, 0, source.Length);
            Position = 0;
        }

        public MemoryTributary(long length)
        {
            SetLength(length);
            GetBlock((int)(length >> blockShift));   //access block to prompt the allocation of memory
        }

        #endregion

        #region Status Properties

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        #endregion

        #region Public Properties

        public override long Length
        {
            get { return length; }
        }

        public override long Position { get; set; } = 0;

        public override int ReadTimeout { get; set; } = 0;

        public long Remaining
        {
            get
            {
                return length - Position;
            }
        }

        #endregion

        #region Members

        protected long length = 0;

        protected long required = 0;
        protected readonly Thread readWait = new Thread(()=>
        {

        });

        protected const int maxBlockCount = 2048;
        protected const byte blockShift = 16;
        protected const int blockSize = 1<<blockShift;
        protected const int blockMask = blockSize-1;
        protected const int addressMask = ~blockMask;

        protected volatile List<byte[]> blocks = new List<byte[]>();

        #endregion

        #region Internal Properties

        /* Use these properties to gain access to the appropriate block of memory for the current Position */

        /// <summary>
        /// The block of memory currently addressed by Position
        /// </summary>
        protected byte[] Block
        {
            get
            {
                return GetBlock(BlockId);
            }
        }
        private byte[] GetBlock(int id)
        {
            if (id >= maxBlockCount)
            {
                throw new ArgumentOutOfRangeException("id",id,"Cannot exceeed maxBlockCount="+maxBlockCount);
            }
            while (blocks.Count <= id)
            {
                blocks.Add(new byte[blockSize]);
            }
            return blocks[id];
        }
        /// <summary>
        /// The id of the block currently addressed by Position
        /// </summary>
        protected int BlockId
        {
            get { return (int)(Position >> blockShift); }
        }
        /// <summary>
        /// The offset of the byte currently addressed by Position, into the block that contains it
        /// </summary>
        protected long BlockOffset
        {
            get { return Position & blockMask; }
        }

        protected long ByteAddress
        {
            get { return Position & addressMask; }
        }

        #endregion

        #region Public Stream Methods

        public override void Flush()
        {
            Position = Length;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", count, "Number of bytes to copy cannot be negative.");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", offset, "Destination offset cannot be negative.");
            }

            if (ReadTimeout == -1 || ReadTimeout>0)//infinite
            {
                if(!SpinWait.SpinUntil(() => Remaining >= count, ReadTimeout))
                {
                    throw new TimeoutException("Timeout while reading bytes");
                }
            }
            else if (ReadTimeout < 0)
            {
                throw new ArgumentOutOfRangeException("ReadTimeout",ReadTimeout,"ReadTimeout must be >=-1");
            }


            long lcount = count > Remaining ? Remaining : count;
            long loffset = offset;
            long read = 0;
            long copysize = 0;
            do
            {
                copysize = Math.Min(lcount, blockSize - BlockOffset);
                Buffer.BlockCopy(Block, (int)BlockOffset, buffer, (int)loffset, (int)copysize);
                lcount -= copysize;
                loffset += copysize;
                read += copysize;
                Position += copysize;
            } while (lcount > 0);

            return (int)read;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    break;
                case SeekOrigin.Current:
                    Position += offset;
                    break;
                case SeekOrigin.End:
                    Position = length - offset;
                    break;
            }
            return Position;
        }

        public override void SetLength(long value)
        {
            if (value <= 0)
            {
                if (blocks.Count > 1)
                {
                    blocks.RemoveRange(1, blocks.Count - 1);
                }
                length = 0;
            }
            else
            {
                length = value;
            }
        }

        protected void EnsureCapacity(long intended_length)
        {
            if (intended_length > length)
            {
                length = intended_length;
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("Writing is not supported");
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", count, "Number of bytes to copy cannot be negative.");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer", "Buffer cannot be null.");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", offset, "Destination offset cannot be negative.");
            }

            long initialPosition = Position;
            long copysize;
            long lcount = count;
            long loffset = offset;
            try
            {
                do
                {
                    copysize = (int)Math.Min(lcount, blockSize - BlockOffset);

                    EnsureCapacity(Position + copysize);

                    Buffer.BlockCopy(buffer, (int)loffset, Block, (int)BlockOffset, (int)copysize);
                    lcount -= copysize;
                    loffset += copysize;

                    Position += copysize;

                } while (lcount > 0);
            }
            catch (Exception e)
            {
                Position = initialPosition;
                throw e;
            }
        }

        public override int ReadByte()
        {
            if (ReadTimeout == -1 || ReadTimeout > 0)//infinite
            {
                if (!SpinWait.SpinUntil(() => Remaining >= 1, ReadTimeout))
                {
                    throw new TimeoutException("Timeout while reading byte");
                }
            }
            else if (ReadTimeout < 0)
            {
                throw new ArgumentOutOfRangeException("ReadTimeout", ReadTimeout, "ReadTimeout must be >=-1");
            }

            if (Position >= length)
                return -1;

            byte b = Block[BlockOffset];
            Position++;

            return b;
        }

        public override void WriteByte(byte value)
        {
            throw new NotSupportedException("Writing is not supported");
            try
            {
                EnsureCapacity(Position + 1);
                Block[BlockOffset] = value;
            }
            catch(Exception e)
            {
                throw e;
            }
            Position++;
        }

        public void AppendByte(byte value)
        {
            try
            {
                GetBlock((int)(length >> blockShift))[length & blockMask] = value;
            }
            catch(Exception e)
            {
                throw e;
            }
            length++;
        }

        #endregion

        #region IDispose

        /* http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx */
        protected override void Dispose(bool disposing)
        {
            /* We do not currently use unmanaged resources */
            base.Dispose(disposing);
        }
        #endregion

        #region Public Additional Helper Methods

        /// <summary>
        /// Returns the entire content of the stream as a byte array. This is not safe because the call to new byte[] may 
        /// fail if the stream is large enough. Where possible use methods which operate on streams directly instead.
        /// </summary>
        /// <returns>A byte[] containing the current data in the stream</returns>
        public byte[] ToArray()
        {
            long firstposition = Position;
            Position = 0;
            byte[] destination = new byte[length];
            Read(destination, 0, (int)length);
            Position = firstposition;
            return destination;
        }

        /// <summary>
        /// Reads length bytes from source into the this instance at the current position.
        /// </summary>
        /// <param name="source">The stream containing the data to copy</param>
        /// <param name="length">The number of bytes to copy</param>
        public void ReadFrom(Stream source, long length)
        {
            byte[] buffer = new byte[4096];
            int read;
            do
            {
                read = source.Read(buffer, 0, (int)Math.Min(4096, length));
                length -= read;
                Write(buffer, 0, read);

            } while (length > 0);
        }

        /// <summary>
        /// Writes the entire stream into destination, regardless of Position, which remains unchanged.
        /// </summary>
        /// <param name="destination">The stream to write the content of this stream to</param>
        public void WriteTo(Stream destination)
        {
            long initialpos = Position;
            Position = 0;
            CopyTo(destination);
            Position = initialpos;
        }

        #endregion
    }
}
