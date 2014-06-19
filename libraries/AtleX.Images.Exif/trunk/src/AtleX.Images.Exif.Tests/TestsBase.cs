using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Tests
{
    public abstract class TestsBase
    {

        public abstract string TestImageFileName
        {
            get;
        }

        public string InvalidFilePng
        {
            get
            {
                return @"..\..\..\..\Testfiles\Invalid\invalid.png";
            }
        }

        public string NonExistantFile
        {
            get
            {
                return @".\image.unknown";
            }
        }

        public static Stream OpenAsStream(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            return fs;
        }
    }
}
