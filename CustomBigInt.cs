using System;
using System.Linq;
using static System.Int32;

namespace CustomBigInt
{
    public class CustomBigInt
    {
        private const int MaxSize = 128; // max_size of custom big integer
        private readonly int _size;
        private readonly int[] _elements;
        private const char Comma = (char) 44;
        private static bool _isFloating = false;
        private static int index;


        public CustomBigInt()
        {
            _elements = new int[MaxSize];
        }

        public CustomBigInt(int[] array)
        {
            _elements = array;
        }

        public CustomBigInt(string number)
        {
            if (number.Length > MaxSize)
            {
                _elements = new int[MaxSize];
            }

            if (number == "")
            {
                throw new ArgumentException();
            }

            if (number.Contains(","))
            {
                index = number.IndexOf(",");
                number = number.Remove(index, 1);
            }

            _elements = new int[number.Length];

            int numberLengt = number.Length - 1;

            for (int i = 0; i < number.Length; i++, numberLengt--)
            {
                _elements[i] = int.Parse(number[numberLengt].ToString());
            }
        }


        private static int GetSize(CustomBigInt value)
        {
            return value._elements.Length;
        }

        private static void Swap(CustomBigInt valueOne, CustomBigInt valueTwo)
        {
            CustomBigInt temp = valueOne;
            valueOne = valueTwo;
            valueTwo = temp;
        }

