//**************************************************************************
//  The code in this class is same as found in the XLabs on GitHub.
//  (The link is: https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/Samples/XLabs.Sample/ViewModel/CameraViewModel.cs)
//
//  The exceptions are:
//    1) The scope of the following method has been
//         changed from 'private' to 'public', so that they can be called directly
//         from another method / event in which using BindingContext to this class
//         is not possible:
//         - TakePicture()
//
//    2) The methods used for selecting videos and images
//       have been removed due to being unnecessary for the
//       WDGS app.
//**************************************************************************
using WDGS;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;

namespace XPA_PickMedia_XLabs_XFP
{
	public class Camera : ViewModel
	{
		private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
		private IMediaPicker _Mediapicker;
		private ImageSource _ImageSource;
		private Command _TakePictureCommand;
		private string _Status;

		public Camera ()
		{
			Setup ();
		}

		public ImageSource ImageSource
		{
			get { return _ImageSource; }
			set { SetProperty (ref _ImageSource, value); }
		}

		public Command TakePictureCommand
		{
			get {
				return _TakePictureCommand ?? (_TakePictureCommand =
					new Command (async () => await TakePicture (), () => true));
			}
		}

		public string Status
		{
			get { return _Status; }
			set { SetProperty (ref _Status, value); }
		}

		private void Setup()
		{
            //
			if (_Mediapicker == null) {
                var device = Resolver.Resolve<IDevice>();
                _Mediapicker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;
			}
		}

		public async Task<MediaFile> TakePicture()
		{
			Setup ();

			ImageSource = null;

            if (_Mediapicker.IsCameraAvailable)
            {
                return await _Mediapicker.TakePhotoAsync(new CameraMediaStorageOptions
                {
                    DefaultCamera = CameraDevice.Rear,
                    MaxPixelDimension = 400,
                    SaveMediaOnCapture = true
                }).ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        Status = t.Exception.InnerException.ToString();
                        DependencyService.Get<SaveAndLoadFiles>().checkCameraAccess();
                    }
                    else if (t.IsCanceled)
                    {
                        Status = "Canceled";
                    }
                    else
                    {
                        var mediaFile = t.Result;
                        ImageSource = ImageSource.FromStream(() => mediaFile.Source);
     
                        return mediaFile;
                    }

                    DependencyService.Get<SaveAndLoadFiles>().checkCameraAccess();
                    return null;
                }, _scheduler);
            }
            else
            {
                DependencyService.Get<SaveAndLoadFiles>().checkCameraAccess();
                return null;
            }
		}
	}
}

