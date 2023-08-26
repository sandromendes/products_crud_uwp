using ProductsCRUD.Exceptions;
using System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using System.IO;
using Windows.Storage.Pickers;

namespace ProductsCRUD.Services.Images
{
    public class ImageConversionService : IImageConversionService
    {
        public async Task<byte[]> ConvertStorageFileToByteArray(StorageFile file)
        {
            using (var inputStream = await file.OpenSequentialReadAsync())
            {
                var readStream = inputStream.AsStreamForRead();
                byte[] buffer = new byte[readStream.Length];
                await readStream.ReadAsync(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        public async Task<StorageFile> ConvertBytesToStorageFile(byte[] bytes, string imageName)
        {
            StorageFile tempFile = await ApplicationData
                .Current
                .TemporaryFolder
                .CreateFileAsync(imageName, CreationCollisionOption.GenerateUniqueName);

            using (IRandomAccessStream stream = await tempFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (DataWriter writer = new DataWriter(stream))
                {
                    writer.WriteBytes(bytes);
                    await writer.StoreAsync();
                }
            }

            return tempFile;
        }

        public async void ValidateImage(StorageFile imageFile)
        {
            var byteImage = await ConvertStorageFileToByteArray(imageFile);

            if (byteImage == null) return;

            if (byteImage.Length <= 1024 * 1024) // 1 MB
                return;

            throw new InvalidImageException();
        }

        public async Task<BitmapImage> ConvertFileToBitmapImage(StorageFile file)
        {
            var bitmapImage = new BitmapImage();
            using (var stream = await file.OpenAsync(FileAccessMode.Read))
            {
                await bitmapImage.SetSourceAsync(stream);
            }
            return bitmapImage;
        }

        public async Task<StorageFile> PickImage()
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            return await picker.PickSingleFileAsync();
        }
    }
}