        public static bool operator ==(CustomBigInt valueOne, CustomBigInt valueTwo)
        {
            if (valueOne.Equals(valueTwo)) // check if the two numbers are equal
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(CustomBigInt valueOne, CustomBigInt valueTwo)
        {
            if (valueOne.Equals(valueTwo)) //check if the two numbers are different
            {
                return false;
            }

            return true;
        }

        public static bool operator <(CustomBigInt valueOne, CustomBigInt valueTwo)
        {
            int valueOneLenght = GetSize(valueOne);
            int valueTwoLenght = GetSize(valueTwo);

            // if lenght of the two numbers are equal
            if (valueOneLenght == valueTwoLenght)
            {
                for (int i = valueOneLenght - 1; i >= 0; i--)
                {
                    if (int.Parse(valueOne._elements[i].ToString())
                        >
                        int.Parse(valueTwo._elements[i].ToString()))
                    {
                        return false;
                    }
                    if
                    (int.Parse(valueOne._elements[i].ToString())
                     <
                     int.Parse(valueTwo._elements[i].ToString()))
                    {
                        return true;
                    }
                }
            }

            if (valueOneLenght > valueTwoLenght)
            {
                return false;
            }

            return true;
        }

        public static bool operator >(CustomBigInt valueOne, CustomBigInt valueTwo)
        {
            int valueOneLenght = GetSize(valueOne);
            int valueTwoLenght = GetSize(valueTwo);

            if (valueOneLenght == valueTwoLenght)
            {
                for (int i = valueOneLenght - 1; i >= 0; i--)
                {
                    if (int.Parse(valueOne._elements[i].ToString()) >
                        int.Parse(valueTwo._elements[i].ToString()))
                    {
                        return true;
                    }
                    if
                    (int.Parse(valueOne._elements[i].ToString())
                     < int.Parse(valueTwo._elements[i].ToString()))
                    {
                        return false;
                    }
                }
            }
            if (valueOneLenght > valueTwoLenght)
            {
                return true;
            }

            return false;
        }

        public static CustomBigInt operator +(CustomBigInt valueOne, CustomBigInt valueTwo)
        {
            string result = string.Empty;

            string valueOneString = valueOne.ToString().Trim();
            string valueTwoString = valueTwo.ToString().Trim();

            int length;

            if (valueOneString.Length > valueTwoString.Length)
            {
                length = valueOneString.Length;
            }
            else
            {
                length = valueTwoString.Length;
            }
            //making the two numbers with same lenght, empty space is replace with 0
            valueOneString = valueOneString.PadLeft(length, '0');
            valueTwoString = valueTwoString.PadLeft(length, '0');

            int remainder = 0;

            for (int i = (valueOneString.Length - 1); i >= 0; i--)
            {
                int valueOneInt = int.Parse(valueOneString[i].ToString());
                int valueTwoInt = int.Parse(valueTwoString[i].ToString());

                int add = valueOneInt + valueTwoInt + remainder;

                if (add >= 10)
                {
                    remainder = 1;
                    add = add % 10;
                }
                else
                {
                    remainder = 0;
                }

                result = add.ToString() + result;
            }

            if (remainder == 1)
            {
                result = remainder + result;
            }

            return new CustomBigInt(result);
        }


        public static CustomBigInt operator -(CustomBigInt valueOne, CustomBigInt valueTwo)
        {
            string result = string.Empty;

            if (valueOne < valueTwo)
            {
                Swap(valueOne, valueTwo);
            }

            string valueOneString = valueOne.ToString().Trim();
            string valueTwoString = valueTwo.ToString().Trim();

            int length;
            int intResult = 0;
            int sum;
            bool con = false;

            if (valueOneString.Length > valueTwoString.Length)
            {
                length = valueOneString.Length;
            }
            else
            {
                length = valueTwoString.Length;
            }

            if (valueOneString.Length > valueTwoString.Length)
            {
                valueTwoString = valueTwoString.PadLeft(length, '0');

                for (int i = 0; i <= valueOneString.Length - 1; i++)
                {
                    int valueOneInt = int.Parse(valueOneString[i].ToString());
                    int valueTwoInt = int.Parse(valueTwoString[i].ToString());

                    if (valueOneInt < valueTwoInt)
                    {
                        valueOneInt += 10;

                        con = true;
                    }
                    sum = valueOneInt - valueTwoInt;
                    result = result + sum.ToString();

                    if (con && i >= valueOneString.Length - 1)
                    {
                        if (valueOneString.Length == 2)
                        {
                            result = (int.Parse(result) - 10).ToString();
                        }
                        else
                        {
                            result = (int.Parse(result) - 100).ToString();
                        }
                    }
                }
            }

            else
            {
                valueOneString = valueOneString.PadLeft(length, '0');
                valueTwoString = valueTwoString.PadLeft(length, '0');

                for (int i = valueOneString.Length - 1; i >= 0; i--)
                {
                    int valueOneInt = int.Parse(valueOneString[i].ToString());
                    int valueTwoInt = int.Parse(valueTwoString[i].ToString());

                    if (valueOneInt < valueTwoInt)
                    {
                        valueOneInt += 10;

                        for (int j = i + 1; j < valueOneString.Length + 1; j++)
                        {
                            if (int.Parse(valueOneString[j - 1].ToString()) == 0)
                            {
                                valueOneInt = 9;
                            }
                            valueOneInt -= 1;
                        }
                    }

                    if (int.Parse(valueOneString) - int.Parse(valueTwoString) < 10)
                    {
                        sum = valueOneInt - valueTwoInt;
                        intResult = sum + intResult;
                        result = intResult.ToString();
                    }

                    else
                    {
                        sum = valueOneInt - valueTwoInt;
                        result = sum.ToString() + result;
                    }
                }
            }

            return new CustomBigInt(result);
        }

        public static CustomBigInt operator /(CustomBigInt valueOne,
            CustomBigInt valueTwo)
        {
            string result = string.Empty;

            string valueOneString = valueOne.ToString().Trim();
            string valueTwoString = valueTwo.ToString().Trim();
            CustomBigInt multyplyBy10 = new CustomBigInt("10");
            CustomBigInt zero = new CustomBigInt("0");


            if (valueOne < valueTwo)
            {
                Swap(valueOne, valueTwo);
            }

            int carry = 1;
            int carrySecond = 0;

            do
            {
                valueOne = valueOne - valueTwo;

                carry++;
            } while (valueOne > valueTwo);

            if (valueOne < valueTwo && valueOne != zero)
            {
                carry -= 1;
                valueOne = valueOne * multyplyBy10;
                do
                {
                    valueOne = valueOne - valueTwo;
                    carrySecond++;
                } while (valueOne > zero);
            }

            if (valueOne < valueTwo && valueOne.ToString() == "0")
            {
                _isFloating = true;
            }

            result = carry.ToString() + "," + carrySecond.ToString();
            return new CustomBigInt(result);
        }

        public static CustomBigInt operator *(CustomBigInt valueOne,
            CustomBigInt valueTwo)
        {
            if (valueOne < valueTwo)
            {
                Swap(valueOne, valueTwo);
            }

            string valueOneString = valueOne.ToString().Trim();
            string valueTwoString = valueTwo.ToString().Trim();

            valueOneString = valueOneString.PadLeft(valueOneString.Length + 2, '0');
            valueTwoString = valueTwoString.PadLeft(valueTwoString.Length + 2, '0');

            int[] arr = valueOneString.Select(x => int.Parse(x.ToString())).ToArray();
            int[] sum = new int[arr.Length];
            int carry = 0;

            for (int i = valueOneString.Length - 1; i >= 0; i--)
            {
                int total = arr[i] * int.Parse(valueTwoString) + carry;
                sum[i] = total % 10;

                if (total > 9)
                {
                    carry = total / 10;
                }
                else carry = 0;
            }
            string result = string.Join("", sum.SkipWhile(x => x == 0));

            return new CustomBigInt(result);
        }

        protected bool Equals(CustomBigInt other)
        {
            return _size == other._size && Equals(_elements, other._elements);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CustomBigInt) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_size * 397) ^ (_elements != null ? _elements.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            string result = "";

            if (_isFloating)
            {
                for (int i = _elements.Length - 1; i >= 0; i--)
                {
                    if (i == index - 1)
                    {
                        result += Comma;
                    }
                    result += _elements[i].ToString();
                }
                return result;
            }

            else
            {
                for (int i = _elements.Length - 1; i >= 0; i--)
                {
                    result += _elements[i].ToString();
                }
                return result;
            }
        }
    }
}