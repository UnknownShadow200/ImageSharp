// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using System;

namespace SixLabors.ImageSharp.Metadata.Profiles.Exif
{
    /// <summary>
    /// The EXIF encoded string structure.
    /// </summary>
    public readonly struct EncodedString : IEquatable<EncodedString>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EncodedString" /> struct.
        /// </summary>
        /// <param name="text">The text value.</param>
        public EncodedString(string text)
          : this(CharacterCode.Unicode, text)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodedString" /> struct.
        /// </summary>
        /// <param name="code">The character code.</param>
        /// <param name="text">The text value.</param>
        public EncodedString(CharacterCode code, string text)
        {
            this.Text = text;
            this.Code = code;
        }

        /// <summary>
        /// The 8-byte character code enum.
        /// </summary>
        public enum CharacterCode
        {
            /// <summary>
            /// The ASCII (ITU-T T.50 IA5) character code.
            /// </summary>
            ASCII,

            /// <summary>
            /// The JIS (X208-1990) character code.
            /// </summary>
            JIS,

            /// <summary>
            /// The Unicode character code.
            /// </summary>
            Unicode,

            /// <summary>
            /// The undefined character code.
            /// </summary>
            Undefined
        }

        /// <summary>
        /// Gets the character ode.
        /// </summary>
        public CharacterCode Code { get; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        public string Text { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is EncodedString other && this.Equals(other);

        /// <inheritdoc/>
        public bool Equals(EncodedString other) => this.Text == other.Text && this.Code == other.Code;

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(this.Text, this.Code);

        /// <inheritdoc/>
        public override string ToString() => this.Text;
    }
}
