using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Coromon
{
    public class Country : Character
    {
        private String name;
        private String imgName;

        public void setName(String tempName)
        {
            this.name = tempName;
        }

        public String getName()
        {
            return this.name;
        }

        public void setImg(String newImage)
        {
            imgName = newImage;
        }

        public String getImgPath()
        {
            String fullFilePath = Directory.GetCurrentDirectory() + @"\..\..\..\images\" + imgName;
            return fullFilePath;
        }

    }
}
