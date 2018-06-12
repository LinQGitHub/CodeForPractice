using System;
using System.Globalization;

class Notes
{

    private void NotesStringProcess()
    {
        //功能：字符串转换为整型
        //依赖：using System.Globalization;
        string varX00 = "2";
        int varY00 = int.Parse(varX00, NumberStyles.Any);
        
        //功能：字符串转换为数组
        string varX11 = "2 3 4";
        string[] varY11= varX11.Split(' ');
        foreach (var item in varY11)
        {
            
        }
    }

}
