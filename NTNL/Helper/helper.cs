using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTNL.Helper
{
    class helper
    {
        public static String join(String[] strs, String glue, String tail)
        {
            StringBuilder sb = new StringBuilder();
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
        /*
        public static  ArrayList<T> filledArray(T obj, int size){
            ArrayList<T> objs = new ArrayList<T>();
            for(int i=0; i<size; i++){
                objs.add(obj);
            }
            return objs;
        }
         */
    }
}
