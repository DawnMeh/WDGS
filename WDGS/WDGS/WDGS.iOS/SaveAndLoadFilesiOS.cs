using AVFoundation;
using Foundation;
using WDGS.iOS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveAndLoadFilesiOS))]

namespace WDGS.iOS
{
    /*
     * iOS implementation of the ISaveAndLoad shared project interface
     * Used for saving and reading files on the users device
     */
    public class SaveAndLoadFilesiOS : SaveAndLoadFiles
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
            var imageToSave = new UIImage(NSData.FromArray(imageBytes));
            imageToSave.SaveToPhotosAlbum((image, error) =>
            {
                if (error != null)
                {
                     message = "error";
                }
            });

            return message;
        }

        /*
         * checks if the user has granted camera access
         * 
         * On iOS this checks if the camera is authorized
         * and sets to false if it isn't. If the camera
         * is authorized for use by the user it sets to true
         * 
         * Params:
         * none
         * 
         * Returns:
         * none
         */ 
        public void checkCameraAccess()
        {
            if (AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video) != AVAuthorizationStatus.Authorized)
            {
                App.cameraAccessGranted = false;
            }
            else
            {
                App.cameraAccessGranted = true;
            }
        }

        #endregion
    }
}
