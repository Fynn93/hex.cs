using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.cs
{
    public class Hexadecimal
    {
        /// <summary>
        /// Gets Hex Values from a File
        /// </summary>
        /// <param name="file">The File to parse</param>
        /// <param name="offset">The offset to get the values at</param>
        /// <param name="length">The length for the values</param>
        /// <returns>The Hex Values without 0x</returns>
        public static string GetHex(string file, long offset, int length)
        {
            using Stream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return GetHex(stream, offset, length);
        }

        /// <summary>
        /// Gets Hex Values from a Stream
        /// </summary>
        /// <param name="file">The Stream to parse</param>
        /// <param name="offset">The offset to get the values at</param>
        /// <param name="length">The length for the values</param>
        /// <returns>The Hex Values without 0x</returns>
        public static string GetHex(Stream fileStream, long offset, int length)
        {
            BinaryReader brFile = new(fileStream);
            fileStream.Position = offset;

            List<byte> offsetByte = brFile.ReadBytes(length).ToList();

            return string.Join(" ", offsetByte.Select(x => "" + x.ToString("x2")).ToArray()).ToUpper();
        }

        /// <summary>
        /// Gets Hex Values from a File as a String
        /// </summary>
        /// <param name="file">The File to parse</param>
        /// <param name="offset">The offset to get the values at</param>
        /// <param name="length">The length for the values</param>
        /// <returns>The Hex Values converted to Text</returns>        
        public static string GetString(string file, long offset, int length)
        {
            using Stream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return GetString(stream, offset, length);
        }

        /// <summary>
        /// Gets Hex Values from a File as a String
        /// </summary>
        /// <param name="file">The Stream to parse</param>
        /// <param name="offset">The offset to get the values at</param>
        /// <param name="length">The length for the values</param>
        /// <returns>The Hex Values converted to Text</returns>           
        public static string GetString(Stream fileStream, long offset, int length)
        {
            BinaryReader brFile = new(fileStream);
            fileStream.Position = offset;

            byte[] offsetByte = brFile.ReadBytes(length);

            string outstring = BitConverter.ToString(offsetByte).Replace("-", "");
            byte[] data = FromHex(outstring);
            return Encoding.ASCII.GetString(data);
        }

        /// <summary>
        /// Gets Bits from a Hex Value
        /// </summary>
        /// <param name="hexValue">The Hex Value</param>
        /// <returns>The Hex Values as bits</returns>   
        public static string GetBits(string hexValue)
        {
            var lup = new Dictionary<char, string>{
            { '0', "0000"},
            { '1', "0001"},
            { '2', "0010"},
            { '3', "0011"},

            { '4', "0100"},
            { '5', "0101"},
            { '6', "0110"},
            { '7', "0111"},

            { '8', "1000"},
            { '9', "1001"},
            { 'A', "1010"},
            { 'B', "1011"},

            { 'C', "1100"},
            { 'D', "1101"},
            { 'E', "1110"},
            { 'F', "1111"}};

            var ret = string.Join("", from character in hexValue.Replace(" ", "") select lup[character]);
            return ret;
        }

        /// <summary>
        /// Gets a Byte Array from Hex Values
        /// </summary>
        /// <param name="hex">The Hex Values</param>
        /// <returns>A Byte Array with the Hex Values</returns>
        public static byte[] FromHex(string hex)
        {
            if (hex.Contains('-'))
                hex = hex.Replace("-", "");
            
            if (hex.Contains(' '))
                hex = hex.Replace(" ", "");

            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }

        /// <summary>
        /// Converts Bits to Hex Values
        /// </summary>
        /// <param name="binary">The Bit Values</param>
        /// <returns>A String with Hex Values from the Bits</returns>
        public static string BitsToHex(string binary)
        {
            if (string.IsNullOrEmpty(binary))
                return binary;

            StringBuilder result = new(binary.Length / 8 + 1);

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }
    }
}
