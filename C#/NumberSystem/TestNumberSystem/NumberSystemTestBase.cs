using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSA;

namespace TestNumberSystem
{
   public class NumberSystemTestBase
    {
       
         
        public NumberSystem numberSystem;
        public void Init(string numberFilePath = @"..\..\..\files\File2.txt")
        {
            numberSystem = new NumberSystem();
            numberSystem.InitializeBase(@"..\..\..\files\File1.txt");            
            numberSystem.CreateNumbers(numberFilePath);          
        }

    }
}
