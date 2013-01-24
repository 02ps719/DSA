using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DSA
{
    public class NumberSystem : INumberSystem
    {
        public Dictionary<char, int> SymbolNumberMap = new Dictionary<char, int>();
        public Dictionary<int,char> NumberSymbolMap = new Dictionary<int,char>();
        public int NumberSystemBASE = 10;
        public List<Number> numbers;

        public void InitializeBase(string baseFilePath)
        {
            StreamReader fileReader = new StreamReader(baseFilePath);
            string[] firstLineAsArrayOfCharacters = fileReader.ReadLine().Split(' ');            
            int index = 0;
            foreach (string symbol in firstLineAsArrayOfCharacters)
            {
                if (!SymbolNumberMap.ContainsKey(symbol[0]))
                {
                    SymbolNumberMap.Add(symbol[0], index);
                    NumberSymbolMap.Add(index, symbol[0]);
                    index++;
                }
            }
            fileReader.Close();
            NumberSystemBASE = index;
        }

        #region NumberCreation

        public void CreateNumbers(string numberFilePath)
        {
            StreamReader fileReader = new StreamReader(numberFilePath);
            if (numbers == null) numbers = new List<Number>();
            string[] sequence = null;            

            while (!fileReader.EndOfStream)
            {
                try
                {
                    sequence = fileReader.ReadLine().Split(' ');
                    numbers.Add(CreateNumber(sequence));
                }
                catch (Exception)
                {
                    throw;
                }                  
                
            }
            fileReader.Close();
        }

        public Number CreateNumber(string[] sequenceOfChars)
        {
            int radix = 0;
            radix = Int32.Parse(sequenceOfChars[0]);
            if (radix > NumberSystemBASE) throw new Exception("Base of input cant be greater than base of the system");
            return new Number(radix, GetSymbolsAsList(sequenceOfChars[1]));
        }

        #endregion NumberCreation

        #region Symbols/Numbers operations

        private List<char> GetSymbolsAsList(string sequence)
        {
            List<char> symbols = new List<char>();
            for (int i = 0; i < sequence.Length; i++)
            {
                char symbol = sequence[i];
                if (SymbolNumberMap.ContainsKey(symbol))
                    symbols.Add(symbol);
                else
                    throw new Exception("Input sequence has some unrecognizable numbers");
            }
            return symbols;
        }

        public int GetNumber(char symbol)
        {
            if (SymbolNumberMap != null)
                return SymbolNumberMap[symbol];
            else
                return -1;
        }

        public char GetSymbol(int number)
        {
            if (NumberSymbolMap != null)
                return NumberSymbolMap[number];
            else
                return ' ';
        }

        private string GetSymbolsFromCharacterSequence(string sequence)
        {
            string symbols = "";
            for (int i = 0; i < sequence.Length; i++)
            {
                symbols += GetSymbol(Int32.Parse(sequence[i].ToString()));
            }
            return symbols;
        }

        #endregion Symbols/Numbers operations

        #region conversion operations

        public Number ConvertToBase(Number number, int targetBase)
        {
            if (targetBase > NumberSystemBASE) throw new Exception("We do not know what some of the values in the target base is in our system");
            //convert number to base10
            int base10Number = ConvertToBase10(number);
            string numberSequence = ConvertFromBase10(base10Number, targetBase);
            return new Number(targetBase, GetSymbolsAsList(GetSymbolsFromCharacterSequence(numberSequence)));          
        }

        public int ConvertToBase10(Number number)
        {
            int base10Number = 0;
            int placeValue = number.Sequence.Count-1;
            for (int position = 0; position < number.Sequence.Count ; position++)
            {
                base10Number += (GetNumber(number.Sequence[position]) * (int)Math.Pow(number.Radix,placeValue));
                placeValue--;
            }
            return base10Number;

        }

        public string ConvertFromBase10(int number,int targetBase)
        {
            if (targetBase > NumberSystemBASE) throw new Exception("We do not know what some of the values in the current base is in our system");
            // if our base is 9, we would have characters representing 0 to 8. So, any base below 8 would have some character corresponding to the remainder
            // in case, the target base is 16, the remainder of say, 15, would know no meaning in our number system.

            int quotient, remainder;
            string sequence = "";
            quotient = number / targetBase;
            remainder = number % targetBase;
            while (quotient >= targetBase)
            {
                sequence = remainder.ToString() + sequence;
                remainder = quotient % targetBase;
                quotient = quotient / targetBase;                
            }
            sequence = quotient.ToString() + remainder.ToString() + sequence;
            return sequence;
            
        }

        #endregion conversion operations

        #region Printing numbers
        public void PrintNumber(Number number)
        {
            Console.WriteLine("Number is (" + string.Join<char>("", number.Sequence) + ")" + number.Radix);
        }

        public void PrintNumber(string destinationFilePath, Number number)
        {
            Stream stream = new FileStream(destinationFilePath, FileMode.OpenOrCreate);
            StreamWriter fileWriter = new StreamWriter(stream);
            fileWriter.WriteLine("(" + string.Join<char>("", number.Sequence) + ")" + number.Radix);
            fileWriter.Close();
        }
        #endregion       

        #region Comparison

        public Order Compare(Number number1, Number number2)
        {
            Number number2InFirstNumberBase = ConvertToBase(number2, number1.Radix);
            return CompareByRecursion(number1, number2InFirstNumberBase, 1, number2InFirstNumberBase.Radix, number1.Sequence.Count - 1, number2InFirstNumberBase.Sequence.Count - 1, Order.EQUAL);
        }

        private Order CompareByRecursion(Number number1, Number number2, int placeValue, int radix,int position1,int position2,Order order)
        {           
            //guards
            if (placeValue == 0) return order ;
            if (position1 < 0 && position2 < 0) return order;           

            int n1 =-1, n2 =-1;
            try
            {
                if(position1 >= 0)
                    n1 = GetNumber(number1.Sequence[position1]);
                if (position2 >= 0)
                    n2 = GetNumber(number2.Sequence[position2]);

                if (n1 * Math.Pow(radix, placeValue) > n2 * Math.Pow(radix, placeValue))                                   
                    return CompareByRecursion(number1, number2, ++placeValue, radix, --position1, --position2, Order.GREATER);                
                else if (n1 * Math.Pow(radix, placeValue) < n2 * Math.Pow(radix, placeValue))                
                    return CompareByRecursion(number1, number2, ++placeValue, radix, --position1, --position2, Order.LESSER);                
                else                
                    //since two numbers are same, pass the previous order
                    //since we start with Order.EQUAL, if all numbers have been equal, it would return equal
                    return CompareByRecursion(number1, number2, ++placeValue, radix, --position1, --position2, order);                             
            }
            catch (Exception e)
            {
                throw e;
            }            

        }
        #endregion Comparison
        
        public int Partition(Number[] numbers, int start, int end)
        {            
            int pivotIndex = GetPivotIndex1(numbers, start, end);
            Number pivotValue = numbers[pivotIndex];
            int inPlaceVirtualArrayIndex = start;

            Swap(numbers, pivotIndex, end);
            for (int i = start; i < end; i++)
            {
                if (Compare(numbers[i] ,pivotValue) == Order.LESSER)
                {
                    Swap(numbers, i, inPlaceVirtualArrayIndex);
                    inPlaceVirtualArrayIndex++;
                }
            }
            Swap(numbers, inPlaceVirtualArrayIndex, end);            
            return inPlaceVirtualArrayIndex;
        }

        public Number QuickSelect(int k)
        {
            return QuickSelect(this.numbers.ToArray(), k, numbers.Count());
        }

        public Number QuickSelect(Number[] numbers, int k, int size)
        {
            if (k == 0) return numbers[0];
            if (size == 1) return numbers[0];
            int start = 0, end;
            end = size - 1;
            while (true)
            {
                int pIndex = Partition(numbers, start, end);
                if (pIndex == k - 1) return numbers[pIndex];
                if (k - 1 < pIndex) //number to be found from 0 to pIndex - 1
                {
                    end = pIndex - 1;
                }
                else if (k - 1 > pIndex) //number to be found from pIndex + 1 to end
                {
                    start = pIndex + 1;
                }
            }
        }       

        private int GetPivotIndex(Number[] numbers, int start, int end)
        {
            int pivotIndex = start;
            int medianIndex = (start+end)/2;
            Number[] medianSequence = { numbers[start], numbers[medianIndex], numbers[end] };
            Number pivot = SortSequence(medianSequence)[1];
            if (Compare(pivot,numbers[end]) == Order.EQUAL) pivotIndex = end;
            else if (Compare(pivot,numbers[medianIndex]) == Order.EQUAL) pivotIndex = medianIndex;
            return pivotIndex;
        }

        private int GetPivotIndex1(Number[] numbers, int start, int end)
        {
            int pivotIndex = start;
            int second = start + (end - start) / 4;
            int third = end - (end - start) / 4;
            int fourth = start + (end - start) / 2;           
            //list[start], list[start+(end-start)/4], list[end-(end-start)/4], list[start+(end-start)/2], and list[end]
            Number[] medianSequence = { numbers[start], numbers[second], numbers[third], numbers[fourth], numbers[end] };
            Number pivot = SortSequence(medianSequence)[3];
            if (Compare(pivot, numbers[end]) == Order.EQUAL) pivotIndex = end;
            else if (Compare(pivot, numbers[second]) == Order.EQUAL) pivotIndex = second;
            else if (Compare(pivot, numbers[third]) == Order.EQUAL) pivotIndex = third;
            else if (Compare(pivot, numbers[fourth]) == Order.EQUAL) pivotIndex = fourth;
            return pivotIndex;
        }

        private Number[] SortSequence(Number[] numbers)
        {                    
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (Compare(numbers[i],numbers[j]) == Order.GREATER)
                    {
                        Swap(numbers, i, j);
                    }
                }
            }
            return numbers;
        }

        private void Swap(Number[] numbers,int pos1,int pos2)
        {
            Number temp;
            temp = numbers[pos1];
            numbers[pos1] = numbers[pos2];
            numbers[pos2] = temp;
        }       

       
    }
}
