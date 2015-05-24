using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NTNL.Helper
{
    class helper
    {
        public static String join(String[] strs, String glue, String tail)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < strs.Length; i++)
            {
                sb.Append(strs[i]);
                sb.Append(tail);
                if (i != strs.Length - 1)
                {
                    sb.Append(glue);
                }
             }
            return sb.ToString();
        }

        public static String join(String[] strs, String glue)
        {
            return join(strs, glue, "");
        }
        
        //?ArrayList間違ってるかも
        public static  ArrayList filledArray(Object obj, int size){
            var objs = new ArrayList();
            for(int i=0; i<size; i++){
                objs.Add(obj);
            }
            return objs;
        }
        
    }
}
