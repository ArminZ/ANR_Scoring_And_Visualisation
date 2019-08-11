using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirNavigationRaceLive.Comps.Helper
{
    public static class ExtensionMethods
    {
        public static Image VaryQualityLevel(this Image img)
        {
            // Extrension for System.Drawing.Imaging.Image
            // used to convert an Image object to JPEG format and use the Quality parameter (to reduce its physical size)

            // convert Image to jpeg and set the Quality parameter
            const Int64 qual = 50L;

            Image retImg;
            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

            // Create an Encoder object based on the GUID
            // for the Quality parameter category.
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.
            // An EncoderParameters object has an array of EncoderParameter objects. 
            // In this case, there is only one EncoderParameter object in the array.
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, qual);
            myEncoderParameters.Param[0] = myEncoderParameter;

            // note: do not close the stream
            var stm = new MemoryStream();
            var bmp = new Bitmap(img);
            bmp.Save(stm, jgpEncoder, myEncoderParameters);
            retImg = System.Drawing.Image.FromStream(stm);
            return retImg;
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
