using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSA
{
    class Program
    {
        

        /*Assumptions:
         * This system is based on the following assumptions.
         * 1. The numbers in the input file are all valid. Program doesnt check for validity
         *        Validity means - if a number is of a valid base(base < the base of the system), 
         *                          then it is expressed using symbols of that base only.
         *         
         * 2.If any number is having a base greater than the base of the system, the system just shuts down. 
         * 3.If any number is expressed using symbols which the system doesnt understand, the system just shuts down.  
         * 4.If any of the file is not found in the given path, the system shuts down. 
         * 5.This uses in-place partition method.
         * 6.The QuickSelect method has been exposed in two different ways. One is as per the problem statment and another is 
         *   just as QuickSelect(int k) as the instance of the numbersystem would have all other information like numbers[], size         * 
         */

        //Sample execution commands
        /*
         *  // Valid passes
         *  mono NumberSystem.exe ./files/File1.txt ./files/File2.txt 3 ./files/File3.txt    
         *  O/P:Press enter to exit
         *  mono NumberSystem.exe ./files/File1.txt ./files/File2.txt 1 ./files/File3.txt
         *  mono NumberSystem.exe ./files/File1.txt ./files/File2.txt 2 ./files/File3.txt    
         
         *  // k > size/base of the number system
         *  mono NumberSystem.exe ./files/File1.txt ./files/File2.txt 15 ./files/File3.txt       
         *  
         * //Invalid base detected
         *  mono NumberSystem.exe ./files/File1.txt ./files/Fie2_Invalid_base.txt 3 ./files/File3.txt
            O/P : Base of input cant be greater than base of the system
            Press enter to exit         
            
         * //Invalid characters detected
         *  mono NumberSystem.exe ./files/File1.txt ./files/Fie2_Invalid_characters.txt 3 ./files/File3.txt
            O/P:Input sequence has some unrecognizable numbers
                Press enter to exit
         */

        static void Main(string[] args)
        {
            string baseFilePath, numberFilePath, resultFilePath;
            int k;  
            try
            {
                baseFilePath = args[0];
                numberFilePath = args[1];
                resultFilePath = args[3];
                k = Int32.Parse(args[2]);
                NumberSystem numberSystem = new NumberSystem();
                numberSystem.InitializeBase(baseFilePath);
                numberSystem.CreateNumbers(numberFilePath);
                if (k > numberSystem.numbers.Count)
                    k = numberSystem.numbers.Count / 2;
                numberSystem.PrintNumber(resultFilePath, numberSystem.QuickSelect(k));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Press enter to exit");
            Console.ReadKey();
                

        }
    }
}
