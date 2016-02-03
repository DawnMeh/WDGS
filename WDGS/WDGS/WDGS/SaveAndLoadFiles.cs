using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDGS
{
    /*
     * Interface to create a dependency service for the iOS and Android platforms
     * 
     * The service allows for saving and retrieving .txt files and image files,
     * and also a method for checking if camera access is granted
     */
    public interface SaveAndLoadFiles
    {
        String saveImageToGallery(string fileName, byte[] imageBytes);
        void checkCameraAccess();
    }
}
