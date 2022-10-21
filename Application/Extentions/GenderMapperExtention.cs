using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extentions
{
    public static class GenderMapperExtention
    {
        public static string MapGender(this char g)
        {
            string gender = "";
            if (g == 'F' || g == 'f')
            {
                gender = "Female";
            }
            else if (g == 'M' || g == 'm')
            {
                gender = "Male";
            }
            else
            {
                gender = "Other";
            }
            return gender;
        }
    }
}
