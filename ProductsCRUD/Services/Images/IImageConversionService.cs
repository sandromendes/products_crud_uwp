using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace ProductsCRUD.Services.Images
{
    public interface IImageConversionService
    {
        Task<StorageFile> ConvertBytesToStorageFile(byte[] bytes, string imageName);
        Task<BitmapImage> ConvertFileToBitmapImage(StorageFile file);
        Task<byte[]> ConvertStorageFileToByteArray(StorageFile file);
        void ValidateImage(StorageFile imageFile);
        Task<StorageFile> PickImage();
    }
}