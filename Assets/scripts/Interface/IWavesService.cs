using System.Threading.Tasks;
public interface IWavesService
{
    Task<string> GetWaveAsync(WavesRequest waves);

    Task<bool> PutWaveAsync(ChangeWaveRequest wave);
}

