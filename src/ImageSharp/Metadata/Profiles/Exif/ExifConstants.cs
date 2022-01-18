// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Buffers.Binary;
using System.Text;
using static SixLabors.ImageSharp.Metadata.Profiles.Exif.EncodedString;

namespace SixLabors.ImageSharp.Metadata.Profiles.Exif
{
    internal static class ExifConstants
    {
        public const int CharacterCodeBytesLength = 8;

        private const ulong AsciiCode = 0x_41_53_43_49_49_00_00_00;
        private const ulong JISCode = 0x_4A_49_53_00_00_00_00_00;
        private const ulong UnicodeCode = 0x_55_4E_49_43_4F_44_45_00;
        private const ulong UndefinedCode = 0x_00_00_00_00_00_00_00_00;

        public static ReadOnlySpan<byte> LittleEndianByteOrderMarker => new byte[]
        {
            (byte)'I',
            (byte)'I',
            0x2A,
            0x00,
        };

        public static ReadOnlySpan<byte> BigEndianByteOrderMarker => new byte[]
        {
            (byte)'M',
            (byte)'M',
            0x00,
            0x2A
        };

        // UTF-8 is better than ASCII, UTF-8 encodes the ASCII codes the same way
        public static Encoding DefaultEncoding => Encoding.UTF8;

        public static Encoding JIS0208Encoding => Encoding.GetEncoding(932);

        private static ReadOnlySpan<byte> AsciiCodeBytes => new byte[] { 0x41, 0x53, 0x43, 0x49, 0x49, 0, 0, 0 };

        private static ReadOnlySpan<byte> JISCodeBytes => new byte[] { 0x4A, 0x49, 0x53, 0, 0, 0, 0, 0 };

        private static ReadOnlySpan<byte> UnicodeCodeBytes => new byte[] { 0x55, 0x4E, 0x49, 0x43, 0x4F, 0x44, 0x45, 0 };

        private static ReadOnlySpan<byte> UndefinedCodeBytes => new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

        public static bool TryDetect(ReadOnlySpan<byte> buffer, out CharacterCode code)
        {
            if (buffer.Length >= CharacterCodeBytesLength)
            {
                ulong test = BinaryPrimitives.ReadUInt64LittleEndian(buffer);
                switch (test)
                {
                    case AsciiCode:
                        code = CharacterCode.ASCII;
                        return true;
                    case JISCode:
                        code = CharacterCode.JIS;
                        return true;
                    case UnicodeCode:
                        code = CharacterCode.Unicode;
                        return true;
                    case UndefinedCode:
                        code = CharacterCode.Undefined;
                        return true;
                    default:
                        break;
                }
            }

            code = default;
            return false;
        }

        public static ReadOnlySpan<byte> GetCodeBytes(CharacterCode code) => code switch
        {
            CharacterCode.ASCII => AsciiCodeBytes,
            CharacterCode.JIS => JISCodeBytes,
            CharacterCode.Unicode => UnicodeCodeBytes,
            CharacterCode.Undefined => UndefinedCodeBytes,
            _ => UndefinedCodeBytes
        };

        public static Encoding GetEncoding(CharacterCode code) => code switch
        {
            CharacterCode.ASCII => Encoding.ASCII,
            CharacterCode.JIS => JIS0208Encoding,
            CharacterCode.Unicode => Encoding.Unicode,
            CharacterCode.Undefined => Encoding.UTF8,
            _ => Encoding.UTF8
        };
    }
}
