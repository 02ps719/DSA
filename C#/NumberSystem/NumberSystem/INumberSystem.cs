using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DSA
{    
    interface INumberSystem
    {
         void InitializeBase(string baseFilePath);

         void CreateNumbers(string numberFilePath);

         int GetNumber(char symbol);

         char GetSymbol(int number);

         Number CreateNumber(string[] sequenceOfChars);

         void PrintNumber(Number number);

         Number ConvertToBase(Number number, int targetBase);

         Order Compare(Number number1, Number number2);

         int Partition(Number[] numbers, int start, int end);

         Number QuickSelect(Number[] numbers, int size, int k);

    }

    public enum Order
    {
        GREATER,LESSER,EQUAL
    }
}
