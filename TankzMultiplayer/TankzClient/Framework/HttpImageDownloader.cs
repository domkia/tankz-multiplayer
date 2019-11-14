using System.Drawing;
using System.IO;
using System.Net;

namespace TankzClient.Framework
{
    class HttpImageDownloader
    {
        public static Bitmap GetBitmapFromURL(string url, Size size)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new System.Exception("Url is null of empty");
            }
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream imageStream = response.GetResponseStream();
            Bitmap bitmap = new Bitmap(imageStream);
            return new Bitmap(bitmap, size);
        }
    }
}
