using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System.Linq;
using System.Threading;
public static class ConvertArabic
{
  
    public static string ConvertNumeralsToArabic2(string input)

    {

        return input = input.Replace('0', '٠')

                    .Replace('1', '۱')

                    .Replace('2', '۲')

                    .Replace('3', '۳')

                    .Replace('4', '٤')

                    .Replace('5', '۵')

                    .Replace('6', '٦')

                    .Replace('7', '٧')

                    .Replace('8', '٨')

                    .Replace('9', '٩')
                    ;
                    ;

    }




}
