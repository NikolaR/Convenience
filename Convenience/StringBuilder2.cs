using System;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace Convenience
{
    /// <summary>
    /// Represents a mutable string of characters. This class cannot be inherited.
    /// </summary>
    public class StringBuilder2
    {
        private StringBuilder _wrapped;
        private int _indentLevel;
        private string _indentLevelContent;
        private bool _indentOnNextAppend;

        /// <summary>
        /// Initializes a new instance of the System.Text.StringBuilder class.
        /// </summary>
        public StringBuilder2()
        {
            _wrapped = new StringBuilder();
        }

        /// <summary>
        /// Initializes a new instance of the System.Text.StringBuilder class using the
        /// specified capacity.
        /// </summary>
        /// <param name="capacity">The suggested starting size of this instance.</param>
        public StringBuilder2(int capacity)
        {
            _wrapped = new StringBuilder(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the System.Text.StringBuilder class using the
        /// specified string.
        /// </summary>
        /// <param name="value">
        /// The string used to initialize the value of the instance. If value is null,
        /// the new System.Text.StringBuilder will contain the empty string (that is,
        /// it contains System.String.Empty).
        /// </param>
        public StringBuilder2(string value)
        {
            _wrapped = new StringBuilder(value);
        }

        /// <summary>
        /// Initializes a new instance of the System.Text.StringBuilder class that starts
        /// with a specified capacity and can grow to a specified maximum.
        /// </summary>
        /// <param name="capacity">The suggested starting size of the System.Text.StringBuilder.</param>
        /// <param name="maxCapacity">The maximum number of characters the current string can contain.</param>
        public StringBuilder2(int capacity, int maxCapacity)
        {
            _wrapped = new StringBuilder(capacity, maxCapacity);
        }

        /// <summary>
        /// Initializes a new instance of the System.Text.StringBuilder class using the
        /// specified string and capacity.
        /// </summary>
        /// <param name="value">
        /// The string used to initialize the value of the instance. If value is null,
        /// the new System.Text.StringBuilder will contain the empty string (that is,
        /// it contains System.String.Empty).
        /// </param>
        /// <param name="capacity">The suggested starting size of the System.Text.StringBuilder.</param>
        public StringBuilder2(string value, int capacity)
        {
            _wrapped = new StringBuilder(value, capacity);
        }

        /// <summary>
        /// Initializes a new instance of the System.Text.StringBuilder class from the
        /// specified substring and capacity.
        /// </summary>
        /// <param name="value">
        /// The string that contains the substring used to initialize the value of this
        /// instance. If value is null, the new System.Text.StringBuilder will contain
        /// the empty string (that is, it contains System.String.Empty).
        /// </param>
        /// <param name="startIndex">The position within value where the substring begins.</param>
        /// <param name="length">The number of characters in the substring.</param>
        /// <param name="capacity">The suggested starting size of the System.Text.StringBuilder.</param>
        public StringBuilder2(string value, int startIndex, int length, int capacity)
        {
            _wrapped = new StringBuilder(value, startIndex, length, capacity);
        }

        /// <summary>
        /// Specifies how many levels new lines should be indented
        /// </summary>
        public int IndentLevel
        {
            get { return _indentLevel; }
            set
            {
                if (_indentLevel < 0)
                    throw new ArgumentException("IndentLevel cannot be lower than 0");
                if (_indentLevel > 100)
                    throw new ArgumentException("IndentLevel cannot be higher than 100");
                _indentLevel = value;
            }
        }

        /// <summary>
        /// Reprents content of one indentation level after each new line.
        /// </summary>
        public string IndentLevelContent
        {
            get
            {
                if (_indentLevelContent == null)
                    _indentLevelContent = "    ";
                return _indentLevelContent;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _indentLevelContent = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of characters that can be contained in the
        /// memory allocated by the current instance.
        /// </summary>
        /// <returns>
        /// The maximum number of characters that can be contained in the memory allocated
        /// by the current instance.
        /// </returns>
        public int Capacity
        {
            get
            {
                return _wrapped.Capacity;
            }
            set
            {
                _wrapped.Capacity = value;
            }
        }

        /// <summary>
        /// Gets or sets the length of the current System.Text.StringBuilder object.
        /// </summary>
        /// <returns>The length of this instance.</returns>
        public int Length
        {
            get
            {
                return _wrapped.Length;
            }
            set
            {
                _wrapped.Length = value;
            }
        }

        /// <summary>
        /// Gets the maximum capacity of this instance.
        /// </summary>
        /// <returns>The maximum number of characters this instance can hold.</returns>
        public int MaxCapacity
        {
            get
            {
                return _wrapped.MaxCapacity;
            }
        }

        /// <summary>
        /// Gets or sets the character at the specified character position in this instance.
        /// </summary>
        /// <param name="index">The position of the character.</param>
        /// <returns>The Unicode character at position index.</returns>
        public char this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = value;
            }
        }

        /// <summary>
        /// Appends the string representation of a specified Boolean value to this instance.
        /// </summary>
        /// <param name="value">The Boolean value to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(bool value)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value);
            return this;
        }

        /// <summary>
        /// Appends the string representation of a specified 8-bit unsigned integer to
        /// this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(byte value)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value);
            return this;
        }

        /// <summary>
        /// Appends the string representation of a specified Unicode character to this
        /// instance.
        /// </summary>
        /// <param name="value">The Unicode character to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(char value)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value);
            return this;
        }

        /// <summary>
        /// Appends the string representation of the Unicode characters in a specified
        /// array to this instance.
        /// </summary>
        /// <param name="value">The array of characters to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(char[] value)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value);
            return this;
        }

        /// <summary>
        /// Appends the string representation of a specified decimal number to this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(decimal value)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value);
            return this;
        }

        /// <summary>
        /// Appends the string representation of a specified double-precision floating-point
        /// number to this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(double value)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value);
            return this;
        }

        /// <summary>
        /// Appends the string representation of a specified single-precision floating-point
        /// number to this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(float value)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value);
            return this;
        }

        /// <summary>
        /// Appends the string representation of a specified 32-bit signed integer to
        /// this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(int value)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value);
            return this;
        }

        /// <summary>
        /// Appends the string representation of a specified 64-bit signed integer to
        /// this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(long value)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value);
            return this;
        }

        /// <summary>
        /// Appends the string representation of a specified object to this instance.
        /// </summary>
        /// <param name="value">The object to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(object value)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value);
            return this;
        }

        /// <summary>
        /// Appends the string representation of a specified 8-bit signed integer to
        /// this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(sbyte value)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value);
            return this;
        }

        /// <summary>
        /// Appends the string representation of a specified 16-bit signed integer to
        /// this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(short value)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value);
            return this;
        }

        /// <summary>
        /// Appends a copy of the specified string to this instance.
        /// </summary>
        /// <param name="value">The string to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(string value)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value);
            return this;
        }

        /// <summary>
        /// Appends the string representation of a specified 32-bit unsigned integer
        /// to this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(uint value)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value);
            return this;
        }

        /// <summary>
        /// Appends the string representation of a specified 64-bit unsigned integer
        /// to this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(ulong value)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value);
            return this;
        }

        /// <summary>
        /// Appends the string representation of a specified 16-bit unsigned integer
        /// to this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(ushort value)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value);
            return this;
        }

        /// <summary>
        /// Appends a specified number of copies of the string representation of a Unicode
        /// character to this instance.
        /// </summary>
        /// <param name="value">The character to append.</param>
        /// <param name="repeatCount">The number of times to append value.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(char value, int repeatCount)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value, repeatCount);
            return this;
        }

        /// <summary>
        /// Appends the string representation of a specified subarray of Unicode characters
        /// to this instance.
        /// </summary>
        /// <param name="value">A character array.</param>
        /// <param name="startIndex">The starting position in value.</param>
        /// <param name="charCount">The number of characters to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(char[] value, int startIndex, int charCount)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value, startIndex, charCount);
            return this;
        }

        /// <summary>
        /// Appends a copy of a specified substring to this instance.
        /// </summary>
        /// <param name="value">The string that contains the substring to append.</param>
        /// <param name="startIndex">The starting position of the substring within value.</param>
        /// <param name="count">The number of characters in value to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 Append(string value, int startIndex, int count)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.Append(value, startIndex, count);
            return this;
        }

        /// <summary>
        /// Appends the string returned by processing a composite format string, which
        /// contains zero or more format items, to this instance. Each format item is
        /// replaced by the string representation of a single argument.
        /// </summary>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="arg0">An object to format.</param>
        /// <returns>
        /// A reference to this instance with format appended. Each format item in format
        /// is replaced by the string representation of arg0.
        /// </returns>
        public StringBuilder2 AppendFormat(string format, object arg0)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.AppendFormat(format, arg0);
            return this;
        }

        /// <summary>
        /// Appends the string returned by processing a composite format string, which
        /// contains zero or more format items, to this instance. Each format item is
        /// replaced by the string representation of a corresponding argument in a parameter
        /// array.
        /// </summary>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="args">An array of objects to format.</param>
        /// <returns>
        /// A reference to this instance with format appended. Each format item in format
        /// is replaced by the string representation of the corresponding object argument.
        /// </returns>
        public StringBuilder2 AppendFormat(string format, params object[] args)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.AppendFormat(format, args);
            return this;
        }

        /// <summary>
        /// Appends the string returned by processing a composite format string, which
        /// contains zero or more format items, to this instance. Each format item is
        /// replaced by the string representation of a corresponding argument in a parameter
        /// array using a specified format provider.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="args">An array of objects to format.</param>
        /// <returns>
        /// A reference to this instance after the append operation has completed. After
        /// the append operation, this instance contains any data that existed before
        /// the operation, suffixed by a copy of format where any format specification
        /// is replaced by the string representation of the corresponding object argument.
        /// </returns>
        public StringBuilder2 AppendFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.AppendFormat(provider, format, args);
            return this;
        }

        /// <summary>
        /// Appends the string returned by processing a composite format string, which
        /// contains zero or more format items, to this instance. Each format item is
        /// replaced by the string representation of either of two arguments.
        /// </summary>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <returns>
        /// A reference to this instance with format appended. Each format item in format
        /// is replaced by the string representation of the corresponding object argument.
        /// </returns>
        public StringBuilder2 AppendFormat(string format, object arg0, object arg1)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.AppendFormat(format, arg0, arg1);
            return this;
        }

        /// <summary>
        /// Appends the string returned by processing a composite format string, which
        /// contains zero or more format items, to this instance. Each format item is
        /// replaced by the string representation of either of three arguments.
        /// </summary>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <param name="arg2">The third object to format.</param>
        /// <returns>
        /// A reference to this instance with format appended. Each format item in format
        /// is replaced by the string representation of the corresponding object argument.
        /// </returns>
        public StringBuilder2 AppendFormat(string format, object arg0, object arg1, object arg2)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.AppendFormat(format, arg0, arg1, arg2);
            return this;
        }

        /// <summary>
        /// Appends the default line terminator to the end of the current System.Text.StringBuilder
        /// object.
        /// </summary>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 AppendLine()
        {
            _indentOnNextAppend = true;
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.AppendLine();
            _indentOnNextAppend = true;
            return this;
        }

        /// <summary>
        /// Appends a copy of the specified string followed by the default line terminator
        /// to the end of the current System.Text.StringBuilder object.
        /// </summary>
        /// <param name="value">The string to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder2 AppendLine(string value)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.AppendLine(value);
            _indentOnNextAppend = true;
            return this;
        }

        /// <summary>
        /// Appends the string returned by processing a composite format string, which
        /// contains zero or more format items, to this instance. Each format item is
        /// replaced by the string representation of either of three arguments.
        /// </summary>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="arg0">The first object to format.</param>
        /// <param name="arg1">The second object to format.</param>
        /// <param name="arg2">The third object to format.</param>
        /// <returns>
        /// A reference to this instance with format appended. Each format item in format
        /// is replaced by the string representation of the corresponding object argument.
        /// </returns>
        public StringBuilder2 AppendLineFormat(string format, object arg0, object arg1, object arg2)
        {
            if (_indentOnNextAppend)
            {
                WriteIndent();
                _indentOnNextAppend = false;
            }
            _wrapped.AppendFormat(format, arg0, arg1, arg2);
            _wrapped.AppendLine();
            _indentOnNextAppend = true;
            return this;
        }

        /// <summary>
        /// Removes all characters from the current System.Text.StringBuilder instance.
        /// </summary>
        /// <returns>An object whose System.Text.StringBuilder.Length is 0 (zero).</returns>
        public StringBuilder2 Clear()
        {
            _wrapped.Clear();
            return this;
        }

        /// <summary>
        /// Copies the characters from a specified segment of this instance to a specified
        /// segment of a destination System.Char array.
        /// </summary>
        /// <param name="sourceIndex">
        /// The starting position in this instance where characters will be copied from.
        /// The index is zero-based.
        /// </param>
        /// <param name="destination">The array where characters will be copied.</param>
        /// <param name="destinationIndex">
        /// The starting position in destination where characters will be copied. The
        /// index is zero-based.
        /// </param>
        /// <param name="count">The number of characters to be copied.</param>
        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
        {
            _wrapped.CopyTo(sourceIndex, destination, destinationIndex, count);
        }

        /// <summary>
        /// Ensures that the capacity of this instance of System.Text.StringBuilder is
        /// at least the specified value.
        /// </summary>
        /// <param name="capacity">The minimum capacity to ensure.</param>
        /// <returns>The new capacity of this instance.</returns>
        public int EnsureCapacity(int capacity)
        {
            return _wrapped.EnsureCapacity(capacity);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified
        /// object.
        /// </summary>
        /// <param name="sb">An object to compare with this instance, or null.</param>
        /// <returns>
        /// true if this instance and sb have equal string, System.Text.StringBuilder.Capacity,
        /// and System.Text.StringBuilder.MaxCapacity values; otherwise, false.
        /// </returns>
        public bool Equals(StringBuilder sb)
        {
            return _wrapped.Equals(sb);
        }

        /// <summary>
        /// Inserts the string representation of a Boolean value into this instance at
        /// the specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">The value to insert.</param>
        /// <returns>A reference to this instance after the insert operation has completed.</returns>
        public StringBuilder2 Insert(int index, bool value)
        {
            _wrapped.Insert(index, value);
            return this;
        }

        /// <summary>
        /// Inserts the string representation of a specified 8-bit unsigned integer into
        /// this instance at the specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">The value to insert.</param>
        /// <returns>A reference to this instance after the insert operation has completed.</returns>
        public StringBuilder2 Insert(int index, byte value)
        {
            _wrapped.Insert(index, value);
            return this;
        }

        /// <summary>
        /// Inserts the string representation of a specified Unicode character into this
        /// instance at the specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">The value to insert.</param>
        /// <returns>A reference to this instance after the insert operation has completed.</returns>
        public StringBuilder2 Insert(int index, char value)
        {
            _wrapped.Insert(index, value);
            return this;
        }

        /// <summary>
        /// Inserts the string representation of a specified array of Unicode characters
        /// into this instance at the specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">The character array to insert.</param>
        /// <returns>A reference to this instance after the insert operation has completed.</returns>
        public StringBuilder2 Insert(int index, char[] value)
        {
            _wrapped.Insert(index, value);
            return this;
        }

        /// <summary>
        /// Inserts the string representation of a decimal number into this instance
        /// at the specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">The value to insert.</param>
        /// <returns>A reference to this instance after the insert operation has completed.</returns>
        public StringBuilder2 Insert(int index, decimal value)
        {
            _wrapped.Insert(index, value);
            return this;
        }

        /// <summary>
        /// Inserts the string representation of a double-precision floating-point number
        /// into this instance at the specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">The value to insert.</param>
        /// <returns>A reference to this instance after the insert operation has completed.</returns>
        public StringBuilder2 Insert(int index, double value)
        {
            _wrapped.Insert(index, value);
            return this;
        }

        /// <summary>
        /// Inserts the string representation of a single-precision floating point number
        /// into this instance at the specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">The value to insert.</param>
        /// <returns>A reference to this instance after the insert operation has completed.</returns>
        public StringBuilder2 Insert(int index, float value)
        {
            _wrapped.Insert(index, value);
            return this;
        }

        /// <summary>
        /// Inserts the string representation of a specified 32-bit signed integer into
        /// this instance at the specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">The value to insert.</param>
        /// <returns>A reference to this instance after the insert operation has completed.</returns>
        public StringBuilder2 Insert(int index, int value)
        {
            _wrapped.Insert(index, value);
            return this;
        }

        /// <summary>
        /// Inserts the string representation of a 64-bit signed integer into this instance
        /// at the specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">The value to insert.</param>
        /// <returns>A reference to this instance after the insert operation has completed.</returns>
        public StringBuilder2 Insert(int index, long value)
        {
            _wrapped.Insert(index, value);
            return this;
        }

        /// <summary>
        /// Inserts the string representation of an object into this instance at the
        /// specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">The object to insert, or null.</param>
        /// <returns>A reference to this instance after the insert operation has completed.</returns>
        public StringBuilder2 Insert(int index, object value)
        {
            _wrapped.Insert(index, value);
            return this;
        }

        /// <summary>
        /// Inserts the string representation of a specified 8-bit signed integer into
        /// this instance at the specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">The value to insert.</param>
        /// <returns>A reference to this instance after the insert operation has completed.</returns>
        public StringBuilder2 Insert(int index, sbyte value)
        {
            _wrapped.Insert(index, value);
            return this;
        }

        /// <summary>
        /// Inserts the string representation of a specified 16-bit signed integer into
        /// this instance at the specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">The value to insert.</param>
        /// <returns>A reference to this instance after the insert operation has completed.</returns>
        public StringBuilder2 Insert(int index, short value)
        {
            _wrapped.Insert(index, value);
            return this;
        }

        /// <summary>
        /// Inserts a string into this instance at the specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">The string to insert.</param>
        /// <returns>A reference to this instance after the insert operation has completed.</returns>
        public StringBuilder2 Insert(int index, string value)
        {
            _wrapped.Insert(index, value);
            return this;
        }

        /// <summary>
        /// Inserts the string representation of a 32-bit unsigned integer into this
        /// instance at the specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">The value to insert.</param>
        /// <returns>A reference to this instance after the insert operation has completed.</returns>
        public StringBuilder2 Insert(int index, uint value)
        {
            _wrapped.Insert(index, value);
            return this;
        }

        /// <summary>
        /// Inserts the string representation of a 64-bit unsigned integer into this
        /// instance at the specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">The value to insert.</param>
        /// <returns>A reference to this instance after the insert operation has completed.</returns>
        public StringBuilder2 Insert(int index, ulong value)
        {
            _wrapped.Insert(index, value);
            return this;
        }

        /// <summary>
        /// Inserts the string representation of a 16-bit unsigned integer into this
        /// instance at the specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">The value to insert.</param>
        /// <returns>A reference to this instance after the insert operation has completed.</returns>
        public StringBuilder2 Insert(int index, ushort value)
        {
            _wrapped.Insert(index, value);
            return this;
        }

        /// <summary>
        /// Inserts one or more copies of a specified string into this instance at the
        /// specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">The string to insert.</param>
        /// <param name="count">The number of times to insert value.</param>
        /// <returns>A reference to this instance after insertion has completed.</returns>
        public StringBuilder2 Insert(int index, string value, int count)
        {
            _wrapped.Insert(index, value, count);
            return this;
        }

        /// <summary>
        /// Inserts the string representation of a specified subarray of Unicode characters
        /// into this instance at the specified character position.
        /// </summary>
        /// <param name="index">The position in this instance where insertion begins.</param>
        /// <param name="value">A character array.</param>
        /// <param name="startIndex">The starting index within value.</param>
        /// <param name="charCount">The number of characters to insert.</param>
        /// <returns>A reference to this instance after the insert operation has completed.</returns>
        public StringBuilder2 Insert(int index, char[] value, int startIndex, int charCount)
        {
            _wrapped.Insert(index, value, startIndex, charCount);
            return this;
        }

        /// <summary>
        /// Removes the specified range of characters from this instance.
        /// </summary>
        /// <param name="startIndex">The zero-based position in this instance where removal begins.</param>
        /// <param name="length">The number of characters to remove.</param>
        /// <returns>A reference to this instance after the excise operation has completed.</returns>
        public StringBuilder2 Remove(int startIndex, int length)
        {
            _wrapped.Remove(startIndex, length);
            return this;
        }

        /// <summary>
        /// Replaces all occurrences of a specified character in this instance with another
        /// specified character.
        /// </summary>
        /// <param name="oldChar">The character to replace.</param>
        /// <param name="newChar">The character that replaces oldChar.</param>
        /// <returns>A reference to this instance with oldChar replaced by newChar.</returns>
        public StringBuilder2 Replace(char oldChar, char newChar)
        {
            _wrapped.Replace(oldChar, newChar);
            return this;
        }

        /// <summary>
        /// Replaces all occurrences of a specified string in this instance with another
        /// specified string.
        /// </summary>
        /// <param name="oldValue">The string to replace.</param>
        /// <param name="newValue">The string that replaces oldValue, or null.</param>
        /// <returns>A reference to this instance with all instances of oldValue replaced by newValue.</returns>
        public StringBuilder2 Replace(string oldValue, string newValue)
        {
            _wrapped.Replace(oldValue, newValue);
            return this;
        }

        /// <summary>
        /// Replaces, within a substring of this instance, all occurrences of a specified
        /// character with another specified character.
        /// </summary>
        /// <param name="oldChar">The character to replace.</param>
        /// <param name="newChar">The character that replaces oldChar.</param>
        /// <param name="startIndex">The position in this instance where the substring begins.</param>
        /// <param name="count">The length of the substring.</param>
        /// <returns>
        /// A reference to this instance with oldChar replaced by newChar in the range
        /// from startIndex to startIndex + count -1.
        /// </returns>
        public StringBuilder2 Replace(char oldChar, char newChar, int startIndex, int count)
        {
            _wrapped.Replace(oldChar, newChar, startIndex, count);
            return this;
        }

        /// <summary>
        /// Replaces, within a substring of this instance, all occurrences of a specified
        /// string with another specified string.
        /// </summary>
        /// <param name="oldValue">The string to replace.</param>
        /// <param name="newValue">The string that replaces oldValue, or null.</param>
        /// <param name="startIndex">The position in this instance where the substring begins.</param>
        /// <param name="count">The length of the substring.</param>
        /// <returns>
        /// A reference to this instance with all instances of oldValue replaced by newValue
        /// in the range from startIndex to startIndex + count - 1.
        /// </returns>
        public StringBuilder2 Replace(string oldValue, string newValue, int startIndex, int count)
        {
            _wrapped.Replace(oldValue, newValue, startIndex, count);
            return this;
        }

        /// <summary>
        /// Converts the value of this instance to a System.String.
        /// </summary>
        /// <returns>A string whose value is the same as this instance.</returns>
        public override string ToString()
        {
            return _wrapped.ToString();
        }

        /// <summary>
        /// Writes indentation characters at current position depending on current indentation level.
        /// </summary>
        public void WriteIndent()
        {
            for (int i = 0; i < IndentLevel; i++)
                _wrapped.Append(IndentLevelContent);
        }

        /// <summary>
        /// Returns indentation scope token. Indentation level is increased upon
        /// call to this method, and it is decreased once indentation token is disposed.
        /// </summary>
        /// <returns>Indentation token</returns>
        public IndentationToken Indent()
        {
            return new IndentationToken(this);
        }

        /// <summary>
        /// Returns indentation scope token. Indentation level is increased upon
        /// call to this method, and it is decreased once indentation token is disposed.
        /// </summary>
        /// <param name="levelsToindent">Number of levels to indent from current level.</param>
        /// <returns>Indentation token</returns>
        public IndentationToken Indent(int levelsToIndent)
        {
            return new IndentationToken(this, levelsToIndent);
        }

        /// <summary>
        /// Disposable indentation token. Once instantiated, it increases indentation level of StringBuilder2,
        /// and once disposed, decreases the indentation level.
        /// </summary>
        public class IndentationToken : IDisposable
        {
            private readonly StringBuilder2 _sb;
            private StringBuilder2 stringBuilder2;
            private int _levelsToIndent;

            internal IndentationToken(StringBuilder2 sb)
            {
                _sb = sb;
                _levelsToIndent = 1;
                _sb.IndentLevel++;
            }

            internal IndentationToken(StringBuilder2 sb, int levelsToIndent)
            {
                _sb = sb;
                _levelsToIndent = levelsToIndent;
                _sb.IndentLevel += _levelsToIndent;
            }

            public void Dispose()
            {
                _sb.IndentLevel -= _levelsToIndent;
            }
        }
    }
}