using System.Threading.Tasks;
using HanumanInstitute.MvvmDialogs.FileSystem;

namespace TestRoutingProject.Services;

public interface IStorageService
{
    Task<IDialogStorageFolder?> GetDownloadsFolderAsync();
}
