using Core.Models;

namespace Services.Database.Services;


public class TestData
{
    /// <summary>
    /// Function to insert Testdata into the In-Memory-Database
    /// </summary>
    /// <param name="workService">The WorkService to use for insertions</param>
    public async static void InsertTestData(IWorkService workService)
    {
        var roomRepo = workService.Repository<Room>();
        Room? r1 = null;
        Room? r2 = null;

        if (roomRepo != null)
        {
            r1 = await roomRepo.InsertEntity(new Room
            {
                RoomNumber = "001",
                Name = "Test"
            });

            r2 = await roomRepo.InsertEntity(new Room
            {
                RoomNumber = "002",
                Name = "Test"
            });
        }

        var departmentRepo = workService.Repository<Department>();
        if (departmentRepo != null)
        {
            _ = await departmentRepo.InsertEntity(new Department
            {
                Name = "Testgericht"
            });
        }

        var displayRepo = workService.Repository<Display>();
        if (displayRepo != null)
        {
            _ = await displayRepo.InsertEntity(new Display
            {
                Name = "TestDisplay",
                RoomId = r1?.Id,
                IpStr = "127.0.0.1",
                MacStr = "00:01:02:03:04:05",
            });
            _ = await displayRepo.InsertEntity(new Display
            {
                Name = "TestDisplay2",
                RoomId = r2?.Id,
                IpStr = "127.0.0.2",
                MacStr = "38F3ABD61F7B",
            });
        }

        var clientVersionRepo = workService.Repository<ClientVersion>();
        if (clientVersionRepo != null)
        {
            _ = await clientVersionRepo.InsertEntity(new ClientVersion
            {
                Version = "0.0.1",
                Data = [0xDE, 0xAD, 0xBE, 0xEF],
                PlatformID = PlatformID.Win32NT,
                Path = ""
            });
        }

        var registerVersionRepo = workService.Repository<RegisterVersion>();
        if (registerVersionRepo != null)
        {
            _ = await registerVersionRepo.InsertEntity(new RegisterVersion
            {
                Version = "0.0.1",
                Data = [0xDE, 0xAD, 0xBE, 0xEF],
                PlatformID = PlatformID.Win32NT,
                Path = ""
            });
        }
    }
}
