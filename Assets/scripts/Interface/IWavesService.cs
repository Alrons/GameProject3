using System.Threading.Tasks;
public interface IWavesService
{
    Task<string> PatchWaveStrength(WavesRequest waves);

    Task<string> GetWaves(int userId);

    Task<bool> PutWaveAsync(ChangeWaveRequest wave);
}

