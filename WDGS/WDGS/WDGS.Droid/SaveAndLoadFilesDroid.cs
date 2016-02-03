using Android.Content;
using WDGS.Droid;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveAndLoadFilesDroid))]

namespace WDGS.Droid
{   
    /*
     * Android implementation of the ISaveAndLoad shared project interface
     * Used for saving and reading files on the users device
     */ 
    public class SaveAndLoadFilesDroid : SaveAndLoadFiles
    {
        #region ISaveAndLoad implementation

        /*
         * saves an image taken with the camera to the users gallery
         * 
         * Params:
         * string fileName: the file name to save the image under
         * byte[] imageBytes: a byte array of the image data
         * 
         * Returns:
         * a string message indicating if the save was successful or failed
         */ 
        public String saveImageToGallery(string fileName, byte[] imageBytes)
        {
            var message = "";
            var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);
            var pictures = dir.AbsolutePath;
            //adding a time stamp time file name to allow saving more than one image... 
            //otherwise it overwrites the previous saved image of the same name
            string name = fileName + System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
            string filePath = System.IO.Path.Combine(pictures, name);
            try
            {
                System.IO.File.WriteAllBytes(filePath, imageBytes);
                //mediascan adds the saved image into the gallery
                var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                mediaScanIntent.SetData(Android.Net.Uri.Parse(filePath));
                Xamarin.Forms.Forms.Context.SendBroadcast(mediaScanIntent);
            }
            catch
            {
                message = "error";
            }
            return message;
        }

        /*
         * checks if the user has granted camera access
         * 
         * On android this is always true as access is granted
         * through the androidmanifest / project properties
         * 
         * Params:
         * none
         * 
         * Returns:
         * none
         */ 
        public void checkCameraAccess()
        {
            App.cameraAccessGranted = true;
        }

        #endregion
    }
}