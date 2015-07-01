using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Media.Imaging;
using System.Net;
using System.Net.Cache;
using System.IO;

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

        /// <summary>
        /// string to long
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long StringToLong(String str)
        {
            long id = long.Parse(str);

            return id;
        }

        public static Task<BitmapImage> GetImage(Uri uri)
        {
            return Task.Run(() =>
            {
                var wc = new WebClient { CachePolicy = new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable) };
                try
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = new MemoryStream(wc.DownloadData(uri));
                    image.EndInit();
                    image.Freeze();
                    return image;
                }
                catch (WebException) { }
                catch (IOException) { }
                catch (InvalidOperationException) { }
                finally
                {
                    wc.Dispose();
                }
                return null;
            });
        }

        public static Task<BitmapImage> GetImage(string uri)
        {
            return GetImage(new Uri(uri));
        }
    }
}
